using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        const int KeySize = 64;
        const int Iterations = 350000;
        private Data d = new Data();

        public Login()
        {
            InitializeComponent();
        }

        private void UserLogin(object sender, RoutedEventArgs e)
        {
            string mail = LoginMail.Text;
            string password = LoginPassword.Password;
            UserAccount LoginUser = d.account.FirstOrDefault(m => m.Email == mail);
            if (LoginUser == null)
            {
                MessageBox.Show($"No account found with email {mail}. Try again or make a new account", "Login failed");
            }
            else
            {
                string storedhash = LoginUser.Password;
                bool hashcheck = d.VerifyPassword(password, storedhash);
                if (hashcheck)
                {
                    MessageBox.Show("You are logged in", "Successfull login", MessageBoxButton.OK, MessageBoxImage.Information);
                    int isadmin = LoginUser.Isadmin;
                    AdminCheck.IsAdmin = isadmin;
                    LogCheck.IsLogged = "true";
                    AccountID.AI = LoginUser.AccountID;
                    // ((MainWindow)Application.Current.MainWindow).RefreshRecs();

                    this.Close();
                }
                if (LogCheck.IsLogged == "false")
                {
                    MessageBox.Show("Failed login attempt. Try again or make a new account", "Failed login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
