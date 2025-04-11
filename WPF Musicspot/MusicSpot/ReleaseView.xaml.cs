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
    /// Interaction logic for ReleaseView.xaml
    /// </summary>
    public partial class ReleaseView : Window
    {
        public ReleaseView()
        {
            InitializeComponent();
            Data d = new Data();
            List<release> allreleases = d.GetAllReleases();
            GenreSelection.ItemsSource = allreleases;
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
    }
}
