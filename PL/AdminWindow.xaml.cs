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
using BE;
using BL;
namespace PL
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GoBotton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Id.Text == Configuration.Admin_PASSWORD)
                {
                    this.Close();
                    new AdminMainWindow().ShowDialog();
                }
                else
                    MessageBox.Show("please enter a correct code!");
            }
            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }
        }
        private void ID_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Id.Text == "Enter your Password")
                Id.Clear();
        }
    }
}