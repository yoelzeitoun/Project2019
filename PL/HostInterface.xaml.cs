using Microsoft.Win32;
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
using BL;
using BE;

namespace PL
{
    /// <summary>
    /// Interaction logic for HostInterface.xaml
    /// </summary>
    public partial class HostInterface : Window
    {
        string eMail;
        public HostInterface()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public HostInterface(string email) :this()
        {
            eMail = email;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog op = new OpenFileDialog();
            //op.Title = "Select a picture";
            //op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            //  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            //  "Portable Network Graphic (*.png)|*.png";
            //if (op.ShowDialog() == true)
            //{
            //    myPicture.Source = new BitmapImage(new Uri(op.FileName));
            //}
            this.Close();
            new AddHostingUnit(eMail).ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            new UpdateHostingUnit(eMail).ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            new RemoveHostingUnit(eMail).ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new HostOrder().ShowDialog();
        }
    }
}
