// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PriceValidationRule
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

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
            string incorrectLimitOrderPrice = LocalizedStrings.IncorrectLimitOrderPrice;
            if ( !( value is Decimal ) )
                return new ValidationResult( false,  incorrectLimitOrderPrice );
            if ( !( ( Decimal ) value <= Decimal.Zero ) )
                return ValidationResult.ValidResult;
            return new ValidationResult( false,  incorrectLimitOrderPrice );
        }
    }
}
