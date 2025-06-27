// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DataTypes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Web.DomainModel
{
    public enum DataTypes
    {
        [Display(Name = "Candles", ResourceType = typeof (LocalizedStrings))] TF,
        [Display(Name = "Ticks", ResourceType = typeof (LocalizedStrings))] Ticks,
        [Display(Name = "Level1", ResourceType = typeof (LocalizedStrings))] Level1,
        [Display(Name = "MarketDepth", ResourceType = typeof (LocalizedStrings))] MarketDepth,
        [Display(Name = "OrderLog", ResourceType = typeof (LocalizedStrings))] OrderLog,
    }
}
