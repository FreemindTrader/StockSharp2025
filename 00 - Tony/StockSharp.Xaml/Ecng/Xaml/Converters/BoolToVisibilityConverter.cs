using Ecng.Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// <see cref="T:System.Boolean" /> to <see cref="T:System.Windows.Visibility" /> converter.
    ///     </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        
        private Visibility _falseValueVisibility = Visibility.Collapsed;
        
        private Visibility _trueValueVisibility;

        /// <summary>
        /// </summary>
        public Visibility TrueValue
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
        /// </summary>
        public Visibility FalseValue
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
            bool boolResult = parameter == null || Converter.To<bool>( parameter ) ;
            return ( object )( Visibility )( ( bool )value == boolResult ? ( int )this.TrueValue : ( int )this.FalseValue );
        }

        object IValueConverter.ConvertBack( object value, Type _param2, object _param3, CultureInfo _param4 )
        {
            return ( object )( ( Visibility )value == this.TrueValue );
        }
    }
}
