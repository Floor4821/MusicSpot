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
using System.Text.RegularExpressions;

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public byte[] ProfilePicture = null;
        public Register()
        {
            InitializeComponent();
        }
        public void AddPfp(object sender, RoutedEventArgs e)
        {
            Data d = new Data();
            byte[] stuff = d.PFP();
            ProfilePicture = stuff;
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
            string name = RegisterName.Text;
            string mail = RegisterMail.Text;
            string password = RegisterPass.Password;
            byte[] pfp = ProfilePicture;
            if(String.IsNullOrEmpty(name) || String.IsNullOrEmpty(mail) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Missing values. Please ensure all fields are satisfied.", "Failed to create new account", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            string[] passcheck = PasswordCheck(password);
            if (passcheck[0] == "Correct")
            {
                UserAccount UA = new UserAccount(name, mail, password, 0, pfp);

                using (var data = new Data())
                {
                    data.account.Add(UA);
                    data.SaveChanges();
                }
                MessageBox.Show("Account has been successfully created", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(passcheck[0], "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public string[] PasswordCheck(string pass)
        {
            Regex DigitCheck = new Regex(@"\d");
            Regex LowercaseCheck = new Regex(@"[a-z]");
            Regex UppercaseCheck = new Regex(@"[A-Z]");
            Regex SpecialCheck = new Regex(@"\W");
            if (pass.Length < 8)
            {
                return ["Password must be at least 8 characters long", "Invalid Length"];
            }
            else if (!LowercaseCheck.IsMatch(pass))
            {
                return ["Password must contain at least 1 lowercase character", "Invalid case"];
            }
            else if (!UppercaseCheck.IsMatch(pass))
            {
                return ["Password must contain at least 1 uppercase character", "Invalid case"];
            }
            else if (!SpecialCheck.IsMatch(pass))
            {
                return ["Password must contain at least 1 special character", "No special characters detected"];
            }
            return ["Correct"];
        }
    }
}
