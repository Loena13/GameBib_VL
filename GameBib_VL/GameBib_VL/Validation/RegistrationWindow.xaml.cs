using GameBib_VL.Data;
using GameBib_VL.Model;
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
    public sealed partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            this.InitializeComponent();
        }

        private void BRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUserenname.Text;
            string password = pbPassword.Password;
            string hashedPassword = SecureHasher.Hash(password);

            User user = new User
            {
                UserName = username,
                HashedPassWord = hashedPassword,
            };

            using (var dbContext = new AppDbContext())
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }

            var loginwindow = new LoginWindow();
            loginwindow.Activate();
            this.Close();
        }
    }
}
