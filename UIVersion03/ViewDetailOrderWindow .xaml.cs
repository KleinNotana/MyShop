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
    public partial class ViewDetailOrderWindow : Window
    {
        IBus _bus;
        Order1 order;
        
        List<OrderDetail> orderDetails = new List<OrderDetail>();
        
        BindingList<dynamic> visibleOrderDetail;
        
        //Product product = new Product();

        public ViewDetailOrderWindow(IBus bus, int Id)
        {
            InitializeComponent();
            _bus = bus;
            order = _bus.GetOrderById(Id);
            txtCustomerName.Text = order.Customer.CustomerName;
            orderDetails = _bus.GetOrdersDetailById(Id);
            txtOrderDate.Text = order.OrderDate.Value.Day +"/"+order.OrderDate.Value.Month + "/"+ order.OrderDate.Value.Year;
            txtTotalPrice.Text = _bus.GetTotalPrice(Id).ToString();
        }

        public void loadOrderDetails()
        {   
            List<Product> products = new List<Product>(_bus.GetProducts());
            var query= from orderdetail in orderDetails
                                 join product in products on orderdetail.ProductId equals product.Id
                                 select new { ProductId=orderdetail.ProductId, ProductName = product.ProductName, Amount = orderdetail.Amount, Price = orderdetail.Price, Total = orderdetail.Amount * orderdetail.Price };
            var list = query.ToList<dynamic>();
            visibleOrderDetail = new BindingList<dynamic>(list);
            OrderDataGrid.ItemsSource = visibleOrderDetail;

            

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            loadOrderDetails();

           
            
            
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

        private void btnClose2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //export to png
            btnClose2.Visibility = Visibility.Hidden;
            btnExport.Visibility = Visibility.Hidden;
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)OrderInfor.ActualWidth, (int)OrderInfor.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(OrderInfor);
                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));

                using (System.IO.Stream stream = System.IO.File.Create(saveFileDialog.FileName))
                {
                    png.Save(stream);
                }
            }
            btnClose2.Visibility = Visibility.Visible;
            btnExport.Visibility = Visibility.Visible;
        }
    }
}
