using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class AxisAlignmentToVeticalAnchorPointConverter :
  IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        AxisAlignment obj = (AxisAlignment) _param1;
        VerticalAnchorPoint output = VerticalAnchorPoint.Center;
        switch ( obj )
        {
            case AxisAlignment.Top:
            case AxisAlignment.Bottom:
                output = VerticalAnchorPoint.Top;
                break;
        }
        return ( object ) output;
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
