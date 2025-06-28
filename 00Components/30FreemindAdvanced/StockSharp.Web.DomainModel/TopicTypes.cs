// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.TopicTypes
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using StockSharp.Localization;

#nullable disable
namespace StockSharp.Web.DomainModel;

public enum TopicTypes
{
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Article")] Article = 0,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Page")] Page = 1,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "News")] News = 2,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Email")] Email = 5,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Private")] PrivateMessage = 7,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Support")] Support = 8,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Forum")] Forum = 9,
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Profile")] PublicDescription = 10, // 0x0000000A
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Description")] PrivateDescription = 11, // 0x0000000B
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Product")] Product = 12, // 0x0000000C
    [Browsable(false)] Community = 13, // 0x0000000D
    [Browsable(false)] Blog = 14, // 0x0000000E
}
