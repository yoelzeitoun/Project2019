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
using PL;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Navigation;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestRequestWindows.xaml
    /// </summary>
    public partial class GuestRequestWindows : Window
    {
        IBL bL;
        GuestRequest guestRequest = new GuestRequest();
        public GuestRequestWindows()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DataContext = this;
            bL = Factory_BL.getBL();
            AreaComboBox.ItemsSource = Enum.GetValues(typeof(Area));
            AreaComboBox.SelectedIndex = 0;
            HostingUnitTypeComboBox.ItemsSource = Enum.GetValues(typeof(Type));
            HostingUnitTypeComboBox.SelectedIndex = 0;
            pool.ItemsSource = Enum.GetValues(typeof(Pool));
            pool.SelectedIndex = 2;
            jacuzzi.ItemsSource = Enum.GetValues(typeof(Jaccuzzi));
            jacuzzi.SelectedIndex = 2;
            garden.ItemsSource = Enum.GetValues(typeof(Garden));
            garden.SelectedIndex = 2;
            childrensAttraction.ItemsSource = Enum.GetValues(typeof(ChildrensAttractions));
            childrensAttraction.SelectedIndex = 2;
        }

        private void HostingUnitTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            guestRequest.FirstName = firstNameTextBox.Text;
            guestRequest.LastName = lastNameTextBox.Text;
            guestRequest.MailAddress = emailTextBox.Text;
            guestRequest.PhoneNumber = phoneNumberTextBox.Text;
            guestRequest.NumAdults = int.Parse(numbersOfAdultsTextBox.Text);
            guestRequest.NumChildren = int.Parse(NumOfChildrentextBox.Text);
            guestRequest.TotalNumPersons = int.Parse(numbersOfAdultsTextBox.Text + NumOfChildrentextBox.Text);
            guestRequest.EntryDate = entryDate.SelectedDate.Value;
            guestRequest.ReleaseDate = ReleaseDateDatePicker_Copy.SelectedDate.Value;
            guestRequest.area = (Area)AreaComboBox.SelectedItem;
            guestRequest.jacuzzi = (Jaccuzzi)jacuzzi.SelectedItem;
            guestRequest.pool = (Pool)pool.SelectedItem;
            guestRequest.childrenAttractions = (ChildrensAttractions)childrensAttraction.SelectedItem;
            guestRequest.garden = (Garden)garden.SelectedItem;
            guestRequest.type = (Type)HostingUnitTypeComboBox.SelectedItem;

            bL.AddGuestRequest(guestRequest);

            MessageBox.Show($"your request has been successfully added,\n" + " one of us Hosts will contact you in the brief delay!  ", "OK!", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
            //new GuestRequestOrder().ShowDialog();

        }

        private void firstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void personalStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void AreaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void phoneNumberTextBox_TextChanged(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(e.Text) && e.Text != "\r")
            {
                e.Handled = true;
                MessageBox.Show("מספרים בלבד");
            }
        }

        private void NumOfChildrentextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}