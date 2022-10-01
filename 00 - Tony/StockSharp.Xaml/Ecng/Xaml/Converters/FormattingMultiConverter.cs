using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>String formatting converter.</summary>
    public class FormattingMultiConverter : IMultiValueConverter
    {
        /// <summary>A composite format string.</summary>
        public string FormatString;

        object IMultiValueConverter.Convert( object[ ] value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( parameter != null && !( parameter is string ) || value.Any<object>( p => p == DependencyProperty.UnsetValue ) )
            {
                return Binding.DoNothing;
            }
        
            string format = parameter as string;
            
            if ( StringHelper.IsEmpty( format ) )
            {
                format = this.FormatString;
            }
                
            return ( object )string.Format( ( IFormatProvider )culture, format, value );
        }

        object[ ] IMultiValueConverter.ConvertBack( object value, Type[ ] targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }        
    }
}
