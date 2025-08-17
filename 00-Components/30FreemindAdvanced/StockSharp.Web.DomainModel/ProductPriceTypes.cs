// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductPriceTypes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Web.DomainModel;

[DataContract]
public enum ProductPriceTypes
{
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Lifetime")] Lifetime,
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "PerMonth")] PerMonth,
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Annual")] Annual,
}
