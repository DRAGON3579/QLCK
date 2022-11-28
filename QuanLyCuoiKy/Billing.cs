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
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            Methods Obj = new Methods();
            Obj.DisplayData1("ItemTb1", ItemsDGV);
            Methods Obj1 = new Methods();
            Obj1.DisplayData3("SaleTb1", BillsDGV);
            ULabel.Text = Login.UName;
            GetUid();
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Items Obj = new Items();
            Obj.Show();
            this.Hide();
        }
        //SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDb;Integrated Security=True");
        // SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDB1;Integrated Security=True");
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDB2;Integrated Security=True");
        int n = 0;
        int GrdTotal = 0;

        private void UpdateItems(){
            try
            {
                int NewQty = Stock - Convert.ToInt32(QuantityTb.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("update ItemTb1 set ItQty=@IQ where IdId = @PKey", Con);              
                cmd.Parameters.AddWithValue("@IQ", NewQty);
                cmd.Parameters.AddWithValue("@PKey", Key);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Đã sửa sản phẩm !!!");
                Con.Close();
                //cập nhật list 
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }
        private void AddToBillBtn_Click(object sender, EventArgs e)
        {
            //add to clint bill
            if(QuantityTb.Text == "" || Convert.ToInt32(QuantityTb.Text) > Stock)
            {
                MessageBox.Show("Nhập Đúng Số Lượng");
            } else
            {
                int total = Convert.ToInt32(QuantityTb.Text) * Convert.ToInt32(PriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(ClientBillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ProdNameTb.Text;
                newRow.Cells[2].Value = PriceTb.Text;
                newRow.Cells[3].Value = QuantityTb.Text;
                newRow.Cells[4].Value = total;
                ClientBillDGV.Rows.Add(newRow);
                //Tính tổng tiền thanh toán
                GrdTotal = GrdTotal + total;
                GrdTotalss.Text = "Tổng Tiền :" + GrdTotal;
                n++;
                UpdateItems();
                Methods Obj = new Methods();
                Obj.DisplayData1("ItemTb1", ItemsDGV);
            }
        }

        int Key = 0;
        int Stock = 0;
        private void ItemsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdNameTb.Text = ItemsDGV.SelectedRows[0].Cells[1].Value.ToString();

            Stock = Convert.ToInt32(ItemsDGV.SelectedRows[0].Cells[3].Value.ToString());
          
            PriceTb.Text = ItemsDGV.SelectedRows[0].Cells[5].Value.ToString();
            
            if (ProdNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ItemsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            ProdNameTb.Text = "";
            QuantityTb.Text = "";
            PriceTb.Text = "";
            Stock = 0;
            GrdTotalss.Text = "Tổng Tiền :";
            ClientBillDGV.Rows.Clear();

        }
        private void SaveBill()
        {
            try
            {
               
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into SaleTb1(SDate, SCustomer, SUser, SAmount) values(@SD, @SC, @SU, @SA)", Con);
                cmd.Parameters.AddWithValue("@SD", BillDate.Value.Date);
                cmd.Parameters.AddWithValue("@SC", BCustomer.Text);
                cmd.Parameters.AddWithValue("@SU", UserId);
                cmd.Parameters.AddWithValue("@SA", GrdTotal);
        
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm Bill!!!");
                Con.Close();
                //cập nhật list
                Methods Obj = new Methods();
                Obj.DisplayData3("SaleTb1", BillsDGV);
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
        }
        int UserId;
        private void GetUid()
        {
            Con.Open();
            string query = "select UId from UserTb1 where UName='" + ULabel.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Con);    
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                UserId = Convert.ToInt32(dr["UID"].ToString());
            }
            Con.Close() ;
        }

        private void PrintBtn_Click_1(object sender, EventArgs e)
        {
            SaveBill();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            
            ClientBillDGV.Rows.Clear();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int prodid, prodqity, prodprice, total, pos = 60;

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Dashboard Obj = new Dashboard();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Cửa Hàng Văn Phòng Phẩm ", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("STT SảnPhẩm Giá SốLượng TổngTiền", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach (DataGridViewRow row in ClientBillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = " " + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column4"].Value);
                prodqity = Convert.ToInt32(row.Cells["Column5"].Value);
                total = Convert.ToInt32(row.Cells["Column3"].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqity, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + total, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Tổng Tiền: " + GrdTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("*******Statitonary********" + GrdTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));
            ClientBillDGV.Rows.Clear();
            ClientBillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;

        }
    }
}
