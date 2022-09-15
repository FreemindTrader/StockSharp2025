
using Ecng.Common;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using System;
using System.Globalization;
using System.Windows.Data;

namespace StockSharp.Xaml
{
    [ValueConversion( typeof( TimeSpan ), typeof( string ) )]
    public class QuoteConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == null )
                return ( object ) LocalizedStrings.Str1567;
            Quote quote = (Quote) value;
            return ( object ) StringHelper.Put( LocalizedStrings.Str1568Params, new object[ 2 ] { quote.Price, quote.Volume } );
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return ( object ) null;
        }
    }
}
