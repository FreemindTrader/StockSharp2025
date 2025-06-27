// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductOrderFlags
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Web.DomainModel
{
    [Flags]
    public enum ProductOrderFlags
    {
        [Display(Name = "None", ResourceType = typeof (LocalizedStrings))] None = 0,
        [Display(Name = "Trial", ResourceType = typeof (LocalizedStrings))] Trial = 1,
        [Display(Name = "Refund", ResourceType = typeof (LocalizedStrings))] Refund = 2,
        [Display(Name = "Demo", ResourceType = typeof (LocalizedStrings))] Test = 4,
        [Display(Name = "Cancelled", ResourceType = typeof (LocalizedStrings))] Cancelled = 8,
    }
}
