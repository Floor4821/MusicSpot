using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using MusicSpot;
using MusicSpot.Classes;
using MySqlConnector;

namespace MusicSpot
{
    public static class LogCheck
    {
        public static string IsLogged { get; set; } = "false";
    }
    public static class AdminCheck
    {
        public static int IsAdmin { get; set; } = 0;
    }
    public static class AccountID
    {
        public static int AI { get; set; }
    }
    public static class ListboxIndex
    {
        public static int LBI { get; set; } = 0;
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data d = new Data();
        public MainWindow()
        {
            InitializeComponent();
            Recommended r = new Recommended();
            List<release> releases = d.release.OrderByDescending(r => r.Releasedate).Take(10).ToList();
            NewestReleases.ItemsSource = releases;
            if (LogCheck.IsLogged != "false")
            {
                int accountID = AccountID.AI;
                if (accountID != 0)
                {
                    List<Likedlist> UserLikelist = d.likedlist.Where(x => x.AccountID == accountID).ToList();
                    List<string> genres = new List<string>();
                    int uniquecount = 0;
                    foreach (Likedlist Like in UserLikelist)
                    {
                        release Likegenre = d.release.FirstOrDefault(x => x.ReleaseID == Like.ReleaseID);
                        GenreObject thegenre = d.genreobject.FirstOrDefault(y => y.GenreID == Likegenre.GenreID);
                        if (!genres.Contains(thegenre.Genre.ToString()))
                        {
                            genres.Add(thegenre.Genre.ToString());
                            uniquecount += 1;
                        }
                    }
                    if (uniquecount >= 2)
                    {
                        List<release> recommended = r.SendRecommended(accountID).Take(10).ToList();

                        Recommended.ItemsSource = recommended;

                    }
                }
                //List<release> recommended = r.SendRecommended(accountID).Take(10).ToList();
                //Recommended.ItemsSource = recommended;
            }

            if (AdminCheck.IsAdmin == 1) { AdminLabel.Visibility = Visibility.Visible; }
        }
        public void login(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }
        public void RefreshRecs()
        {
            Recommended r = new Recommended();
            int accountID = AccountID.AI;
            List<release> recommended = r.SendRecommended(accountID).Take(10).ToList();
            Recommended.ItemsSource = recommended;
        }

        public void Register(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
        }

        private void NewestReleases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WM_Recommended(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowRecommended();
            this.Close();
        }

        private void WM_Releases(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowReleaseView();
            this.Close();
        }

        private void WM_Account(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            Data d = new Data();
            n.ShowAccount();
            if (LogCheck.IsLogged == "true") { this.Close(); }
        }

        private void RefreshHome(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowHome();
            this.Close();
        }

        private void InsertWhishlist(object sender, RoutedEventArgs e)
        {
            Wishlist wl = new Wishlist(1, 266);
            d.wishlist.Add(wl);
            d.SaveChanges();
        }

        public void Show_ReleasePage(object sender, RoutedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is release selectedRelease)
            {
                var currentWindow = this;
                int RLindex = listBox.Items.IndexOf(selectedRelease);
                ListboxIndex.LBI = RLindex;

                ReleasePage RP = new ReleasePage(selectedRelease, previousWindow: currentWindow);
                RP.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select a release from the list.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}