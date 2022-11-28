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
using System.Xml.Linq;

namespace QuanLyCuoiKy
{
    public partial class Items : Form
    {
        public Items()
        {
            InitializeComponent();
            Methods Obj = new Methods();
            Obj.DisplayData1("ItemTb1", ProductDGV);
            GetCategory();
            MessageBox.Show("Chào Mừng " + Login.UName + " Đã Đăng Nhập Thành Công");
            string U = Login.UName;
        }
        //SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDb;Integrated Security=True");
        // SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDB1;Integrated Security=True");
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDB2;Integrated Security=True");
        private void GetUID()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Catid from CategoryTb1", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Catid", typeof(int));
            dt.Load(Rdr);
            Catcb.ValueMember = "Catid";
            //Catcb.DataSource = dt;
            Con.Close();
        }
        private void GetCategory()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select Catid from CategoryTb1", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Catid", typeof(int));
            dt.Load(Rdr);
            Catcb.ValueMember = "Catid";
            Catcb.DataSource= dt;
            Con.Close();
        }

        //Thêm sản phẩm
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (ProdNameTb.Text == "" || ProdDetailTb.Text == "" || SPriceTb.Text == "" || BPriceTb.Text == "" || ProdDetailTb.Text == "")
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                try
                {
                    int Profit = Convert.ToInt32(SPriceTb.Text) - Convert.ToInt32(BPriceTb.Text);
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ItemTb1(ItName, ItCat, ItQty, ItBprice, ItSprice, ItProfit, ItDetails, ItAddDate) values(@IN, @IC, @IQ, @IBP, @ISP, @IP, @ID, @IADate)", Con);
                    cmd.Parameters.AddWithValue("@IN", ProdNameTb.Text);
                    cmd.Parameters.AddWithValue("@IC", Catcb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@IQ", QuantityTb.Text);
                    cmd.Parameters.AddWithValue("@IBP", BPriceTb.Text);
                    cmd.Parameters.AddWithValue("@ISP", SPriceTb.Text);
                    cmd.Parameters.AddWithValue("@IP", Profit);
                    cmd.Parameters.AddWithValue("@ID", ProdDetailTb.Text);
                    cmd.Parameters.AddWithValue("@IADate", ProdDate.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm sản phẩm !!!");
                    Con.Close();
                    //cập nhật list
                    Methods Obj = new Methods();
                    Obj.DisplayData1("ItemTb1", ProductDGV);
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("delete from ItemTb1 where IdId = @PK", Con);

                    cmd.Parameters.AddWithValue("@PK", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa sản phẩm !!!");
                    Con.Close();
                    //Cập nhật list
                    Methods Obj = new Methods();
                    Obj.DisplayData1("ItemTb1", ProductDGV);
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;

        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdNameTb.Text = ProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            Catcb.Text = ProductDGV.SelectedRows[0].Cells[2].Value.ToString();
            QuantityTb.Text = ProductDGV.SelectedRows[0].Cells[3].Value.ToString();
            BPriceTb.Text = ProductDGV.SelectedRows[0].Cells[4].Value.ToString();
            SPriceTb.Text = ProductDGV.SelectedRows[0].Cells[5].Value.ToString();
            ProdDetailTb.Text = ProductDGV.SelectedRows[0].Cells[7].Value.ToString();
            ProdDate.Text = ProductDGV.SelectedRows[0].Cells[8].Value.ToString();
            if (ProdNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ProdNameTb.Text == "" || ProdDetailTb.Text == "" || SPriceTb.Text == "" || BPriceTb.Text == "" || ProdDetailTb.Text == "")
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                try
                {
                    int Profit = Convert.ToInt32(SPriceTb.Text) - Convert.ToInt32(BPriceTb.Text);
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update ItemTb1 set ItName=@IN, ItCat=@IC, ItQty=@IQ, ItBprice=@IBP, ItSprice=@ISP, ItProfit=@IP, ItDetails=@ID, ItAddDate=@IADate where IdId = @PKey", Con);
                    cmd.Parameters.AddWithValue("@IN", ProdNameTb.Text);
                    cmd.Parameters.AddWithValue("@IC", Catcb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@IQ", QuantityTb.Text);
                    cmd.Parameters.AddWithValue("@IBP", BPriceTb.Text);
                    cmd.Parameters.AddWithValue("@ISP", SPriceTb.Text);
                    cmd.Parameters.AddWithValue("@IP", Profit);
                    cmd.Parameters.AddWithValue("@ID", ProdDetailTb.Text);
                    cmd.Parameters.AddWithValue("@IADate", ProdDate.Value.Date);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã sửa sản phẩm !!!");
                    Con.Close();
                    //cập nhật list
                    Methods Obj = new Methods();
                    Obj.DisplayData1("ItemTb1", ProductDGV);
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Category Obj = new Category();
            Obj.Show();
            //this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Billing Obj = new Billing();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void BPriceTb_TextChanged(object sender, EventArgs e)
        {

        }
        private void SearchItem()
        {
            Con.Open();
            string Query = "select * from ItemTb1 Where ItName Like '%"+ SearchTb.Text+"%'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ShowItem()
        {
            Con.Open();
            string Query = "select * from ItemTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Searchbtn_Click_1(object sender, EventArgs e)
        {
            SearchItem();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            ShowItem();
            SearchTb.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }
}


