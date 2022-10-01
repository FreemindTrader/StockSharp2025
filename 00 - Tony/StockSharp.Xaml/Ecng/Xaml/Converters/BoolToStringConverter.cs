using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// <see cref="T:System.Boolean" /> to <see cref="T:System.String" /> converter.
    ///     </summary>
    public class BoolToStringConverter : IValueConverter
    {
        
        private string _trueValueVisibility;
        
        private string _falseValueVisibility;

        /// <summary>
        /// <see langword="true" /> value.
        ///     </summary>
        public string TrueValue
        {
            get
            {
                return this._trueValueVisibility;
            }
            set
            {
                this._trueValueVisibility = value;
            }
        }

        /// <summary>
        /// <see langword="false" /> value.
        ///     </summary>
        public string FalseValue
        {
            get
            {
                return this._falseValueVisibility;
            }
            set
            {
                this._falseValueVisibility = value;
            }
        }

        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            bool? boolValue = value as bool?;

            if ( !( boolValue.GetValueOrDefault() & boolValue.HasValue ) )
            {
                return ( object )this.FalseValue;
            }
                
            return ( object )this.TrueValue;
        }

        object IValueConverter.ConvertBack(
          object _param1,
          Type _param2,
          object _param3,
          CultureInfo _param4 )
        {
            return ( object )( ( string )_param1 == this.TrueValue );
        }
    }
}
