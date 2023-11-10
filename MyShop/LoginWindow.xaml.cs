﻿using DemoAdoNet2;
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
                //builder.IntegratedSecurity = true;
                builder.TrustServerCertificate = true;
                builder.UserID = username;
                builder.Password = password;






                string connectionString = builder.ConnectionString;
                //var connection = new SqlConnection(connectionString);
                MyShopDbContext context = new MyShopDbContext(connectionString);
                //run in background thread
                
                bool canLogin = await context.Database.CanConnectAsync();
                if (canLogin)
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

                    //DB.Instance.ConnectionString = connectionString;

                    var screen = new MainWindow();
                    screen.Show();

                    this.Close();
                }
                else
                {
                    
                    LoginWarning.Visibility = Visibility.Visible;
                }

                




            }
            catch (Exception ex)
            {
                Log.Error(ex);
                MessageBox.Show(
                                       $"Error: {ex.Message}"
                                                      );
            }
            finally
            {
                loading.IsIndeterminate = false;
                loading.Visibility = Visibility.Collapsed;
            }
        }
           
            
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //load login info
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
