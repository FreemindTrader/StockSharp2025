// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.ProductContentTypes2
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Web.DomainModel;

[DataContract]
[Flags]
public enum ProductContentTypes2 : long
{
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "SourceCode")] SourceCode = 1,
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Dll")] CompiledAssembly = 2,
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Schema")] Schema = 4,
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "EncryptedSchema"), Obsolete] EncryptedSchema = 8,
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "StandaloneApp")] StandaloneApp = 16, // 0x0000000000000010
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Indicator"), Obsolete] Indicator = 32, // 0x0000000000000020
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Connector"), Obsolete] Connector = 64, // 0x0000000000000040
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Stock"), Obsolete] StockConnector = 128, // 0x0000000000000080
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "DiagramElement"), Obsolete] DiagramElement = 256, // 0x0000000000000100
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Other")] Other = 512, // 0x0000000000000200
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Video"), Obsolete] Video = 1024, // 0x0000000000000400
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Support"), Obsolete] Support = 2048, // 0x0000000000000800
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Development"), Obsolete] Development = 4096, // 0x0000000000001000
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Account"), Obsolete] Account = 8192, // 0x0000000000002000
    [EnumMember, Display(ResourceType = typeof(LocalizedStrings), Name = "Freelance"), Obsolete] Freelance = 16384, // 0x0000000000004000
}
