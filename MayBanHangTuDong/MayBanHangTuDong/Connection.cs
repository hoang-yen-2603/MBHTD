using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayBanHangTuDong
{
    internal class Connection
    {
        public static SqlConnection con;
        // ket noi 
        public static void connect()
        {
            string cn = @"Data Source=ADMIN-PC\MYSQL;Initial Catalog=Products;Integrated Security=True;TrustServerCertificate=True;Encrypt=True;";


            try
            {
                con = new SqlConnection(cn);
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối không thành công! " + ex.Message, "Lỗi");
            }
        }


        // ngat ket noi
        public static void disconnect()
        {
            if (con != null && con.State == ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
                con = null;
            }
        }
    }
}
