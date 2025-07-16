using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class HorisontalOrientationVisibilityConverter :
  IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        return ( object ) ( Visibility ) ( ( Orientation ) _param1 == Orientation.Horizontal ? 0 : 2 );
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
