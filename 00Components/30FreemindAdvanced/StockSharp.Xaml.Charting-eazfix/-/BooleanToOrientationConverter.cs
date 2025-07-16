using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class BooleanToOrientationConverter : IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        return ( object ) ( Orientation ) ( ( bool ) _param1 ? 0 : 1 );
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
