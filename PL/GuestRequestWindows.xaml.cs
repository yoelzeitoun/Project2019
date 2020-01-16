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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestRequestWindows.xaml
    /// </summary>
    public partial class GuestRequestWindows : Window
    {
        public GuestRequestWindows()
        {
            InitializeComponent();
            InitializeComponent();
            AreaComboBox.ItemsSource = Enum.GetValues(typeof(Area));
            HostingUnitTypeComboBox.ItemsSource = Enum.GetValues(typeof(Type));
            pool.ItemsSource = Enum.GetValues(typeof(Pool));
            jacuzzi.ItemsSource = Enum.GetValues(typeof(Jaccuzzi));
            garden.ItemsSource = Enum.GetValues(typeof(Garden));
            childrensAttraction.ItemsSource = Enum.GetValues(typeof(ChildrensAttractions));
        }

        private void HostingUnitTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            GuestRequest guestRequest = new GuestRequest();
            guestRequest.FirstName = firstNameTextBox.Text;
        }

        private void firstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void maxWeeklyTestsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cellPhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void experienceYearsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void idTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void genderTesterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void maxDistanceTesterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void personalStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void workerTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void houseNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void streetTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AreaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void totalNumOfPerson_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void jacuzzi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void garden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void childrensAttraction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
