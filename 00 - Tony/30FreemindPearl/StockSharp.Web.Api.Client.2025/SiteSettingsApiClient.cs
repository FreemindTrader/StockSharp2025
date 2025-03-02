// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.SiteSettingsApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Net;
using Ecng.Net.Captcha;
using Newtonsoft.Json.Linq;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StockSharp.Web.Api.Client
{
    internal class SiteSettingsApiClient : BaseApiEntityClient<SiteSettings>, ISiteSettingsService, IBaseEntityService<SiteSettings>, ICaptchaService, ICaptchaValidator<bool>
    {
        public SiteSettingsApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public SiteSettingsApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<SiteSettings>> ISiteSettingsService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<SiteSettings>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [8]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd
            } );
        }

        Task ISiteSettingsService.ResetCacheAsync(
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "ResetCacheAsync" ), cancellationToken, Array.Empty<object>() );
        }

        Task<byte [ ]> ISiteSettingsService.SignAsync(
          byte [ ] data,
          CancellationToken cancellationToken )
        {
            return this.Post<byte [ ]>( RestBaseApiClient.GetCurrentMethod( "SignAsync" ), cancellationToken, new object [1]
            {
         data
            } );
        }

        
        Task<(string value1, string value2, string value3)> ISiteSettingsService.EncryptUrlAsync(
          string value1,
          string value2,
          string value3,
          CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<string, string, string>>( RestBaseApiClient.GetCurrentMethod( "EncryptUrlAsync" ), cancellationToken, new object [3]
            {
         value1,
         value2,
         value3
            } );
        }

        
        Task<(string value1, string value2, string value3)> ISiteSettingsService.DecryptUrlAsync(
          string value1,
          string value2,
          string value3,
          CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<string, string, string>>( RestBaseApiClient.GetCurrentMethod( "DecryptUrlAsync" ), cancellationToken, new object [3]
            {
         value1,
         value2,
         value3
            } );
        }

        Task<string> ISiteSettingsService.GetStatAsync(
          long skip,
          long? count,
          CancellationToken cancellationToken )
        {
            return this.Get<string>( RestBaseApiClient.GetCurrentMethod( "GetStatAsync" ), cancellationToken, new object [2]
            {
         skip,
         count
            } );
        }

        Task<BaseEntitySet<SiteSettingsKeys>> ISiteSettingsService.FindKeysAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          string appName,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<SiteSettingsKeys>>( RestBaseApiClient.GetCurrentMethod( "FindKeysAsync" ), cancellationToken, new object [9]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         appName
            } );
        }

        Task<SiteSettingsKeys> ISiteSettingsService.AddKeysAsync(
          SiteSettingsKeys keys,
          CancellationToken cancellationToken )
        {
            return this.Post<SiteSettingsKeys>( RestBaseApiClient.GetCurrentMethod( "AddKeysAsync" ), cancellationToken, new object [1]
            {
         keys
            } );
        }

        Task<bool> ISiteSettingsService.RemoveKeysAsync(
          long keysId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveKeysAsync" ), cancellationToken, new object [1]
            {
         keysId
            } );
        }

        
        Task<(string name, string link) [ ]> ISiteSettingsService.GoogleSearchAsync(
          long skip,
          long? count,
          long domainId,
          string query,
          CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<string, string> [ ]>( RestBaseApiClient.GetCurrentMethod( "GoogleSearchAsync" ), cancellationToken, new object [4]
            {
         skip,
         count,
         domainId,
         query
            } );
        }


        

        

        Task<float> GetScoreAsync( IPAddress address, CancellationToken cancellationToken )
        {
            return this.Get<float>( RestBaseApiClient.GetCurrentMethod( "GetScoreAsync" ), cancellationToken, new object [1]
            {
         address
            } );
        }

        
        Task<BaseEntitySet<(string address, DateTime time, float? score, string message)>> GetPendingAsync( CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<ValueTuple<string, DateTime, float?, string>>>( RestBaseApiClient.GetCurrentMethod( "GetPendingAsync" ), cancellationToken, Array.Empty<object>() );
        }

        Task<bool> ICaptchaValidator<bool>.ValidateAsync(
          string response,
          string address,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "ValidateAsync" ), cancellationToken, new object [2]
            {
         response,
         address
            } );
        }
    }
}
