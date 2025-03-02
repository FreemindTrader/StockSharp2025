// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.InstrumentInfoApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using StockSharp.Messages;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    internal class InstrumentInfoApiClient : BaseApiEntityClient<InstrumentInfo>, IInstrumentInfoService, IBaseEntityService<InstrumentInfo>
    {
        public InstrumentInfoApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public InstrumentInfoApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
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
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<InstrumentInfo>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [21]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         type,
         boardLike,
         boardLikeCompare,
         assetLike,
         assetLikeCompare,
         lastDate,
         settlDate,
         allowBacktest,
         allowLive,
         like,
         likeCompare,
         currency,
         isFinam
            } );
        }

        Task<InstrumentInfo> IInstrumentInfoService.TryGetByCodeAndBoardAsync(
          string code,
          string board,
          CancellationToken cancellationToken )
        {
            return this.Get<InstrumentInfo>( RestBaseApiClient.GetCurrentMethod( "TryGetByCodeAndBoardAsync" ), cancellationToken, new object [2]
            {
         code,
         board
            } );
        }

        
        Task<(int added, int updated, int deleted)> IInstrumentInfoService.RefreshFinamAsync(
          InstrumentInfo [ ] instruments,
          CancellationToken cancellationToken )
        {
            return this.Post<ValueTuple<int, int, int>>( RestBaseApiClient.GetCurrentMethod( "RefreshFinamAsync" ), cancellationToken, new object [1]
            {
         instruments
            } );
        }
    }
}
