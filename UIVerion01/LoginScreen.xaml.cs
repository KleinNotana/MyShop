using Contract;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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

namespace UIVersion01
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class LoginScreen : UserControl
    {
        IBus _bus = null;
        Window parentWindow;
        public LoginScreen(IBus bus)
        {
            InitializeComponent();
            _bus = bus;
            parentWindow = Window.GetWindow(this);
            this.Loaded += (s, e) => SetWindowProperties();
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnLogin_ClickAsync(object sender, RoutedEventArgs e)
        {

            string username = txtUser.Text;
            string password = txtPass.Password;

            loading.Visibility = Visibility.Visible;
            loading.IsIndeterminate = true;

            bool resultLogin = _bus.Login(username, password);

            loading.IsIndeterminate = false;
            loading.Visibility = Visibility.Collapsed;
            if (resultLogin)
            {
                if (rememberMe.IsChecked == true)
                {
                    var passwordInBytes = Encoding.UTF8.GetBytes(password);
                    var entropy = new byte[20];
                    using (var rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(entropy);
                    }
                    var cypherText = ProtectedData.Protect(passwordInBytes, entropy,
                        DataProtectionScope.CurrentUser);
                    var passwordIn64 = Convert.ToBase64String(cypherText);
                    var entropyIn64 = Convert.ToBase64String(entropy);

                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["Username"].Value = username;
                    config.AppSettings.Settings["Password"].Value = passwordIn64;
                    config.AppSettings.Settings["Entropy"].Value = entropyIn64;
                    config.Save(ConfigurationSaveMode.Minimal);

                    ConfigurationManager.RefreshSection("appSettings");
                }

                parentWindow.Content = new MainScreen(_bus);
            }
            else
            {
                LoginWarning.Visibility = Visibility.Visible;
            }
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.WindowState = WindowState.Minimized;
        }

        public void SetWindowProperties()
        {
            if (parentWindow != null)
            {
                parentWindow.WindowStyle = WindowStyle.None;
                parentWindow.ResizeMode = ResizeMode.NoResize;
                parentWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                parentWindow.AllowsTransparency = true;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var username = config.AppSettings.Settings["Username"].Value;
            var passwordIn64 = config.AppSettings.Settings["Password"].Value;
            var entropyIn64 = config.AppSettings.Settings["Entropy"].Value;
            //var screen = new MainWindow();
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(passwordIn64) && !string.IsNullOrEmpty(entropyIn64))
            {
                var passwordInBytes = Convert.FromBase64String(passwordIn64);
                var entropy = Convert.FromBase64String(entropyIn64);
                var password = Encoding.UTF8.GetString(ProtectedData.Unprotect(passwordInBytes, entropy,
                                       DataProtectionScope.CurrentUser));
                txtUser.Text = username;
                txtPass.Password = password;
                rememberMe.IsChecked = true;
                //btnLogin_ClickAsync(sender, e);
            }
        }
    }
}
