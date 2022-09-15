// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.EndPointValidationRule
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using Ecng.Common;
using MoreLinq;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Windows.Controls;

namespace Ecng.Xaml
{
  /// <summary>
  /// <see cref="T:System.Windows.Controls.ValidationRule" /> for <see cref="T:System.Net.EndPoint" />.
  ///     </summary>
  public class EndPointValidationRule : ValidationRule
  {
    
    private bool \u0023\u003Dzap5dxRpaXSQLmrpNEQ\u003D\u003D;

    /// <summary>Is a passed value represents multiple end-points.</summary>
    public bool Multi
    {
      get
      {
        return this.\u0023\u003Dzap5dxRpaXSQLmrpNEQ\u003D\u003D;
      }
      set
      {
        this.\u0023\u003Dzap5dxRpaXSQLmrpNEQ\u003D\u003D = value;
      }
    }

    /// <inheritdoc />
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
      if (value == null)
        return new ValidationResult(false, (object) LocalizedStrings.Str1459);
      try
      {
        if (this.Multi)
          ((IEnumerable<string>) value.To<string>().SplitBySep(nameof(2127280360), true)).ForEach<string>(EndPointValidationRule.SomeShit.\u0023\u003DzVwwelgNQxpkuB0q29w\u003D\u003D ?? (EndPointValidationRule.SomeShit.\u0023\u003DzVwwelgNQxpkuB0q29w\u003D\u003D = new Action<string>(EndPointValidationRule.SomeShit.ShitMethod02.\u0023\u003Dz\u0024G4Y5qzPeoYB\u00247IbHg\u003D\u003D)));
        else
          value.To<EndPoint>();
        return ValidationResult.ValidResult;
      }
      catch (Exception ex)
      {
        return new ValidationResult(false, (object) LocalizedStrings.Str1459);
      }
    }

    [Serializable]
    private sealed class SomeShit
    {
      public static readonly EndPointValidationRule.SomeShit ShitMethod02 = new EndPointValidationRule.SomeShit();
      public static Action<string> \u0023\u003DzVwwelgNQxpkuB0q29w\u003D\u003D;

      internal void \u0023\u003Dz\u0024G4Y5qzPeoYB\u00247IbHg\u003D\u003D(string _param1)
      {
        _param1.To<EndPoint>();
      }
    }
  }
}
