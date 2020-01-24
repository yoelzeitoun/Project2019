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
    /// Interaction logic for HostOrder.xaml
    /// </summary>
    public partial class HostOrder : Window
    {
        string email;
        IBL bL;
        public HostOrder()
        {
            InitializeComponent();
            
            //for (int i = 0; i < 10; ++i)
            //{
            //    ListBoxItem newItem = new ListBoxItem();
            //    newItem.Content = "Item " + i;
            //    hostListBox.Items.Add(newItem);
            //}
        }
        public HostOrder(string eMail) : this()
        {
            bL = Factory_BL.getBL();
            var hostingUnits = bL.HostingUnitList(eMail);
            var guestRequests = bL.GuestRequestList();
            int index = 0;
            foreach (var guest in guestRequests)
                foreach (var hosting in hostingUnits)
                {
                    HostingUnit hostingUnit = bL.GetHostingUnit(eMail, hosting);
                    GuestRequestOrderUserControl a = new GuestRequestOrderUserControl(hostingUnit);
                    MainGrid.Children.Add(a);
                    Grid.SetRow(a, index + 1);
                    index++;
                }
        }

        private void hostListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
