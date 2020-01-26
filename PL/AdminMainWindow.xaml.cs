using BE;
using BL;
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
using System.Windows.Shapes;
namespace PL
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void TesterTab_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void OrdersTab_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TabAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new GetOrdersListWindow().ShowDialog();

        }

        private void ButtonHostingUnits_Click(object sender, RoutedEventArgs e)
        {
            new GetHostingUnitListWindow().ShowDialog();
        }

        private void ButtonHosts_Click(object sender, RoutedEventArgs e)
        {
            new GetAllHostListWindow().ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new GetAllGuestRequestsListWindow().ShowDialog();
        }
    }

}