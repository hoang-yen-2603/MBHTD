using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayBanHangTuDong
{
    public partial class ThongBaoGDKTC : Form
    {
        public ThongBaoGDKTC()
        {
            InitializeComponent();
            this.Load += async (sender, e) =>
            {
                Maybanhang mayBH = new Maybanhang();
                await openForm.LoadAndNavigateAsync(this, mayBH);
            };
        }
        
        private void ThongBaoGDTC_Load(object sender, EventArgs e){}

        private void label2_Click(object sender, EventArgs e){}
    }
}
