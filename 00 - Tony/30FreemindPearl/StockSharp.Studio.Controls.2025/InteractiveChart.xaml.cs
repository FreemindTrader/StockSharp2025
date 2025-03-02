// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.InteractiveChart
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.NavBar;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
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
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
using static LinqToDB.Sql;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "CandleChartPanel", Name = "Chart", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "CandleStick" )]
    [Doc( "topics/designer/user_interface/components/chart.html" )]
    public partial class InteractiveChart : BaseStudioControl
    {
        private readonly Dictionary<CandleSeries, RefQuadruple<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset>> _candles = new Dictionary<CandleSeries, RefQuadruple<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset>>();
        private readonly Dictionary<IChartIndicatorElement, InteractiveChart.IndicatorPair> _indicators = new Dictionary<IChartIndicatorElement, InteractiveChart.IndicatorPair>();
        private readonly Dictionary<CandleSeries, (IChartIndicatorElement indicator, InteractiveChart.IndicatorPair pair)[]> _indicatorsBySeries = new Dictionary<CandleSeries, ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[]>();
        private readonly Dictionary<Order, IChartActiveOrdersElement> _chartOrders = new Dictionary<Order, IChartActiveOrdersElement>();
        private readonly Dictionary<IChartAnnotation, ChartDrawData.AnnotationData> _annotations = new Dictionary<IChartAnnotation, ChartDrawData.AnnotationData>();

        private readonly List< (IChartTradeElement element, SecurityId secid, Subscription subscription) > _tradeElements = new List<ValueTuple<IChartTradeElement, SecurityId, Subscription>>();
        private bool _loading;
        private bool _isInteracted;

        public InteractiveChart()
        {
            this.InitializeComponent();
            this.ChartPanel.SettingsChanged += ( Action ) ( () =>
            {
                if ( !this.CanTrackChanges() )
                    return;
                this.RaiseChangedCommand();
            } );
            this.ChartPanel.AreaAdded += new Action<IChartArea>( this.AreasOnAdded );
            this.ChartPanel.AreaRemoved += new Action<IChartArea>( this.AreasOnRemoved );
            this.ChartPanel.DisableIndicatorReset = true;
            this.Register<EntityCommand<MyTrade>>( this, true, new Action<EntityCommand<MyTrade>>( this.OnMyTradeCommand ), ( Func<EntityCommand<MyTrade>, bool> ) null );
            this.Register<CandleCommand>( this, false, new Action<CandleCommand>( this.OnCandleCommand ), ( Func<CandleCommand, bool> ) null );
            this.Register<SelectCommand>( this, true, ( Action<SelectCommand> ) ( cmd =>
            {
                if ( this.ChartPanel.OrderSettings.Security != null )
                    return;
                Security instance = cmd.Instance as Security;
                if ( instance == null )
                    return;
                this.ChartPanel.OrderSettings.Security = instance;
            } ), ( Func<SelectCommand, bool> ) null );
            this.Register<OrderCommand>( this, true, ( Action<OrderCommand> ) ( cmd =>
            {
                if ( !this.ChartPanel.OrderCreationMode )
                    return;
                Order order = cmd.Entity;
                IChartActiveOrdersElement activeOrdersElement;
                if ( !this._chartOrders.TryGetValue( order, out activeOrdersElement ) )
                {
                    if ( order.State.IsFinal() )
                        return;
                    IChartCandleElement chartCandleElement = this.ChartPanel.GetElements<IChartCandleElement>().FirstOrDefault<IChartCandleElement>((Func<IChartCandleElement, bool>) (e => ((CandleSeries) this.ChartPanel.GetSource((IChartElement) e))?.Security == order.Security));
                    if ( chartCandleElement == null )
                        return;
                    activeOrdersElement = ( ( IEnumerable ) chartCandleElement.ChartArea.Elements ).OfType<IChartActiveOrdersElement>().FirstOrDefault<IChartActiveOrdersElement>();
                    if ( activeOrdersElement == null )
                    {
                        activeOrdersElement = this.ChartPanel.CreateActiveOrdersElement();
                        this.ChartPanel.AddElement( chartCandleElement.ChartArea, ( IChartElement ) activeOrdersElement );
                    }
                    this._chartOrders.Add( order, activeOrdersElement );
                }
                ChartPanel chartPanel = this.ChartPanel;
                IChartDrawData data1 = this.ChartPanel.CreateData();
                IChartActiveOrdersElement element = activeOrdersElement;
                Order order1 = order;
                Decimal? nullable = new Decimal?(cmd.Price);
                bool? isFrozen = new bool?();
                bool? isError = new bool?();
                Decimal? price = nullable;
                Decimal? balance = new Decimal?();
                OrderStates? state = new OrderStates?();
                IChartDrawData data2 = data1.Add(element, order1, isFrozen, true, false, isError, price, balance, state);
                chartPanel.Draw( data2 );
            } ), ( Func<OrderCommand, bool> ) null );
            this.Register<OrderFailCommand>( this, true, ( Action<OrderFailCommand> ) ( cmd =>
            {
                Order order1 = cmd.Entity.Order;
                IChartActiveOrdersElement activeOrdersElement;
                if ( !this._chartOrders.TryGetValue( order1, out activeOrdersElement ) )
                    return;
                ChartPanel chartPanel = this.ChartPanel;
                IChartDrawData data1 = this.ChartPanel.CreateData();
                IChartActiveOrdersElement element = activeOrdersElement;
                Order order2 = order1;
                bool? nullable = new bool?(true);
                bool? isFrozen = new bool?();
                bool? isError = nullable;
                Decimal? price = new Decimal?();
                Decimal? balance = new Decimal?();
                OrderStates? state = new OrderStates?();
                IChartDrawData data2 = data1.Add(element, order2, isFrozen, true, false, isError, price, balance, state);
                chartPanel.Draw( data2 );
                if ( cmd.Type != OrderFailTypes.Register )
                    return;
                this._chartOrders.Remove( order1 );
            } ), ( Func<OrderFailCommand, bool> ) null );
            this.Register<ChartAddElementCommand>( this, false, new Action<ChartAddElementCommand>( this.OnChartAddElementCommand ), ( Func<ChartAddElementCommand, bool> ) null );
            this.Register<ChartRemoveElementCommand>( this, false, new Action<ChartRemoveElementCommand>( this.OnChartRemoveElementCommand ), ( Func<ChartRemoveElementCommand, bool> ) null );
            this.Register<ChartResetElementCommand>( this, false, new Action<ChartResetElementCommand>( this.OnChartResetElementCommand ), ( Func<ChartResetElementCommand, bool> ) null );
            this.Register<FirstInitSecuritiesCommand>( this, false, ( Action<FirstInitSecuritiesCommand> ) ( cmd =>
            {
                if ( this.ChartPanel.GetElements().Any<IChartElement>() )
                    return;
                IChartArea area = this.ChartPanel.Areas.FirstOrDefault<IChartArea>();
                if ( area == null )
                    return;
                Security security = cmd.Securities.First<Security>();
                CandleSeries candleSeries = security.TimeFrame(TimeSpan.FromMinutes(1.0));
                candleSeries.From = new DateTimeOffset?( DateTimeOffset.Now.AddDays( -5.0 ) );
                IChartCandleElement candleElement = this.ChartPanel.CreateCandleElement();
                candleElement.PriceStep = security.PriceStep;
                this.ChartPanel.AddElement( area, candleElement, candleSeries );
            } ), ( Func<FirstInitSecuritiesCommand, bool> ) null );
            this.Register<FirstInitPortfoliosCommand>( this, true, ( Action<FirstInitPortfoliosCommand> ) ( cmd => this.ChartPanel.OrderSettings.Portfolio = cmd.Portfolios.First<Portfolio>() ), ( Func<FirstInitPortfoliosCommand, bool> ) null );
            this.ChartPanel.MinimumRange = 200;
            this.ChartPanel.IsInteracted = true;
            this.ChartPanel.OrderCreationMode = true;
            this.ChartPanel.FillIndicators();
            this.ChartPanel.SubscribeCandleElement += new Action<IChartCandleElement, CandleSeries>( this.Chart_OnSubscribeCandleElement );
            this.ChartPanel.SubscribeIndicatorElement += new Action<IChartIndicatorElement, CandleSeries, IIndicator>( this.Chart_OnSubscribeIndicatorElement );
            this.ChartPanel.SubscribeOrderElement += new Action<IChartOrderElement, Security>( this.Chart_OnSubscribeOrderElement );
            this.ChartPanel.SubscribeTradeElement += new Action<IChartTradeElement, Security>( this.Chart_OnSubscribeTradeElement );
            this.ChartPanel.UnSubscribeElement += new Action<IChartElement>( this.OnChartPanelUnSubscribeElement );
            this.ChartPanel.SecurityProvider = BaseStudioControl.SecurityProvider;
            ( ( INotifyPropertyChanged ) this.ChartPanel ).PropertyChanged += ( PropertyChangedEventHandler ) ( ( _1, _2 ) => updateInteracted() );
            updateInteracted();
            this.WhenLoaded( ( Action ) ( () => new SubscribeCommand( StockSharp.Messages.DataType.Transactions.ToSubscription() ).Process( this, false ) ) );

            void updateInteracted()
            {
                this._isInteracted = this.ChartPanel.IsInteracted;
            }
        }

        public override void Dispose( CloseReason reason )
        {
            foreach ( CandleSeries key in this.ChartPanel.GetElements<IChartCandleElement>().Select<IChartCandleElement, object>( new Func<IChartCandleElement, object>( this.ChartPanel.GetSource ) ).OfType<CandleSeries>() )
            {
                RefQuadruple<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset> refQuadruple;
                if ( this._candles.TryGetValue( key, out refQuadruple ) )
                    new UnSubscribeCommand( ( ( RefTriple<IChartCandleElement, List<ICandleMessage>, Subscription> ) refQuadruple ).Third ).Process( this, false );
            }
            this.ChartPanel.SubscribeCandleElement -= new Action<IChartCandleElement, CandleSeries>( this.Chart_OnSubscribeCandleElement );
            this.ChartPanel.SubscribeIndicatorElement -= new Action<IChartIndicatorElement, CandleSeries, IIndicator>( this.Chart_OnSubscribeIndicatorElement );
            this.ChartPanel.SubscribeOrderElement -= new Action<IChartOrderElement, Security>( this.Chart_OnSubscribeOrderElement );
            this.ChartPanel.SubscribeTradeElement -= new Action<IChartTradeElement, Security>( this.Chart_OnSubscribeTradeElement );
            this.ChartPanel.UnSubscribeElement -= new Action<IChartElement>( this.OnChartPanelUnSubscribeElement );
            base.Dispose( reason );
        }

        public override void FirstTimeInit()
        {
            base.FirstTimeInit();
            this.ChartPanel.AddArea();
        }

        private bool CanTrackChanges()
        {
            return this._isInteracted && !this._loading;
        }

        private void AreasOnRemoved( IChartArea area )
        {
            area.XAxises.Changed -= OnAxisesChanged;
            area.YAxises.Changed -= OnAxisesChanged;
            area.PropertyChanged -= new PropertyChangedEventHandler( this.OnAreaPropertyChanged );
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void AreasOnAdded( IChartArea area )
        {
            area.XAxises.Changed += OnAxisesChanged;
            area.YAxises.Changed += OnAxisesChanged;
            area.PropertyChanged += new PropertyChangedEventHandler( this.OnAreaPropertyChanged );
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void OnAreaPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( !this.CanTrackChanges() || !( e.PropertyName == "Height" ) )
                return;
            this.RaiseChangedCommand();
        }

        private void OnAxisesChanged()
        {
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void Chart_OnSubscribeTradeElement( IChartTradeElement element, Security security )
        {
            this.OnChartAddElementCommand( new ChartAddElementCommand( ( IChartElement ) element, security ) );
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void Chart_OnSubscribeOrderElement( IChartOrderElement element, Security security )
        {
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void Chart_OnSubscribeCandleElement( IChartCandleElement element, CandleSeries series )
        {
            this.OnChartAddElementCommand( new ChartAddElementCommand( ( IChartElement ) element, series ) );
            if ( !this.CanTrackChanges() )
                return;
            if ( this.ChartPanel.OrderSettings.Security == null )
                this.ChartPanel.OrderSettings.Security = series.Security;
            this.RaiseChangedCommand();
        }

        private void OnChartPanelUnSubscribeElement( IChartElement element )
        {
            this.OnChartRemoveElementCommand( new ChartRemoveElementCommand( element, this.ChartPanel.GetSource( element ) ) );
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void Chart_OnSubscribeIndicatorElement(
          IChartIndicatorElement element,
          CandleSeries series,
          IIndicator indicator )
        {
            this.OnChartAddElementCommand( new ChartAddElementCommand( ( IChartElement ) element, series, indicator ) );
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void OnChartResetElementCommand( ChartResetElementCommand command )
        {
            lock ( this._candles )
                this.OnChartResetElementCommandImpl( command );
        }

        private void OnChartResetElementCommandImpl( ChartResetElementCommand command )
        {
            // ISSUE: object of a compiler-generated type is created
            this.ChartPanel.Reset( new IChartElement [1]
            {
                command.Element
            } );
            InteractiveChart.IndicatorPair tag = (InteractiveChart.IndicatorPair) command.Tag;
            ( ( IPersistable ) tag.Working ).Load( PersistableHelper.Save( ( IPersistable ) tag.UI ) );
            RefQuadruple<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset> refQuadruple = (RefQuadruple<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset>) CollectionHelper.TryGetValue<CandleSeries, RefQuadruple<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset>>( this._candles,  tag.Series);
            if ( refQuadruple == null )
                return;
            IChartDrawData data = this.ChartPanel.CreateData();
            foreach ( ICandleMessage candle in ( ( RefPair<IChartCandleElement, List<ICandleMessage>> ) refQuadruple ).Second )
                data.Group( candle.OpenTime ).Add( command.Element, tag.Working.Process( candle ) );
            this.ChartPanel.Draw( data );
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void OnChartAddElementCommand( ChartAddElementCommand command )
        {
            lock ( this._candles )
                this.OnChartAddElementCommandImpl( command );
        }

        private void OnChartAddElementCommandImpl( ChartAddElementCommand command )
        {
            IChartElement element = command.Element;
            element.PropertyChanged += new PropertyChangedEventHandler( this.OnChartElementPropertyChanged );
            IChartCandleElement chartCandleElement = element as IChartCandleElement;
            if ( chartCandleElement != null )
            {
                CandleSeries source = (CandleSeries) command.Source;
                Subscription subscription = new Subscription(source);
                if ( !this._candles.TryAdd( source, ( RefQuadruple<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset> ) RefTuple.Create<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset>( chartCandleElement,  new List<ICandleMessage>(), subscription, DateTimeOffset.MinValue ) ) )
                    return;
                new SubscribeCommand( subscription ).Process( this, false );
            }
            else
            {
                IChartIndicatorElement indicatorElement = element as IChartIndicatorElement;
                if ( indicatorElement != null )
                {
                    if ( this._indicators.ContainsKey( indicatorElement ) )
                        return;
                    CandleSeries source = (CandleSeries) command.Source;
                    RefQuadruple<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset> refQuadruple;
                    if ( !this._candles.TryGetValue( source, out refQuadruple ) )
                        return;
                    InteractiveChart.IndicatorPair indicatorPair = new InteractiveChart.IndicatorPair(this, indicatorElement, command.Indicator, source);
                    this._indicators.Add( indicatorElement, indicatorPair );
                    var m1 = CollectionHelper.SafeAdd<CandleSeries, ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[]>( this._indicatorsBySeries,  source,  (key => Array.Empty<ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>>()));
                    Dictionary<CandleSeries, ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[]> indicatorsBySeries = this._indicatorsBySeries;
                    CandleSeries index1 = source;
                    ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[] valueTupleArray1 = (ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[]) m1;
                    ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[] valueTupleArray2 = new ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[1]
          {
            new ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>(indicatorElement, indicatorPair)
          };
                    int index2 = 0;
                    ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[] valueTupleArray3 = new ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[valueTupleArray1.Length + valueTupleArray2.Length];
                    foreach ( ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair> valueTuple in valueTupleArray1 )
                    {
                        valueTupleArray3 [index2] = valueTuple;
                        ++index2;
                    }
                    foreach ( ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair> valueTuple in valueTupleArray2 )
                    {
                        valueTupleArray3 [index2] = valueTuple;
                        ++index2;
                    }
                    indicatorsBySeries [index1] = valueTupleArray3;
                    IChartDrawData data = this.ChartPanel.CreateData();
                    foreach ( ICandleMessage candle in ( ( RefPair<IChartCandleElement, List<ICandleMessage>> ) refQuadruple ).Second )
                        data.Group( candle.OpenTime ).Add( indicatorElement, indicatorPair.Working.Process( candle ) );
                    this.ChartPanel.Draw( data );
                }
                else
                {
                    IChartTradeElement trd = element as IChartTradeElement;
                    if ( trd == null )
                        return;
                    Security source = (Security) command.Source;
                    ValueTuple<IChartTradeElement, SecurityId, Subscription> valueTuple = this._tradeElements.FirstOrDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>((Func<ValueTuple<IChartTradeElement, SecurityId, Subscription>, bool>) (p => p.Item1 == trd));
                    IChartTradeElement chartTradeElement = valueTuple.Item1;
                    SecurityId securityId = valueTuple.Item2;
                    Subscription subscription1 = valueTuple.Item3;
                    if ( chartTradeElement != null || ( securityId != new SecurityId() || subscription1 != null ) )
                        return;
                    Subscription subscription2 = StockSharp.Messages.DataType.Transactions.ToSubscription();
                    this._tradeElements.Add( new ValueTuple<IChartTradeElement, SecurityId, Subscription>( trd, source.Id.ToSecurityId( ( SecurityIdGenerator ) null ), subscription2 ) );
                    Subscription subscription3 = StockSharp.Messages.DataType.Transactions.ToSubscription();
                    ( ( OrderStatusMessage ) subscription3.SubscriptionMessage ).Count = new long?( 1000L );
                    new SubscribeCommand( subscription3 ).Process( this, false );
                    new SubscribeCommand( subscription2 ).Process( this, false );
                }
            }
        }

        private void OnChartRemoveElementCommand( ChartRemoveElementCommand command )
        {
            lock ( this._candles )
                this.OnChartRemoveElementCommandImpl( command );
        }

        private void OnChartRemoveElementCommandImpl( ChartRemoveElementCommand command )
        {
            IChartElement element = command.Element;
            element.PropertyChanged -= new PropertyChangedEventHandler( this.OnChartElementPropertyChanged );
            IChartIndicatorElement ind = element as IChartIndicatorElement;
            if ( ind != null )
            {
                this._indicators.Remove( ind );
                foreach ( KeyValuePair<CandleSeries, ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair> [ ]> keyValuePair in this._indicatorsBySeries.ToArray<KeyValuePair<CandleSeries, ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair> [ ]>>() )
                {
                    ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[] array = ((IEnumerable<ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>>) keyValuePair.Value).Where<ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>>((Func<ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>, bool>) (t => t.Item1 != ind)).ToArray<ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>>();
                    if ( array.Length == 0 )
                        this._indicatorsBySeries.Remove( keyValuePair.Key );
                    else
                        this._indicatorsBySeries [keyValuePair.Key] = array;
                }
            }
            else if ( element is IChartCandleElement )
            {
                CandleSeries source = (CandleSeries) command.Source;
                RefQuadruple<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset> refQuadruple;
                // ISSUE: cast to a reference type
                if ( !CollectionHelper.TryGetAndRemove<CandleSeries, RefQuadruple<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset>>(  this._candles, source, out refQuadruple ) )
                    return;
                new UnSubscribeCommand( ( ( RefTriple<IChartCandleElement, List<ICandleMessage>, Subscription> ) refQuadruple ).Third ).Process( this, false );
                if ( this._candles.Count != 0 || source.Security != this.ChartPanel.OrderSettings.Security )
                    return;
                ( ( DispatcherObject ) this ).GuiAsync( ( Action ) ( () =>
                {
                    this.ChartPanel.OrderSettings.Security = ( Security ) null;
                    foreach ( IChartActiveOrdersElement activeOrdersElement in ( ( IEnumerable ) element.PersistantChartArea.Elements ).OfType<IChartActiveOrdersElement>() )
                    {
                        // ISSUE: object of a compiler-generated type is created
                        this.ChartPanel.Reset( new IChartElement [1] { activeOrdersElement });
                        foreach ( KeyValuePair<Order, IChartActiveOrdersElement> keyValuePair in this._chartOrders.ToArray<KeyValuePair<Order, IChartActiveOrdersElement>>() )
                        {
                            if ( keyValuePair.Value == activeOrdersElement )
                                this._chartOrders.Remove( keyValuePair.Key );
                        }
                    }
                } ) );
            }
            else
            {
                IChartTradeElement trd = element as IChartTradeElement;
                if ( trd == null )
                    return;
                ValueTuple<IChartTradeElement, SecurityId, Subscription> valueTuple1 = this._tradeElements.FirstOrDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>((Func<ValueTuple<IChartTradeElement, SecurityId, Subscription>, bool>) (p => p.Item1 == trd));
                ValueTuple<IChartTradeElement, SecurityId, Subscription> valueTuple2 = valueTuple1;
                IChartTradeElement chartTradeElement = valueTuple2.Item1;
                SecurityId securityId = valueTuple2.Item2;
                Subscription subscription = valueTuple2.Item3;
                if ( chartTradeElement == null && ( securityId == new SecurityId() && subscription == null ) )
                    return;
                new UnSubscribeCommand( valueTuple1.Item3 ).Process( this, false );
                CollectionHelper.RemoveWhere<ValueTuple<IChartTradeElement, SecurityId, Subscription>>( this._tradeElements, ( p => p.Item1 == trd ) );
            }
        }

        private void OnChartElementPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            this.RaiseChangedCommand();
        }

        private void OnMyTradeCommand( EntityCommand<MyTrade> cmd )
        {
            lock ( this._candles )
                this.OnMyTradeCommandImpl( cmd );
        }

        private void OnMyTradeCommandImpl( EntityCommand<MyTrade> cmd )
        {
            SecurityId securityId1 = cmd.Entity.Trade.Security.Id.ToSecurityId((SecurityIdGenerator) null);
            ValueTuple<IChartTradeElement, SecurityId, Subscription> valueTuple1 = this._tradeElements.FirstOrDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>((Func<ValueTuple<IChartTradeElement, SecurityId, Subscription>, bool>) (p => p.Item3 == cmd.Subscription));
            ValueTuple<IChartTradeElement, SecurityId, Subscription> valueTuple2 = valueTuple1;
            IChartTradeElement chartTradeElement = valueTuple2.Item1;
            SecurityId securityId2 = valueTuple2.Item2;
            Subscription subscription = valueTuple2.Item3;
            if ( chartTradeElement == null && ( securityId2 == new SecurityId() && subscription == null ) || ( valueTuple1.Item2 != securityId1 || !this.ChartPanel.GetElements().Contains<IChartElement>( ( IChartElement ) valueTuple1.Item1 ) ) )
                return;
            IChartDrawData data = this.ChartPanel.CreateData();
            data.Group( cmd.Entity.Trade.ServerTime ).Add( valueTuple1.Item1, cmd.Entity );
            this.ChartPanel.Draw( data );
        }

        private void OnCandleCommand( CandleCommand cmd )
        {
            lock ( this._candles )
                this.OnCandleCommandImpl( cmd );
        }

        private void OnCandleCommandImpl( CandleCommand cmd )
        {
            ICandleMessage candle = cmd.Candle;
            RefQuadruple<IChartCandleElement, List<ICandleMessage>, Subscription, DateTimeOffset> refQuadruple;
            if ( !this._candles.TryGetValue( cmd.Series, out refQuadruple ) )
                return;
            DateTimeOffset openTime = candle.OpenTime;
            if ( openTime < refQuadruple.Fourth )
                return;
            List<ICandleMessage> second = ((RefPair<IChartCandleElement, List<ICandleMessage>>) refQuadruple).Second;
            if ( second.Count > 0 )
            {
                List<ICandleMessage> candleMessageList1 = second;
                if ( candleMessageList1 [candleMessageList1.Count - 1].IsSame<ICandleMessage>( candle ) )
                {
                    List<ICandleMessage> candleMessageList2 = second;
                    candleMessageList2 [candleMessageList2.Count - 1] = candle;
                }
                else
                    second.Add( candle );
            }
            else
                second.Add( candle );
            refQuadruple.Fourth = ( openTime );
            IChartDrawData data = this.ChartPanel.CreateData();
            IChartDrawData.IChartDrawDataItem chartDrawDataItem = IChartExtensions.Add(data.Group(openTime), ((RefPair<IChartCandleElement, List<ICandleMessage>>) refQuadruple).First, candle);
            ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[] valueTupleArray;
            if ( this._indicatorsBySeries.TryGetValue( cmd.Series, out valueTupleArray ) )
            {
                foreach ( ValueTuple<IChartIndicatorElement, InteractiveChart.IndicatorPair> valueTuple in valueTupleArray )
                    chartDrawDataItem.Add( valueTuple.Item1, valueTuple.Item2.Working.Process( candle ) );
            }
            this.ChartPanel.Draw( data );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this._loading = true;
            try
            {
                PersistableHelper.LoadIfNotNull( ( IPersistable ) this.ChartPanel, storage, "ChartPanel" );
                storage.TryLoad<IEnumerable<SettingsStorage>>( "Annotations", new Action<IEnumerable<SettingsStorage>>( this.LoadAnnotations ) );
            }
            finally
            {
                this._loading = false;
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "ChartPanel", PersistableHelper.Save( ( IPersistable ) this.ChartPanel ) );
            storage.SetValue<SettingsStorage [ ]>( "Annotations", this.SaveAnnotations() );
        }

        private void ChartPanel_OnRegisterOrder( IChartArea area, Order orderDraft )
        {
            Security security = orderDraft.Security;
            if ( security == null )
            {
                security = ( ( IEnumerable ) area.Elements ).OfType<IChartCandleElement>().Select<IChartCandleElement, Security>( ( Func<IChartCandleElement, Security> ) ( e => ( this.ChartPanel.GetSource( ( IChartElement ) e ) as CandleSeries )?.Security ) ).FirstOrDefault<Security>( ( Func<Security, bool> ) ( s => s != null ) );
                if ( security == null )
                {
                    int num = (int) new MessageBoxBuilder().Warning().Text(LocalizedStrings.SecurityNotSpecified).Owner((DependencyObject) this).Show();
                    return;
                }
            }
            Order order1 = orderDraft;
            if ( order1.Portfolio == null )
            {
                Portfolio portfolio;
                order1.Portfolio = portfolio = this.ChartPanel.OrderSettings.Portfolio;
            }
            if ( orderDraft.Portfolio == null )
            {
                PortfolioPickerWindow wnd = new PortfolioPickerWindow()
                {
                    Portfolios = BaseStudioControl.PortfolioDataSource
                };
                if ( wnd.ShowModal( ( DependencyObject ) this ) )
                    orderDraft.Portfolio = this.ChartPanel.OrderSettings.Portfolio = wnd.SelectedPortfolio;
                if ( orderDraft.Portfolio == null )
                {
                    int num = (int) new MessageBoxBuilder().Warning().Text(LocalizedStrings.PortfolioNotSpecified).Owner((DependencyObject) this).Show();
                    return;
                }
            }
            Order order2 = new Order()
            {
                Type = new OrderTypes?(OrderTypes.Limit),
                Volume = orderDraft.Volume,
                Side = orderDraft.Side,
                Security = security,
                Portfolio = orderDraft.Portfolio,
                Price = security.ShrinkPrice(orderDraft.Price)
            };
            IChartActiveOrdersElement activeOrdersElement = ((IEnumerable) area.Elements).OfType<IChartActiveOrdersElement>().FirstOrDefault<IChartActiveOrdersElement>();
            if ( activeOrdersElement == null )
            {
                activeOrdersElement = this.ChartPanel.CreateActiveOrdersElement();
                this.ChartPanel.AddElement( area, ( IChartElement ) activeOrdersElement );
            }
            ChartPanel chartPanel = this.ChartPanel;
            IChartDrawData data1 = this.ChartPanel.CreateData();
            IChartActiveOrdersElement element = activeOrdersElement;
            Order order3 = order2;
            Decimal? nullable = new Decimal?(order2.Volume);
            bool? isFrozen = new bool?();
            bool? isError = new bool?();
            Decimal? price = new Decimal?();
            Decimal? balance = nullable;
            OrderStates? state = new OrderStates?();
            IChartDrawData data2 = data1.Add(element, order3, isFrozen, true, false, isError, price, balance, state);
            chartPanel.Draw( data2 );
            this._chartOrders.Add( order2, activeOrdersElement );
            new RegisterOrderCommand( order2 ).Process( this, false );
        }

        private void ChartPanel_OnMoveOrder( Order oldOrder, Decimal price )
        {
            Order oldOrder1 = oldOrder;
            Decimal? newPrice = new Decimal?(price);
            Decimal? nullable = new Decimal?();
            Decimal? newVolume = nullable;
            Order newOrder = oldOrder1.ReRegisterClone(newPrice, newVolume);
            IChartActiveOrdersElement activeOrdersElement = (IChartActiveOrdersElement) CollectionHelper.TryGetValue<Order, IChartActiveOrdersElement>( this._chartOrders,  oldOrder);
            if ( activeOrdersElement != null )
            {
                ChartPanel chartPanel = this.ChartPanel;
                IChartDrawData data1 = this.ChartPanel.CreateData();
                IChartActiveOrdersElement element = activeOrdersElement;
                Order order = oldOrder;
                bool? isFrozen = new bool?(true);
                nullable = new Decimal?( price );
                bool? isError = new bool?();
                Decimal? price1 = nullable;
                Decimal? balance = new Decimal?();
                OrderStates? state = new OrderStates?();
                IChartDrawData data2 = data1.Add(element, order, isFrozen, true, false, isError, price1, balance, state);
                chartPanel.Draw( data2 );
            }
            new ReRegisterOrderCommand( oldOrder, newOrder ).Process( this, false );
        }

        private void ChartPanel_OnCancelOrder( Order order )
        {
            IChartActiveOrdersElement element = (IChartActiveOrdersElement) CollectionHelper.TryGetValue<Order, IChartActiveOrdersElement>( this._chartOrders,  order);
            if ( element != null )
                this.ChartPanel.Draw( this.ChartPanel.CreateData().Add( element, order, new bool?( true ), true, false, new bool?(), new Decimal?(), new Decimal?(), new OrderStates?() ) );
            new CancelOrderCommand( order ).Process( this, false );
        }

        private void ChartPanel_OnAnnotationCreated( IChartAnnotation annotation )
        {
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void ChartPanel_OnAnnotationDeleted( IChartAnnotation annotation )
        {
            this._annotations.Remove( annotation );
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void ChartPanel_OnAnnotationModified(
          IChartAnnotation annotation,
          ChartDrawData.AnnotationData data )
        {
            if ( this._loading || data.X1 == null && data.X2 == null && ( data.Y1 == null && data.Y2 == null ) )
                return;
            this._annotations [annotation] = data;
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private SettingsStorage [ ] SaveAnnotations()
        {
            List<SettingsStorage> settingsStorageList = new List<SettingsStorage>();
            foreach ( KeyValuePair<IChartAnnotation, ChartDrawData.AnnotationData> annotation in this._annotations )
            {
                IChartAnnotation key = annotation.Key;
                ChartDrawData.AnnotationData annotationData = annotation.Value;
                SettingsStorage settingsStorage = new SettingsStorage();
                settingsStorage.SetValue<SettingsStorage>( "Annotation", PersistableHelper.Save( ( IPersistable ) key ) );
                settingsStorage.SetValue<Guid>( "ChartArea", key.ChartArea.Id );
                settingsStorage.SetValue<SettingsStorage>( "Data", PersistableHelper.Save( ( IPersistable ) annotationData ) );
                settingsStorageList.Add( settingsStorage );
            }
            return settingsStorageList.ToArray();
        }

        private void LoadAnnotations( IEnumerable<SettingsStorage> storages )
        {
            using ( IEnumerator<SettingsStorage> enumerator = storages.GetEnumerator() )
            {
                while ( ( ( IEnumerator ) enumerator ).MoveNext() )
                {
                    SettingsStorage current = enumerator.Current;
                    ChartAnnotation chartAnnotation = (ChartAnnotation) PersistableHelper.Load<ChartAnnotation>((SettingsStorage) current.GetValue<SettingsStorage>("Annotation",  null));
                    Guid areaId = (Guid) current.GetValue<Guid>("ChartArea",  new Guid());
                    ChartDrawData.AnnotationData annotationData = (ChartDrawData.AnnotationData) PersistableHelper.Load<ChartDrawData.AnnotationData>((SettingsStorage) current.GetValue<SettingsStorage>("Data",  null));
                    IChartArea chartArea = this.ChartPanel.Areas.FirstOrDefault<IChartArea>((Func<IChartArea, bool>) (a => a.Id == areaId));
                    if ( chartArea == null )
                    {
                        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 1);
                        interpolatedStringHandler.AppendLiteral( "Chart area " );
                        interpolatedStringHandler.AppendFormatted<Guid>( areaId );
                        interpolatedStringHandler.AppendLiteral( " not found." );
                        throw new InvalidOperationException( interpolatedStringHandler.ToStringAndClear() );
                    }
                    IChartArea area = chartArea;
                    IChartDrawData data = this.ChartPanel.CreateData();
                    data.Add( ( IChartAnnotation ) chartAnnotation, ( IAnnotationData ) annotationData );
                    this.ChartPanel.AddElement( area, ( IChartElement ) chartAnnotation );
                    this.ChartPanel.Draw( data );
                    this._annotations [( IChartAnnotation ) chartAnnotation] = annotationData;
                }
            }
        }



        private class IndicatorPair
        {
            private readonly InteractiveChart _parent;
            private readonly IChartIndicatorElement _elem;

            public IIndicator UI { get; }

            public CandleSeries Series { get; }

            public IIndicator Working { get; }

            public IndicatorPair(
              InteractiveChart parent,
              IChartIndicatorElement elem,
              IIndicator ui,
              CandleSeries series )
            {
                InteractiveChart interactiveChart = parent;
                if ( interactiveChart == null )
                    throw new ArgumentNullException( nameof( parent ) );
                this._parent = interactiveChart;
                IChartIndicatorElement indicatorElement = elem;
                if ( indicatorElement == null )
                    throw new ArgumentNullException( nameof( elem ) );
                this._elem = indicatorElement;
                IIndicator indicator = ui;
                if ( indicator == null )
                    throw new ArgumentNullException( nameof( ui ) );
                this.UI = indicator;
                CandleSeries candleSeries = series;
                if ( candleSeries == null )
                    throw new ArgumentNullException( nameof( series ) );
                this.Series = candleSeries;
                this.Working = ( ( ICloneable<IIndicator> ) this.UI ).Clone();
                this.UI.Reseted += new Action( this.OnReseted );
            }

            private void OnReseted()
            {
                this._elem.FullTitle = this.UI.ToString();
                new ChartResetElementCommand( ( IChartElement ) this._elem, this ).Process( this._parent, false );
            }
        }
    }
}
