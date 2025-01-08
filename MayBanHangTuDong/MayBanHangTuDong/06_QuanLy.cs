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
    public partial class QuanLy : Form
    {
        public QuanLy()
        {
            InitializeComponent();
        }
        //
        private decimal tongDoanhThu;
        private void DanhSachSP()
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để tính tổng số lượng và tổng tiền với điều kiện thoiGian
                string query = @"SELECT maSP AS [Mã sản phẩm], tenSP AS [Tên sản phẩm], gia AS [Giá], tongSL AS [Tổng số lượng] FROM Product";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();

                // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ cơ sở dữ liệu
                SqlDataAdapter data = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                // Đổ dữ liệu vào DataTable
                data.Fill(dt);

                DSSP.DataSource = dt;

                // Thiết lập DataGridView
                DSSP.Columns["Giá"].DefaultCellStyle.Format = "N0"; // Định dạng số
                DSSP.Columns["Mã sản phẩm"].ReadOnly = true;    // Cột "Mã sản phẩm" không cho phép chỉnh sửa
                DSSP.Columns["Tên sản phẩm"].ReadOnly = true;   // Cột "Tên sản phẩm" không cho phép chỉnh sửa
                DSSP.Columns["Giá"].ReadOnly = false;           // Cho phép chỉnh sửa các cột "Giá" và "Tổng số lượng"
                DSSP.Columns["Tổng số lượng"].ReadOnly = false;

                // Thêm cột nút vào DataGridView
                if (!DSSP.Columns.Contains("Action"))
                {
                    DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                    {
                        Name = "Action",
                        HeaderText = "Thao tác",
                        Text = "Cập nhật",
                        UseColumnTextForButtonValue = true // Hiển thị văn bản trên nút
                    };
                    // Tùy chỉnh màu sắc và kiểu chữ
                    buttonColumn.DefaultCellStyle.BackColor = Color.Red;

                    buttonColumn.DefaultCellStyle.BackColor = Color.Red; // Màu nền của cột
                    buttonColumn.DefaultCellStyle.ForeColor = Color.White; // Màu chữ
                    buttonColumn.DefaultCellStyle.Font = new Font("Times New Roman", 14, FontStyle.Bold); // Phông chữ Times New Roman, 12pt, in đậm
                    DSSP.Columns.Add(buttonColumn);
                }
            }
            catch (Exception ex)
            {
                //Thông báo
                ThongBao.ShowTemporaryMessage($"Không tìm thấy danh sách sản phẩm!", 3000);
            }
            finally
            {
                // Đảm bảo kết nối được đóng khi hoàn thành
                Connection.disconnect();
            }
        }
        //Hàm cập nhật giá và số lượng
        private void capNhatGia_soluongSP(string maSP, string tenSP, decimal gia, int tongSL)
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để cập nhật số lượng sản phẩm trong bảng "product"
                string query = "UPDATE Product SET gia = @gia, tongSL = @tongSL WHERE maSP = @maSP";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@maSP", maSP);
                cmd.Parameters.AddWithValue("@gia", gia);
                cmd.Parameters.AddWithValue("@tongSL", tongSL);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();

                //Thông báo
                ThongBao.ShowTemporaryMessage($"CẬP NHẬT SẢN PHẨM \n \n {tenSP} (Mã: {maSP}) \nGiá: {gia:n0}\n Số lượng: {tongSL}", 3000);

            }
            catch (Exception ex)
            {
                // Thông báo
                ThongBao.ShowTemporaryMessage($"Cập nhật không thành công! \n\nLỗi: {ex.Message}", 3000);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi thực hiện xong
                Connection.disconnect();
            }
        }
        //Hàm hiển thị danh sách mệnh giá tiền
        private void DSMenhGiaTien()
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để tính tổng số lượng và tổng tiền với điều kiện thoiGian
                string query = @"SELECT maMG AS [Mã mệnh giá], menhGia AS [Mệnh giá], slToTien AS [Số lượng tờ tiền] FROM Denomination";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();

                // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ cơ sở dữ liệu
                SqlDataAdapter data = new SqlDataAdapter(cmd);
                DataTable dttb = new DataTable();

                // Đổ dữ liệu vào DataTable
                data.Fill(dttb);

                MGT.DataSource = dttb;

                // Thiết lập DataGridView
                MGT.Columns["Mệnh giá"].DefaultCellStyle.Format = "N0"; // Định dạng số
                MGT.Columns["Mã mệnh giá"].ReadOnly = true;   // Cột "Mã mệnh giá" không cho phép chỉnh sửa
                MGT.Columns["Mệnh giá"].ReadOnly = true;           // Cho phép chỉnh sửa các cột "Mệnh giá" và "Số lượng tờ tiền"
                MGT.Columns["Số lượng tờ tiền"].ReadOnly = false;

                // Thêm cột nút vào DataGridView
                if (!MGT.Columns.Contains("ThaoTac"))
                {
                    DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                    {
                        Name = "ThaoTac",
                        HeaderText = "Thao tác",
                        Text = "Cập nhật",
                        UseColumnTextForButtonValue = true // Hiển thị văn bản trên nút
                    };
                    // Tùy chỉnh màu sắc và kiểu chữ
                    buttonColumn.DefaultCellStyle.BackColor = Color.Red;

                    buttonColumn.DefaultCellStyle.BackColor = Color.Red; // Màu nền của cột
                    buttonColumn.DefaultCellStyle.ForeColor = Color.White; // Màu chữ
                    buttonColumn.DefaultCellStyle.Font = new Font("Times New Roman", 14, FontStyle.Bold); // Phông chữ Times New Roman, 12pt, in đậm
                    MGT.Columns.Add(buttonColumn);
                }

            }
            catch (Exception ex)
            {
                //Thông báo
                ThongBao.ShowTemporaryMessage($"Không tìm thấy danh sách mệnh giá tiền!", 3000);
            }
            finally
            {
                // Đảm bảo kết nối được đóng khi hoàn thành
                Connection.disconnect();
            }
        }
        //Hàm cập nhật giá và số lượng
        private void capNhatMenhGiaTien(string maMG, decimal menhGia, int slToTien)
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để cập nhật số lượng sản phẩm trong bảng "product"
                string query = "UPDATE Denomination SET slToTien = @slToTien WHERE maMG = @maMG";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@maMG", maMG);
                cmd.Parameters.AddWithValue("@slToTien", slToTien);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();

                //Thông báo
                ThongBao.ShowTemporaryMessage($"CẬP NHẬT MỆNH GIÁ TIỀN \n \n {menhGia:n0} (Mã: {maMG}) \nSố lượng tờ tiền: {slToTien}", 3000);

            }
            catch (Exception ex)
            {
                //Thông báo
                ThongBao.ShowTemporaryMessage($"Cập nhật mệnh giá tiền không thành công! \nLỗi: {ex.Message}", 3000);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi thực hiện xong
                Connection.disconnect();
            }
        }
        //Hàm hiển thị danh sách tài khoản
        private void DSTaiKhoan()
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để tính tổng số lượng và tổng tiền với điều kiện thoiGian
                string query = @"SELECT tenTaiKhoan AS [Tên tài khoản], matKhau AS [Mật khẩu] FROM Account";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();

                // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ cơ sở dữ liệu
                SqlDataAdapter data = new SqlDataAdapter(cmd);
                DataTable dttb = new DataTable();

                // Đổ dữ liệu vào DataTable
                data.Fill(dttb);

                TK.DataSource = dttb;

                // Thiết lập DataGridView
                TK.Columns["Tên tài khoản"].ReadOnly = true;           // Cho phép chỉnh sửa các cột "Mệnh giá" và "Số lượng tờ tiền"
                TK.Columns["Mật khẩu"].ReadOnly = false;

                // Kiểm tra và thêm cột "Xóa" (Delete)
                if (!TK.Columns.Contains("Xoa"))
                {
                    DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                    {
                        Name = "Xoa",
                        HeaderText = "Xóa",
                        Text = "Xóa",
                        UseColumnTextForButtonValue = true // Hiển thị văn bản trên nút
                    };
                    // Tùy chỉnh màu sắc và kiểu chữ cho cột "Xóa"
                    buttonColumn.DefaultCellStyle.BackColor = Color.Red;
                    buttonColumn.DefaultCellStyle.ForeColor = Color.White;
                    buttonColumn.DefaultCellStyle.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                    TK.Columns.Add(buttonColumn);
                }

                // Kiểm tra và thêm cột "Thay đổi" (Edit)
                if (!TK.Columns.Contains("ThayDoi"))
                {
                    DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                    {
                        Name = "ThayDoi",
                        HeaderText = "Thao tác",
                        Text = "Cập nhật",
                        UseColumnTextForButtonValue = true // Hiển thị văn bản trên nút
                    };
                    // Tùy chỉnh màu sắc và kiểu chữ cho cột "Thay đổi"
                    buttonColumn.DefaultCellStyle.BackColor = Color.Orange;
                    buttonColumn.DefaultCellStyle.ForeColor = Color.White;
                    buttonColumn.DefaultCellStyle.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                    TK.Columns.Add(buttonColumn);
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
        }
        //Hàm thêm tài khoản vào cơ sở dữ liệu
        private void themTK(string tenTK, string matKhau)
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để thêm dữ liệu vào bảng "bill"
                string query = "INSERT INTO Account (tenTaiKhoan, matKhau) " +
                               "VALUES (@tenTK, @matKhau)";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm các tham số vào câu lệnh SQL để bảo vệ khỏi SQL Injection
                cmd.Parameters.AddWithValue("@tenTK", tenTK);
                cmd.Parameters.AddWithValue("@matKhau", matKhau);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();

                DSTaiKhoan();
                //Thông báo
                ThongBao.ShowTemporaryMessage($"THÊM TÀI KHOẢN \n \n {tenTK} \nMật khẩu: {matKhau}", 3000);

            }
            catch (Exception ex)
            {
                //Thông báo
                ThongBao.ShowTemporaryMessage($"Tên tài khoản: {tenTK} đã được đăng ký!\n \nVui lòng đăng ký tên tài khoản khác.", 3000);
            }
            finally
            {
                // Đảm bảo kết nối được đóng khi hoàn thành
                Connection.disconnect();
            }
        }
        //Hàm xóa tài khoản trong cơ sở dữ liệu
        private void xoaTK(string tenTK)
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để thêm dữ liệu vào bảng "bill"
                string query = "DELETE FROM Account WHERE tenTaiKhoan = @tenTK";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm các tham số vào câu lệnh SQL để bảo vệ khỏi SQL Injection
                cmd.Parameters.AddWithValue("@tenTK", tenTK);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();

                DSTaiKhoan();
                //Thông báo
                ThongBao.ShowTemporaryMessage($"XÓA TÀI KHOẢN THÀNH CÔNG \n \n {tenTK}", 3000);
            }
            catch (Exception ex)
            {
                ThongBao.ShowTemporaryMessage($"Xóa tài khoản không thành công! \n Lỗi: {ex.Message}",3000);
            }
            finally
            {
                // Đảm bảo kết nối được đóng khi hoàn thành
                Connection.disconnect();
            }
        }
        //Hàm cập nhật tài khoản
        private void capNhatTaiKhoan(string tenTK, string matKhau)
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để cập nhật số lượng sản phẩm trong bảng "product"
                string query = "UPDATE Account SET matKhau = @matKhau WHERE tenTaiKhoan = @tenTK";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@tenTK", tenTK);
                cmd.Parameters.AddWithValue("@matKhau", matKhau);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();

                DSTaiKhoan();
                //Thông báo
                ThongBao.ShowTemporaryMessage($"CẬP NHẬT TÀI KHOẢN \n \n {tenTK} \nMật khẩu: {matKhau}", 3000);
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu xảy ra vấn đề
                ThongBao.ShowTemporaryMessage($"CẬP NHẬT TÀI KHOẢN KHÔNG THÀNH CÔNG! \nLỗi: {ex.Message}",3000);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi thực hiện xong
                Connection.disconnect();
            }
        }
        //Hàm hiển danh sách hóa đơn
        private void DSHoaDon()
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để tính tổng số lượng và tổng tiền với điều kiện thoiGian
                string query = @"SELECT maHD AS [Mã hóa đơn], tongSLMua AS [Tổng số lượng mua], thanhTien AS [Thành tiền], thoiGian AS [Thời gian]  FROM Bill";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();

                // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ cơ sở dữ liệu
                SqlDataAdapter data = new SqlDataAdapter(cmd);
                DataTable dttb = new DataTable();

                // Đổ dữ liệu vào DataTable
                data.Fill(dttb);

                HD.DataSource = dttb;

                // Thiết lập DataGridView
                HD.Columns["Thành tiền"].DefaultCellStyle.Format = "N0"; // Định dạng số
                HD.ReadOnly = true;           // Cho phép chỉnh sửa các cột "Mệnh giá" và "Số lượng tờ tiền"

                // Kiểm tra và thêm cột "Xóa" (Delete)
                if (!HD.Columns.Contains("ChiTiet"))
                {
                    DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                    {
                        Name = "ChiTiet",
                        HeaderText = "",
                        Text = "Chi Tiết",
                        UseColumnTextForButtonValue = true // Hiển thị văn bản trên nút
                    };
                    // Tùy chỉnh màu sắc và kiểu chữ cho cột "Xóa"
                    buttonColumn.DefaultCellStyle.BackColor = Color.Red;
                    buttonColumn.DefaultCellStyle.ForeColor = Color.White;
                    buttonColumn.DefaultCellStyle.Font = new Font("Times New Roman", 14, FontStyle.Bold);
                    HD.Columns.Add(buttonColumn);
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
        }
        //Hiển thị chi tiết của một hóa đơn
        private void HienThiCTHD(int maHD)
        {
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để tính tổng số lượng và tổng tiền với điều kiện thoiGian
                string query = @"SELECT maHD AS [Mã hóa đơn], tenSP AS [Tên sản phẩm], gia AS [Giá], soLuong AS [Số lượng], tongTien AS [Tổng tiền] FROM DetailsBill WHERE maHD = @maHD";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@maHD", maHD);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();

                // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ cơ sở dữ liệu
                SqlDataAdapter data = new SqlDataAdapter(cmd);
                DataTable dttb = new DataTable();

                // Đổ dữ liệu vào DataTable
                data.Fill(dttb);

                CTHD.DataSource = dttb;

                // Thiết lập DataGridView
                CTHD.Columns["Giá"].DefaultCellStyle.Format = "N0"; // Định dạng số
                CTHD.Columns["Tổng tiền"].DefaultCellStyle.Format = "N0"; // Định dạng số
                CTHD.ReadOnly = true;           
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu xảy ra vấn đề
                ThongBao.ShowTemporaryMessage($"Không tìm thấy chi tiết của hóa đơn! \nLỗi: {ex.Message}", 3000);
            }
            finally
            {
                // Đảm bảo kết nối được đóng khi hoàn thành
                Connection.disconnect();
            }
        }
        //Hàm thống kê
        private void loadThongKe(DateTime selectedDate)  // Nhận tham số ngày đã chọn
        {
            tongDoanhThu = 0;
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để tính tổng số lượng và tổng tiền với điều kiện thoiGian
                string query = @"SELECT DetailsBill.maSP AS [Mã sản phẩm], DetailsBill.tenSP AS [Tên sản phẩm],  DetailsBill.gia AS [Giá], SUM(DetailsBill.soLuong) AS [Tổng số lượng bán],  SUM(DetailsBill.tongTien) AS [Tổng tiền bán], CONVERT(DATE, Bill.thoiGian) AS [Thời gian]
                                 FROM Bill
                                 INNER JOIN DetailsBill ON Bill.maHD = DetailsBill.maHD
                                 WHERE DAY(Bill.thoiGian) = @ngay AND MONTH(Bill.thoiGian) = @thang AND YEAR(Bill.thoiGian) = @nam
                                 GROUP BY DetailsBill.maSP, DetailsBill.tenSP, DetailsBill.gia, CONVERT(DATE, Bill.thoiGian)";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm tham số cho thoiGian
                cmd.Parameters.AddWithValue("@ngay", selectedDate.Day);
                cmd.Parameters.AddWithValue("@thang", selectedDate.Month);
                cmd.Parameters.AddWithValue("@nam", selectedDate.Year);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                cmd.ExecuteNonQuery();

                // Tạo đối tượng SqlDataAdapter để lấy dữ liệu từ cơ sở dữ liệu
                SqlDataAdapter data = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                // Đổ dữ liệu vào DataTable
                data.Fill(dt);

                // Chuyển "Tổng tiền bán" sang định dạng tiền tệ
                thongKe.DataSource = dt;

                thongKe.Columns["Giá"].DefaultCellStyle.Format = "N0"; // Định dạng số
                thongKe.Columns["Tổng tiền bán"].DefaultCellStyle.Format = "N0"; // Định dạng số

                //Tính tổng doanh thu
                foreach (DataGridViewRow row in thongKe.Rows)
                {
                    // Kiểm tra nếu hàng không phải hàng mới (NewRow) và giá trị không null
                    if (row.Cells["Tổng tiền bán"].Value != null && row.Cells["Tổng tiền bán"].Value != DBNull.Value)
                    {
                        // Cộng dồn giá trị cột "Tổng tiền bán"
                        tongDoanhThu += Convert.ToDecimal(row.Cells["Tổng tiền bán"].Value);
                    }
                }
                label10.Text = $" {tongDoanhThu:n0} VND";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: thoiGian " + ex.Message);
            }
            finally
            {
                // Đảm bảo kết nối được đóng khi hoàn thành
                Connection.disconnect();
            }
        }
        private void tabPage1_Click(object sender, EventArgs e){}

        private void QuanLy_Load(object sender, EventArgs e)
        {
            DanhSachSP();
            DSMenhGiaTien();
            DSTaiKhoan();
            DSHoaDon();
            loadThongKe(thoiGian.Value.Date);
            ThongBao.loadLable(this);
            label10.Text = $" {tongDoanhThu:n0} VND";
        }

        private void MGT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn vào cột "Thao tác"
            if (e.ColumnIndex == MGT.Columns["ThaoTac"].Index && e.RowIndex >= 0)
            {
                var columSL = MGT.Rows[e.RowIndex].Cells["Số lượng tờ tiền"].Value;

                if (columSL == null || string.IsNullOrWhiteSpace(columSL.ToString()))
                {
                    ThongBao.ShowTemporaryMessage($"Bạn chưa nhập dữ liệu", 3000);
                }
                else
                {
                    // Lấy giá trị của hàng hiện tại
                    string maMG = MGT.Rows[e.RowIndex].Cells["Mã mệnh giá"].Value.ToString();
                    decimal menhGia = Convert.ToDecimal(MGT.Rows[e.RowIndex].Cells["Mệnh giá"].Value);
                    int tongSL = Convert.ToInt32(MGT.Rows[e.RowIndex].Cells["Số lượng tờ tiền"].Value);

                    // Tùy chỉnh để thực hiện lưu thay đổi vào cơ sở dữ liệu tại đây.
                    capNhatMenhGiaTien(maMG, menhGia, tongSL);
                }
            }
        }

        private void TK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn vào cột "Xóa"
            if (e.ColumnIndex == TK.Columns["Xoa"].Index && e.RowIndex >= 0)
            {
                // Lấy giá trị của hàng hiện tại
                string tenTK = TK.Rows[e.RowIndex].Cells["Tên tài khoản"].Value.ToString();

                // Tùy chỉnh để thực hiện lưu thay đổi vào cơ sở dữ liệu tại đây.
                xoaTK(tenTK);
            }
            // Kiểm tra nếu người dùng nhấn vào cột "Thay đổi"
            if (e.ColumnIndex == TK.Columns["ThayDoi"].Index && e.RowIndex >= 0)
            {
                var columMK = TK.Rows[e.RowIndex].Cells["Mật khẩu"].Value;

                if (columMK == null || string.IsNullOrWhiteSpace(columMK.ToString()))
                {
                    ThongBao.ShowTemporaryMessage($"Bạn chưa nhập dữ liệu", 3000);
                }
                else
                {
                    // Lấy giá trị của hàng hiện tại
                    string tenTK = TK.Rows[e.RowIndex].Cells["Tên tài khoản"].Value.ToString();
                    string matKhau = TK.Rows[e.RowIndex].Cells["Mật khẩu"].Value.ToString();

                    // Tùy chỉnh để thực hiện lưu thay đổi vào cơ sở dữ liệu tại đây.
                    capNhatTaiKhoan(tenTK, matKhau);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e){}

        private void label5_Click(object sender, EventArgs e){}

        private void themTaiKhoan_Click(object sender, EventArgs e)
        {
            if(tenTK.Text == "")
            {
                //Thông báo
                ThongBao.ShowTemporaryMessage($"Bạn chưa nhập tên tài khoản.", 3000);
            }
            else if(matKhauTK.Text == "")
            {
                //Thông báo
                ThongBao.ShowTemporaryMessage($"Bạn chưa nhập mật khẩu.", 3000);
            }
            else
            {
                themTK(tenTK.Text, matKhauTK.Text);
                tenTK.Text = "";
                matKhauTK.Text = "";
            }
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            TaiKhoan tk = new TaiKhoan();
            openForm.ShowNextForm(this, tk);
        }

        private void label6_Click(object sender, EventArgs e){}

        private void HD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn vào cột "Chi Tiết"
            if (e.ColumnIndex == HD.Columns["ChiTiet"].Index && e.RowIndex >= 0)
            {
                int maHD = Convert.ToInt32(HD.Rows[e.RowIndex].Cells["Mã hóa đơn"].Value);
                HienThiCTHD(maHD);
            }
        }

        private void thoiGian_ValueChanged(object sender, EventArgs e)
        {
            // Gọi phương thức loadThongKe và truyền vào ngày đã chọn
            loadThongKe(thoiGian.Value);
        }

        private void thongKe_CellContentClick(object sender, DataGridViewCellEventArgs e){}

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void DSSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng nhấn vào cột "Thao tác"
            if (e.ColumnIndex == DSSP.Columns["Action"].Index && e.RowIndex >= 0)
            {
                var columGia = DSSP.Rows[e.RowIndex].Cells["Giá"].Value;
                var columSL = DSSP.Rows[e.RowIndex].Cells["Tổng số lượng"].Value;

                if (columGia == null || string.IsNullOrWhiteSpace(columGia.ToString()) || columSL == null || string.IsNullOrWhiteSpace(columSL.ToString()))
                {
                    ThongBao.ShowTemporaryMessage($"Bạn chưa nhập dữ liệu", 3000);
                }
                else
                {
                    // Lấy giá trị của hàng hiện tại
                    string maSP = DSSP.Rows[e.RowIndex].Cells["Mã sản phẩm"].Value.ToString();
                    string tenSP = DSSP.Rows[e.RowIndex].Cells["Tên sản phẩm"].Value.ToString();
                    decimal gia = Convert.ToDecimal(DSSP.Rows[e.RowIndex].Cells["Giá"].Value);
                    int tongSL = Convert.ToInt32(DSSP.Rows[e.RowIndex].Cells["Tổng số lượng"].Value);

                    // Tùy chỉnh để thực hiện lưu thay đổi vào cơ sở dữ liệu tại đây.
                    capNhatGia_soluongSP(maSP, tenSP, gia, tongSL);
                }
            }    
        }

        private void DSSP_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ThongBao.ShowTemporaryMessage($"Kiểu dữ liệu không đúng", 3000);
            e.Cancel = true;
        }

        private void MGT_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ThongBao.ShowTemporaryMessage($"Kiểu dữ liệu không đúng", 3000);
            e.Cancel = true;
        }

        private void TK_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ThongBao.ShowTemporaryMessage($"Kiểu dữ liệu không đúng", 3000);
            e.Cancel = true;
        }
    }
}
