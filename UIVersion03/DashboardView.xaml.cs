using Contract;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace UIVersion03
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DashboardView :  UserControl
    {
        IBus _bus = null;
        public DashboardView(IBus bus)
        {
            InitializeComponent();
            _bus = bus;
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            outOfStockProducts.ItemsSource = _bus.GetProducts();
        }
    }
}
