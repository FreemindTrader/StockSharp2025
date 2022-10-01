using Ecng.Common;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Ecng.Xaml.Converters
{
    public class MultiBoolToOpacityConverter : IMultiValueConverter
    {
        public MultiBoolToOpacityConverter()
        {
            this.FalseOpacityValue = 1.0;
            this.TrueOpacityValue = 0.5;
        }

        public double TrueOpacityValue { get; set; }

        public double FalseOpacityValue { get; set; }

        object IMultiValueConverter.Convert( object[ ] values, Type targetType, object parameter, CultureInfo culture )
        {
            bool flag = parameter == null || parameter.To<bool>();
            return ( object )( values.OfType<bool>().All<bool>( ( Func<bool, bool> )( v => v ) ) == flag ? this.TrueOpacityValue : this.FalseOpacityValue );
        }

        object[ ] IMultiValueConverter.ConvertBack( object value, Type[ ] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException();
        }
    }
}
