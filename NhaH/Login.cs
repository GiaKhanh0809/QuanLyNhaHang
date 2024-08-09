using NhaH.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NhaH.Properties;
namespace NhaH
{
    public partial class Login : Form
    {
        LoginMethod loginMethod = new LoginMethod();
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            string pass = txtPass.Text;

            int userType = Logincheck(user, pass);

            if (userType == 1)
            {
                Session.Username = user;
                Session.UserType = 1;
                //1 là nhân viên
                DashBoard_User dashBoard_User = new DashBoard_User();
                this.Hide();
                dashBoard_User.ShowDialog();
                this.Show();
            }
            else if (userType == 2)
            {
                Session.Username = user;
                Session.UserType = 2;
                // 2 là admin
                DashBoard_Admin dashBoard_Admin = new DashBoard_Admin();
                this.Hide();
                dashBoard_Admin.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu !");
            }



        }
        public int Logincheck(string user, string pass)
        {
            if (loginMethod.Login(user, pass))
            {
                return 1;
            }
            else if (user == "admin" && pass == "admin")
            {
                return 2;
            }
            else return 3;

        }
    }
}
