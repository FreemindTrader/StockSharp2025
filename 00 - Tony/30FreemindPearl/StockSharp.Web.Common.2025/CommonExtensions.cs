// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.CommonExtensions
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Common;
using Ecng.Net;
using Ecng.Serialization;
using SmartFormat;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockSharp.Web.Common
{
    public static class CommonExtensions
    {
        public static SmartFormatter CreateEmailFormatter()
        {
            SmartFormatter defaultSmartFormat = Smart.CreateDefaultSmartFormat();
            defaultSmartFormat.Parser.Settings.DoubleBrace = true;
            return defaultSmartFormat;
        }

        public static bool IsImage( this File file )
        {
            if ( file == null )
                throw new ArgumentNullException( nameof( file ) );
            return file.Name.IsImage();
        }

        public static TEntityDomain TryGetEntityDomain<TEntity, TEntityDomain>(
          this TEntity entity,
          long domainId,
          Func<TEntityDomain, bool> filter )
          where TEntity : class, IDomainsEntity<TEntityDomain>
          where TEntityDomain : BaseEntity, IDomainEntity
        {
            TEntityDomain[] items = ((IDomainsEntity<TEntityDomain>)  TypeHelper.CheckOnNull<TEntity>(entity, nameof (entity))).Domains?.Items;
            TEntityDomain entityDomain1 = items != null ? ((IEnumerable<TEntityDomain>) items).FirstOrDefault<TEntityDomain>((Func<TEntityDomain, bool>) (d =>
      {
          if (d.Domain.Id == domainId)
              return filter(d);
          return false;
      })) : default (TEntityDomain);
            if ( ( object ) entityDomain1 != null )
                return entityDomain1;
            TEntityDomain entityDomain2 = items != null ? ((IEnumerable<TEntityDomain>) items).FirstOrDefault<TEntityDomain>(filter) : default (TEntityDomain);
            if ( ( object ) entityDomain2 != null )
                return entityDomain2;
            if ( items == null )
                return default( TEntityDomain );
            return ( ( IEnumerable<TEntityDomain> ) items ).FirstOrDefault<TEntityDomain>();
        }

        public static DynamicPageDomain TryGetDomain(
          this DynamicPage page,
          long domainId,
          Func<DynamicPageDomain, bool> filter )
        {
            return page.TryGetEntityDomain<DynamicPage, DynamicPageDomain>( domainId, filter );
        }

        public static string GetUrlRelative( this DynamicPage page, long domainId )
        {
            return page.TryGetDomain( domainId, ( Func<DynamicPageDomain, bool> ) ( d => !StringHelper.IsEmpty( d.UrlRelative ) ) )?.UrlRelative;
        }

        public static string GetUrlPart( this DynamicPage page, long domainId )
        {
            return page.TryGetDomain( domainId, ( Func<DynamicPageDomain, bool> ) ( d => !StringHelper.IsEmpty( d.UrlPart ) ) )?.UrlPart;
        }

        public static DynamicMenuDomain TryGetDomain(
          this DynamicMenu menu,
          long domainId,
          Func<DynamicMenuDomain, bool> filter )
        {
            return menu.TryGetEntityDomain<DynamicMenu, DynamicMenuDomain>( domainId, filter );
        }

        public static string GetUrl( this DynamicMenu menu, long domainId )
        {
            return StringHelper.IsEmpty( menu.TryGetDomain( domainId, ( Func<DynamicMenuDomain, bool> ) ( d => !StringHelper.IsEmpty( d.UrlAbsolute ) ) )?.UrlAbsolute, menu.TryGetDomain( domainId, ( Func<DynamicMenuDomain, bool> ) ( d => !StringHelper.IsEmpty( d.UrlRelative ) ) )?.UrlRelative );
        }

        public static string GetName( this DynamicMenu menu, long domainId )
        {
            return menu.TryGetDomain( domainId, ( Func<DynamicMenuDomain, bool> ) ( d => !StringHelper.IsEmpty( d.Name ) ) )?.Name;
        }

        public static string GetDescription( this DynamicMenu menu, long domainId )
        {
            return menu.TryGetDomain( domainId, ( Func<DynamicMenuDomain, bool> ) ( d => !StringHelper.IsEmpty( d.Description ) ) )?.Description;
        }

        public static string GetRedirectUrl( this DynamicPage page, long domainId )
        {
            return page.TryGetDomain( domainId, ( Func<DynamicPageDomain, bool> ) ( d => !StringHelper.IsEmpty( d.RedirectUrl ) ) )?.RedirectUrl;
        }

        public static File GetFile( this DynamicPage page, long domainId )
        {
            return page.TryGetDomain( domainId, ( Func<DynamicPageDomain, bool> ) ( d => d.File != null ) )?.File;
        }

        public static Topic GetContent( this DynamicPage page, long domainId )
        {
            return page.TryGetDomain( domainId, ( Func<DynamicPageDomain, bool> ) ( d => d.Topic != null ) )?.Topic;
        }

        public static string GetActionTitle( this DynamicPage page, long domainId )
        {
            return page.TryGetDomain( domainId, ( Func<DynamicPageDomain, bool> ) ( d => d.Topic != null ) )?.ActionTitle;
        }

        public static ISerializer CreateJsonSerializer( this Type type )
        {
            Type type1 = typeof (JsonSerializer<>);
            Type[] typeArray = new Type[1]{ type };
            IJsonSerializer instance;

            var instance2 = TypeHelper.CreateInstance<ISerializer>( TypeHelper.Make( type1, typeArray ), Array.Empty<object>() );

            instance = ( IJsonSerializer ) instance2;

            instance.Encoding = ( ( Encoding ) JsonHelper.UTF8NoBom );
            return ( ISerializer ) instance;
        }
    }
}
