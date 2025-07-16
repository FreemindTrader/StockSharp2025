using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace SciChart.Charting;

internal sealed class NullToVisibilityConverter : IValueConverter
{
    public object Convert( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        int num = string.Equals(_param3 as string, "INVERSE", StringComparison.InvariantCultureIgnoreCase) ? 1 : 0;
        Visibility visibility1 = num != 0 ? Visibility.Visible : Visibility.Collapsed;
        Visibility visibility2 = num != 0 ? Visibility.Collapsed : Visibility.Visible;
        return ( object ) ( Visibility ) ( _param1 == null ? ( int ) visibility1 : ( int ) visibility2 );
    }

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
