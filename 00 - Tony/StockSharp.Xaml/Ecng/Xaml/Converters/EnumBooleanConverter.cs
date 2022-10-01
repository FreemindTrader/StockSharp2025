using Ecng.Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// <see cref="T:System.Enum" /> to <see cref="T:System.Boolean" /> converter.
    ///     </summary>
    public class EnumBooleanConverter : IValueConverter
    {
        public bool DefaultValueWhenUnchecked { get; set; }

        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            string str;
            if ( ( str = parameter as string ) == null || !Enum.IsDefined( value.GetType(), value ) )
            {
                return DependencyProperty.UnsetValue;
            }

            return ( object )str.To( value.GetType() ).Equals( value );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            string str;
            if ( ( str = parameter as string ) == null )
            {
                return DependencyProperty.UnsetValue;
            }

            if ( this.DefaultValueWhenUnchecked && !( bool )value )
            {
                return targetType.GetValues().ElementAt<object>( 0 );
            }

            return str.To( targetType );
        }
    }
}