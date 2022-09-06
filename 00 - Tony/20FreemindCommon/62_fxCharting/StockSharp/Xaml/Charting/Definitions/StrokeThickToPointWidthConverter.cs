using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;

internal sealed class StrokeThickToPointWidthConverter : IValueConverter
{
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
        return value.To<double>( ) / 10.0;
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
        throw new NotSupportedException( );
    }
}
