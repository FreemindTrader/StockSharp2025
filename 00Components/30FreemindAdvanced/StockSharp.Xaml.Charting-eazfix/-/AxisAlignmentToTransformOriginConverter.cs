using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class AxisAlignmentToTransformOriginConverter :
  IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        switch ( ( AxisAlignment ) _param1 )
        {
            case AxisAlignment.Right:
                return ( object ) new Point( 0.0, 0.0 );
            case AxisAlignment.Left:
                return ( object ) new Point( 1.0, 0.0 );
            case AxisAlignment.Top:
                return ( object ) new Point( 0.0, 1.0 );
            case AxisAlignment.Bottom:
                return ( object ) new Point( 0.0, 0.0 );
            default:
                return ( object ) new Point();
        }
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
