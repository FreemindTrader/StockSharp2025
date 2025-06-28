// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Remote.RemoteStrategyManager
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 79FA112F-39E9-4D2F-8DA4-EB9B4E826551
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Core.dll

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Logging;
using Nito.AsyncEx;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Studio.WebApi;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

namespace StockSharp.Studio.Core.Remote;

public class RemoteStrategyManager : BaseLogReceiver, IAsyncDisposable
{
    private readonly
#nullable disable
    CachedSynchronizedDictionary<Guid, RemoteStrategyManager.RemoteStrategyController> _controllers;
    private readonly SynchronizedSet<string> _uniqueMsgs;
    private readonly SynchronizedDictionary<Guid, long> _idMap;
    private readonly Func<App> _createApp;
    private App _app;
    private IAppService _appSvc;
    private IStrategyService _strategySvc;
    private IClientSocialService _socialSvc;
    private bool _isRemoteEnabled;

    public RemoteStrategyManager(bool isRemoteEnabled, Func<App> createApp)
    {
        Func<App> func = createApp;
        if (func == null)
            throw new ArgumentNullException(nameof(createApp));
        this._createApp = func;
        this._isRemoteEnabled = isRemoteEnabled;

    }

    public bool IsRemoteEnabled
    {
        get => this._isRemoteEnabled;
        set
        {
            this._isRemoteEnabled = value;
            AsyncContext.Run((Func<Task>)(() => this.RefreshRemoteEnable(new CancellationToken())));
        }
    }

    public async Task Init(CancellationToken cancellationToken)
    {
        IClientSocialService socialSvc;
        if (WebApiServicesRegistry.Offline)
        {
            socialSvc = null;
        }
        else
        {
            socialSvc = WebApiServicesRegistry.GetService<IClientSocialService>();
            await socialSvc.RefreshAsync(cancellationToken);
            await this.Init(WebApiServicesRegistry.GetService<IAppService>(), WebApiServicesRegistry.GetService<IStrategyService>(), socialSvc, cancellationToken);
            socialSvc = null;
        }
    }

    public async Task Init(IAppService appSvc, IStrategyService strategySvc, IClientSocialService socialSvc, CancellationToken cancellationToken)
    {
        RemoteStrategyManager remoteStrategyManager1 = this;
        RemoteStrategyManager remoteStrategyManager2 = remoteStrategyManager1;
        remoteStrategyManager2._appSvc = appSvc ?? throw new ArgumentNullException(nameof(appSvc));
        RemoteStrategyManager remoteStrategyManager3 = remoteStrategyManager1;
        remoteStrategyManager3._strategySvc = strategySvc ?? throw new ArgumentNullException(nameof(strategySvc));
        RemoteStrategyManager remoteStrategyManager4 = remoteStrategyManager1;
        remoteStrategyManager4._socialSvc = socialSvc ?? throw new ArgumentNullException(nameof(socialSvc));
        await remoteStrategyManager1.RefreshRemoteEnable(cancellationToken);
    }

    public async Task AddStrategy(StockSharp.Algo.Strategies.Strategy strategy, RemoteStrategySettings settings, CancellationToken cancellationToken)
    {
        RemoteStrategyManager parent = this;
        if (strategy == null)
            throw new ArgumentNullException(nameof(strategy));
        if (settings == null)
            throw new ArgumentNullException(nameof(settings));
        if (((SynchronizedDictionary<Guid, RemoteStrategyManager.RemoteStrategyController>)parent._controllers).ContainsKey(((BaseLogSource)strategy).Id))
            throw new DuplicateException(((BaseLogSource)strategy).Id.ToString());
        RemoteStrategyManager.RemoteStrategyController strategyController = new RemoteStrategyManager.RemoteStrategyController(parent, strategy, settings);
        ((SynchronizedDictionary<Guid, RemoteStrategyManager.RemoteStrategyController>)parent._controllers).Add(((BaseLogSource)strategy).Id, strategyController);
        if (!strategyController.CanSend || parent._idMap.ContainsKey(((BaseLogSource)strategy).Id))
            return;
        if (parent._app == null)
            return;
        try
        {
            IStrategyService strategySvc = parent._strategySvc;
            StockSharp.Web.DomainModel.Strategy entity = new StockSharp.Web.DomainModel.Strategy();
            entity.Name = ((BaseLogSource)strategy).Name;
            entity.UserId = Converter.To<string>((object)((BaseLogSource)strategy).Id);
            App app = new App();
            app.Id = parent._app.Id;
            entity.App = app;
            CancellationToken cancellationToken1 = cancellationToken;
            StockSharp.Web.DomainModel.Strategy strategy1 = await strategySvc.AddAsync(entity, cancellationToken1);
            parent._idMap[((BaseLogSource)strategy).Id] = strategy1.Id;
        }
        catch (Exception ex)
        {
            parent.LogError(ex);
        }
    }

    public async Task RemoveStrategy(StockSharp.Algo.Strategies.Strategy strategy, CancellationToken cancellationToken)
    {
        RemoteStrategyManager remoteStrategyManager = this;
        if (strategy == null)
            throw new ArgumentNullException(nameof(strategy));
        RemoteStrategyManager.RemoteStrategyController strategyController;
        
        if (!CollectionHelper.TryGetAndRemove<Guid, RemoteStrategyManager.RemoteStrategyController>(remoteStrategyManager._controllers, ((BaseLogSource)strategy).Id, out strategyController))
            return;

        strategyController.Dispose();
        if (!strategyController.CanSend)
            return;
        try
        {
            int num = await remoteStrategyManager._strategySvc.DeleteByUserIdAsync(Converter.To<string>((object)((BaseLogSource)strategy).Id), cancellationToken) ? 1 : 0;
            remoteStrategyManager._idMap.Remove(((BaseLogSource)strategy).Id);
        }
        catch (Exception ex)
        {
            remoteStrategyManager.LogError(ex);
        }
    }

    public void ProcessRemoteCommand(CommandResponse command)
    {
        Guid result;
        RemoteStrategyManager.RemoteStrategyController strategyController;
        if (!Guid.TryParse(command.UserId, out result) || !((SynchronizedDictionary<Guid, RemoteStrategyManager.RemoteStrategyController>)this._controllers).TryGetValue(result, out strategyController))
            return;
        strategyController.Process(command);
    }

    private async Task RefreshRemoteEnable(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

        //RemoteStrategyManager remoteStrategyManager = this;
        //if (!remoteStrategyManager.IsRemoteEnabled)
        //    return;
        //try
        //{
        //    if (remoteStrategyManager._app == null)
        //    {
        //        App webApp = remoteStrategyManager._createApp();
        //        IAppService appSvc1 = remoteStrategyManager._appSvc;
        //        long? count = new long?(1L);
        //        string userId = webApp.UserId;
        //        ComparisonOperator? nullable = new ComparisonOperator?((ComparisonOperator)0);
        //        CancellationToken cancellationToken1 = cancellationToken;
        //        bool? deleted = new bool?();
        //        bool? orderByDesc = new bool?();
        //        bool? totalCount = new bool?();
        //        DateTime? creationStart = new DateTime?();
        //        DateTime? creationEnd = new DateTime?();
        //        long? clientId = new long?();
        //        string like = userId;
        //        ComparisonOperator? likeCompare = nullable;
        //        CancellationToken cancellationToken2 = cancellationToken1;
        //        App app1 = ((await appSvc1.FindAsync(count: count, deleted: deleted, orderByDesc: orderByDesc, totalCount: totalCount, creationStart: creationStart, creationEnd: creationEnd, clientId: clientId, like: like, likeCompare: likeCompare, cancellationToken: cancellationToken2)).Items).FirstOrDefault<App>((Func<App, bool>)(a => a.UserId == webApp.UserId));
        //        if (app1 != null)
        //        {
        //            remoteStrategyManager._app = app1;
        //        }
        //        else
        //        {
        //            App app2 = await remoteStrategyManager._appSvc.AddAsync(webApp, cancellationToken);
        //            remoteStrategyManager._app = app2;
        //        }
        //        IAppService appSvc2 = remoteStrategyManager._appSvc;
        //        App status = new App();
        //        status.Id = remoteStrategyManager._app.Id;
        //        status.State = new SubscriptionStates?(SubscriptionStates.Online);
        //        CancellationToken cancellationToken3 = cancellationToken;
        //        await appSvc2.UpdateStatusAsync(status, cancellationToken3);
        //    }
        //    // ISSUE: reference to a compiler-generated method
        //    foreach (var strategy in (await remoteStrategyManager._strategySvc.GetPaginatedDataAsync(new Func<IStrategyService, int, int, CancellationToken, Task<BaseEntitySet<StockSharp.Web.DomainModel.Strategy>>>(remoteStrategyManager.\u003CRefreshRemoteEnable\u003Eb__19_1), cancellationToken: cancellationToken)).Where<StockSharp.Web.DomainModel.Strategy>((Func<StockSharp.Web.DomainModel.Strategy, bool>)(s => !StringHelper.IsEmpty(s.UserId))))
        //    {
        //        Guid result;
        //        if (Guid.TryParse(strategy.UserId, out result))
        //            remoteStrategyManager._idMap[result] = strategy.Id;
        //    }

        //    var strategyList1 = new List<StockSharp.Web.DomainModel.Strategy>();
        //    var toUpdate = new List<StockSharp.Web.DomainModel.Strategy>();

        //    foreach (var strategyController in (remoteStrategyManager._controllers.CachedValues).Where(c => c.Settings.RemoteControl))
        //    {
        //        StockSharp.Algo.Strategies.Strategy strategy1 = strategyController.Strategy;
        //        long num;
        //        if (!remoteStrategyManager._idMap.TryGetValue(((BaseLogSource)strategy1).Id, out num))
        //        {
        //            List<StockSharp.Web.DomainModel.Strategy> strategyList2 = strategyList1;
        //            StockSharp.Web.DomainModel.Strategy strategy2 = new StockSharp.Web.DomainModel.Strategy();
        //            strategy2.UserId = Converter.To<string>((object)((BaseLogSource)strategy1).Id);
        //            strategy2.Name = ((BaseLogSource)strategy1).Name;
        //            App app = new App();
        //            app.Id = remoteStrategyManager._app.Id;
        //            strategy2.App = app;
        //            strategyList2.Add(strategy2);
        //        }
        //        else
        //        {
        //            List<StockSharp.Web.DomainModel.Strategy> strategyList3 = toUpdate;
        //            StockSharp.Web.DomainModel.Strategy strategy3 = new StockSharp.Web.DomainModel.Strategy();
        //            strategy3.Id = num;
        //            strategy3.State = new SubscriptionStates?(RemoteStrategyManager.ToSubscriptionState(strategy1.ProcessState));
        //            strategyList3.Add(strategy3);
        //        }
        //    }
        //    foreach (StockSharp.Web.DomainModel.Strategy entity in strategyList1)
        //    {
        //        StockSharp.Web.DomainModel.Strategy strategy = await remoteStrategyManager._strategySvc.AddAsync(entity, cancellationToken);
        //        remoteStrategyManager._idMap[Converter.To<Guid>((object)strategy.UserId)] = strategy.Id;
        //    }
        //    foreach (StockSharp.Web.DomainModel.Strategy strategy4 in toUpdate)
        //    {
        //        IStrategyService strategySvc = remoteStrategyManager._strategySvc;
        //        StrategyUpdateData status = new StrategyUpdateData();
        //        StockSharp.Web.DomainModel.Strategy strategy5 = new StockSharp.Web.DomainModel.Strategy();
        //        strategy5.Id = strategy4.Id;
        //        strategy5.UserId = strategy4.UserId;
        //        strategy5.State = strategy4.State;
        //        status.Strategy = strategy5;
        //        CancellationToken cancellationToken4 = cancellationToken;
        //        await strategySvc.UpdateStatusAsync(status, cancellationToken4);
        //    }
        //    toUpdate = (List<StockSharp.Web.DomainModel.Strategy>)null;
        //}
        //catch (Exception ex)
        //{
        //    remoteStrategyManager.LogError(ex);
        //}
    }

    private static SubscriptionStates ToSubscriptionState(ProcessStates state)
    {
        switch (state)
        {
            case ProcessStates.Stopped:
                return (SubscriptionStates)0;
            case ProcessStates.Stopping:
                return (SubscriptionStates)1;
            case ProcessStates.Started:
                return (SubscriptionStates)4;
            default:
                throw new ArgumentOutOfRangeException(nameof(state));
        }
    }

    public async ValueTask DisposeAsync()
    {
        RemoteStrategyManager remoteStrategyManager = this;
        if (remoteStrategyManager._app != null)
        {
            IAppService appSvc = remoteStrategyManager._appSvc;
            App status = new App();
            status.Id = remoteStrategyManager._app.Id;
            status.State = new SubscriptionStates?((SubscriptionStates)0);
            CancellationToken cancellationToken = new CancellationToken();
            await appSvc.UpdateStatusAsync(status, cancellationToken);
        }
      ((Disposable)remoteStrategyManager).Dispose();
    }

    private class RemoteStrategyController : Disposable
    {
        private readonly RemoteStrategyManager _parent;

        public RemoteStrategyController(
          RemoteStrategyManager parent,
          StockSharp.Algo.Strategies.Strategy strategy,
          RemoteStrategySettings settings)
        {
            this._parent = parent ?? throw new ArgumentNullException(nameof(parent));
            this.Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.Strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            ((BaseLogSource)this.Strategy).Log += new Action<LogMessage>(this.OnLog);
            this.Strategy.OrderReceived += new Action<StockSharp.BusinessEntities.Subscription, Order>(this.OnOrderReceived);
            this.Strategy.OwnTradeReceived += new Action<StockSharp.BusinessEntities.Subscription, MyTrade>(this.OnOwnTradeReceived);
            this.Strategy.PositionReceived += new Action<StockSharp.BusinessEntities.Subscription, Position>(this.OnPositionReceived);
            this.Strategy.PnLReceived2 += new Action<StockSharp.BusinessEntities.Subscription, Portfolio, DateTimeOffset, Decimal, Decimal?, Decimal?>(this.OnPnLReceived);
            this.Strategy.ProcessStateChanged += new Action<StockSharp.Algo.Strategies.Strategy>(this.OnProcessStateChanged);
        }

        protected override void DisposeManaged()
        {
            ((BaseLogSource)this.Strategy).Log -= new Action<LogMessage>(this.OnLog);
            this.Strategy.OrderReceived -= new Action<StockSharp.BusinessEntities.Subscription, Order>(this.OnOrderReceived);
            this.Strategy.OwnTradeReceived -= new Action<StockSharp.BusinessEntities.Subscription, MyTrade>(this.OnOwnTradeReceived);
            this.Strategy.PositionReceived -= new Action<StockSharp.BusinessEntities.Subscription, Position>(this.OnPositionReceived);
            this.Strategy.PnLReceived2 -= new Action<StockSharp.BusinessEntities.Subscription, Portfolio, DateTimeOffset, Decimal, Decimal?, Decimal?>(this.OnPnLReceived);
            this.Strategy.ProcessStateChanged -= new Action<StockSharp.Algo.Strategies.Strategy>(this.OnProcessStateChanged);
            base.DisposeManaged();
        }

        public RemoteStrategySettings Settings { get; }

        public StockSharp.Algo.Strategies.Strategy Strategy { get; }

        private long? TryGetWebId()
        {
            return CollectionHelper.TryGetValue2<Guid, long>((IDictionary<Guid, long>)this._parent._idMap, ((BaseLogSource)this.Strategy).Id);
        }

        public bool CanSend
        {
            get
            {
                return this.Settings.RemoteControl && this._parent.IsRemoteEnabled && !WebApiServicesRegistry.Offline;
            }
        }

        private void OnPnLReceived(
          StockSharp.BusinessEntities.Subscription subscription,
          Portfolio portfolio,
          DateTimeOffset time,
          Decimal realized,
          Decimal? unrealized,
          Decimal? commission)
        {
            if (!this.CanSend || !this.Settings.LogEquity)
                return;
            this.UpdateStatus(pnl: new StrategyPnL()
            {
                Realized = realized,
                Unrealized = unrealized,
                Commission = commission
            });
        }

        private void OnPositionReceived(StockSharp.BusinessEntities.Subscription subscription, Position position)
        {
            if (!this.CanSend || !this.Settings.LogPositions)
                return;
            Decimal? currentValue = position.CurrentValue;
            if (!currentValue.HasValue)
                return;
            SecurityId securityId = position.Security.Id.ToSecurityId(null);
            StrategyPosition position1 = new StrategyPosition();
            position1.Security = new InstrumentInfo()
            {
                Code = securityId.SecurityCode,
                Board = securityId.BoardCode
            };
            position1.Account = new StrategyAccount()
            {
                Name = position.Portfolio.Name
            };
            StrategyPosition strategyPosition = position1;
            StrategyPositionChange[] strategyPositionChangeArray = new StrategyPositionChange[1];
            StrategyPositionChange strategyPositionChange = new StrategyPositionChange();
            currentValue = position.CurrentValue;
            strategyPositionChange.Value = currentValue.Value;
            strategyPositionChangeArray[0] = strategyPositionChange;
            strategyPosition.Changes = strategyPositionChangeArray;
            this.UpdateStatus(position: position1);
        }

        private void OnOwnTradeReceived(StockSharp.BusinessEntities.Subscription subscription, MyTrade trade)
        {
            if (!this.CanSend || !this.Settings.LogTrades)
                return;
            StrategyTrade trade1 = new StrategyTrade();
            StrategyOrder strategyOrder = new StrategyOrder();
            long? id = trade.Order.Id;
            string stringId;
            if (id.HasValue)
            {
                id = trade.Order.Id;
                stringId = Converter.To<string>((object)id.Value);
            }
            else
                stringId = trade.Order.StringId;
            strategyOrder.UserId = stringId;
            trade1.Order = strategyOrder;
            trade1.Price = trade.Trade.Price;
            trade1.Volume = trade.Trade.Volume;
            this.UpdateStatus(trade: trade1);
        }

        private void OnOrderReceived(StockSharp.BusinessEntities.Subscription subscription, Order order)
        {
            if (!this.CanSend || !this.Settings.LogOrders || order.State != OrderStates.Active && order.State != OrderStates.Done)
                return;

            SecurityId securityId = order.Security.Id.ToSecurityId(null);
            this.UpdateStatus(order: new StrategyOrder()
            {
                Price = order.Price,
                Account = new StrategyAccount()
                {
                    Name = order.Portfolio.Name
                },
                Type = order.Type.GetValueOrDefault(),
                Volume = order.Volume,
                Balance = order.Balance,
                Side = order.Side,
                Security = new InstrumentInfo()
                {
                    Code = securityId.SecurityCode,
                    Board = securityId.BoardCode
                },
                State = order.State,
                Name = order.Comment,
                UserId = !order.Id.HasValue ? order.StringId : Converter.To<string>((object)order.Id.Value)
            });
        }

        private void OnProcessStateChanged(StockSharp.Algo.Strategies.Strategy strategy)
        {
            if (!this.CanSend || strategy != this.Strategy)
                return;
            this.UpdateStatus(new SubscriptionStates?(RemoteStrategyManager.ToSubscriptionState(this.Strategy.ProcessState)));
        }

        private void UpdateStatus(
          SubscriptionStates? state = null,
          StrategyOrder order = null,
          StrategyTrade trade = null,
          StrategyPosition position = null,
          StrategyPnL pnl = null)
        {
            try
            {
                long? webId1 = this.TryGetWebId();
                if (!webId1.HasValue)
                    return;
                long webId = webId1.GetValueOrDefault();
                AsyncContext.Run((Func<Task>)(() =>
                {
                    IStrategyService strategySvc = this._parent._strategySvc;
                    StrategyUpdateData status = new StrategyUpdateData();
                    status.Strategy = new StockSharp.Web.DomainModel.Strategy()
                    {
                        Id = webId,
                        State = state,
                        PnL = pnl
                    };
                    status.Order = order;
                    status.Trade = trade;
                    status.Position = position;
                    CancellationToken cancellationToken = new CancellationToken();
                    return strategySvc.UpdateStatusAsync(status, cancellationToken);
                }));
            }
            catch (Exception ex)
            {
                LoggingHelper.AddErrorLog((ILogReceiver)this._parent, ex);
            }
        }

        private void OnLog(LogMessage message)
        {
            if (!this.CanSend || message.Level < this.Settings.RemoteLogLevel)
                return;
            ITelegramChannel channel = (ITelegramChannel)((object)this.Settings.RemoteChannel ?? (object)WebApiHelper.DefaultClientSocial);
            if (channel == null)
                return;
            if (!this._parent._uniqueMsgs.TryAdd(message.Message))
                return;
            try
            {
                AsyncContext.Run<(long, bool, string, string)[]>((Func<Task<(long, bool, string, string)[]>>)(() => this._parent._socialSvc.SendAsync($"Task {((BaseLogSource)this.Strategy).Name}: ({message.Level}) {message.Message}", new long[1]
                {
          channel.Id
                }, Array.Empty<long>())));
            }
            catch (Exception ex)
            {
                LoggingHelper.AddErrorLog((ILogReceiver)this._parent, ex);
            }
        }

        public void Process(CommandResponse response)
        {
            CommandInfo command = response.Command;
            CommandMessage cmdMsg = new CommandMessage()
            {
                Command = command.Command,
                Scope = command.Scope,
                ObjectId = Converter.To<string>((object)response.EntityId)
            };
            if (command.Args != null)
                CollectionHelper.AddRange<KeyValuePair<string, string>>((ICollection<KeyValuePair<string, string>>)cmdMsg.Parameters, (IEnumerable<KeyValuePair<string, string>>)command.Args);
            this.Strategy.ApplyCommand(cmdMsg);
        }
    }
}
