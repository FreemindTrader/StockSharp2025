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
    
    private bool _myBool;

    /// <summary>Is a passed value represents multiple end-points.</summary>
    public bool Multi
    {
      get
      {
        return this._myBool;
      }
      set
      {
        this._myBool = value;
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
          ((IEnumerable<string>) StringHelper.SplitBySep((string) Converter.To<string>(value), nameof(2127280360), true)).ForEach<string>(EndPointValidationRule.Lamdba0003._myAction ?? (EndPointValidationRule.Lamdba0003._myAction = new Action<string>(EndPointValidationRule.Lamdba0003._this.LDM07)));
        else
          Converter.To<EndPoint>(value);
        return ValidationResult.ValidResult;
      }
      catch (Exception ex)
      {
        return new ValidationResult(false, (object) LocalizedStrings.Str1459);
      }
    }

    [Serializable]
    private sealed class Lamdba0003
    {
      public static readonly EndPointValidationRule.Lamdba0003 _this = new EndPointValidationRule.Lamdba0003();
      public static Action<string> _myAction;

      internal void LDM07(string _param1)
      {
        Converter.To<EndPoint>((object) _param1);
      }
    }
  }
}
