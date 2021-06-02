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
    /// Main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)//로그아웃
        {
            NavigationService.Navigate(new Uri("/view/Home.xaml", UriKind.Relative));

        }

        private void Button_Click(object sender, RoutedEventArgs e)//주문내역
        {
            NavigationService.Navigate(new Uri("/view/Orderlist.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//퍼ㅗ인트충전
        {
            NavigationService.Navigate(new Uri("/view/Pointcharge.xaml", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/view/cpu.xaml", UriKind.Relative));
        }
    }
}
