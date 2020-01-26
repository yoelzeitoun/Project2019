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
using BE;
using BL;

namespace PL
{
    /// <summary>
    /// Interaction logic for GuestRequestOrderUserControl.xaml
    /// </summary>
    public partial class GuestRequestOrderUserControl : UserControl
    {
        public HostingUnit CurrentHostingUnit { get; set; }
        public GuestRequest CurrentGuestRequest { get; set; }
        Image MyImage;
        public IBL bl;
        public GuestRequestOrderUserControl(HostingUnit hostUnit, GuestRequest guest)
        {
            InitializeComponent();
            this.CurrentHostingUnit = hostUnit;
            this.CurrentGuestRequest = guest;
            UserDataContext userDataContext = new UserDataContext();
            userDataContext.hostUnit = hostUnit;
            userDataContext.guest = guest;
            UserControlGrid.DataContext = userDataContext;
            Grid.SetColumn(vbImage, 1);
            Grid.SetRow(vbImage, 0);
            bl = Factory_BL.getBL();
            MyImage = CreateViewImage();
            vbImage.Child = null;
            vbImage.Child = MyImage;
            numOfGuestsTB.Text = guest.TotalNumPersons.ToString();
            totalPriceTB.Text = CalculateTotalPrice(hostUnit, guest);
            entryDateTB.Text = guest.EntryDate.ToString("dd/MM/yyyy");
            releaseDateTB.Text = guest.ReleaseDate.ToString("dd/MM/yyyy");
            totalNumberOfDaysTB.Text = bl.Time_Span(guest.EntryDate, guest.ReleaseDate).ToString();
        }
        public string CalculateTotalPrice (HostingUnit hostUnit, GuestRequest guest)
        {
            int total = (hostUnit.PriceForAdult * guest.NumAdults + hostUnit.PriceForChild * guest.NumChildren)* bl.Time_Span(guest.EntryDate, guest.ReleaseDate);
            return total.ToString();
        }
        public class UserDataContext
        {
            public HostingUnit hostUnit { get; set; }
            public GuestRequest guest { get; set; }
        }
        private Image CreateViewImage()
        {
            Image dynamicImage = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(CurrentHostingUnit.Pictures[0]);
            bitmap.EndInit();
            dynamicImage.Source = bitmap;
            return dynamicImage;
        }
        private void approveBtn_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            bl.SetOrderKey(order);
            order.HostingUnitKey = CurrentHostingUnit.HostingUnitKey;
            order.GuestRequestKey = CurrentGuestRequest.GuestRequestKey;
            order.status_Order = Status_order.Email_sent;
            order.OrderDate = DateTime.Now;
            order.CreateDate = DateTime.Now;
            bl.AddOrder(order);
            bl.DiaryChangeToOccuped(CurrentHostingUnit, CurrentGuestRequest);
            bl.UpdateHostingUnit(CurrentHostingUnit);

            MessageBox.Show($"An email has just been sent!", "OK!", MessageBoxButton.OK, MessageBoxImage.Information);
            Window.GetWindow(this).Close();
        }

        private void rejectBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
