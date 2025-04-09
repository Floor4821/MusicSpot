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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32; 
using System.Windows.Media.Imaging; 
using System.IO;
using System.Net.Mail;

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public string ProfilePicture = "NULL";
        public Register()
        {
            InitializeComponent();
        }
        public void AddPfp(object sender, RoutedEventArgs e)
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
                ProfilePicture = bitmap.ToString();
            }
        }
        /*public bool CheckMail(string email)
        {
            try
            {
                var mail = new MailAddress(email);
                using (var client = new SmtpClient(mail.Host))
                {
                    client.Port = 25;
                    client.Send(new MailMessage("jamey.verlinden@gmail.com", mail.ToString(), "Verification Email", "This is a verification email."));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }*/
        private void CreateAccount(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
