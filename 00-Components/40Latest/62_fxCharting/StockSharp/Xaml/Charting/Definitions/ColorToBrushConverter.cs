using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

internal sealed class ColorToBrushConverter : IValueConverter
{
    object IValueConverter.Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture )
    {
        Color color = ( Color )value;
        float num = parameter != null ? parameter.To<float>( ) : 0.8f;
        return new SolidColorBrush( new Color( )
        {
            ScA = num,
            R = color.R,
            G = color.G,
            B = color.B
        } );
    }

    object IValueConverter.ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture )
    {
        throw new NotSupportedException( );
    }
}
