using Microsoft.EntityFrameworkCore.Metadata;
using MusicSpot.Classes;
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

        public void RemoveFromLikelist(object sender, RoutedEventArgs e)
        {
            Data d = new Data();
            int acID = AccountID.AI;
            var selectedlikes = Likedlist.SelectedItems;
            if (selectedlikes.Count == 0)
            {
                MessageBox.Show("Nothing was selected", "Insufficient requirements");
            }
            else
            {
                foreach (var stuff in selectedlikes)
                {
                    release r = stuff as release; int relID = r.ReleaseID;

                    Likedlist LL = new Likedlist(acID, r.ReleaseID);

                    Likedlist RemoveThisLL = d.likedlist.FirstOrDefault(x => x.ReleaseID == LL.ReleaseID && x.AccountID == LL.AccountID);

                    d.likedlist.Remove(RemoveThisLL);
                    d.SaveChanges();

                }
                MessageBox.Show("Selection has been deleted from your account", "Deletion successfull");
            }
        }

        private void RemoveFromWishlist(object sender, RoutedEventArgs e)
        {
            Data d = new Data();
            int acID = AccountID.AI;
            var selectedlikes = Wishlist.SelectedItems;
            if (selectedlikes.Count == 0)
            {
                MessageBox.Show("Nothing was selected", "Insufficient requirements");
            }
            else
            {
                foreach (var stuff in selectedlikes)
                {
                    Product P = stuff as Product;

                    Wishlist WL = new Wishlist(acID, P.ProductID);

                    Wishlist RemoveThisWL = d.wishlist.FirstOrDefault(x => x.ProductID == WL.ProductID && x.AccountID == acID);

                    d.wishlist.Remove(RemoveThisWL);
                    d.SaveChanges();

                }
                MessageBox.Show("Selection has been deleted from your account", "Deletion successfull");
            }
        }

        private void RemoveFromTransactions(object sender, RoutedEventArgs e)
        {
            /*Data d = new Data();
            int acID = AccountID.AI;
            var selectedlikes = ShoppingCart.SelectedItems;
            if (selectedlikes.Count == 0)
            {
                MessageBox.Show("Nothing was selected", "Insufficient requirements");
            }
            else
            {
                foreach (var stuff in selectedlikes)
                {
                    MessageBox.Show(stuff.ToString());
                    Product P = stuff as Product;

                    Purchase WL = new Purchase(acID, P.ProductID);

                    Wishlist RemoveThisWL = d.wishlist.FirstOrDefault(x => x.ProductID == WL.ProductID && x.AccountID == acID);

                    d.wishlist.Remove(RemoveThisWL);
                    d.SaveChanges();

                }
                MessageBox.Show("Selection has been deleted from your account", "Deletion successfull");
            }*/
        }

        public void ChageAccountSettings(object sender, RoutedEventArgs e)
        {
            ChangeAccount CA = new ChangeAccount(accountID, "Hi_Anthony_;)");
            CA.Show();
        }
    }
}
