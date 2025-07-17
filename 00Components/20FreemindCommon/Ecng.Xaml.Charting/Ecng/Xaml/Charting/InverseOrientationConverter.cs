using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ecng.Xaml.Charting;

/// <summary>
///
/// </summary>
public class InverseOrientationConverter : IValueConverter
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
        return ( object ) ( Orientation ) ( ( Orientation ) value == Orientation.Horizontal ? 1 : 0 );
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
        throw new NotImplementedException();
    }
}
