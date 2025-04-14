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
using System.Windows.Shapes;

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for AccountManager.xaml
    /// </summary>
    public partial class AccountManager : Window
    {
        public AccountManager()
        {
            InitializeComponent();
            Data D = new Data();
            List<account> AllAccount = D.GetAllUsers();
            ProfileList.ItemsSource = AllAccount;
        }

        private void AM_Account(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowAccount();
        }

        private void AM_Recommended(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowRecommended();
        }

        private void AM_Releases(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowReleaseView();
        }

        private void AM_Home(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowHome();
        }
        public void EditProfile(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowAccount();
        }
    }
}
