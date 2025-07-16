using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class AllDoubleToThicknessMultiConverter : IMultiValueConverter
{
    public object Convert( object[ ] _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        return ( object ) new Thickness( _param1.OfType<double>().Sum() );
    }

    public object[ ] ConvertBack( object _param1, Type[ ] _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
