using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>Concat values into single string converter.</summary>
    public class ConcatMultiValueConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert( object[ ] value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object )StringHelper.Join( ( ( IEnumerable<object> )value ).Select<object, string>( p => Converter.To<string>( p ) ), "," );
        }

        object[ ] IMultiValueConverter.ConvertBack( object _param1, Type[ ] _param2, object _param3, CultureInfo _param4 )
        {
            throw new NotSupportedException();
        }        
    }
}
