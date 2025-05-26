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
using System.Security.Cryptography;
using System.Net;
using System.Linq.Expressions;

namespace MusicSpot
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        const int KeySize = 64;
        const int Iterations = 350000;
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
        public void CreateAccount(object sender, RoutedEventArgs e)
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
            UserAccount MailCheck = data.account.FirstOrDefault(x => x.Email == mail);
            if (MailCheck is null)
            {
                string[] passcheck = data.PasswordCheck(password);
                if (passcheck[0] == "Correct")
                {
                    try
                    {
                        //bool sendemail = SendEmailUsingOutlook(mail, password, name);

                        hashedpassword = data.HashPassword(password);
                        UserAccount UA = new UserAccount(name, mail, hashedpassword, 0, pfp);

                        data.account.Add(UA);
                        data.SaveChanges();

                        MessageBox.Show("Account has been successfully created", "Success", MessageBoxButton.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Something went wrong; please try again");
                    }
                }
                else
                {
                    MessageBox.Show(passcheck[0], "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Email address is already taken", "Invalid Email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        /*public bool SendEmailUsingOutlook(string useremail, string password, string username)
        {
            string to = useremail;
            string from = "jamey.verlinden@outlook.com";
            string OutlookPass = "kZM1LF=_Oy~n#hP8PA=1}?e~I~m,oH;a#V,p[5(iU";

            MailMessage message = new MailMessage(from, to)
            {
                Subject = "Account successfully made in Musicspot",
                Body = "Your account has been successfully been added to Musicspot.\n" +
                $"Your username: {username}\n" +
                $"Your password: {password}"
            };

            SmtpClient client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(from, OutlookPass)
            };

            try
            {
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }*/
    }
}
