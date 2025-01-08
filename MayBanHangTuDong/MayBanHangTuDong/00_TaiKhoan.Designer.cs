namespace MayBanHangTuDong
{
    partial class TaiKhoan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.matKhau = new System.Windows.Forms.TextBox();
            this.button25 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tenTK = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(94, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mật khẩu:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // matKhau
            // 
            this.matKhau.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.matKhau.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matKhau.Location = new System.Drawing.Point(319, 193);
            this.matKhau.Name = "matKhau";
            this.matKhau.Size = new System.Drawing.Size(409, 53);
            this.matKhau.TabIndex = 1;
            // 
            // button25
            // 
            this.button25.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button25.BackColor = System.Drawing.Color.Aqua;
            this.button25.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button25.Location = new System.Drawing.Point(162, 317);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(225, 86);
            this.button25.TabIndex = 87;
            this.button25.Text = "Khách hàng";
            this.button25.UseVisualStyleBackColor = false;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.Aqua;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(461, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 86);
            this.button1.TabIndex = 89;
            this.button1.Text = "Quản lý";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(285, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 45);
            this.label2.TabIndex = 90;
            this.label2.Text = "ĐĂNG NHẬP";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(94, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 45);
            this.label3.TabIndex = 91;
            this.label3.Text = "Tài khoản:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tenTK
            // 
            this.tenTK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tenTK.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenTK.Location = new System.Drawing.Point(319, 99);
            this.tenTK.Name = "tenTK";
            this.tenTK.Size = new System.Drawing.Size(409, 53);
            this.tenTK.TabIndex = 92;
            // 
            // TaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tenTK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button25);
            this.Controls.Add(this.matKhau);
            this.Controls.Add(this.label1);
            this.Name = "TaiKhoan";
            this.Text = "TaiKhoan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TaiKhoan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox matKhau;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tenTK;
    }
}