using DemoAdoNet2;
using log4net;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Numerics;
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
using System.Windows.Shapes;

namespace MyShop
{

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private static readonly ILog Log = LogManager.GetLogger(
MethodBase.GetCurrentMethod().DeclaringType);

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
                       
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnLogin_ClickAsync(object sender, RoutedEventArgs e)
        {

            try
            {
                string username = txtUser.Text;
                string password = txtPass.Password;

                loading.Visibility = Visibility.Visible;

                loading.IsIndeterminate = true;

                // Cộng chuỗi - sql injection
               

                var builder = new SqlConnectionStringBuilder();
                builder.DataSource = ".\\SQLEXPRESS";
                builder.InitialCatalog = "MyShopDB";
                builder.IntegratedSecurity = true;
                builder.TrustServerCertificate = true;
                builder.UserID = username;
                builder.Password = password;

                




                string connectionString = builder.ConnectionString;
                var connection = new SqlConnection(connectionString);

                connection = await Task.Run(() =>
                {
                    var _connection = new SqlConnection(connectionString);

                    try
                    {
                        _connection.Open();
                    }
                    catch (Exception ex)
                    {

                        _connection = null;
                    }

                    // Test khi chạy quá nhanh
                    //System.Threading.Thread.Sleep(3000);
                    return _connection;
                });

                loading.IsIndeterminate = false;
                loading.Visibility = Visibility.Collapsed;
                if (connection != null)
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

                    DB.Instance.ConnectionString = connectionString;

                    var screen = new MainWindow();
                    screen.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        $"Cannot connect"
                    );
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Show("Error when login");
            }
        }
           
            
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        
    }
}
