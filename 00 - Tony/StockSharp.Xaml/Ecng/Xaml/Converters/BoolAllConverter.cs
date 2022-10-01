using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// Set of <see cref="T:System.Boolean" /> (all) check on <see cref="P:Ecng.Xaml.Converters.BoolAllConverter.Value" /> equality converter.
    /// </summary>
    public class BoolAllConverter : IMultiValueConverter
    {

        private bool _value = true;

        /// <summary>Value.</summary>
        public bool Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        object IMultiValueConverter.Convert( object[ ] value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object )( ( IEnumerable<object> )value ).All<object>( new Func<object, bool>( this.IsBoolEqual ) );
        }

        object[ ] IMultiValueConverter.ConvertBack( object value, Type[ ] targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }

        private bool IsBoolEqual( object o )
        {
            if ( o is bool )
                return ( bool )o == this.Value;
            return false;
        }
    }
}
