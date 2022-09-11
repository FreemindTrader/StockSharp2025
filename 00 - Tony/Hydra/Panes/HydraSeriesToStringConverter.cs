using StockSharp.Localization;
using StockSharp.Xaml.Converters;
using System;
using System.Globalization;

namespace StockSharp.Hydra.Panes
{
    internal class HydraSeriesToStringConverter : SeriesToStringConverter
    {
        public override object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            Messages.DataType msg = ( Messages.DataType )value;

            if ( msg == TaskPane.Custom )
                return LocalizedStrings.Custom;

            return base.Convert( value, targetType, parameter, culture );
        }
    }
}
