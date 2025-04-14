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
    /// Interaction logic for ReleasePage.xaml
    /// </summary>
    public partial class ReleasePage : Window
    {
        public ReleasePage()
        {
            InitializeComponent();
            if (AdminCheck.IsAdmin == 1) 
            {
                DeleteRelease.Visibility = Visibility.Visible;
                EditRelease.Visibility = Visibility.Visible;
            }
        }

        private void RP_Home(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowHome();
            this.Close();
        }

        private void RP_Releases(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowReleaseView();
            this.Close();
        }

        private void RP_Recommended(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowRecommended();
            this.Close();
        }

        private void RP_Account(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowAccount();
            if (LogCheck.IsLogged == "true") { this.Close(); }
        }

        public void ReleaseEdit(object sender, RoutedEventArgs e)
        {
            Navigation n = new Navigation();
            n.ShowReleaseManager();
        }
    }
}
