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
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        IBus _bus = null;
        List<string> priceFilters;
        List<string> sortTypes;
        int _itemPerPage = 5;
        int _changedItemPerPage = 5;
        int _currentPage = 1;
        int _totalPage = 1;
        int _totalItems = 1;
        public ProductsView(IBus bus)
        {
            InitializeComponent();
            _bus = bus;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
            sortTypes = new List<string>() { "Name", "Price" };
            priceFilters = new List<string>() { "All", "0 - 100", "100 - 200", "200 - 300", "300 - 400", "400 - 500" };
            sortComboBox.ItemsSource = sortTypes;
            filterComboBox.ItemsSource = priceFilters;
            sortComboBox.SelectedIndex = 0;
            filterComboBox.SelectedIndex = 0;
            loadProducts();
        }

        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
               loadProducts();
            }
        }


        private void loadProducts()
        {
            String sortType = sortComboBox.SelectedItem as String;
            String priceFilter = filterComboBox.SelectedItem as String;
            if(sortType == null)
            {
                sortType = "Name";
            }
            if(priceFilter == null)
            {
                priceFilter = "All";
            }
            var priceFrom = -1;
            var priceTo = -1;
            if (priceFilter != "All")
            {
                var priceFilterArr = priceFilter.Split('-');
                priceFrom = int.Parse(priceFilterArr[0]);
                priceTo = int.Parse(priceFilterArr[1]);
            }
            var products = _bus.GetProductsByFilter(txtSearch.Text, sortType, priceFrom, priceTo, _currentPage, _itemPerPage);
            productsList.ItemsSource = products;
            if (products.Count() > 0)
            {
                _totalItems = products.FirstOrDefault()?.GetType().GetProperty("Total").GetValue(products.FirstOrDefault());
                _totalPage = _totalItems / _itemPerPage + (_totalItems % _itemPerPage == 0 ? 0 : 1);

                
            }
            else
            {
                _totalItems = 0;
                _totalPage = 1;
              
            }
            dynamic pageNumbers = new {Current = _currentPage, Total = _totalPage};
            txtPages.DataContext = pageNumbers;

        }

        private void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadProducts();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            loadProducts();
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage > 1)
            {
                _currentPage--;
                loadProducts();
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _totalPage)
            {
                _currentPage++;
                loadProducts();
            }
        }
    }
}
