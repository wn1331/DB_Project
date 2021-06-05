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
    /// reviewPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class reviewPage : Page
    {
        public reviewPage()
        {
            InitializeComponent(); this.DataContext = this;
            string connectionString = "Server=localhost;Database=pcshop;UId=root;Password=020512;";
            MySqlConnection connection_review_parts = new MySqlConnection(connectionString);
            MySqlCommand cmd_review_parts = new MySqlCommand("select * from review where review_id in (select review_id from review_parts where part_id = '" + Application.Current.Properties["PartNumsession"] + "');", connection_review_parts);
            connection_review_parts.Open();
            DataTable dt_review_parts = new DataTable();
            dt_review_parts.Load(cmd_review_parts.ExecuteReader());
            List<Review> review_Parts = dt_review_parts.DataTableToList<Review>();
            dtGrid.DataContext = review_Parts;
            connection_review_parts.Close();
        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)//클릭시 가져오는것
        {
            Review myrow = (Review)dtGrid.CurrentCell.Item;

            reviewid.Text = myrow.user_id;
            reviewtext.Text = myrow.review_text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/cpu.xaml", UriKind.Relative));
        }
    }
}
