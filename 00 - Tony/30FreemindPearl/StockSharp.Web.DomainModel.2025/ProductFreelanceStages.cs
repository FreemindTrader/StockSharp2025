// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductFreelanceStages
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Web.DomainModel
{
    public enum ProductFreelanceStages
    {
        [Display(Name = "None", ResourceType = typeof (LocalizedStrings))] None,
        [Display(Name = "Declared", ResourceType = typeof (LocalizedStrings))] MoneyDeclared,
        [Display(Name = "Selected", ResourceType = typeof (LocalizedStrings))] ExecutorSelected,
        [Display(Name = "Reserved", ResourceType = typeof (LocalizedStrings))] MoneyReserved,
        [Display(Name = "Started", ResourceType = typeof (LocalizedStrings))] ExecutorStarted,
        [Display(Name = "Finished", ResourceType = typeof (LocalizedStrings))] ExecutorFinished,
        [Display(Name = "Paid", ResourceType = typeof (LocalizedStrings))] ExecutorPaid,
        [Display(Name = "RequestRefund", ResourceType = typeof (LocalizedStrings))] RefundRequested,
        [Display(Name = "Refund", ResourceType = typeof (LocalizedStrings))] Refunded,
    }
}
