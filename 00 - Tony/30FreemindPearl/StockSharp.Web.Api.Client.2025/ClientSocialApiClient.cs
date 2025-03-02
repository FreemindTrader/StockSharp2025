// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ClientSocialApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using static StockSharp.Web.DomainModel.Ids;

namespace StockSharp.Web.Api.Client
{
    internal class ClientSocialApiClient : BaseApiEntityClient<ClientSocial>, IClientSocialService, IBaseEntityService<ClientSocial>
    {
        public ClientSocialApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public ClientSocialApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<ClientSocial>> IClientSocialService.FindAsync( long skip, long? count, bool? deleted, string orderBy, bool? orderByDesc, bool? totalCount, DateTime? creationStart, DateTime? creationEnd, long? clientId, long? socialId, long? domainId, string like, ComparisonOperator? likeCompare, CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<ClientSocial>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [13]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         clientId,
         socialId,
         domainId,
         like,
         likeCompare
            } );
        }

        Task<string> IClientSocialService.ShortUrlsAsync(
          string text,
          long domainId,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "ShortUrlsAsync" ), cancellationToken, new object [2]
            {
         text,
         domainId
            } );
        }

        
        Task<(long socialId, bool isOk, string message, string url) [ ]> IClientSocialService.SendAsync(
          string text,
          long [ ] socialIds,
          long [ ] attachIds,
          long? clientId,
          DateTime? schedule,
          CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<long, bool, string, string> [ ]>( RestBaseApiClient.GetCurrentMethod( "SendAsync" ), cancellationToken, new object [5]
            {
         text,
         socialIds,
         attachIds,
         clientId,
         schedule
            } );
        }

        Task<ClientSocial> IClientSocialService.GetOrCreateAsync(
          long clientId,
          long socialId,
          string code,
          string name,
          CancellationToken cancellationToken )
        {
            return this.Get<ClientSocial>( RestBaseApiClient.GetCurrentMethod( "GetOrCreateAsync" ), cancellationToken, new object [4]
            {
         clientId,
         socialId,
         code,
         name
            } );
        }

        Task<ClientSocial> IClientSocialService.TryGetBySystemId(
          long socialId,
          string systemId,
          CancellationToken cancellationToken )
        {
            return this.Get<ClientSocial>( RestBaseApiClient.GetCurrentMethod( "TryGetBySystemId" ), cancellationToken, new object [2]
            {
         socialId,
         systemId
            } );
        }

        Task<string> IClientSocialService.AskAIAsync(
          long socialId,
          string quote,
          string question,
          long [ ] attachIds,
          CancellationToken cancellationToken )
        {
            return this.Post<string>( RestBaseApiClient.GetCurrentMethod( "AskAIAsync" ), cancellationToken, new object [4]
            {
         socialId,
         quote,
         question,
         attachIds
            } );
        }
    }
}
