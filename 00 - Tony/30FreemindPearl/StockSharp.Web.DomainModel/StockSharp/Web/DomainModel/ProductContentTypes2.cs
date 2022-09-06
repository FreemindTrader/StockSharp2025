
using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StockSharp.Web.DomainModel
{
    [DataContract]
    [Flags]
    public enum ProductContentTypes2 : long
    {
        [EnumMember, Display(Name = "Str3180", ResourceType = typeof(LocalizedStrings))] SourceCode = 1,
        [EnumMember, Display(Name = "Str3182", ResourceType = typeof(LocalizedStrings))] CompiledAssembly = 2,
        [EnumMember, Display(Name = "Schema", ResourceType = typeof(LocalizedStrings))] Schema = 4,
        [EnumMember, Display(Name = "EncryptedSchema", ResourceType = typeof(LocalizedStrings))] EncryptedSchema = 8,
        [EnumMember, Display(Name = "StandaloneApp", ResourceType = typeof(LocalizedStrings))] StandaloneApp = 16, // 0x0000000000000010
        [EnumMember, Display(Name = "Str1981", ResourceType = typeof(LocalizedStrings))] Indicator = 32, // 0x0000000000000020
        [EnumMember, Display(Name = "Crypto", ResourceType = typeof(LocalizedStrings))] CryptoConnector = 64, // 0x0000000000000040
        [EnumMember, Display(Name = "Stock", ResourceType = typeof(LocalizedStrings))] StockConnector = 128, // 0x0000000000000080
        [EnumMember, Display(Name = "DiagramElement", ResourceType = typeof(LocalizedStrings))] DiagramElement = 256, // 0x0000000000000100
        [EnumMember, Display(Name = "Str2381", ResourceType = typeof(LocalizedStrings))] Other = 512, // 0x0000000000000200
        [EnumMember, Display(Name = "Video", ResourceType = typeof(LocalizedStrings))] Video = 1024, // 0x0000000000000400
        [EnumMember, Display(Name = "Support", ResourceType = typeof(LocalizedStrings))] Support = 2048, // 0x0000000000000800
        [EnumMember, Display(Name = "Development", ResourceType = typeof(LocalizedStrings))] Development = 4096, // 0x0000000000001000
        [EnumMember, Display(Name = "Account", ResourceType = typeof(LocalizedStrings))] Account = 8192, // 0x0000000000002000
        [EnumMember, Display(Name = "Freelance", ResourceType = typeof(LocalizedStrings))] Freelance = 16384, // 0x0000000000004000
    }
}
