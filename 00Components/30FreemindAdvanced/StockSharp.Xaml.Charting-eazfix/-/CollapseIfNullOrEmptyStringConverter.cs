using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class CollapseIfNullOrEmptyStringConverter :
  IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        bool flag = _param1 != null;
        if ( _param1 is string str )
            flag &= !string.IsNullOrEmpty( str );
        return ( object ) ( Visibility ) ( flag ? 0 : 2 );
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
