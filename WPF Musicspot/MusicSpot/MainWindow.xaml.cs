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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Data dt = new Data();
            List<release> releases = dt.GetAllReleases();
            NewestReleases.ItemsSource = releases;
            if (AdminCheck.IsAdmin == 1) { AdminLabel.Visibility = Visibility.Visible; }
        }
        public void login(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
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
        /*
var client = new HttpClient();
var request = new HttpRequestMessage
{
Method = HttpMethod.Get,
RequestUri = new Uri("https://spotify23.p.rapidapi.com/search/?q=Smells%20like%20teen%20spirit&type=multi&offset=0&limit=10&numberOfTopResults=5"),
Headers =
{
{ "x-rapidapi-key", "00f8a3d412mshae13d562b6987c6p169362jsn20c5892a8c91" },
{ "x-rapidapi-host", "spotify23.p.rapidapi.com" },
},
};
using (var response = await client.SendAsync(request))
{
response.EnsureSuccessStatusCode();
var body = await response.Content.ReadAsStringAsync();
JObject jsonResponse = JObject.Parse(body);
var urlcover = jsonResponse["albums"]?["items"]?[0]?["data"]?["coverArt"]?["sources"]?[0]?["url"]?.ToString();

}
*/
    }
}