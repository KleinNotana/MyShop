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
using LiveCharts;
using LiveCharts.Wpf;

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
        int currentMode = 0;

        int currentGroupByType = 0;
        
        public ReportView(IBus bus)
        {   

            InitializeComponent();
            _bus = bus;
            
        }

        public void loadReport()
        {
            if (currentGroupByType == 0)
            {
                loadDailyReport();

            }
            else if (currentGroupByType == 1)
            {
                loadMonthlyReport();
            }
            else if (currentGroupByType == 2)
            {
                loadYearlyReport();
            }
            else if (currentGroupByType == 3)
            {
                loadWeeklyReport();
            }
        }
        private void loadMonthlyReport()
        {
            List<dynamic> orderslist = _bus.GetMonthlyReport(fromDate,toDate,currentMode);
            orders = new BindingList<dynamic>(orderslist);
            ReportByTimeDataGrid.ItemsSource = orders;
            ReportProductByTimeDataGrid.ItemsSource = orders;

        }
        private void loadDailyReport()
        {
            List<dynamic> orderslist = _bus.GetDailyReport(fromDate, toDate, currentMode);
            orders = new BindingList<dynamic>(orderslist);
            ReportByTimeDataGrid.ItemsSource = orders;
            ReportProductByTimeDataGrid.ItemsSource = orders;

        }

        private void loadWeeklyReport()
        {

            List<dynamic> orderslist = _bus.GetWeeklyReport(fromDate, toDate, currentMode);
            orders = new BindingList<dynamic>(orderslist);
            ReportByTimeDataGrid.ItemsSource = orders;
            ReportProductByTimeDataGrid.ItemsSource = orders;
        }

        private void loadYearlyReport()
        {
            List<dynamic> orderslist = _bus.GetYearlyReport(fromDate, toDate, currentMode);
            orders = new BindingList<dynamic>(orderslist);
            ReportByTimeDataGrid.ItemsSource = orders;
            ReportProductByTimeDataGrid.ItemsSource = orders;
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

            GroupComboBox.ItemsSource = new List<string> { "Daily", "Monthly","Yearly", "Weekly"  };
            GroupComboBox.SelectedIndex = 0;

            ModeComboBox.ItemsSource = new List<string> { "By Profit", "By Product" };
            ModeComboBox.SelectedIndex = 0;
            //loadMonthlyReport();
            loadDailyReport();

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

       

        

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }



        

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            fromDate = convertdate(txtFromDate.Text);
            toDate = convertdate(txtToDate.Text);
            try
            {
                DateTime.Parse(fromDate);
                DateTime.Parse(toDate);
            }
            catch (Exception)
            {
                fromDate = "1/1/1900";
                toDate = "1/1/2100";
                MessageBox.Show("Invalid date format");
            }

            loadReport();

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

        private void GroupTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupComboBox.SelectedIndex == 0)
            {   
                currentGroupByType = 0;
                
            }
            else if (GroupComboBox.SelectedIndex == 1)
            {   
                currentGroupByType = 1;
                


            }
            else if (GroupComboBox.SelectedIndex == 2)
            {
                currentGroupByType = 2;
                
            }
            else if (GroupComboBox.SelectedIndex == 3)
            {
                currentGroupByType = 3;
                
            }
            loadReport();

        }

        private void ModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ModeComboBox.SelectedIndex == 0)
            {   
                ReportByProfitZone.Visibility = Visibility.Visible;
                ReportByProductZone.Visibility = Visibility.Hidden;
                currentMode = 0;
            }
            else if (ModeComboBox.SelectedIndex == 1)
            {   
                ReportByProfitZone.Visibility = Visibility.Hidden;
                ReportByProductZone.Visibility = Visibility.Visible;

                currentMode = 1;
            }
            loadReport();
        }
    }
}
