using Ecng.ComponentModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// <see cref="T:System.Enum" /> to display name converter.
    ///     </summary>
    public class EnumDisplayNameConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == DependencyProperty.UnsetValue )
            {
                return Binding.DoNothing;
            }

            if ( value == null )
            {
                return ( object )string.Empty;
            }

            if ( !( value is Enum ) )
            {
                return Binding.DoNothing;
            }

            return ( object )value.GetDisplayName();
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
