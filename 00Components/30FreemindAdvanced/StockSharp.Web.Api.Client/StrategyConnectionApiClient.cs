// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.StrategyConnectionApiClient
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
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

internal class StrategyConnectionApiClient :
  BaseApiEntityClient<StrategyConnection>,
  IStrategyConnectionService,
  IBaseEntityService<StrategyConnection>
{
    public StrategyConnectionApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public StrategyConnectionApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<StrategyConnection>> IStrategyConnectionService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? typeId,
      long? clientId,
      bool? isDemo,
      bool? isApproved,
      bool? withError,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<StrategyConnection>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[15]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) typeId,
      (object) clientId,
      (object) isDemo,
      (object) isApproved,
      (object) withError,
      (object) like,
      (object) likeCompare
        });
    }
}
