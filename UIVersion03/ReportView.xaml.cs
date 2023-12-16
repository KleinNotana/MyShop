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
using Microsoft.Identity.Client;
using System.Windows.Media.Animation;

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


        string currenttopdate = "";
        int currentsiTimeCB = 0;
        int currentGroupByType = 0;

        
        public ReportView(IBus bus)
        {   

            InitializeComponent();
            _bus = bus;
            DataContext = this;
            Labels = new List<string>();
            
            
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

        public List<string> Labels { get; set; }
        private void loadMonthlyReport()
        {
            



            List<dynamic> orderslist = _bus.GetMonthlyReport(fromDate,toDate,currentMode);
            orders = new BindingList<dynamic>(orderslist);
            ReportByTimeDataGrid.ItemsSource = orders;
            ReportProductByTimeDataGrid.ItemsSource = orders;

            BindingList<dynamic> top5sellingproduct;
            List<string> timelist = new List<string>();


            foreach (var order in orders)
            {
                string time = order.GetType().GetProperty("Time").GetValue(order);
                if (!timelist.Contains(time))
                {
                    timelist.Add(time);
                }
            }
            TimeComboBox.ItemsSource = timelist;
            //TimeComboBox.SelectedIndex = currentsiTimeCB;
            if (TimeComboBox.SelectedItem != null)
            {
                currenttopdate = "1/" + TimeComboBox.SelectedItem.ToString();
                //get top 3 selling product at current time
                var topproduct = _bus.GetTop5productbymonth(convertdateback(currenttopdate));
                topsellingproduct.ItemsSource = topproduct;
            }
            else
            {
                topsellingproduct.ItemsSource = null;
            }
            
            if (currentMode == 0)
            {
                SeriesCollection series = new SeriesCollection();
                foreach (var order in orders)
                {
                    double total = order.GetType().GetProperty("Total").GetValue(order);
                    string time = order.GetType().GetProperty("Time").GetValue(order);
                    series.Add(new PieSeries { Title = time, Values = new ChartValues<double> { total } });
                }
                ProfitPieChart.Series = series;

                SeriesCollection ColummCollection = new SeriesCollection();
                var values = new List<double>();
                Labels.Clear();
                foreach (var order in orders)
                {
                    double total = order.GetType().GetProperty("Total").GetValue(order);
                    string time = order.GetType().GetProperty("Time").GetValue(order);
                    values.Add(total);
                    Labels.Add(time);

                }
                ColummCollection.Add(new ColumnSeries
                {
                    Title = "Daily Sales",
                    Values = new ChartValues<double>(values),
                    DataLabels = true,
                    ColumnPadding = 10
                });
                //ProfitCartesianChart.DataContext = this;
                ProfitCartesianChart.Series = ColummCollection;
            }
            


        }
        private void loadDailyReport()
        {
            List<dynamic> orderslist = _bus.GetDailyReport(fromDate, toDate, currentMode);
            orders = new BindingList<dynamic>(orderslist);
            ReportByTimeDataGrid.ItemsSource = orders;
            ReportProductByTimeDataGrid.ItemsSource = orders;

            //get top 3 selling product at current time
            BindingList<dynamic> top5sellingproduct;
            List<string> timelist = new List<string>();


            foreach (var order in orders)
            {
                string time = order.GetType().GetProperty("Time").GetValue(order);
                if (!timelist.Contains(time))
                {
                    timelist.Add(time);
                }
            }
            TimeComboBox.ItemsSource = timelist;
            
            //TimeComboBox.SelectedIndex = currentsiTimeCB;
            if (TimeComboBox.SelectedItem != null) {
                currenttopdate = TimeComboBox.SelectedItem.ToString();
                //get top 3 selling product at current time
                var topproduct = _bus.GetTop5productbyday(convertdateback(currenttopdate));
                topsellingproduct.ItemsSource = topproduct;
            }
            else
            {
                topsellingproduct.ItemsSource = null;
            }
            
            if (currentMode == 0)
            {
                SeriesCollection series = new SeriesCollection();
                foreach (var order in orders)
                {
                    double total = order.GetType().GetProperty("Total").GetValue(order);
                    string time = order.GetType().GetProperty("Time").GetValue(order);
                    series.Add(new PieSeries { Title = time, Values = new ChartValues<double> { total } });
                }
                ProfitPieChart.Series = series;


               
                
                SeriesCollection ColummCollection = new SeriesCollection();
                var values = new List<double>();
                Labels.Clear();
                foreach (var order in orders)
                {
                    double total = order.GetType().GetProperty("Total").GetValue(order);
                    string time = order.GetType().GetProperty("Time").GetValue(order);
                    values.Add(total);
                    Labels.Add(time);
                   
                }
                ColummCollection.Add(new ColumnSeries
                {
                    Title = "Daily Sales",
                    Values = new ChartValues<double>(values),
                    DataLabels = true,
                    ColumnPadding = 10
                });
                //ProfitCartesianChart.DataContext = this;
                ProfitCartesianChart.Series = ColummCollection;
                
            }
            else
            {
                //draw cartisian chart
                //each line is a product
                //each column is a day

                SeriesCollection LineCollection= new SeriesCollection();
                var values = new List<double>();
                Labels.Clear();
                //get list of products name in orders
                List<string> productNames = new List<string>();
                foreach (var order in orders)
                {
                    string name = order.GetType().GetProperty("Name").GetValue(order);
                    if (!productNames.Contains(name))
                    {
                        productNames.Add(name);
                    }
                }

                //create multiple line for each product
                foreach (var productName in productNames)
                {
                    var values1 = new List<double>();
                    foreach (var order in orders)
                    {
                        double total = order.GetType().GetProperty("Total").GetValue(order);
                        string time = order.GetType().GetProperty("Time").GetValue(order);
                        string name = order.GetType().GetProperty("Name").GetValue(order);
                        if (name == productName)
                        {
                            values1.Add(total);
                        }
                    }
                    LineCollection.Add(new LineSeries
                    {
                        Title = productName,
                        Values = new ChartValues<double>(values1),
                        DataLabels = true,
                        LineSmoothness = 0,
                        PointGeometrySize = 10
                    });
                }
                
                ProductCartesianChart.Series = LineCollection;

            }

        }

        private void loadWeeklyReport()
        {

            List<dynamic> orderslist = _bus.GetWeeklyReport(fromDate, toDate, currentMode);
            orders = new BindingList<dynamic>(orderslist);
            ReportByTimeDataGrid.ItemsSource = orders;
            ReportProductByTimeDataGrid.ItemsSource = orders;


            BindingList<dynamic> top5sellingproduct;
            List<string> timelist = new List<string>();


            foreach (var order in orders)
            {   
                
                var week = order.GetType().GetProperty("Week").GetValue(order);
                string time=week.ToString();
                if (!timelist.Contains(time))
                {
                    timelist.Add(time);
                }
            }
            TimeComboBox.ItemsSource = timelist;
            //TimeComboBox.SelectedIndex = currentsiTimeCB;

            if (TimeComboBox.SelectedItem != null)
            {
                currenttopdate = TimeComboBox.SelectedItem.ToString();
                //get top 3 selling product at current time
                var topproduct = _bus.GetTop5productbyweek(fromDate, int.Parse(currenttopdate));
                topsellingproduct.ItemsSource = topproduct;
            }
            else
            {
                topsellingproduct.ItemsSource = null;
            }
            

            if (currentMode == 0)
            {
                SeriesCollection series = new SeriesCollection();
                foreach (var order in orders)
                {
                    double total = order.GetType().GetProperty("Total").GetValue(order);
                    string time = order.GetType().GetProperty("Time").GetValue(order);
                    series.Add(new PieSeries { Title = time, Values = new ChartValues<double> { total } });
                }
                ProfitPieChart.Series = series;
                SeriesCollection ColummCollection = new SeriesCollection();
                var values = new List<double>();
                Labels.Clear();

                foreach (var order in orders)
                {
                    double total = order.GetType().GetProperty("Total").GetValue(order);
                    string time = order.GetType().GetProperty("Time").GetValue(order);
                    values.Add(total);
                    Labels.Add(time);

                }
                ColummCollection.Add(new ColumnSeries
                {
                    Title = "Daily Sales",
                    Values = new ChartValues<double>(values),
                    DataLabels = true,
                    ColumnPadding = 10
                });
                //ProfitCartesianChart.DataContext = this;
                ProfitCartesianChart.Series = ColummCollection;
            }
        }

        private void loadYearlyReport()
        {
            List<dynamic> orderslist = _bus.GetYearlyReport(fromDate, toDate, currentMode);
            orders = new BindingList<dynamic>(orderslist);
            ReportByTimeDataGrid.ItemsSource = orders;
            ReportProductByTimeDataGrid.ItemsSource = orders;

            BindingList<dynamic> top5sellingproduct;
            List<string> timelist = new List<string>();


            foreach (var order in orders)
            {
                string time = order.GetType().GetProperty("Time").GetValue(order);
                if (!timelist.Contains(time))
                {
                    timelist.Add(time);
                }
            }
            TimeComboBox.ItemsSource = timelist;
            //TimeComboBox.SelectedIndex = currentsiTimeCB;

            if (TimeComboBox.SelectedItem != null)
            {
                currenttopdate = "1/1/" + TimeComboBox.SelectedItem.ToString();
                //get top 3 selling product at current time
                var topproduct = _bus.GetTop5productbyyear(convertdateback(currenttopdate));
                topsellingproduct.ItemsSource = topproduct;
            }
            else
            {
                topsellingproduct.ItemsSource = null;
            }
            


            if (currentMode == 0)
            {
                SeriesCollection series = new SeriesCollection();
                foreach (var order in orders)
                {
                    double total = order.GetType().GetProperty("Total").GetValue(order);
                    string time = order.GetType().GetProperty("Time").GetValue(order);
                    series.Add(new PieSeries { Title = time, Values = new ChartValues<double> { total } });
                }
                ProfitPieChart.Series = series;
                SeriesCollection ColummCollection = new SeriesCollection();
                var values = new List<double>();
                Labels.Clear();
                foreach (var order in orders)
                {
                    double total = order.GetType().GetProperty("Total").GetValue(order);
                    string time = order.GetType().GetProperty("Time").GetValue(order);
                    values.Add(total);
                    Labels.Add(time);

                }
                ColummCollection.Add(new ColumnSeries
                {
                    Title = "Daily Sales",
                    Values = new ChartValues<double>(values),
                    DataLabels = true,
                    ColumnPadding = 10
                });
                //ProfitCartesianChart.DataContext = this;
                ProfitCartesianChart.Series = ColummCollection;
            }
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

            //TimeComboBox.SelectedIndex = 0;
            //currentsiTimeCB = 0;
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
        public string convertdateback(string mmddyyyydate)
        {
            string result = "";
            if (mmddyyyydate.Length > 0)
            {
                string[] date = mmddyyyydate.Split('/');
                result = date[1] + "/" + date[0] + "/" + date[2];
            }
            return result;
        }
        private void GroupTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            //currentsiTimeCB = 0;
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
            //currentsiTimeCB = 0;
            if (ModeComboBox.SelectedIndex == 0)
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

        private void TimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TimeComboBox.SelectedIndex>=0)
            {   
                //currentsiTimeCB = TimeComboBox.SelectedIndex;
                //currenttopdate = TimeComboBox.SelectedItem.ToString();
                loadReport();
            }

            //loadReport();

        }
    }
}
