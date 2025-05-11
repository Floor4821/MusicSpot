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

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for ReleaseView.xaml
    /// </summary>
    public partial class ReleaseView : Window
    {
        //public bool KeepAlive { get; set; }
        public ReleaseView()
        {
            InitializeComponent();
            Data d = new Data();
            List<release> allreleases = d.GetAllReleases();
            List<Genretype> GenreTypes = d.GetGenretype();
            GenreSelection.ItemsSource = GenreTypes;
            ReleaseList.ItemsSource = allreleases;
            if (AdminCheck.IsAdmin == 1)
            {
                InsertRelease.Visibility = Visibility.Visible;
            }
            //this.KeepAlive = true;
        }

        public void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is release R)
            {
                GenreSelection.SelectedItems.Add(R);
            }
        }
        public void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is release R)
            {
                GenreSelection.SelectedItems.Remove(R);
            }
        }
        private void RV_Home(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowHome();
            this.Close();
        }

        private void R_Account(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowAccount();
            if (LogCheck.IsLogged == "true") { this.Close(); }
        }
        public void Show_ReleasePage(object sender, RoutedEventArgs e)
        {
            var item = ReleaseList.SelectedItem;

            release selectedrelease = (release)item;

            ReleasePage RP = new ReleasePage(selectedrelease);
            RP.Show();
            this.Close();
        }

        private void RV_Recommended(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowRecommended();
            this.Close();
        }
        public void InsertNewRelease(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowReleaseManager();
        }

        private void SearchReleaseName(object sender, RoutedEventArgs e)
        {
            using (var d = new Data())
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
                    if (GenreSelection.SelectedItems.Count != 0)
                    {
                        if (SelectedGenreStrings.Contains(r.GenreString.ToLower()) && regex.IsMatch(r.ReleaseName.ToLower()))
                        {
                            ReleaseFilter.Add(r);
                        }
                    }
                    else
                    {
                        if (regex.IsMatch(r.ReleaseName.ToLower()))
                        {
                            ReleaseFilter.Add(r);
                        }
                    }
                }
                ReleaseList.ItemsSource = ReleaseFilter;

            }
        }
    }
}
