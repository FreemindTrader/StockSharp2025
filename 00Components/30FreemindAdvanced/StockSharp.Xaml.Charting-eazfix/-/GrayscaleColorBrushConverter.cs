using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal sealed class GrayscaleColorBrushConverter :
  IValueConverter
{
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
        Color color = new Color();
        SolidColorBrush solidColorBrush = value as SolidColorBrush;
        if ( solidColorBrush != null )
            color = solidColorBrush.Color;
        else if ( value is Color )
            color = ( Color ) value;
        return ( object ) new SolidColorBrush( ( double ) color.R * 0.299 + ( double ) color.G * 0.587 + ( double ) color.B * 0.114 > 128.0 ? Colors.Black : Colors.White );
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
