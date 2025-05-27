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
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using MusicSpot.Classes;

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for Manage_User.xaml
    /// </summary>
    
    public partial class Manage_User : Window
    {
        public int CurrentAccount = 0;
        public Manage_User(int accountid)
        {
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", "defaultpfp.png");
            CurrentAccount = accountid;
            InitializeComponent();
            Data d = new Data();
            Dictionary<string, string> pen = d.PEN(accountid);

            AccountName.Content = "Username: " + pen["Username"];
            AccountMail.Content = "Email: " + pen["Email"];
            AccountPass.Content = "Password: " + pen["Password"];

            List<Product> wishlist = d.GetWishlist(accountid);
            UserWishlist.ItemsSource = wishlist;

            List<release> likedlist = d.GetLikedList(accountid);
            UserLikedlist.ItemsSource = likedlist;

            List<Product> shoppingCart = d.GetShoppingCart(accountid);
            UserShoppingCart.ItemsSource = shoppingCart;

            BitmapImage BMI = d.pfp(accountid);
            profilepicture.Source = BMI;
        }

        private void Edit_Profile(object sender, RoutedEventArgs e)
        {
            ChangeAccount CA = new ChangeAccount(CurrentAccount);
            CA.Show();
        }

        private void Delete_Profile(object sender, RoutedEventArgs e)
        {
            Data d = new Data();

            MessageBoxResult MBR =  MessageBox.Show("Are you sure you want to delete this account?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(MBR == MessageBoxResult.Yes)
            {
                d.DeleteAccount(CurrentAccount);
                MessageBox.Show("Account has been terminated", "Deleted", MessageBoxButton.OK);
            }
        }

        private void MU_Home(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowHome();
            this.Close();
        }

        private void MU_Releases(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowReleaseView();
            this.Close();
        }

        private void MU_Recommended(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowRecommended();
            this.Close();
        }

        private void MU_Account(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowAccount();
            this.Close();
        }
    }
}
