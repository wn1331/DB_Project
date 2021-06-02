using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    /// Join.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Join : Page
    {
        public Join()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)//가입버튼
        {
            //모든정보입력하도록
            if (string.IsNullOrEmpty(textbox5.Text))
            {
                MessageBox.Show("아이디를 입력해주세요");
                this.textbox5.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textbox1.Text))
            {
                MessageBox.Show("이름을 입력해주세요");
                this.textbox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textbox2.Text))
            {
                MessageBox.Show("패스워드를 입력해주세요");
                this.textbox2.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textbox3.Text))
            {
                MessageBox.Show("핸드폰번호를 입력해주세요");
                this.textbox3.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textbox4.Text))
            {
                MessageBox.Show("주소를 입력해주세요");
                this.textbox4.Focus();
                return;
            }




            //db에 textbox내용으로 usertable만들기
            try
            {
                MySqlConnection conn = new MySqlConnection("Server=localhost;Database=pcshop;UId=root;Password=020512;");
                MySqlCommand comm = new MySqlCommand();
                conn.Open();


                comm.CommandText = "INSERT INTO user(user_id, user_name, user_pwd, user_phone, user_address, user_point) VALUES (@user_id, @user_name, @user_pwd, @user_phone, @user_address, @user_point)";
                comm.Parameters.Add("@user_id", MySqlDbType.Text).Value = textbox5.Text;
                comm.Parameters.Add("@user_name", MySqlDbType.Text).Value = textbox1.Text;
                comm.Parameters.Add("@user_pwd", MySqlDbType.Text).Value = textbox2.Text;
                comm.Parameters.Add("@user_phone", MySqlDbType.Text).Value = textbox3.Text;
                comm.Parameters.Add("@user_address", MySqlDbType.Text).Value = textbox4.Text;
                comm.Parameters.Add("@user_point", MySqlDbType.Int32).Value = 0;

                comm.Connection = conn;

                comm.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("회원가입 되었습니다.");
                NavigationService.Navigate(new Uri("/view/Home.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show("아이디 중복"); }
        }
    }
}
