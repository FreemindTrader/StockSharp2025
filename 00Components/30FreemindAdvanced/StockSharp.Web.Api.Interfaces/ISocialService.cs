// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ISocialService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public interface ISocialService : IBaseEntityService<Social>
{
    Task<BaseEntitySet<Social>> FindAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      DateTime? creationStart = null,
      DateTime? creationEnd = null,
      long? authDomainId = null,
      SocialFlags? flags = null,
      string like = null,
      ComparisonOperator? likeCompare = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<string[]> GetAuthHandlersAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task<string[]> GetBlogHandlersAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task<string> GetOAuthRequestAsync(
      long socialId,
      bool isDemo,
      long domainId,
      string returnUrl = null,
      long? returnPageId = null,
      string nonce = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<(Client client, bool isNew, string url, string nonce)> HandleOAuthResponseAsync(
      long socialId,
      long domainId,
      string url,
      IDictionary<string, string> formArgs,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<SocialToken> TryGetAccessTokenAsync(
      long socialId,
      bool isDemo,
      long domainId,
      CancellationToken cancellationToken = default(CancellationToken));

    Task HandleOAuthAccessTokenAsync(
      long socialId,
      long domainId,
      string url,
      IDictionary<string, string> formArgs,
      CancellationToken cancellationToken = default(CancellationToken));
}
