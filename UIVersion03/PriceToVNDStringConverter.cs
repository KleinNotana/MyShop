using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UIVersion03.Converters
{
    public class PriceToVNDStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double number = (double)value;
            string result = number.ToString("#,###");
            result = $"{result} VND";
            return result;
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string number = (string)value;
            double result = double.Parse(number);
            return result;
        }
    }

      
}
