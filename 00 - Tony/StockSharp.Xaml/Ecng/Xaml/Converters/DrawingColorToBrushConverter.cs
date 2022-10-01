using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// <see cref="T:System.Drawing.Color" /> to <see cref="T:System.Windows.Media.SolidColorBrush" /> converter.
    ///     </summary>
    public class DrawingColorToBrushConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object )new SolidColorBrush( ( ( System.Drawing.Color )value ).ToWpf() );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
