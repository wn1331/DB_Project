using MySql.Data.MySqlClient;
using PcApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PcApp.View
{
    /// <summary>
    /// cpu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class cpu : Page
    {

        Part parts = new Part();
        public cpu()
        {
            InitializeComponent(); this.DataContext = this;
            string connectionString = "Server=localhost;Database=pcshop;UId=root;Password=020512;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from part", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            List<Part> parts = dt.DataTableToList<Part>();


            dtGrid.DataContext = parts;
            connection.Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //////셀 선택한 값 textbox 집어넣기////
        {
            try
            {

                Part myrow = (Part)dtGrid.CurrentCell.Item;

                pt_name.Text = myrow.part_name.ToString();
                ptcompany.Text = myrow.part_company.ToString();
                ptprice.Text = myrow.part_price.ToString();
                ptcategory.Text = myrow.part_category.ToString();
                ptquantity.Text = myrow.part_quantity.ToString();
                
                Application.Current.Properties["PartNumsession"] = myrow.part_id;

                //string connectionString = "Server=localhost;Database=pcshop;UId=root;Password=020512;";
                //MySqlConnection connection_review_parts = new MySqlConnection(connectionString);
                //MySqlCommand cmd_review_parts = new MySqlCommand("select * from review where review_id in (select review_id from review_parts where part_id = '"+myrow.part_id+"');", connection_review_parts);
               
                
                //connection_review_parts.Open();
                //DataTable dt_review_parts = new DataTable();
                //dt_review_parts.Load(cmd_review_parts.ExecuteReader());
                //List<Review> review_Parts = dt_review_parts.DataTableToList<Review>();

                

            }
            catch (Exception ex)
            {
                MessageBox.Show("?오류");
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)//검색버튼
        {
            string connectionString = "Server=localhost;Database=pcshop;UId=root;Password=020512;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from part where part_category='"+searchbox.Text+"';", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            List<Part> parts = dt.DataTableToList<Part>();

            
            dtGrid.DataContext = parts;
            connection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//구매버튼
        {
            string connectionString = "Server=localhost;Database=pcshop;UId=root;Password=020512;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from part", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            
            MySqlConnection connection_user = new MySqlConnection(connectionString);
            MySqlCommand cmd_user = new MySqlCommand("select * from user", connection_user);
            connection_user.Open();
            DataTable dt_user = new DataTable();
            dt_user.Load(cmd_user.ExecuteReader());

            MySqlConnection connection_orders = new MySqlConnection(connectionString);
            MySqlCommand cmd_orders = new MySqlCommand("select * from orders", connection_orders);
            connection_orders.Open();
            DataTable dt_orders = new DataTable();
            dt_orders.Load(cmd_orders.ExecuteReader());//오더리스트 데이터가져오기

            List<Part> parts = dt.DataTableToList<Part>();
            List<User> users = dt_user.DataTableToList<User>();
            List<Orders> orders = dt_orders.DataTableToList<Orders>();
            Part partsession = parts.Single((x) => x.part_id.ToString() == (string)Application.Current.Properties["PartNumsession"]);//세션으로가져옴
            User usersession = users.Single((x) => x.user_id.ToString() == (string)Application.Current.Properties["loginID"]);
            int partQuantity = int.Parse(partsession.part_quantity);
            


            



            try
            {
               
                if (usersession.user_point > partsession.part_price) { 
                    partQuantity -= 1;
                    cmd.Connection = connection;
                    cmd_user.Connection = connection_user;
                    cmd_orders.Connection = connection_orders;
                    cmd.CommandText = "update part set part_quantity =" + partQuantity + "" + " where part_id='" + partsession.part_id + "';"; //제품의 재고 1개 감소
                    cmd_user.CommandText = "update user set user_point=user_point-" + partsession.part_price + " where user_id='" + usersession.user_id + "';";//제품을 구매한 유저의 포인트 소모
                    cmd_orders.CommandText = "insert into pcshop.orders (user_id,part_id) values ('"+usersession.user_id+"','"+partsession.part_id+"');";//제품 구매 후 

                    MessageBox.Show("구매 성공!");
                    NavigationService.Navigate(new Uri("/view/Main.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("돈 부족!");
                }
                cmd.ExecuteNonQuery();
                cmd_user.ExecuteNonQuery();
                cmd_orders.ExecuteNonQuery();


            }
            finally
            {

            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/reviewPage.xaml", UriKind.Relative));
        }
    }
}
