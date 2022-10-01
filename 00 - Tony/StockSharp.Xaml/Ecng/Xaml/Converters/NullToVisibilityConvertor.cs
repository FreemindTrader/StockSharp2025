using Ecng.Common;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    public class NullToVisibilityConvertor : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == null )
            {
                return ( object )Visibility.Collapsed;
            }

            return ( value.To<string>().IsEmptyOrWhiteSpace() ? Visibility.Collapsed : Visibility.Visible );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
