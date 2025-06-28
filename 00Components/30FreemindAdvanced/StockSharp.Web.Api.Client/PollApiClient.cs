// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.PollApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class PollApiClient : BaseApiEntityClient<Poll>, IPollService, IBaseEntityService<Poll>
{
    public PollApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public PollApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
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
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<Poll>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[9]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) clientId
        });
    }

    Task<PollChoice> IPollService.AddChoiceAsync(
      PollChoice choice,
      CancellationToken cancellationToken)
    {
        return this.Post<PollChoice>(RestBaseApiClient.GetCurrentMethod("AddChoiceAsync"), cancellationToken, new object[1]
        {
      (object) choice
        });
    }

    Task<bool> IPollService.RemoveChoiceAsync(long choiceId, CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveChoiceAsync"), cancellationToken, new object[1]
        {
      (object) choiceId
        });
    }

    Task<PollVote> IPollService.AddVoteAsync(long choiceId, CancellationToken cancellationToken)
    {
        return this.Post<PollVote>(RestBaseApiClient.GetCurrentMethod("AddVoteAsync"), cancellationToken, new object[1]
        {
      (object) choiceId
        });
    }

    Task<bool> IPollService.RemoveVoteAsync(long voteId, CancellationToken cancellationToken)
    {
        return this.Post<bool>(RestBaseApiClient.GetCurrentMethod("RemoveVoteAsync"), cancellationToken, new object[1]
        {
      (object) voteId
        });
    }
}
