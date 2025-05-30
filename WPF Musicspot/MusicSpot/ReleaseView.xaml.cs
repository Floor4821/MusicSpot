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
using System.Text.RegularExpressions;
using MusicSpot.Classes;

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for ReleaseView.xaml
    /// </summary>
    public partial class ReleaseView : Window
    {
        public List<int> FilteredReleasesID = null;
        private Data d = new Data();
        //public bool KeepAlive { get; set; }
        public ReleaseView()
        {
            InitializeComponent();
            List<release> allreleases = d.GetAllReleases();
            List<Genretype> GenreTypes = d.GetGenretype();
            GenreSelection.ItemsSource = GenreTypes;
            ReleaseList.ItemsSource = allreleases;
            if (AdminCheck.IsAdmin == 1)
            {
                InsertRelease.Visibility = Visibility.Visible;
            }

            int LastIndex = ListboxIndex.LBI;
            object item = ReleaseList.Items[LastIndex];
            ReleaseList.ScrollIntoView(item);
        }
        private void RV_Home(object sender, RoutedEventArgs e)
        {
            ListboxIndex.LBI = 0;
            Navigation n = new Navigation();
            n.ShowHome();
            this.Close();
        }

        private void R_Account(object sender, RoutedEventArgs e)
        {
            ListboxIndex.LBI = 0;
            Navigation n = new Navigation();
            n.ShowAccount();
            if (LogCheck.IsLogged == "true") { this.Close(); }
        }
        public void Show_ReleasePage(object sender, RoutedEventArgs e)
        {
            if (ReleaseList.SelectedItem == null) return;
            var item = ReleaseList.SelectedItem;
            var currentWindow = this;

            object selecteditem = ReleaseList.SelectedItem;
            int RLindex = ReleaseList.Items.IndexOf(selecteditem);
            ListboxIndex.LBI = RLindex;

            release selectedrelease = (release)item;

            ReleasePage RP = new ReleasePage(selectedrelease, previousWindow: currentWindow);

            RP.Show();
            this.Hide();
        }

        private void RV_Recommended(object sender, RoutedEventArgs e)
        {
            ListboxIndex.LBI = 0;
            Navigation n = new Navigation();
            n.ShowRecommended();
            this.Close();
        }
        public void InsertNewRelease(object sender, RoutedEventArgs e)
        {
            ListboxIndex.LBI = 0;
            Navigation n = new Navigation();
            n.ShowReleaseManager();
        }

        private void SearchReleaseName(object sender, RoutedEventArgs e)
        {
            string name = ReleaseNameInput.Text.ToLower();
                
            var genres = GenreSelection.SelectedItems;
            List<string> SelectedGenreStrings = new List<string>();
            foreach (var g in genres)
            {
                Genretype mag = g as Genretype;
                Genretype Ranoutofnames = d.genretype.FirstOrDefault(x => x.Type == mag.Type);
                string GID = Ranoutofnames.Type; SelectedGenreStrings.Add(GID.ToLower());
            }
                
            List<release> ReleaseFilter = new List<release>();
            List<release> allreleases = d.GetAllReleases();
                
            Regex regex = new Regex(name);
            foreach (release r in allreleases)
            {
                string currentgenre = r.GenreString;
                if (GenreSelection.SelectedItems.Count != 0)
                {
                    if (SelectedGenreStrings.Contains(currentgenre.ToLower()) && regex.IsMatch(r.ReleaseName.ToLower()))
                    {
                        ReleaseFilter.Add(r);
                        //FilteredReleasesID.Add(r.ReleaseID);
                    }
                }
                else if(regex.IsMatch(r.ReleaseName.ToLower()))
                {
                    ReleaseFilter.Add(r);
                    //FilteredReleasesID.Add(r.ReleaseID);
                }
                else
                {
                    //FilteredReleasesID = null;
                }
            }
            ReleaseList.ItemsSource = ReleaseFilter;         
        }
    }
}
