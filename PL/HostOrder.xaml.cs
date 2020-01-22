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

namespace PL
{
    /// <summary>
    /// Interaction logic for HostOrder.xaml
    /// </summary>
    public partial class HostOrder : Window
    {
        public HostOrder()
        {
            InitializeComponent();
            for (int i = 0; i < 10; ++i)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = "Item " + i;
                hostListBox.Items.Add(newItem);
            }
        }

        private void hostListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
