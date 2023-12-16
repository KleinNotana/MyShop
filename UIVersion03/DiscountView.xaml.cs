using Contract;
using System;
using System.Collections.Generic;
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
        public DiscountView(IBus bus)
        {
            InitializeComponent();
            _bus = bus;
        }

        private void btnAddDiscount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void filterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cb_category_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtPageSize_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtPageSize_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
