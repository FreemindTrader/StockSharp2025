// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IClientSocialService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using Ecng.Common;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
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
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<string> ShortUrlsAsync(
          string text,
          long domainId,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<(long socialId, bool isOk, string message, string url) [ ]> SendAsync( string text, long [ ] socialIds, long [ ] attachIds, long? clientId = null, DateTime? schedule = null, CancellationToken cancellationToken = default( CancellationToken ) );


        Task<ClientSocial> GetOrCreateAsync(
          long clientId,
          long socialId,
          string code,
          string name,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<ClientSocial> TryGetBySystemId(
          long socialId,
          string systemId,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<string> AskAIAsync(
          long socialId,
          string quote,
          string question,
          long [ ] attachIds,
          CancellationToken cancellationToken = default( CancellationToken ) );
    }
}
