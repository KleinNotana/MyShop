using Contract;
using Entity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    public partial class AddProductWindow : Window
    {
        IBus _bus;
        Product product = new Product();
        bool isSetImage = false;
        public AddProductWindow(IBus bus)
        {
            InitializeComponent();
            _bus = bus;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            categoryComboBox.ItemsSource = _bus.GetCategories();
            categoryComboBox.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtPrice.Text == "" || txtStock.Text == "")
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(!isSetImage)
            {
                MessageBox.Show("Please upload an image", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string name = txtName.Text;
            int price = int.Parse(txtPrice.Text);
            int stock = int.Parse(txtStock.Text);
            var category = (Category) categoryComboBox.SelectedItem ;

            product.ProductName = name;
            product.Price = price;
            product.Amount = stock;
            product.CategoryId = category.Id;
            product.Description = txtDescription.Text;

            bool result = _bus.addProduct(product);

            if (result)
            {
                MessageBox.Show("Add product successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Add product failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

            
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
                var fileName = openFileDialog.FileName;
                var destFile = System.IO.Path.Combine("Images", System.IO.Path.GetFileName(fileName));
                try
                {
                    System.IO.File.Copy(fileName, destFile, true);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }

                //string sourcePath = openFileDialog.FileName;

                product.ImgPath = destFile;


                /*string sourcePath = openFileDialog.FileName;

                product.ImgPath = sourcePath;*/
                isSetImage = true;
            }
        }
    }
}
