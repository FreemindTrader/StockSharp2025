// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductContentTypes2
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

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
        [EnumMember, Display(Name = "SourceCode", ResourceType = typeof (LocalizedStrings))] SourceCode = 1,
        [EnumMember, Display(Name = "Dll", ResourceType = typeof (LocalizedStrings))] CompiledAssembly = 2,
        [EnumMember, Display(Name = "Schema", ResourceType = typeof (LocalizedStrings))] Schema = 4,
        [EnumMember, Display(Name = "EncryptedSchema", ResourceType = typeof (LocalizedStrings)), Obsolete] EncryptedSchema = 8,
        [EnumMember, Display(Name = "StandaloneApp", ResourceType = typeof (LocalizedStrings))] StandaloneApp = 16, // 0x0000000000000010
        [EnumMember, Display(Name = "Indicator", ResourceType = typeof (LocalizedStrings)), Obsolete] Indicator = 32, // 0x0000000000000020
        [EnumMember, Display(Name = "Connector", ResourceType = typeof (LocalizedStrings)), Obsolete] Connector = 64, // 0x0000000000000040
        [EnumMember, Display(Name = "Stock", ResourceType = typeof (LocalizedStrings)), Obsolete] StockConnector = 128, // 0x0000000000000080
        [EnumMember, Display(Name = "DiagramElement", ResourceType = typeof (LocalizedStrings)), Obsolete] DiagramElement = 256, // 0x0000000000000100
        [EnumMember, Display(Name = "Other", ResourceType = typeof (LocalizedStrings))] Other = 512, // 0x0000000000000200
        [EnumMember, Display(Name = "Video", ResourceType = typeof (LocalizedStrings)), Obsolete] Video = 1024, // 0x0000000000000400
        [EnumMember, Display(Name = "Support", ResourceType = typeof (LocalizedStrings)), Obsolete] Support = 2048, // 0x0000000000000800
        [EnumMember, Display(Name = "Development", ResourceType = typeof (LocalizedStrings)), Obsolete] Development = 4096, // 0x0000000000001000
        [EnumMember, Display(Name = "Account", ResourceType = typeof (LocalizedStrings)), Obsolete] Account = 8192, // 0x0000000000002000
        [EnumMember, Display(Name = "Freelance", ResourceType = typeof (LocalizedStrings)), Obsolete] Freelance = 16384, // 0x0000000000004000
    }
}
