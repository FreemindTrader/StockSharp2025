// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PaymentStates
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.ComponentModel.DataAnnotations;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public enum PaymentStates
{
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Done")] Done,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Active")] Active,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Error")] Error,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Approved")] Approved,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Cancelled")] Cancelled,
}
