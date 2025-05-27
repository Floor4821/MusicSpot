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
            if (accountID == 0)
            {
                MessageBox.Show("Recommendationlist is not accessible; please log in to your account to view your recommendations", "No account permissions");
            }
            else
            {
                Data d = new Data();
                List<Likedlist> UserLikelist = d.likedlist.Where(x => x.AccountID == accountID).ToList();
                List<string> genres = new List<string>();
                int uniquecount = 0;
                foreach(Likedlist Like in UserLikelist)
                {
                    release Likegenre = d.release.FirstOrDefault(x => x.ReleaseID == Like.ReleaseID);
                    GenreObject thegenre = d.genreobject.FirstOrDefault(y => y.GenreID == Likegenre.GenreID);
                    if (!genres.Contains(thegenre.Genre.ToString()))
                    {
                        genres.Add(thegenre.Genre.ToString());
                        uniquecount += 1;
                    }
                }
                if (uniquecount <= 1)
                {
                    MessageBox.Show("You have not liked any releases yet or not enough to make accurate recommendations. Thus no recommendations can be shown", "Unable to load recommendationlist", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    List<release> recommended = r.SendRecommended(accountID);
                    
                    Recommended.ItemsSource = recommended;
                    
                }
            }
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
            var currentWindow = this;

            release selectedrelease = (release)item;

            ReleasePage RP = new ReleasePage(selectedrelease, previousWindow: currentWindow, "rec");

            RP.Show();
            this.Hide();
        }
    }
}
