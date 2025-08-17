// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyExecutionModes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using System.ComponentModel.DataAnnotations;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Web.DomainModel;

[Flags]
public enum StrategyExecutionModes
{
    [Display(ResourceType = typeof(LocalizedStrings), Name = "No")] None = 0,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Backtest")] Backtest = 1,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Optimization")] Optimization = 2,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Live")] Live = 4,
}
