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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DiscountView : UserControl
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
        public DiscountView(IBus bus)
        {
            InitializeComponent();
            _bus = bus;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            sortTypes = new List<string>() { "Name", "Price" };
            priceFilters = new List<string>() { "All", "0 - 1,000,000", "1,000,000 - 10,000,000", "10,000,000 - 50,000,000", "Over 50,000,000" };
            sortComboBox.ItemsSource = sortTypes;
            filterComboBox.ItemsSource = priceFilters;
            sortComboBox.SelectedIndex = 0;
            filterComboBox.SelectedIndex = 0;
            _itemPerPage = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            categories = _bus.GetCategories();
            categories.Insert(0, new Category() { Id = -1, Name = "All" });
            cb_category.ItemsSource = categories;

            if (_itemPerPage <= 0)
            {
                _itemPerPage = 5;
            }

            txtPageSize.Text = _itemPerPage.ToString();
            loadProducts();
        }

        void loadProducts()
        {
            String sortType = sortComboBox.SelectedItem as String;
            String priceFilter = filterComboBox.SelectedItem as String;
            var priceFrom = -1;
            var priceTo = -1;
            var category = cb_category.SelectedItem as Category;
            var categoryId = -1;

            if (category != null)
            {
                categoryId = category.Id;
            }

            if (sortType == null)
            {
                sortType = "Name";
            }

            if (priceFilter == null)
            {
                priceFilter = "All";
            }



            if (priceFilter != "All")
            {
                if (priceFilter == "Over 50,000,000")
                {
                    priceFrom = 50000000;
                    priceTo = int.MaxValue;
                }
                else
                {
                    var priceRange = priceFilter.Split('-');
                    priceFrom = int.Parse(priceRange[0].Replace(",", ""));
                    priceTo = int.Parse(priceRange[1].Replace(",", ""));
                }
            }

            var products = _bus.GetDiscountProductsByFilter(txtSearch.Text, sortType, priceFrom, priceTo, _currentPage, _itemPerPage, categoryId);

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
            dynamic pageNumbers = new { Current = _currentPage, Total = _totalPage };
            txtPages.DataContext = pageNumbers;

        }

        private void btnAddDiscount_Click(object sender, RoutedEventArgs e)
        {
            var product = productsList.SelectedItem as dynamic;
            
            string discount = txtDiscount.Text;

            int discountInt = 0;

            if (discount != "")
            {
                discountInt = int.Parse(discount);
            }
            else {                
                MessageBox.Show("Please enter discount");
                           
                return;
                      
            }

            DateTime expDate = dpDiscount.SelectedDate.Value;

            if(expDate == null)
            {
                MessageBox.Show("Please enter date");
                return;
            }

            if (expDate < DateTime.Now)
            {
                MessageBox.Show("Please enter valid date");
                return;
            }

            if (discountInt < 0 || discountInt > 100)
            {
                MessageBox.Show("Please enter valid discount");
                return;
            }
            

            if (product != null)
            {
               int id =  product.GetType().GetProperty("Id").GetValue(product);
                _bus.UpdateDiscountProduct(id, discountInt, expDate);
                loadProducts();
            }
            else
            {
                MessageBox.Show("Please select a product to add discount");
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loadProducts();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            loadProducts();
        }

        private void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadProducts();
        }

        private void cb_category_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            loadProducts();
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
                        config.AppSettings.Settings["PageSize"].Value = _itemPerPage.ToString();
                        config.Save(ConfigurationSaveMode.Minimal);

                        ConfigurationManager.RefreshSection("appSettings");
                        loadProducts();
                    }
                }
            }
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
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

        private void txtPageSize_LostFocus(object sender, RoutedEventArgs e)
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

        private void btnRemoveDiscount_Click(object sender, RoutedEventArgs e)
        {
            var product = productsList.SelectedItem as dynamic;

            if (product != null)
            {
                int id = product.GetType().GetProperty("Id").GetValue(product);
                _bus.RemoveDiscountProduct(id);
                loadProducts();
            }
            else
            {
                MessageBox.Show("Please select a product to remove discount");
            }
        }
    }
}
