using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
namespace QuanLyCuoiKy
{
    internal class Methods
    {
        //SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDb;Integrated Security=True");
        // SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDB1;Integrated Security=True");
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDB2;Integrated Security=True");
        public void DisplayData(string Table, DataGridView DGV)
         {
             Con.Open();
             string Query = "select Catid as N'ID Danh Mục', CatName as N'Danh Mục' from CategoryTb1";
             SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
             SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
             var ds = new DataSet();
             sda.Fill(ds);
             DGV.DataSource = ds.Tables[0];
             Con.Close();

         }
        
        //SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-VL7UGOA\SQLEXPRESS;Initial Catalog=StationeryDb;Integrated Security=True");
        public void DisplayData1(string Table, DataGridView DGV)
        {
            Con.Open();
            string Query = "select tb1.IdId as N'Mã SP', tb1.ItName as N'Tên SP', tb2.CatName as N'Danh Mục'," +
                "tb1.ItQty as N'Số Lượng', tb1.ItBprice as N'Giá Nhập', tb1.ItSprice as N'Giá Bán', tb1.ItProfit as N'Lợi Nhuận', tb1.ItDetails as N'Mô Tả SP', tb1.ItAddDate as N'Thời Gian Nhập Hàng'" +
                "   from ItemTb1 tb1 join CategoryTb1 tb2 on tb1.ItCat = tb2.Catid ";
            
            //string Query = "select * from " + Table + "";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        public void DisplayData2(string Table, DataGridView DGV)
        {
            Con.Open();
            //string Query = "select * from UserTb1";
            string Query = "select UId as N'Mã Nhân Viên', UName as 'User Name', UEmail as N'Email', UDOB as N'Ngày Sinh', UGen as N'Giới Tính', UPhone as N'SĐT',UPassword from UserTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con); 
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGV.DataSource = ds.Tables[0];
            Con.Close();

        }
        public void DisplayData3(string Table, DataGridView DGV)
        {
            Con.Open();
            string Query = "select SNum as 'Số Hóa Đơn', SDate as N'Thời Gian', SCustomer as N'Khách Hàng', SUser, SAmount as N'Tổng Tiền' from SaleTb1";         
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DGV.DataSource = ds.Tables[0];
            Con.Close();

        }

    }
}
