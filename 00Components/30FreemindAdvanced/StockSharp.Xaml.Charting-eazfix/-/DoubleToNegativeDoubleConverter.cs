using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class DoubleToNegativeDoubleConverter : IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        return ( object ) -( double ) _param1;
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
