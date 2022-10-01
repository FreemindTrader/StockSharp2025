using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    public class VisibilityToBoolConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object )( ( Visibility )value == Visibility.Visible );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object )( Visibility )( ( bool )value ? 0 : 2 );
        }
    }
}