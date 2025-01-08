using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Globalization;
using System.Data.SqlClient;

namespace MayBanHangTuDong
{
    
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            loadhoadon();
            label3.Text = $"{DanhSachSPMua.tongTien:n0} VND";
        }
        private void Form2_Load(object sender, EventArgs e){}
        DataTable dataTB = new DataTable();
        private void loadhoadon()
        {
         
            dataTB.Columns.Add("STT", typeof(int));
            dataTB.Columns.Add("Tên sản phẩm", typeof(string));
            dataTB.Columns.Add("Giá", typeof(string));
            dataTB.Columns.Add("Số lượng", typeof(string));
            int index = 1;
            foreach (var item in DanhSachSPMua.danhSachSanPham)
            {
                dataTB.Rows.Add(index, item.tenSP,item.gia.ToString("N0"), item.soLuong);
                index++;
            }

            //dataTB cho DataGridView
            hd.DataSource = dataTB;

        }

        private void thanhToan_Click(object sender, EventArgs e)
        {
            thongBaoNhanTien TBNT = new thongBaoNhanTien();
            openForm.ShowNextForm(this, TBNT);
        }

        private void Huy_Click(object sender, EventArgs e)
        {
            DanhSachSPMua.danhSachSanPham.Clear();
            DanhSachSPMua.tongTien = 0;
            Maybanhang mayBanHang = new Maybanhang();
            openForm.ShowNextForm(this, mayBanHang);

        }
        
    }
}
