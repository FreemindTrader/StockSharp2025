using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class ThicknessToDoubleConverter : IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        Thickness thickness = (Thickness) _param1;
        switch ( ( string ) _param3 )
        {
            case "Top":
                return ( object ) thickness.Top;
            case "Bottom":
                return ( object ) thickness.Bottom;
            case "Left":
                return ( object ) thickness.Left;
            case "Right":
                return ( object ) thickness.Right;
            default:
                return ( object ) ( ( thickness.Left + thickness.Right + thickness.Top + thickness.Bottom ) / 4.0 );
        }
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
