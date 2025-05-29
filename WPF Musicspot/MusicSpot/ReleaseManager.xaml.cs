using MusicSpot.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ReleaseManager.xaml
    /// </summary>
    public partial class ReleaseManager : Window
    {
        public int Status = 3;
        public int NewReleaseID = 999;
        public byte[] COVER = null;
        public int editrelease = 0;
        private Data d = new Data();
        public ReleaseManager(int EditReleaseID = 0)
        {
            InitializeComponent();

            List<Genretype> allgenres = d.genretype.OrderBy(x => x.Type).ToList();
            List<SubgenreType> allsubgenres = d.subgenretype.OrderBy(y => y.Type).ToList();
                
            GenreSelection.ItemsSource = allgenres;
            SubgenreSelection.ItemsSource = allsubgenres;

            editrelease = EditReleaseID;
        }
        public void AddColumn_Click(object sender, RoutedEventArgs e)
        {
            var newRow = new ListViewItem();

            var textBox = new TextBox
            {
                Width = 360
            };

            newRow.Content = textBox;

            ReleaseSongs.Items.Add(newRow);
        }
        public void Album_Status(object sender, RoutedEventArgs e)
        {
            Status = 1;
        }
        public void EP_Status(object sender, RoutedEventArgs e)
        {
            Status = 2;
        }
        public void Other_Status(object sender, RoutedEventArgs e)
        {
            Status = 3;
        }
        public void InsertRelease(object sender, RoutedEventArgs e)
        {
            if (editrelease == 0)
            {
                var SelectedGenre = (Genretype)GenreSelection.SelectedItem;
                var SelectedSubgenre = (SubgenreType)SubgenreSelection.SelectedItem;
                if (SelectedGenre == null || SelectedSubgenre == null)
                {
                    MessageBox.Show("Please select both a genre and subgenre", "Missing requirements", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string Genre = SelectedGenre.Type;
                    string Subgenre = SelectedSubgenre.Type;
                    string RN = Releasename.Text;
                    string AN = Artistname.Text;
                    bool temp = DateTime.TryParse(ReleaseDate.Text, out DateTime ReleaseTime);
                    int RT = Status;

                    string VP = VinylPrice.Text;
                    string VS = VinlyStock.Text;

                    string CP = CassettePrice.Text;
                    string CS = CassetteStock.Text;

                    string CD_P = CDPrice.Text;
                    string CD_S = CDStock.Text;

                    bool Vinyl1 = double.TryParse(VP, out double v1);
                    bool Vinyl2 = int.TryParse(VS, out int v2);

                    bool Cassette1 = double.TryParse(CP, out double c1);
                    bool Cassette2 = int.TryParse(CS, out int c2);

                    bool CD1 = double.TryParse(CD_P, out double cd1);
                    bool CD2 = int.TryParse(CD_S, out int cd2);

                    if (!Vinyl1 || !Vinyl2 || !Cassette1 || !Cassette2 || !CD1 || !CD2)
                    {
                        MessageBox.Show("The product information contains values that cannot be converted to numeric characters, or the wrong type of numeric character was given", "Invalid arguments", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        string Columninfo = ReleaseSongs.ToString();
                        Regex r = new Regex(@"\d+"); MatchCollection AnyNumbers = r.Matches(Columninfo);
                        int RowNumbers = Convert.ToInt32(AnyNumbers[0].Value);

                        //THE GALLERY OF CONDITIONAL STATEMENTS

                        if (string.IsNullOrWhiteSpace(RN) || string.IsNullOrWhiteSpace(RN) || string.IsNullOrWhiteSpace(AN) || string.IsNullOrWhiteSpace(Genre) || string.IsNullOrWhiteSpace(Subgenre))
                        {
                            MessageBox.Show("Missing parameters, make sure all required parameters are satisfied", "Missing requirements", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (!temp)
                        {
                            MessageBox.Show("Invalid Datetime format for releasedate. Make sure you enter a valid format", "Invalid parameter", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else if (RowNumbers == 0)
                        {
                            MessageBox.Show("A release without songs is just a cover, which is just an image; and we're not an image store", "Missing requirements", MessageBoxButton.OK, MessageBoxImage.Question);
                        }
                        else if (Convert.ToDouble(VP) <= 0 || Convert.ToDouble(CP) <= 0 || Convert.ToDouble(CD_P) <= 0)
                        {
                            MessageBox.Show("Prices can't be negative or zero... Kind of thought that was obvious", "Invalid arguments", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else if (Convert.ToDouble(VS) < 0 || Convert.ToDouble(CS) < 0 || Convert.ToDouble(CD_S) < 0)
                        {
                            MessageBox.Show("Stock can't be negative; not even sure how that would work", "Invalid arguments", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            try
                            {
                                List<string> songs = new List<string>();
                                foreach (var item in ReleaseSongs.Items)
                                {
                                    string songstring = item.ToString().Split(": ")[1];
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Songs with no names are not songs", "Missing requirements", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                return;
                            }
                            int G_ID = 0;
                            GenreObject Exists = d.genreobject.FirstOrDefault(x => x.Genre == SelectedGenre.ID && x.Subgenre == SelectedSubgenre.ID);
                            if (Exists == null)
                            {
                                G_ID = d.InsertGenre(SelectedGenre.ID, SelectedSubgenre.ID);
                            }
                            else
                            {
                                G_ID = Exists.GenreID;
                            }
                            release newrelease = new release(RN, COVER, Status, AN, ReleaseTime, G_ID);
                            d.release.Add(newrelease);

                            d.SaveChanges();

                            int LastID = newrelease.ReleaseID;

                            foreach (var item in ReleaseSongs.Items)
                            {
                                string SongAdd = item.ToString().Split(": ")[1];
                                Song s = new Song(SongAdd, LastID);
                                d.song.Add(s);
                            }

                            d.SaveChanges();

                            Product InsertVinyl = new Product(v1, v2, 1, LastID);
                            Product InsertCD = new Product(cd1, cd2, 2, LastID);
                            Product InsertCassette = new Product(c1, c2, 3, LastID);

                            d.product.Add(InsertVinyl);
                            d.product.Add(InsertCD);
                            d.product.Add(InsertCassette);

                            d.SaveChanges();

                            MessageBox.Show($"Release has been added: \n" +
                                $"ReleaseID: {LastID}\n" +
                                $"Title: {RT}\n" +
                                $"Genre: {Genre}\n" +
                                $"Subgenre: {Subgenre}");

                            /*else if (String.IsNullOrWhiteSpace(VP) || String.IsNullOrWhiteSpace(VS))
                            {
                                MessageBox.Show("Vinyl requirements are missing", "Missing requirements", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (String.IsNullOrWhiteSpace(CP) || String.IsNullOrWhiteSpace(CS))
                            {
                                MessageBox.Show("Cassette requirements are missing", "Missing requirements", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else if (String.IsNullOrWhiteSpace(CD_P) || String.IsNullOrWhiteSpace(CD_S))
                            {
                                MessageBox.Show("CD requirements are missing", "Missing requirements", MessageBoxButton.OK, MessageBoxImage.Error);
                            }*/
                        }
                    }
                }
                
            }
            else
            {

            }
        }
        public void InsertReleaseImage(object sender, RoutedEventArgs e)
        {
            COVER = d.PFP();
        }
    }
}
