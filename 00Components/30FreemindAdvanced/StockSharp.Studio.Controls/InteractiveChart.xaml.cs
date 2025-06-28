// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.InteractiveChart
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Services;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;
using DataType = StockSharp.Messages.DataType;

#nullable enable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Chart", Description = "CandleChartPanel")]
[VectorIcon("CandleStick")]
[Doc("topics/designer/user_interface/components/chart.html")]
public partial class InteractiveChart : BaseStudioControl, IComponentConnector
{
    private readonly
#nullable disable
    Dictionary<Subscription, (IChartCandleElement elem, List<ICandleMessage> candles, Subscription subscription, DateTimeOffset last)> _candles = new Dictionary<Subscription, (IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset)>();
    private readonly Dictionary<IChartIndicatorElement, InteractiveChart.IndicatorPair> _indicators = new Dictionary<IChartIndicatorElement, InteractiveChart.IndicatorPair>();
    private readonly Dictionary<Subscription, (IChartIndicatorElement indicator, InteractiveChart.IndicatorPair pair)[]> _indicatorsBySeries = new Dictionary<Subscription, (IChartIndicatorElement, InteractiveChart.IndicatorPair)[]>();
    private readonly Dictionary<Order, IChartActiveOrdersElement> _chartOrders = new Dictionary<Order, IChartActiveOrdersElement>();
    private readonly Dictionary<IChartAnnotationElement, ChartDrawData.AnnotationData> _annElements = new Dictionary<IChartAnnotationElement, ChartDrawData.AnnotationData>();
    private readonly Dictionary<Subscription, IChartOrderElement> _orderElements = new Dictionary<Subscription, IChartOrderElement>();
    private readonly Dictionary<Subscription, IChartTradeElement> _tradeElements = new Dictionary<Subscription, IChartTradeElement>();
    private readonly Dictionary<Security, HashSet<Subscription>> _secSubs = new Dictionary<Security, HashSet<Subscription>>();
    private bool _loading;
    private bool _isInteracted;

    public InteractiveChart()
    {
        this.InitializeComponent();
        this.ChartPanel.SettingsChanged += (Action)(() =>
        {
            if (!this.CanTrackChanges())
                return;
            this.RaiseChangedCommand();
        });
        this.ChartPanel.AreaAdded += new Action<IChartArea>(this.AreasOnAdded);
        this.ChartPanel.AreaRemoved += new Action<IChartArea>(this.AreasOnRemoved);
        this.ChartPanel.DisableIndicatorReset = true;
        this.Register<EntityCommand<MyTrade>>((object)this, true, new Action<EntityCommand<MyTrade>>(this.OnMyTradeCommand));
        this.Register<CandleCommand>((object)this, false, new Action<CandleCommand>(this.OnCandleCommand));
        this.Register<SelectCommand>((object)this, true, (Action<SelectCommand>)(cmd =>
        {
            if (this.ChartPanel.OrderSettings.Security != null || !(cmd.Instance is Security instance2))
                return;
            this.ChartPanel.OrderSettings.Security = instance2;
        }));
        this.Register<OrderCommand>((object)this, true, (Action<OrderCommand>)(cmd =>
        {
            this.OnOrderCommand((EntityCommand<Order>)cmd);
            if (!this.ChartPanel.OrderCreationMode)
                return;
            Order order = cmd.Entity;
            IChartActiveOrdersElement element1;
            if (!this._chartOrders.TryGetValue(order, out element1))
            {
                if (StockSharp.Messages.Extensions.IsFinal(order.State))
                    return;
                IChartCandleElement chartCandleElement = this.ChartPanel.GetElements<IChartCandleElement>().FirstOrDefault<IChartCandleElement>((Func<IChartCandleElement, bool>)(e =>
            {
                Subscription subscription = this.ChartPanel.TryGetSubscription((IChartElement)e);
                return (subscription != null ? subscription.TryGetSecurity() : (Security)null) == order.Security;
            }));
                if (chartCandleElement == null)
                    return;
                element1 = ((IEnumerable)chartCandleElement.ChartArea.Elements).OfType<IChartActiveOrdersElement>().FirstOrDefault<IChartActiveOrdersElement>();
                if (element1 == null)
                {
                    element1 = this.ChartPanel.CreateActiveOrdersElement();
                    this.ChartPanel.AddElement(chartCandleElement.ChartArea, (IChartElement)element1);
                }
                this._chartOrders.Add(order, element1);
            }
            ChartPanel chartPanel = this.ChartPanel;
            IChartDrawData data1 = this.ChartPanel.CreateData();
            IChartActiveOrdersElement element2 = element1;
            Order order1 = order;
            Decimal? nullable = new Decimal?(cmd.Price);
            bool? isFrozen = new bool?();
            bool? isError = new bool?();
            Decimal? price = nullable;
            Decimal? balance = new Decimal?();
            OrderStates? state = new OrderStates?();
            IChartDrawData data2 = data1.Add(element2, order1, isFrozen, isError: isError, price: price, balance: balance, state: state);
            chartPanel.Draw(data2);
        }));
        this.Register<OrderFailCommand>((object)this, true, (Action<OrderFailCommand>)(cmd =>
        {
            Order order2 = cmd.Entity.Order;
            IChartActiveOrdersElement activeOrdersElement;
            if (!this._chartOrders.TryGetValue(order2, out activeOrdersElement))
                return;
            ChartPanel chartPanel = this.ChartPanel;
            IChartDrawData data3 = this.ChartPanel.CreateData();
            IChartActiveOrdersElement element = activeOrdersElement;
            Order order3 = order2;
            bool? nullable = new bool?(true);
            bool? isFrozen = new bool?();
            bool? isError = nullable;
            Decimal? price = new Decimal?();
            Decimal? balance = new Decimal?();
            OrderStates? state = new OrderStates?();
            IChartDrawData data4 = data3.Add(element, order3, isFrozen, isError: isError, price: price, balance: balance, state: state);
            chartPanel.Draw(data4);
            if (cmd.Type != OrderFailTypes.Register)
                return;
            this._chartOrders.Remove(order2);
        }));
        this.Register<ChartAddElementCommand>((object)this, false, (Action<ChartAddElementCommand>)(cmd => this.OnChartAddElementCommand(cmd.Element, cmd.Subscription, cmd.Indicator)));
        this.Register<ChartRemoveElementCommand>((object)this, false, (Action<ChartRemoveElementCommand>)(cmd => this.OnChartRemoveElementCommand(cmd.Element, cmd.Subscription)));
        this.Register<ChartResetElementCommand>((object)this, false, new Action<ChartResetElementCommand>(this.OnChartResetElementCommand));
        this.Register<FirstInitSecuritiesCommand>((object)this, false, (Action<FirstInitSecuritiesCommand>)(cmd =>
        {
            if (this.ChartPanel.GetElements().Any<IChartElement>())
                return;
            IChartArea area = this.ChartPanel.Areas.FirstOrDefault<IChartArea>();
            if (area == null)
                return;
            Security security = cmd.Securities.First<Security>();
            Subscription subscription = new Subscription(StockSharp.Messages.Extensions.TimeFrame(TimeSpan.FromMinutes(1.0)), security)
            {
                From = new DateTimeOffset?(DateTimeOffset.UtcNow.AddDays(-5.0))
            };
            IChartCandleElement candleElement = this.ChartPanel.CreateCandleElement();
            candleElement.PriceStep = security.PriceStep;
            this.ChartPanel.AddElement(area, candleElement, subscription);
        }));
        this.Register<FirstInitPortfoliosCommand>((object)this, true, (Action<FirstInitPortfoliosCommand>)(cmd => this.ChartPanel.OrderSettings.Portfolio = cmd.Portfolios.First<Portfolio>()));
        this.ChartPanel.MinimumRange = 200;
        this.ChartPanel.IsInteracted = true;
        this.ChartPanel.OrderCreationMode = true;
        this.ChartPanel.FillIndicators();
        this.ChartPanel.SubscribeCandleElement += new Action<IChartCandleElement, Subscription>(this.Chart_OnSubscribeCandleElement);
        this.ChartPanel.SubscribeIndicatorElement += new Action<IChartIndicatorElement, Subscription, IIndicator>(this.Chart_OnSubscribeIndicatorElement);
        this.ChartPanel.SubscribeOrderElement += new Action<IChartOrderElement, Subscription>(this.Chart_OnSubscribeOrderElement);
        this.ChartPanel.SubscribeTradeElement += new Action<IChartTradeElement, Subscription>(this.Chart_OnSubscribeTradeElement);
        this.ChartPanel.UnSubscribeElement += new Action<IChartElement>(this.OnChartPanelUnSubscribeElement);
        this.ChartPanel.SecurityProvider = BaseStudioControl.SecurityProvider;
        ((INotifyPropertyChanged)this.ChartPanel).PropertyChanged += (PropertyChangedEventHandler)((_1, _2) => updateInteracted());
        updateInteracted();

        void updateInteracted() => this._isInteracted = this.ChartPanel.IsInteracted;
    }

    public override void Dispose(CloseReason reason)
    {
        foreach (Subscription key in CollectionHelper.WhereNotNull<Subscription>(this.ChartPanel.GetElements<IChartCandleElement>().Select<IChartCandleElement, Subscription>(new Func<IChartCandleElement, Subscription>(this.ChartPanel.TryGetSubscription))))
        {
            (IChartCandleElement elem, List<ICandleMessage> candles, Subscription subscription, DateTimeOffset last) tuple;
            if (this._candles.TryGetValue(key, out tuple))
                new UnSubscribeCommand(tuple.subscription).Process((object)this);
        }
        this.ChartPanel.SubscribeCandleElement -= new Action<IChartCandleElement, Subscription>(this.Chart_OnSubscribeCandleElement);
        this.ChartPanel.SubscribeIndicatorElement -= new Action<IChartIndicatorElement, Subscription, IIndicator>(this.Chart_OnSubscribeIndicatorElement);
        this.ChartPanel.SubscribeOrderElement -= new Action<IChartOrderElement, Subscription>(this.Chart_OnSubscribeOrderElement);
        this.ChartPanel.SubscribeTradeElement -= new Action<IChartTradeElement, Subscription>(this.Chart_OnSubscribeTradeElement);
        this.ChartPanel.UnSubscribeElement -= new Action<IChartElement>(this.OnChartPanelUnSubscribeElement);
        base.Dispose(reason);
    }

    public override void FirstTimeInit()
    {
        base.FirstTimeInit();
        this.ChartPanel.AddArea();
    }

    private bool CanTrackChanges() => this._isInteracted && !this._loading;

    private void AreasOnRemoved(IChartArea area)
    {
        ((INotifyCollection<IChartAxis>)area.XAxises).Changed -= new Action(this.OnAxisesChanged);
        ((INotifyCollection<IChartAxis>)area.YAxises).Changed -= new Action(this.OnAxisesChanged);
        area.PropertyChanged -= new PropertyChangedEventHandler(this.OnAreaPropertyChanged);
        if (!this.CanTrackChanges())
            return;
        this.RaiseChangedCommand();
    }

    private void AreasOnAdded(IChartArea area)
    {
        ((INotifyCollection<IChartAxis>)area.XAxises).Changed += new Action(this.OnAxisesChanged);
        ((INotifyCollection<IChartAxis>)area.YAxises).Changed += new Action(this.OnAxisesChanged);
        area.PropertyChanged += new PropertyChangedEventHandler(this.OnAreaPropertyChanged);
        if (!this.CanTrackChanges())
            return;
        this.RaiseChangedCommand();
    }

    private void OnAreaPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (!this.CanTrackChanges() || !(e.PropertyName == "Height"))
            return;
        this.RaiseChangedCommand();
    }

    private void OnAxisesChanged()
    {
        if (!this.CanTrackChanges())
            return;
        this.RaiseChangedCommand();
    }

    private void Chart_OnSubscribeTradeElement(IChartTradeElement element, Subscription subscription)
    {
        this.OnChartAddElementCommand((IChartElement)element, subscription);
    }

    private void Chart_OnSubscribeOrderElement(IChartOrderElement element, Subscription subscription)
    {
        this.OnChartAddElementCommand((IChartElement)element, subscription);
    }

    private void Chart_OnSubscribeCandleElement(
      IChartCandleElement element,
      Subscription subscription)
    {
        this.OnChartAddElementCommand((IChartElement)element, subscription);
    }

    private void OnChartPanelUnSubscribeElement(IChartElement element)
    {
        this.OnChartRemoveElementCommand(element, this.ChartPanel.TryGetSubscription(element));
        if (!this.CanTrackChanges())
            return;
        this.RaiseChangedCommand();
    }

    private void Chart_OnSubscribeIndicatorElement(
      IChartIndicatorElement element,
      Subscription subscription,
      IIndicator indicator)
    {
        this.OnChartAddElementCommand((IChartElement)element, subscription, indicator);
    }

    private void OnChartResetElementCommand(ChartResetElementCommand command)
    {
        // ISSUE: object of a compiler-generated type is created
        this.ChartPanel.Reset(new IChartElement[1]
            {
                command.Element
            });

        lock (this._candles)
        {
            InteractiveChart.IndicatorPair tag = (InteractiveChart.IndicatorPair)command.Tag;
            ((IPersistable)tag.Working).Load(PersistableHelper.Save((IPersistable)tag.UI));
            (IChartCandleElement elem, List<ICandleMessage> candles, Subscription subscription, DateTimeOffset last) tuple;
            if (!this._candles.TryGetValue(tag.Subscription, out tuple))
                return;
            IChartDrawData data = this.ChartPanel.CreateData();
            foreach (ICandleMessage candle in tuple.candles)
                data.Group(candle.OpenTime).Add(command.Element, (object)tag.Working.Process(candle));
            this.ChartPanel.Draw(data);
            if (!this.CanTrackChanges())
                return;
            this.RaiseChangedCommand();
        }
    }

    private void OnChartAddElementCommand(IChartElement element, Subscription subscription, IIndicator indicator = null)
    {
        element.PropertyChanged += new PropertyChangedEventHandler(this.OnChartElementPropertyChanged);
        lock (this._candles)
        {
            switch (element)
            {
                case IChartCandleElement chartCandleElement:
                    if (this._candles.TryAdd(subscription, (chartCandleElement, new List<ICandleMessage>(), subscription, DateTimeOffset.MinValue)))
                    {
                        new SubscribeCommand(subscription).Process((object)this);
                        break;
                    }
                    break;
                case IChartIndicatorElement indicatorElement:
                    (IChartCandleElement elem, List<ICandleMessage> candles, Subscription subscription, DateTimeOffset last) tuple;
                    if (this._indicators.ContainsKey(indicatorElement) || !this._candles.TryGetValue(subscription, out tuple))
                        return;
                    InteractiveChart.IndicatorPair indicatorPair = new InteractiveChart.IndicatorPair(this, indicatorElement, indicator, subscription);
                    this._indicators.Add(indicatorElement, indicatorPair);
                    (IChartIndicatorElement, InteractiveChart.IndicatorPair)[] tupleArray1 = CollectionHelper.SafeAdd<Subscription, (IChartIndicatorElement, InteractiveChart.IndicatorPair)[]>((IDictionary<Subscription, (IChartIndicatorElement indicator, InteractiveChart.IndicatorPair pair)[]>)this._indicatorsBySeries, subscription, (Func<Subscription, (IChartIndicatorElement, InteractiveChart.IndicatorPair)[]>)(key => Array.Empty<(IChartIndicatorElement, InteractiveChart.IndicatorPair)>()));
                    Dictionary<Subscription, (IChartIndicatorElement, InteractiveChart.IndicatorPair)[]> indicatorsBySeries = this._indicatorsBySeries;
                    Subscription key1 = subscription;
                    (IChartIndicatorElement, InteractiveChart.IndicatorPair)[] tupleArray2 = tupleArray1;
                    (IChartIndicatorElement, InteractiveChart.IndicatorPair)[] valueTupleArray1 = new (IChartIndicatorElement, InteractiveChart.IndicatorPair)[1]
                    {
            (indicatorElement, indicatorPair)
                    };
                    int index = 0;
                    var valueTupleArray2 = new (IChartIndicatorElement, InteractiveChart.IndicatorPair)[tupleArray2.Length + valueTupleArray1.Length];

                    foreach (var pair1 in tupleArray2)
                    {
                        valueTupleArray2[index] = pair1;
                        ++index;
                    }

                    foreach (var pair2 in valueTupleArray1)
                    {
                        valueTupleArray2[index] = pair2;
                        ++index;
                    }
                    indicatorsBySeries[key1] = valueTupleArray2;
                    IChartDrawData data = this.ChartPanel.CreateData();
                    foreach (ICandleMessage candle in tuple.candles)
                        data.Group(candle.OpenTime).Add(indicatorElement, indicatorPair.Working.Process(candle));
                    this.ChartPanel.Draw(data);
                    break;
                case IChartOrderElement elem1:
                    tryAddTransElem<IChartOrderElement>(elem1, this._orderElements);
                    break;
                case IChartTradeElement elem2:
                    tryAddTransElem<IChartTradeElement>(elem2, this._tradeElements);
                    break;
            }
            if (this.ChartPanel.OrderSettings.Security == null)
                this.ChartPanel.OrderSettings.Security = subscription.TryGetSecurity();
        }
        if (!this.CanTrackChanges())
            return;
        this.RaiseChangedCommand();

        void tryAddTransElem<TTransElem>(TTransElem elem, Dictionary<Subscription, TTransElem> elems) where TTransElem : IChartTransactionElement
        {
            Security security = subscription.TryGetSecurity();
            if (security == null)
                return;
            HashSet<Subscription> subscriptionSet = CollectionHelper.SafeAdd<Security, HashSet<Subscription>>((IDictionary<Security, HashSet<Subscription>>)this._secSubs, security);
            bool flag = CollectionHelper.IsEmpty<Subscription>((ICollection<Subscription>)subscriptionSet);
            subscriptionSet.Add(subscription);
            elems.Add(subscription, elem);
            if (!flag)
                return;
            new SubscribeCommand(subscription).Process((object)this);
        }
    }

    private void OnChartRemoveElementCommand(IChartElement element, Subscription subscription)
    {
        element.PropertyChanged -= OnChartElementPropertyChanged;

        lock (this._candles)
        {
            IChartIndicatorElement ind = element as IChartIndicatorElement;

            if (ind != null)
            {
                this._indicators.Remove(ind);

                foreach (var keyValuePair in this._indicatorsBySeries.ToArray())
                {
                    var array = keyValuePair.Value.Where(t => t.Item1 != ind).ToArray();
                    if (array.Length == 0)
                        this._indicatorsBySeries.Remove(keyValuePair.Key);
                    else
                        this._indicatorsBySeries[keyValuePair.Key] = array;
                }
            }
            else if (element is IChartCandleElement)
            {
                if (!CollectionHelper.TryGetAndRemove(this._candles, subscription, out var valueTuple))
                    return;

                new UnSubscribeCommand(valueTuple.Item3).Process(this);

                if (this._candles.Count != 0 || subscription.TryGetSecurity() != this.ChartPanel.OrderSettings.Security)
                    return;

                this.GuiAsync((Action)(() =>
                {
                    this.ChartPanel.OrderSettings.Security = (Security)null;
                    foreach (var activeOrdersElement in element.PersistentChartArea.Elements.OfType<IChartActiveOrdersElement>())
                    {
                        this.ChartPanel.Reset(new IChartElement[1] { activeOrdersElement });

                        foreach (KeyValuePair<Order, IChartActiveOrdersElement> keyValuePair in this._chartOrders.ToArray<KeyValuePair<Order, IChartActiveOrdersElement>>())
                        {
                            if (keyValuePair.Value == activeOrdersElement)
                                this._chartOrders.Remove(keyValuePair.Key);
                        }
                    }
                }));
            }
            else if (element is IChartOrderElement elem1)
            {
                tryRemoveTransElem<IChartOrderElement>(elem1, this._orderElements);
            }
            else
            {
                if (!(element is IChartTradeElement elem))
                    return;
                tryRemoveTransElem<IChartTradeElement>(elem, this._tradeElements);
            }
        }

        void tryRemoveTransElem<TTransElem>(TTransElem elem, Dictionary<Subscription, TTransElem> elems) where TTransElem : IChartTransactionElement
        {
            Security security = subscription.TryGetSecurity();
            HashSet<Subscription> subscriptionSet;
            if (security == null || !elems.Remove(subscription) || !this._secSubs.TryGetValue(security, out subscriptionSet) || !subscriptionSet.Remove(subscription) || !CollectionHelper.IsEmpty<Subscription>((ICollection<Subscription>)subscriptionSet))
                return;
            this._secSubs.Remove(security);
            new UnSubscribeCommand(subscription).Process((object)this);
        }
    }

    private void OnChartElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        this.RaiseChangedCommand();
    }

    private void OnMyTradeCommand(EntityCommand<MyTrade> cmd)
    {
        Subscription subscription = cmd.Subscription;
        MyTrade entity = cmd.Entity;
        lock (this._candles)
        {
            IChartTradeElement element;
            if (!this._tradeElements.TryGetValue(subscription, out element))
                return;
            IChartDrawData data = this.ChartPanel.CreateData();
            data.Group(entity.Trade.ServerTime).Add(element, entity);
            this.ChartPanel.Draw(data);
        }
    }

    private void OnOrderCommand(EntityCommand<Order> cmd)
    {
        Subscription subscription = cmd.Subscription;
        Order entity = cmd.Entity;
        if (entity.Price == 0M)
            return;
        lock (this._candles)
        {
            IChartOrderElement element;
            if (!this._orderElements.TryGetValue(subscription, out element))
                return;
            IChartDrawData data = this.ChartPanel.CreateData();
            data.Group(entity.ServerTime).Add(element, entity);
            this.ChartPanel.Draw(data);
        }
    }

    private void OnCandleCommand(CandleCommand cmd)
    {
        lock (this._candles)
        {
            Subscription subscription = cmd.Subscription;
            ICandleMessage candle = cmd.Candle;
            (IChartCandleElement elem, List<ICandleMessage> candles, Subscription subscription, DateTimeOffset last) tuple1;
            if (!this._candles.TryGetValue(subscription, out tuple1))
                return;
            DateTimeOffset openTime = candle.OpenTime;
            if (openTime < tuple1.last)
                return;
            List<ICandleMessage> candles = tuple1.candles;
            if (candles.Count > 0)
            {
                List<ICandleMessage> icandleMessageList1 = candles;
                if (icandleMessageList1[icandleMessageList1.Count - 1].IsSame<ICandleMessage>(candle))
                {
                    List<ICandleMessage> icandleMessageList2 = candles;
                    icandleMessageList2[icandleMessageList2.Count - 1] = candle;
                }
                else
                    candles.Add(candle);
            }
            else
                candles.Add(candle);
            tuple1.last = openTime;
            this._candles[subscription] = tuple1;
            IChartDrawData data = this.ChartPanel.CreateData();
            IChartDrawData.IChartDrawDataItem chartDrawDataItem = IChartExtensions.Add(data.Group(openTime), tuple1.elem, candle);
            (IChartIndicatorElement indicator, InteractiveChart.IndicatorPair pair)[] tupleArray;
            if (this._indicatorsBySeries.TryGetValue(subscription, out tupleArray))
            {
                foreach ((IChartIndicatorElement indicator, InteractiveChart.IndicatorPair pair) tuple2 in tupleArray)
                    chartDrawDataItem.Add(tuple2.indicator, tuple2.pair.Working.Process(candle));
            }
            this.ChartPanel.Draw(data);
        }
    }

    private void ChartPanel_OnRegisterOrder(IChartArea area, Order orderDraft)
    {
        Security security = orderDraft.Security;
        if (security == null)
        {
            security = ((IEnumerable)area.Elements).OfType<IChartCandleElement>().Select<IChartCandleElement, Security>((Func<IChartCandleElement, Security>)(e =>
            {
                Subscription subscription = this.ChartPanel.TryGetSubscription((IChartElement)e);
                return subscription == null ? (Security)null : subscription.TryGetSecurity();
            })).FirstOrDefault<Security>((Func<Security, bool>)(s => s != null));
            if (security == null)
            {
                int num = (int)new MessageBoxBuilder().Warning().Text(LocalizedStrings.SecurityNotSpecified).Owner((DependencyObject)this).Show();
                return;
            }
        }
        Order order1 = orderDraft;
        if (order1.Portfolio == null)
        {
            Portfolio portfolio;
            order1.Portfolio = portfolio = this.ChartPanel.OrderSettings.Portfolio;
        }
        if (orderDraft.Portfolio == null)
        {
            PortfolioPickerWindow wnd = new PortfolioPickerWindow()
            {
                Portfolios = BaseStudioControl.PortfolioDataSource
            };
            if (wnd.ShowModal((DependencyObject)this))
                orderDraft.Portfolio = this.ChartPanel.OrderSettings.Portfolio = wnd.SelectedPortfolio;
            if (orderDraft.Portfolio == null)
            {
                int num = (int)new MessageBoxBuilder().Warning().Text(LocalizedStrings.PortfolioNotSpecified).Owner((DependencyObject)this).Show();
                return;
            }
        }
        Order order2 = new Order()
        {
            Type = new OrderTypes?((OrderTypes)0),
            Volume = orderDraft.Volume,
            Side = orderDraft.Side,
            Security = security,
            Portfolio = orderDraft.Portfolio,
            Price = security.ShrinkPrice(orderDraft.Price)
        };
        IChartActiveOrdersElement element1 = ((IEnumerable)area.Elements).OfType<IChartActiveOrdersElement>().FirstOrDefault<IChartActiveOrdersElement>();
        if (element1 == null)
        {
            element1 = this.ChartPanel.CreateActiveOrdersElement();
            this.ChartPanel.AddElement(area, (IChartElement)element1);
        }
        ChartPanel chartPanel = this.ChartPanel;
        IChartDrawData data1 = this.ChartPanel.CreateData();
        IChartActiveOrdersElement element2 = element1;
        Order order3 = order2;
        Decimal? nullable = new Decimal?(order2.Volume);
        bool? isFrozen = new bool?();
        bool? isError = new bool?();
        Decimal? price = new Decimal?();
        Decimal? balance = nullable;
        OrderStates? state = new OrderStates?();
        IChartDrawData data2 = data1.Add(element2, order3, isFrozen, isError: isError, price: price, balance: balance, state: state);
        chartPanel.Draw(data2);
        this._chartOrders.Add(order2, element1);
        new RegisterOrderCommand(order2).Process((object)this);
    }

    private void ChartPanel_OnMoveOrder(Order oldOrder, Decimal price)
    {
        Order oldOrder1 = oldOrder;
        Decimal? newPrice = new Decimal?(price);
        Decimal? nullable = new Decimal?();
        Decimal? newVolume = nullable;
        Order newOrder = oldOrder1.ReRegisterClone(newPrice, newVolume);
        IChartActiveOrdersElement activeOrdersElement = CollectionHelper.TryGetValue<Order, IChartActiveOrdersElement>((IDictionary<Order, IChartActiveOrdersElement>)this._chartOrders, oldOrder);
        if (activeOrdersElement != null)
        {
            ChartPanel chartPanel = this.ChartPanel;
            IChartDrawData data1 = this.ChartPanel.CreateData();
            IChartActiveOrdersElement element = activeOrdersElement;
            Order order = oldOrder;
            bool? isFrozen = new bool?(true);
            nullable = new Decimal?(price);
            bool? isError = new bool?();
            Decimal? price1 = nullable;
            Decimal? balance = new Decimal?();
            OrderStates? state = new OrderStates?();
            IChartDrawData data2 = data1.Add(element, order, isFrozen, isError: isError, price: price1, balance: balance, state: state);
            chartPanel.Draw(data2);
        }
        new ReRegisterOrderCommand(oldOrder, newOrder).Process((object)this);
    }

    private void ChartPanel_OnCancelOrder(Order order)
    {
        IChartActiveOrdersElement element = CollectionHelper.TryGetValue<Order, IChartActiveOrdersElement>((IDictionary<Order, IChartActiveOrdersElement>)this._chartOrders, order);
        if (element != null)
            this.ChartPanel.Draw(this.ChartPanel.CreateData().Add(element, order, new bool?(true)));
        new CancelOrderCommand(order).Process((object)this);
    }

    private void ChartPanel_OnAnnotationCreated(IChartAnnotationElement annotation)
    {
        if (!this.CanTrackChanges())
            return;
        this.RaiseChangedCommand();
    }

    private void ChartPanel_OnAnnotationDeleted(IChartAnnotationElement annotation)
    {
        this._annElements.Remove(annotation);
        if (!this.CanTrackChanges())
            return;
        this.RaiseChangedCommand();
    }

    private void ChartPanel_OnAnnotationModified(
      IChartAnnotationElement annotation,
      ChartDrawData.AnnotationData data)
    {
        if (!this.CanTrackChanges() || data.X1 == null && data.X2 == null && data.Y1 == null && data.Y2 == null)
            return;
        this._annElements[annotation] = data;
        this.RaiseChangedCommand();
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        this._loading = true;
        try
        {
            this._candles.Clear();
            this._indicators.Clear();
            this._indicatorsBySeries.Clear();
            this._chartOrders.Clear();
            this._annElements.Clear();
            this._orderElements.Clear();
            this._tradeElements.Clear();
            this._secSubs.Clear();
            PersistableHelper.LoadIfNotNull((IPersistable)this.ChartPanel, storage, "ChartPanel");
            Dictionary<Guid, IChartArea> areas = this.ChartPanel.Areas.ToDictionary<IChartArea, Guid>((Func<IChartArea, Guid>)(a => a.Id));
            Dictionary<Guid, Subscription> subscriptions = new Dictionary<Guid, Subscription>();
            storage.TryLoad<IEnumerable<SettingsStorage>>("Subscriptions", new Action<IEnumerable<SettingsStorage>>(loadSubscriptions));
            storage.TryLoad<IEnumerable<SettingsStorage>>("Elements", new Action<IEnumerable<SettingsStorage>>(loadElements));

            void loadSubscriptions(IEnumerable<SettingsStorage> storages)
            {
                foreach (SettingsStorage storage in storages)
                {
                    Guid key = storage.GetValue<Guid>("Id", new Guid());
                    SettingsStorage settingsStorage = storage.GetValue<SettingsStorage>("Subscription", (SettingsStorage)null);
                    SecurityId? nullable1 = ((SynchronizedDictionary<string, object>)settingsStorage).ContainsKey("SecId") ? new SecurityId?(settingsStorage.GetValue<SecurityId>("SecId", new SecurityId())) : new SecurityId?();
                    DataType dataType = settingsStorage.GetValue<DataType>("Type", null);
                    DateTimeOffset? nullable2 = settingsStorage.GetValue<DateTimeOffset?>("From", new DateTimeOffset?());
                    DateTimeOffset? nullable3 = settingsStorage.GetValue<DateTimeOffset?>("To", new DateTimeOffset?());
                    SecurityMessage security;
                    if (nullable1.HasValue)
                        security = new SecurityMessage()
                        {
                            SecurityId = nullable1.Value
                        };
                    else
                        security = (SecurityMessage)null;
                    Subscription subscription1 = new Subscription(dataType, security);
                    subscription1.From = nullable2;
                    subscription1.To = nullable3;
                    Subscription subscription2 = subscription1;
                    if (subscription2.SubscriptionMessage is MarketDataMessage subscriptionMessage1)
                    {
                        subscriptionMessage1.IsCalcVolumeProfile = settingsStorage.GetValue<bool>("IsCalcVolumeProfile", false);
                        subscriptionMessage1.AllowBuildFromSmallerTimeFrame = settingsStorage.GetValue<bool>("AllowBuildFromSmallerTimeFrame", false);
                        subscriptionMessage1.IsRegularTradingHours = settingsStorage.GetValue<bool?>("IsRegularTradingHours", new bool?());
                        subscriptionMessage1.Count = settingsStorage.GetValue<long?>("Count", new long?());
                        subscriptionMessage1.BuildMode = settingsStorage.GetValue<MarketDataBuildModes>("BuildMode", (MarketDataBuildModes)0);
                        subscriptionMessage1.BuildFrom = settingsStorage.GetValue<DataType>("BuildFrom", (DataType)null);
                        subscriptionMessage1.BuildField = settingsStorage.GetValue<Level1Fields?>("BuildField", new Level1Fields?());
                        subscriptionMessage1.IsFinishedOnly = settingsStorage.GetValue<bool>("IsFinishedOnly", false);
                    }
                    else if (subscription2.SubscriptionMessage is OrderStatusMessage subscriptionMessage2)
                    {
                        List<OrderStates> orderStatesList = new List<OrderStates>();
                        orderStatesList.AddRange(((IEnumerable<string>)StringHelper.SplitByComma(settingsStorage.GetValue<string>("States", (string)null), false)).Select<string, OrderStates>((Func<string, OrderStates>)(s => Converter.To<OrderStates>((object)s))));
                        OrderStates[] array = orderStatesList.ToArray();
                        subscriptionMessage2.States = array;
                    }
                    subscriptions.Add(key, subscription2);
                }
            }

            void loadElements(IEnumerable<SettingsStorage> storages)
            {
                foreach (SettingsStorage storage in storages)
                {
                    IChartArea area = areas[storage.GetValue<Guid>("Area", new Guid())];
                    IChartElement myElement = PersistableHelper.LoadEntire<IChartElement>(storage.GetValue<SettingsStorage>("Element", (SettingsStorage)null));
                    Guid? nullable = storage.GetValue<Guid?>("Subscription", new Guid?());
                    Subscription subscription = !nullable.HasValue ? (Subscription)null : subscriptions[nullable.Value];
                    switch (myElement)
                    {
                        case IChartCandleElement candleElement:
                            this.ChartPanel.AddElement(area, candleElement, subscription);
                            continue;
                        case IChartIndicatorElement indicatorElement:
                            SettingsStorage settingsStorage1 = storage.GetValue<SettingsStorage>("Indicator", (SettingsStorage)null);
                            IIndicator indicator = settingsStorage1 != null ? PersistableHelper.LoadEntire<IIndicator>(settingsStorage1) : (IIndicator)null;
                            if (indicator != null)
                            {
                                this.ChartPanel.AddElement(area, indicatorElement, subscription, indicator);
                                continue;
                            }
                            this.ChartPanel.AddElement(area, indicatorElement);
                            continue;

                        case IChartOrderElement orderElement:
                            this.ChartPanel.AddElement(area, orderElement, subscription);
                            continue;

                        case IChartTradeElement tradeElement:
                            this.ChartPanel.AddElement(area, tradeElement, subscription);
                            continue;

                        case IChartAnnotationElement annotationElement:
                            SettingsStorage settingsStorage2 = storage.GetValue<SettingsStorage>("Data", (SettingsStorage)null);
                            ChartDrawData.AnnotationData data = settingsStorage2 != null ? PersistableHelper.Load<ChartDrawData.AnnotationData>(settingsStorage2) : (ChartDrawData.AnnotationData)null;
                            if (data != null)
                            {
                                this.ChartPanel.AddElement(area, (IChartElement)annotationElement);
                                this.ChartPanel.Draw(this.ChartPanel.CreateData().Add(annotationElement, (IAnnotationData)data));
                                this._annElements[annotationElement] = data;
                                continue;
                            }
                            continue;
                        default:
                            this.ChartPanel.AddElement(area, myElement);
                            continue;
                    }
                }
            }
        }
        finally
        {
            this._loading = false;
        }
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        Dictionary<Subscription, Guid> subscriptions = new Dictionary<Subscription, Guid>();
        foreach (Subscription subscription in this.ChartPanel.Subscriptions)
            subscriptions.Add(subscription, Guid.NewGuid());
        storage.Set<SettingsStorage>("ChartPanel", PersistableHelper.Save((IPersistable)this.ChartPanel)).Set<SettingsStorage[]>("Subscriptions", subscriptions.Select<KeyValuePair<Subscription, Guid>, SettingsStorage>((Func<KeyValuePair<Subscription, Guid>, SettingsStorage>)(p => new SettingsStorage().Set<Guid>("Id", p.Value).Set<SettingsStorage>("Subscription", save(p.Key)))).ToArray<SettingsStorage>()).Set<SettingsStorage[]>("Elements", this.ChartPanel.GetElements().Select<IChartElement, SettingsStorage>((Func<IChartElement, SettingsStorage>)(e =>
        {
            SettingsStorage settingsStorage = new SettingsStorage();
            settingsStorage.Set<Guid>("Id", e.Id).Set<Guid>("Area", e.ChartArea.Id).Set<SettingsStorage>("Element", PersistableHelper.SaveEntire((IPersistable)e, false));
            if (e is IChartIndicatorElement element2)
            {
                IIndicator indicator = element2.TryGetIndicator();
                if (indicator != null)
                    settingsStorage.Set<SettingsStorage>("Indicator", PersistableHelper.SaveEntire((IPersistable)indicator, false));
            }
            Subscription subscription = e.TryGetSubscription();
            Guid guid;
            if (subscription != null && subscriptions.TryGetValue(subscription, out guid))
                settingsStorage.Set<Guid>("Subscription", guid);
            ChartDrawData.AnnotationData annotationData;
            if (e is IChartAnnotationElement key2 && this._annElements.TryGetValue(key2, out annotationData))
                settingsStorage.Set<SettingsStorage>("Data", PersistableHelper.Save((IPersistable)annotationData));
            return settingsStorage;
        })).ToArray<SettingsStorage>());

        static SettingsStorage save(Subscription sub)
        {
            SettingsStorage settingsStorage = new SettingsStorage();
            settingsStorage.Set<SecurityId?>("SecId", sub.SecurityId).Set<DataType>("Type", sub.DataType).Set<DateTimeOffset?>("From", sub.From).Set<DateTimeOffset?>("To", sub.To);
            if (sub.SubscriptionMessage is MarketDataMessage subscriptionMessage1)
                settingsStorage.Set<bool>("IsCalcVolumeProfile", subscriptionMessage1.IsCalcVolumeProfile).Set<bool>("AllowBuildFromSmallerTimeFrame", subscriptionMessage1.AllowBuildFromSmallerTimeFrame).Set<bool?>("IsRegularTradingHours", subscriptionMessage1.IsRegularTradingHours).Set<long?>("Count", subscriptionMessage1.Count).Set<MarketDataBuildModes>("BuildMode", subscriptionMessage1.BuildMode).Set<DataType>("BuildFrom", subscriptionMessage1.BuildFrom).Set<Level1Fields?>("BuildField", subscriptionMessage1.BuildField).Set<bool>("IsFinishedOnly", subscriptionMessage1.IsFinishedOnly);
            else if (sub.SubscriptionMessage is OrderStatusMessage subscriptionMessage2)
                settingsStorage.Set<string>("States", StringHelper.JoinComma(((IEnumerable<OrderStates>)subscriptionMessage2.States).Select<OrderStates, string>((Func<OrderStates, string>)(s => s.ToString()))));
            return settingsStorage;
        }
    }



    private class IndicatorPair
    {
        private readonly InteractiveChart _parent;
        private readonly IChartIndicatorElement _elem;

        public IIndicator UI { get; }

        public Subscription Subscription { get; }

        public IIndicator Working { get; }

        public IndicatorPair(
          InteractiveChart parent,
          IChartIndicatorElement elem,
          IIndicator ui,
          Subscription subscription)
        {
            this._parent = parent ?? throw new ArgumentNullException(nameof(parent));
            this._elem = elem ?? throw new ArgumentNullException(nameof(elem));
            this.UI = ui ?? throw new ArgumentNullException(nameof(ui));
            this.Subscription = subscription ?? throw new ArgumentNullException(nameof(subscription));
            this.Working = ((ICloneable<IIndicator>)this.UI).Clone();
            this.UI.Reseted += new Action(this.OnReseted);
        }

        private void OnReseted()
        {
            this._elem.FullTitle = this.UI.ToString();
            new ChartResetElementCommand((IChartElement)this._elem, (object)this).Process((object)this._parent);
        }
    }

    private static class Keys
    {
        public const string Subscriptions = "Subscriptions";
        public const string Subscription = "Subscription";
        public const string Elements = "Elements";
        public const string Element = "Element";
        public const string Area = "Area";
        public const string Indicator = "Indicator";
        public const string Id = "Id";
        public const string Type = "Type";
        public const string SecId = "SecId";
        public const string From = "From";
        public const string To = "To";
        public const string Data = "Data";
        public const string IsCalcVolumeProfile = "IsCalcVolumeProfile";
        public const string AllowBuildFromSmallerTimeFrame = "AllowBuildFromSmallerTimeFrame";
        public const string IsRegularTradingHours = "IsRegularTradingHours";
        public const string Count = "Count";
        public const string BuildMode = "BuildMode";
        public const string BuildFrom = "BuildFrom";
        public const string BuildField = "BuildField";
        public const string IsFinishedOnly = "IsFinishedOnly";
        public const string States = "States";
    }
}
