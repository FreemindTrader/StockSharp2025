// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IInstrumentInfoService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using Ecng.Common;
using StockSharp.Messages;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IInstrumentInfoService : IBaseEntityService<InstrumentInfo>
    {
        Task<BaseEntitySet<InstrumentInfo>> FindAsync(
          long skip = 0,
          long? count = null,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? totalCount = null,
          DateTime? creationStart = null,
          DateTime? creationEnd = null,
          SecurityTypes? type = null,
          string boardLike = null,
          ComparisonOperator? boardLikeCompare = null,
          string assetLike = null,
          ComparisonOperator? assetLikeCompare = null,
          DateTime? lastDate = null,
          DateTime? settlDate = null,
          bool? allowBacktest = null,
          bool? allowLive = null,
          string like = null,
          ComparisonOperator? likeCompare = null,
          CurrencyTypes? currency = null,
          bool? isFinam = null,
          CancellationToken cancellationToken = default( CancellationToken ) );

        Task<InstrumentInfo> TryGetByCodeAndBoardAsync(
          string code,
          string board,
          CancellationToken cancellationToken = default( CancellationToken ) );


        Task<(int added, int updated, int deleted)> RefreshFinamAsync( InstrumentInfo [ ] instruments, CancellationToken cancellationToken = default( CancellationToken ) );
    }
}
