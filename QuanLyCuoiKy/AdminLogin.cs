using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuoiKy
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        
        private void LoginBtn_Click_1(object sender, EventArgs e)
        {
            if (PasswordTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                PasswordTb.Text = "";
            }
            else if (PasswordTb.Text == "Password")
            {
                User Obj = new User();
                Obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đúng mật khẩu");
                PasswordTb.Text = "";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
    }
}
