using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StockSharp.Xaml
{
    internal sealed class WidthToPointNumberConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            double? nullable = value as double?;
            return ( object )( !nullable.HasValue || double.IsNaN( nullable.Value ) ? 0 : ( int )( nullable.Value / 500.0 + 1.0 ) * 100 );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException( );
        }
    }
}
