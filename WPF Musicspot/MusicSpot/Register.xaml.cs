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
using Microsoft.Win32; 
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        const int KeySize = 64;
        const int Iterations = 350000;
        public Data d = new Data();
        public byte[] ProfilePicture;
  
    public Register()
        {
            InitializeComponent();
            ProfilePicture = d.GetDefaultPfp();
        }
        public void AddPfp(object sender, RoutedEventArgs e)
        {
            byte[] stuff = d.PFP();
            ProfilePicture = stuff;
        }
        private void CreateAccount(object sender, RoutedEventArgs e)
        {
            Data data = new Data();
            string name = RegisterName.Text;
            string mail = RegisterMail.Text;
            string password = RegisterPass.Password;
            string hashedpassword = "";
            byte[] pfp = ProfilePicture;
            if(String.IsNullOrEmpty(name) || String.IsNullOrEmpty(mail) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Missing values. Please ensure all fields are satisfied.", "Failed to create new account", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            string[] passcheck = PasswordCheck(password);
            if (passcheck[0] == "Correct")
            {
                hashedpassword = data.HashPassword(password);
                UserAccount UA = new UserAccount(name, mail, hashedpassword, 0, pfp);
                
                data.account.Add(UA);
                data.SaveChanges();
                
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
