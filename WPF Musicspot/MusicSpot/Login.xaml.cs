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
using MusicSpot;

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void UserLogin(object sender, RoutedEventArgs e)
        {
            string mail = LoginMail.Text;
            string password = LoginPassword.Password;
            Data d = new Data();
            List<UserAccount> allaccount = d.GetAllUsers();
            foreach (UserAccount a in allaccount)
            {
                if (a.Email == mail && a.Password == password)
                {
                    MessageBox.Show("You are logged in", "Successfull login", MessageBoxButton.OK, MessageBoxImage.Information);
                    int isadmin = a.Isadmin;
                    AdminCheck.IsAdmin = isadmin;
                    LogCheck.IsLogged = "true";
                    AccountID.AI = a.AccountID;
                    return;
                }
            }
            if (LogCheck.IsLogged == "false")
            {
                MessageBox.Show("Failed login attempt. Try again or make a new account", "Failed login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
