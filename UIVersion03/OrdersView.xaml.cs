using Contract;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        int _changedItemPerPage = 5;
        string fromDate="1/1/1900";
        string toDate="1/1/2100";
        public OrdersView(IBus bus)
        {   

            InitializeComponent();
            _bus = bus;
            
        }
        private void loadOrders()
        {
            var orderslist = _bus.GetOrderByFilter(fromDate, toDate, _currentPage, _itemPerPage);
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
            else
            {
                dynamic pageNumbers = new { Current = 0, Total = 0 };
                txtPages.DataContext = pageNumbers;
            }



        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _itemPerPage = int.Parse(ConfigurationManager.AppSettings["OrderPageSize"]);

            if (_itemPerPage <= 0)
            {
                _itemPerPage = 5;
            }

            txtPageSize.Text = _itemPerPage.ToString();
            loadOrders();

        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        /*private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Order1 order = new Order1();
            order.OrderDate = DateTime.Now;
            order.CustomerId = 1;
            _bus.addOrder(order);
            loadOrders();
      
        }*/

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
            var order = OrderDataGrid.SelectedItem;
            int id = (int)order.GetType().GetProperty("Id").GetValue(order);
            var editOrderWindow = new EditOrderWindow(_bus,id);
            editOrderWindow.ShowDialog();
            loadOrders();
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            /*Order1 order = new Order1();
            order.OrderDate = DateTime.Now;
            order.CustomerId = 1;
            _bus.addOrder(order);

            OrderDetail orderDetail = new OrderDetail();
            orderDetail.OrderId = order.Id;
            orderDetail.ProductId = 1;
            orderDetail.Amount = 1;
            orderDetail.Price = 20;
            orderDetail.TotalPrice = 20;
            _bus.addOrderDetail(orderDetail);*/
            var addOrderWindow = new AddOrderWindow(_bus);
            addOrderWindow.ShowDialog();
            loadOrders();
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            fromDate = convertdate(txtFromDate.Text);
            toDate = convertdate(txtToDate.Text);
            try {
                DateTime.Parse(fromDate);
                DateTime.Parse(toDate);
             }
            catch (Exception)
            {
                fromDate = "1/1/1900";
                toDate = "1/1/2100";
                MessageBox.Show("Invalid date format");
            }
            loadOrders();
        }

        private void txtPageSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Enter)
            {
                if (txtPageSize.Text != "")
                {
                    _changedItemPerPage = int.Parse(txtPageSize.Text);
                    if (_changedItemPerPage > 0)
                    {
                        _itemPerPage = _changedItemPerPage;
                        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        config.AppSettings.Settings["OrderPageSize"].Value = _itemPerPage.ToString();
                        config.Save(ConfigurationSaveMode.Minimal);

                        ConfigurationManager.RefreshSection("appSettings");
                        loadOrders();
                    }
                }
            }
        }

        private void txtPageSize_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPageSize.Text != "")
            {
                _changedItemPerPage = int.Parse(txtPageSize.Text);
                if (_changedItemPerPage > 0)
                {
                    _itemPerPage = _changedItemPerPage;
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["OrderPageSize"].Value = _itemPerPage.ToString();
                    config.Save(ConfigurationSaveMode.Minimal);

                    ConfigurationManager.RefreshSection("appSettings");
                    loadOrders();
                }
            }
        }

        public string convertdate(string mmddyyyydate)
        {
            string result = "";
            if (mmddyyyydate.Length>0) {
            string[] date = mmddyyyydate.Split('/');
            result = date[1] + "/" + date[0] + "/" + date[2];
            }
            
            
            return result;
            

        }

        private void btnDetailItem_Click(object sender, RoutedEventArgs e)
        {
            var order = OrderDataGrid.SelectedItem;
            int id = (int)order.GetType().GetProperty("Id").GetValue(order);
            var viewDetailOrderWindow = new ViewDetailOrderWindow(_bus, id);
            viewDetailOrderWindow.ShowDialog();

        }
    }
}
