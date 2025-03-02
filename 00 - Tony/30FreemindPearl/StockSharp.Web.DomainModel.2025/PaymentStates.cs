// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.PaymentStates
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Web.DomainModel
{
    public enum PaymentStates
    {
        [Display(Name = "Done", ResourceType = typeof (LocalizedStrings))] Done,
        [Display(Name = "Active", ResourceType = typeof (LocalizedStrings))] Active,
        [Display(Name = "Error", ResourceType = typeof (LocalizedStrings))] Error,
        [Display(Name = "Approved", ResourceType = typeof (LocalizedStrings))] Approved,
        [Display(Name = "Cancelled", ResourceType = typeof (LocalizedStrings))] Cancelled,
    }
}
