using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicSpot
{
    public class Navigation
    {
        public void ShowAccount()
        {
            if (LogCheck.IsLogged == "true" && AdminCheck.IsAdmin == 1)
            {
                AccountManager AM = new AccountManager();
                AM.Show();
            }
            else if (LogCheck.IsLogged == "true")
            {
                Account A = new Account();
                A.Show();
            }
            else
            {
                MessageBox.Show("In order to access your account, you need to login or create a new account", "Can not access account", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void CloseWindow(Window window)
        {
            window.Close();
        }
        public void ShowHome()
        {
            MainWindow MW = new MainWindow();
            MW.Show();
        }
        public void ShowTrans()
        {
            ConfirmTransaction CT = new ConfirmTransaction();
            CT.Show();
        }
        public void ShowReleaseView()
        {
            ReleaseView RV = new ReleaseView();
            RV.Show();
        }
        public void ShowRecommended()
        {
            RecommendedView RV = new RecommendedView();
            RV.Show();
        }
        public void ShowReleaseManager()
        {
            ReleaseManager RM = new ReleaseManager();
            RM.Show();
        }
        public void ShowAccountManager()
        {
            AccountManager AM = new AccountManager();
            AM.Show();
        }
    }
}
