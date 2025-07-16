using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class AxisAlignmentToVerticalAlignmentConverter :
  IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        AxisAlignment demydmpA2K68QEjd = (AxisAlignment) _param1;
        VerticalAlignment verticalAlignment = VerticalAlignment.Top;
        switch ( demydmpA2K68QEjd )
        {
            case AxisAlignment.Right:
            case AxisAlignment.Left:
                verticalAlignment = VerticalAlignment.Center;
                break;
            case AxisAlignment.Top:
                verticalAlignment = VerticalAlignment.Top;
                break;
            case AxisAlignment.Bottom:
                verticalAlignment = VerticalAlignment.Bottom;
                break;
        }
        return ( object ) verticalAlignment;
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
