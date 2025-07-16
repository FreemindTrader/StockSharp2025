using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class AxisAlignmentToHorizontalAlignmentConverter : IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        AxisAlignment obj = (AxisAlignment) _param1;
        HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;
        switch ( obj )
        {
            case AxisAlignment.Right:
                horizontalAlignment = HorizontalAlignment.Right;
                break;
            case AxisAlignment.Left:
                horizontalAlignment = HorizontalAlignment.Left;
                break;
            case AxisAlignment.Top:
            case AxisAlignment.Bottom:
                horizontalAlignment = HorizontalAlignment.Center;
                break;
        }
        return ( object ) horizontalAlignment;
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
