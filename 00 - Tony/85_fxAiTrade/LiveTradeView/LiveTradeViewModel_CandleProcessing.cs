using fx.Definitions;
using StockSharp.Algo;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;

namespace FreemindAITrade.ViewModels
{
    public partial class LiveTradeViewModel
    {
        DateTime _reloadCandleFromDate = DateTime.MinValue;
        DateTime _reloadCandleToDate = DateTime.MinValue;

        bool _isReloading = false;

        public override void Step9_OnCandleStruct( ref CandleStruct cmd, bool endOfBatch )
        {
            if ( !_candles.TryGetValue( cmd.Series, out CandleSeriesData seriesData ) )
            {
                return;
            }

            _candlesSeries = cmd.Series;

            var myCandle   = cmd.Candle;

            var candleTime = cmd.Candle.OpenTime.UtcDateTime;

            // Meaning we are at the most current databar
            if ( cmd.Candle.State == CandleStates.Active )
            {
                endOfBatch = true;
            }

            switch ( cmd.Candle.BatchStatus )
            {
                case fx.Base.fxBatchStatus.FromStorage:
                {

                }
                break;

                case fx.Base.fxBatchStatus.LiveUpdate:
                {
                    if( _isReloading )
                            return;

                    if ( candleTime >= _lastFinishedBarTime )
                    {
                        if ( cmd.Candle.OpenTime == _lastFinishedBarTime )
                        {
                            // So this is an update to the existing bar
                            _lastFinishedBarTime = candleTime;

                            _candleBuffer.Add( cmd.Candle );                            
                        }
                        else if ( cmd.Candle.OpenTime == _lastFinishedBarTime + ResponsibleTF )
                        {
                            _lastFinishedBarTime = candleTime;

                            _candleBuffer.Add( cmd.Candle );                            
                        }
                        else
                        {
                            // We are missing some databars.
                        }
                    }
                    else
                    {

                    }
                }
                break;

                case fx.Base.fxBatchStatus.InitialUpdate:
                {
                    if ( candleTime >= _lastFinishedBarTime )
                    {
                        if ( cmd.Candle.OpenTime == _lastFinishedBarTime )
                        {
                            // So this is an update to the existing bar
                            var sameBar = _bars.GetBarByTime( candleTime );

                            if ( sameBar.Volume != myCandle.TotalVolume )
                            {
                                _candleBuffer.Add( cmd.Candle );
                            }
                        }
                        else if ( cmd.Candle.OpenTime == _lastFinishedBarTime + ResponsibleTF )
                        {
                            _lastFinishedBarTime = candleTime;

                            _candleBuffer.Add( cmd.Candle );
                        }
                        else
                        {
                            // We are missing some databars.
                        }
                    }
                    else
                    {
                        throw new NotImplementedException( );
                    }

                    
                }
                break;

                case fx.Base.fxBatchStatus.Reloaded:
                {                    
                    _lastFinishedBarTime = candleTime;
                    _candleBuffer.Add( cmd.Candle );                                          
                }
                break;
            }

            // if ( !_bars.DoneIndicatorCalculation ) return;

            if( _candleBuffer.Count == 0 )
                return;

            if ( endOfBatch )
            {
                if ( ResponsibleTF == TimeSpan.FromMinutes( 1 ) )
                {
                    //continue;
                }

                if( _isReloading )
                    _isReloading = false;

                (uint first, uint last) barsRange = default;

                if ( _candleBuffer.Count > 2 && _reloadCandleFromDate != DateTime.MinValue && _reloadCandleToDate != DateTime.MinValue )
                {
                    var first = _candleBuffer[0].OpenTime.UtcDateTime;
                    var last = _candleBuffer[_candleBuffer.Count - 1].OpenTime.UtcDateTime;

                    if ( first >= _reloadCandleFromDate && last <= _reloadCandleToDate )
                    {
                        barsRange = _bars.AddReplaceCandlesRange( SelectedSecurity, _candleBuffer, ResponsibleTF, _waveScenarioNumber, true );
                    }
                    else
                    {
                        barsRange = _bars.AddReplaceCandlesRange( SelectedSecurity, _candleBuffer, ResponsibleTF, _waveScenarioNumber );
                    }
                }
                else
                {
                    barsRange = _bars.AddReplaceCandlesRange( SelectedSecurity, _candleBuffer, ResponsibleTF, _waveScenarioNumber );
                }

                if ( _isNonVisual )
                {
                    return;
                }


                if ( barsRange != default )
                {
                    string sendMsg = string.Format( "[{0}] ({1}) : Candle Command Batch End. Count = [{2}] ...", SelectedSecurity.Code, ResponsibleTF.ToReadable(), ( barsRange.last - barsRange.first ) );

                    LoggingHelper.AddInfoLog( this, sendMsg );

                    _candleBuffer.Clear();


                    if ( barsRange == default )
                        return;

                    InternalDrawCandles( cmd.Series, seriesData.CandleUI, _bars.MainDataBars, barsRange );
                }
            }
        }

        public override void ReloadCandles()
        {
            (TimeSpan, (long from, long to)) range = _bars.GetSelectedBarTimeRange();

            var subscription = new Subscription( _candlesSeries );

            _reloadCandleFromDate = range.Item2.from.FromLinuxTime().Date;
            _reloadCandleToDate = range.Item2.to.FromLinuxTime().Date;

            var mdMsg = ( MarketDataMessage )subscription.SubscriptionMessage;
            mdMsg.From = _reloadCandleFromDate;
            mdMsg.To = _reloadCandleToDate.AddDays( 1 ).AddSeconds( -1 );
            mdMsg.Adapter = null;

            _candleBuffer.Clear( );

            _isReloading = true;

            mdMsg.ExtensionInfo = new Dictionary<string, object>();
            mdMsg.ExtensionInfo.Add( "ReloadCandles", true );

            DeleteCandlesFromStorage( _reloadCandleFromDate, _reloadCandleToDate );

            _reloadCandleToDate = _reloadCandleToDate.AddDays( 1 ).AddSeconds( -1 );

            _connector.Subscribe( subscription );
        }

        public void RequestMarketDataFromLastBar( DateTimeOffset lastBarTime )
        {
            var subscription = new Subscription( _candlesSeries );

            var mdMsg = ( MarketDataMessage )subscription.SubscriptionMessage;
            mdMsg.From = lastBarTime;
            mdMsg.To = DateTime.UtcNow; ;
            mdMsg.Adapter = null;


            /* --------------------------------------------------------------------------------------------------
            *  I need the liveTrading to download all the databar before send out message so that 
            *  Scichart, databars can be inserted all together.
            * 
            *  As for Octopus, I need it to send out databars for every day so that we can save all the everyday bars
            *  One day by one day, just in case we have a long time to redownload
            * 
            * -------------------------------------------------------------------------------------------------- 
            */
            mdMsg.ExtensionInfo = new Dictionary<string, object>();
            mdMsg.ExtensionInfo.Add( "FullDownload", true );

            _connector.Subscribe( subscription );
        }

    }


}
