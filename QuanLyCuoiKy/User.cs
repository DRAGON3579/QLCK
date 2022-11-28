using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuoiKy
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
            Methods Obj = new Methods();
            Obj.DisplayData2("ItemTb1", UsersDGV);
        }
        //SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDb;Integrated Security=True");
        //SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDB1;Integrated Security=True");
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDB2;Integrated Security=True");
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PasswordTb.Text == "" || PhoneTb.Text == "" || EmailTb.Text == "" || GenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                try
                {
                    
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into UserTb1(UName, UEmail, UDOB, UGen, UPhone, UPassword) values(@UN, @UEM, @UD, @UG, @UP, @UPa)", Con);
                    cmd.Parameters.AddWithValue("@UN", UNameTb.Text);
                    cmd.Parameters.AddWithValue("@UEM", EmailTb.Text);
                    cmd.Parameters.AddWithValue("@UD", UDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@UG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@UP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UPa",PasswordTb.Text);
                   
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm user !!!");
                    Con.Close();
                    //cập nhật list
                    Methods Obj = new Methods();
                    Obj.DisplayData2("UserTb1", UsersDGV);
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

         int Key = 0;
        private void UsersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            UNameTb.Text = UsersDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmailTb.Text = UsersDGV.SelectedRows[0].Cells[2].Value.ToString();
            UDOB.Text = UsersDGV.SelectedRows[0].Cells[3].Value.ToString();
            GenCb.Text = UsersDGV.SelectedRows[0].Cells[4].Value.ToString();
            PhoneTb.Text = UsersDGV.SelectedRows[0].Cells[5].Value.ToString();
            PasswordTb.Text = UsersDGV.SelectedRows[0].Cells[6].Value.ToString();
            
            if (UNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(UsersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from UserTb1 where UId = @UK", Con);

                    cmd.Parameters.AddWithValue("@UK", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa user !!!");
                    Con.Close();
                    //Cập nhật list
                    Methods Obj = new Methods();
                    Obj.DisplayData2("UserTb1", UsersDGV);
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (UNameTb.Text == "" || PasswordTb.Text == "" || PhoneTb.Text == "" || EmailTb.Text == "" || GenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                try
                {

                    Con.Open(); 
                    SqlCommand cmd = new SqlCommand("update UserTb1 set UName=@UN, UEmail=@UEM, UDOB=@UD, UGen=@UG, UPhone=@UP, UPassword= @UPa where UId=@UK", Con);
                    cmd.Parameters.AddWithValue("@UN", UNameTb.Text);
                    cmd.Parameters.AddWithValue("@UEM", EmailTb.Text);
                    cmd.Parameters.AddWithValue("@UD", UDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@UG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@UP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UPa", PasswordTb.Text);
                    cmd.Parameters.AddWithValue("@UK", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã sửa thông tin user !!!");
                    Con.Close();
                    //cập nhật list
                    Methods Obj = new Methods();
                    Obj.DisplayData2("UserTb1", UsersDGV);
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
    }
}
