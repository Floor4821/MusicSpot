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
        private Data d = new Data();
        public Account()
        {
            InitializeComponent();

            int accountid = AccountID.AI;

            Dictionary<string, string> userinfo = d.PEN(accountid);
            double toPay = d.ToPay(accountid);

            AccountName.Content = "USERNAME: " + userinfo["Username"];
            AccountMail.Content = "EMAIL: " + userinfo["Email"];
            ToPay.Content = $"To Pay: €{toPay}";

            List<Product> wishlist = d.GetWishlist(accountid);
            Wishlist.ItemsSource = wishlist;

            List<release> likedlist = d.GetLikedList(accountid);
            Likedlist.ItemsSource = likedlist;

            List<Product> shoppingCart = d.GetShoppingCart(accountid);
            ShoppingCart.ItemsSource = shoppingCart;

            BitmapImage BMI = d.pfp();
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
            double paid = d.ConfirmTransaction(accountID);
            MessageBox.Show($"Transaction succesfully processed: You paid €{paid}");
        }
        public void RefreshShoppingCart()
        {
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

        public void RemoveFromTransactions(object sender, RoutedEventArgs e)
        {

            int accountID = AccountID.AI;
            var cartSelection = ShoppingCart.SelectedItems;
            if (cartSelection.Count == 0)
            {
                MessageBox.Show("Nothing was selected", "Insufficient requirements");
            }
            else
            {
                foreach (var p in cartSelection)
                {
                    MessageBox.Show(p.ToString());
                    Product product = p as Product;
                    int productID = product.ProductID;
                    

                    using (Data d = new Data())
                    {
                        d.RemoveFromCart(accountID, productID);
                    }

                }
                MessageBox.Show("Selection has been deleted from your account", "Deletion successfull");
            }
        }

        public void ChageAccountSettings(object sender, RoutedEventArgs e)
        {
            ChangeAccount CA = new ChangeAccount(accountID, "Hi_Anthony_;)");
            CA.Show();
        }

        private void Likedlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
