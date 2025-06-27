// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.ISocialService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using Ecng.Common;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface ISocialService : IBaseEntityService<StockSharp.Web.DomainModel.Social>
    {
        Task<BaseEntitySet<StockSharp.Web.DomainModel.Social>> FindAsync(
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
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<string [ ]> GetAuthHandlersAsync( CancellationToken cancellationToken = default( CancellationToken ) );

        Task<string [ ]> GetBlogHandlersAsync( CancellationToken cancellationToken = default( CancellationToken ) );

        Task<string> GetOAuthRequestAsync(
          long socialId,
          bool isDemo,
          long domainId,
          string returnUrl = null,
          long? returnPageId = null,
          string nonce = null,
          CancellationToken cancellationToken = default( CancellationToken ) );


        Task<(StockSharp.Web.DomainModel.Client client, bool isNew, string url, string nonce)> HandleOAuthResponseAsync(
          long socialId,
          long domainId,
          string url,
          IDictionary<string, string> formArgs,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<SocialToken> TryGetAccessTokenAsync(
          long socialId,
          bool isDemo,
          long domainId,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task HandleOAuthAccessTokenAsync(
          long socialId,
          long domainId,
          string url,
          IDictionary<string, string> formArgs,
          CancellationToken cancellationToken = default( CancellationToken ) );
    }
}
