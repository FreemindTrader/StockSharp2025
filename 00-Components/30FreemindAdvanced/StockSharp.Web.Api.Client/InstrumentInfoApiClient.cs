// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.InstrumentInfoApiClient
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
using StockSharp.Messages;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class InstrumentInfoApiClient :
  BaseApiEntityClient<InstrumentInfo>,
  IInstrumentInfoService,
  IBaseEntityService<InstrumentInfo>
{
    public InstrumentInfoApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public InstrumentInfoApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<InstrumentInfo>> IInstrumentInfoService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      SecurityTypes? type,
      string boardLike,
      ComparisonOperator? boardLikeCompare,
      string assetLike,
      ComparisonOperator? assetLikeCompare,
      DateTime? lastDate,
      DateTime? settlDate,
      bool? allowBacktest,
      bool? allowLive,
      string like,
      ComparisonOperator? likeCompare,
      CurrencyTypes? currency,
      bool? isFinam,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<InstrumentInfo>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[21]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) type,
      (object) boardLike,
      (object) boardLikeCompare,
      (object) assetLike,
      (object) assetLikeCompare,
      (object) lastDate,
      (object) settlDate,
      (object) allowBacktest,
      (object) allowLive,
      (object) like,
      (object) likeCompare,
      (object) currency,
      (object) isFinam
        });
    }

    Task<InstrumentInfo> IInstrumentInfoService.TryGetByCodeAndBoardAsync(
      string code,
      string board,
      CancellationToken cancellationToken)
    {
        return this.Get<InstrumentInfo>(RestBaseApiClient.GetCurrentMethod("TryGetByCodeAndBoardAsync"), cancellationToken, new object[2]
        {
      (object) code,
      (object) board
        });
    }

    Task<(int added, int updated, int deleted)> IInstrumentInfoService.RefreshFinamAsync(
      InstrumentInfo[] instruments,
      CancellationToken cancellationToken)
    {
        return this.Post<(int, int, int)>(RestBaseApiClient.GetCurrentMethod("RefreshFinamAsync"), cancellationToken, new object[1]
        {
      (object) instruments
        });
    }
}
