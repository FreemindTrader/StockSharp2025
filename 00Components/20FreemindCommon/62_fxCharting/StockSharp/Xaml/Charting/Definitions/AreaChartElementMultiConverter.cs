using StockSharp.Charting;
using StockSharp.Xaml.Charting;
using System;
using System.Globalization;
using System.Windows.Data;

internal sealed class AreaChartElementMultiConverter : IMultiValueConverter
{
    public object Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
    {
        return Tuple.Create( values[ 0 ] as ChartArea, values[ 1 ] as IChartElement );
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
