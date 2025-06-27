// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.StrategyParamApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Client
{
    internal class StrategyParamApiClient : BaseApiEntityClient<StrategyParam>, IStrategyParamService, IBaseEntityService<StrategyParam>
    {
        public StrategyParamApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public StrategyParamApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }

        Task<BaseEntitySet<StrategyParam>> IStrategyParamService.FindAsync(
          long skip,
          long? count,
          bool? deleted,
          string orderBy,
          bool? orderByDesc,
          bool? totalCount,
          DateTime? creationStart,
          DateTime? creationEnd,
          long? strategyId,
          string like,
          ComparisonOperator? likeCompare,
          CancellationToken cancellationToken )
        {
            return this.Get<BaseEntitySet<StrategyParam>>( RestBaseApiClient.GetCurrentMethod( "FindAsync" ), cancellationToken, new object [11]
            {
         skip,
         count,
         deleted,
         orderBy,
         orderByDesc,
         totalCount,
         creationStart,
         creationEnd,
         strategyId,
         like,
         likeCompare
            } );
        }
    }
}
