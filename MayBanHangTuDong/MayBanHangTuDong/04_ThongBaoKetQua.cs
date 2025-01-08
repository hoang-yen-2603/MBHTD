using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayBanHangTuDong
{
    public partial class ThongBaoKetQua : Form
    {
        public ThongBaoKetQua()
        {
            InitializeComponent();

            this.Load += async (sender, e) =>
            {
                Maybanhang mayBH = new Maybanhang();
                await openForm.LoadAndNavigateAsync(this, mayBH);
            };
        }

        //Hàm thông báo kết quả sau khi nhận tiền
        private void KetQuaThongBao()
        {
            if (totalMoney.total == DanhSachSPMua.tongTien)
            {
                // Gọi hàm ghi vào bảng Bill trong database
                int tongSL = DanhSachSPMua.danhSachSanPham.Sum(sp => sp.soLuong);
                ghiHoaDon(tongSL, DanhSachSPMua.tongTien, DateTime.Now);

                int maHD = layMaHD();
                foreach (var item in DanhSachSPMua.danhSachSanPham)
                {
                    ghiChiTietHoaDon(maHD, item.maSP, item.tenSP, item.gia, item.soLuong, (item.gia * item.soLuong));
                    capNhatSoLuongSP(item.maSP, item.soLuong);
                }
                capNhatDSToTienVao();
                label2.Text = $"GIAO DỊCH THÀNH CÔNG, QUÝ KHÁCH CÓ THỂ NHẬN NƯỚC.";
                totalMoney.total = 0;                       //reset lại số tờ tiền đưa vào
            }
            else if (totalMoney.total > DanhSachSPMua.tongTien)
            {
                // Gọi hàm ghi vào bảng Bill trong database
                int tongSL = DanhSachSPMua.danhSachSanPham.Sum(sp => sp.soLuong);
                ghiHoaDon(tongSL, DanhSachSPMua.tongTien, DateTime.Now);

                int maHD = layMaHD();
                foreach (var item in DanhSachSPMua.danhSachSanPham)
                {
                    ghiChiTietHoaDon(maHD, item.maSP, item.tenSP, item.gia, item.soLuong, (item.gia * item.soLuong));
                    capNhatSoLuongSP (item.maSP, item.soLuong);
                }
                capNhatDSToTienVao();
                totalMoney.total = totalMoney.total - DanhSachSPMua.tongTien;   //Cập nhật lại số tiền dư của khách hàng đưa vào
                soToTienTrongKho();
                var ketQua = TraLaiTien(totalMoney.total, SoToTien.myDictionary);
                foreach (var item in ketQua)
                {
                    capNhatTienTrongKho(item.Key, item.Value);
                }
                label2.Text = $"GIAO DỊCH THÀNH CÔNG, QUÝ KHÁCH CÓ THỂ NHẬN NƯỚC. \n \nTIỀN THỪA NHẬN LẠI: {totalMoney.total:n0} VND";
                totalMoney.total = 0;
            }
            else if (totalMoney.total < DanhSachSPMua.tongTien)
            {
                label2.Text = $"GIAO DỊCH KHÔNG THÀNH CÔNG DO SỐ TIỀN ĐƯA VÀO KHÔNG ĐỦ. \n \nVUI LÒNG NHẬN LẠI TIỀN: {totalMoney.total:n0} VND";
                totalMoney.total = 0;
            }
        }
        
        //Hàm ghi giao dịch vào cơ sở dữ liệu
        private void ghiHoaDon(int tongSLMua, decimal thanhTien, DateTime thoiGan)
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để thêm dữ liệu vào bảng "bill"
                string query = "INSERT INTO Bill (tongSLMua, thanhTien, thoiGian) " +
                               "VALUES (@tongSLMua, @thanhThien, @thoiGian)";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm các tham số vào câu lệnh SQL để bảo vệ khỏi SQL Injection
                cmd.Parameters.AddWithValue("@tongSLMua", tongSLMua);
                cmd.Parameters.AddWithValue("@thanhThien", thanhTien);
                cmd.Parameters.AddWithValue("@thoiGian", thoiGan);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Đảm bảo kết nối được đóng khi hoàn thành
                Connection.disconnect();
            }
        }

        //hàm lấy mã hóa đơn từ cơ sở dữ liệu
        private int layMaHD()
        {
            int maHD = 0; ;
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để thêm dữ liệu vào bảng "bill"
                string query = "SELECT TOP 1 maHD FROM Bill ORDER BY maHD DESC";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thực thi câu lệnh SQL để lấy kết quả
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    maHD = Convert.ToInt32(result); // Chuyển đổi kết quả về kiểu int
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Đảm bảo kết nối được đóng khi hoàn thành
                Connection.disconnect();
            }
            return maHD;
        }

        //Hàm ghi chi tiết danh sách sản phẩm đã mua vào cơ sở dữ liệu
        private void ghiChiTietHoaDon(int maHD, string maSP, string tenSP, decimal gia, int soLuong, decimal tongTien)
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để thêm dữ liệu vào bảng "bill"
                string query = "INSERT INTO DetailsBill (maHD, maSP, tenSP, gia, soLuong, tongTien) " +
                               "VALUES (@maHD, @maSP, @tenSp, @gia, @soLuong, @tongTien)";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm các tham số vào câu lệnh SQL để bảo vệ khỏi SQL Injection
                cmd.Parameters.AddWithValue("@maHD", maHD);
                cmd.Parameters.AddWithValue("@maSP", maSP);
                cmd.Parameters.AddWithValue("@tenSp", tenSP);
                cmd.Parameters.AddWithValue("@gia", gia);
                cmd.Parameters.AddWithValue("@soLuong", soLuong);
                cmd.Parameters.AddWithValue("@tongTien", tongTien);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Đảm bảo kết nối được đóng khi hoàn thành
                Connection.disconnect();
            }
        }
        
        //Hàm cập nhật lại số lượng của sản phẩm trong cơ sở dữ liệu sau khi giao dịch thành công
        private void capNhatSoLuongSP(string maSP, int soLuongGiam)
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để cập nhật số lượng sản phẩm trong bảng "product"
                string query = "UPDATE Product SET tongSL = tongSL - @soLuongGiam WHERE maSP LIKE @maSP";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@maSP", maSP);
                cmd.Parameters.AddWithValue("@soLuongGiam", soLuongGiam);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu xảy ra vấn đề
                MessageBox.Show("Lỗi: capNhatSoLuong" + ex.Message);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi thực hiện xong
                Connection.disconnect();
            }
        }

        //Hàm cập nhật số lượng tờ tiền đưa vào
        static void capNhatDSToTienVao()
        {
            foreach (var menhGia in SoToTien.DSToTienVao)
            {
                capNhatToTienVao(menhGia); 
            }
        }

        //Hàm cập nhật số lượng tờ tiền đưa vào vào trong cơ sở dữ liệu
        static void capNhatToTienVao(decimal menhGia)
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để cập nhật số lượng sản phẩm trong bảng "product"
                string query = "UPDATE Denomination SET slToTien = slToTien + 1 WHERE menhGia = @menhGia";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@menhGia", menhGia); ;

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu xảy ra vấn đề
                MessageBox.Show("Lỗi: capNhatToTienVao" + ex.Message);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi thực hiện xong
                Connection.disconnect();
            }
        }

        //Hàm lấy danh sách số tờ tiền trong cơ sở dữ liệu
        static void soToTienTrongKho(){
            try
            {
                // Mở kết nối
                Connection.connect();

                SqlCommand cmd = new SqlCommand("SELECT menhGia, slToTien FROM Denomination", Connection.con);

                SqlDataReader reader = cmd.ExecuteReader(); // Sử dụng ExecuteReader để lấy cả tenSP và gia

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Lấy giá trị từng cột từ dòng hiện tại
                        decimal menhGiaDecimal= reader.GetDecimal(0);
                        int menhGia = (int)menhGiaDecimal;
                        int soTo = reader.GetInt32(1);    // Cột thứ hai: slToTien

                        // Thêm giá trị vào từ điển (menhGia là khóa, soTo là giá trị)
                        SoToTien.myDictionary.Add(menhGia, soTo);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu xảy ra vấn đề
                MessageBox.Show("Lỗi: soToTienTrongKho" + ex.Message);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi thực hiện xong
                Connection.disconnect();
            }
        }

        //Hàm tính toán số lượng tờ tiền đưa vào
        static Dictionary<decimal, int> TraLaiTien(decimal soTien, Dictionary<decimal, int> tonKho)
        {
            // Sắp xếp các mệnh giá tờ tiền theo thứ tự giảm dần
            var cacToTien = new List<decimal>(tonKho.Keys);
            cacToTien.Sort((a, b) => b.CompareTo(a));

            // Kết quả trả lại
            var ketQua = new Dictionary<decimal, int>();

            // Lặp qua từng mệnh giá tờ tiền
            foreach (var toTien in cacToTien)
            {
                // Nếu số tiền còn lại là 0, thoát khỏi vòng lặp
                if (soTien == 0) break;

                // Tính số tờ tiền cần dùng
                int soToCan = (int)(soTien / toTien);

                // Tính số tờ tiền thực tế có thể trả (không vượt quá tồn kho)
                int soToThucTe = Math.Min(soToCan, tonKho[toTien]);

                if (soToThucTe > 0)
                {
                    ketQua[toTien] = soToThucTe; // Thêm vào kết quả
                    soTien -= (int)(soToThucTe * toTien); // Cập nhật số tiền còn lại
                }
            }

            // Nếu sau khi xử lý mà số tiền còn lại vẫn > 0, trả về null (không thể trả chính xác)
            if (soTien > 0)
            {
                return null;
            }

            return ketQua;
        }

        //Hàm cập nhật lại tờ tiền sau giao dịch vào cơ sở dữ liệu
        private void capNhatTienTrongKho(decimal menhGia, int soTo)
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để cập nhật số lượng sản phẩm trong bảng "product"
                string query = "UPDATE Denomination SET slToTien = slToTien - @soTo WHERE menhGia = @menhGia";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@menhGia", menhGia);
                cmd.Parameters.AddWithValue("@soTo", soTo); 

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu xảy ra vấn đề
                MessageBox.Show("Lỗi cập nhật tiền trong kho: " + ex.Message);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi thực hiện xong
                Connection.disconnect();
            }
        }
        private void ThongBaoKetQua_Load(object sender, EventArgs e){
            SoToTien.myDictionary.Clear();
            ThongBao.loadLable(this);
            ThongBao.ShowTemporaryMessage($"ĐÃ NHẬN TIỀN: {totalMoney.total:n0} VND \n \nĐANG XỬ LÝ GIAO DỊCH...", 10000);
            // Tạo Timer với thời gian 10 giây (10000 milliseconds)
            Timer timer = new Timer();
            timer.Interval = 10000; // 10000 giây
            timer.Tick += (s, ev) =>
            {
                // Dừng Timer sau khi đã hiển thị đủ 10 giây
                timer.Stop();

                //Gọi hàm thông báo kết quả 
                KetQuaThongBao();
            };
            timer.Start();
        }
    }
}
