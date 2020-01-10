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
using System.Xml.Linq;
using System.Xml.XmlConfiguration;
using System.Xml;
using BE;
using BL;


namespace PL
{
    /// <summary>
    /// Interaction logic for HostingUnitWindows.xaml
    /// </summary>
    public partial class HostingUnitWindows : Window
    {
        BL.IBL bL;
        public HostingUnitWindows()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Host host = new Host();
            host.MailAddress = email.Text;
            host.Password = pword.Text;
            host.FirstName = "yoel";
            host.LastName = "zeitoun";
            bL.AddHost(host);
            Console.WriteLine("bravo!");
            //var UName = email.Text;
            //var PWord = pword.Text;
            //XElement config = XElement.Load("../Resources/config.xml");
            //var query = from o in config.Root.Elements("user")
            //            where (string)o.Element("username") == UName
            //            select (string)o.Element("username").Value;
            //var query2 = from o in config.Root.Elements("user")
            //             where (string)o.Element("username") == UName
            //             select o.Element("password").Value;
            //var password = query2.ToString();
            //if (PWord == password)
            //{
            //    NavigationService service = NavigationService.GetNavigationService(this);
            //    service.Navigate(new Uri("MainMenu.xaml", UriKind.RelativeOrAbsolute));
            //}
            //else
            //{
            //    LblError.Content = "Username or Password Incorrect !";
            //}
            new HostInterface().ShowDialog();
        }
    }
}
