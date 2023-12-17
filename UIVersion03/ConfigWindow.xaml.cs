using Contract;
using Entity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace UIVersion03
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public string cservername = ".\\SQLEXPRESS";
        public string cdatabasename = "MyShopDB";
        public ConfigWindow(string cservername, string cdatabasename)
        {
            InitializeComponent();
            this.cservername = cservername;
            this.cdatabasename = cdatabasename;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtServer.Text = cservername;
            txtDatabase.Text = cdatabasename;
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string servername = txtServer.Text;
            string databasename = txtDatabase.Text;
            if (servername == "" || databasename == "")
            {
                MessageBox.Show("Server name and database name cannot be empty");
                return;
            }
            else
            {
                cservername = servername;
                cdatabasename = databasename;
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    
                config.AppSettings.Settings["Server"].Value = servername;
                config.AppSettings.Settings["Database"].Value = databasename;

                config.Save(ConfigurationSaveMode.Minimal);

                ConfigurationManager.RefreshSection("appSettings");
                DialogResult = true;
                //this.Close();
            }
            
            
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        


       

        
    }
}
