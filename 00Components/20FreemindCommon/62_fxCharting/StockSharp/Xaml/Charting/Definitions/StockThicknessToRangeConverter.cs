using System;
using System.Globalization;
using System.Windows.Data;

internal sealed class StockThicknessToRangeConverter : IValueConverter
{
    object IValueConverter.Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture )
    {
        return ( int ) value == 0;
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
