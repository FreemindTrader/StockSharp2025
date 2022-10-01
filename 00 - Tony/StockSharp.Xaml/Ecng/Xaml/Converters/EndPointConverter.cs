using Ecng.Common;
using System;
using System.Globalization;
using System.Net;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// <see cref="T:System.Net.EndPoint" /> to <see cref="T:System.String" /> converter.
    ///     </summary>
    public class EndPointConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object )value.To<string>();
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object )value.To<EndPoint>();
        }
    }
}
