using Ecng.Common;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using StockSharp.Studio.Core.Commands;
using fx.Charting;
using System;
using System.Collections.Generic;
using fx.Algorithm;
using static fx.Charting.ChartDrawDataEx;
using fx.Collections;
using fx.Bars;
using SciChart.Charting.Model.DataSeries;

namespace FreemindAITrade.ViewModels
{
    public partial class BackTesterViewModel 
    {       
        private PooledList< SBar > _toBeDrawnBars = new PooledList< SBar >( );

        

        public override void Step9_OnCandleStruct( ref CandleStruct cmd, bool endOfBatch )
        {
            Candle candle = cmd.Candle;

            CandleSeriesData candleSeriesData;
            if ( !_candles.TryGetValue( cmd.Series, out candleSeriesData ) )
            {
                return;
            }

            DateTimeOffset openTime = candle.OpenTime;
            if ( openTime < candleSeriesData.LastBarTime )
                return;


            candleSeriesData.LastBarTime = openTime;

            ref SBar singleBar = ref _bars.AddSingleCandle( candle );

            if ( singleBar == SBar.EmptySBar )
            {
                return;
            }

            var drawData     = new ChartDrawDataEx( );
            drawData.SetCandleSource( candleSeriesData.CandleUI, _bars, ref singleBar );

            Tuple<IndicatorUI, IndicatorPair>[] indicatorTuple;

            var ssIndicators = _indicatorsBySeries.TryGetValue( _candlesSeries , out indicatorTuple );

            if ( ssIndicators )
            {
                var indicatorList = drawData.SetIndicatorSource( indicatorTuple[ 0 ].Item1, indicatorTuple.Length );

                foreach ( var indicatorValue in indicatorTuple )
                {
                    var indicatorRes = indicatorValue.Item2.MyIndicator.Process( ref singleBar );

                    indicatorList.SetIndicatorValue( singleBar.BarTime, indicatorRes );
                }
            }

            ChartVM.Draw( drawData );
        }
    }
}
