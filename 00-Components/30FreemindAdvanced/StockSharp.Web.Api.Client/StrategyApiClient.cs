// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.StrategyApiClient
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

internal class StrategyApiClient :
  BaseApiEntityClient<Strategy>,
  IStrategyService,
  IBaseEntityService<Strategy>,
  ICommandService<StrategyUpdateData>
{
    public StrategyApiClient(Uri baseAddress, HttpMessageInvoker http, SecureString token)
      : base(baseAddress, http, token)
    {
    }

    public StrategyApiClient(
      Uri baseAddress,
      HttpMessageInvoker http,
      string login,
      SecureString password)
      : base(baseAddress, http, login, password)
    {
    }

    Task<BaseEntitySet<Strategy>> IStrategyService.FindAsync(
      long skip,
      long? count,
      bool? deleted,
      string orderBy,
      bool? orderByDesc,
      bool? totalCount,
      DateTime? creationStart,
      DateTime? creationEnd,
      long? appId,
      long? securityId,
      long? typeId,
      long? clientId,
      SubscriptionStates? state,
      StrategyExecutionModes? execMode,
      StrategyOptimizations? optimizations,
      string like,
      ComparisonOperator? likeCompare,
      CancellationToken cancellationToken)
    {
        return this.Get<BaseEntitySet<Strategy>>(RestBaseApiClient.GetCurrentMethod("FindAsync"), cancellationToken, new object[17]
        {
      (object) skip,
      (object) count,
      (object) deleted,
      (object) orderBy,
      (object) orderByDesc,
      (object) totalCount,
      (object) creationStart,
      (object) creationEnd,
      (object) appId,
      (object) securityId,
      (object) typeId,
      (object) clientId,
      (object) state,
      (object) execMode,
      (object) optimizations,
      (object) like,
      (object) likeCompare
        });
    }

    Task<bool> IStrategyService.DeleteByUserIdAsync(
      string userId,
      CancellationToken cancellationToken)
    {
        return this.Delete<bool>(RestBaseApiClient.GetCurrentMethod("DeleteByUserIdAsync"), cancellationToken, new object[1]
        {
      (object) userId
        });
    }

    Task<string> IStrategyService.AnalyzeAIAsync(
      long strategyId,
      bool isResult,
      string question,
      CancellationToken cancellationToken)
    {
        return this.Post<string>(RestBaseApiClient.GetCurrentMethod("AnalyzeAIAsync"), cancellationToken, new object[3]
        {
      (object) strategyId,
      (object) isResult,
      (object) question
        });
    }

    Task<(string code, string description)> IStrategyService.GenerateCodeAIAsync(
      ProductContentTypes2 contentType,
      string description,
      CancellationToken cancellationToken)
    {
        return this.Post<(string, string)>(RestBaseApiClient.GetCurrentMethod("GenerateCodeAIAsync"), cancellationToken, new object[2]
        {
      (object) contentType,
      (object) description
        });
    }

    Task ICommandService<StrategyUpdateData>.SendAsync(
      long? id,
      string userId,
      CommandInfo command,
      CancellationToken cancellationToken)
    {
        return this.Post(RestBaseApiClient.GetCurrentMethod("SendAsync"), cancellationToken, new object[3]
        {
      (object) id,
      (object) userId,
      (object) command
        });
    }

    Task ICommandService<StrategyUpdateData>.UpdateStatusAsync(
      StrategyUpdateData status,
      CancellationToken cancellationToken)
    {
        return this.Put(RestBaseApiClient.GetCurrentMethod("UpdateStatusAsync"), cancellationToken, new object[1]
        {
      (object) status
        });
    }
}
