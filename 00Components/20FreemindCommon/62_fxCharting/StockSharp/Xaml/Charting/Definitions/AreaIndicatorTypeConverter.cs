using fx.Charting;
using StockSharp.Algo.Indicators;
using System;
using System.Globalization;
using System.Windows.Data;

internal sealed class AreaIndicatorTypeConverter : IMultiValueConverter
{
    public object Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
    {
        return Tuple.Create( ( ChartArea ) values[ 0 ], ( IndicatorType ) values[ 1 ] );
    }

    public object[ ] ConvertBack(
      object value,
      Type[ ] targetTypes,
      object parameter,
      CultureInfo culture )
    {
        throw new NotSupportedException( );
    }
}
