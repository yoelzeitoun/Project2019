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
        Image MyImage;
        public GuestRequestOrderUserControl(HostingUnit hostUnit)
        {
            InitializeComponent();
            this.CurrentHostingUnit = hostUnit;
            UserControlGrid.DataContext = hostUnit;
            Grid.SetColumn(vbImage, 1);
            Grid.SetRow(vbImage, 0);

            MyImage = CreateViewImage();
            vbImage.Child = null;
            vbImage.Child = MyImage;
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

        }

        private void rejectBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
