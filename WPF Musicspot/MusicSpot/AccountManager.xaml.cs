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
            List<UserAccount> AllAccount = D.GetAllUsers();
            ProfileList.ItemsSource = AllAccount;
        }

        private void AM_Recommended(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowRecommended();
            this.Close();
        }

        private void AM_Releases(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowReleaseView();
            this.Close();
        }

        private void AM_Home(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowHome();
            this.Close();
        }
        public void AM_Account(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowAccountManager();
        }
        public void EditProfile(object sender, RoutedEventArgs e)
        {
            UserAccount items = (UserAccount)ProfileList.SelectedItem;
            if (items != null)
            {
                int userid = items.AccountID;
                Manage_User a = new Manage_User(userid);
                a.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Commander, please select the item before editing it", "Selection error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CreateNewAccount(object sender, RoutedEventArgs e)
        {
            ChangeAccount CA = new ChangeAccount();
            CA.Show();
        }
    }
}
