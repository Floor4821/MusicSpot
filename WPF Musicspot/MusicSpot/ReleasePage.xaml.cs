using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for ReleasePage.xaml
    /// </summary>
    public partial class ReleasePage : Window
    {
        public int CurrentReleaseID = 0;
        public ReleasePage(release r)
        {
            InitializeComponent();
            CurrentReleaseID = r.ReleaseID;
            if (AdminCheck.IsAdmin == 1) 
            {
                DeleteRelease.Visibility = Visibility.Visible;
                EditRelease.Visibility = Visibility.Visible;
            }
            ReleaseName.Content = "Name: " + r.ReleaseName.ToString();
            Artist.Content ="Artist: " + r.Artist.ToString();
            Data d = new Data();
            Genre.Content = $"Genre: {r.GenreString}";

            int ReleaseID = r.ReleaseID;
            List<Song> songs = d.songlist(ReleaseID);

            SongList.ItemsSource = songs;
            ReleasepageCover.Source = r.CoverImage;

            List<Product> products = d.GetProductsByID(r.ReleaseID);

            ProductList.ItemsSource = products;
        }

        private void RP_Home(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowHome();
            this.Close();
        }

        private void RP_Releases(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowReleaseView();
            this.Close();
        }

        private void RP_Recommended(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowRecommended();
            this.Close();
        }

        private void RP_Account(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowAccount();
            if (LogCheck.IsLogged == "true") { this.Close(); }
        }

        public void ReleaseEdit(object sender, RoutedEventArgs e)
        {
            ReleaseManager RM = new ReleaseManager(CurrentReleaseID);
            RM.Show();
            this.Close();
        }
        public void ReleaseDestroy(object sender, RoutedEventArgs e)
        {
            string RN = ReleaseName.Content.ToString();
            string CorrectName = (string)RN.Split(": ")[1];

            Data d = new Data();
            d.ReleaseDel(CorrectName);
        }
        public void AddProduct(object sender, RoutedEventArgs e)
        {
            Product item = (Product)ProductList.SelectedItem;
            if (item != null)
            {
                Data d = new Data();
                int productID = item.ProductID;
                d.AddProductToCart(productID);
                MessageBox.Show(item.MediaString.ToString());
            }
            else
            {
                MessageBox.Show("Select the product before adding it", "Selection error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddToWishlist(object sender, RoutedEventArgs e)
        {
            Product item = (Product)ProductList.SelectedItem;
            if(item == null)
            {
                MessageBox.Show("No item selected", "Selection error");
            }
            else
            {
                Data d = new Data();
                int productID = item.ProductID;
                d.AddToWishlist(productID);

            }
        }
        public void LikeRelease(object sender, RoutedEventArgs e)
        {
            Data d = new Data();
            int releaseID = CurrentReleaseID;
            d.AddToLikeList(releaseID);

        }
        public void GoBackToReleasePage(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowReleaseView();
            this.Close();
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
