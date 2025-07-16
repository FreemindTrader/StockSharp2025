using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class AxisAlignmentToFlowDirectionConverter :
  IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        return ( object ) ( FlowDirection ) ( ( AxisAlignment ) _param1 == AxisAlignment.Left ? 1 : 0 );
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
