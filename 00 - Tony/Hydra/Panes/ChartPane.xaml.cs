using DevExpress.Xpf.Core;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Export;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.IndicatorPainters;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Hydra.Panes
{
    [DisplayNameLoc( "Candles" )]
    [VectorIcon( "CandleStick" )]
    public partial class ChartPane : BaseStudioControl, IPane, IStudioControl, IPersistable, IDisposable, IComponentConnector
    {
        private readonly SynchronizedDictionary<IChartIndicatorElement, IIndicator> _indicators = new SynchronizedDictionary<IChartIndicatorElement, IIndicator>();
        private readonly CachedSynchronizedList<RefPair<IChartElement, int>> _elements = new CachedSynchronizedList<RefPair<IChartElement, int>>();
        private IEnumerable<Candle> _candles;
        private ValueTuple<IChartIndicatorElement, SimpleMovingAverage> _bids;
        private ValueTuple<IChartIndicatorElement, SimpleMovingAverage> _asks;
        private ValueTuple<IChartIndicatorElement, SimpleMovingAverage> _volumes;
        private IEnumerable<QuoteChangeMessage> _quotes;
        private IChartIndicatorElement _indicatorElement;
        private IEnumerable<IndicatorValue> _indicatorValues;
        private readonly ResettableTimer _drawTimer;
        private bool _initializing;

        public ChartPane()
        {
            InitializeComponent();
            _drawTimer = new ResettableTimer( TimeSpan.FromSeconds( 2.0 ), "Chart" );
            _drawTimer.Elapsed += new Action<Func<bool>>( DrawTimerOnElapsed );
            ChartPanel.MinimumRange = 200;
            ChartPanel.IsInteracted = true;
            ChartPanel.FillIndicators();
            SaveWithLayout = false;
        }

        private void OnChartPanelSubscribeCandleElement(
          IChartCandleElement element,
          CandleSeries candleSeries )
        {
            if ( !_initializing )
                _drawTimer.Cancel();
            _elements.Add( new RefPair<IChartElement, int>( element, -1 ) );
            if ( _initializing )
                return;
            _drawTimer.Activate();
        }

        private void OnChartPanelSubscribeIndicatorElement(
          IChartIndicatorElement element,
          CandleSeries candleSeries,
          IIndicator indicator )
        {
            if ( !_initializing )
                _drawTimer.Cancel();
            _elements.Add( new RefPair<IChartElement, int>( element, -1 ) );
            _indicators.Add( element, indicator );
            if ( _initializing )
                return;
            _drawTimer.Activate();
        }

        private void ChartPanelOnUnSubscribeElement( IChartElement element )
        {
            lock ( _elements.SyncRoot )
                _elements.RemoveWhere( p => p.First == element );
            IChartIndicatorElement key = element as IChartIndicatorElement;
            if ( key == null )
                return;
            _indicators.Remove( key );
        }

        private void DrawTimerOnElapsed( Func<bool> canProcess )
        {
            GuiDispatcher.GlobalDispatcher.AddAction( () => CancelButton.Visibility = Visibility.Visible );
            try
            {
                if ( _candles != null )
                {
                    int num = 0;
                    foreach ( IEnumerable<Candle> candles in _candles.Batch( 50, x => x, () => !canProcess() ) )
                    {
                        if ( !canProcess() )
                            break;
                        IChartDrawData data = ChartPanel.CreateData();
                        foreach ( Candle candle in candles )
                        {
                            IChartDrawData.IChartDrawDataItem chartDrawDataItem = data.Group( candle.OpenTime );
                            foreach ( RefPair<IChartElement, int> refPair in _elements.Cache )
                            {
                                if ( refPair.Second < num )
                                {
                                    IChartCandleElement first1 = refPair.First as IChartCandleElement;
                                    if ( first1 != null )
                                    {
                                        chartDrawDataItem.Add( first1, candle );
                                    }
                                    else
                                    {
                                        IChartIndicatorElement first2 = refPair.First as IChartIndicatorElement;
                                        if ( first2 != null )
                                        {
                                            IIndicator indicator = _indicators.TryGetValue( first2 );
                                            if ( indicator == null )
                                                throw new InvalidOperationException( LocalizedStrings.IndicatorNotFound.Put( first2 ) );
                                            chartDrawDataItem.Add( first2, indicator.Process( candle ) );
                                        }
                                    }
                                    refPair.Second = num;
                                }
                            }
                            ++num;
                        }
                        ChartPanel.Draw( data );
                    }
                }
                else if ( _quotes != null )
                {
                    foreach ( IEnumerable<QuoteChangeMessage> quoteChangeMessages in _quotes.Batch( 50, x => x, () => !canProcess() ) )
                    {
                        if ( !canProcess() )
                            break;
                        IChartDrawData data = ChartPanel.CreateData();
                        foreach ( QuoteChangeMessage quoteChangeMessage in quoteChangeMessages )
                        {
                            QuoteChange? nullable1 = quoteChangeMessage.Bids.FirstOr();
                            QuoteChange? nullable2 = quoteChangeMessage.Asks.FirstOr();
                            if ( nullable1.HasValue || nullable2.HasValue )
                            {
                                IChartDrawData.IChartDrawDataItem chartDrawDataItem = data.Group( quoteChangeMessage.ServerTime );
                                Decimal num = new Decimal();
                                if ( nullable1.HasValue )
                                {
                                    QuoteChange quoteChange = nullable1.Value;
                                    chartDrawDataItem.Add( _bids.Item1, _bids.Item2.Process( quoteChange.Price, true ) );
                                    num += quoteChange.Volume;
                                }
                                if ( nullable2.HasValue )
                                {
                                    QuoteChange quoteChange = nullable2.Value;
                                    chartDrawDataItem.Add( _asks.Item1, _asks.Item2.Process( quoteChange.Price, true ) );
                                    num += quoteChange.Volume;
                                }
                                chartDrawDataItem.Add( _volumes.Item1, _volumes.Item2.Process( num, true ) );
                            }
                        }
                        ChartPanel.Draw( data );
                    }
                }
                else
                {
                    if ( _indicatorValues == null )
                        throw new InvalidOperationException();
                    foreach ( IEnumerable<IndicatorValue> indicatorValues in _indicatorValues.Batch( 50, x => x, () => !canProcess() ) )
                    {
                        if ( !canProcess() )
                            break;
                        IChartDrawData data = ChartPanel.CreateData();
                        foreach ( IndicatorValue indicatorValue in indicatorValues )
                            data.Group( indicatorValue.Time ).Add( _indicatorElement, indicatorValue.Value );
                        ChartPanel.Draw( data );
                    }
                }
            }
            catch ( Exception ex )
            {
                ex.LogError( null );
            }
            finally
            {
                GuiDispatcher.GlobalDispatcher.AddAction( () => CancelButton.Visibility = Visibility.Collapsed );
            }
        }

        public void DrawCandles(
          CandleSeries series,
          IEnumerable<Candle> candles,
          TimeZoneInfo timeZone )
        {
            if ( series == null )
                throw new ArgumentNullException( nameof( series ) );
            if ( candles == null )
                throw new ArgumentNullException( nameof( candles ) );
            ChartPanel.SubscribeCandleElement += new Action<IChartCandleElement, CandleSeries>( OnChartPanelSubscribeCandleElement );
            ChartPanel.SubscribeIndicatorElement += new Action<IChartIndicatorElement, CandleSeries, IIndicator>( OnChartPanelSubscribeIndicatorElement );
            ChartPanel.UnSubscribeElement += new Action<IChartElement>( ChartPanelOnUnSubscribeElement );
            if ( timeZone != null )
                ChartPanel.TimeZone = timeZone;
            Title = LocalizedStrings.Str3200 + " " + series?.ToString();
            _candles = candles;
            _initializing = true;
            try
            {
                IChartArea area1 = ChartPanel.CreateArea();
                area1.Title = LocalizedStrings.Candles;
                area1.Height = 210.0;
                ChartPanel.AddArea( area1 );
                IChartCandleElement candleElement = ChartPanel.CreateCandleElement();
                ChartPanel.AddElement( area1, candleElement, series );
                IChartArea area2 = ChartPanel.CreateArea();
                area2.Title = LocalizedStrings.Volume;
                area2.Height = 130.0;
                ChartPanel.AddArea( area2 );
                IChartIndicatorElement indicatorElement = ChartPanel.CreateIndicatorElement();
                indicatorElement.IndicatorPainter = new VolumePainter();
                ChartPanel.AddElement( area2, indicatorElement, series, new VolumeIndicator() );
                CancelButton.Visibility = Visibility.Visible;
            }
            finally
            {
                _initializing = false;
            }
            _drawTimer.Activate();
        }

        public void DrawLines(
          SecurityId securityId,
          IEnumerable<QuoteChangeMessage> quotes,
          TimeZoneInfo timeZone )
        {
            if ( quotes == null )
                throw new ArgumentNullException( nameof( quotes ) );
            if ( timeZone != null )
                ChartPanel.TimeZone = timeZone;
            Title = string.Format( "{0} {1} {2}", LocalizedStrings.Str3200, securityId, LocalizedStrings.Spread );
            _quotes = quotes;
            _initializing = true;
            try
            {
                IChartArea area1 = ChartPanel.CreateArea();
                area1.Title = LocalizedStrings.Str1659;
                area1.Height = 210.0;
                ChartPanel.AddArea( area1 );
                IChartIndicatorElement indicatorElement1 = ChartPanel.CreateIndicatorElement();
                indicatorElement1.Color = Color.DarkBlue;
                ChartPanel.AddElement( area1, indicatorElement1 );
                IChartIndicatorElement indicatorElement2 = ChartPanel.CreateIndicatorElement();
                indicatorElement2.Color = Color.DarkRed;
                ChartPanel.AddElement( area1, indicatorElement2 );
                IChartArea area2 = ChartPanel.CreateArea();
                area2.Title = LocalizedStrings.Volume;
                area2.Height = 130.0;
                ChartPanel.AddArea( area2 );
                IChartIndicatorElement indicatorElement3 = ChartPanel.CreateIndicatorElement();
                indicatorElement3.DrawStyle = ChartIndicatorDrawStyles.Histogram;
                ChartPanel.AddElement( area2, indicatorElement3 );
                IChartIndicatorElement indicatorElement4 = indicatorElement1;
                SimpleMovingAverage simpleMovingAverage1 = new SimpleMovingAverage();
                simpleMovingAverage1.Length = 1;
                _bids = new ValueTuple<IChartIndicatorElement, SimpleMovingAverage>( indicatorElement4, simpleMovingAverage1 );
                IChartIndicatorElement indicatorElement5 = indicatorElement2;
                SimpleMovingAverage simpleMovingAverage2 = new SimpleMovingAverage();
                simpleMovingAverage2.Length = 1;
                _asks = new ValueTuple<IChartIndicatorElement, SimpleMovingAverage>( indicatorElement5, simpleMovingAverage2 );
                IChartIndicatorElement indicatorElement6 = indicatorElement3;
                SimpleMovingAverage simpleMovingAverage3 = new SimpleMovingAverage();
                simpleMovingAverage3.Length = 1;
                _volumes = new ValueTuple<IChartIndicatorElement, SimpleMovingAverage>( indicatorElement6, simpleMovingAverage3 );
                CancelButton.Visibility = Visibility.Visible;
            }
            finally
            {
                _initializing = false;
            }
            _drawTimer.Activate();
        }

        public void DrawIndicator(
          SecurityId securityId,
          string indicatorName,
          IEnumerable<IndicatorValue> values,
          IChartIndicatorPainter painter,
          TimeZoneInfo timeZone )
        {
            if ( values == null )
                throw new ArgumentNullException( nameof( values ) );
            if ( timeZone != null )
                ChartPanel.TimeZone = timeZone;
            Title = string.Format( "{0} {1} {2} {3}", LocalizedStrings.Str3200, securityId, indicatorName, LocalizedStrings.Str1981 );
            _indicatorValues = values;
            _initializing = true;
            try
            {
                IChartArea area = ChartPanel.CreateArea();
                area.Title = LocalizedStrings.Indicators;
                area.Height = 210.0;
                ChartPanel.AddArea( area );
                _indicatorElement = ChartPanel.CreateIndicatorElement();
                _indicatorElement.IndicatorPainter = painter;
                ChartPanel.AddElement( area, _indicatorElement );
                CancelButton.Visibility = Visibility.Visible;
            }
            finally
            {
                _initializing = false;
            }
            _drawTimer.Activate();
        }

        public override void Load( SettingsStorage storage )
        {
            ChartPanel.Load( storage );
            base.Load( storage );
        }

        public override void Save( SettingsStorage storage )
        {
            ChartPanel.Save( storage );
            base.Save( storage );
        }

        bool IPane.IsValid
        {
            get
            {
                return false;
            }
        }

        public override void Dispose()
        {
            _drawTimer.Dispose();
            base.Dispose();
        }

        private void Cancel_OnClick( object sender, RoutedEventArgs e )
        {
            _drawTimer.Cancel();
        }        
    }
}
