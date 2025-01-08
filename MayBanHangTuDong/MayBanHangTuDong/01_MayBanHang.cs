using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MayBanHangTuDong
{
    public partial class Maybanhang : Form
    {
        public Maybanhang()
        {
            InitializeComponent();
        }
        // Thêm các sản phẩm đã chọn vào danh sách 
        private void ThemDSSanPham(string maSP, NumericUpDown numericUpDown)
        {
            int soLuong = (int)numericUpDown.Value;
            try
            {
                Connection.connect(); // Kết nối đến cơ sở dữ liệu
                SqlCommand cmd = new SqlCommand("SELECT tenSP, gia FROM Product WHERE maSP = @maSP", Connection.con);
                cmd.Parameters.AddWithValue("@maSP", maSP);

                SqlDataReader reader = cmd.ExecuteReader(); // Sử dụng ExecuteReader để lấy cả tenSP và gia

                if (reader.HasRows)
                {
                    reader.Read(); // Đọc dữ liệu trả về
                    string tenSP = reader.GetString(0);  // Lấy tên sản phẩm
                    decimal gia = reader.GetDecimal(1);  // Lấy giá sản phẩm

                    if (soLuong > 0)
                    {
                        // Kiểm tra nếu sản phẩm đã tồn tại trong danh sách
                        var existingProduct = DanhSachSPMua.danhSachSanPham.FirstOrDefault(p => p.maSP == maSP);

                        if (existingProduct.maSP != null)
                        {
                            //lấy số lượng hiện tại
                            int currentQuantity = existingProduct.soLuong;
                            // Cập nhật lại số lượng của sản phẩm
                            var index = DanhSachSPMua.danhSachSanPham.FindIndex(p => p.maSP == maSP);
                            DanhSachSPMua.danhSachSanPham[index] = (maSP, tenSP,gia, soLuong);
                            DanhSachSPMua.tongTien += (soLuong - currentQuantity) * gia;
                            ThongBao.ShowTemporaryMessage($"Sản phẩm {tenSP} đã được cập nhật vào danh sách. \n \nSố lượng mới: {soLuong}.",3000);
                        }
                        else
                        {
                            // Nếu sản phẩm chưa tồn tại, thêm mới vào danh sách
                            DanhSachSPMua.danhSachSanPham.Add((maSP, tenSP, (int)gia, soLuong));
                            DanhSachSPMua.tongTien += (soLuong * gia);
                            ThongBao.ShowTemporaryMessage($"Đã thêm sản phẩm {tenSP} vào danh sách. \n \nSố lượng: {soLuong}",3000);
                        }
                    }
                    else
                    {
                        ThongBao.ShowTemporaryMessage($"Số lượng phải lớn hơn 0!",3000);
                    }
                }
                reader.Close(); // Đảm bảo đóng SqlDataReader sau khi sử dụng
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi");
            }
            finally
            {
                Connection.disconnect(); // Ngắt kết nối
            }
        }

        //Hàm lấy giá sản phẩm từ cơ sở dữ liệu
        private decimal LayGiaSP(string maSP)
        {
            decimal gia = 0;
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để cập nhật số lượng sản phẩm trong bảng "product"
                string query = "SELECT gia FROM Product WHERE maSP = @maSP";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@maSP", maSP);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                object result = cmd.ExecuteScalar();
                gia = Convert.ToDecimal(result);
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu xảy ra vấn đề
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi thực hiện xong
                Connection.disconnect();
            }
            return gia;
        }

        //Hàm lấy số lượng sản phẩm từ cơ sở dữ liệu
        private int LaySoLuongSP(string maSP)
        {
            int soLuong = 0;
            try
            {
                // Mở kết nối
                Connection.connect();

                // Câu lệnh SQL để cập nhật số lượng sản phẩm trong bảng "product"
                string query = "SELECT tongSL FROM Product WHERE maSP = @maSP";

                // Tạo đối tượng SqlCommand với câu lệnh SQL và kết nối
                SqlCommand cmd = new SqlCommand(query, Connection.con);

                // Thêm tham số vào câu lệnh SQL
                cmd.Parameters.AddWithValue("@maSP", maSP);

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng
                object result = cmd.ExecuteScalar();
                soLuong = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu xảy ra vấn đề
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi thực hiện xong
                Connection.disconnect();
            }
            return soLuong;
        }

        //Hàm hiển thị giá của các sản phẩm 
        private void ganGiaTriSP()
        {
            SP01.Text = $"{LayGiaSP(SP01.Name).ToString("N0")}";
            SP02.Text = $"{LayGiaSP(SP02.Name).ToString("N0")}";
            SP03.Text = $"{LayGiaSP(SP03.Name).ToString("N0")}";
            SP04.Text = $"{LayGiaSP(SP04.Name).ToString("N0")}";
            SP05.Text = $"{LayGiaSP(SP05.Name).ToString("N0")}";
            SP06.Text = $"{LayGiaSP(SP06.Name).ToString("N0")}";
            SP07.Text = $"{LayGiaSP(SP07.Name).ToString("N0")}";
            SP08.Text = $"{LayGiaSP(SP08.Name).ToString("N0")}";
            SP09.Text = $"{LayGiaSP(SP09.Name).ToString("N0")}";
            SP10.Text = $"{LayGiaSP(SP10.Name).ToString("N0")}";
            SP11.Text = $"{LayGiaSP(SP11.Name).ToString("N0")}";
            SP12.Text = $"{LayGiaSP(SP12.Name).ToString("N0")}";
            SP13.Text = $"{LayGiaSP(SP13.Name).ToString("N0")}";
            SP14.Text = $"{LayGiaSP(SP14.Name).ToString("N0")}";
            SP15.Text = $"{LayGiaSP(SP15.Name).ToString("N0")}";
            SP16.Text = $"{LayGiaSP(SP16.Name).ToString("N0")}";
            SP17.Text = $"{LayGiaSP(SP17.Name).ToString("N0")}";
            SP18.Text = $"{LayGiaSP(SP18.Name).ToString("N0")}";
            SP19.Text = $"{LayGiaSP(SP19.Name).ToString("N0")}";
            SP20.Text = $"{LayGiaSP(SP20.Name).ToString("N0")}";
            SP21.Text = $"{LayGiaSP(SP21.Name).ToString("N0")}";
            SP22.Text = $"{LayGiaSP(SP22.Name).ToString("N0")}";
            SP23.Text = $"{LayGiaSP(SP23.Name).ToString("N0")}";
            SP24.Text = $"{LayGiaSP(SP24.Name).ToString("N0")}";
        }
        //Hàm quy định số lượng lớn nhất của các sản phẩm có thể mua ở trong máy
        private void ganMaxSLSP()
        {
            numericUpDown1.Maximum = LaySoLuongSP("SP01");
            numericUpDown2.Maximum = LaySoLuongSP("SP02");
            numericUpDown3.Maximum = LaySoLuongSP("SP03");
            numericUpDown4.Maximum = LaySoLuongSP("SP04");
            numericUpDown5.Maximum = LaySoLuongSP("SP05");
            numericUpDown6.Maximum = LaySoLuongSP("SP06");
            numericUpDown7.Maximum = LaySoLuongSP("SP07");
            numericUpDown8.Maximum = LaySoLuongSP("SP08");
            numericUpDown9.Maximum = LaySoLuongSP("SP09");
            numericUpDown10.Maximum = LaySoLuongSP("SP10");
            numericUpDown11.Maximum = LaySoLuongSP("SP11");
            numericUpDown12.Maximum = LaySoLuongSP("SP12");
            numericUpDown13.Maximum = LaySoLuongSP("SP13");
            numericUpDown14.Maximum = LaySoLuongSP("SP14");
            numericUpDown15.Maximum = LaySoLuongSP("SP15");
            numericUpDown16.Maximum = LaySoLuongSP("SP16");
            numericUpDown17.Maximum = LaySoLuongSP("SP17");
            numericUpDown18.Maximum = LaySoLuongSP("SP18");
            numericUpDown19.Maximum = LaySoLuongSP("SP19");
            numericUpDown20.Maximum = LaySoLuongSP("SP20");
            numericUpDown21.Maximum = LaySoLuongSP("SP21");
            numericUpDown22.Maximum = LaySoLuongSP("SP22");
            numericUpDown23.Maximum = LaySoLuongSP("SP23");
            numericUpDown24.Maximum = LaySoLuongSP("SP24");
        }
        // xu ly cac button san pham
        private void button1_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP01", numericUpDown1);
            //CalculateTotal();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP02", numericUpDown2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP03", numericUpDown3);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP04", numericUpDown4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP05", numericUpDown5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP06", numericUpDown6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP07", numericUpDown7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP08", numericUpDown8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP09", numericUpDown9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP10", numericUpDown10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP11", numericUpDown11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP12", numericUpDown12);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP13", numericUpDown13);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP14", numericUpDown14);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP15", numericUpDown15);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP16", numericUpDown16);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP17", numericUpDown17);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP18", numericUpDown18);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP19", numericUpDown19);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP20", numericUpDown20);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP21", numericUpDown21);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP22", numericUpDown22);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP23", numericUpDown23);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            ThemDSSanPham("SP24", numericUpDown24);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var control in Controls)
            {
                if (control is NumericUpDown numericUpDown)
                {
                    numericUpDown.Minimum = 0;
                }
            }
            ganGiaTriSP();
            ganMaxSLSP();
            ThongBao.loadLable(this);
            DanhSachSPMua.danhSachSanPham.Clear();
            DanhSachSPMua.tongTien = 0;
            SoToTien.DSToTienVao.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e){}

        private void button25_Click(object sender, EventArgs e)
        {
            if(DanhSachSPMua.tongTien > 0)
            {
                Form2 frm2 = new Form2();
                openForm.ShowNextForm(this, frm2);
            }
            else
            {
                ThongBao.ShowTemporaryMessage($"Bạn vui lòng chọn sản phẩm để mua!",3000);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            DanhSachSPMua.danhSachSanPham.Clear();
            DanhSachSPMua.tongTien = 0;
            foreach (var control in Controls)
            {
                if (control is NumericUpDown numericUpDown)
                {
                    numericUpDown.Value = 0;
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e) {}

        private void QuayVe_Click(object sender, EventArgs e)
        {
            TaiKhoan TK = new TaiKhoan();
            openForm.ShowNextForm(this, TK);
        }
    }
}
