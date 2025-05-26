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
    /// Interaction logic for RecommendedView.xaml
    /// </summary>
    public partial class RecommendedView : Window
    {
        public RecommendedView()
        {
            InitializeComponent();
            int accountID = AccountID.AI;
            Recommended r = new Recommended();
            List<release> recommended = r.SendRecommended(accountID);
            Recommended.ItemsSource = recommended;
        }

        private void RV_Home(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowHome();
            this.Close();
        }

        private void RV_Releases(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowReleaseView();
            this.Close();
        }

        private void RV_Recommended(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowRecommended();
            this.Close();
        }

        private void RV_Account(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowAccount();
            if (LogCheck.IsLogged == "true") { this.Close(); }
        }

        public void Show_RecommendedPage(object sender, RoutedEventArgs e)
        {
            var item = Recommended.SelectedItem;

            object selecteditem = Recommended.SelectedItem;
            int RLindex = Recommended.Items.IndexOf(selecteditem);
            ListboxIndex.LBI = RLindex;

            release selectedrelease = (release)item;

            ReleasePage RP = new ReleasePage(selectedrelease);

            RP.Show();
            this.Close();
        }
    }
}
