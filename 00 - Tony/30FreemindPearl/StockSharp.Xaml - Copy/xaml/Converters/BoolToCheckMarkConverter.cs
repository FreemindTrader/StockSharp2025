using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using Ecng.Common;

using StockSharp.BusinessEntities;

using StockSharp.Localization;

namespace StockSharp.Xaml
{
    sealed class BoolToCheckMarkConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( bool ) value ? "\u2713" : string.Empty;
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
