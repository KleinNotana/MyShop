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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Contract;
using FontAwesome.Sharp;
using System.Reflection.Metadata;
using Entity;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace UIVersion03
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBus _bus = null;

        enum Menu
        {
            Dashboard,
            Products,
            Orders,
            Reports, 
            Discount
        }

        Menu currentMenu;

        MainWindowDataObject dataObject = null;
        public MainWindow(IBus bus)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            _bus = bus;

            dataObject = new MainWindowDataObject();

            var lastMenu = ConfigurationManager.AppSettings["LastView"];
            if (lastMenu != null)
            {
                navigate((Menu)Enum.Parse(typeof(Menu), lastMenu));
            }
            else
            {
                navigate(Menu.Dashboard);
            }
            DataContext = dataObject;


        }

        [DllImport("user32.dll")]  
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper wih = new WindowInteropHelper(this);
            SendMessage(wih.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["LastView"].Value = currentMenu.ToString();

            config.Save(ConfigurationSaveMode.Minimal);

            ConfigurationManager.RefreshSection("appSettings");
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void DashboardNav_Checked(object sender, RoutedEventArgs e)
        {
            dataObject.Content = new DashboardView(_bus);
            dataObject.Icon = IconChar.Home;
            dataObject.Title = "Dashboard";
            currentMenu = Menu.Dashboard;
        }

        private void ProductsNav_Checked(object sender, RoutedEventArgs e)
        {
            dataObject.Content = new ProductsView(_bus);
            dataObject.Icon = IconChar.Boxes;
            dataObject.Title = "Products";
            currentMenu = Menu.Products;
        }

        private void OrdersNav_Checked(object sender, RoutedEventArgs e)
        {
            dataObject.Content = new OrdersView(_bus);
            dataObject.Icon = IconChar.ShoppingCart;
            dataObject.Title = "Orders";
            currentMenu = Menu.Orders;
        }

        private void DiscountNav_Checked(object sender, RoutedEventArgs e)
        {
            dataObject.Content = new DiscountView(_bus);
            dataObject.Icon = IconChar.Tags;
            dataObject.Title = "Discount";
            currentMenu = Menu.Discount;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*List<Product> products = _bus.GetProducts();
            MessageBox.Show(products[0].ProductName);*/

        }

        private void ReportsNav_Checked(object sender, RoutedEventArgs e)
        {
            dataObject.Content = new ReportView(_bus);
            dataObject.Icon = IconChar.Pager;
            dataObject.Title = "Report";
            currentMenu = Menu.Reports;
        }

        private void navigate(Menu menu)
        {
            switch (menu)
            {
                case Menu.Dashboard:
                    DashboardNav.IsChecked = true;
                    break;
                case Menu.Products:
                    ProductsNav.IsChecked = true;
                    break;
                case Menu.Orders:
                    OrdersNav.IsChecked = true;
                    break;
                case Menu.Reports:
                    ReportsNav.IsChecked = true;
                    break;
                case Menu.Discount:
                    DiscountNav.IsChecked = true;
                    break;
            }
        }

        
    }
}
