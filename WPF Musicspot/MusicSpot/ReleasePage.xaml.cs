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
        public int CurrentAccountID = AccountID.AI;
        public string OriginPage = "";
        public Window PreviousWindow;
        private Data d = new Data();

        public ReleasePage(release r, Window previousWindow, string originpage = "rel")
        {
            InitializeComponent();
            PreviousWindow = previousWindow;
            CurrentReleaseID = r.ReleaseID;
            if (AdminCheck.IsAdmin == 1) 
            {
                DeleteRelease.Visibility = Visibility.Visible;
                EditRelease.Visibility = Visibility.Visible;
            }
            ReleaseName.Content = "Name: " + r.ReleaseName.ToString();
            Artist.Content ="Artist: " + r.Artist.ToString();
            Genre.Content = $"Genre: {r.GenreString}";
            Subgenre.Content = $"Subgenre: {r.SubgenreString}";
            ReleaseDate.Content = $"Release Date: {r.Releasedate.ToString("yyyy-MM-dd")}";

            int ReleaseID = r.ReleaseID;
            List<Song> songs = d.songlist(ReleaseID);

            SongList.ItemsSource = songs;
            ReleasepageCover.Source = r.CoverImage;

            List<Product> products = d.GetProductsByID(r.ReleaseID);

            ProductList.ItemsSource = products;
            OriginPage = originpage;
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

            d.ReleaseDel(CorrectName);
        }
        public void AddProduct(object sender, RoutedEventArgs e)
        {
            Product item = (Product)ProductList.SelectedItem;
            if (item != null)
            {
                int productID = item.ProductID;
                d.AddProductToCart(CurrentAccountID, productID);
                MessageBox.Show("Item has been added to your shoppingcart");
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
                int productID = item.ProductID;
                d.AddToWishlist(CurrentAccountID, productID);
                MessageBox.Show("Item has been added to your wishlist");
            }
        }
        public void LikeRelease(object sender, RoutedEventArgs e)
        {
            int releaseID = CurrentReleaseID;
            d.AddToLikeList(CurrentAccountID, releaseID);
            MessageBox.Show("Item has been added to your likelist");
        }
        public void GoBackToReleasePage(object sender, RoutedEventArgs e)
        {
            PreviousWindow.Show();
            this.Hide();
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
