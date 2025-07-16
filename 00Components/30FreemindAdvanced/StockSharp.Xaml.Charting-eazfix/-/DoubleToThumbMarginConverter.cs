using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class DoubleToThumbMarginConverter : IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        double num1 = (double) _param1;
        int num2 = string.Equals(_param3 as string, "VERTICAL", StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;
        double num3 = num1 / 2.0;
        return ( object ) ( num2 != 0 ? new Thickness( 0.0, -num3, 0.0, -num3 ) : new Thickness( -num3, 0.0, -num3, 0.0 ) );
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
