// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ClientSocialApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class ClientSocialApiClient :
  BaseApiEntityClient<ClientSocial>,
  IClientSocialService,
  IBaseEntityService<ClientSocial>
{
    public ClientSocialApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public ClientSocialApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<ClientSocial>> IClientSocialService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? clientId,
      long? socialId,
      long? domainId,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<ClientSocial>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[13]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) clientId,
      (object) socialId,
      (object) domainId,
      (object) like,
      (object) likeCompare
        });
    }

    Task<string> IClientSocialService.ShortUrlsAsync(
      string text,
      long domainId,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("ShortUrlsAsync"), cancellationToken, new object[2]
        {
      (object) text,
      (object) domainId
        });
    }

    Task<(long socialId, bool isOk, string message, string url)[]> IClientSocialService.SendAsync(
      string text,
      long[] socialIds,
      long[] attachIds,
      long? clientId,
      DateTime? schedule,
      CancellationToken cancellationToken)
    {
        return this.Post<(long, bool, string, string)[]>(RestBaseApiClient.GetCurrentMethod("SendAsync"), cancellationToken, new object[5]
        {
      (object) text,
      (object) socialIds,
      (object) attachIds,
      (object) clientId,
      (object) schedule
        });
    }

    Task<ClientSocial> IClientSocialService.GetOrCreateAsync(
      long clientId,
      long socialId,
      string code,
      string name,
      CancellationToken cancellationToken)
    {
        return this.Get<ClientSocial>(RestBaseApiClient.GetCurrentMethod("GetOrCreateAsync"), cancellationToken, new object[4]
        {
      (object) clientId,
      (object) socialId,
      (object) code,
      (object) name
        });
    }

    Task<ClientSocial> IClientSocialService.TryGetBySystemId(
      long socialId,
      string systemId,
      CancellationToken cancellationToken)
    {
        return this.Get<ClientSocial>(RestBaseApiClient.GetCurrentMethod("TryGetBySystemId"), cancellationToken, new object[2]
        {
      (object) socialId,
      (object) systemId
        });
    }

    Task<string> IClientSocialService.AskAIAsync(
      long socialId,
      string quote,
      string question,
      long[] attachIds,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("AskAIAsync"), cancellationToken, new object[4]
        {
      (object) socialId,
      (object) quote,
      (object) question,
      (object) attachIds
        });
    }
}
