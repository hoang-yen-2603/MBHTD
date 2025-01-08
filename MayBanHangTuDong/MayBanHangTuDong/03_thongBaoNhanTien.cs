using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayBanHangTuDong
{
    public partial class thongBaoNhanTien : Form
    {
        public thongBaoNhanTien()
        {
            InitializeComponent();
        }
        private void label2_Click(object sender, EventArgs e){}

        //Hàm xử lý nhận diện
        private void xuLyNhanDien(){

            // Tiến hành các hành động trong thongBaoNhanTien_Load
            try
            {
                // Sửa theo Đường dẫn đến Python và script
                string pythonPath = @"C:\Users\Hoang Yen\AppData\Local\Programs\Python\Python312\python.exe";
                string scriptDirectory = @"C:\MBHTD\MONEY DETECTION";
                string scriptPath = Path.Combine(scriptDirectory, "Main.py");

                // Tạo đường dẫn log file
                string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "debug_log.txt");

                // Ghi log khởi động process
                File.AppendAllText(logFilePath, $"[{DateTime.Now}] Starting Python process...\n");

                // Cấu hình process
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    Arguments = $"\"{scriptPath}\"",
                    WorkingDirectory = scriptDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Khởi chạy process
                using (Process process = Process.Start(startInfo))
                {
                    if (process == null)
                    {
                        MessageBox.Show("Không thể khởi chạy Python script.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Đọc dữ liệu đầu ra không đồng bộ
                    Task<string> outputTask = process.StandardOutput.ReadToEndAsync();
                    Task<string> errorTask = process.StandardError.ReadToEndAsync();

                    process.WaitForExit();
                    process.Dispose();

                    // Đợi process hoàn tất hoặc timeout
                    string output = outputTask.Result;
                    string error = errorTask.Result;

                    // Ghi log kết quả
                    File.AppendAllText(logFilePath, $"[{DateTime.Now}] Output: {output}\n");
                    File.AppendAllText(logFilePath, $"[{DateTime.Now}] Error: {error}\n");

                    string detectedObjects = "";

                    if (!string.IsNullOrEmpty(output))
                    {
                        if (output.Contains("Detection stopped"))
                        {
                            // Tách thông tin danh sách mệnh giá
                            int startIndex = output.IndexOf("Final detected objects:") + "Final detected objects:".Length;
                            detectedObjects = output.Substring(startIndex).Trim();
                            tinhTongTien(detectedObjects);
                            ThongBaoKetQua TBKQ = new ThongBaoKetQua();
                            openForm.ShowNextForm(this, TBKQ);
                        }
                        else if (output.Contains("Object"))
                        {
                            // Kết quả hợp lệ
                            MessageBox.Show("Nhận diện thành công:\n" + output, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            ThongBaoGDKTC thongBaoGDKTC = new ThongBaoGDKTC();              //Mở form thông báo giao dịch không thành công
                            openForm.ShowNextForm(this, thongBaoGDKTC);
                        }
                    }
                }
                // Ghi log hoàn tất
                File.AppendAllText(logFilePath, $"[{DateTime.Now}] Process completed successfully.\n");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung
                string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "debug_log.txt");
                File.AppendAllText(logFilePath, $"[{DateTime.Now}] Error: {ex.Message}\n");
                MessageBox.Show("Error: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Hàm chuyển danh sách tờ tiền đưa vào thành int và tính tổng lại
        static void tinhTongTien(string input)
        {
            // Loại bỏ dấu ngoặc vuông và dấu nháy đơn
            string cleanedInput = input.Trim('[', ']', ' ').Replace("'", "").Replace("\"", "");

            // Tách các phần tử bằng dấu phẩy
            string[] elements = cleanedInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Lặp qua từng phần tử
            foreach (var element in elements)
            {
                // Xử lý loại bỏ khoảng trắng và chuyển thành số
                string cleanedElement = element.Trim().Replace(",", "").Replace("'", "").Replace("\"", "");

                if (decimal.TryParse(cleanedElement, out decimal menhGia))
                {
                    SoToTien.DSToTienVao.Add(menhGia);
                    totalMoney.total += menhGia;
                }
                else
                {
                    Console.WriteLine($"Không thể chuyển đổi: {element}");
                }
            }
        }
        private void thongBaoNhanTien_Load(object sender, EventArgs e)
        {
            label3.Text = $" {totalMoney.total:n0} VND";
            // Tạo Timer với thời gian 3 giây (3000 milliseconds)
            Timer timer = new Timer();
            timer.Interval = 3000; // 3 giây
            timer.Tick += (s, ev) =>
            {
                // Dừng Timer sau khi đã hiển thị đủ 3 giây
                timer.Stop();

                // Gọi MoneyDetection
                xuLyNhanDien();
            };
            timer.Start();
        }
    }
}
