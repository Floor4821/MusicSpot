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

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string PFP = "NULL";
        public MainWindow()
        {
            InitializeComponent();
            Data dt = new Data();
            List<release> releases = dt.GetAllReleases();
            NewestReleases.ItemsSource = releases;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

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
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void NewestReleases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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