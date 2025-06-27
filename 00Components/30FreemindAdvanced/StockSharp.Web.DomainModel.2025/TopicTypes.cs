// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.TopicTypes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using StockSharp.Localization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Web.DomainModel
{
    public enum TopicTypes
    {
        [Display(Name = "Article", ResourceType = typeof (LocalizedStrings))] Article = 0,
        [Display(Name = "Page", ResourceType = typeof (LocalizedStrings))] Page = 1,
        [Display(Name = "News", ResourceType = typeof (LocalizedStrings))] News = 2,
        [Display(Name = "Email", ResourceType = typeof (LocalizedStrings))] Email = 5,
        [Display(Name = "Private", ResourceType = typeof (LocalizedStrings))] PrivateMessage = 7,
        [Display(Name = "Support", ResourceType = typeof (LocalizedStrings))] Support = 8,
        [Display(Name = "Forum", ResourceType = typeof (LocalizedStrings))] Forum = 9,
        [Display(Name = "Profile", ResourceType = typeof (LocalizedStrings))] PublicDescription = 10, // 0x0000000A
        [Display(Name = "Description", ResourceType = typeof (LocalizedStrings))] PrivateDescription = 11, // 0x0000000B
        [Display(Name = "Product", ResourceType = typeof (LocalizedStrings))] Product = 12, // 0x0000000C
        [Browsable(false)] Community = 13, // 0x0000000D
        [Browsable(false)] Blog = 14, // 0x0000000E
    }
}
