// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyCancelReasons
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.ComponentModel.DataAnnotations;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public enum StrategyCancelReasons
{
    [Display(ResourceType = typeof(LocalizedStrings), Name = "User")] User,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "TimeOut")] Timeout,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "System")] System,
}
