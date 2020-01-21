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
    /// Interaction logic for RemoveHostingUnit.xaml
    /// </summary>
    public partial class RemoveHostingUnit : Window
    {
        string email;
        IBL bL;
        public RemoveHostingUnit()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bL = Factory_BL.getBL();
        }
        public RemoveHostingUnit(string eMail) : this()
        {
            email = eMail;
            comboB.ItemsSource = bL.HostingUnitList(eMail);
        }

        private void comboB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure to remove this Hosting Unit?", "OK!", MessageBoxButton.YesNo, MessageBoxImage.Information);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if(bL.DeleteHostingUnit(email, comboB.SelectedItem.ToString()))
                        MessageBox.Show($"You Successfully removed the Hosting Unit", "OK!", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
