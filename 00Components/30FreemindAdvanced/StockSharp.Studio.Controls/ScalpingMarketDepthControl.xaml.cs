// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ScalpingMarketDepthControl
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using DevExpress.Xpf.Bars;
using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using DataType = StockSharp.Messages.DataType;

#nullable enable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "MarketDepth", Description = "ScalpingMarketDepthControl")]
[VectorIcon("UpDown")]
[Doc("topics/terminal/user_interface/components/chart.html")]
public partial class ScalpingMarketDepthControl :
  BaseStudioControl,
  IStudioCommandScope,
  IComponentConnector
{
    private readonly
#nullable disable
    CachedSynchronizedDictionary<long, Order> _activeOrders = new CachedSynchronizedDictionary<long, Order>();
    private readonly SynchronizedSet<Subscription> _subscriptions = new SynchronizedSet<Subscription>();
    private bool _isLoaded;
    private double? _prevBid;
    private double? _prevAsk;
    private readonly SubscriptionManager _subscriptionManager;
    public static readonly DependencyProperty OrdersBuyCountProperty = DependencyProperty.Register(nameof(OrdersBuyCount), typeof(int), typeof(ScalpingMarketDepthControl));
    public static readonly DependencyProperty OrdersSellCountProperty = DependencyProperty.Register(nameof(OrdersSellCount), typeof(int), typeof(ScalpingMarketDepthControl));

    bool IStudioCommandScope.UseParentScope => true;

    bool IStudioCommandScope.RouteToGlobalScope => false;

    public BuySellSettings Settings => this.BuySellPanel.Settings;

    public int OrdersBuyCount
    {
        get => (int)this.GetValue(ScalpingMarketDepthControl.OrdersBuyCountProperty);
        private set => this.SetValue(ScalpingMarketDepthControl.OrdersBuyCountProperty, (object)value);
    }

    public int OrdersSellCount
    {
        get => (int)this.GetValue(ScalpingMarketDepthControl.OrdersSellCountProperty);
        private set
        {
            this.SetValue(ScalpingMarketDepthControl.OrdersSellCountProperty, (object)value);
        }
    }

    public ScalpingMarketDepthControl()
    {
        DelayActionHelper delayActionHelper = new DelayActionHelper()
        {
            Interval = TimeSpan.FromSeconds(2.5).TotalMilliseconds
        };
        this.InitializeComponent();
        this._subscriptionManager = new SubscriptionManager((IStudioControl)this);
        this.BuySellPanel.Host = (IStudioControl)this;
        this.Settings.PropertyChanged += (PropertyChangedEventHandler)((s, e) =>
        {
            string propertyName = e.PropertyName;
            bool needChangeTitle = propertyName == "Security";
            bool needResubscribe = needChangeTitle || propertyName == "Depth" || propertyName == "ShowChart";
            if (needResubscribe)
                delayActionHelper.Start(new Action(this.Resubscribe));
            if (needResubscribe | needChangeTitle)
                ((DispatcherObject)this).GuiAsync((Action)(() =>
            {
                if (needResubscribe)
                    this.ClearChart();
                if (!needChangeTitle)
                    return;
                this.UpdateTitle();
            }));
            if (this._isLoaded)
                return;
            this.RaiseChangedCommand();
        });
        this.MdControl.LayoutChanged += RaiseChangedCommand;
        this.MdControl.RegisteringOrder += new Action<Sides, Decimal>(this.OnRegisteringOrder);
        this.MdControl.MovingOrder += new Action<Order, Decimal>(this.OnMovingOrder);
        this.MdControl.CancelingOrder += new Action<Order>(this.OnCancelingOrder);
        this.MdControl.SelectedQuoteChanged += new Action<QuoteChange?>(this.OnSelectedQuoteChanged);
        this.Register<EntityCommand<IOrderBookMessage>>((object)this, false, (Action<EntityCommand<IOrderBookMessage>>)(cmd =>
        {
            if (!((BaseCollection<Subscription, ISet<Subscription>>)this._subscriptions).Contains(cmd.Subscription))
                return;
            this.MdControl.UpdateDepth(cmd.Entity, this.Settings.Security);
            if (!this.Settings.ShowChart)
                return;
            this.UpdateDepthChartWithTick(cmd.Entity, (ITickTradeMessage)null);
        }));
        this.Register<EntityCommand<ITickTradeMessage>>((object)this, false, (Action<EntityCommand<ITickTradeMessage>>)(cmd =>
        {
            if (!this.Settings.ShowChart || !((BaseCollection<Subscription, ISet<Subscription>>)this._subscriptions).Contains(cmd.Subscription))
                return;
            this.UpdateDepthChartWithTick((IOrderBookMessage)null, cmd.Entity);
        }));
        this.Register<ResetedCommand>((object)this, true, (Action<ResetedCommand>)(cmd =>
        {
            this.MdControl.Clear();
            this.ClearChart();
        }));
        this.Register<OrderCommand>((object)this, true, (Action<OrderCommand>)(cmd =>
        {
            Order entity = cmd.Entity;
            if (entity.Type.GetValueOrDefault() == OrderTypes.Conditional || entity.Security != this.Settings.Security)
                return;
            if (cmd.State == OrderStates.Active)
            {
                if (((IDictionary<long, Order>)this._activeOrders).TryAdd<long, Order>(entity.TransactionId, entity))
                {
                    if (entity.Side == Sides.Buy)
                        ++this.OrdersBuyCount;
                    else
                        ++this.OrdersSellCount;
                }
            }
            else if ((cmd.State == OrderStates.Done || cmd.State == OrderStates.Failed) && ((SynchronizedDictionary<long, Order>)this._activeOrders).Remove(entity.TransactionId))
            {
                if (entity.Side == Sides.Buy)
                    --this.OrdersBuyCount;
                else
                    --this.OrdersSellCount;
            }
            this.MdControl.ProcessOrder(entity, cmd.Price, cmd.Balance, cmd.State);
        }));
        this.Register<OrderFailCommand>((object)this, false, (Action<OrderFailCommand>)(cmd => this.MdControl.ProcessOrderFail(cmd.Entity, cmd.State)));
        this.Register<EntitiesRemovedCommand<Security>>((object)this, false, (Action<EntitiesRemovedCommand<Security>>)(cmd =>
        {
            bool flag = false;
            foreach (Security entity in cmd.Entities)
            {
                if (this.Settings.Security == entity)
                {
                    this.Settings.Security = (Security)null;
                    this.MdControl.Clear();
                    flag = true;
                    break;
                }
            }
            if (!flag)
                return;
            this.RaiseChangedCommand();
        }));
        this.Register<FirstInitSecuritiesCommand>((object)this, true, (Action<FirstInitSecuritiesCommand>)(cmd =>
        {
            if (this.Settings.Security != null)
                return;
            this.Settings.Security = cmd.Securities.First<Security>();
        }));
        this.Register<FirstInitPortfoliosCommand>((object)this, true, (Action<FirstInitPortfoliosCommand>)(cmd =>
        {
            if (this.Settings.Portfolio != null)
                return;
            this.Settings.Portfolio = cmd.Portfolios.First<Portfolio>();
        }));
        this.WhenLoaded((Action)(() => this._subscriptionManager.CreateSubscription(DataType.Transactions)));
    }

    public override void SendCommand(IStudioCommand command)
    {
        if (command is CancelAllOrdersCommand)
            this.MdControl.CancelOrders();
        else
            base.SendCommand(command);
    }

    private void OnSelectedQuoteChanged(QuoteChange? quote)
    {
        if (!quote.HasValue)
            return;
        BuySellSettings settings = this.BuySellPanel.Settings;
        QuoteChange quoteChange = quote.Value;
        Decimal price = (quoteChange).Price;
        settings.LimitPrice = price;
    }

    private void OnCancelingOrder(Order order)
    {
        new CancelOrderCommand(order).Process((object)this);
    }

    private void OnMovingOrder(Order order, Decimal newPrice)
    {
        Order order1 = order.ReRegisterClone(new Decimal?(newPrice));
        if (BaseStudioControl.Connector.IsOrderReplaceable(order).GetValueOrDefault())
        {
            new ReRegisterOrderCommand(order, order1).Process((object)this);
        }
        else
        {
            new CancelOrderCommand(order).Process((object)this);
            new RegisterOrderCommand(order1).Process((object)this);
        }
    }

    private void Resubscribe()
    {
        foreach (Subscription subscription in CollectionHelper.CopyAndClear<Subscription>((ICollection<Subscription>)this._subscriptions))
            this._subscriptionManager.RemoveSubscription(subscription);
        this.MdControl.Clear();
        int? depth = this.Settings.Depth;
        int? maxDepth = !depth.HasValue ? new int?() : new int?(Math.Max(1, Math.Min(200, depth.Value)));
        Security security = this.Settings.Security;
        if (security == null)
            return;
        ((BaseCollection<Subscription, ISet<Subscription>>)this._subscriptions).Add(this._subscriptionManager.CreateSubscription(security, DataType.MarketDepth, (Action<Subscription>)(s => s.MarketData.MaxDepth = maxDepth)));
        if (!this.Settings.ShowChart)
            return;
        ((BaseCollection<Subscription, ISet<Subscription>>)this._subscriptions).Add(this._subscriptionManager.CreateSubscription(security, DataType.Ticks));
    }

    private void OnRegisteringOrder(Sides side, Decimal price)
    {
        if (this.Settings.Portfolio == null)
        {
            PortfolioPickerWindow wnd = new PortfolioPickerWindow()
            {
                Portfolios = BaseStudioControl.PortfolioDataSource
            };
            if (wnd.ShowModal((DependencyObject)this))
                this.Settings.Portfolio = wnd.SelectedPortfolio;
        }
        if (this.Settings.Portfolio == null)
            return;
        new RegisterOrderCommand(this.CreateOrder(side, price, (OrderTypes)0)).Process((object)this);
    }

    protected override void OnActiveLanguageChanged() => this.UpdateTitle();

    private void UpdateTitle()
    {
        this.Title = this.Settings.Security != null ? this.Settings.Security.Id : LocalizedStrings.MarketDepth;
    }

    private Order CreateOrder(Sides side, Decimal price = 0M, OrderTypes type = OrderTypes.Market)
    {
        return new Order()
        {
            Portfolio = this.Settings.Portfolio,
            Security = this.Settings.Security,
            Side = side,
            Price = price,
            Volume = this.Settings.Volume,
            Type = new OrderTypes?(type)
        };
    }

    private void ClearChart()
    {
        this._prevAsk = this._prevBid = new double?();
        this.Chart.Clear();
        ((SynchronizedDictionary<long, Order>)this._activeOrders).Clear();
        this.OrdersBuyCount = this.OrdersSellCount = 0;
    }

    private void UpdateDepthChartWithTick(IOrderBookMessage book, ITickTradeMessage tick)
    {
        double? bidPrice = new double?();
        double? askPrice = new double?();
        if (book != null)
        {
            QuoteChange? quoteChange = StockSharp.Messages.Extensions.GetBestBid(book);

            if (quoteChange.HasValue)
            {
                bidPrice = (double)quoteChange.GetValueOrDefault().Price;
            }

            quoteChange = StockSharp.Messages.Extensions.GetBestAsk(book);

            if (quoteChange.HasValue)
            {
                askPrice = (double)quoteChange.GetValueOrDefault().Price;
            }
        }
        (Sides, double, double)? tick1 = new (Sides, double, double)?();
        if (tick != null)
        {
            Sides? originSide = tick.OriginSide;
            object obj;
            if (!originSide.HasValue)
            {
                bool? isUpTick = tick.IsUpTick;
                if (isUpTick.HasValue)
                {
                    isUpTick = tick.IsUpTick;
                    obj = isUpTick.Value ? (object)0 : (object)1;
                }
                else
                {
                    double price = (double)tick.Price;
                    double? prevBid = this._prevBid;
                    double valueOrDefault = prevBid.GetValueOrDefault();
                    obj = price > valueOrDefault & prevBid.HasValue ? (object)0 : (object)1;
                }
            }
            else
                obj = (object)originSide.GetValueOrDefault();
            tick1 = new (Sides, double, double)?(((Sides)obj, (double)tick.Price, (double)tick.Volume));
        }
        this.Chart.UpdateChart(book != null ? ((IServerTimeMessage)book).ServerTime.UtcDateTime : ((IServerTimeMessage)tick).ServerTime.UtcDateTime, bidPrice, askPrice, tick1);
        this._prevBid = bidPrice ?? this._prevBid;
        this._prevAsk = askPrice ?? this._prevAsk;
    }

    private void Orders_ItemDoubleClick(object sender, ItemClickEventArgs e)
    {
        if (!(sender is BarStaticItem barStaticItem))
            return;
        object tag = barStaticItem.Tag;
        if (!(tag is Sides))
            return;
        Sides side = (Sides)tag;
        foreach (Order order in ((IEnumerable<Order>)this._activeOrders.CachedValues).Where<Order>((Func<Order, bool>)(o => o.Side == side)))
            this.OnCancelingOrder(order);
    }

    public override void Dispose(CloseReason reason)
    {
        this.MdControl.LayoutChanged -= RaiseChangedCommand;
        this.MdControl.RegisteringOrder -= new Action<Sides, Decimal>(this.OnRegisteringOrder);
        this.MdControl.MovingOrder -= new Action<Order, Decimal>(this.OnMovingOrder);
        this.MdControl.CancelingOrder -= new Action<Order>(this.OnCancelingOrder);
        this.MdControl.SelectedQuoteChanged -= new Action<QuoteChange?>(this.OnSelectedQuoteChanged);
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }

    public override void Save(SettingsStorage settings)
    {
        base.Save(settings);
        settings.SetValue<SettingsStorage>("MdControl", PersistableHelper.Save((IPersistable)this.MdControl));
        settings.SetValue<SettingsStorage>("BuySellPanel", PersistableHelper.Save((IPersistable)this.BuySellPanel));
    }

    public override void Load(SettingsStorage settings)
    {
        this._isLoaded = true;
        try
        {
            base.Load(settings);
            PersistableHelper.LoadIfNotNull((IPersistable)this.MdControl, settings, "MdControl");
            PersistableHelper.LoadIfNotNull((IPersistable)this.BuySellPanel, settings, "BuySellPanel");
        }
        finally
        {
            this._isLoaded = false;
        }
    }


}
