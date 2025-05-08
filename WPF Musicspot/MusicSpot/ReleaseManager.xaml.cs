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
    /// Interaction logic for ReleaseManager.xaml
    /// </summary>
    public partial class ReleaseManager : Window
    {
        public int Status = 3;
        public int NewReleaseID = 999;
        public byte[] COVER = null;
        public ReleaseManager()
        {
            InitializeComponent();
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
            using (var d = new Data())
            {
                string Genre = InsertGenre.Text;
                string Subgenre = InsertSubgenre.Text;
                string RN = Releasename.Text;
                string AN = Artistname.Text;
                DateTime ReleaseTime = DateTime.Parse(ReleaseDate.Text);
                int RT = Status;

                if(String.IsNullOrWhiteSpace(Genre) || String.IsNullOrWhiteSpace(Subgenre))
                {
                    MessageBox.Show("Genre or Subgenre is missing!", "Missing requirements");
                }
                else
                {
                    Genretype GenreLookup = d.genretype.FirstOrDefault(x => x.Type.ToLower() == Genre.ToLower());
                    int genreID = GenreLookup.ID;

                    SubgenreType SubgenreLookup = d.subgenretype.FirstOrDefault(y => y.Type.ToLower() == Subgenre.ToLower());
                    int subgenreID = SubgenreLookup.ID;

                    int GID = d.InsertGenre(genreID, subgenreID);

                    if (String.IsNullOrWhiteSpace(RN) || String.IsNullOrWhiteSpace(AN) || ReleaseTime == null || RT == null)
                    {
                        MessageBox.Show("Missing requirements; make sure all parameters are satisfied", "Missing requirements", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        release IRS = new release(RN, COVER, RT, AN, ReleaseTime, GID);
                        d.release.Add(IRS);
                        d.SaveChanges();
                    }
                }
            }
        }
        public void InsertReleaseImage(object sender, RoutedEventArgs e)
        {
            using (var d = new Data()) 
            {
                COVER = d.PFP();
            }
        }
    }
}
