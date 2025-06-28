// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductOrderFlags
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using System.ComponentModel.DataAnnotations;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Web.DomainModel;

[Flags]
public enum ProductOrderFlags
{
    [Display(ResourceType = typeof(LocalizedStrings), Name = "None")] None = 0,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Trial")] Trial = 1,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Refund")] Refund = 2,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Demo")] Test = 4,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Cancelled")] Cancelled = 8,
}
