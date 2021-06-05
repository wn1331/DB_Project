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
    /// Pointcharge.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Pointcharge : Page
    {
        public Pointcharge()
        {
            InitializeComponent();
            this.DataContext = this;
            string connect_money = "Server=localhost;Database=pcshop;UId=root;Password=020512;";
            MySqlConnection connection_money = new MySqlConnection(connect_money);
            MySqlCommand cmd_money = new MySqlCommand("select * from user", connection_money);//유저테이블의 모든 요소 갖고옴
            connection_money.Open();

            DataTable dt_money = new DataTable();
            dt_money.Load(cmd_money.ExecuteReader());//그걸 데이터테이블에 저장
            List<User> users = dt_money.DataTableToList<User>();//그 데이터테이블을 리스트화. users리스트에 user테이블의 모든 값 들어옴

            User usersession = users.Single((x)=>x.user_id.ToString()==(string)Application.Current.Properties["loginID"]);//ID 세션으로 유저정보 가져옴
            int userMoney = usersession.user_point;
            userPoint.Text = userMoney.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connect_money = "Server=localhost;Database=pcshop;UId=root;Password=020512;";
            MySqlConnection connection_money = new MySqlConnection(connect_money);
            MySqlCommand cmd_money = new MySqlCommand("select * from user", connection_money);//유저테이블의 모든 요소 갖고옴
            connection_money.Open();

            DataTable dt_money = new DataTable();
            dt_money.Load(cmd_money.ExecuteReader());//그걸 데이터테이블에 저장
            List<User> users = dt_money.DataTableToList<User>();//그 데이터테이블을 리스트화. users리스트에 user테이블의 모든 값 들어옴

            User usersession = users.Single((x) => x.user_id.ToString() == (string)Application.Current.Properties["loginID"]);//ID 세션으로 유저정보 가져옴

            int userMoney = usersession.user_point;
            try
            {
                userMoney += int.Parse(chargeMoney.Text);
                cmd_money.Connection = connection_money;
                cmd_money.CommandText = "update user set user_point ="+userMoney+"" + " where user_id='" +usersession.user_id+ "';";
                cmd_money.ExecuteNonQuery();
                userPoint.Text = userMoney.ToString();
                MessageBox.Show("정상 입금 되었습니다!");
            }
            catch
            {
                MessageBox.Show("입금할 돈을 입력하세요!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/Main.xaml", UriKind.Relative));
        }
    }
}
