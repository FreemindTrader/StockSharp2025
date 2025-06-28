// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductFreelanceStages
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.ComponentModel.DataAnnotations;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public enum ProductFreelanceStages
{
    [Display(ResourceType = typeof(LocalizedStrings), Name = "None")] None,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Declared")] MoneyDeclared,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Selected")] ExecutorSelected,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Reserved")] MoneyReserved,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Started")] ExecutorStarted,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Finished")] ExecutorFinished,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Paid")] ExecutorPaid,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "RequestRefund")] RefundRequested,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Refund")] Refunded,
}
