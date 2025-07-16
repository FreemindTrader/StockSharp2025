using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
namespace SciChart.Charting;

internal sealed class OrientationToAxisLabelRotationConverter : IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        Orientation orientation = (Orientation) _param1;
        return ( object ) new RotateTransform()
        {
            Angle = ( orientation == Orientation.Horizontal ? 0.0 : -90.0 )
        };
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
