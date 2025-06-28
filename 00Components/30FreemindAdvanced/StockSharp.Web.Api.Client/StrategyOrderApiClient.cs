// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.StrategyOrderApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Net;
using StockSharp.Messages;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class StrategyOrderApiClient :
  BaseApiEntityClient<StrategyOrder>,
  IStrategyOrderService,
  IBaseEntityService<StrategyOrder>
{
    public StrategyOrderApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public StrategyOrderApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<StrategyOrder>> IStrategyOrderService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? clientId,
      long? strategyId,
      long? accountId,
      Sides? side,
      long? securityId,
      OrderTypes? type,
      OrderStates? state,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<StrategyOrder>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[15]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) clientId,
      (object) strategyId,
      (object) accountId,
      (object) side,
      (object) securityId,
      (object) type,
      (object) state
        });
    }
}
