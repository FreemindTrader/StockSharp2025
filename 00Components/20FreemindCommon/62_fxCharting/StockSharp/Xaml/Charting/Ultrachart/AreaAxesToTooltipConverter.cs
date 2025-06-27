using StockSharp.Localization;
using System;
using System.Globalization;
using System.Windows.Data;

namespace fx.Charting.Ultrachart
{
    public class AreaAxesToTooltipConverter : IMultiValueConverter
    {
        public object Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {
            if( values.Length == 2 && values[ 0 ] != null && values[ 1 ] != null )
            {
                return null;
            }
            return LocalizedStrings.AxisIsNotSet;
        }

        public object[ ] ConvertBack( object value,
                                      Type[ ] targetTypes,
                                      object parameter,
                                      CultureInfo culture )
        {
            throw new NotSupportedException( );
        }
    }
}
