using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayBanHangTuDong
{
    internal class ThongBao
    {
        public static Label lblMessage;
        public static void loadLable(Form parentForm)
        {
            // Khởi tạo Label
            lblMessage = new Label();

            // Thiết lập các thuộc tính của Label
            lblMessage.Name = "lblMessage";
            lblMessage.Text = ""; // Chưa có thông điệp ban đầu
            lblMessage.AutoSize = false;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter; // Căn giữa văn bản
            lblMessage.BackColor = Color.Black; // Màu nền
            lblMessage.ForeColor = Color.White; // Màu chữ
            lblMessage.Font = new Font("Times New Roman", 18, FontStyle.Bold);
            lblMessage.Size = new Size(500, 200); // Đặt kích thước cho Label
            lblMessage.BorderStyle = BorderStyle.None;

            // Căn giữa Label trên form (cả chiều ngang và chiều dọc)
            lblMessage.Location = new Point(
                (parentForm.ClientSize.Width - lblMessage.Width) / 2,  // Căn giữa theo chiều ngang
                (parentForm.ClientSize.Height - lblMessage.Height) / 2 // Căn giữa theo chiều dọc
            );

            // Ẩn label khi chưa cần thông báo
            lblMessage.Visible = false;

            // Thêm label vào form
            parentForm.Controls.Add(lblMessage);

            // Đảm bảo label luôn nằm trên cùng
            lblMessage.BringToFront();

        }
        public static async void ShowTemporaryMessage(string message, int Time)
        {
            lblMessage.Text = message; // Cập nhật nội dung của Label
            lblMessage.Visible = true; // Hiển thị label

            // Đợi trước khi ẩn thông báo
            await Task.Delay(Time);

            lblMessage.Visible = false; // Ẩn label 
        }
    }
}
