using Ecng.Common;
using Ecng.Xaml;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

#pragma warning disable CA1416

internal sealed class ColorToSeriesColorConverter : IValueConverter
{
    object IValueConverter.Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture )
    {
        return ( ( Color ) value ).ToTransparent( parameter != null ? parameter.To<byte>( ) : ( byte ) 0 );
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
