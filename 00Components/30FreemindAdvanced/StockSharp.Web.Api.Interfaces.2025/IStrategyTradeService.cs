// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.IStrategyTradeService
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8F35BF5B-4009-41CB-AE35-4A783DE154B0
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Interfaces.dll

using StockSharp.Messages;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Web.Api.Interfaces
{
    public interface IStrategyTradeService : IBaseEntityService<StrategyTrade>
    {
        Task<BaseEntitySet<StrategyTrade>> FindAsync(
          long skip = 0,
          long? count = null,
          bool? deleted = null,
          string orderBy = null,
          bool? orderByDesc = null,
          bool? totalCount = null,
          DateTime? creationStart = null,
          DateTime? creationEnd = null,
          long? clientId = null,
          long? strategyId = null,
          long? accountId = null,
          Sides? side = null,
          long? securityId = null,
          CancellationToken cancellationToken = default( CancellationToken ) );
    }
}
