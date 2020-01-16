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
    /// Interaction logic for UpdateHostingUnit.xaml
    /// </summary>
    public partial class UpdateHostingUnit : Window
    {
        IBL bL;
        public UpdateHostingUnit()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bL = Factory_BL.getBL();
            
        }
        public UpdateHostingUnit(string eMail) :this()
        {
            comboB.ItemsSource = bL.HostingUnitList(eMail);
        }

        private void comboB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
