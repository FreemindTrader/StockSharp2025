
using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StockSharp.Web.DomainModel
{
    [DataContract]
    [Obsolete("Use ProductContentTypes2 enum.")]
    public enum ProductContentTypes
    {
        [EnumMember, Display(Name = "Str3180", ResourceType = typeof(LocalizedStrings))] SourceCode,
        [EnumMember, Display(Name = "Str3182", ResourceType = typeof(LocalizedStrings))] CompiledAssembly,
        [EnumMember, Display(Name = "Schema", ResourceType = typeof(LocalizedStrings))] Schema,
        [EnumMember, Display(Name = "EncryptedSchema", ResourceType = typeof(LocalizedStrings))] EncryptedSchema,
        [EnumMember, Display(Name = "StandaloneApp", ResourceType = typeof(LocalizedStrings))] StandaloneApp,
        [EnumMember, Display(Name = "Str1981", ResourceType = typeof(LocalizedStrings))] Indicator,
        [EnumMember, Display(Name = "Crypto", ResourceType = typeof(LocalizedStrings))] CryptoConnector,
        [EnumMember, Display(Name = "Stock", ResourceType = typeof(LocalizedStrings))] StockConnector,
        [EnumMember, Display(Name = "DiagramElement", ResourceType = typeof(LocalizedStrings))] DiagramElement,
        [EnumMember, Display(Name = "Str2381", ResourceType = typeof(LocalizedStrings))] Other,
        [EnumMember, Display(Name = "Video", ResourceType = typeof(LocalizedStrings))] Video,
        [EnumMember, Display(Name = "Support", ResourceType = typeof(LocalizedStrings))] Support,
        [EnumMember, Display(Name = "Development", ResourceType = typeof(LocalizedStrings))] Development,
        [EnumMember, Display(Name = "Account", ResourceType = typeof(LocalizedStrings))] Account,
        [EnumMember, Display(Name = "Freelance", ResourceType = typeof(LocalizedStrings))] Freelance,
    }
}
