// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ISiteSettingsService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using Ecng.Net.Captcha;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface ISiteSettingsService : IBaseEntityService<SiteSettings>, ICaptchaService, ICaptchaValidator<bool>
    {
        Task<BaseEntitySet<SiteSettings>> FindAsync(
          long skip = 0,
          long? count = null,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? totalCount = null,
          DateTime? creationStart = null,
          DateTime? creationEnd = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task ResetCacheAsync( CancellationToken cancellationToken = default( CancellationToken ) );

        Task<byte [ ]> SignAsync( byte [ ] data, CancellationToken cancellationToken = default( CancellationToken ) );


        Task<(string value1, string value2, string value3)> EncryptUrlAsync( string value1, string value2, string value3, CancellationToken cancellationToken = default( CancellationToken ) );


        Task<(string value1, string value2, string value3)> DecryptUrlAsync( string value1, string value2, string value3, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<string> GetStatAsync( long skip = 0, long? count = null, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<BaseEntitySet<SiteSettingsKeys>> FindKeysAsync(
          long skip = 0,
          long? count = null,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? totalCount = null,
          DateTime? creationStart = null,
          DateTime? creationEnd = null,
          string appName = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<SiteSettingsKeys> AddKeysAsync(
          SiteSettingsKeys keys,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<bool> RemoveKeysAsync( long keysId, CancellationToken cancellationToken = default( CancellationToken ) );


        Task<(string name, string link) [ ]> GoogleSearchAsync( long skip, long? count, long domainId, string query, CancellationToken cancellationToken = default( CancellationToken ) );
    }
}
