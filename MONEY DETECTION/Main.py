from PyQt5 import uic
from PyQt5.QtWidgets import QApplication, QMainWindow, QFileDialog
from PyQt5.QtCore import QThread, pyqtSignal, QFile
from PyQt5.QtGui import QImage, QPixmap
import cv2, sys, json, socket, os, time
import numpy as np
from multiprocessing import Process, Event
import warnings
warnings.filterwarnings("ignore")

# 1. tao noi luu ket qua
output_path = "output"
message = ""
stop_requested = False  # dung viec chay camera
detected_objects = []  # danh sach luu menh gia de nhan dien duoc

if not os.path.exists(output_path):
    os.makedirs(output_path)

# 2. tao server de nhan yeu cau tu client khi chon start/stop 
class ServerThread(QThread):
    message_received = pyqtSignal(str)
    message_to_send = ""

    def run(self):
        HOST = '127.0.0.1'
        PORT = 8888

        server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        server_socket.bind((HOST, PORT))
        server_socket.listen(1)

        print(f"Server is listening on {HOST}:{PORT}")
        while True:
            try:
                conn, addr = server_socket.accept()
                print(f"Connected by {addr}")

                while True:
                    conn.settimeout(5)  # cho ket noi trong 5s, qua t/g se tu dong dong ket noi
                    try:
                        data = conn.recv(1024)  # nhan du lieu tu nguoi dung
                        if not data:
                            print("No data received. Closing connection.")
                            conn.close()
                            break

                        message = data.decode()
                        print(f"Message received: {message}")
                        self.message_received.emit(message)  # phat tin hieu den GUI (giao dien do hoa nguoi dung)

                        # gui phan hoi
                        if message == "START":
                            conn.sendall(b"START_ACK")
                        elif message == "STOP":
                            conn.sendall(b"STOP_ACK")
                        else:
                            conn.sendall(b"UNKNOWN_COMMAND")
                    except socket.timeout:
                        print("Connection timeout. Closing connection.")
                        conn.close()
                        break
            except Exception as e:
                print(f"Server error: {e}")

modelfile = "yolov5/data/best.pt" # duong dan den yolov5

# 3. tao va su dung yolov5 de nhan dien doi tuong
class YoloModel:
    # 3.1 khoi tao lop yolo de lay duong dan file 
    def __init__(self, file):
        global model_file_path
        model_file_path = ""
        global modelTrain
        modelTrain = None
        if file != "":
            model_file_path = file
            self.load_config()
    # 3.2 tai mo hinh yolov5 tu PyTorch qua thu vien torch
    def load_config(self):
        import torch
        global model_file_path
        global modelTrain
        try:
            modelTrain = torch.hub.load("yolov5", "custom", path=model_file_path, source='local') 
            modelTrain.conf = 0.85  # nguong confidence duoc giu lai
            modelTrain.iou = 0.45  # loai bo cac du doan trung lap
            modelTrain.agnostic = True  # khong phan biet cac doi tuong khi dung nms
            modelTrain.multi_label = False  # khong cho phep mang nhieu nhan
            modelTrain.classes = None  # khong loc theo bat ky lop nao
            modelTrain.max_det = 100  # gioi han 100 doi tuong duoc tra ve trong anh
            modelTrain.amp = True  # kich hoat amp de tang hieu suat tinh toan
            modelTrain.cpu()  # dung CPU thay gi GPU

            print("MODEL INIT SUCESSFULLY !!!")
        except Exception as Error:
            model_file_path = ""
            print("MODEL INIT FAILED : " + Error)
            pass
    # 3.3 nhan anh va du doan  
    def detector(self, frame):
        global model_file_path
        global modelTrain
        try:
            if model_file_path == "": # mo hinh chua duoc tai 
                return ""
            else:
                result = modelTrain(frame) # mo hinh da duoc tai
                return result 
        except:
            return ""

# 4. xu ly webcam va phat hien doi tuong
class ThreadClass(QThread):
    ImageUpdate = pyqtSignal(np.ndarray)
    global yolo
    print("Initializing AI model, please wait ...")
    yolo = YoloModel(modelfile) # khoi tao doi tuong yolo voi tham so la duong dan den noi luu yolov5
    
    # 4.1 doc video tu webcam, phat hien va xu ly dua ra ket qua
    def run(self):
        global webcamStatus, confidence, stop_requested, detected_objects
        Capture = cv2.VideoCapture(0)
        count = 10
        save = False
        id = 0
        self.ThreadActive = True
        webcamStatus = True

        detection_done = False
        last_detect_time = 0

        last_id = None # id cuoi cua 1 doi tuong
        last_object = None # nhan cuoi cua 1 doi tuong

        while self.ThreadActive and not stop_requested:
            try:
                ret, frame_cap = Capture.read()
                if ret:
                    flip_frame = frame_cap
                    frame_rgb = cv2.cvtColor(flip_frame, cv2.COLOR_BGR2RGB)

                    if detection_done and (time.time() - last_detect_time > 2):
                        detection_done = False

                    if not detection_done:
                        results = yolo.detector(frame_rgb)

                        if count >= 10:
                            if save == False:
                                save = True
                                id += 1
                            cv2.putText(flip_frame, f"Please give me the money...ID : {id}", (10, 30), cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 0, 255), 2)
                        else:
                            count += 1

                        if results != "":
                            try:
                                json_data = results.pandas().xyxy[0].to_json(orient="records")
                                if json_data != "[]":
                                    count = 0
                                    data = json.loads(json_data)
                                    data = sorted(data, key=lambda x: x['confidence'], reverse=True)
                                    object_data = data[0]

                                    x1 = int(object_data['xmin'])
                                    y1 = int(object_data['ymin'])
                                    x2 = int(object_data['xmax'])
                                    y2 = int(object_data['ymax'])
                                    scores = float(object_data['confidence'])
                                    label = object_data['name']

                                    # kiem tra id hoac nhan cua doi tuong hien tai co trung voi doi tuong truoc do khong
                                    if id != last_id or label != last_object:
                                        print(f"Current ID: {id}")
                                        print(f"Object: {label}, Score: {scores}")

                                        # kiem tra label co phai kieu string va chua dau ','
                                        if isinstance(label, str):
                                            label = label.replace(",", "")  # loai bo dau phay
                                            label = int(label)  # chuyen thanh so nguyen 

                                        # kiem tra co trung lap label va them vao danh sach neu chua co
                                        if id != last_id or label != last_object:
                                            detected_objects.append(label)
                                        last_id = id
                                        last_object = label

                                    detection_done = True # hoan thanh nhan dien cho 1 chu ky (1 id)
                                    last_detect_time = time.time() # luu lai thoi gian cua lan nhan dien gan nhat

                                    # hien thi ket qua 
                                    cv2.rectangle(flip_frame, (x1, y1), (x2, y2), (255, 0, 0), 2)
                                    if scores >= confidence:
                                        self.add_text(flip_frame, f"{label}", (x1 + 5, y1 + 30), "OK")
                                        cropped_image = flip_frame[y1:y2, x1:x2]
                                        if save:
                                            save = False
                                            cv2.imwrite("output/" + str(label) + "_" + str(id) + ".png", cropped_image)
                                        cv2.putText(flip_frame, f"I received the money !!!", (10, 30), cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 255, 0), 2)
                                    else:
                                        self.add_text(flip_frame, f"{label}", (x1 + 5, y1 + 30), "NG")

                            except Exception as e:
                                print(f"Error processing detection: {e}")
                                pass
                    self.ImageUpdate.emit(flip_frame)
            except:
                pass

        # ket thuc nhan dien, tra ve danh sach ket qua 
        print(f"Detection stopped. Final detected objects: {detected_objects}")
        Capture.release()
        cv2.destroyAllWindows()

    def stop(self):
        global stop_requested
        self.ThreadActive = False
        stop_requested = True  # ket thuc qua trinh nhan dien
        self.quit()

        cv2.destroyAllWindows() # giai phong tai nguyen cua opencv

        global modelTrain
        modelTrain = None  # reset lai model yolov5 de giai phong GPU

    # 4.3 them van ban vao khong hinh theo tung ket qua
    def add_text(self,frame, text, position, result):
        # dieu chinh van ban va cac thong so hien thi
        try:
            font = cv2.FONT_HERSHEY_SIMPLEX
            font_scale = 1
            text_color = (255, 255, 255)  # chu mau trang
            if result == "OK":
                background_color = (0, 255, 0)  # nen xanh neu Ok
            else:
                background_color = (0, 0, 255)  # nguoc lai nen do
            thickness = 2

            # tao hinh chu nhat voi nen do
            text_width, text_height = cv2.getTextSize(text, font, font_scale, thickness)[0]
            background_width = text_width + 10
            background_height = text_height + 10
            background_position = position[0], position[1] - text_height - 5
            background_end_position = background_position[0] + background_width, background_position[1] + background_height
            cv2.rectangle(frame, background_position, background_end_position, background_color, -1)

            # them van ban vao anh
            image_with_text = cv2.putText(frame, text, position, font, font_scale, text_color, thickness)
        except:
            pass

# 5. quan ly thoi gian
class TimerThread(QThread):
    time_up = pyqtSignal()

    def run(self):
        self.timer_event = Event()
        self.timer_event.wait(timeout=180)  # cho 180 giay
        if not self.timer_event.is_set():
            self.time_up.emit()  # phat tin hieu khi het thoi gian

    def stop(self):
        self.timer_event.set()

# 6. giao dien nguoi dung va quan ly cac chuc nang
class MainWindow(QMainWindow):
    # 6.1 khoi tao lop MainWindow, quan ly thanh phan ui va thread
    def __init__(self):
        super().__init__()
        self.ui = uic.loadUi("UI/mainwindow.ui", self)

        self.server_thread = ServerThread()
        self.server_thread.message_received.connect(self.handle_message)
        self.server_thread.start()
        
        self.btn_start.clicked.connect(self.StartWebCam)
        self.btn_stop.clicked.connect(self.StopWebcam)

        self.Worker1_Opencv = ThreadClass()

        self.TimerThread = TimerThread()  # them thread de quan ly thoi gian
        self.TimerThread.time_up.connect(self.handle_time_up)

        global webcamStatus
        webcamStatus = False
        self.oldStatus = False

        global confidence
        confidence = 0.85

    # 6.2 tu dong ket thuc chuong trinh khi het gio
    def handle_time_up(self):
        print("Time is up!")
        self.StopWebcam() # dung webcam
        QApplication.quit()  # ket thuc chuong trinh

    # 6.3 xu ly tin hieu nhan duoc tu server
    def handle_message(self, message):
        print(f"Received message: {message}")
        if "START" in message:
            print("Camera Open")
            self.StartWebCam()
        elif "STOP" in message:
            print("Camera Close")
            self.StopWebcam()

    # 6.4 cap nhat trang thai webcam
    def main_loop(self):
        try:
            global message
            if len(message) > 0:
                print(message)

            global webcamStatus
            if self.oldStatus != webcamStatus:
                self.oldStatus = webcamStatus
                if webcamStatus:
                    self.lbWebcam.setStyleSheet('color:#00ff7f;')
                    self.lbWebcam.setText("STATUS : RUNNING")
                else:
                    self.lbWebcam.setStyleSheet('color:#ff5c5f;')
                    self.lbWebcam.setText("STATUS : STOPPED")
        except:
            pass

    # 6.5 hien thi video tu webcam de nhan dien
    def ShowVideo(self, Image):
        try:
            original = self.cvt_cv_qt(Image)
            self.disp_main.setPixmap(original)
            self.disp_main.setScaledContents(True)
        except:
            pass

    # 6.6 chuyen doi hinh anh tu dinh dang BGR(opencv) sang TGB(qt) de luu tru, xu ly mau sac, chuyen thanh QImage de thao tac, luu hinh anh, sang QPixmap de hien thi hinh anh tren giao dien nguoi dung
    def cvt_cv_qt(self, Image):
        rgb_img = cv2.cvtColor(src=Image, code=cv2.COLOR_BGR2RGB)
        h, w, ch = rgb_img.shape
        bytes_per_line = ch * w
        cvt2QtFormat = QImage(rgb_img.data, w, h, bytes_per_line, QImage.Format_RGB888)
        pixmap = QPixmap.fromImage(cvt2QtFormat)
        return pixmap  

    # 6.7 start webcam
    def StartWebCam(self):
        try:
            # lap qua cac tep trong thu muc va xoa truoc khi luu moi
            for file_name in os.listdir(output_path):
                if file_name.endswith('.jpg') or file_name.endswith('.png') or file_name.endswith('.jpeg'):
                    file_path = os.path.join(output_path, file_name)
                    os.remove(file_path) 
            # thay doi mau sac va hien thi trang thai
            self.lbWebcam.setStyleSheet('color:#ffaa00;')
            self.lbWebcam.setText("STATUS : WAITING")
            # cap nhat trang thai cac nut
            self.btn_stop.setEnabled(True)
            self.btn_start.setEnabled(False)
            # khoi chay thread xu ly video
            self.Worker1_Opencv.ImageUpdate.connect(self.ShowVideo)
            self.Worker1_Opencv.start()

            self.TimerThread.start() # dem nguoc thoi gian
        
        # in loi va cap nhat lai trang thai
        except Exception as error:
            print(error)
            self.btn_start.setEnabled(True)
            self.btn_stop.setEnabled(True)
            pass

    # 6.8 stop webcam
    def StopWebcam(self):
        try:
            self.lbWebcam.setStyleSheet('color:#ffaa00;')
            self.lbWebcam.setText("STATUS : WAITING")
            self.btn_start.setEnabled(True)
            self.btn_stop.setEnabled(False)

            # dung thread dem nguoc
            self.TimerThread.stop()
            print("Timer stopped")

            # dung thread xu ly video
            self.Worker1_Opencv.stop()
            print("Webcam stopped")

        # thong bao loi va cap nhat lai trang thai 
        except Exception as error:
            print(error)
            self.btn_start.setEnabled(True)
            self.btn_stop.setEnabled(True)
            pass

    # 6.9 ket thuc chuong trinh 
    def Close_software(self):
        self.Worker1_Opencv.stop()
        sys.exit(app.exec_())

# 7. chay chuong trinh
if __name__ == "__main__":
    app = QApplication([])
    window = MainWindow()
    window.show()
    app.exec_()