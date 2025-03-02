// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.SocialApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using static StockSharp.Web.DomainModel.Ids;
using Social = StockSharp.Web.DomainModel.Social;

namespace StockSharp.Web.Api.Client
{
    internal class SocialApiClient : BaseApiEntityClient<Social>, ISocialService, IBaseEntityService<Social>
    {
        public SocialApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public SocialApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<Social>> ISocialService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          long? authDomainId,
          SocialFlags? flags,
          string like,
          ComparisonOperator? likeCompare,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<Social>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [12]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         authDomainId,
         flags,
         like,
         likeCompare
            } );
        }

        Task<string [ ]> ISocialService.GetAuthHandlersAsync(
          CancellationToken cancellationToken )
        {
            return this.Get<string [ ]>( RestBaseApiClient.GetCurrentMethod( "GetAuthHandlersAsync" ), cancellationToken, Array.Empty<object>() );
        }

        Task<string [ ]> ISocialService.GetBlogHandlersAsync(
          CancellationToken cancellationToken )
        {
            return this.Get<string [ ]>( RestBaseApiClient.GetCurrentMethod( "GetBlogHandlersAsync" ), cancellationToken, Array.Empty<object>() );
        }

        Task<string> ISocialService.GetOAuthRequestAsync(
          long socialId,
          bool isDemo,
          long domainId,
          string returnUrl,
          long? returnPageId,
          string nonce,
          CancellationToken cancellationToken )
        {
            return this.Get<string>( RestBaseApiClient.GetCurrentMethod( "GetOAuthRequestAsync" ), cancellationToken, new object [6]
            {
         socialId,
         isDemo,
         domainId,
         returnUrl,
         returnPageId,
         nonce
            } );
        }

        
        Task<(StockSharp.Web.DomainModel.Client client, bool isNew, string url, string nonce)> ISocialService.HandleOAuthResponseAsync( long socialId, long domainId, string url, IDictionary<string, string> formArgs, CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<StockSharp.Web.DomainModel.Client, bool, string, string>>( RestBaseApiClient.GetCurrentMethod( "HandleOAuthResponseAsync" ), cancellationToken, new object [4]
            {
         socialId,
         domainId,
         url,
         formArgs
            } );
        }

        Task<SocialToken> ISocialService.TryGetAccessTokenAsync(
          long socialId,
          bool isDemo,
          long domainId,
          CancellationToken cancellationToken )
        {
            return this.Get<SocialToken>( RestBaseApiClient.GetCurrentMethod( "TryGetAccessTokenAsync" ), cancellationToken, new object [3]
            {
         socialId,
         isDemo,
         domainId
            } );
        }

        Task ISocialService.HandleOAuthAccessTokenAsync(
          long socialId,
          long domainId,
          string url,
          IDictionary<string, string> formArgs,
          CancellationToken cancellationToken )
        {
            return this.Post( RestBaseApiClient.GetCurrentMethod( "HandleOAuthAccessTokenAsync" ), cancellationToken, new object [4]
            {
         socialId,
         domainId,
         url,
         formArgs
            } );
        }
    }
}
