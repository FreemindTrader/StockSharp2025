
using StockSharp.Localization;
using System;
using System.Globalization;
using System.Windows.Controls;

namespace StockSharp.Studio.Controls
{
    internal class PriceValidationRule : ValidationRule
    {
        public override ValidationResult Validate( object value, CultureInfo cultureInfo )
        {
            string str3273 = LocalizedStrings.Str3273;
            if ( !( value is Decimal ) )
                return new ValidationResult( false, ( object )str3273 );
            if ( !( ( Decimal )value <= Decimal.Zero ) )
                return ValidationResult.ValidResult;
            return new ValidationResult( false, ( object )str3273 );
        }
    }
}
