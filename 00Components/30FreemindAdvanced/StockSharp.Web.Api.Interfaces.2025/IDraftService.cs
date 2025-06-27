// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IDraftService
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
    public interface IDraftService : IBaseEntityService<Draft>
    {
        Task<BaseEntitySet<Draft>> FindAsync(
          long skip = 0,
          long? count = null,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? totalCount = null,
          DateTime? creationStart = null,
          DateTime? creationEnd = null,
          long? clientId = null,
          string like = null,
          ComparisonOperator? likeCompare = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<Draft> TryGetByPageIdAsync( string pageId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<bool> RemoveByPageAsync( string pageId, CancellationToken cancellationToken = default( CancellationToken ) );

        Task<bool> RemoveFileAsync( long fileId, CancellationToken cancellationToken = default( CancellationToken ) );
    }
}
