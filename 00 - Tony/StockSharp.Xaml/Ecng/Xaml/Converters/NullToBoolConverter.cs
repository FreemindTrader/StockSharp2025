using Ecng.Common;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// <see langword="null" /> (or <see cref="F:System.String.Empty" />) to <see cref="T:System.Boolean" /> converter.
    ///     </summary>
    public class NullToBoolConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == null )
                return ( object )false;
            return ( object )!StringHelper.IsEmptyOrWhiteSpace( ( string )Converter.To<string>( value ) );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
