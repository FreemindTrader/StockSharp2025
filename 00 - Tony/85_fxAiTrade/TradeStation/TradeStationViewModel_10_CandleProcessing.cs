using Disruptor;
using fx.Collections;
using StockSharp.Algo.Candles;
using StockSharp.Studio.Core.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FreemindAITrade.ViewModels
{
    public struct CandleStruct
    {
        public CandleSeries Series { get; set; }

        public Candle Candle { get; set; }

        public CandleStruct( CandleSeries series, Candle candle )
        {
            if ( series == null )
            {
                throw new ArgumentNullException( nameof( series ) );
            }

            Series = series;

            if ( candle == null )
            {
                throw new ArgumentNullException( nameof( candle ) );
            }

            Candle = candle;
        }

        public void CopyFrom( CandleStruct other )
        {
            var tf = other.Candle as TimeFrameCandle;

            if ( tf != null )
            {
                Candle = tf.Clone();
            }

            Series = other.Series;
        }
    }

    public struct SeriesStruct
    {
        public TimeSpan Period;

        public DateTimeOffset? From;

        public DateTimeOffset? To;
    }


    public partial class TradeStationViewModel
    {
        private Disruptor.Dsl.ValueDisruptor<CandleStruct> _candlesDisruptor = null;
        private ValueRingBuffer<CandleStruct> _ringBuffer;

        ThreadSafeDictionary<CandleSeries, ChartTabViewModelBase> _candle2Vm = new ThreadSafeDictionary<CandleSeries, ChartTabViewModelBase>();

        public void LinkSeriesWithVM( CandleSeries candlesSeries, ChartTabViewModelBase backTesterVM )
        {
            //var key = new SeriesStruct( );
            //key.Period = (TimeSpan) candlesSeries.Arg;
            //key.From = candlesSeries.From;
            //key.To = candlesSeries.To;

            _candle2Vm.TryAdd( candlesSeries, backTesterVM );
        }

        private void StartRingBuffer()
        {
            if ( _candlesDisruptor == null )
            {
                _candlesDisruptor = new Disruptor.Dsl.ValueDisruptor<CandleStruct>( () => new CandleStruct(), BUFFER_SIZE, TaskScheduler.Default, Disruptor.Dsl.ProducerType.Single, new BlockingSpinWaitWaitStrategy() );
                _candlesDisruptor.TonyHandleEventsWith( this );
                _candlesDisruptor.Start();

                _ringBuffer = _candlesDisruptor.RingBuffer;
            }
        }

        Queue<CandleCommand> _pasuedCandleHolder = new Queue<CandleCommand>();

        private void RingBufferCandleSeriesProcessing( CandleSeries series, Candle candle )
        {
            using ( var scope = _candlesDisruptor.PublishEvent() )
            {
                ref var data = ref scope.Event();
                data.Series = series;
                data.Candle = candle;
            }
        }

        public void OnEvent( ref CandleStruct data, long sequence, bool endOfBatch )
        {
            if ( _candle2Vm.TryGetValue( data.Series, out ChartTabViewModelBase myClass ) )
            {
                myClass.Step9_OnCandleStruct( ref data, endOfBatch );
            }

            if ( StepByStepChecked )
            {
                Thread.Sleep( 500 );
            }
        }

        public void CancelAllTasks()
        {
            _cancelToken.Cancel();
        }

        //public void OnEvent( CandleCommand cmd, long sequence, bool endOfBatch )
        //{            
        //    var series = cmd.Series;

        //    if ( _candle2Vm.TryGetValue( series, out ChartTabViewModelBase myClass ) )
        //    {
        //        myClass.Step9_OnCandleCommand( cmd, endOfBatch );
        //    }

        //    if ( StepByStepChecked )
        //    {
        //        Thread.Sleep( 500 );
        //    }                        
        //}
    }
}