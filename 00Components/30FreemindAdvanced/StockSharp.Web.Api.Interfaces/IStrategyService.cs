// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IStrategyService
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

public interface IStrategyService : IBaseEntityService<Strategy>, ICommandService<StrategyUpdateData>
{
    Task<BaseEntitySet<Strategy>> FindAsync(
      long skip = 0,
      long? count = null,
      bool? deleted = null,
      string orderBy = null,
      bool? orderByDesc = null,
      bool? totalCount = null,
      DateTime? creationStart = null,
      DateTime? creationEnd = null,
      long? appId = null,
      long? securityId = null,
      long? typeId = null,
      long? clientId = null,
      SubscriptionStates? state = null,
      StrategyExecutionModes? execMode = null,
      StrategyOptimizations? optimizations = null,
      string like = null,
      ComparisonOperator? likeCompare = null,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<bool> DeleteByUserIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken));

    Task<string> AnalyzeAIAsync(
      long strategyId,
      bool isResult,
      string question,
      CancellationToken cancellationToken = default(CancellationToken));

    Task<(string code, string description)> GenerateCodeAIAsync(
      ProductContentTypes2 contentType,
      string description,
      CancellationToken cancellationToken = default(CancellationToken));
}
