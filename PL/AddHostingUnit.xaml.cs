using BE;
using BL;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddHostingUnit.xaml
    /// </summary>
    public partial class AddHostingUnit : Window
    {
        IBL bL;
        Host currentHost;
        public AddHostingUnit()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            areaComboBox.ItemsSource = Enum.GetValues(typeof(Area)).Cast<Area>().Where(i=>i.ToString()!="All");
            HostingUnitTypeComboBox.ItemsSource = Enum.GetValues(typeof(Type)).Cast<Type>().Where(i => i.ToString() != "All");
            bL = Factory_BL.getBL();

        }
        public AddHostingUnit(string email, string hu = null) : this()
        {
            currentHost = bL.GetHost(email);
            if(hu!=null)
            {
                HostingUnit hostingUnit = bL.GetHostingUnit(email, hu);
                hostingUnitNameTextBox.Text = hostingUnit.HostingUnitName;
                areaComboBox.SelectedItem = hostingUnit.area;
                jaccuziBox.IsChecked = hostingUnit.jacuzzi.ToString() == "Yes" ? true : false;
                poolBox.IsChecked = hostingUnit.pool.ToString() == "Yes" ? true : false;
                childrenAttractionBox.IsChecked = hostingUnit.childrenAttractions.ToString() == "Yes" ? true : false;
                gardenBox.IsChecked = hostingUnit.garden.ToString() == "Yes" ? true : false;
                HostingUnitTypeComboBox.SelectedItem = hostingUnit.type;
                NumOfAdultsTextBox.Text = hostingUnit.NumOfAdults.ToString();
                NumOfChildrenTextBox.Text = hostingUnit.NumOfChildren.ToString();
                cityTextBox1.Text = hostingUnit.City;
                streetTextBox1.Text = hostingUnit.Street;
                houseNumberTextBox1.Text=hostingUnit.HouseNumber;
                adultPrice.Text = hostingUnit.PriceForAdult.ToString();
                childPrice.Text = hostingUnit.PriceForChild.ToString();
                for (int i = 0; i < 10; i++)
                {
                    var textBoxName = string.Format("pic{0}", i);
                    var textBox = (TextBox)this.FindName(textBoxName);
                    textBox.Text = hostingUnit.Pictures[i];
                }
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            HostingUnit hostingUnit = new HostingUnit();
            bL.SetHostingUnitKey(hostingUnit);
            hostingUnit.HostingUnitName = hostingUnitNameTextBox.Text;
            hostingUnit.Owner = currentHost;
            hostingUnit.area = (Area)areaComboBox.SelectedItem;
            hostingUnit.jacuzzi = (bool)jaccuziBox.IsChecked ? Jaccuzzi.Yes : Jaccuzzi.No;
            hostingUnit.pool = (bool)poolBox.IsChecked ? Pool.Yes : Pool.No;
            hostingUnit.childrenAttractions = (bool)childrenAttractionBox.IsChecked ? ChildrensAttractions.Yes : ChildrensAttractions.No;
            hostingUnit.garden = (bool)gardenBox.IsChecked ? Garden.Yes : Garden.No;
            hostingUnit.type = (Type)HostingUnitTypeComboBox.SelectedItem;
            hostingUnit.NumOfAdults = int.Parse(NumOfAdultsTextBox.Text);
            hostingUnit.NumOfChildren = int.Parse(NumOfChildrenTextBox.Text);
            hostingUnit.City = cityTextBox1.Text;
            hostingUnit.Street = streetTextBox1.Text;
            hostingUnit.HouseNumber = houseNumberTextBox1.Text;
            hostingUnit.PriceForAdult = int.Parse(adultPrice.Text);
            hostingUnit.PriceForChild = int.Parse(childPrice.Text);
            hostingUnit.Pictures = new string[10];
            for (int i = 0; i < 10; i++) 
            {
                var textBoxName = string.Format("pic{0}", i);
                var textBox = (TextBox)this.FindName(textBoxName);
                hostingUnit.Pictures[i] = textBox.Text;
            }

            bL.AddHostingUnit(hostingUnit);
            MessageBox.Show($"You successfully saved the Hosting Unit!", "OK!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
            new HostInterface(currentHost.MailAddress).ShowDialog();
        }
    }
}
