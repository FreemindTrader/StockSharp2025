// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IInstrumentInfoService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using StockSharp.Messages;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

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
      CancellationToken cancellationToken = default(CancellationToken));

    Task<InstrumentInfo> TryGetByCodeAndBoardAsync(
      string code,
      string board,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<(int added, int updated, int deleted)> RefreshFinamAsync(
      InstrumentInfo[] instruments,
      CancellationToken cancellationToken = default(CancellationToken));
}
