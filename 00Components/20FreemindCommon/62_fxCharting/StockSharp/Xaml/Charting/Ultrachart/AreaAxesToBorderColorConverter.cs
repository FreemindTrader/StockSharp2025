using Ecng.Xaml;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting.Ultrachart
{
    public class AreaAxesToBorderColorConverter : IMultiValueConverter
    {
        public object Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {
            if( values.Length == 2 && values[ 0 ] != null && values[ 1 ] != null )
            {
                return null;
            }
            return new SolidColorBrush( Colors.Red.ToTransparent( 50 ) );
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
