using MySql.Data.MySqlClient;
using PcApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Orderlist.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Orderlist : Page
    {
        public Orderlist()
        {
            InitializeComponent(); this.DataContext = this;
            string connectionString = "Server=localhost;Database=pcshop;UId=root;Password=020512;";

            //유저테이블 데이터 가져오기
            MySqlConnection connection_user = new MySqlConnection(connectionString);
            MySqlCommand cmd_user = new MySqlCommand("select * from user", connection_user);
            connection_user.Open();
            DataTable dt_user = new DataTable();
            dt_user.Load(cmd_user.ExecuteReader());
            List<User> users = dt_user.DataTableToList<User>();
            //유저세션
            User usersession = users.Single((x) => x.user_id.ToString() == (string)Application.Current.Properties["loginID"]);


            //오더테이블 데이터 가쟈오기
            MySqlConnection connection_orders = new MySqlConnection(connectionString);
            MySqlCommand cmd_orders = new MySqlCommand("select * from orders where user_id='"+usersession.user_id+"';", connection_orders);
            connection_orders.Open();
            DataTable dt_orders = new DataTable();
            dt_orders.Load(cmd_orders.ExecuteReader());

            List<Orders> orders = dt_orders.DataTableToList<Orders>();

            //제품테이블 데이터 가져오기
            MySqlConnection connection_part = new MySqlConnection(connectionString);
            MySqlCommand cmd_part = new MySqlCommand("select * from part", connection_part);
            connection_part.Open();
            DataTable dt_part = new DataTable();
            dt_part.Load(cmd_part.ExecuteReader());
            List<Part> parts = dt_part.DataTableToList<Part>();



            dtGrid.DataContext = orders;

            connection_user.Close();
            connection_orders.Close();
            connection_part.Close();
        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)//클릭했을 시에 왼쪽에 뜨는 창
        {
            Orders myrow = (Orders)dtGrid.CurrentCell.Item;
            partname.Text = myrow.part_id.ToString();
            orderid.Text = myrow.orders_id.ToString();
            Application.Current.Properties["PartNumsession"] = myrow.part_id;
            
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)//리뷰작성버튼
        {
            string addreview=reviewtext.Text;
            string connectionString = "Server=localhost;Database=pcshop;UId=root;Password=020512;";

            //유저리스트받아오고세션저장

            MySqlConnection connection_user = new MySqlConnection(connectionString);
            MySqlCommand cmd_user = new MySqlCommand("select * from user", connection_user);
            connection_user.Open();
            DataTable dt_user = new DataTable();
            dt_user.Load(cmd_user.ExecuteReader());
            List<User> users = dt_user.DataTableToList<User>();
            
            User usersession = users.Single((x) => x.user_id.ToString() == (string)Application.Current.Properties["loginID"]);
            //제품리스트받아오고세션저장
            MySqlConnection connection_part = new MySqlConnection(connectionString);
            MySqlCommand cmd_part = new MySqlCommand("select * from part", connection_part);
            connection_part.Open();
            DataTable dt_part = new DataTable();
            dt_part.Load(cmd_part.ExecuteReader());
            List<Part> parts = dt_part.DataTableToList<Part>();
            //오더리스트받아오고세션저장
            Part partsession = parts.Single((x) => x.part_id.ToString() == Application.Current.Properties["PartNumsession"].ToString());
            try
            {
                //review에 유저아이디와 리뷰내용 저장.
                MySqlConnection conn = new MySqlConnection("Server=localhost;Database=pcshop;UId=root;Password=020512;");
                MySqlCommand comm = new MySqlCommand();
                
                conn.Open();
                comm.CommandText = "INSERT INTO review(user_id, review_text) VALUES (@user_id, @review_text)";
                
                comm.Parameters.Add("@user_id", MySqlDbType.Text).Value = usersession.user_id;
                comm.Parameters.Add("@review_text", MySqlDbType.Text).Value = addreview;

                comm.Connection = conn;
                
                comm.ExecuteNonQuery();
                conn.Close();

                MySqlConnection connection_review = new MySqlConnection(connectionString);
                MySqlCommand cmd_review = new MySqlCommand("select *from review", connection_review);
                connection_review.Open();
                DataTable dt_review = new DataTable();
                dt_review.Load(cmd_review.ExecuteReader());
                List<Review> reviews = dt_review.DataTableToList<Review>();
                int max = Convert.ToInt32(dt_review.AsEnumerable().Max(row => row["review_id"]));
                Review reviewsession = reviews.Single((x) => x.review_id.Equals(max));

                //review_parts에 데이터 집어넣기
                MySqlConnection conn2 = new MySqlConnection("Server=localhost;Database=pcshop;UId=root;Password=020512;");
                MySqlCommand comm2 = new MySqlCommand();

                conn2.Open();
                comm2.CommandText = "INSERT INTO review_parts(part_id, review_id) VALUES (@part_id, @review_id)";
                
                comm2.Parameters.Add("@part_id", MySqlDbType.Text).Value = partsession.part_id;
                comm2.Parameters.Add("@review_id", MySqlDbType.Text).Value = reviewsession.review_id;

                comm2.Connection = conn2;

                comm2.ExecuteNonQuery();
                conn2.Close();

                MessageBox.Show("리뷰를 작성했습니다!");
                



            }
            catch (Exception ex)
            {
                MessageBox.Show("에러_1!");
            }

           
        }
    }
}
