using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayBanHangTuDong
{
    internal class openForm
    {
        public static async Task LoadAndNavigateAsync(Form currentForm, Form nextForm)
        {
            await Task.Delay(20000); // Chờ 20 giây
            ShowNextForm(currentForm, nextForm); // Chuyển sang Form tiếp theo
        }
        public static void ShowNextForm(Form current, Form next)
        {
            //Mở form tiếp theo
            next.Show();

            //Ẩn Form hiện tại 
            current.Hide();
        }
    }
}
