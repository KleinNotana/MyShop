using Contract;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class CategoryWindow : Window
    {
        IBus _bus;
        BindingList<Category> categories;
        public CategoryWindow(IBus bus)
        {
            InitializeComponent();
            _bus = bus;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            categories = _bus.GetCategories();
            categoryList.ItemsSource = categories;
        }

        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
           
            
            Category category = categoryList.SelectedItem as Category;
            if(category != null)
            {
                btnAddNewCategory.Visibility = Visibility.Collapsed;
                editCategory.Visibility = Visibility.Visible;
                editCategoryButtons.Visibility = Visibility.Visible;
                txtEditName.Text = category.Name;
            }
            else
            {
                   MessageBox.Show("No category was selected");
            }
        }

       

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var category = (Category) categoryList.SelectedItem ;
            if(category != null)
            {
                _bus.deleteCategory(category.Id);
                categories.Remove(category);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var category = new Category();
            category.Name = txtName.Text;
            bool result = _bus.addCategory(category);
            if (result)
            {
                categories.Add(category);
            }
            else
            {
                MessageBox.Show("Category name is existed or invalid format name");
            }

            txtName.Text = "";
            addNewCategory.Visibility = Visibility.Collapsed;
            btnAddCategory.Visibility = Visibility.Collapsed;
            btnAddNewCategory.Visibility = Visibility.Visible;
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

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            btnAddCategory.Visibility = Visibility.Visible;
            addNewCategory.Visibility = Visibility.Visible;
            btnAddNewCategory.Visibility = Visibility.Collapsed;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtEditName.Text;
            if(name == "")
            {
                MessageBox.Show("Please fill the name");
                return;
            }
            else
            {
                Category category = categoryList.SelectedItem as Category;
                category.Name = txtEditName.Text;
                _bus.updateCategory(category);
                editCategory.Visibility = Visibility.Collapsed;
                editCategoryButtons.Visibility = Visibility.Collapsed;
                btnAddNewCategory.Visibility = Visibility.Visible;
            }


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            editCategory.Visibility = Visibility.Collapsed;
            editCategoryButtons.Visibility = Visibility.Collapsed;
            btnAddNewCategory.Visibility = Visibility.Visible;
        }
    }
}
