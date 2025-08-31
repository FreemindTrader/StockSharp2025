using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace StockSharp.Xaml.Charting;

/// <summary>
/// Converts stroke thickness to DataPointWidth
/// </summary>
public sealed class StrokeThickToPointWidthConverter : IValueConverter
{
    public object Convert(object _param1, Type _param2, object _param3, CultureInfo _param4)
    {
        return ( Converter.To<double>(_param1) / 10.0 );
    }

    public object ConvertBack(object _param1, Type _param2, object _param3, CultureInfo _param4)
    {
        throw new NotSupportedException();
    }
}
