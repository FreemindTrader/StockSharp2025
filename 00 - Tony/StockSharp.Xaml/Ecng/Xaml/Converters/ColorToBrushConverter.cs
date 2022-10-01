using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// <see cref="T:System.Windows.Media.Color" /> to <see cref="T:System.Windows.Media.SolidColorBrush" /> converter.
    ///     </summary>
    public class ColorToBrushConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object )new SolidColorBrush( ( Color )value );
        }

        object IValueConverter.ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
        {
            throw new NotSupportedException();
        }
    }
}
