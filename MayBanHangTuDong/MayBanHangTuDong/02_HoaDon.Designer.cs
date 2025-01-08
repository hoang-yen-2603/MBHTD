
namespace MayBanHangTuDong
{
    partial class Form2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.productTableAdapter1 = new MayBanHangTuDong.ProductsDataSet1TableAdapters.ProductTableAdapter();
            this.hd = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.tongTien = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.thanhToan = new System.Windows.Forms.Button();
            this.Huy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.hd)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Location = new System.Drawing.Point(354, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 55);
            this.label1.TabIndex = 39;
            this.label1.Text = "HÓA ĐƠN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // productTableAdapter1
            // 
            this.productTableAdapter1.ClearBeforeFill = true;
            // 
            // hd
            // 
            this.hd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.hd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.hd.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.hd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.hd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.hd.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hd.DefaultCellStyle = dataGridViewCellStyle5;
            this.hd.GridColor = System.Drawing.Color.White;
            this.hd.Location = new System.Drawing.Point(123, 132);
            this.hd.Name = "hd";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.hd.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.hd.RowHeadersVisible = false;
            this.hd.RowHeadersWidth = 70;
            this.hd.RowTemplate.Height = 28;
            this.hd.Size = new System.Drawing.Size(707, 197);
            this.hd.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(113, 380);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 60);
            this.label2.TabIndex = 41;
            this.label2.Text = "Tổng tiền:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tongTien
            // 
            this.tongTien.AutoSize = true;
            this.tongTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tongTien.Location = new System.Drawing.Point(316, 376);
            this.tongTien.Name = "tongTien";
            this.tongTien.Size = new System.Drawing.Size(0, 37);
            this.tongTien.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(465, 376);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 60);
            this.label3.TabIndex = 43;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(342, 376);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 37);
            this.label4.TabIndex = 44;
            // 
            // thanhToan
            // 
            this.thanhToan.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.thanhToan.AutoSize = true;
            this.thanhToan.BackColor = System.Drawing.Color.Cyan;
            this.thanhToan.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.thanhToan.ForeColor = System.Drawing.Color.Black;
            this.thanhToan.Location = new System.Drawing.Point(210, 443);
            this.thanhToan.Name = "thanhToan";
            this.thanhToan.Size = new System.Drawing.Size(200, 70);
            this.thanhToan.TabIndex = 45;
            this.thanhToan.Text = "Thanh toán ";
            this.thanhToan.UseVisualStyleBackColor = false;
            this.thanhToan.Click += new System.EventHandler(this.thanhToan_Click);
            // 
            // Huy
            // 
            this.Huy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Huy.AutoSize = true;
            this.Huy.BackColor = System.Drawing.Color.Cyan;
            this.Huy.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Huy.Location = new System.Drawing.Point(509, 443);
            this.Huy.Name = "Huy";
            this.Huy.Size = new System.Drawing.Size(200, 70);
            this.Huy.TabIndex = 46;
            this.Huy.Text = "Hủy";
            this.Huy.UseVisualStyleBackColor = false;
            this.Huy.Click += new System.EventHandler(this.Huy_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 522);
            this.Controls.Add(this.Huy);
            this.Controls.Add(this.thanhToan);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tongTien);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hd);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ProductsDataSet1TableAdapters.ProductTableAdapter productTableAdapter1;
        private System.Windows.Forms.DataGridView hd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label tongTien;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button thanhToan;
        private System.Windows.Forms.Button Huy;
    }
}