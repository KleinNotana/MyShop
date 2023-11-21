using Contract;
using Entity;
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

namespace UIVersion03
{
    /// <summary>
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : UserControl
    {   
        IBus _bus = null;
        public OrdersView(IBus bus)
        {   

            InitializeComponent();
            _bus = bus;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<Product> products = _bus.GetProducts();
            MessageBox.Show(products[0].ProductName);
        }
    }
}
