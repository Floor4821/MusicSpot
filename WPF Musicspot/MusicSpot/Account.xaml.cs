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
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        public Account()
        {
            InitializeComponent();
            Data dt = new Data();
            List<release> releases = dt.GetAllReleases();
            ShoppingCart.ItemsSource = releases;
        }
        public void A_Releases(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowReleaseView();
            this.Close();
        }
        private void A_Home(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowHome();
            this.Close();
        }
        public void Logout(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Fallout = MessageBox.Show("Are you sure you want to log out?", "Confirm logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (Fallout)
            {
                case MessageBoxResult.Yes:
                    LogCheck.IsLogged = "false";
                    Navigation n = new Navigation();
                    n.ShowHome();
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        public void ConfirmTransaction(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowTrans();
        }

        private void A_Recommended(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowRecommended();
            this.Close();
        }
    }
}
