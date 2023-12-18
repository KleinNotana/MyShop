using Contract;
using FontAwesome.Sharp;
using LiveCharts;
using LiveCharts.Wpf;
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
    public partial class DashboardView :  UserControl, INotifyPropertyChanged
    {
        IBus _bus = null;
        public DashboardView(IBus bus)
        {
            InitializeComponent();
            _bus = bus;
            DataContext = this;
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            outOfStockProducts.ItemsSource = _bus.GetOutOfStockProducts();
            totalSales.DataContext = _bus.GetTotalSales();
            totalSellingProducts.DataContext = _bus.GetSellingProductAmount();
            totalSoldProducts.DataContext = _bus.GetSoldProductAmount();
            totalCustomers.DataContext = _bus.GetTotalCustomers();

            var topSaleProducts = _bus.GetTopSaleProducts();

            PieChartSeriesCollection = new SeriesCollection();

           

            for(int i = 0; i < topSaleProducts.Count; i++)
            {
                var product = topSaleProducts[i];
                var Name = product.GetType().GetProperty("Name").GetValue(product);
                var Sold = product.GetType().GetProperty("Sold").GetValue(product);

                PieChartSeriesCollection.Add(new PieSeries
                {
                    Title = Name,
                    Values = new ChartValues<int> { Sold },
                    DataLabels = true
                });
            }
            
            ColumnChartSeriesCollection = new SeriesCollection();
           

            var currentDailySales = _bus.GetCurrentDailySales();

            var values = new List<double>();

            Labels = new List<string>();

            foreach(var dailySale in currentDailySales)
            {
                var date = dailySale.GetType().GetProperty("Date").GetValue(dailySale);
                var total = dailySale.GetType().GetProperty("Income").GetValue(dailySale);

                Labels.Add(date.ToString());
                values.Add((double)total);

            }

            ColumnChartSeriesCollection.Add(new ColumnSeries
            {
                Title = "Daily Sales",
                Values = new ChartValues<double>(values),
                DataLabels = true,
                ColumnPadding = 10
            });

            

            
        }

        public SeriesCollection PieChartSeriesCollection { get; set; }

        public SeriesCollection ColumnChartSeriesCollection { get; set; }

        public List<string> Labels { get; set; }


    }
}
