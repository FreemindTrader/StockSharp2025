// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.CommonExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Ecng.Common;
using Ecng.Net;
using Ecng.Serialization;
using SmartFormat;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public static class CommonExtensions
{
    public static SmartFormatter CreateEmailFormatter()
    {
        SmartFormatter defaultSmartFormat = Smart.CreateDefaultSmartFormat();
        defaultSmartFormat.Parser.Settings.DoubleBrace = true;
        return defaultSmartFormat;
    }

    public static bool IsImage(this File file)
    {
        return file != null ? NetworkHelper.IsImage(file.Name) : throw new ArgumentNullException(nameof(file));
    }

    public static TEntityDomain TryGetEntityDomain<TEntity, TEntityDomain>(
      this TEntity entity,
      long domainId,
      Func<TEntityDomain, bool> filter)
      where TEntity : class, IDomainsEntity<TEntityDomain>
      where TEntityDomain : BaseEntity, IDomainEntity
    {
        TEntityDomain[] items = TypeHelper.CheckOnNull<TEntity>(entity, nameof(entity)).Domains?.Items;
        TEntityDomain entityDomain1 = items != null ? ((IEnumerable<TEntityDomain>)items).FirstOrDefault<TEntityDomain>((Func<TEntityDomain, bool>)(d => d.Domain.Id == domainId && filter(d))) : default(TEntityDomain);
        if ((object)entityDomain1 != null)
            return entityDomain1;
        TEntityDomain entityDomain2 = items != null ? ((IEnumerable<TEntityDomain>)items).FirstOrDefault<TEntityDomain>(filter) : default(TEntityDomain);
        if ((object)entityDomain2 != null)
            return entityDomain2;
        return items == null ? default(TEntityDomain) : ((IEnumerable<TEntityDomain>)items).FirstOrDefault<TEntityDomain>();
    }

    public static DynamicPageDomain TryGetDomain(
      this DynamicPage page,
      long domainId,
      Func<DynamicPageDomain, bool> filter)
    {
        return page.TryGetEntityDomain<DynamicPage, DynamicPageDomain>(domainId, filter);
    }

    public static string GetUrlRelative(this DynamicPage page, long domainId)
    {
        return page.TryGetDomain(domainId, (Func<DynamicPageDomain, bool>)(d => !StringHelper.IsEmpty(d.UrlRelative)))?.UrlRelative;
    }

    public static string GetUrlPart(this DynamicPage page, long domainId)
    {
        return page.TryGetDomain(domainId, (Func<DynamicPageDomain, bool>)(d => !StringHelper.IsEmpty(d.UrlPart)))?.UrlPart;
    }

    public static DynamicMenuDomain TryGetDomain(
      this DynamicMenu menu,
      long domainId,
      Func<DynamicMenuDomain, bool> filter)
    {
        return menu.TryGetEntityDomain<DynamicMenu, DynamicMenuDomain>(domainId, filter);
    }

    public static string GetUrl(this DynamicMenu menu, long domainId)
    {
        return StringHelper.IsEmpty(menu.TryGetDomain(domainId, (Func<DynamicMenuDomain, bool>)(d => !StringHelper.IsEmpty(d.UrlAbsolute)))?.UrlAbsolute, menu.TryGetDomain(domainId, (Func<DynamicMenuDomain, bool>)(d => !StringHelper.IsEmpty(d.UrlRelative)))?.UrlRelative);
    }

    public static string GetName(this DynamicMenu menu, long domainId)
    {
        return menu.TryGetDomain(domainId, (Func<DynamicMenuDomain, bool>)(d => !StringHelper.IsEmpty(d.Name)))?.Name;
    }

    public static string GetDescription(this DynamicMenu menu, long domainId)
    {
        return menu.TryGetDomain(domainId, (Func<DynamicMenuDomain, bool>)(d => !StringHelper.IsEmpty(d.Description)))?.Description;
    }

    public static string GetRedirectUrl(this DynamicPage page, long domainId)
    {
        return page.TryGetDomain(domainId, (Func<DynamicPageDomain, bool>)(d => !StringHelper.IsEmpty(d.RedirectUrl)))?.RedirectUrl;
    }

    public static File GetFile(this DynamicPage page, long domainId)
    {
        return page.TryGetDomain(domainId, (Func<DynamicPageDomain, bool>)(d => d.File != null))?.File;
    }

    public static Topic GetContent(this DynamicPage page, long domainId)
    {
        return page.TryGetDomain(domainId, (Func<DynamicPageDomain, bool>)(d => d.Topic != null))?.Topic;
    }

    public static string GetActionTitle(this DynamicPage page, long domainId)
    {
        return page.TryGetDomain(domainId, (Func<DynamicPageDomain, bool>)(d => d.Topic != null))?.ActionTitle;
    }

    public static ISerializer CreateJsonSerializer(this Type type)
    {
        Type type1 = typeof(JsonSerializer<>);
        Type[] typeArray = new Type[1] { type };
        ISerializer instance;
        ((IJsonSerializer)(instance = TypeHelper.CreateInstance<ISerializer>(TypeHelper.Make(type1, typeArray), Array.Empty<object>()))).Encoding = JsonHelper.UTF8NoBom;
        return instance;
    }
}
