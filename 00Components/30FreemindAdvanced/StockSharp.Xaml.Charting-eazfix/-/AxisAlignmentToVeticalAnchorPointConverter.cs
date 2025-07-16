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
        AxisAlignment demydmpA2K68QEjd = (AxisAlignment) _param1;
        VerticalAnchorPoint r852JmG36S4EnausEjd = VerticalAnchorPoint.Center;
        switch ( demydmpA2K68QEjd )
        {
            case AxisAlignment.Top:
            case AxisAlignment.Bottom:
                r852JmG36S4EnausEjd = VerticalAnchorPoint.Top;
                break;
        }
        return ( object ) r852JmG36S4EnausEjd;
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
