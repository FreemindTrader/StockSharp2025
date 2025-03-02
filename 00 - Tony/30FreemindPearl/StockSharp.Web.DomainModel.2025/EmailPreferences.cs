// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.EmailPreferences
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using StockSharp.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace StockSharp.Web.DomainModel
{
    [Flags]
    public enum EmailPreferences : long
    {
        [Display(Description = "PromotionNewsletters", Name = "Promotion", ResourceType = typeof (LocalizedStrings))] Promo = 1,
        [Display(Description = "ArticlesNewsletters", Name = "Articles", ResourceType = typeof (LocalizedStrings))] Articles = 2,
        [Display(Description = "ReleasesNewsletters", Name = "Releases", ResourceType = typeof (LocalizedStrings))] Releases = 4,
        [Display(Description = "ForumNotifications", Name = "Forum", ResourceType = typeof (LocalizedStrings))] Forum = 8,
    }
}
