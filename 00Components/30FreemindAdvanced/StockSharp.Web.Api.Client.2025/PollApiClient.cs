// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.PollApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    internal class PollApiClient : BaseApiEntityClient<Poll>, IPollService, IBaseEntityService<Poll>
    {
        public PollApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public PollApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<Poll>> IPollService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          long? clientId,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<Poll>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [9]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         clientId
            } );
        }

        Task<PollChoice> IPollService.AddChoiceAsync(
          PollChoice choice,
          CancellationToken cancellationToken )
        {
            return this.Post<PollChoice>( RestBaseApiClient.GetCurrentMethod( "AddChoiceAsync" ), cancellationToken, new object [1]
            {
         choice
            } );
        }

        Task<bool> IPollService.RemoveChoiceAsync(
          long choiceId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveChoiceAsync" ), cancellationToken, new object [1]
            {
         choiceId
            } );
        }

        Task<PollVote> IPollService.AddVoteAsync(
          long choiceId,
          CancellationToken cancellationToken )
        {
            return this.Post<PollVote>( RestBaseApiClient.GetCurrentMethod( "AddVoteAsync" ), cancellationToken, new object [1]
            {
         choiceId
            } );
        }

        Task<bool> IPollService.RemoveVoteAsync(
          long voteId,
          CancellationToken cancellationToken )
        {
            return this.Post<bool>( RestBaseApiClient.GetCurrentMethod( "RemoveVoteAsync" ), cancellationToken, new object [1]
            {
         voteId
            } );
        }
    }
}
