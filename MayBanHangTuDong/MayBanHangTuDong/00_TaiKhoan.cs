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
    public partial class TaiKhoan : Form
    {
        public TaiKhoan()
        {
            InitializeComponent();
        }
        //Hàm kiểm tra tài khoản
        private bool kiemTraTaiKhoan(string tenTaiKhoan, string matKhau)
        {
            try
            {
                // Mở kết nối (connection pooling sẽ tự động được sử dụng)
                Connection.connect();

                SqlCommand cmd = new SqlCommand("SELECT tenTaiKhoan, matKhau FROM Account", Connection.con);

                SqlDataReader reader = cmd.ExecuteReader(); // Sử dụng ExecuteReader để lấy mật khẩu

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string tenTK = reader["tenTaiKhoan"].ToString();
                        string mk = reader["matKhau"].ToString();

                        // So sánh mật khẩu (cân nhắc dùng hashing để tăng bảo mật)
                        if (tenTK.Equals(tenTaiKhoan, StringComparison.Ordinal) && mk.Equals(matKhau, StringComparison.Ordinal)) // So sánh chính xác, không phân biệt văn bản Unicode
                        {
                            return true; // Mật khẩu đúng, trả về true
                        }
                    }
                }
                reader.Close();
                return false; // Nếu không có mật khẩu đúng, trả về false
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có vấn đề và trả về false
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
            finally
            {
                // Đảm bảo kết nối được đóng để trả lại vào pool
                Connection.disconnect();
            }
        }
        private void button25_Click(object sender, EventArgs e)
        {
            Maybanhang MBH = new Maybanhang();
            openForm.ShowNextForm(this, MBH);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tenTK.Text == "")
            {
                //Thông báo
                ThongBao.ShowTemporaryMessage($"Vui lòng nhập tên tài khoản!", 3000);
            }
            else if (matKhau.Text == "")
            {
                //Thông báo
                ThongBao.ShowTemporaryMessage($"Vui lòng nhập mật khẩu!", 3000);
            }
            else
            {
                if (kiemTraTaiKhoan(tenTK.Text, matKhau.Text))
                {
                    // Nếu mật khẩu đúng, làm gì đó
                    QuanLy QL = new QuanLy();
                    openForm.ShowNextForm(this, QL);
                }
                else
                {
                    // Nếu mật khẩu sai, thông báo lỗi
                    ThongBao.ShowTemporaryMessage($"Tên tài khoản hoặc mật khẩu không đúng!",3000);
                }
            }
            
        }

        private void TaiKhoan_Load(object sender, EventArgs e)
        {
            ThongBao.loadLable(this);
        }

        private void label1_Click(object sender, EventArgs e){}
    }
}
