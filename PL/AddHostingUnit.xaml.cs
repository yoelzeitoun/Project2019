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
    /// Interaction logic for AddHostingUnit.xaml
    /// </summary>
    public partial class AddHostingUnit : Window
    {
        IBL bL;
        Host currentHost;
        string eMail;
        public AddHostingUnit()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bL = Factory_BL.getBL();
            
        }
        public AddHostingUnit(string email) :this()
        {
            currentHost = bL.GetHost(email);
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            HostingUnit hostingUnit = new HostingUnit();
            hostingUnit.Owner = currentHost;
            //hostingUnit.area = (Area)areaComboBox.SelectedItem;
            hostingUnit.jacuzzi = (bool)jaccuziBox.IsChecked ? Jaccuzzi.Yes : Jaccuzzi.No;
            hostingUnit.pool = (bool)poolBox.IsChecked ? Pool.Yes : Pool.No;
            hostingUnit.childrenAttractions = (bool)childrenAttractionBox.IsChecked ? ChildrensAttractions.Yes : ChildrensAttractions.No;
            hostingUnit.garden = (bool)gardenBox.IsChecked ? Garden.Yes : Garden.No;
            //hostingUnit.type = (Type)HostingUnitTypeComboBox.SelectedItem;
            hostingUnit.NumOfAdults = int.Parse(NumOfAdultsTextBox.Text);
            hostingUnit.NumOfChildren = int.Parse(NumOfChildrenTextBox.Text);
            hostingUnit.City = cityTextBox1.Text;
            hostingUnit.Street = streetTextBox1.Text;
            hostingUnit.HouseNumber = houseNumberTextBox1.Text;
            bL.AddHostingUnit(hostingUnit);
            MessageBox.Show($"Bravo!", "BRAVO", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
