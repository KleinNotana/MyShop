using Contract;
using Entity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UIVersion03
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        public Product product = null;
        IBus _bus = null;

        public EditProductWindow(IBus bus , int id)
        {
            InitializeComponent();
            _bus = bus;
            product = _bus.getProductById(id);
            this.DataContext = product;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindingList<Category> categories = _bus.GetCategories();
            categoryComboBox.ItemsSource = categories;
            var category = from c in categories
                        where c.Id == product.CategoryId
                        select c;
            var index = categories.IndexOf(category.First());
            categoryComboBox.SelectedIndex =  index;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtPrice.Text == "" || txtStock.Text == "")
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _bus.updateProduct(product);
            
            DialogResult = true;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper wih = new WindowInteropHelper(this);
            SendMessage(wih.Handle, 161, 2, 0);
        }


        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                e.Handled = true;
            }
        }

        private void txtStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                e.Handled = true;
            }
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog.Title = "Please select an image file.";
            if (openFileDialog.ShowDialog() == true)
            {

                string sourcePath = openFileDialog.FileName;

                product.ImgPath = sourcePath;
            }
        }

        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category category = categoryComboBox.SelectedItem as Category;
            product.CategoryId = category.Id;
        }
    }
}
