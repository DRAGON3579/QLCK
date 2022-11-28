using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuoiKy
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CounItems();
            SumSales();
            CounUsuer();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDB2;Integrated Security=True");

        private void CounItems()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from ItemTb1", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StockLbl.Text = dt.Rows[0][0].ToString() + " Items";
            Con.Close();
        }
        private void SumSales()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Sum(SAmount) from SaleTb1", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SumSalesLbl.Text = "Tổng "+ dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CounUsuer()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UserTb1", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            UserLbl.Text = dt.Rows[0][0].ToString() + " User";
            Con.Close();
        }
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Items Obj= new Items();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }
    }
}
