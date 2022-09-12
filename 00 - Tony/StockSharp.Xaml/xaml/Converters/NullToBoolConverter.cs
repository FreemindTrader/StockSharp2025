using System;
using System.Globalization;
using System.Windows.Data;

using Ecng.Common;

using StockSharp.BusinessEntities;

using StockSharp.Localization;

namespace StockSharp.Xaml
{
    public class NullToBoolConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return value != null;
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
