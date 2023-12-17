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
    public partial class AddOrderWindow : Window
    {
        IBus _bus;
        
        List<OrderDetail> orderDetails = new List<OrderDetail>();
        BindingList<dynamic> visibleOrderDetail;
        BindingList<Product> products;
        //Product product = new Product();

        public AddOrderWindow(IBus bus)
        {
            InitializeComponent();
            _bus = bus;
        }

        public void loadOrderDetails()
        {   
            List<Product> products = new List<Product>(_bus.GetProducts());
            var query= from orderdetail in orderDetails
                                 join product in products on orderdetail.ProductId equals product.Id
                                 select new {OrderId=orderdetail.OrderId, ProductId= orderdetail.ProductId, ProductName = product.ProductName, Amount = orderdetail.Amount, Price = orderdetail.Price, Total = orderdetail.Amount * orderdetail.Price };
            var list = query.ToList<dynamic>();
            visibleOrderDetail = new BindingList<dynamic>(list);
            OrderDataGrid.ItemsSource = visibleOrderDetail;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {   
            products = new BindingList<Product>(_bus.GetProducts());
            categoryComboBox.ItemsSource = products;
            categoryComboBox.SelectedIndex = 0;
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            //validate
            if (txtCustomerName.Text == "" || txtCustomerPhone.Text == "" || txtCustomerAge.Text == "")
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int age = 0;
            //try to parse age
            try
            {
                age = int.Parse(txtCustomerAge.Text);
                if (age < 0)
                {
                    MessageBox.Show("Age must be positive", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Order1 order = new Order1();
            order.OrderDate = DateTime.Now;
            order.CustomerId = 1;
            
            Customer customer = _bus.getCustomerByPhone(txtCustomerPhone.Text);
            if (customer != null) {
                //show option to use old customer or create new customer
                MessageBoxResult result = MessageBox.Show("Customer with this phone number already exists. Do you want to update this customer", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    //add new customer
                    //Customer newCustomer = new Customer();
                    customer.CustomerName = txtCustomerName.Text;
                    customer.PhoneNumber = txtCustomerPhone.Text;
                    customer.Age = age;
                    _bus.saveChanges();
                    order.CustomerId = customer.Id;
                }
                else
                order.CustomerId = customer.Id; 
            }
            else
            {
                //add new customer
                Customer newCustomer = new Customer();
                newCustomer.CustomerName = txtCustomerName.Text;
                newCustomer.PhoneNumber = txtCustomerPhone.Text;
                newCustomer.Age = int.Parse(txtCustomerAge.Text);
                _bus.addCustomer(newCustomer);
                order.CustomerId = newCustomer.Id;

            }

            //add order
            _bus.addOrder(order);
            //add order details
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderId = order.Id;
                _bus.addOrderDetail(orderDetail);
            }
            MessageBox.Show("Add order successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();





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


        

        

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {   
            Product product = categoryComboBox.SelectedItem as Product;
            if (product == null)
            {
                MessageBox.Show("Please choose a product", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //if can't parse
            
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.ProductId = product.Id;
            //check if product has discount
            DateTime now = DateTime.Now;
            DateTime? discountDate = product.DiscountDate;
            if (product.Discount != null && now <= discountDate)
            {   
                var discount = 1.0*(int)product.Discount/100;
                orderDetail.Price = (int)(product.Price * (1 - discount));
            }
            else
                orderDetail.Price = product.Price;
            //orderDetail.Price = product.Price;
            

            try
            {
                orderDetail.Amount = int.Parse(txtAmount.Text);
                if (orderDetail.Amount < 0)
                {
                    MessageBox.Show("Amount must be positive", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (orderDetail.Amount > product.Amount)
                {
                    MessageBox.Show("Amount must be less than or equal to product amount", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            orderDetail.TotalPrice = (int?)(orderDetail.Amount * orderDetail.Price);

            orderDetails.Add(orderDetail);
            products.Remove(product);

            loadOrderDetails();

        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            var orderDetail = OrderDataGrid.SelectedItem;
            
            var productID = orderDetail.GetType().GetProperty("ProductId").GetValue(orderDetail);

            var deleteOrderDetail = orderDetails.Where(o => o.ProductId == (int)productID).FirstOrDefault();
            orderDetails.Remove(deleteOrderDetail);
            products.Add(_bus.getProductById(deleteOrderDetail.ProductId));
            loadOrderDetails();
        }
    }
}
