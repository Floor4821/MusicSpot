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
        public MainWindow()
        {
            InitializeComponent();
            Data dt = new Data();
            Recommended r = new Recommended();
            List<release> releases = dt.release.OrderByDescending(r => r.Releasedate).Take(10).ToList();
            NewestReleases.ItemsSource = releases;
            if (LogCheck.IsLogged != "false")
            {
                int accountID = AccountID.AI;
                List<release> recommended = r.SendRecommended(accountID).Take(10).ToList();
                Recommended.ItemsSource = recommended;
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
            Data d = new Data();
            Wishlist wl = new Wishlist(1, 266);
            d.wishlist.Add(wl);
            d.SaveChanges();
        }

        private void Recommended_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}