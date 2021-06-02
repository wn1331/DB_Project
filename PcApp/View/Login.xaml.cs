using MySql.Data.MySqlClient;
using PcApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PcApp.View
{
    /// <summary>
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//로그인버튼
        {
            if (string.IsNullOrEmpty(textbox1.Text))
            {
                MessageBox.Show("Id를 입력해주세요");
                this.textbox1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(passwordbox.Password))
            {
                MessageBox.Show("password를 입력해주세요");
                this.passwordbox.Focus();
                return;
            }

            string user_id = textbox1.Text;
            string user_pwd = passwordbox.Password;



            //데이터 연동 시작
            string connectionString = "Server=localhost;Database=pcshop;UId=root;Password=020512;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from user", connection);
            connection.Open();

            DataTable dt = new DataTable();

            dt.Load(cmd.ExecuteReader());//데이터테이블을 sql에 접근해서 전부 로드함
            List<User> Users = dt.DataTableToList<User>();//그것들을 리스트화해서 Users에 저장.

            try
            {
                User user1 = Users.Single((x) => x.user_id.Equals(user_id));//id가 일치한 하나의객체를가져옴.


                if (user1.user_pwd.Equals(user_pwd))
                {
                    Application.Current.Properties["loginID"] = user1.user_id;//세션에 저장. 세션은 아니지만 프로퍼티라고 C#에서 사용하는 비슷한 기능.
                    MessageBox.Show("로그인 되었습니다.");
                    NavigationService.Navigate(new Uri("/view/Main.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("비밀번호가 틀렸습니다.");
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("존재하지 않는 아이디입니다.");
            }

            

        }
    }
}
