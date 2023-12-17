using Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Shapes;
using System.Windows.Interop;

namespace UIVersion03
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        IBus _bus = null;
        string servername = ".\\SQLEXPRESS";
        string databasename = "MyShopDB";
        public LoginWindow(IBus bus)
        {
            InitializeComponent();
            _bus = bus;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnLogin_ClickAsync(object sender, RoutedEventArgs e)
        {

            string username = txtUser.Text;
            string password = txtPass.Password;
            servername = txtServer.Text;
            databasename = txtDatabase.Text;
            
            loading.Visibility = Visibility.Visible;
            loading.IsIndeterminate = true;

            bool resultLogin = await _bus.Login(username, password, servername, databasename);

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
                    config.AppSettings.Settings["Server"].Value = servername;
                    config.AppSettings.Settings["Database"].Value = databasename;

                    config.Save(ConfigurationSaveMode.Minimal);

                    ConfigurationManager.RefreshSection("appSettings");
                }

                Window window = new MainWindow(_bus);
                window.Show();
                this.Close();
            }
            else
            {
                LoginWarning.Visibility = Visibility.Visible;
            }
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper wih = new WindowInteropHelper(this);
            SendMessage(wih.Handle, 161, 2, 0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {   
            var passwordIn64 = ConfigurationManager.AppSettings["Password"];
            var server = ConfigurationManager.AppSettings["Server"];
            var db = ConfigurationManager.AppSettings["Database"];
            servername= server;
            databasename = db;
            if (server.Length != 0)
            {
                txtServer.Text = server;
            }
            if (db.Length != 0)
            {
                txtDatabase.Text = db;
            }
            if (passwordIn64.Length != 0)
            {
                var entropyIn64 = ConfigurationManager.AppSettings["Entropy"];

                var cyperTextInBytes = Convert.FromBase64String(passwordIn64);
                var entropyInBytes = Convert.FromBase64String(entropyIn64);

                var passwordInBytes = ProtectedData.Unprotect(cyperTextInBytes, entropyInBytes,
                    DataProtectionScope.CurrentUser);
                var password = Encoding.UTF8.GetString(passwordInBytes);
                txtPass.Password = password;

                txtUser.Text = ConfigurationManager.AppSettings["Username"];
            }
        }
    }
}
