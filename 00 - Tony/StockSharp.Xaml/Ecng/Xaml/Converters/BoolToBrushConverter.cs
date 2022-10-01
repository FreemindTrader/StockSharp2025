using Ecng.Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ecng.Xaml.Converters
{
    /// <summary>
    /// <see cref="T:System.Boolean" /> to <see cref="T:System.Windows.Media.Brush" /> converter.
    ///     </summary>
    public class BoolToBrushConverter : IValueConverter
    {

        private Brush _trueValueBrush = (Brush) new SolidColorBrush( Colors.Transparent);

        private Brush _falseValueBrush = (Brush) new SolidColorBrush( Colors.Transparent);

        /// <summary>
        /// <see langword="true" /> value.
        ///     </summary>
        public Brush TrueValue
        {
            get
            {
                return this._trueValueBrush;
            }
            set
            {
                this._trueValueBrush = value;
            }
        }

        /// <summary>
        /// <see langword="false" /> value.
        ///     </summary>
        public Brush FalseValue
        {
            get
            {
                return this._falseValueBrush;
            }
            set
            {
                this._falseValueBrush = value;
            }
        }

        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            bool flag = parameter == null || parameter.To<bool>();
            if ( ( bool )value != flag )
                return ( object )this.FalseValue;
            return ( object )this.TrueValue;
        }

        object IValueConverter.ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
        {
            throw new NotSupportedException();
        }
    }
}
