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
using BE;
using BL;
using PL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private HostingUnit hostingUnit;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Guest_Request_Button_Click(object sender, RoutedEventArgs e)
        {
            new GuestRequestWindows().ShowDialog();
        }

        private void Hosting_Units_Button_Click(object sender, RoutedEventArgs e)
        {
            new HostingUnitWindows().ShowDialog();
        }

        private void Administrator_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void More_Window_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
