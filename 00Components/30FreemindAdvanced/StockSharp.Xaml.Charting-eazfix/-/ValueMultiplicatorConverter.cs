using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class ValueMultiplicatorConverter :
  IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        double num = (double) _param1;
        string s = (string) _param3 ?? "1";
        double result;
        if ( double.TryParse( s, NumberStyles.Any, ( IFormatProvider ) _param4, out result ) )
            return ( object ) ( num * result );
        return double.TryParse( s, NumberStyles.Any, ( IFormatProvider ) CultureInfo.InvariantCulture, out result ) ? ( object ) ( num * result ) : ( object ) null;
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
