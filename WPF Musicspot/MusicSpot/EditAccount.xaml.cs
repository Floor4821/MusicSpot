using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for EditAccount.xaml
    /// </summary>
    public partial class EditAccount : Page
    {
        public int Permission = 999;
        public byte[] PFP = null;
        public EditAccount()
        {
            InitializeComponent();
        }

        private void AdminPer(object sender, RoutedEventArgs e)
        {

        }

        private void UserPer(object sender, RoutedEventArgs e)
        {

        }
        public void AddPicture(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                MessageBox.Show(selectedFileName);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                PFP = File.ReadAllBytes(selectedFileName);
            }
            MessageBox.Show(PFP.ToString());
        }
    }
}
