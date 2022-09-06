// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.OptionsQuotingTypes
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Options quoting types.</summary>
  public enum OptionsQuotingTypes
  {
    /// <summary>Option volatility quoting.</summary>
    [Display(Name = "Volatility", ResourceType = typeof (LocalizedStrings))] Volalitity,
    /// <summary>Option theoretical price quoting.</summary>
    [Display(Name = "Str294", ResourceType = typeof (LocalizedStrings))] TheorPrice,
  }
}
