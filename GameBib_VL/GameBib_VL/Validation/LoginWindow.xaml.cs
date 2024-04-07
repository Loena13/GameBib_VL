using GameBib_VL.Data;
using GameBib_VL.Model;
using GameBib_VL.Games;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GameBib_VL.Validation
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            this.InitializeComponent();
        }
        private async void BLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUserenname.Text;
            string password = pbPassword.Password;

            User user;
            using (var dbContext = new AppDbContext())
            {
                user = dbContext.Users.FirstOrDefault(u => u.UserName == username);
            }

            if (user != null)
            {
                User.LoggedInUser = user;

                bool isValidUser = SecureHasher.Verify(password, user.HashedPassWord);
                if (isValidUser)
                {
                    GameWindow GameWindow = new GameWindow();
                    GameWindow.LoggedInUser = user;
                    GameWindow.Activate();
                    this.Close();
                }
                else
                {
                    await cdLoginDialog.ShowAsync();
                }
            }
            else
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "User not found",
                    CloseButtonText = "Close",
                    Content = new StackPanel
                    {
                        Children =
                        {
                            new TextBlock { Text = "user not found. Check if the username is correct." }
                        }
                    }
                };
                await dialog.ShowAsync();
            }
        }
        public User GetUserByUsername(string username)
        {
            using (var dbContext = new AppDbContext())
            {
                return dbContext.Users.FirstOrDefault(u => u.UserName == username);
            }
        }
    }
}
