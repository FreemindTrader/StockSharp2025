using StockSharp.Xaml.Charting.Ultrachart;
using System;
using System.Globalization;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting
{
    internal sealed class ChartIndicatorConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert( object[ ] values,
                                             Type targetType,
                                             object parameter,
                                             CultureInfo culture )
        {
            if( !( values[ 0 ] is IChartElement element ) )
            {
                return null;
            }
            if( !( element is ChartIndicatorElement ) && !( element is ChartBandElement ) && ( !( element is ChartLineElement ) && !( element is VolatilitySmileUI ) ) )
            {
                return element;
            }
            return new ChartIndicatorElementSettingsObject( element );
        }

        object[ ] IMultiValueConverter.ConvertBack( object value,
                                                    Type[ ] targetTypes,
                                                    object parameter,
                                                    CultureInfo culture )
        {
            throw new NotSupportedException( );
        }
    }
}
