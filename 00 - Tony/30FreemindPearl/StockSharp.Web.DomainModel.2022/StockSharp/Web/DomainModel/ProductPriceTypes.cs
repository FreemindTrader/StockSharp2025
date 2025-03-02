
using StockSharp.Localization;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StockSharp.Web.DomainModel
{
    [DataContract]
    public enum ProductPriceTypes
    {
        [EnumMember, Display(Name = "Lifetime", ResourceType = typeof(LocalizedStrings))] Lifetime,
        [EnumMember, Display(Name = "PerMonth", ResourceType = typeof(LocalizedStrings))] PerMonth,
        [EnumMember, Display(Name = "Annual", ResourceType = typeof(LocalizedStrings))] Annual,
    }
}
