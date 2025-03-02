// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductPriceTypes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StockSharp.Web.DomainModel
{
    [DataContract]
    public enum ProductPriceTypes
    {
        [EnumMember, Display(Name = "Lifetime", ResourceType = typeof (LocalizedStrings))] Lifetime,
        [EnumMember, Display(Name = "PerMonth", ResourceType = typeof (LocalizedStrings))] PerMonth,
        [EnumMember, Display(Name = "Annual", ResourceType = typeof (LocalizedStrings))] Annual,
    }
}
