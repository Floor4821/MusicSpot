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
        public int accountID = AccountID.AI;
        public Account()
        {
            InitializeComponent();
            Data dt = new Data();

            int accountid = AccountID.AI;

            Dictionary<string, string> userinfo = dt.PEN(accountid);

            AccountName.Content = "USERNAME: " + userinfo["Username"];
            AccountPass.Content = "PASSWORD: " + userinfo["Password"];
            AccountMail.Content = "EMAIL: " + userinfo["Email"];

            List<Product> wishlist = dt.GetWishlist(accountid);
            Wishlist.ItemsSource = wishlist;

            List<release> likedlist = dt.GetLikedList(accountid);
            Likedlist.ItemsSource = likedlist;

            List<Product> shoppingCart = dt.GetShoppingCart(accountid);
            ShoppingCart.ItemsSource = shoppingCart;

            BitmapImage BMI = dt.pfp();
            profilepicture.Source = BMI;

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
            Data d = new Data();
            double paid = d.ConfirmTransaction(accountID);
            MessageBox.Show($"Transaction succesfully processed: You paid €{paid}");
            ((Account)Application.Current.MainWindow.Content).RefreshShoppingCart();
        }
        public void RefreshShoppingCart()
        {
            Data d = new Data();
            List<Product> shoppingCart = d.GetShoppingCart(accountID);
            ShoppingCart.ItemsSource = shoppingCart;
        }

        private void A_Recommended(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowRecommended();
            this.Close();
        }

        private void A_Account(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowAccount();
            this.Close();
        }

        private void Wishlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
