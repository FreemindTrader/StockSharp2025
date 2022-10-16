using DevExpress.Xpf.Docking;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using fx.Algorithm;
using fx.Bars;
using fx.Charting;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using fx.Indicators;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Indicators;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using ChartDrawDataEx = fx.Charting.ChartDrawDataEx;

namespace FreemindAITrade.ViewModels
{
    public partial class LiveTradeViewModel
    {
        private readonly SynchronizedDictionary<Security, PooledList<LongDownloadTaskInfo>> _longRunningCandlesTask = new SynchronizedDictionary<Security, PooledList<LongDownloadTaskInfo>>();

        PooledList<DownloadRange> _downloadBackward = new PooledList<DownloadRange>();
        private fxList<ChartDrawDataEx> _preloadedData = new fxList<ChartDrawDataEx>();
        PooledList<DownloadRange> _nonContinousDownload = new PooledList<DownloadRange>();
        private IEnumerable<Candle> _drawCandles;
        private bool _isDrawingCandles = false;
        private WorkFlowStatus _preloadCandlesStatus = WorkFlowStatus.NotStarted;
        private IMarketDataStorage _candleStorage;
        private IMarketDataStorage _level1Storage;

        private DateTimeOffset _lastBarTime = DateTimeOffset.MinValue;

        private DateTime _lastCandleCommandTime = DateTime.MinValue;

        //private bool                    _flushingCandles      = false;

        private bool _doneDrawing = false;

        private List<Candle> _candleBuffer = new List<Candle>( 1000 );


        private fxList<ChartDrawDataEx> _newCandlesList = new fxList<ChartDrawDataEx>();


        private void Step4_SubscribeCandleUiEventHandler( CandlestickUI element, CandleSeries series )
        {
            _stopWatch.Stop();

            string msg = string.Format( "Step3 takes {0} ms", _stopWatch.Elapsed.TotalMilliseconds );

            this.AddWarningLog( msg );

            /* -------------------------------------------------------------------------------------------------------------------------------------------
           *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
           *  
           *  Step A ----------> 5 SubscribeCandleElement event call our handler and Send out an ChartAddElementCommand Command
           * 
           * ------------------------------------------------------------------------------------------------------------------------------------------- 
           */
            var command = new ChartAddElementExCommand( element, series );

            _initializing = false;

            _stopWatch.Start();

            if ( _waveScenarioNumber > 1 )
            {
                ParentViewModel.CheckEnabledViewOptions();
            }

            if ( _loading )
            {
                Step5_OnChartAddUIsToChart( command );
            }
            else
            {
                command.Process( this, false );
            }

            OnUiChanged();
        }

        /* -------------------------------------------------------------------------------------------------------------------------------------------
        *  When we clicked on the Add Candle Command from the context menu, the following function get executed.
        *  
        *  Step A ----------> 6 StudioCommanService Route the command to our OnChartAddElementCommand Handler which will request for Market Data. 
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- 
        */
        private void Step5_OnChartAddUIsToChart( ChartAddElementExCommand command )
        {
            _stopWatch.Stop();

            string msg = string.Format( "Step4_SubscribeCandleUiEventHandler takes {0} ms", _stopWatch.Elapsed.TotalMilliseconds );

            this.AddWarningLog( msg );

            var candleSeries = ( CandleSeries )command.Source;


            /* -------------------------------------------------------------------------------------------------------------------------------------------
            *  ChartAddElementComand is a global event, so all the instances of LiveTradeViewModel will be invoked with ChartAddElementCommand command
            *  
            *  Step A ----------> 6.1.1 We need to make sure that this command is intended for us. I check the Command.Series to see if this ViewModel
            *                           matches the timeframe to take care.
            * ------------------------------------------------------------------------------------------------------------------------------------------- 
            */
            if ( _candlesSeries != candleSeries )
            {
                return;
            }

            _doneDrawing = false;
            _drawSeries = candleSeries;

            var period = ( TimeSpan )_drawSeries.Arg;

            if ( period == TimeSpan.FromMinutes( 5 ) )
            {

            }

            CandlestickUI candleUI;
            IndicatorUI indicatorUI;
            DateTimeOffset? lastTime = null;

            if ( ( candleUI = command.Element as CandlestickUI ) != null )
            {
                if ( !_initializing )
                {
                    _drawTimer.Cancel();
                }

                _chartVM.WaveScenarioNo( _waveScenarioNumber );

                CandleSeries myCandles = ( CandleSeries )command.Source;
                Subscription subscription = new Subscription( myCandles );

                var candleSeriesData = new CandleSeriesData( candleUI, _bars, subscription, DateTimeOffset.MinValue );

                if ( !_candles.TryAdd( myCandles, candleSeriesData ) )
                {
                    return;
                }


                // Instead of requesting for MarketDat, I am going to load Candle here and Draw the Chart directly.
                CacheBuildableCandles = true;

                if ( ResponsibleTF >= TimeSpan.FromMinutes( 1 ) )
                {
                    _candleStorage = GetTimeFrameCandleMessageStorage( _drawSeries.Security, ( TimeSpan )_drawSeries.Arg, _drawSeries.AllowBuildFromSmallerTimeFrame );




                    lastTime = LoadCandles( ( IMarketDataStorage<CandleMessage> )_candleStorage, _drawSeries, TimeSpan.FromDays( 5 ) );
                }
                else
                {
                    var secId = _drawSeries.Security.ToSecurityId();
                    _level1Storage = _storageRegistry.GetLevel1MessageStorage( secId, Drive, Format ); ;
                    _candleStorage = _storageRegistry.GetCandleMessageStorage( typeof( TimeFrameCandleMessage ), secId, period, Drive, Format );

                    lastTime = LoadTicksAndBuildOneSecondBars( _drawSeries, TimeSpan.FromDays( 5 ) );
                }


                if ( _initializing )
                {
                    return;
                }

                if ( lastTime != null )
                {
                    _stopWatch.Restart();

                    _drawTimer.Activate();
                }
                else
                {
                    _doneDrawing = true;

                    RaiseDoneDownloadBarsEvent();

                    RequestMarketDataFromLastBar( _drawSeries.From.Value.UtcDateTime );
                }


                // Tony
                // The reason we are not seeing the candle is because I already request for market data. We should instead load the most recent
                // Candles from Local Cache and start requesting market Data.

                /* -------------------------------------------------------------------------------------------------------------------------------------------         
                 *  Step 7B----------> 0 After all the account related info, we want to get the MarketData                 
                 * ------------------------------------------------------------------------------------------------------------------------------------------- 
                 */
                //new RequestMarketDataCommand( series ).Process( this, false );
            }
            else if ( ( indicatorUI = command.Element as IndicatorUI ) != null )
            {
                if ( _indicators.ContainsKey( indicatorUI ) )
                {
                    return;
                }

                CandleSeries myCandles = ( CandleSeries )command.Source;
                CandleSeriesData candleSeriesData;

                if ( !_candles.TryGetValue( myCandles, out candleSeriesData ) )
                {
                    return;
                }

                if ( candleSeriesData.BarsRepo.MainDataBars == null )
                {
                    return;
                }


                var indicatorPair = new IndicatorPair( this, indicatorUI, command.Indicator, _drawSeries );
                _indicators.Add( indicatorUI, indicatorPair );

                var first = _indicatorsBySeries.SafeAdd( myCandles, key => Array.Empty<Tuple<IndicatorUI, IndicatorPair>>() );
                _indicatorsBySeries[myCandles] = first.Concat( new Tuple<IndicatorUI, IndicatorPair>[1] { Tuple.Create( indicatorUI, indicatorPair ) } ).ToArray();

                var data          = new ChartDrawDataEx();

                var indicator     = indicatorPair.MyIndicator;
                var count         = candleSeriesData.BarsRepo.MainDataBars.Count;
                var indicatorList = data.SetIndicatorSource( indicatorUI, count );

                var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( _drawSeries.Security );

                FreemindIndicator fmIndicator = null;

                if ( aa != null )
                {
                    fmIndicator = ( FreemindIndicator )aa.GetFreemindIndicator( _bars.Period.Value );
                }

                //if ( indicator.Name == "SMA" )
                //{
                    
                //}
                //else if ( indicator.Name == "MACD Histogram" )
                //{

                //}
                //else if ( indicator.Name == "HewRsiComplex" )
                //{

                //}
                //else
                {
                    for ( int i = 0; i < count; i++ )
                    {
                        ref SBar bar = ref candleSeriesData.BarsRepo[i];

                        var indicatorFm = indicator.Process( ref bar );                        

                        indicatorList.SetIndicatorValue( bar.BarTime, indicatorFm );
                    }
                }
                    

                _chartVM.Draw( data );

                _doneDrawing = true;
            }
        }



        /* -------------------------------------------------------------------------------------------------------------------------------------------
        * 
        *  Mistakes 1----------> 1  Finally found out why when loading data, the software is taking up so much time CPU time
        *                           If we don't set AllowBuildFromSmallerTimeFrame to false, we will be using GetCandleMessageBuildableStorage
        * 
        * ------------------------------------------------------------------------------------------------------------------------------------------- 
        */
        private IMarketDataStorage<CandleMessage> GetTimeFrameCandleMessageStorage( Security security, TimeSpan timeFrame, bool allowBuildFromSmallerTimeFrame )
        {
            if ( ResponsibleTF == TimeSpan.FromSeconds( 1 ) )
            {
            }

            if ( !allowBuildFromSmallerTimeFrame )
            {
                return _storageRegistry.GetCandleMessageStorage( typeof( TimeFrameCandleMessage ), security.ToSecurityId(), timeFrame, Drive, Format );
            }


            var candleBuilderProvider = ConfigManager.TryGetService<CandleBuilderProvider>() ?? new CandleBuilderProvider( ServicesRegistry.EnsureGetExchangeInfoProvider() );

            var smallerTFSource = StorageHelper.GetCandleMessageBuildableStorage( candleBuilderProvider, _storageRegistry, security.ToSecurityId(), timeFrame, Drive, Format );

            //if ( CacheBuildableCandles )
            //{
            //    var cacheStorage = _storageRegistry.GetCandleMessageStorage( typeof( TimeFrameCandleMessage ), security.ToSecurityId(), timeFrame, Drive, Format );
            //    smallerTFSource  = new CacheableMarketDataStorage<CandleMessage>( smallerTFSource, cacheStorage );
            //}

            return smallerTFSource;
        }

        private void TonyChartPaneBackgroundWorkTimer( Func<bool> canProcess )
        {
            try
            {
                _stopWatch.Stop();

                if ( IsActive )
                {
                    if ( _drawCandles != null )
                    {
                        if ( _preloadedData.Count > 0 )
                        {
                            _stopWatch.Restart();

                            DrawCandlesFromPreloadedData();

                            FlushBufferedCandles();

                            _stopWatch.Stop();
                            string msg = string.Format( "DrawCandlesFromPreloadedData, takes {0} ms", _stopWatch.Elapsed.TotalMilliseconds );
                            this.AddWarningLog( msg );
                        }
                        else
                        {
                            DrawCandles( _drawSeries, _drawCandles );

                            FlushBufferedCandles();
                        }
                    }
                    else
                    {
                        DrawIndicators( canProcess );
                    }
                }
                else
                {
                    if ( ResponsibleTF == TimeSpan.FromSeconds( 1 ) )
                    {
                        PreloadTicksAndBuild( _drawSeries, _drawCandles );
                    }
                    else
                    {
                        if ( _drawCandles != null )
                        {
                            _stopWatch.Restart();

                            PreloadCandles( _drawSeries, _drawCandles );

                            _stopWatch.Stop();
                            string msg = string.Format( "PreloadCandles, takes {0} ms", _stopWatch.Elapsed.TotalMilliseconds );
                            this.AddWarningLog( msg );
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                ex.LogError( null );
            }
            finally
            {
            }
        }

        private void FlushBufferedCandles()
        {
            if ( !_bars.DoneLoading )
            {
                return;
            }

            TimeSpan timeDiff = DateTime.UtcNow - _lastCandleCommandTime;

            if ( timeDiff.TotalSeconds > 30 && _candleBuffer.Count > 0 )
            {
                var toBeDrawCandle = _candleBuffer;

                //_flushingCandles = true;

                _candleBuffer = new List<Candle>();

                var buffer = new List<Candle>();

                foreach ( var candle in toBeDrawCandle )
                {
                    buffer.Add( candle );
                }

                var bars = _bars.AddReplaceCandlesRange( SelectedSecurity, buffer, ResponsibleTF, _waveScenarioNumber );


                CandleSeriesData candleSeriesData;

                if ( !_candles.TryGetValue( _candlesSeries, out candleSeriesData ) )
                {
                    return;
                }

                toBeDrawCandle.Clear();

                InternalDrawCandles( _candlesSeries, candleSeriesData.CandleUI, _bars.MainDataBars, bars );

                //_flushingCandles = false;
            }

        }



        void InternalDrawCandles( CandleSeries series, CandlestickUI candleUI, SBarList barList, (uint first, uint last) barsRange )
        {
            ChartDrawDataEx drawData = new ChartDrawDataEx();

            Tuple<IndicatorUI, IndicatorPair>[ ] indicatorTuple;

            var ssIndicators = _indicatorsBySeries.TryGetValue( series, out indicatorTuple );

            if ( drawData.SetCandleSource( candleUI, _bars, barsRange ) )
            {
                if ( ssIndicators )
                {
                    //var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( series.Security );

                    //FreemindIndicator fmIndicator = null;

                    //if ( aa != null )
                    //{
                    //    fmIndicator = ( FreemindIndicator )aa.GetFreemindIndicator( _bars.Period.Value );                        
                    //}

                    foreach ( var indicator in indicatorTuple )
                    {
                        var indicatorUI = indicator.Item1;
                        var length = ( int )( barsRange.last - barsRange.first + 1 );
                        var indicatorList = drawData.SetIndicatorSource( indicatorUI, length );

                        for ( var i = barsRange.first; i <= barsRange.last; i++ )
                        {
                            var myBar = _bars[ i ];

                            var indicatorFm = indicator.Item2.MyIndicator.Process( ref myBar );                            

                            indicatorList.SetIndicatorValue( myBar.BarTime, indicatorFm );
                        }
                    }
                }
            }



            if ( !_doneDrawing )
            {
                /* -------------------------------------------------------------------------------------------------------------------------------------------
                * 
                *  Since we haven't rendered the preloaded data to Chart, we can't draw this new candles. If we did, the preloaded candles will be ignored.
                *                           
                * ------------------------------------------------------------------------------------------------------------------------------------------- 
                */
                _newCandlesList.Add( drawData );
            }
            else
            {
                _chartVM.Draw( drawData );
            }
        }


        private DateTimeOffset? DrawCandles( CandleSeries series, IEnumerable<Candle> allCandles )
        {
            if ( _doneDrawing )
            {
                return null;
            }

            CandleSeriesData candleSeriesData;

            if ( !_candles.TryGetValue( series, out candleSeriesData ) )
            {
                return null;
            }

            if ( !_isDrawingCandles )
            {
                _isDrawingCandles = true;
            }

            var drawData = new ChartDrawDataEx();

            int offerId = SymbolsMgr.Instance.GetOfferId( SelectedSecurity );

            List<Candle> holder = new List<Candle>();

            if ( _waveScenarioNumber == 1 )
            {
                foreach ( Candle candle in _drawCandles )
                {
                    holder.Add( candle );

                    DateTimeOffset openTime = candle.OpenTime;

                    if ( openTime < candleSeriesData.LastBarTime )
                    {
                        continue;
                    }

                    candleSeriesData.LastBarTime = openTime;
                }


                if ( _isBarIntegrityCheck )
                {
                    if ( _security.Type == SecurityTypes.Currency )
                    {
                        CheckAndDeleteNonContinuousCandles( series, holder, candleSeriesData );
                    }
                    else
                    {
                        CheckNotContinousInstrumentsCandles( series, holder, candleSeriesData );
                    }

                    _bars.ReloadAllCandles( SelectedSecurity, holder, ResponsibleTF, _waveScenarioNumber );
                }
                else
                {
                    _lastBarTime = holder[holder.Count - 1].OpenTime;

                    _bars.ReloadAllCandles( SelectedSecurity, holder, ResponsibleTF, _waveScenarioNumber );
                }

                RaiseCandleLoadedEvent();
            }
            else
            {
                _lastBarTime = _bars.LastBarTime.Value;
            }


            var bars = _bars.MainDataBars;

            if ( bars.Count == 0 ) return _lastBarTime;

            var candleUI = candleSeriesData.CandleUI;


            Tuple<IndicatorUI, IndicatorPair>[ ] indicatorTuple;

            var ssIndicators = _indicatorsBySeries.TryGetValue( series, out indicatorTuple );

            _stopWatch.Restart();

            if ( drawData.SetCandleSource( candleUI, _bars, 0, ( uint )bars.Count - 1 ) )
            {
                if ( ssIndicators )
                {
                    //var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( series.Security );

                    //FreemindIndicator fmIndicator = null;

                    //if ( aa != null )
                    //{
                    //    fmIndicator = ( FreemindIndicator )aa.GetFreemindIndicator( _bars.Period.Value );
                    //}

                    foreach ( var indicator in indicatorTuple )
                    {
                        var indicatorUI = indicator.Item1;
                        var indicatorList = drawData.SetIndicatorSource( indicatorUI, bars.Count );

                        for ( int i = 0; i < bars.Count; i++ )
                        {
                            var indicatorFm = indicator.Item2.MyIndicator.Process( ref bars[i] );
                            
                            indicatorList.SetIndicatorValue( bars.BarTime( i ), indicatorFm );
                        }
                    }
                }
            }

            _stopWatch.Stop();
            string msg = string.Format( "DrawCandles - Creating sCandle, takes {0} ms", _stopWatch.Elapsed.TotalMilliseconds );
            this.AddWarningLog( msg );


            _stopWatch.Restart();
            _chartVM.Draw( drawData );

            _doneDrawing = true;

            _stopWatch.Stop();
            msg = string.Format( "DrawCandles - Draw To Scichart, takes {0} ms", _stopWatch.Elapsed.TotalMilliseconds );
            this.AddWarningLog( msg );

            RaiseDoneDownloadBarsEvent();

            RequestMarketDataFromLastBar( _lastBarTime );

            return _lastBarTime;
        }

        private void CheckNotContinousInstrumentsCandles( CandleSeries series, List<Candle> allCandles, CandleSeriesData sData )
        {
            decimal lastClose = 0;
            Candle lastCandle = null;
            var earliestDate = DateTimeOffset.MaxValue;
            var today = DateTime.UtcNow;
            var period = ( TimeSpan )series.Arg;

            var minCandleType = DataType.Create( typeof( TimeFrameCandleMessage ), series.Arg );
            var candleStore = Drive.GetStorageDrive( series.Security.ToSecurityId(), minCandleType, StorageFormats.Binary );

            DateTime deleteFromHere = DateTime.MinValue;

            if ( period < TimeSpan.FromDays( 1 ) )
            {
                deleteFromHere = new DateTime( today.Year, 1, 1 );
            }
            else
            {
                deleteFromHere = today - TimeSpan.FromTicks( period.Ticks * 300 );
            }

            var priceStep = series.Security.PriceStep;
            var secID = series.Security.ToSecurityId();

            bool needDelete = false;
            DateTime prevCandleTime = DateTime.MinValue;

            foreach ( Candle candle in allCandles )
            {
                if ( lastClose != 0 )
                {
                    if ( candle.OpenPrice != lastClose && period >= TimeSpan.FromMinutes( 1 ) )
                    {
                        var candleTime = candle.OpenTime.UtcDateTime;

                        if ( candleTime < earliestDate )
                        {
                            earliestDate = candleTime;
                        }

                        var diff = Math.Abs( candle.OpenPrice - lastClose );

                        if ( diff > priceStep * 3 )
                        {
                            prevCandleTime = lastCandle.OpenTime.UtcDateTime;

                            if ( candleTime == prevCandleTime + period )
                            {
                                // Gapping is normal in Stocks
                            }
                            else
                            {
                                var barDiffs = ( candleTime - prevCandleTime ).TotalMinutes / period.TotalMinutes;

                                if ( barDiffs > 5 )
                                {
                                    var bDays = GetBusinessDays( prevCandleTime, candleTime );

                                    if ( bDays > 1 )
                                    {
                                        needDelete = true;
                                        this.AddWarningLog( "Detect {0} Databars starting from {1}", period, prevCandleTime );
                                        DeleteCandlesFromStorage( prevCandleTime );

                                        break;
                                    }
                                }

                                // Most of the non continous candle problem is the previous day candle not probably saved because I exited the software.
                                var creationTime = Drive.GetFileCreationTime( secID, minCandleType, prevCandleTime );

                                if ( creationTime != DateTime.MinValue )
                                {
                                    // Since we need accurate Daily Bar, Weekly Bar and Monthly Bar to calculate Pivot Points, Cannot allow error bars in 
                                    // This big time frame data.
                                    if ( period >= TimeSpan.FromDays( 1 ) )
                                    {
                                        TimeSpan howLongAgo = today - prevCandleTime;

                                        // if our file creation time is very recent and the discrepancy in data happend more than one year ago. This shouldn't affect our pivot point 
                                        // calculation.
                                        var numberOfDaysToCheck = ( today.DayOfWeek == DayOfWeek.Sunday || today.DayOfWeek == DayOfWeek.Monday ) ? TimeSpan.FromDays( 3 ) : TimeSpan.FromDays( 1 );

                                        if ( ( today - creationTime ) < numberOfDaysToCheck && ( today - candleTime > numberOfDaysToCheck ) && howLongAgo.Days > 365 )
                                        {
                                            earliestDate = candleTime;
                                        }
                                        else
                                        {
                                            if ( prevCandleTime > deleteFromHere )
                                            {
                                                this.AddWarningLog( "Detect {0} Databars starting from {1}", period, prevCandleTime );
                                                DeleteCandlesFromStorage( prevCandleTime );

                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if ( ( today - creationTime ) < TimeSpan.FromDays( 1 ) && ( today - candleTime > TimeSpan.FromDays( 1 ) ) )
                                        {
                                            earliestDate = candleTime;
                                        }
                                        else
                                        {
                                            if ( prevCandleTime > deleteFromHere )
                                            {
                                                this.AddWarningLog( "Detect {0} Databars starting from {1}", period, prevCandleTime );
                                                DeleteCandlesFromStorage( prevCandleTime );

                                                break;
                                            }
                                        }
                                    }
                                }
                            }


                        }
                    }
                }

                _lastBarTime = candle.OpenTime;
                lastClose = candle.ClosePrice;
                sData.LastBarTime = candle.OpenTime;
                lastCandle = candle;
            }

            if ( needDelete )
            {
                allCandles.RemoveAll( x => x.OpenTime.UtcDateTime > prevCandleTime );
            }
        }

        public static double GetBusinessDays( DateTime startD, DateTime endD )
        {
            double calcBusinessDays = 1 + ( ( endD - startD ).TotalDays * 5 - ( startD.DayOfWeek - endD.DayOfWeek ) * 2 ) / 7;

            if ( endD.DayOfWeek == DayOfWeek.Saturday ) calcBusinessDays--;
            if ( startD.DayOfWeek == DayOfWeek.Sunday ) calcBusinessDays--;

            return calcBusinessDays;
        }

        private void CheckAndDeleteNonContinuousCandles( CandleSeries series, List<Candle> allCandles, CandleSeriesData sData )
        {
            decimal lastClose = 0;
            Candle lastCandle = null;
            var earliestDate = DateTimeOffset.MaxValue;
            var today = DateTime.UtcNow;
            var period = ( TimeSpan )series.Arg;

            var minCandleType = DataType.Create( typeof( TimeFrameCandleMessage ), series.Arg );
            var candleStore = Drive.GetStorageDrive( series.Security.ToSecurityId(), minCandleType, StorageFormats.Binary );

            var priceStep = series.Security.PriceStep;
            var secID = series.Security.ToSecurityId();

            bool needDelete = false;

            DateTime prevCandleTime = DateTime.MinValue;

            foreach ( Candle candle in allCandles )
            {
                if ( lastClose != 0 )
                {
                    if ( candle.OpenPrice != lastClose && period >= TimeSpan.FromMinutes( 1 ) )
                    {
                        var candleTime = candle.OpenTime.UtcDateTime;

                        if ( candleTime < earliestDate )
                        {
                            earliestDate = candleTime;
                        }

                        var diff = Math.Abs( candle.OpenPrice - lastClose );

                        if ( diff > priceStep * 3 )
                        {
                            prevCandleTime = lastCandle.OpenTime.UtcDateTime;

                            var barDiffs = ( candleTime - prevCandleTime ).TotalMinutes / period.TotalMinutes;

                            if ( barDiffs > 5 )
                            {
                                var bDays = GetBusinessDays( prevCandleTime, candleTime );

                                if ( bDays > 1 )
                                {
                                    needDelete = true;
                                    this.AddWarningLog( "Detect {0} Databars starting from {1}", period, prevCandleTime );
                                    DeleteCandlesFromStorage( prevCandleTime );

                                    break;
                                }
                            }



                            // Most of the non continous candle problem is the previous day candle not probably saved because I exited the software.
                            var creationTime = Drive.GetFileCreationTime( secID, minCandleType, prevCandleTime );

                            if ( creationTime != DateTime.MinValue )
                            {
                                // Since we need accurate Daily Bar, Weekly Bar and Monthly Bar to calculate Pivot Points, Cannot allow error bars in 
                                // This big time frame data.
                                if ( period >= TimeSpan.FromDays( 1 ) )
                                {
                                    TimeSpan howLongAgo = today - prevCandleTime;

                                    // if our file creation time is very recent and the discrepancy in data happend more than one year ago. This shouldn't affect our pivot point 
                                    // calculation.
                                    var numberOfDaysToCheck = ( today.DayOfWeek == DayOfWeek.Sunday || today.DayOfWeek == DayOfWeek.Monday ) ? TimeSpan.FromDays( 3 ) : TimeSpan.FromDays( 1 );

                                    if ( ( today - creationTime ) < numberOfDaysToCheck && ( today - candleTime > numberOfDaysToCheck ) && howLongAgo.Days > 365 )
                                    {
                                        earliestDate = candleTime;
                                    }
                                    else
                                    {
                                        needDelete = true;
                                        this.AddWarningLog( "Detect {0} Databars starting from {1}", period, prevCandleTime );
                                        DeleteCandlesFromStorage( prevCandleTime );

                                        break;

                                    }
                                }
                                else
                                {
                                    if ( ( today - creationTime ) < TimeSpan.FromDays( 1 ) && ( today - candleTime > TimeSpan.FromDays( 1 ) ) )
                                    {
                                        earliestDate = candleTime;
                                    }
                                    else
                                    {
                                        needDelete = true;
                                        this.AddWarningLog( "Detect {0} Databars starting from {1}", period, prevCandleTime );
                                        DeleteCandlesFromStorage( prevCandleTime );

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                _lastBarTime = candle.OpenTime;
                lastClose = candle.ClosePrice;
                sData.LastBarTime = candle.OpenTime;
                lastCandle = candle;
            }

            if ( needDelete )
            {
                allCandles.RemoveAll( x => x.OpenTime.UtcDateTime > prevCandleTime );
            }
        }




        public void DeleteCandlesFromStorage( DateTime startDate )
        {
            var delTillDate = DateTime.UtcNow;

            _lastBarTime = startDate.Date.AddDays( -1 );

            do
            {
                startDate = startDate.AddDays( 1 );

                if ( startDate.Date == delTillDate.Date )
                {

                }

                _candleStorage.Delete( startDate );
            }
            while ( startDate <= delTillDate );
        }

        public void DeleteCandlesFromStorage( DateTime startDate, DateTime endDate )
        {
            _lastBarTime = startDate.Date.AddDays( -1 );

            do
            {
                startDate = startDate.AddDays( 1 );

                if ( startDate.Date > endDate.Date )
                {
                    break;
                }

                _candleStorage.Delete( startDate );
            }
            while ( startDate < endDate );
        }

        private void ProcessLongRunningDownload()
        {
            if ( _longRunningCandlesTask.Count == 0 )
            {
                return;
            }

            foreach ( KeyValuePair<Security, PooledList<LongDownloadTaskInfo>> pair in _longRunningCandlesTask )
            {
                var security = pair.Key;
                var taskList = pair.Value;

                var priorityTasks = taskList.OrderBy( x => x.Priority );

                foreach ( var runInfo in priorityTasks )
                {
                    if ( runInfo.IsTick )
                    {
                        long nextId = _connector.TransactionIdGenerator.GetNextId();
                        //_histTickRequests.Add( nextId, Tuple.Create( security, fromDay ) );

                        //Connector connector = Connector;
                        var msg           = new MarketDataMessage();
                        msg.SecurityId    = security.ToSecurityId();
                        msg.DataType      = MarketDataTypes.Level1;
                        msg.From          = new DateTimeOffset?( runInfo.From.Value );
                        msg.To            = new DateTimeOffset?( runInfo.To.Value );
                        msg.IsSubscribe   = true;
                        msg.TransactionId = nextId;

                        MarketDataMessage marketDataMessage = msg.ValidateBounds();
                        _connector.SendInMessage( marketDataMessage );
                    }
                    else
                    {
                        var count = new long?();
                        var transactionId = new long?();

                        var extendedInfo = new PooledDictionary<string, object>();

                        extendedInfo.Add( "DownloadBackward", true );

                        if ( this is ILogReceiver logs )
                            logs.AddInfoLog( "SubscribeCandles" );

                        var subscription = new Subscription( runInfo.CandleSeries );

                        var mdMsg = ( MarketDataMessage )subscription.SubscriptionMessage;

                        mdMsg.ExtensionInfo = extendedInfo;

                        if ( runInfo.From != null )
                            mdMsg.From = runInfo.From.Value;

                        if ( runInfo.To != null )
                            mdMsg.To = runInfo.To.Value;

                        if ( count != null )
                            mdMsg.Count = count.Value;

                        mdMsg.Adapter = _connector.Adapter;

                        if ( transactionId != null )
                            subscription.TransactionId = transactionId.Value;

                        _connector.Subscribe( subscription );
                    }

                }
            }
        }



        private void DrawCandlesFromPreloadedData()
        {
            if ( _doneDrawing )
            {
                return;
            }

            foreach ( ChartDrawDataEx drawData in _preloadedData )
            {
                _chartVM.Draw( drawData );
            }

            if ( _newCandlesList.Count > 0 )
            {
                foreach ( var candleDraw in _newCandlesList )
                {
                    _chartVM.Draw( candleDraw );
                }

                _newCandlesList.Clear();
            }

            _doneDrawing = true;

            RaiseDoneDownloadBarsEvent();

            _isDrawingCandles = false;
        }

        private DateTimeOffset? PreloadCandles( CandleSeries series, IEnumerable<Candle> allCandles )
        {
            if ( _preloadCandlesStatus == WorkFlowStatus.DoneWork )
            {
                return null;
            }

            CandleSeriesData candleSeriesData;
            if ( !_candles.TryGetValue( series, out candleSeriesData ) )
            {
                return null;
            }

            _preloadCandlesStatus = WorkFlowStatus.StartWork;

            List<Candle> holder = new List<Candle>( _drawCandles.Count() );

            foreach ( Candle candle in _drawCandles )
            {
                holder.Add( candle );

                DateTimeOffset openTime = candle.OpenTime;

                if ( openTime < candleSeriesData.LastBarTime )
                {
                    continue;
                }

                candleSeriesData.LastBarTime = openTime;
            }

            if ( _isBarIntegrityCheck )
            {
                if ( _security.Type == SecurityTypes.Currency )
                {
                    CheckAndDeleteNonContinuousCandles( series, holder, candleSeriesData );
                }
                else
                {
                    CheckNotContinousInstrumentsCandles( series, holder, candleSeriesData );
                }

                _bars.ReloadAllCandles( SelectedSecurity, holder, ResponsibleTF, _waveScenarioNumber );
            }
            else
            {
                _lastBarTime = holder[holder.Count - 1].OpenTime;

                _bars.ReloadAllCandles( SelectedSecurity, holder, ResponsibleTF, _waveScenarioNumber );
            }

            RaiseCandleLoadedEvent();

            ChartDrawDataEx drawData = new ChartDrawDataEx();

            var bars = _bars.MainDataBars;

            if ( bars.Count == 0 ) return _lastBarTime;

            var candleUI = candleSeriesData.CandleUI;


            Tuple<IndicatorUI, IndicatorPair>[ ] indicatorTuple;

            var ssIndicators = _indicatorsBySeries.TryGetValue( series, out indicatorTuple );

            _stopWatch.Restart();

            if ( drawData.SetCandleSource( candleUI, _bars, 0, ( uint )bars.Count - 1 ) )
            {
                if ( ssIndicators )
                {
                    //var aa = ( AdvancedAnalysisManager )SymbolsMgr.Instance.GetOrCreateAdvancedAnalysis( series.Security );

                    //FreemindIndicator fmIndicator = null;

                    //if ( aa != null )
                    //{
                    //    fmIndicator = ( FreemindIndicator )aa.GetFreemindIndicator( _bars.Period.Value );
                    //}

                    foreach ( var indicator in indicatorTuple )
                    {
                        var indicatorUI = indicator.Item1;
                        var length = bars.Count;
                        var indicatorList = drawData.SetIndicatorSource( indicatorUI, length );

                        for ( int i = 0; i < length; i++ )
                        {
                            var indicatorFm = indicator.Item2.MyIndicator.Process( ref _bars[i] );
                            
                            indicatorList.SetIndicatorValue( _bars[i].BarTime, indicatorFm );
                        }
                    }
                }
            }

            _preloadedData.Add( drawData );

            RequestMarketDataFromLastBar( _lastBarTime );

            _preloadCandlesStatus = WorkFlowStatus.DoneWork;
            return _lastBarTime;
        }



        #region Get and Draw Candles


        private DateTimeOffset? LoadCandles( IMarketDataStorage<CandleMessage> storage, CandleSeries series, TimeSpan daysLoad )
        {
            var taskBegin = series.From.Value.DateTime;
            var taskEnd = series.To.HasValue ? series.To.Value.DateTime : DateTime.UtcNow;
            var period = ( TimeSpan )series.Arg;

            var range = fxCandleHelper.GetRange( storage, series.From, series.To, daysLoad );

            if ( range == null )
            {
                return null;
            }

            //if ( ResponsibleTF >= TimeSpan.FromMinutes( 60 ) )
            //{
            //    // Tony:    The reason that I am deleting the last bar is because, most of the time the last bar is in a complete bar, so we only get half bar.
            //    // And we want to redownload that bar again.            
            //    // We don't want to delete one minute 5 minutes chart, as it has too many databars to download
            //    var lastBar = storage.GetToDate( );

            //    storage.Delete( lastBar );
            //}

            /* -------------------------------------------------------------------------------------------------------------------------------------------
            * 
            *  Mistakes 2           ----------> Need to be super careful about manipulating DateTimeOffset and DateTime. The following
            *                       Correct            DateTimeOffset begin = range.Item1.UtcDateTime.Date;       
            *                       Wrong              var  begin = range.Item1.Date ( this produce a DateTime which is Unspecified Time )
            * ------------------------------------------------------------------------------------------------------------------------------------------- 
            */
            DateTimeOffset begin = range.Item1.UtcDateTime.Date;
            DateTimeOffset end = range.Item2.UtcDateTime.Date.EndOfDay();



            var messages = storage.Load( begin, end );

            _drawCandles = messages.ToCandles<Candle>( series.Security );

            if ( _drawCandles != null )
            {
                var last = _drawCandles.FirstOrDefault();

                if ( last != null )
                {
                    return last.OpenTime;
                }
            }

            return null;
        }



        #endregion



    }
}
