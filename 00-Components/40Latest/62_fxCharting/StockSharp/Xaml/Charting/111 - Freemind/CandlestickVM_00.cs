using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using StockSharp.Charting;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Common;
using SciChart.Charting.Model.ChartData;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Utility;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Model;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.Charting.ATony;
using StockSharp.Xaml.Charting.Definitions;
using System;
using System.Collections.Generic; 
using fx.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using fx.Bars;
using fx.Algorithm;
using fx.Indicators;
using fx.Base;


#pragma warning disable 067

namespace StockSharp.Xaml.Charting
{
    public class DefaultSelectableMetadata : IPointMetadata
    {
        private bool _isSelected = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;
            }
        }
    }

    internal class NullBar
    {
        public DateTime BarTime { get; set; }    // Open (the first price) of the time period        
        public TimeSpan Period { get; set; }    // Open (the first price) of the time period        
        public double Open { get; set; }    // Open (the first price) of the time period        
        public double High { get; set; }    // High (the greatest price) of the time period
        public double Low { get; set; }    // Low (the smallest price) of the time period
        public double Close { get; set; }    // Close (the latest price) of the time period

        public NullBar( ChartDrawData.sCandle bar )
        {
            BarTime = bar.Time;
            Period  = ( TimeSpan ) bar.DataType.Arg;
            Open    = bar.OpenPrice;
            High    = bar.HighPrice;
            Low     = bar.LowPrice;
            Close   = bar.ClosePrice;
        }
    }

    internal partial class CandlestickVM : ChartCompentWpfUiDomain<ChartCandleElementEx>, IPaletteProvider, IStrokePaletteProvider, IFillPaletteProvider, INullBar
    {
        private double                                      _pnfBoxSize = 0.2;
        private readonly OhlcDataSeries< DateTime, double > _ohlcDataSeries;
        private readonly XyDataSeries< DateTime, double >   _xyDataSeries;
        //private ChartSeriesViewModel                        _chartSeriesViewModel;
        //private TimeframeSegmentDataSeries                  _timeframeSegmentDataSeries;
        private DateTime                                    _dateTimeUtc;
        private readonly Sequence _latestBarIndex = new Sequence( -1 );
        private bool                                        _setMinMax;
        private bool                                        _setNewRange;
        private IndexRange                                  _indexRange;
        private int                                         _totalMinutes;
        private double                                      _priceStep;
        private ChartElementViewModel                                     _openViewModel;
        private ChartElementViewModel                                     _highViewModel;
        private ChartElementViewModel                                     _lowViewModel;
        private ChartElementViewModel                                     _closeViewModel;
        private ChartElementViewModel                                     _lineViewModel;
        private bool                                        _isPnfBoxSizeSet;
        private Func< DateTimeOffset, bool, bool, Color? >  _colorerFunction;
        private IDataSeries                                 _candlestickData;
        private IRenderableSeries                           _candlestickSeries;

        private SBar _lastNullBar;
        
        
        private bool _barEventsSubscribed = false;

        private int _fifoCapacity;

        public CandlestickVM( ChartCandleElementEx element ) : base( element )
        {
            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  Tony: Add Fifo Capacity to  DataSeries as we don't need to keep all the databars for BackTesting
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- */

            _fifoCapacity = element.FifoCapacity;

            if ( _fifoCapacity > 0 )
            {
                _ohlcDataSeries = new OhlcDataSeries<DateTime, double>() { FifoCapacity = _fifoCapacity };
                _xyDataSeries   = new XyDataSeries<DateTime, double>() { FifoCapacity = _fifoCapacity }; 
            }
            else
            {
                _ohlcDataSeries = new OhlcDataSeries<DateTime, double>();
                _xyDataSeries   = new XyDataSeries<DateTime, double>();
            }

            
        }

        private IDataSeries GetDataSeriesByDrawStyle( )
        {
            //if( ChartComponent.DrawStyle.IsVolumeProfileChart( ) )
            //{
            //    return _timeframeSegmentDataSeries;
            //}

            if ( ChartComponentView.DrawStyle != ChartCandleDrawStyles.Area )
            {
                return _ohlcDataSeries;
            }

            return _xyDataSeries;
        }

        protected override void Init( )
        {
            base.Init( );
            string[ ] strArray = new string[ 2 ]
            {
                "UpFillColor",
                "DownFillColor"
            };

            ChartComponentUiDomain.AddChild( _openViewModel = new ChartElementViewModel( "O", ChartComponentView, new Func<SeriesInfo, Color>( GetLegendColor ), ( s => ( s as OhlcSeriesInfo )?.FormattedOpenValue ), strArray ) );
            ChartComponentUiDomain.AddChild( _highViewModel = new ChartElementViewModel( "H", ChartComponentView, new Func<SeriesInfo, Color>( GetLegendColor ), ( s => ( s as OhlcSeriesInfo )?.FormattedHighValue ), strArray ) );
            ChartComponentUiDomain.AddChild( _lowViewModel = new ChartElementViewModel( "L", ChartComponentView, new Func<SeriesInfo, Color>( GetLegendColor ), ( s => ( s as OhlcSeriesInfo )?.FormattedLowValue ), strArray ) );
            ChartComponentUiDomain.AddChild( _closeViewModel = new ChartElementViewModel( "C", ChartComponentView, new Func<SeriesInfo, Color>( GetLegendColor ), ( s => ( s as OhlcSeriesInfo )?.FormattedCloseValue ), strArray ) );
            ChartComponentUiDomain.AddChild( _lineViewModel = new ChartElementViewModel( "Line", ChartComponentView, new Func<SeriesInfo, Color>( GetLegendColor ), new Func<SeriesInfo, string>( SetLineViewModelName ), strArray ) );
            //ChartViewModel.AddChild( _volumeViewModel = new ChildrenChartViewModel( "Vol", ChartComponent, new Func< SeriesInfo, Color >( GetCandleColor ), ( s => ( s as TimeframeSegmentSeriesInfo )?.Volume.ToString( ) ), strArray ) );

            GuiInitSeries( );
            ValidateDrawStylePropertyChanging( ChartComponentView, "DrawStyle", new ChartCandleDrawStyles[ 10 ]
                                                                                                                        {
                                                                                                                            ChartCandleDrawStyles.CandleStick,
                                                                                                                            ChartCandleDrawStyles.Ohlc,
                                                                                                                            ChartCandleDrawStyles.PnF,
                                                                                                                            ChartCandleDrawStyles.LineOpen,
                                                                                                                            ChartCandleDrawStyles.LineHigh,
                                                                                                                            ChartCandleDrawStyles.LineLow,
                                                                                                                            ChartCandleDrawStyles.LineClose,
                                                                                                                            ChartCandleDrawStyles.BoxVolume,
                                                                                                                            ChartCandleDrawStyles.ClusterProfile,
                                                                                                                            ChartCandleDrawStyles.Area
                                                                                                                        } );
        }




        private void GuiInitSeries( )
        {
            if ( !IsUiThread( ) )
            {
                PerformUiAction( new Action( GuiInitSeries ), true );
            }
            else
            {
                InitTimeframeSegmentDataSeries( );

                if ( GetDataSeriesByDrawStyle( ) == null )
                {
                    RemoveChartSeries( );
                }
                else
                {
                    if ( _candlestickSeries != null )
                    {
                        Type type1 = GetRenderingSeriesByType( );
                        Type type2 = _candlestickSeries.GetType( );

                        if ( _candlestickSeries.DataSeries == GetDataSeriesByDrawStyle( ) && type1 == type2 )
                        {
                            return;
                        }

                        BindingOperations.ClearAllBindings( ( DependencyObject ) _candlestickSeries );
                    }

                    if ( _candlestickSeries != null && _candlestickSeries.DataSeries == GetDataSeriesByDrawStyle( ) )
                    {
                        _candlestickSeries = CreateRenderableSeries( );

                        ClearAll( );

                        SetupAxisMarkerAndBinding( _candlestickSeries, ChartComponentView, "ShowAxisMarker", null );
                    }
                    else
                    {
                        RemoveChartSeries( );
                        NewChartSeries( );
                    }
                }
            }
        }

        private void SetupTimeAndPriceStep( object period, double? priceStep )
        {
            //if( _timeframeSegmentDataSeries != null )
            //{
            //    return;
            //}

            _totalMinutes = 0;
            _priceStep = 0.0;

            if ( !( period is TimeSpan periodTime ) )
            {
                return;
            }

            double myStep = 0.0;

            if ( priceStep.HasValue )
            {
                myStep = priceStep.Value;
            }

            if ( myStep <= 0.0 )
            {
                return;
            }

            int totalMinutes = ( int )periodTime.TotalMinutes;

            if ( periodTime != TimeSpan.FromMinutes( totalMinutes ) )
            {
                return;
            }

            _priceStep = myStep;
            _totalMinutes = totalMinutes;

            GuiInitSeries( );
        }

        private void SetPnfBoxSize( object chartElement )
        {
            if ( _isPnfBoxSizeSet )
            {
                return;
            }

            _isPnfBoxSizeSet = true;
            _pnfBoxSize = 0.2;

            if ( !( chartElement is PnFArg pnFarg ) )
            {
                return;
            }

            _pnfBoxSize = ( double ) ( pnFarg.BoxSize );

            if ( !( _candlestickSeries is FastXORenderableSeries fastXOr ) )
            {
                return;
            }

            fastXOr.XOBoxSize = _pnfBoxSize;
        }

        private void InitTimeframeSegmentDataSeries( )
        {
            //if( _totalMinutes < 1 || _totalMinutes > 10080 || _priceStep <= 0.0 )
            //{
            //    _timeframeSegmentDataSeries = null;
            //}
            //else
            //{
            //    if( _timeframeSegmentDataSeries == null || _timeframeSegmentDataSeries.Timeframe != _totalMinutes || Math.Abs( _timeframeSegmentDataSeries.PriceStep - _priceStep ) >= 1E-10 )
            //    {
            //        _timeframeSegmentDataSeries = new TimeframeSegmentDataSeries( _totalMinutes, _priceStep );
            //    }
            //}
        }

        private Type GetRenderingSeriesByType( )
        {
            switch ( ChartComponentView.DrawStyle )
            {
                case ChartCandleDrawStyles.CandleStick:
                    return typeof( FreemindCandlestickRenderableSeries );

                case ChartCandleDrawStyles.Ohlc:
                    return typeof( FastOhlcRenderableSeries );

                case ChartCandleDrawStyles.LineOpen:
                case ChartCandleDrawStyles.LineHigh:
                case ChartCandleDrawStyles.LineLow:
                case ChartCandleDrawStyles.LineClose:
                    return typeof( FastLineRenderableSeries );

                //case ChartCandleDrawStyles.BoxVolume:
                //    return typeof( BoxVolumeRenderableSeries );

                //case ChartCandleDrawStyles.ClusterProfile:
                //    return typeof( ClusterProfileRenderableSeries );

                case ChartCandleDrawStyles.Area:
                    return typeof( FastMountainRenderableSeries );

                case ChartCandleDrawStyles.PnF:
                    return typeof( FastXORenderableSeries );

                default:
                    throw new ArgumentOutOfRangeException( );
            }
        }

        protected override void RootElementPropertyChanged( IChartComponent elementXY, string property )
        {
            base.RootElementPropertyChanged( elementXY, property );

            if ( !( property == "DrawStyle" ) )
            {
                return;
            }

            GuiInitSeries( );
        }



        protected override void Clear( )
        {
            RemoveChartSeries( );
        }

        protected override void UpdateUi( )
        {
            _ohlcDataSeries.Clear( );
            _xyDataSeries.Clear( );

            // _timeframeSegmentDataSeries = null;
            _totalMinutes                  = 1;
            _priceStep                     = 0.01;
            _dateTimeUtc                   = new DateTime( );
            _isPnfBoxSizeSet               = false;
            _pnfBoxSize                    = 0.2;

            PerformUiAction( new Action( Reset ), true );
        }

        private void NewChartSeries( )
        {
            _candlestickData = GetDataSeriesByDrawStyle( );

            var rSeries = CreateRenderableSeries( );

            _candlestickSeries = rSeries;
            _candlestickSeries.DataSeries = _candlestickData;

            var rd = new ResourceDictionary( );
            rd.Source = new Uri( "pack://application:,,,/StockSharp.Xaml.Charting;component/ChartExViewRes.xaml" );


            CursorModifier.SetTooltipTemplate( rSeries, ( DataTemplate ) rd[ "CursorTooltipTemplate" ] );



            ClearAll( );

            throw new NotImplementedException();

            //SetupAxisMarkerAndBinding( _candlestickSeries, ChartComponentView, "ShowAxisMarker", null );

            //DrawingSurface.AddRenderableSeriesToChartSurface( RootElem, _candlestickSeries );


        }

        private void RemoveChartSeries( )
        {
            if ( _candlestickSeries == null )
            {
                return;
            }

            throw new NotImplementedException();

            //DrawingSurface.Remove( RootElem );

            //_candlestickSeries = null;
        }


        /* -------------------------------------------------------------------------------------------------------------------------------------------
        * 
        *  Step Range ----------> 1 Will investigate GetRangeDependencyProperty and see howto get to show the lastest 400 - 500 bars.
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- 
        */
        public override void PerformPeriodicalAction( )
        {
            base.PerformPeriodicalAction( );
            VisibleRangeDpo xAxisVisibleRange = DrawingSurface.GetVisibleRangeDpo( ChartComponentView.XAxisId );

            if ( xAxisVisibleRange == null )
            {
                return;
            }

            IndexRange categoryDateTimeRange = xAxisVisibleRange.CategoryDateTimeRange;
            int count = _ohlcDataSeries.Count;

            if ( _needToCenterOnBar )
            {
                var xAxis = DrawingSurface.XAxises.FirstOrDefault( );
                ICategoryCoordinateCalculator<DateTime> coordinateCalculator = xAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;
                if ( coordinateCalculator != null )
                {
                    int x = ( coordinateCalculator.TransformDataToIndex( _centerBarTime ) );
                    int minX = Math.Max( x - DrawingSurface.MinimumRange / 2, 0 );
                    int maxX = x + DrawingSurface.MinimumRange / 2;

                    categoryDateTimeRange.SetMinMax( minX, maxX );

                    _needToCenterOnBar = false;

                    return;
                }
            }

            if ( _setMinMax )
            {
                categoryDateTimeRange.SetMinMax( 0.0, DrawingSurface.MinimumRange );
            }

            throw new NotImplementedException();

            //bool autoScroll      = false;

            //bool cantSrcoll      = ( count <= 0 || ( !categoryDateTimeRange.IsDefined || DrawingSurface.IsAutoRange ) );

            //bool chartAutoScroll = DrawingSurface.IsAutoScroll;

            //bool condition3      = _setNewRange && _indexRange == categoryDateTimeRange || !_setNewRange && categoryDateTimeRange.Max >= count;

            //autoScroll = !cantSrcoll && chartAutoScroll;

            //int max = count;

            //if ( autoScroll && categoryDateTimeRange.Max < max )
            //{
            //    int showBarCount                   = categoryDateTimeRange.Max - categoryDateTimeRange.Min + 1;
            //    var indexRange                     = new IndexRange( max - showBarCount + 1, max+ DrawingSurface.RightMarginBars );
            //    xAxisVisibleRange.CategoryDateTimeRange = indexRange;
            //    _indexRange = indexRange;
            //}
            //else
            //{
            //    _indexRange = categoryDateTimeRange;
            //}

            //_setNewRange = autoScroll;
            //_setMinMax = false;
        }

        public override bool Draw( IEnumerableEx<ChartDrawData.IDrawValue> drawValues )
        {
            /* -------------------------------------------------------------------------------------------------------------------------------------------
            * 
            *  Step 7----------> 11 ChartCandleElementViewModel OnDraw Ultimately call the UpdateSeries to append the data to the OHLC series.
            *                           
            * ------------------------------------------------------------------------------------------------------------------------------------------- 
            */
            return UpdateDataSeries( drawValues.Cast<ChartDrawData.sCandle>( ).ToEx( drawValues.Count ) );
        }

        public bool TonyDrawSeries( ChartDrawData.sCandleEx candleRangeData )
        {
            /* -------------------------------------------------------------------------------------------------------------------------------------------
            * 
            *  Step 7----------> 11 ChartCandleElementViewModel OnDraw Ultimately call the UpdateSeries to append the data to the OHLC series.
            *                           
            * ------------------------------------------------------------------------------------------------------------------------------------------- 
            */
            return UpdateDataSeries( candleRangeData );
        }

        public bool UpdateDataSeries( ChartDrawData.sCandleEx barRange )
        {
            if ( _colorerFunction != ChartComponentView.Colorer )
            {
                _colorerFunction = ChartComponentView.Colorer;

                _candlestickSeries.Services?.GetService<ISciChartSurface>()?.InvalidateElement();
            }

            if ( !barRange.IsSet )
            {
                return false;
            }

            if ( barRange.Count == 0 )
            {
                return false;
            }


            var barsRepo = barRange.BarRepo;

            if ( ! _barEventsSubscribed )
            {                
                var aa = ( AdvancedAnalysisManager ) SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( barsRepo.Security );

                if ( aa != null )
                {
                    var freemindIndicator = ( FreemindIndicator ) aa.GetFreemindIndicator( barsRepo.Period.Value );

                    freemindIndicator.IndicatorCalculatedEvent += FreemindIndicator_IndicatorCalculatedEvent;
                }

                _barEventsSubscribed = true;
            }

            var xAxisIdRange     = DrawingSurface.GetVisibleRangeDpo( ChartComponentView.XAxisId );
            bool xAxisIsDateTime = xAxisIdRange != null && xAxisIdRange.GetAxisType( ) == ChartAxisType.CategoryDateTime;
            int count            = barRange.Count;
            var lastBarTime      = _dateTimeUtc;
            
            int index            = -1;

            DateTime[ ] timeArray                  = new DateTime[ count ];
            double[ ] openArray                    = new double[ count ];
            double[ ] highArray                    = new double[ count ];
            double[ ] lowArray                     = new double[ count ];
            double[ ] closeArray                   = new double[ count ];
            IPointMetadata[] advancedTAInfo        = new IPointMetadata[ count ];

            var bars = barsRepo.MainDataBars;

            _updateNullBar = false;

            for ( uint i = barRange.StartIndex; i<= barRange.EndIndex; i++ )
            {
                var barTime        = bars.BarTime(i);
                var open           = bars.Open( i );
                var high           = bars.High( i );
                var low            = bars.Low( i );
                var close          = bars.Close( i );
                var bIndex         = bars[ i ].Index;

                var latestBarIndex = _latestBarIndex.Value;

                if ( i > latestBarIndex )
                {
                    if ( open ==0 || close == 0 || low == 0 || high == 0 )
                    {

                    }

                    _latestBarIndex.CompareAndSet( latestBarIndex, ( int ) i );
                    ++index;
                    timeArray[ index ]      = barTime;
                    openArray[ index ]      = open;
                    highArray[ index ]      = high;
                    lowArray[ index ]       = low;
                    closeArray[ index ]     = close;
                    advancedTAInfo[ index ] = bars[ i ];
                    lastBarTime             = barTime;
                }
                else
                {
                    if ( open == 0 || close == 0 || low == 0 || high == 0 )
                    {

                    }

                    _ohlcDataSeries.Update( (int)i, open, high, low, close );
                    _xyDataSeries.Update( (int)i, close );

                    --count;
                }                               
            }

            _updateNullBar = true;


            if ( count == 0 )
            {
                return false;
            }

            if ( lastBarTime > _dateTimeUtc )
            {
                throw new NotImplementedException();

                //_setMinMax = ( ( _setMinMax ? 1 : 0 ) | ( !( !DrawingSurface.IsAutoRange & xAxisIsDateTime ) ? 0 : ( _ohlcDataSeries.Count <= DrawingSurface.MinimumRange ? 1 : 0 ) ) ) != 0;
            }

            Array.Resize( ref timeArray, index + 1 );
            Array.Resize( ref openArray, index + 1 );
            Array.Resize( ref highArray, index + 1 );
            Array.Resize( ref lowArray, index + 1 );
            Array.Resize( ref closeArray, index + 1 );
            Array.Resize( ref advancedTAInfo, index + 1 );

            _ohlcDataSeries.Append( timeArray, openArray, highArray, lowArray, closeArray, advancedTAInfo );
            _xyDataSeries.Append( timeArray, closeArray );

            if ( _lastNullBar.Index < _latestBarIndex.Value )
            {
                _lastNullBar = barsRepo.MainDataBars[ _latestBarIndex.Value ];
            }

            



            // Tony: After update of the Candles, we need to notify the binding that our datasource has changed and need to rerender.
            PerformUiAction( () =>
                                    {
                                        _ohlcDataSeries.InvalidateParentSurface( RangeMode.None, true );
                                        _xyDataSeries.InvalidateParentSurface( RangeMode.None, true );
                                        
                                    },
                                    true
                            );

            _dateTimeUtc = lastBarTime;

            return true;
        }

        private void FreemindIndicator_IndicatorCalculatedEvent( object sender, HistoricBarsUpdateEventArg e )
        {
            var indicator = ( FreemindIndicator ) sender;
            var bars = indicator.Bars;

            var end  = Math.Min( _ohlcDataSeries.Count, e == null ? 0 : e.EndIndex );
            var start  = e == null ? 0 : e.BeginIndex;

            for ( uint i = start; i < end; i++ )
            {
                ref SBar bar = ref bars[ i ];

                _ohlcDataSeries.Update( bar.Index, bar.Open, bar.High, bar.Low, bar.Close, bar );
            }

            PerformUiAction( () =>
                                    {
                                        _ohlcDataSeries.InvalidateParentSurface( RangeMode.None, true );
                                        _xyDataSeries.InvalidateParentSurface( RangeMode.None, true );
                                    },
                                    true
                            );
        }

        

        public bool UpdateDataSeries( IEnumerableEx<ChartDrawData.sCandle> candles )
        {
            if ( _colorerFunction != ChartComponentView.Colorer )
            {
                _colorerFunction = ChartComponentView.Colorer;

                _candlestickSeries.Services?.GetService<ISciChartSurface>( )?.InvalidateElement( );
            }

            if ( candles == null || candles.IsEmpty( ) )
            {
                return false;
            }

            var xAxisIdRange     = DrawingSurface.GetVisibleRangeDpo( ChartComponentView.XAxisId );
            bool xAxisIsDateTime = xAxisIdRange != null && xAxisIdRange.GetAxisType( ) == ChartAxisType.CategoryDateTime;
            int count            = candles.Count;
            var lastBarTime      = _dateTimeUtc;
            int index            = -1;

            DateTime[ ] timeArray                  = new DateTime[ count ];
            double[ ] openArray                    = new double[ count ];
            double[ ] highArray                    = new double[ count ];
            double[ ] lowArray                     = new double[ count ];
            double[ ] closeArray                   = new double[ count ];
            IPointMetadata[] advancedTAInfo        = new IPointMetadata[ count ];

            foreach ( var bar in candles )
            {
                SetPnfBoxSize( bar.DataType );

                SetupTimeAndPriceStep( bar.DataType, bar.PriceStep );

                switch ( bar.Time.CompareTo( lastBarTime ) )
                {
                    case -1:
                    {
                        continue;
                    }

                    case 0:
                    {
                        _ohlcDataSeries.Update( bar.Time, bar.OpenPrice, bar.HighPrice, bar.LowPrice, bar.ClosePrice );
                        _xyDataSeries.Update( bar.Time, bar.ClosePrice );

                        //if ( bar.PriceLevels( ) != null /* && _timeframeSegmentDataSeries != null */ )
                        //{
                        //    foreach ( ChartDrawData.sCandle.sPriceVolume priceVol in bar.PriceLevels( ) )
                        //    {
                        //        _timeframeSegmentDataSeries.Update( bar.Time, priceVol.Price( ), priceVol.Volume( ) );
                        //    }
                        //}
                        --count;
                    }
                    break;

                    default:
                    {
                        ++index;
                        timeArray[ index ] = bar.Time;
                        openArray[ index ] = bar.OpenPrice;
                        highArray[ index ] = bar.HighPrice;
                        lowArray[ index ] = bar.LowPrice;
                        closeArray[ index ] = bar.ClosePrice;
                        advancedTAInfo[ index ] = bar.AdvancedTAInfo( ) ?? new DefaultSelectableMetadata( ) { IsSelected = false };


                         
                        //if ( bar.PriceLevels( ) != null /* && _timeframeSegmentDataSeries != null */ )
                        //{
                        //    foreach ( ChartDrawData.sCandle.sPriceVolume priceVol in bar.PriceLevels( ) )
                        //    {
                        //        // _timeframeSegmentDataSeries.Append( bar.Time, priceVol.Price( ), priceVol.Volume( ) );
                        //    }
                        //    break;
                        //}
                    }
                    break;
                }
                lastBarTime = bar.Time;
            }

            


            if ( count == 0 )
            {
                return false;
            }

            if ( lastBarTime > _dateTimeUtc )
            {
                throw new NotImplementedException();

                //_setMinMax = ( ( _setMinMax ? 1 : 0 ) | ( !( !DrawingSurface.IsAutoRange & xAxisIsDateTime ) ? 0 : ( _ohlcDataSeries.Count <= DrawingSurface.MinimumRange ? 1 : 0 ) ) ) != 0;
            }

            Array.Resize( ref timeArray, index + 1 );
            Array.Resize( ref openArray, index + 1 );
            Array.Resize( ref highArray, index + 1 );
            Array.Resize( ref lowArray, index + 1 );
            Array.Resize( ref closeArray, index + 1 );
            Array.Resize( ref advancedTAInfo, index + 1 );

            _ohlcDataSeries.Append( timeArray, openArray, highArray, lowArray, closeArray, advancedTAInfo );
            _xyDataSeries.Append( timeArray, closeArray );


            // Tony: After update of the Candles, we need to notify the binding that our datasource has changed and need to rerender.
            PerformUiAction( ( ) =>
                                    {
                                        _ohlcDataSeries.InvalidateParentSurface( RangeMode.None, true );
                                        _xyDataSeries.InvalidateParentSurface( RangeMode.None, true );
                                    },
                                    true
                            );



            _dateTimeUtc = lastBarTime;

            return true;
        }





        internal static bool IsRising( SeriesInfo seriesInfo )
        {
            var series = seriesInfo as OhlcSeriesInfo;

            return series.Return( s => s.CloseValue > s.OpenValue, false );
        }

        private Color GetCandleColor( SeriesInfo series )
        {
            if ( !IsRising( series ) )
            {
                return ChartComponentView.DownFillColor;
            }
            return ChartComponentView.UpFillColor;
        }

        private Color GetLegendColor( SeriesInfo series )
        {
            if ( !IsRising( series ) )
            {
                return Colors.Red;
            }
            return Colors.Green;
        }

        private string SetLineViewModelName( SeriesInfo seriesInfo )
        {
            if ( !( seriesInfo is XySeriesInfo xySeriesInfo ) )
            {
                return null;
            }
            switch ( ChartComponentView.DrawStyle )
            {
                case ChartCandleDrawStyles.LineOpen:
                    _lineViewModel.Name = "O";
                    return xySeriesInfo.FormattedYValue;

                case ChartCandleDrawStyles.LineHigh:
                    _lineViewModel.Name = "H";
                    return xySeriesInfo.FormattedYValue;

                case ChartCandleDrawStyles.LineLow:
                    _lineViewModel.Name = "L";
                    return xySeriesInfo.FormattedYValue;

                case ChartCandleDrawStyles.LineClose:
                    _lineViewModel.Name = "C";
                    return xySeriesInfo.FormattedYValue;

                case ChartCandleDrawStyles.Area:
                    _lineViewModel.Name = "C";
                    return xySeriesInfo.FormattedYValue;

                default:
                    _lineViewModel.Value = null;
                    return null;
            }
        }

        private void Reset( )
        {
            ClearAll( );
            RemoveChartSeries( );
            NewChartSeries( );
        }


        public void OnBeginSeriesDraw( IRenderableSeries rSeries )
        {
            // throw new NotImplementedException( );
        }


        /// <summary>
        /// Overrides the color of the outline on the attached <see cref="IRenderableSeries" />.
        /// Return null to keep the default series color.
        /// Return a value to override the series color.
        /// </summary>
        /// <param name="rSeries">The source <see cref="IRenderableSeries" />.</param>
        /// <param name="index">The index of the data-point. To get X,Y values use rSeries.DataSeries.XValues[index] etc...</param>
        /// <param name="metadata">The PointMetadata associated with this X,Y data-point.</param>
        /// <returns></returns>
        public Color? OverrideStrokeColor( IRenderableSeries rSeries, int index, IPointMetadata metadata )
        {


            if ( metadata != null && metadata.IsSelected )
            {
                return Color.FromRgb( 255, 0, 0 );
            }

            return null;
        }

        public Brush OverrideFillBrush( IRenderableSeries rSeries, int index, IPointMetadata metadata )
        {
            if ( metadata != null && metadata.IsSelected )
            {
                Brush brush = new SolidColorBrush( Colors.Red );

                return brush;
            }

            return null;
        }



        //Color? IPaletteProvider.GetColor( IRenderableSeries visualSeries,
        //                                  double double_2,
        //                                  double double_3 )
        //{
        //    return new Color?( );
        //}

        //Color? IPaletteProvider.OverrideColor( IRenderableSeries visualSeries,
        //                                       double double_2,
        //                                       double double_3,
        //                                       double double_4 )
        //{
        //    return new Color?( );
        //}

        //Color? IPaletteProvider.OverrideColor( IRenderableSeries visualSeries,
        //                                       double double_2,
        //                                       double double_3,
        //                                       double double_4,
        //                                       double double_5,
        //                                       double double_6 )
        //{
        //    int index = ( int )double_2;
        //    Func< DateTimeOffset, bool, bool, Color? > func0 = _colorerFunction;
        //    if( func0 == null )
        //    {
        //        return new Color?( );
        //    }
        //    return func0( _ohlcDataSeries.XValues[ index ].ToDateTimeOffset( TimeZoneInfo.Utc ), double_6 >= double_3, index == _ohlcDataSeries.Count - 1 );
        //}

        private sealed class TimeframeMultiplierConverter : IValueConverter
        {
            private readonly int int_0;

            public TimeframeMultiplierConverter( int int_1 )
            {
                int_0 = int_1;
            }

            object IValueConverter.Convert( object value,
                                            Type targetType,
                                            object parameter,
                                            CultureInfo culture )
            {
                return ( int ) value * int_0;
            }

            object IValueConverter.ConvertBack( object value,
                                                Type targetType,
                                                object parameter,
                                                CultureInfo culture )
            {
                throw new NotSupportedException( );
            }
        }
    }


    
}


