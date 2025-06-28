// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IClientSocialService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public interface IClientSocialService : IBaseEntityService<ClientSocial>
{
    Task<BaseEntitySet<ClientSocial>> FindAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      DateTime? creationStart = null,
      DateTime? creationEnd = null,
      long? clientId = null,
      long? socialId = null,
      long? domainId = null,
      string like = null,
      ComparisonOperator? likeCompare = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<string> ShortUrlsAsync(string text, long domainId, CancellationToken cancellationToken = default(CancellationToken));

    Task<(long socialId, bool isOk, string message, string url)[]> SendAsync(
      string text,
      long[] socialIds,
      long[] attachIds,
      long? clientId = null,
      DateTime? schedule = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<ClientSocial> GetOrCreateAsync(
      long clientId,
      long socialId,
      string code,
      string name,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<ClientSocial> TryGetBySystemId(
      long socialId,
      string systemId,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<string> AskAIAsync(
      long socialId,
      string quote,
      string question,
      long[] attachIds,
      CancellationToken cancellationToken = default(CancellationToken));
}
