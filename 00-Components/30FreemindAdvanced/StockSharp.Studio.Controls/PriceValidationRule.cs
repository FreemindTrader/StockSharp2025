// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PriceValidationRule
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Globalization;
using System.Windows.Controls;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Studio.Controls;

internal class PriceValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string incorrectLimitOrderPrice = LocalizedStrings.IncorrectLimitOrderPrice;
        if (!(value is Decimal num))
            return new ValidationResult(false, (object)incorrectLimitOrderPrice);
        return !(num <= 0M) ? ValidationResult.ValidResult : new ValidationResult(false, (object)incorrectLimitOrderPrice);
    }
}
