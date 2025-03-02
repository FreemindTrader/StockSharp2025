// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyExecutionModes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Web.DomainModel
{
    [Flags]
    public enum StrategyExecutionModes
    {
        [Display(Name = "No", ResourceType = typeof (LocalizedStrings))] None = 0,
        [Display(Name = "Backtest", ResourceType = typeof (LocalizedStrings))] Backtest = 1,
        [Display(Name = "Optimization", ResourceType = typeof (LocalizedStrings))] Optimization = 2,
        [Display(Name = "Live", ResourceType = typeof (LocalizedStrings))] Live = 4,
    }
}
