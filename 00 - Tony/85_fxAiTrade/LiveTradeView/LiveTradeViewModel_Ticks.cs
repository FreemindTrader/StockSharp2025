using DevExpress.Xpf.Docking;
using Ecng.Common;
using Ecng.Serialization;
using fx.Collections;
using fx.Common;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Storages;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreemindAITrade.ViewModels
{
    public partial class LiveTradeViewModel
    {
        private DateTimeOffset? LoadTicksAndBuildOneSecondBars( CandleSeries series, TimeSpan daysLoad )
        {
            var taskBegin = series.From.Value.UtcDateTime;
            var taskEnd = series.To.HasValue ? series.To.Value.UtcDateTime : DateTime.UtcNow;

            var lvl1From = _level1Storage.GetFromTime();
            var lvl1To = _level1Storage.GetToTime();

            var OneSecFrom = _candleStorage.GetFromTime();
            var OneSecTo = _candleStorage.GetToTime();

            var buffer = new PooledList<IEnumerable<Candle>>();

            if ( lvl1From.HasValue && lvl1To.HasValue )
            {
                lvl1From = new DateTime?( taskBegin.Max( lvl1From.Value ) );

                var dates1Sec = _candleStorage.Dates;

                var missingDates = new PooledList<DateTime>();

                if ( lvl1From.Value > lvl1To.Value )
                {
                    // We don't have required tick data in the storage
                    lvl1To = DateTime.UtcNow.AddDays( 5 );
                }
                else
                {
                    DatesHelper.GetDatesListForMissingBars( dates1Sec, lvl1From.Value.Date, lvl1To.Value.Date, ref missingDates );

                    foreach ( DateTime date in missingDates )
                    {
                        this.AddInfoLog( LocalizedStrings.Str3786Params.Put( SelectedSecurity.Id, "Level1ToCandles", date ) );

                        try
                        {
                            var data = ( ( IMarketDataStorage<Level1ChangeMessage> )_level1Storage ).Load( date );
                            var dataTicks = data.ToFxcmTicks();

                            var candlesMsg = dataTicks.ToCandles( series );

                            var count = _candleStorage.Save( candlesMsg );

                            if ( count > 0 )
                            {
                                buffer.Add( candlesMsg.ToCandles<Candle>( SelectedSecurity ) );
                            }

                            //RaiseDataLoaded( security, Algo.DataType.Create( type, args ), date, count );
                        }
                        catch ( Exception ex )
                        {
                            this.AddErrorLog( ex );
                        }
                    }
                }

                var new1SecDates = _candleStorage.Dates;

                var range = fxCandleHelper.GetRange( _candleStorage, series.From, series.To, daysLoad );

                if ( range == null )
                {
                    return null;
                }

                DateTimeOffset begin = range.Item1.UtcDateTime.Date;
                DateTimeOffset end = range.Item2.UtcDateTime.Date.EndOfDay();

                var messages = ( ( IMarketDataStorage<CandleMessage> )_candleStorage ).Load( begin, end );

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

            return null;
        }

        private DateTimeOffset? PreloadTicksAndBuild( CandleSeries series, IEnumerable<Candle> allCandles )
        {
            throw new NotImplementedException();

            //if ( _preloadCandlesStatus == WorkFlowStatus.DoneWork)
            //{
            //    return null;
            //}

            //CandleSeriesData candleSeriesData;

            //if ( !_candles.TryGetValue( series, out candleSeriesData ) )
            //{
            //    return null;
            //}

            //_preloadCandlesStatus = WorkFlowStatus.StartWork;

            //var indicators = _indicatorsBySeries.TryGetValue( series );

            //DateTimeOffset lastBarTime = DateTimeOffset.MinValue;

            //int count = 0;            

            //ChartDrawDataEx drawData = new ChartDrawDataEx( );

            //foreach ( Candle candle in allCandles )
            //{
            //    _lastBarTime    = candle.CloseTime;

            //    var currentOpen = candle.OpenTime;
            //    var lastOpen = candleSeriesData.LastBarTime;

            //    if ( currentOpen < lastOpen )
            //    {
            //        continue;
            //    }

            //    candleSeriesData.Second[ currentOpen ] = candle;
            //    candleSeriesData.LastBarTime = currentOpen;                
            //}

            //foreach ( var candleDict in candleSeriesData.Second )
            //{
            //    count++;
            //    lastBarTime = candleDict.Key;
            //    var drawDataItem = drawData.Group( lastBarTime ).Add( candleSeriesData.First, candleDict.Value );

            //    if ( indicators != null )
            //    {
            //        foreach ( Tuple<IndicatorUI, IndicatorPair> indicatorValue in indicators )
            //        {
            //            drawDataItem.Add( indicatorValue.Item1, indicatorValue.Item2.MyIndicator.Process( candleDict.Value ) );
            //        }
            //    }
            //}            

            //_preloadedData.Add( drawData );

            //RequestMarketDataFromLastBar( _lastBarTime.AddTicks( 1 ) );

            //_preloadCandlesStatus = WorkFlowStatus.DoneWork;
            //return lastBarTime;
        }
    }
}
