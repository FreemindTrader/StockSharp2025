// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ISiteSettingsServiceExtensions
// Assembly: StockSharp.Web.Api.Interfaces, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8DA76177-7390-4832-AC1B-2142F78BAD4C
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Interfaces.dll

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
          CancellationToken cancellationToken = default(CancellationToken))
        {
            ISiteSettingsService siteSettingsService = service;
            if (siteSettingsService == null)
                throw new ArgumentNullException(nameof(service));
            return siteSettingsService.GetAsync(1L, cancellationToken);
        }
    }
}
