// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.DataTypes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.ComponentModel.DataAnnotations;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public enum DataTypes
{
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Candles")] TF,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Ticks")] Ticks,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Level1")] Level1,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "MarketDepth")] MarketDepth,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "OrderLog")] OrderLog,
}
