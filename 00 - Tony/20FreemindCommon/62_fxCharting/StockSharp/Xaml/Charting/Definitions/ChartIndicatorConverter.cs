using fx.Charting.Ultrachart;
using System;
using System.Globalization;
using System.Windows.Data;

namespace fx.Charting
{
    internal sealed class ChartIndicatorConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert( object[ ] values,
                                             Type targetType,
                                             object parameter,
                                             CultureInfo culture )
        {
            if( !( values[ 0 ] is IfxChartElement element ) )
            {
                return null;
            }
            if( !( element is IndicatorUI ) && !( element is BandsUI ) && ( !( element is LineUI ) && !( element is VolatilitySmileUI ) ) )
            {
                return element;
            }
            return new IndicatorUISettingsObject( element );
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
