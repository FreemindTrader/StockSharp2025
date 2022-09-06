// Decompiled with JetBrains decompiler
// Type: StockSharp.Diagram.Elements.BlackScholesGreeks
// Assembly: StockSharp.Diagram.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 360B6A39-6C02-41BD-7BF8-8057191DCB82
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Diagram.Core.dll

using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Diagram.Elements
{
  /// <summary>Black-Scholes "greeks".</summary>
  public enum BlackScholesGreeks
  {
    /// <summary>Delta.</summary>
    [Display(Name = "Delta", ResourceType = typeof (LocalizedStrings))] Delta,
    /// <summary>Gamma.</summary>
    [Display(Name = "Gamma", ResourceType = typeof (LocalizedStrings))] Gamma,
    /// <summary>Vega.</summary>
    [Display(Name = "Vega", ResourceType = typeof (LocalizedStrings))] Vega,
    /// <summary>Theta.</summary>
    [Display(Name = "Theta", ResourceType = typeof (LocalizedStrings))] Theta,
    /// <summary>Rho.</summary>
    [Display(Name = "Rho", ResourceType = typeof (LocalizedStrings))] Rho,
  }
}
