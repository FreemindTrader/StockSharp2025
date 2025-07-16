using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class addEllipsisConv : IValueConverter
{
    object IValueConverter.Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        string str = _param1 as string;
        return StringHelper.IsEmpty( str ) ? _param1 : ( object ) ( str + "..." );
    }

    object IValueConverter.ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotSupportedException();
    }
}
