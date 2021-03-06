﻿using System;
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
using System.Xml.Linq;
using System.Xml.XmlConfiguration;
using System.Xml;
using BE;
using BL;


namespace PL
{
    /// <summary>
    /// Interaction logic for HostingUnitWindows.xaml
    /// </summary>
    public partial class HostingUnitWindows : Window
    {
        IBL bL; 
        public HostingUnitWindows()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bL = Factory_BL.getBL();
            Host host = new Host();
            
            if (bL.IsHostExists(email.Text))
            {
                if (bL.CheckPass(email.Text, pword.Text))
                {
                    this.Close();
                    HostInterface hostInterface = new HostInterface(email.Text);
                    hostInterface.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"The combination of email address and password you entered does not match.","ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (email.Text == "" || pword.Text == "" || First_name.Text == "" || Last_Name.Text == "" || Phone_number.Text == "" || BankAccountNumber.Text == "")
                    MessageBox.Show($"Please fill all the fields!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    bL.SetHostKey(host);
                    host.MailAddress = email.Text;
                    host.Password = pword.Text;
                    host.FirstName = First_name.Text;
                    host.LastName = Last_Name.Text;
                    host.PhoneNumber = Phone_number.Text;
                    host.BankAccountNumber = int.Parse(BankAccountNumber.Text);
                    if (CollectionClearanceCheckBox.IsChecked == true)
                    {
                        login_button.IsEnabled = false;
                        bL.AddHost(host);
                        this.Close();
                        new HostInterface(email.Text).ShowDialog();
                    }
                    else
                        MessageBox.Show("please accept collections clearance!");
                }
            }
        }
    }
}
