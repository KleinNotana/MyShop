using Contract;
using Entity;
using FontAwesome.Sharp;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Dynamic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
        BindingList<Category> categories;
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
            priceFilters = new List<string>() { "All", "0 - 100", "200 - 500", "500 - 700", "700 - 1000", "Over 1000" };
            sortComboBox.ItemsSource = sortTypes;
            filterComboBox.ItemsSource = priceFilters;
            sortComboBox.SelectedIndex = 0;
            filterComboBox.SelectedIndex = 0;
            _itemPerPage = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            categories = _bus.GetCategories();
            categories.Insert(0, new Category() { Id = -1, Name = "All" });
            cb_category.ItemsSource = categories;

            if(_itemPerPage <= 0)
            {
                _itemPerPage = 5;
            }

            txtPageSize.Text = _itemPerPage.ToString();
            loadProducts();
        }

        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = productsList.SelectedItem;
            if (selectedItem != null)
            {
                int id = selectedItem.GetType().GetProperty("Id").GetValue(selectedItem);
                var editProductWindow = new EditProductWindow(_bus, id);
                editProductWindow.ShowDialog();
                loadProducts();
            }
        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = productsList.SelectedItem;
            if (selectedItem != null)
            {
                int id = selectedItem.GetType().GetProperty("Id").GetValue(selectedItem);
                var result = MessageBox.Show("Are you sure to delete this product?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _bus.deleteProduct(id);
                    loadProducts();
                }
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic selectedItem = productsList.SelectedItem;
            if (selectedItem != null)
            {
                int id = selectedItem.GetType().GetProperty("Id").GetValue(selectedItem);

                DetailProduct.Visibility = Visibility.Visible;
                DetailProduct.DataContext = _bus.getDetailProduct(id);
            }
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
            var priceFrom = -1;
            var priceTo = -1;
            var category = cb_category.SelectedItem as Category;
            var categoryId = -1;

            if(category != null)
            {
                categoryId = category.Id;
            }

            if (sortType == null)
            {
                sortType = "Name";
            }

            if(priceFilter == null)
            {
                priceFilter = "All";
            }

            

            if (priceFilter != "All")
            {
                if(priceFilter == "Over 1000")
                {
                    priceFrom = 1000;
                    priceTo = int.MaxValue;
                }
                else
                {
                    var priceRange = priceFilter.Split('-');
                    priceFrom = int.Parse(priceRange[0]);
                    priceTo = int.Parse(priceRange[1]);
                }   
            }


            var products = _bus.GetProductsByFilter(txtSearch.Text, sortType, priceFrom, priceTo, _currentPage, _itemPerPage, categoryId);

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

        private void txtPageSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                e.Handled = true;
            }   

            if(e.Key == Key.Enter)
            {
                if (txtPageSize.Text != "")
                {
                    _changedItemPerPage = int.Parse(txtPageSize.Text);
                    if (_changedItemPerPage > 0)
                    {
                        _itemPerPage = _changedItemPerPage;
                        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        config.AppSettings.Settings["PageSize"].Value = _itemPerPage.ToString();
                        config.Save(ConfigurationSaveMode.Minimal);

                        ConfigurationManager.RefreshSection("appSettings");
                        loadProducts();
                    }
                }
            }
        }

        private void txtPageSize_LostFocus(object sender, RoutedEventArgs e)
        {
            if(txtPageSize.Text != "")
            {
                _changedItemPerPage = int.Parse(txtPageSize.Text);
                if(_changedItemPerPage > 0)
                {
                    _itemPerPage = _changedItemPerPage;
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["PageSize"].Value = _itemPerPage.ToString();
                    config.Save(ConfigurationSaveMode.Minimal);

                    ConfigurationManager.RefreshSection("appSettings");
                    loadProducts();
                }
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var addProductWindow = new AddProductWindow(_bus);
            addProductWindow.ShowDialog();
            loadProducts();
        }

        private void btnListProductTypes_Click(object sender, RoutedEventArgs e)
        {
            var categoryWindow = new CategoryWindow(_bus);
            categoryWindow.ShowDialog();
            categories = _bus.GetCategories();
            categories.Insert(0, new Category() { Id = -1, Name = "All" });
            cb_category.ItemsSource = categories;
            loadProducts();

        }

        private void cb_category_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            loadProducts();
        }

        private void btnImportData_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (fileDialog.ShowDialog() == true)
            {
                var filePath = fileDialog.FileName;
                var result = _bus.importData(filePath);
                if (result)
                {
                    MessageBox.Show("Import data successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    cb_category.ItemsSource = _bus.GetCategories();
                    loadProducts();
                }
                else
                {
                    MessageBox.Show("Import data failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
