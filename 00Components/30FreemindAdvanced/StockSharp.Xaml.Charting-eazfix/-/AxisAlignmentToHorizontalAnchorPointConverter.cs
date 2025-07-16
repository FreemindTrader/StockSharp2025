
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace StockSharp.Charting;

public sealed class AxisAlignmentToHorizontalAnchorPointConverter :
  IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        AxisAlignment demydmpA2K68QEjd = (AxisAlignment) _param1;
        HorizontalAnchorPoint awnnsejcnkX3MwEjd = HorizontalAnchorPoint.Left;
        switch ( demydmpA2K68QEjd )
        {
            case AxisAlignment.Top:
            case AxisAlignment.Bottom:
                awnnsejcnkX3MwEjd = HorizontalAnchorPoint.Center;
                break;
        }
        return ( object ) awnnsejcnkX3MwEjd;
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
