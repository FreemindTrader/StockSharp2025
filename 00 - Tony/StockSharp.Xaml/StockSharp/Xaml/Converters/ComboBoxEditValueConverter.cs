using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace StockSharp.Xaml.Converters
{
    public sealed class ComboBoxEditValueConverter<T> : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object ) ( ( IEnumerable<T> ) value ).Select( x => (object) x ).ToList<object>();
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == null )
                return ( object ) ArrayHelper.Empty<T>();
            return ( object ) ( ( IEnumerable<object> ) value ).Select( x => ( T ) x ).ToArray<T>();
        }        
    }
}
