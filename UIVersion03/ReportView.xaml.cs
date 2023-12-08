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
    public partial class ReportView : UserControl
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
        public ReportView(IBus bus)
        {   

            InitializeComponent();
            _bus = bus;
            
        }
        private void loadMonthlyReport()
        {
            List<dynamic> orderslist = _bus.GetMonthlyReport();
            orders = new BindingList<dynamic>(orderslist);
            ReportByMonthDataGrid.ItemsSource = orders;

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {/*
            _itemPerPage = int.Parse(ConfigurationManager.AppSettings["OrderPageSize"]);

            if (_itemPerPage <= 0)
            {
                _itemPerPage = 5;
            }

            txtPageSize.Text = _itemPerPage.ToString();
            loadOrders();*/
            loadMonthlyReport();

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

       

        private void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }



        

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            /*fromDate = convertdate(txtFromDate.Text);
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
            loadOrders();*/
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

        
    }
}
