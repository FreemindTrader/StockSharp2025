using System;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    [ValueConversion( typeof( bool ), typeof( bool ) )]
    public class InverseBooleanConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object )!( bool )value;
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object )!( bool )value;
        }
    }
}
