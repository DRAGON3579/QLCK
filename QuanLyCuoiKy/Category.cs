using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace QuanLyCuoiKy
{
    public partial class Category : Form
    {
        public Category()
        {
            InitializeComponent();
            Methods Obj = new Methods();
            Obj.DisplayData("CategoryTb1", CategoryDGV);
        }
        //SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDb;Integrated Security=True");
        // SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDB1;Integrated Security=True");
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDB2;Integrated Security=True");
        //Thêm danh mục
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if(CatNameTb.Text == "")
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CategoryTb1(CatName)values(@CN)", Con);
                    cmd.Parameters.AddWithValue("@CN", CatNameTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm danh mục !!!");
                    Con.Close();
                    //cập nhật list
                    Methods Obj = new Methods();
                    Obj.DisplayData("CategoryTb1", CategoryDGV);
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (CatNameTb.Text == "")
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update CategoryTb1 set CatName = @CN where Catid = @CK", Con);
                    cmd.Parameters.AddWithValue("@CN", CatNameTb.Text);
                    cmd.Parameters.AddWithValue("@CK", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã sửa danh mục !!!");
                    Con.Close();
                    //Cập nhật list
                    Methods Obj = new Methods();
                    Obj.DisplayData("CategoryTb1", CategoryDGV);
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void CategoryDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatNameTb.Text = CategoryDGV.SelectedRows[0].Cells[1].Value.ToString();
            if(CatNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CategoryDGV.SelectedRows[0].Cells[0].Value.ToString());
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
                    SqlCommand cmd = new SqlCommand("delete from CategoryTb1 where Catid = @CK", Con);
                    
                    cmd.Parameters.AddWithValue("@CK", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa danh mục !!!");
                    Con.Close();
                    //Cập nhật list
                    Methods Obj = new Methods();
                    Obj.DisplayData("CategoryTb1", CategoryDGV);
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
