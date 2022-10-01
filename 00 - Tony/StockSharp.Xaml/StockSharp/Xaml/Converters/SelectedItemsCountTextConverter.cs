using Ecng.Common;
using StockSharp.Localization;
using System;
using System.Globalization;
using System.Windows.Data;

namespace StockSharp.Xaml.Converters
{
    public sealed class SelectedItemsCountTextConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object ) LocalizedStrings.Str1852Params.Put(  value );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
