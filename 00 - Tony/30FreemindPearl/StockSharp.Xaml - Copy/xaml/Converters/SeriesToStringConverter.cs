using StockSharp.Algo;
using System;
using System.Globalization;
using System.Windows.Data;

namespace StockSharp.Xaml.Converters
{
    public class SeriesToStringConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object ) TraderHelper.ToReadableString( ( DataType ) value );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
