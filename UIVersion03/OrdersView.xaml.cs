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
        BindingList<dynamic> orders;
        int _itemPerPage = 5;
        
        int _currentPage = 1;
        int _totalPage = 1;
        int _totalItems = 1;
        public OrdersView(IBus bus)
        {   

            InitializeComponent();
            _bus = bus;
            
        }
        private void loadOrders()
        {
            var orderslist = _bus.GetOrderByFilter("1/1/1900", "1/1/2100", _currentPage, _itemPerPage);
            orders = new BindingList<dynamic>(orderslist);
            OrderDataGrid.ItemsSource = orders;
            if (orders.Count > 0)
            {
                _totalItems = orders.FirstOrDefault()?.GetType().GetProperty("Total").GetValue(orders.FirstOrDefault());
                //MessageBox.Show(_totalItems.ToString());
                _totalPage = _totalItems / _itemPerPage + (_totalItems % _itemPerPage == 0 ? 0 : 1);
                
                dynamic pageNumbers = new { Current = _currentPage, Total = _totalPage };
                txtPages.DataContext = pageNumbers;
            }



        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadOrders();

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

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _totalPage)
            {
                _currentPage++;
                loadOrders();
            }
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                loadOrders();
            }
        }

        private void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var order = OrderDataGrid.SelectedItem;
            orders.Remove(order);
            
            int id = (int)order.GetType().GetProperty("Id").GetValue(order);
            MessageBox.Show(id.ToString());
           _bus.DeleteOrder(id); 
            loadOrders();
            //test order detail
            /*var orderDetails = _bus.GetOrdersDetailById(id);
            MessageBox.Show(orderDetails[0].Price.ToString());
            */

        }
    }
}
