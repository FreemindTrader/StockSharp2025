// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.InteractiveChart
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str3200" )]
    [DescriptionLoc( "Str3201", false )]
    [VectorIcon( "CandleStick" )]
    [Doc( "topics/Designer_Chart.html" )]
    public partial class InteractiveChart : BaseStudioControl, IComponentConnector
    {
        private readonly Dictionary<CandleSeries, RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset>> _candles = new Dictionary<CandleSeries, RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset>>();
        private readonly Dictionary<IChartIndicatorElement, IndicatorPair> _indicators = new Dictionary<IChartIndicatorElement, IndicatorPair>();
        private readonly Dictionary<CandleSeries, Tuple<IChartIndicatorElement, IndicatorPair>[ ]> _indicatorsBySeries = new Dictionary<CandleSeries, Tuple<IChartIndicatorElement, IndicatorPair>[ ]>();
        private readonly Dictionary<Order, IChartActiveOrdersElement> _chartOrders = new Dictionary<Order, IChartActiveOrdersElement>();
        private readonly Dictionary<IChartAnnotation, ChartDrawData.AnnotationData> _annotations = new Dictionary<IChartAnnotation, ChartDrawData.AnnotationData>();
        
        private readonly List< ( IChartTradeElement element, SecurityId secid, Subscription subscription )> _tradeElements = new List<ValueTuple<IChartTradeElement, SecurityId, Subscription>>();
        private bool _loading;
        private bool _isInteracted;
        
        public InteractiveChart()
        {
            InitializeComponent();
            ChartPanel.SettingsChanged += () =>
              {
                  if ( !CanTrackChanges() )
                      return;
                  RaiseChangedCommand();
              };
            ChartPanel.Areas.Added += new Action<IChartArea>( AreasOnAdded );
            ChartPanel.Areas.Removed += new Action<IChartArea>( AreasOnRemoved );
            ChartPanel.Areas.Cleared += new Action( AreasOnCleared );
            ChartPanel.DisableIndicatorReset = true;
            IStudioCommandService commandService = CommandService;
            commandService.Register( this, true, new Action<EntityCommand<MyTrade>>( OnMyTradeCommand ), null );
            commandService.Register( this, false, new Action<CandleCommand>( OnCandleCommand ), null );
            commandService.Register<SelectCommand>( this, true, cmd =>
                {
                    if ( ChartPanel.OrderSettings.Security != null )
                        return;
                    Security instance = cmd.Instance as Security;
                    if ( instance == null )
                        return;
                    ChartPanel.OrderSettings.Security = instance;
                }, null );
            commandService.Register<OrderCommand>( this, true, cmd =>
                {
                    if ( !ChartPanel.OrderCreationMode )
                        return;
                    Order order = cmd.Entity;
                    IChartActiveOrdersElement activeOrdersElement;
                    if ( !_chartOrders.TryGetValue( order, out activeOrdersElement ) )
                    {
                        if ( order.State.IsFinal() )
                            return;
                        IChartCandleElement chartCandleElement = ChartPanel.Elements.OfType<IChartCandleElement>().FirstOrDefault( e => ( ( CandleSeries )ChartPanel.GetSource( e ) )?.Security == order.Security );
                        if ( chartCandleElement == null )
                            return;
                        activeOrdersElement = chartCandleElement.ChartArea.Elements.OfType<IChartActiveOrdersElement>().FirstOrDefault();
                        if ( activeOrdersElement == null )
                        {
                            activeOrdersElement = ChartPanel.CreateActiveOrdersElement();
                            ChartPanel.AddElement( chartCandleElement.ChartArea, activeOrdersElement );
                        }
                        _chartOrders.Add( order, activeOrdersElement );
                    }
                    ChartPanel chartPanel = ChartPanel;
                    IChartDrawData data1 = ChartPanel.CreateData();
                    IChartActiveOrdersElement element = activeOrdersElement;
                    Order order1 = order;
                    Decimal? nullable = new Decimal?( cmd.Price );
                    bool? isFrozen = new bool?();
                    bool? isError = new bool?();
                    Decimal? price = nullable;
                    Decimal? balance = new Decimal?();
                    OrderStates? state = new OrderStates?();
                    IChartDrawData data2 = data1.Add( element, order1, isFrozen, true, false, isError, price, balance, state );
                    chartPanel.Draw( data2 );
                }, null );
            commandService.Register<OrderFailCommand>( this, true, cmd =>
                {
                    Order order1 = cmd.Entity.Order;
                    IChartActiveOrdersElement activeOrdersElement;
                    if ( !_chartOrders.TryGetValue( order1, out activeOrdersElement ) )
                        return;
                    ChartPanel chartPanel = ChartPanel;
                    IChartDrawData data1 = ChartPanel.CreateData();
                    IChartActiveOrdersElement element = activeOrdersElement;
                    Order order2 = order1;
                    bool? nullable = new bool?( true );
                    bool? isFrozen = new bool?();
                    bool? isError = nullable;
                    Decimal? price = new Decimal?();
                    Decimal? balance = new Decimal?();
                    OrderStates? state = new OrderStates?();
                    IChartDrawData data2 = data1.Add( element, order2, isFrozen, true, false, isError, price, balance, state );
                    chartPanel.Draw( data2 );
                    if ( cmd.Type != OrderFailTypes.Register )
                        return;
                    _chartOrders.Remove( order1 );
                }, null );
            commandService.Register( this, false, new Action<ChartAddElementCommand>( OnChartAddElementCommand ), null );
            commandService.Register( this, false, new Action<ChartRemoveElementCommand>( OnChartRemoveElementCommand ), null );
            commandService.Register( this, false, new Action<ChartResetElementCommand>( OnChartResetElementCommand ), null );
            commandService.Register<FirstInitSecuritiesCommand>( this, false, cmd =>
                {
                    if ( ChartPanel.Elements.Any() )
                        return;
                    IChartArea area = ChartPanel.Areas.FirstOrDefault();
                    if ( area == null )
                        return;
                    CandleSeries candleSeries = new CandleSeries( typeof( TimeFrameCandle ), cmd.Securities.First(), TimeSpan.FromMinutes( 1.0 ) ) { From = new DateTimeOffset?( DateTimeOffset.Now.AddDays( -5.0 ) ) };
                    IChartCandleElement candleElement = ChartPanel.CreateCandleElement();
                    ChartPanel.AddElement( area, candleElement, candleSeries );
                }, null );
            commandService.Register<FirstInitPortfoliosCommand>( this, true, cmd => ChartPanel.OrderSettings.Portfolio = cmd.Portfolios.First(), null );
            ChartPanel.MinimumRange = 200;
            ChartPanel.IsInteracted = true;
            ChartPanel.OrderCreationMode = true;
            ChartPanel.FillIndicators();
            ChartPanel.SubscribeCandleElement += new Action<IChartCandleElement, CandleSeries>( Chart_OnSubscribeCandleElement );
            ChartPanel.SubscribeIndicatorElement += new Action<IChartIndicatorElement, CandleSeries, IIndicator>( Chart_OnSubscribeIndicatorElement );
            ChartPanel.SubscribeOrderElement += new Action<IChartOrderElement, Security>( Chart_OnSubscribeOrderElement );
            ChartPanel.SubscribeTradeElement += new Action<IChartTradeElement, Security>( Chart_OnSubscribeTradeElement );
            ChartPanel.UnSubscribeElement += new Action<IChartElement>( OnChartPanelUnSubscribeElement );
            ChartPanel.SecurityProvider = SecurityProvider;
            ( ( INotifyPropertyChanged )ChartPanel ).PropertyChanged += ( _1, _2 ) => updateInteracted();
            updateInteracted();
            WhenLoaded( () => new SubscribeCommand( DataType.Transactions.ToSubscription() ).Process( this, false ) );

            void updateInteracted()
            {
                _isInteracted = ChartPanel.IsInteracted;
            }
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = CommandService;
            foreach ( CandleSeries key in ChartPanel.Elements.OfType<IChartCandleElement>().Select( new Func<IChartCandleElement, object>( ChartPanel.GetSource ) ).Where( s => s != null ) )
            {
                RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset> refQuadruple;
                if ( _candles.TryGetValue( key, out refQuadruple ) )
                    new UnSubscribeCommand( refQuadruple.Third ).Process( this, false );
            }
            commandService.UnRegister<OrderCommand>( this );
            commandService.UnRegister<OrderFailCommand>( this );
            commandService.UnRegister<CandleCommand>( this );
            commandService.UnRegister<SelectCommand>( this );
            commandService.UnRegister<ChartAddElementCommand>( this );
            commandService.UnRegister<ChartRemoveElementCommand>( this );
            commandService.UnRegister<ChartResetElementCommand>( this );
            commandService.UnRegister<FirstInitSecuritiesCommand>( this );
            commandService.UnRegister<FirstInitPortfoliosCommand>( this );
            ChartPanel.SubscribeCandleElement -= new Action<IChartCandleElement, CandleSeries>( Chart_OnSubscribeCandleElement );
            ChartPanel.SubscribeIndicatorElement -= new Action<IChartIndicatorElement, CandleSeries, IIndicator>( Chart_OnSubscribeIndicatorElement );
            ChartPanel.SubscribeOrderElement -= new Action<IChartOrderElement, Security>( Chart_OnSubscribeOrderElement );
            ChartPanel.SubscribeTradeElement -= new Action<IChartTradeElement, Security>( Chart_OnSubscribeTradeElement );
            ChartPanel.UnSubscribeElement -= new Action<IChartElement>( OnChartPanelUnSubscribeElement );
            base.Dispose();
        }

        public override void FirstTimeInit()
        {
            base.FirstTimeInit();
            ChartPanel.Areas.Add( ChartPanel.CreateArea() );
        }

        private bool CanTrackChanges()
        {
            return _isInteracted && !_loading;
        }

        private void AreasOnCleared()
        {
            if ( !CanTrackChanges() )
                return;
            RaiseChangedCommand();
        }

        private void AreasOnRemoved( IChartArea area )
        {
            if ( !CanTrackChanges() )
                return;
            RaiseChangedCommand();
        }

        private void AreasOnAdded( IChartArea area )
        {
            if ( !CanTrackChanges() )
                return;
            RaiseChangedCommand();
        }

        private void Chart_OnSubscribeTradeElement( IChartTradeElement element, Security security )
        {
            OnChartAddElementCommand( new ChartAddElementCommand( element, security ) );
            if ( !CanTrackChanges() )
                return;
            RaiseChangedCommand();
        }

        private void Chart_OnSubscribeOrderElement( IChartOrderElement element, Security security )
        {
            if ( !CanTrackChanges() )
                return;
            RaiseChangedCommand();
        }

        private void Chart_OnSubscribeCandleElement( IChartCandleElement element, CandleSeries series )
        {
            OnChartAddElementCommand( new ChartAddElementCommand( element, series ) );
            if ( !CanTrackChanges() )
                return;
            if ( ChartPanel.OrderSettings.Security == null )
                ChartPanel.OrderSettings.Security = series.Security;
            RaiseChangedCommand();
        }

        private void OnChartPanelUnSubscribeElement( IChartElement element )
        {
            OnChartRemoveElementCommand( new ChartRemoveElementCommand( element, ChartPanel.GetSource( element ) ) );
            if ( !CanTrackChanges() )
                return;
            RaiseChangedCommand();
        }

        private void Chart_OnSubscribeIndicatorElement(
          IChartIndicatorElement element,
          CandleSeries series,
          IIndicator indicator )
        {
            OnChartAddElementCommand( new ChartAddElementCommand( element, series, indicator ) );
            if ( !CanTrackChanges() )
                return;
            RaiseChangedCommand();
        }

        private void OnChartResetElementCommand( ChartResetElementCommand command )
        {
            lock ( _candles )
                OnChartResetElementCommandImpl( command );
        }

        private void OnChartResetElementCommandImpl( ChartResetElementCommand command )
        {
            ChartPanel.Reset( new IChartElement[1]
            {
        command.Element
            } );
            IndicatorPair tag = ( IndicatorPair )command.Tag;
            tag.Working.Load( tag.UI.Save() );
            RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset> refQuadruple = _candles.TryGetValue( tag.Series );
            if ( refQuadruple == null )
                return;
            IChartDrawData data = ChartPanel.CreateData();
            foreach ( KeyValuePair<DateTimeOffset, Candle> keyValuePair in refQuadruple.Second )
            {
                Candle candle = keyValuePair.Value;
                data.Group( candle.OpenTime ).Add( command.Element, tag.Working.Process( candle ) );
            }
            ChartPanel.Draw( data );
            if ( !CanTrackChanges() )
                return;
            RaiseChangedCommand();
        }

        private void OnChartAddElementCommand( ChartAddElementCommand command )
        {
            lock ( _candles )
                OnChartAddElementCommandImpl( command );
        }

        private void OnChartAddElementCommandImpl( ChartAddElementCommand command )
        {
            IChartCandleElement element1 = command.Element as IChartCandleElement;
            if ( element1 != null )
            {
                CandleSeries source = ( CandleSeries )command.Source;
                Subscription subscription = new Subscription( source );
                if ( !_candles.TryAdd( source, RefTuple.Create( element1, new SortedDictionary<DateTimeOffset, Candle>(), subscription, DateTimeOffset.MinValue ) ) )
                    return;
                new SubscribeCommand( subscription ).Process( this, false );
            }
            else
            {
                IChartIndicatorElement element2 = command.Element as IChartIndicatorElement;
                if ( element2 != null )
                {
                    if ( _indicators.ContainsKey( element2 ) )
                        return;
                    CandleSeries source = ( CandleSeries )command.Source;
                    RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset> refQuadruple;
                    if ( !_candles.TryGetValue( source, out refQuadruple ) )
                        return;
                    IndicatorPair indicatorPair = new IndicatorPair( this, element2, command.Indicator, source );
                    _indicators.Add( element2, indicatorPair );
                    Tuple<IChartIndicatorElement, IndicatorPair>[ ] first = _indicatorsBySeries.SafeAdd( source, key => Array.Empty<Tuple<IChartIndicatorElement, IndicatorPair>>() );
                    _indicatorsBySeries[source] = first.Concat( new Tuple<IChartIndicatorElement, IndicatorPair>[1]
                    {
            Tuple.Create(element2, indicatorPair)
                    } ).ToArray();
                    IChartDrawData data = ChartPanel.CreateData();
                    foreach ( KeyValuePair<DateTimeOffset, Candle> keyValuePair in refQuadruple.Second )
                    {
                        Candle candle = keyValuePair.Value;
                        data.Group( candle.OpenTime ).Add( element2, indicatorPair.Working.Process( candle ) );
                    }
                    ChartPanel.Draw( data );
                }
                else
                {
                    IChartTradeElement trd = command.Element as IChartTradeElement;
                    if ( trd == null )
                        return;
                    Security source = ( Security )command.Source;
                    if ( !_tradeElements.FirstOrDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>( p => p.Item1 == trd ).IsDefault() )
                        return;
                    Subscription subscription1 = DataType.Transactions.ToSubscription();
                    _tradeElements.Add( new ValueTuple<IChartTradeElement, SecurityId, Subscription>( trd, source.Id.ToSecurityId( null ), subscription1 ) );
                    Subscription subscription2 = DataType.Transactions.ToSubscription();
                    ( ( OrderStatusMessage )subscription2.SubscriptionMessage ).Count = new long?( 1000L );
                    new SubscribeCommand( subscription2 ).Process( this, false );
                    new SubscribeCommand( subscription1 ).Process( this, false );
                }
            }
        }

        private void OnChartRemoveElementCommand( ChartRemoveElementCommand command )
        {
            lock ( _candles )
                OnChartRemoveElementCommandImpl( command );
        }

        private void OnChartRemoveElementCommandImpl( ChartRemoveElementCommand command )
        {
            IChartIndicatorElement ind = command.Element as IChartIndicatorElement;
            if ( ind != null )
            {
                _indicators.Remove( ind );
                foreach ( KeyValuePair<CandleSeries, Tuple<IChartIndicatorElement, IndicatorPair>[ ]> keyValuePair in _indicatorsBySeries.ToArray() )
                {
                    Tuple<IChartIndicatorElement, IndicatorPair>[ ] array = keyValuePair.Value.Where( t => t.Item1 != ind ).ToArray();
                    if ( array.Length == 0 )
                        _indicatorsBySeries.Remove( keyValuePair.Key );
                    else
                        _indicatorsBySeries[keyValuePair.Key] = array;
                }
            }
            else if ( command.Element is IChartCandleElement )
            {
                CandleSeries source = ( CandleSeries )command.Source;
                RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset> refQuadruple;
                if ( !_candles.TryGetAndRemove( source, out refQuadruple ) )
                    return;
                new UnSubscribeCommand( refQuadruple.Third ).Process( this, false );
                if ( _candles.Count != 0 || source.Security != ChartPanel.OrderSettings.Security )
                    return;
                this.GuiAsync( () =>
                   {
                       ChartPanel.OrderSettings.Security = null;
                       foreach ( IChartActiveOrdersElement activeOrdersElement in command.Element.PersistantChartArea.Elements.OfType<IChartActiveOrdersElement>() )
                       {
                           ChartPanel.Reset( new IChartActiveOrdersElement[1]
             {
              activeOrdersElement
                       } );
                           foreach ( KeyValuePair<Order, IChartActiveOrdersElement> keyValuePair in _chartOrders.ToArray() )
                           {
                               if ( keyValuePair.Value == activeOrdersElement )
                                   _chartOrders.Remove( keyValuePair.Key );
                           }
                       }
                   } );
            }
            else
            {
                IChartTradeElement trd = command.Element as IChartTradeElement;
                if ( trd == null )
                    return;
                ValueTuple<IChartTradeElement, SecurityId, Subscription> valueTuple = _tradeElements.FirstOrDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>( p => p.Item1 == trd );
                if ( valueTuple.IsDefault() )
                    return;
                new UnSubscribeCommand( valueTuple.Item3 ).Process( this, false );
                _tradeElements.RemoveWhere<ValueTuple<IChartTradeElement, SecurityId, Subscription>>( p => p.Item1 == trd );
            }
        }

        private void OnMyTradeCommand( EntityCommand<MyTrade> cmd )
        {
            lock ( _candles )
                OnMyTradeCommandImpl( cmd );
        }

        private void OnMyTradeCommandImpl( EntityCommand<MyTrade> cmd )
        {
            SecurityId securityId = cmd.Entity.Trade.Security.Id.ToSecurityId( null );
            ValueTuple<IChartTradeElement, SecurityId, Subscription> valueTuple = _tradeElements.FirstOrDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>( p => p.Item3 == cmd.Subscription );
            if ( valueTuple.IsDefault() || valueTuple.Item2 != securityId || !ChartPanel.Elements.Contains( valueTuple.Item1 ) )
                return;
            IChartDrawData data = ChartPanel.CreateData();
            data.Group( cmd.Entity.Trade.Time ).Add( valueTuple.Item1, cmd.Entity );
            ChartPanel.Draw( data );
        }

        private void OnCandleCommand( CandleCommand cmd )
        {
            lock ( _candles )
                OnCandleCommandImpl( cmd );
        }

        private void OnCandleCommandImpl( CandleCommand cmd )
        {
            Candle candle = cmd.Candle;
            RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset> refQuadruple;
            if ( !_candles.TryGetValue( cmd.Series, out refQuadruple ) )
                return;
            DateTimeOffset openTime = candle.OpenTime;
            if ( openTime < refQuadruple.Fourth )
                return;
            refQuadruple.Second[openTime] = candle;
            refQuadruple.Fourth = openTime;
            IChartDrawData data = ChartPanel.CreateData();
            IChartDrawData.IChartDrawDataItem chartDrawDataItem = data.Group( openTime ).Add( refQuadruple.First, candle );
            Tuple<IChartIndicatorElement, IndicatorPair>[ ] tupleArray;
            if ( _indicatorsBySeries.TryGetValue( cmd.Series, out tupleArray ) )
            {
                foreach ( Tuple<IChartIndicatorElement, IndicatorPair> tuple in tupleArray )
                    chartDrawDataItem.Add( tuple.Item1, tuple.Item2.Working.Process( candle ) );
            }
            ChartPanel.Draw( data );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            _loading = true;
            try
            {
                ChartPanel.Load( storage.GetValue<SettingsStorage>( "ChartPanel", null ) );
                storage.TryLoadSettings( "Annotations", new Action<IEnumerable<SettingsStorage>>( LoadAnnotations ) );
            }
            finally
            {
                _loading = false;
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "ChartPanel", ChartPanel.Save() );
            storage.SetValue( "Annotations", SaveAnnotations() );
        }

        private void ChartPanel_OnRegisterOrder( IChartArea area, Order orderDraft )
        {
            Security security = orderDraft.Security;
            if ( security == null )
            {
                security = area.Elements.OfType<IChartCandleElement>().Select( e => ( ChartPanel.GetSource( e ) as CandleSeries )?.Security ).FirstOrDefault( s => s != null );
                if ( security == null )
                {
                    int num = ( int )new MessageBoxBuilder().Warning().Text( LocalizedStrings.Str1380 ).Owner( this ).Show();
                    return;
                }
            }
            Order order1 = orderDraft;
            if ( order1.Portfolio == null )
            {
                Portfolio portfolio;
                order1.Portfolio = portfolio = ChartPanel.OrderSettings.Portfolio;
            }
            if ( orderDraft.Portfolio == null )
            {
                PortfolioPickerWindow wnd = new PortfolioPickerWindow() { Portfolios = PortfolioDataSource };
                if ( wnd.ShowModal( this ) )
                    orderDraft.Portfolio = ChartPanel.OrderSettings.Portfolio = wnd.SelectedPortfolio;
                if ( orderDraft.Portfolio == null )
                {
                    int num = ( int )new MessageBoxBuilder().Warning().Text( LocalizedStrings.Str1381 ).Owner( this ).Show();
                    return;
                }
            }
            Order order2 = new Order() { Type = new OrderTypes?( OrderTypes.Limit ), Volume = orderDraft.Volume, Direction = orderDraft.Direction, Security = security, Portfolio = orderDraft.Portfolio, Price = security.ShrinkPrice( orderDraft.Price, ShrinkRules.Auto ) };
            IChartActiveOrdersElement activeOrdersElement = area.Elements.OfType<IChartActiveOrdersElement>().FirstOrDefault();
            if ( activeOrdersElement == null )
            {
                activeOrdersElement = ChartPanel.CreateActiveOrdersElement();
                ChartPanel.AddElement( area, activeOrdersElement );
            }
            ChartPanel chartPanel = ChartPanel;
            IChartDrawData data1 = ChartPanel.CreateData();
            IChartActiveOrdersElement element = activeOrdersElement;
            Order order3 = order2;
            Decimal? nullable = new Decimal?( order2.Volume );
            bool? isFrozen = new bool?();
            bool? isError = new bool?();
            Decimal? price = new Decimal?();
            Decimal? balance = nullable;
            OrderStates? state = new OrderStates?();
            IChartDrawData data2 = data1.Add( element, order3, isFrozen, true, false, isError, price, balance, state );
            chartPanel.Draw( data2 );
            _chartOrders.Add( order2, activeOrdersElement );
            new RegisterOrderCommand( order2 ).Process( this, false );
        }

        private void ChartPanel_OnMoveOrder( Order oldOrder, Decimal price )
        {
            Order oldOrder1 = oldOrder;
            Decimal? newPrice = new Decimal?( price );
            Decimal? nullable = new Decimal?();
            Decimal? newVolume = nullable;
            Order newOrder = oldOrder1.ReRegisterClone( newPrice, newVolume );
            IChartActiveOrdersElement activeOrdersElement = _chartOrders.TryGetValue( oldOrder );
            if ( activeOrdersElement != null )
            {
                ChartPanel chartPanel = ChartPanel;
                IChartDrawData data1 = ChartPanel.CreateData();
                IChartActiveOrdersElement element = activeOrdersElement;
                Order order = oldOrder;
                bool? isFrozen = new bool?( true );
                nullable = new Decimal?( price );
                bool? isError = new bool?();
                Decimal? price1 = nullable;
                Decimal? balance = new Decimal?();
                OrderStates? state = new OrderStates?();
                IChartDrawData data2 = data1.Add( element, order, isFrozen, true, false, isError, price1, balance, state );
                chartPanel.Draw( data2 );
            }
            new ReRegisterOrderCommand( oldOrder, newOrder ).Process( this, false );
        }

        private void ChartPanel_OnCancelOrder( Order order )
        {
            IChartActiveOrdersElement element = _chartOrders.TryGetValue( order );
            if ( element != null )
                ChartPanel.Draw( ChartPanel.CreateData().Add( element, order, new bool?( true ), true, false, new bool?(), new Decimal?(), new Decimal?(), new OrderStates?() ) );
            new CancelOrderCommand( order ).Process( this, false );
        }

        private void ChartPanel_OnAnnotationCreated( IChartAnnotation annotation )
        {
            if ( !CanTrackChanges() )
                return;
            RaiseChangedCommand();
        }

        private void ChartPanel_OnAnnotationDeleted( IChartAnnotation annotation )
        {
            _annotations.Remove( annotation );
            if ( !CanTrackChanges() )
                return;
            RaiseChangedCommand();
        }

        private void ChartPanel_AnnotationModified(
          IChartAnnotation annotation,
          ChartDrawData.AnnotationData data )
        {
            if ( _loading || data.X1 == null && data.X2 == null && ( data.Y1 == null && data.Y2 == null ) )
                return;
            _annotations[annotation] = data;
            if ( !CanTrackChanges() )
                return;
            RaiseChangedCommand();
        }

        private SettingsStorage[ ] SaveAnnotations()
        {
            List<SettingsStorage> settingsStorageList = new List<SettingsStorage>();
            foreach ( KeyValuePair<IChartAnnotation, ChartDrawData.AnnotationData> annotation in _annotations )
            {
                IChartAnnotation key = annotation.Key;
                ChartDrawData.AnnotationData persistable = annotation.Value;
                SettingsStorage settingsStorage = new SettingsStorage();
                settingsStorage.SetValue( "Annotation", key.Save() );
                settingsStorage.SetValue( "ChartArea", key.ChartArea.Id );
                settingsStorage.SetValue( "Data", persistable.Save() );
                settingsStorageList.Add( settingsStorage );
            }
            return settingsStorageList.ToArray();
        }

        private void LoadAnnotations( IEnumerable<SettingsStorage> storages )
        {
            foreach ( SettingsStorage storage in storages )
            {
                ChartAnnotation chartAnnotation = storage.GetValue<SettingsStorage>( "Annotation", null ).Load<ChartAnnotation>();
                Guid areaId = storage.GetValue( "ChartArea", new Guid() );
                ChartDrawData.AnnotationData annotationData = storage.GetValue<SettingsStorage>( "Data", null ).Load<ChartDrawData.AnnotationData>();
                IChartArea area = ChartPanel.Areas.FirstOrDefault( a => a.Id == areaId );
                if ( area == null )
                    throw new InvalidOperationException( string.Format( "Chart area {0} not found.", areaId ) );
                IChartDrawData data = ChartPanel.CreateData();
                data.Add( chartAnnotation, annotationData );
                ChartPanel.AddElement( area, chartAnnotation );
                ChartPanel.Draw( data );
                _annotations[chartAnnotation] = annotationData;
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
                _parent = interactiveChart;
                IChartIndicatorElement indicatorElement = elem;
                if ( indicatorElement == null )
                    throw new ArgumentNullException( nameof( elem ) );
                _elem = indicatorElement;
                IIndicator indicator = ui;
                if ( indicator == null )
                    throw new ArgumentNullException( nameof( ui ) );
                UI = indicator;
                CandleSeries candleSeries = series;
                if ( candleSeries == null )
                    throw new ArgumentNullException( nameof( series ) );
                Series = candleSeries;
                Working = UI.Clone();
                UI.Reseted += new Action( OnReseted );
            }

            private void OnReseted()
            {
                _elem.FullTitle = UI.ToString();
                new ChartResetElementCommand( _elem, this ).Process( _parent, false );
            }
        }
    }
}
