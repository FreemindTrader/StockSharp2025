// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.SocialApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Collections.Generic;
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

internal class SocialApiClient :
  BaseApiEntityClient<Social>,
  ISocialService,
  IBaseEntityService<Social>
{
    public SocialApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public SocialApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
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
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<Social>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[12]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) authDomainId,
      (object) flags,
      (object) like,
      (object) likeCompare
        });
    }

    Task<string[]> ISocialService.GetAuthHandlersAsync(CancellationToken cancellationToken)
    {
        return this.Get<string[]>(RestBaseApiClient.GetCurrentMethod("GetAuthHandlersAsync"), cancellationToken, Array.Empty<object>());
    }

    Task<string[]> ISocialService.GetBlogHandlersAsync(CancellationToken cancellationToken)
    {
        return this.Get<string[]>(RestBaseApiClient.GetCurrentMethod("GetBlogHandlersAsync"), cancellationToken, Array.Empty<object>());
    }

    Task<string> ISocialService.GetOAuthRequestAsync(
      long socialId,
      bool isDemo,
      long domainId,
      string returnUrl,
      long? returnPageId,
      string nonce,
      CancellationToken cancellationToken)
    {
        return this.Get<string>(RestBaseApiClient.GetCurrentMethod("GetOAuthRequestAsync"), cancellationToken, new object[6]
        {
      (object) socialId,
      (object) isDemo,
      (object) domainId,
      (object) returnUrl,
      (object) returnPageId,
      (object) nonce
        });
    }

    Task<(StockSharp.Web.DomainModel.Client client, bool isNew, string url, string nonce)> ISocialService.HandleOAuthResponseAsync(
      long socialId,
      long domainId,
      string url,
      IDictionary<string, string> formArgs,
      CancellationToken cancellationToken)
    {
        return this.Post<(StockSharp.Web.DomainModel.Client, bool, string, string)>(RestBaseApiClient.GetCurrentMethod("HandleOAuthResponseAsync"), cancellationToken, new object[4]
        {
      (object) socialId,
      (object) domainId,
      (object) url,
      (object) formArgs
        });
    }

    Task<SocialToken> ISocialService.TryGetAccessTokenAsync(
      long socialId,
      bool isDemo,
      long domainId,
      CancellationToken cancellationToken)
    {
        return this.Get<SocialToken>(RestBaseApiClient.GetCurrentMethod("TryGetAccessTokenAsync"), cancellationToken, new object[3]
        {
      (object) socialId,
      (object) isDemo,
      (object) domainId
        });
    }

    Task ISocialService.HandleOAuthAccessTokenAsync(
      long socialId,
      long domainId,
      string url,
      IDictionary<string, string> formArgs,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("HandleOAuthAccessTokenAsync"), cancellationToken, new object[4]
        {
      (object) socialId,
      (object) domainId,
      (object) url,
      (object) formArgs
        });
    }
}
