using Contract;
using Entity;
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
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : UserControl
    {   
        IBus _bus = null;
        List<Order1> orders;
        public OrdersView(IBus bus)
        {   

            InitializeComponent();
            _bus = bus;
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            orders = new List<Order1>();
            orders = _bus.GetOrders();
            List<Customer> customers = _bus.GetCustomers();
            var query = from o in orders join c in customers on o.CustomerId equals c.Id select new { Id = o.Id, Name = c.CustomerName };
            for (int i = 0; i < query.Count(); i++)
            {
                orders[i]._customerName = query.ToList()[i].Name;
                //MessageBox.Show(orders[i]._customerName);
            }

            OrderListView.ItemsSource = orders;

            


        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            orders[0].Id = 99;
      
        }

        private void BookListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
