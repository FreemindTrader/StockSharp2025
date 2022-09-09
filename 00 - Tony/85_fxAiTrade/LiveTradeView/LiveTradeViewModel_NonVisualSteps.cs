using fx.Common;
using fx.Definitions;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Storages;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    public partial class LiveTradeViewModel
    {
        public override void Step3_LoadCandlesFromLocalStorage_NonVisual()
        {

            _stopWatch.Restart();

            _initializing = true;

            var startDate = DateTime.MinValue;
            var endDate = DateTime.MinValue;

            _candlesSeries = new CandleSeries( typeof( TimeFrameCandle ), SelectedSecurity, ResponsibleTF );

            if ( _waveScenarioNumber == 1 )
            {
                ParentViewModel.LinkSeriesWithVM( _candlesSeries, this );
            }

            if ( ResponsibleTF >= TimeSpan.FromDays( 1 ) /*|| ResponsibleForWhatTimeFrame == TimeSpan.FromHours( 1 )*/ ) // I would only add one hour bars from the beginning during wave Analysis sesssion.
            {
                _candleStorage = _storageRegistry.GetCandleMessageStorage( typeof( TimeFrameCandleMessage ), SelectedSecurity.ToSecurityId(), ResponsibleTF, Drive, Format );
                var start = _candleStorage.GetFromDate();

                if ( start.HasValue )
                {
                    startDate = start.Value;
                }
                else
                {
                    ForexHelper.GetStartAndEndDateForDatabar( ResponsibleTF, out startDate, out endDate );
                }

                endDate = DateTime.UtcNow.AddMinutes( 5 );
            }
            else
            {
                ForexHelper.GetStartAndEndDateForDatabar( ResponsibleTF, out startDate, out endDate );
            }



            _candlesSeries.From = startDate;
            _candlesSeries.To = endDate;

            /* -------------------------------------------------------------------------------------------------------------------------------------------
             * 
             *  Mistakes 1----------> Finally found out why when loading data, the software is taking up so much time CPU time
             *                        If we don't set AllowBuildFromSmallerTimeFrame to false, we will be using GetCandleMessageBuildableStorage
             * 
             * ------------------------------------------------------------------------------------------------------------------------------------------- 
             */
            var shouldBuild = ShouldBuilldFromLowerTimerFrame( ResponsibleTF );

            _candlesSeries.AllowBuildFromSmallerTimeFrame = shouldBuild;

            if ( shouldBuild )
            {
                if ( ResponsibleTF == TimeSpan.FromSeconds( 1 ) )
                {
                    _candlesSeries.BuildCandlesFrom2 = StockSharp.Messages.DataType.Level1;
                    _candlesSeries.BuildCandlesField = Level1Fields.SpreadMiddle;
                }
                else if ( ResponsibleTF == TimeSpan.FromMinutes( 4 ) )
                {
                    _candlesSeries.BuildCandlesFrom2 = StockSharp.Messages.DataType.CandleTimeFrame;
                }
            }

            Step5_OnChartAddUIsToChart_NonVisual();
        }

        private void Step5_OnChartAddUIsToChart_NonVisual()
        {
            _doneDrawing = false;
            _drawSeries = _candlesSeries;

            var period = ( TimeSpan )_drawSeries.Arg;

            var subscription = new Subscription( _candlesSeries );

            var candleSeriesData = new CandleSeriesData( null, _bars, subscription, DateTimeOffset.MinValue );

            if ( !_candles.TryAdd( _candlesSeries, candleSeriesData ) )
            {
                return;
            }

            // Instead of requesting for MarketDat, I am going to load Candle here and Draw the Chart directly.
            CacheBuildableCandles = true;

            DateTimeOffset? lastTime = null;

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


            if ( lastTime != null )
            {
                PreloadCandles_NonVisual( _drawSeries, _drawCandles );
            }
            else
            {
                _doneDrawing = true;
                RequestMarketDataFromLastBar( _drawSeries.From.Value.UtcDateTime );
            }
        }

        private DateTimeOffset? PreloadCandles_NonVisual( CandleSeries series, IEnumerable<Candle> allCandles )
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

                _bars.ReloadAllCandlesNoWaves( SelectedSecurity, holder, ResponsibleTF, _waveScenarioNumber );
            }
            else
            {
                _lastBarTime = holder[holder.Count - 1].OpenTime;

                _bars.ReloadAllCandlesNoWaves( SelectedSecurity, holder, ResponsibleTF, _waveScenarioNumber );
            }

            RaiseCandleLoadedEvent();


            RequestMarketDataFromLastBar( _lastBarTime );

            _preloadCandlesStatus = WorkFlowStatus.DoneWork;
            return _lastBarTime;
        }
    }
}
