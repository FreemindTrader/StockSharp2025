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
        private readonly Dictionary<IChartIndicatorElement, InteractiveChart.IndicatorPair> _indicators = new Dictionary<IChartIndicatorElement, InteractiveChart.IndicatorPair>();
        private readonly Dictionary<CandleSeries, Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[ ]> _indicatorsBySeries = new Dictionary<CandleSeries, Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[ ]>();
        private readonly Dictionary<Order, IChartActiveOrdersElement> _chartOrders = new Dictionary<Order, IChartActiveOrdersElement>();
        private readonly Dictionary<IChartAnnotation, ChartDrawData.AnnotationData> _annotations = new Dictionary<IChartAnnotation, ChartDrawData.AnnotationData>();
        
        private readonly List< ( IChartTradeElement element, SecurityId secid, Subscription subscription )> _tradeElements = new List<ValueTuple<IChartTradeElement, SecurityId, Subscription>>();
        private bool _loading;
        private bool _isInteracted;
        
        public InteractiveChart()
        {
            this.InitializeComponent();
            this.ChartPanel.SettingsChanged += ( Action )( () =>
              {
                  if ( !this.CanTrackChanges() )
                      return;
                  this.RaiseChangedCommand();
              } );
            this.ChartPanel.Areas.Added += new Action<IChartArea>( this.AreasOnAdded );
            this.ChartPanel.Areas.Removed += new Action<IChartArea>( this.AreasOnRemoved );
            this.ChartPanel.Areas.Cleared += new Action( this.AreasOnCleared );
            this.ChartPanel.DisableIndicatorReset = true;
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.Register<EntityCommand<MyTrade>>( ( object )this, true, new Action<EntityCommand<MyTrade>>( this.OnMyTradeCommand ), ( Func<EntityCommand<MyTrade>, bool> )null );
            commandService.Register<CandleCommand>( ( object )this, false, new Action<CandleCommand>( this.OnCandleCommand ), ( Func<CandleCommand, bool> )null );
            commandService.Register<SelectCommand>( ( object )this, true, ( Action<SelectCommand> )( cmd =>
                {
                    if ( this.ChartPanel.OrderSettings.Security != null )
                        return;
                    Security instance = cmd.Instance as Security;
                    if ( instance == null )
                        return;
                    this.ChartPanel.OrderSettings.Security = instance;
                } ), ( Func<SelectCommand, bool> )null );
            commandService.Register<OrderCommand>( ( object )this, true, ( Action<OrderCommand> )( cmd =>
                {
                    if ( !this.ChartPanel.OrderCreationMode )
                        return;
                    Order order = cmd.Entity;
                    IChartActiveOrdersElement activeOrdersElement;
                    if ( !this._chartOrders.TryGetValue( order, out activeOrdersElement ) )
                    {
                        if ( order.State.IsFinal() )
                            return;
                        IChartCandleElement chartCandleElement = this.ChartPanel.Elements.OfType<IChartCandleElement>().FirstOrDefault<IChartCandleElement>( ( Func<IChartCandleElement, bool> )( e => ( ( CandleSeries )this.ChartPanel.GetSource( ( IChartElement )e ) )?.Security == order.Security ) );
                        if ( chartCandleElement == null )
                            return;
                        activeOrdersElement = chartCandleElement.ChartArea.Elements.OfType<IChartActiveOrdersElement>().FirstOrDefault<IChartActiveOrdersElement>();
                        if ( activeOrdersElement == null )
                        {
                            activeOrdersElement = this.ChartPanel.CreateActiveOrdersElement();
                            this.ChartPanel.AddElement( chartCandleElement.ChartArea, ( IChartElement )activeOrdersElement );
                        }
                        this._chartOrders.Add( order, activeOrdersElement );
                    }
                    ChartPanel chartPanel = this.ChartPanel;
                    IChartDrawData data1 = this.ChartPanel.CreateData();
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
                } ), ( Func<OrderCommand, bool> )null );
            commandService.Register<OrderFailCommand>( ( object )this, true, ( Action<OrderFailCommand> )( cmd =>
                {
                    Order order1 = cmd.Entity.Order;
                    IChartActiveOrdersElement activeOrdersElement;
                    if ( !this._chartOrders.TryGetValue( order1, out activeOrdersElement ) )
                        return;
                    ChartPanel chartPanel = this.ChartPanel;
                    IChartDrawData data1 = this.ChartPanel.CreateData();
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
                    this._chartOrders.Remove( order1 );
                } ), ( Func<OrderFailCommand, bool> )null );
            commandService.Register<ChartAddElementCommand>( ( object )this, false, new Action<ChartAddElementCommand>( this.OnChartAddElementCommand ), ( Func<ChartAddElementCommand, bool> )null );
            commandService.Register<ChartRemoveElementCommand>( ( object )this, false, new Action<ChartRemoveElementCommand>( this.OnChartRemoveElementCommand ), ( Func<ChartRemoveElementCommand, bool> )null );
            commandService.Register<ChartResetElementCommand>( ( object )this, false, new Action<ChartResetElementCommand>( this.OnChartResetElementCommand ), ( Func<ChartResetElementCommand, bool> )null );
            commandService.Register<FirstInitSecuritiesCommand>( ( object )this, false, ( Action<FirstInitSecuritiesCommand> )( cmd =>
                {
                    if ( this.ChartPanel.Elements.Any<IChartElement>() )
                        return;
                    IChartArea area = this.ChartPanel.Areas.FirstOrDefault<IChartArea>();
                    if ( area == null )
                        return;
                    CandleSeries candleSeries = new CandleSeries( typeof( TimeFrameCandle ), cmd.Securities.First<Security>(), ( object )TimeSpan.FromMinutes( 1.0 ) ) { From = new DateTimeOffset?( DateTimeOffset.Now.AddDays( -5.0 ) ) };
                    IChartCandleElement candleElement = this.ChartPanel.CreateCandleElement();
                    this.ChartPanel.AddElement( area, candleElement, candleSeries );
                } ), ( Func<FirstInitSecuritiesCommand, bool> )null );
            commandService.Register<FirstInitPortfoliosCommand>( ( object )this, true, ( Action<FirstInitPortfoliosCommand> )( cmd => this.ChartPanel.OrderSettings.Portfolio = cmd.Portfolios.First<Portfolio>() ), ( Func<FirstInitPortfoliosCommand, bool> )null );
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
            ( ( INotifyPropertyChanged )this.ChartPanel ).PropertyChanged += ( PropertyChangedEventHandler )( ( _1, _2 ) => updateInteracted() );
            updateInteracted();
            this.WhenLoaded( ( Action )( () => new SubscribeCommand( DataType.Transactions.ToSubscription() ).Process( ( object )this, false ) ) );

            void updateInteracted()
            {
                this._isInteracted = this.ChartPanel.IsInteracted;
            }
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            foreach ( CandleSeries key in this.ChartPanel.Elements.OfType<IChartCandleElement>().Select<IChartCandleElement, object>( new Func<IChartCandleElement, object>( this.ChartPanel.GetSource ) ).Where<object>( ( Func<object, bool> )( s => s != null ) ) )
            {
                RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset> refQuadruple;
                if ( this._candles.TryGetValue( key, out refQuadruple ) )
                    new UnSubscribeCommand( refQuadruple.Third ).Process( ( object )this, false );
            }
            commandService.UnRegister<OrderCommand>( ( object )this );
            commandService.UnRegister<OrderFailCommand>( ( object )this );
            commandService.UnRegister<CandleCommand>( ( object )this );
            commandService.UnRegister<SelectCommand>( ( object )this );
            commandService.UnRegister<ChartAddElementCommand>( ( object )this );
            commandService.UnRegister<ChartRemoveElementCommand>( ( object )this );
            commandService.UnRegister<ChartResetElementCommand>( ( object )this );
            commandService.UnRegister<FirstInitSecuritiesCommand>( ( object )this );
            commandService.UnRegister<FirstInitPortfoliosCommand>( ( object )this );
            this.ChartPanel.SubscribeCandleElement -= new Action<IChartCandleElement, CandleSeries>( this.Chart_OnSubscribeCandleElement );
            this.ChartPanel.SubscribeIndicatorElement -= new Action<IChartIndicatorElement, CandleSeries, IIndicator>( this.Chart_OnSubscribeIndicatorElement );
            this.ChartPanel.SubscribeOrderElement -= new Action<IChartOrderElement, Security>( this.Chart_OnSubscribeOrderElement );
            this.ChartPanel.SubscribeTradeElement -= new Action<IChartTradeElement, Security>( this.Chart_OnSubscribeTradeElement );
            this.ChartPanel.UnSubscribeElement -= new Action<IChartElement>( this.OnChartPanelUnSubscribeElement );
            base.Dispose();
        }

        public override void FirstTimeInit()
        {
            base.FirstTimeInit();
            this.ChartPanel.Areas.Add( this.ChartPanel.CreateArea() );
        }

        private bool CanTrackChanges()
        {
            return this._isInteracted && !this._loading;
        }

        private void AreasOnCleared()
        {
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void AreasOnRemoved( IChartArea area )
        {
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void AreasOnAdded( IChartArea area )
        {
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private void Chart_OnSubscribeTradeElement( IChartTradeElement element, Security security )
        {
            this.OnChartAddElementCommand( new ChartAddElementCommand( ( IChartElement )element, ( object )security ) );
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
            this.OnChartAddElementCommand( new ChartAddElementCommand( ( IChartElement )element, ( object )series ) );
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
            this.OnChartAddElementCommand( new ChartAddElementCommand( ( IChartElement )element, ( object )series, indicator ) );
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
            this.ChartPanel.Reset( ( IEnumerable<IChartElement> )new IChartElement[1]
            {
        command.Element
            } );
            InteractiveChart.IndicatorPair tag = ( InteractiveChart.IndicatorPair )command.Tag;
            tag.Working.Load( tag.UI.Save() );
            RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset> refQuadruple = this._candles.TryGetValue<CandleSeries, RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset>>( tag.Series );
            if ( refQuadruple == null )
                return;
            IChartDrawData data = this.ChartPanel.CreateData();
            foreach ( KeyValuePair<DateTimeOffset, Candle> keyValuePair in refQuadruple.Second )
            {
                Candle candle = keyValuePair.Value;
                data.Group( candle.OpenTime ).Add( command.Element, ( object )tag.Working.Process( candle ) );
            }
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
            IChartCandleElement element1 = command.Element as IChartCandleElement;
            if ( element1 != null )
            {
                CandleSeries source = ( CandleSeries )command.Source;
                Subscription subscription = new Subscription( source );
                if ( !this._candles.TryAdd( source, RefTuple.Create<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset>( element1, new SortedDictionary<DateTimeOffset, Candle>(), subscription, DateTimeOffset.MinValue ) ) )
                    return;
                new SubscribeCommand( subscription ).Process( ( object )this, false );
            }
            else
            {
                IChartIndicatorElement element2 = command.Element as IChartIndicatorElement;
                if ( element2 != null )
                {
                    if ( this._indicators.ContainsKey( element2 ) )
                        return;
                    CandleSeries source = ( CandleSeries )command.Source;
                    RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset> refQuadruple;
                    if ( !this._candles.TryGetValue( source, out refQuadruple ) )
                        return;
                    InteractiveChart.IndicatorPair indicatorPair = new InteractiveChart.IndicatorPair( this, element2, command.Indicator, source );
                    this._indicators.Add( element2, indicatorPair );
                    Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[ ] first = this._indicatorsBySeries.SafeAdd<CandleSeries, Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[ ]>( source, ( Func<CandleSeries, Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[ ]> )( key => Array.Empty<Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>>() ) );
                    this._indicatorsBySeries[source] = ( ( IEnumerable<Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>> )first.Concat<Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>>( new Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[1]
                    {
            Tuple.Create<IChartIndicatorElement, InteractiveChart.IndicatorPair>(element2, indicatorPair)
                    } ) ).ToArray<Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>>();
                    IChartDrawData data = this.ChartPanel.CreateData();
                    foreach ( KeyValuePair<DateTimeOffset, Candle> keyValuePair in refQuadruple.Second )
                    {
                        Candle candle = keyValuePair.Value;
                        data.Group( candle.OpenTime ).Add( element2, indicatorPair.Working.Process( candle ) );
                    }
                    this.ChartPanel.Draw( data );
                }
                else
                {
                    IChartTradeElement trd = command.Element as IChartTradeElement;
                    if ( trd == null )
                        return;
                    Security source = ( Security )command.Source;
                    if ( !this._tradeElements.FirstOrDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>( ( Func<ValueTuple<IChartTradeElement, SecurityId, Subscription>, bool> )( p => p.Item1 == trd ) ).IsDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>() )
                        return;
                    Subscription subscription1 = DataType.Transactions.ToSubscription();
                    this._tradeElements.Add( new ValueTuple<IChartTradeElement, SecurityId, Subscription>( trd, source.Id.ToSecurityId( ( SecurityIdGenerator )null ), subscription1 ) );
                    Subscription subscription2 = DataType.Transactions.ToSubscription();
                    ( ( OrderStatusMessage )subscription2.SubscriptionMessage ).Count = new long?( 1000L );
                    new SubscribeCommand( subscription2 ).Process( ( object )this, false );
                    new SubscribeCommand( subscription1 ).Process( ( object )this, false );
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
            IChartIndicatorElement ind = command.Element as IChartIndicatorElement;
            if ( ind != null )
            {
                this._indicators.Remove( ind );
                foreach ( KeyValuePair<CandleSeries, Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[ ]> keyValuePair in this._indicatorsBySeries.ToArray<KeyValuePair<CandleSeries, Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[ ]>>() )
                {
                    Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[ ] array = ( ( IEnumerable<Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>> )keyValuePair.Value ).Where<Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>>( ( Func<Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>, bool> )( t => t.Item1 != ind ) ).ToArray<Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>>();
                    if ( array.Length == 0 )
                        this._indicatorsBySeries.Remove( keyValuePair.Key );
                    else
                        this._indicatorsBySeries[keyValuePair.Key] = array;
                }
            }
            else if ( command.Element is IChartCandleElement )
            {
                CandleSeries source = ( CandleSeries )command.Source;
                RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset> refQuadruple;
                if ( !this._candles.TryGetAndRemove<CandleSeries, RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset>>( source, out refQuadruple ) )
                    return;
                new UnSubscribeCommand( refQuadruple.Third ).Process( ( object )this, false );
                if ( this._candles.Count != 0 || source.Security != this.ChartPanel.OrderSettings.Security )
                    return;
                ( ( DispatcherObject )this ).GuiAsync( ( Action )( () =>
                      {
                          this.ChartPanel.OrderSettings.Security = ( Security )null;
                          foreach ( IChartActiveOrdersElement activeOrdersElement in command.Element.PersistantChartArea.Elements.OfType<IChartActiveOrdersElement>() )
                          {
                              this.ChartPanel.Reset( ( IEnumerable<IChartElement> )new IChartActiveOrdersElement[1]
                {
              activeOrdersElement
                          } );
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
                IChartTradeElement trd = command.Element as IChartTradeElement;
                if ( trd == null )
                    return;
                ValueTuple<IChartTradeElement, SecurityId, Subscription> valueTuple = this._tradeElements.FirstOrDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>( ( Func<ValueTuple<IChartTradeElement, SecurityId, Subscription>, bool> )( p => p.Item1 == trd ) );
                if ( valueTuple.IsDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>() )
                    return;
                new UnSubscribeCommand( valueTuple.Item3 ).Process( ( object )this, false );
                this._tradeElements.RemoveWhere<ValueTuple<IChartTradeElement, SecurityId, Subscription>>( ( Func<ValueTuple<IChartTradeElement, SecurityId, Subscription>, bool> )( p => p.Item1 == trd ) );
            }
        }

        private void OnMyTradeCommand( EntityCommand<MyTrade> cmd )
        {
            lock ( this._candles )
                this.OnMyTradeCommandImpl( cmd );
        }

        private void OnMyTradeCommandImpl( EntityCommand<MyTrade> cmd )
        {
            SecurityId securityId = cmd.Entity.Trade.Security.Id.ToSecurityId( ( SecurityIdGenerator )null );
            ValueTuple<IChartTradeElement, SecurityId, Subscription> valueTuple = this._tradeElements.FirstOrDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>( ( Func<ValueTuple<IChartTradeElement, SecurityId, Subscription>, bool> )( p => p.Item3 == cmd.Subscription ) );
            if ( valueTuple.IsDefault<ValueTuple<IChartTradeElement, SecurityId, Subscription>>() || valueTuple.Item2 != securityId || !this.ChartPanel.Elements.Contains<IChartElement>( ( IChartElement )valueTuple.Item1 ) )
                return;
            IChartDrawData data = this.ChartPanel.CreateData();
            data.Group( cmd.Entity.Trade.Time ).Add( valueTuple.Item1, cmd.Entity );
            this.ChartPanel.Draw( data );
        }

        private void OnCandleCommand( CandleCommand cmd )
        {
            lock ( this._candles )
                this.OnCandleCommandImpl( cmd );
        }

        private void OnCandleCommandImpl( CandleCommand cmd )
        {
            Candle candle = cmd.Candle;
            RefQuadruple<IChartCandleElement, SortedDictionary<DateTimeOffset, Candle>, Subscription, DateTimeOffset> refQuadruple;
            if ( !this._candles.TryGetValue( cmd.Series, out refQuadruple ) )
                return;
            DateTimeOffset openTime = candle.OpenTime;
            if ( openTime < refQuadruple.Fourth )
                return;
            refQuadruple.Second[openTime] = candle;
            refQuadruple.Fourth = openTime;
            IChartDrawData data = this.ChartPanel.CreateData();
            IChartDrawData.IChartDrawDataItem chartDrawDataItem = data.Group( openTime ).Add( refQuadruple.First, candle );
            Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair>[ ] tupleArray;
            if ( this._indicatorsBySeries.TryGetValue( cmd.Series, out tupleArray ) )
            {
                foreach ( Tuple<IChartIndicatorElement, InteractiveChart.IndicatorPair> tuple in tupleArray )
                    chartDrawDataItem.Add( tuple.Item1, tuple.Item2.Working.Process( candle ) );
            }
            this.ChartPanel.Draw( data );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this._loading = true;
            try
            {
                this.ChartPanel.Load( storage.GetValue<SettingsStorage>( "ChartPanel", ( SettingsStorage )null ) );
                storage.TryLoadSettings<IEnumerable<SettingsStorage>>( "Annotations", new Action<IEnumerable<SettingsStorage>>( this.LoadAnnotations ) );
            }
            finally
            {
                this._loading = false;
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "ChartPanel", this.ChartPanel.Save() );
            storage.SetValue<SettingsStorage[ ]>( "Annotations", this.SaveAnnotations() );
        }

        private void ChartPanel_OnRegisterOrder( IChartArea area, Order orderDraft )
        {
            Security security = orderDraft.Security;
            if ( security == null )
            {
                security = area.Elements.OfType<IChartCandleElement>().Select<IChartCandleElement, Security>( ( Func<IChartCandleElement, Security> )( e => ( this.ChartPanel.GetSource( ( IChartElement )e ) as CandleSeries )?.Security ) ).FirstOrDefault<Security>( ( Func<Security, bool> )( s => s != null ) );
                if ( security == null )
                {
                    int num = ( int )new MessageBoxBuilder().Warning().Text( LocalizedStrings.Str1380 ).Owner( ( DependencyObject )this ).Show();
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
                PortfolioPickerWindow wnd = new PortfolioPickerWindow() { Portfolios = BaseStudioControl.PortfolioDataSource };
                if ( wnd.ShowModal( ( DependencyObject )this ) )
                    orderDraft.Portfolio = this.ChartPanel.OrderSettings.Portfolio = wnd.SelectedPortfolio;
                if ( orderDraft.Portfolio == null )
                {
                    int num = ( int )new MessageBoxBuilder().Warning().Text( LocalizedStrings.Str1381 ).Owner( ( DependencyObject )this ).Show();
                    return;
                }
            }
            Order order2 = new Order() { Type = new OrderTypes?( OrderTypes.Limit ), Volume = orderDraft.Volume, Direction = orderDraft.Direction, Security = security, Portfolio = orderDraft.Portfolio, Price = security.ShrinkPrice( orderDraft.Price, ShrinkRules.Auto ) };
            IChartActiveOrdersElement activeOrdersElement = area.Elements.OfType<IChartActiveOrdersElement>().FirstOrDefault<IChartActiveOrdersElement>();
            if ( activeOrdersElement == null )
            {
                activeOrdersElement = this.ChartPanel.CreateActiveOrdersElement();
                this.ChartPanel.AddElement( area, ( IChartElement )activeOrdersElement );
            }
            ChartPanel chartPanel = this.ChartPanel;
            IChartDrawData data1 = this.ChartPanel.CreateData();
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
            this._chartOrders.Add( order2, activeOrdersElement );
            new RegisterOrderCommand( order2 ).Process( ( object )this, false );
        }

        private void ChartPanel_OnMoveOrder( Order oldOrder, Decimal price )
        {
            Order oldOrder1 = oldOrder;
            Decimal? newPrice = new Decimal?( price );
            Decimal? nullable = new Decimal?();
            Decimal? newVolume = nullable;
            Order newOrder = oldOrder1.ReRegisterClone( newPrice, newVolume );
            IChartActiveOrdersElement activeOrdersElement = this._chartOrders.TryGetValue<Order, IChartActiveOrdersElement>( oldOrder );
            if ( activeOrdersElement != null )
            {
                ChartPanel chartPanel = this.ChartPanel;
                IChartDrawData data1 = this.ChartPanel.CreateData();
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
            new ReRegisterOrderCommand( oldOrder, newOrder ).Process( ( object )this, false );
        }

        private void ChartPanel_OnCancelOrder( Order order )
        {
            IChartActiveOrdersElement element = this._chartOrders.TryGetValue<Order, IChartActiveOrdersElement>( order );
            if ( element != null )
                this.ChartPanel.Draw( this.ChartPanel.CreateData().Add( element, order, new bool?( true ), true, false, new bool?(), new Decimal?(), new Decimal?(), new OrderStates?() ) );
            new CancelOrderCommand( order ).Process( ( object )this, false );
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

        private void ChartPanel_AnnotationModified(
          IChartAnnotation annotation,
          ChartDrawData.AnnotationData data )
        {
            if ( this._loading || data.X1 == null && data.X2 == null && ( data.Y1 == null && data.Y2 == null ) )
                return;
            this._annotations[annotation] = data;
            if ( !this.CanTrackChanges() )
                return;
            this.RaiseChangedCommand();
        }

        private SettingsStorage[ ] SaveAnnotations()
        {
            List<SettingsStorage> settingsStorageList = new List<SettingsStorage>();
            foreach ( KeyValuePair<IChartAnnotation, ChartDrawData.AnnotationData> annotation in this._annotations )
            {
                IChartAnnotation key = annotation.Key;
                ChartDrawData.AnnotationData persistable = annotation.Value;
                SettingsStorage settingsStorage = new SettingsStorage();
                settingsStorage.SetValue<SettingsStorage>( "Annotation", key.Save() );
                settingsStorage.SetValue<Guid>( "ChartArea", key.ChartArea.Id );
                settingsStorage.SetValue<SettingsStorage>( "Data", persistable.Save() );
                settingsStorageList.Add( settingsStorage );
            }
            return settingsStorageList.ToArray();
        }

        private void LoadAnnotations( IEnumerable<SettingsStorage> storages )
        {
            foreach ( SettingsStorage storage in storages )
            {
                ChartAnnotation chartAnnotation = storage.GetValue<SettingsStorage>( "Annotation", ( SettingsStorage )null ).Load<ChartAnnotation>();
                Guid areaId = storage.GetValue<Guid>( "ChartArea", new Guid() );
                ChartDrawData.AnnotationData annotationData = storage.GetValue<SettingsStorage>( "Data", ( SettingsStorage )null ).Load<ChartDrawData.AnnotationData>();
                IChartArea area = this.ChartPanel.Areas.FirstOrDefault<IChartArea>( ( Func<IChartArea, bool> )( a => a.Id == areaId ) );
                if ( area == null )
                    throw new InvalidOperationException( string.Format( "Chart area {0} not found.", ( object )areaId ) );
                IChartDrawData data = this.ChartPanel.CreateData();
                data.Add( ( IChartAnnotation )chartAnnotation, ( IAnnotationData )annotationData );
                this.ChartPanel.AddElement( area, ( IChartElement )chartAnnotation );
                this.ChartPanel.Draw( data );
                this._annotations[( IChartAnnotation )chartAnnotation] = annotationData;
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
                this.Working = this.UI.Clone();
                this.UI.Reseted += new Action( this.OnReseted );
            }

            private void OnReseted()
            {
                this._elem.FullTitle = this.UI.ToString();
                new ChartResetElementCommand( ( IChartElement )this._elem, ( object )this ).Process( ( object )this._parent, false );
            }
        }
    }
}
