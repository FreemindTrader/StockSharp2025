// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductScopes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Web.DomainModel;

[DataContract]
public enum ProductScopes
{
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Free")] Public,
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Paid")] Restricted,
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Private")] Private,
}
