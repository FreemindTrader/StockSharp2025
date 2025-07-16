using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class AxisAlignmentToAxisOrientationConverter :
  IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        AxisAlignment demydmpA2K68QEjd = (AxisAlignment) _param1;
        bool flag = _param3 is string;
        return ( object ) ( Orientation ) ( demydmpA2K68QEjd == AxisAlignment.Left || demydmpA2K68QEjd == AxisAlignment.Right ? ( flag ? 0 : 1 ) : ( flag ? 1 : 0 ) );
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
