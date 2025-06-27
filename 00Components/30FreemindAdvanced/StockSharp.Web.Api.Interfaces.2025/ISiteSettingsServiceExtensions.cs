// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ISiteSettingsServiceExtensions
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public static class ISiteSettingsServiceExtensions
    {
        public static Task<SiteSettings> GetDefaultAsync(
          this ISiteSettingsService service,
          CancellationToken cancellationToken = default( CancellationToken ) )
        {
            ISiteSettingsService siteSettingsService = service;
            if ( siteSettingsService == null )
                throw new ArgumentNullException( nameof( service ) );
            return siteSettingsService.GetAsync( 1L, cancellationToken );
        }
    }
}
