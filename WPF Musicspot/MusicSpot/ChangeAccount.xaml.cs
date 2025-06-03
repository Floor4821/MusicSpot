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
    /// Interaction logic for ChangeAccount.xaml
    /// </summary>
    public partial class ChangeAccount : Window
    {
        public int permission = 0;
        public byte[] ImagePFP = null;
        public int ID = 0;
        private Data d = new Data();

        public ChangeAccount(int userid = 0, string permission = "admin")
        {
            InitializeComponent();
            ID = userid;
            if (userid == 0)
            {
                EditorUpdate.Content = "Insert new user";
            }
            else
            {
                EditorUpdate.Content = $"Edit user {ID.ToString()}";
            }
            if(permission == "admin")
            {
                PermissionExpander.Visibility = Visibility.Visible;
            }
            else
            {
                PermissionExpander.Visibility = Visibility.Hidden;
                PermissionLabel.Visibility = Visibility.Hidden;
            }
        }
        public void NewPFP(object sender, MouseButtonEventArgs e)
        {
            byte[] image = d.PFP(false);
            ImagePFP = image;
        }

        public void ConfirmAdmin(object sender, RoutedEventArgs e)
        {
            permission = 1;
            MessageBox.Show("New account has admin privileges");
        }
        public void ConfirmUser(object sender, RoutedEventArgs e)
        {
            permission = 0;
            MessageBox.Show("New account has user privileges");
        }

        private void SubmitNewUser(object sender, RoutedEventArgs e)
        {
            string UN = Username.Text;
            string UM = Email.Text;
            string UP = Password.Password;
            string hashedpass = "";
            bool emailCheck = d.account.Any(a => a.Email == UM);
            if (emailCheck == true)
            {
                MessageBox.Show("This email is already in use");
                return;
            }
            if (ID == 0)
            {
                if (string.IsNullOrWhiteSpace(UN) || string.IsNullOrWhiteSpace(UM) || string.IsNullOrWhiteSpace(UP))
                {
                    MessageBox.Show("Missing requirements: Username, Email and Password may not be empty!", "Missing requirements", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    using (var context = new Data())
                    {
                        hashedpass = context.HashPassword(UP);
                        UserAccount UA = new UserAccount(UN, UM, hashedpass, permission, ImagePFP);
                        context.account.Add(UA);
                        context.SaveChanges();
                    }
                    ImagePFP = null;
                    permission = 0;
                    MessageBox.Show("User has been created/updated", "Succesfull edit", MessageBoxButton.OK);
                }
            }
            else
            {
                using (var context = new Data())
                {
                    var user = context.account.FirstOrDefault(x => x.AccountID == ID);
                    if (!string.IsNullOrWhiteSpace(UN))
                    {
                        user.Name = UN;
                    }
                    if (!string.IsNullOrWhiteSpace(UM))
                    {
                        bool emailcheck = context.account.Any(a => a.Email == UM);
                        if (emailcheck == true)
                        {
                            MessageBox.Show("Email is already taken");
                        }
                        else
                        {
                            user.Email = UM;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(UP))
                    {
                        string Hash = context.HashPassword(UP);
                        user.Password = Hash;
                    }
                    if (ImagePFP != null)
                    {
                        user.Profilepic = ImagePFP;
                    }
                    user.Isadmin = permission; //Beware! If nothing is chosen; this value will always default to 0.
                    context.SaveChanges();
                    MessageBox.Show($"User {ID}, has been successfully updated", "Success!", MessageBoxButton.OK);
                    permission = 0; ImagePFP = null;
                }
            }
        }
    }
}
