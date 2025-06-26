using Ecng.Common;
using fx.Definitions;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Indicators;
using System;
using System.Collections.Generic;
using System.Text;

#pragma warning disable CS0414

namespace fx.Bars
{
    public class ZigZag 
    {
        SBarList _bars;

        public ZigZag( SBarList barList )
        {
            _bars = barList;
        }

        int _retraceIndex;
        float _segmentLowest;
        float _segmentHighest;

        float _localHigh;
        float _localLow;

        int _segmentLowestIndex;
        int _segmentHighestIndex;
        float _avgLength;
        TrendDirection _direction;
        int _count;
        bool _hasRetracement;

        int _correctionCount;

        int _retExtremeIndex;
        float _retExtremeValue;


        public int HighestIndex
        {
            get => _segmentHighestIndex;
            set => _segmentHighestIndex = value;
        }

        
        public int RetraceIndex
        {
            get => _retraceIndex;
            set => _retraceIndex = value;
        }
        

        public float Highest
        {
            get => _segmentHighest;
            set => _segmentHighest = value;
        }


        public int LowestIndex
        {
            get => _segmentLowestIndex;
            set => _segmentLowestIndex = value;
        }

        
        public float Lowest
        {
            get => _segmentLowest;
            set => _segmentLowest = value;
        }
        

        public float Length
        {
            get 
            {
                return _segmentHighest - _segmentLowest;
            }
        }

        
        public float AvgLength
        {
            get => _avgLength;
            set => _avgLength = value;
        }
        

        public int Count
        {
            get => _count;
            set => _count = value;
        }

        
        public TrendDirection Direction
        {
            get => _direction;
            set => _direction = value;
        }

        private List< ZigZag > _counters = new List< ZigZag >( );

        

        public void Initialize( int index )
        {
            _segmentHighestIndex = index;
            _segmentLowestIndex  = index;
            _segmentHighest      = _bars.High( index );
            _segmentLowest       = _bars.Low( index );
            _localHigh           = _bars.High( index );
            _localLow            = _bars.Low( index );
        }
        public void SetAvgLength( double avgRange )
        {
            _avgLength = ( float ) avgRange;
        }
        public void BreakDownAction( int i, float low )
        {
            if ( low < _segmentLowest )
            {
                // Check if there is any valid retracement
                if ( _retExtremeIndex != 0 )
                {

                }

                _segmentLowest = low;
                _segmentLowestIndex = i;

                // Here we have broken the low, so there is a change of trend.
                if ( _direction == TrendDirection.Uptrend )
                {
                    _direction = TrendDirection.DownTrend;
                    _retExtremeIndex = 0;
                }
            }
            else
            {
                // Since we haven't broken the lowest yet, we might be in for a correction.
                _retExtremeIndex = i;

                var retracement = _segmentHighest - low;

                if ( ( retracement / Length * 100 ) > 22.3 )
                {
                    
                }
            }
        }

        public void BreakDownAndRetrace( int i, float low, float high )
        {
            if ( low < _segmentLowest )
            {
                _segmentLowest = low;
                _segmentLowestIndex = i;

                // Here we have broken the low, so there is a change of trend.
                if ( _direction == TrendDirection.Uptrend )
                {
                    _direction = TrendDirection.DownTrend;
                    _retExtremeIndex = 0;
                    _retExtremeValue = 0;
                    _hasRetracement = false;
                }
                else
                {
                    _retExtremeIndex = i;
                    _retExtremeValue = high;

                    var retracement = high - _segmentLowest;

                    if ( ( retracement / Length * 100 ) > 22.3 )
                    {
                        
                    }
                }
            }
            else
            {
                // Since we haven't broken the lowest yet, we might be in for a correction.
                _retExtremeIndex = i;
                _retExtremeValue = _direction == TrendDirection.Uptrend ? low : high;


                var retracement = _segmentHighest - low;

                if ( ( retracement / Length * 100 ) > 22.3 )
                {
                    _hasRetracement = true;
                }
            }
        }

        public void OutsideBarFalling( int i )
        {
            ref var bar = ref _bars[ i ];
            
            if ( bar.High > _segmentHighest )
            {
                _segmentHighest = bar.High;

                _segmentHighestIndex = i;

                _correctionCount = 0;

                if ( _direction == TrendDirection.DownTrend )
                {
                    _direction = TrendDirection.Uptrend;
                    _retExtremeIndex = 0;
                    _retExtremeValue = 0;
                    _hasRetracement = false;
                }
                else
                {
                    _direction = TrendDirection.Uptrend;
                }
            }
        }

        public void OutsideBarRising( int i )
        {
            throw new NotImplementedException();
        }

        public void InsideBarAction( int i )
        {
            _correctionCount++;
        }

        public void BreakUpAction( int i, float high )
        {
            if ( high > _segmentHighest )
            {
                _segmentHighest = high;

                _segmentHighestIndex = i;

                _correctionCount = 0;

                if ( _direction == TrendDirection.DownTrend )
                {
                    _direction = TrendDirection.Uptrend;
                    _retExtremeIndex = 0;
                    _retExtremeValue = 0;
                    _hasRetracement = false;
                }
                else
                {
                    _direction = TrendDirection.Uptrend;
                }
            }

            //_retExtremeIndex = i;
            //_retExtremeValue = high;

            //var retracement = high - _segmentLowest;

            //if ( ( retracement / Length * 100 ) > 22.3 )
            //{

            //}
        }

        public void BreakUpAndRetrace( int i, float low, float high )
        {
            if ( high > _segmentHighest )
            {
                _segmentHighest = high;

                _segmentHighestIndex = i;

                _correctionCount = 0;

                if ( _direction == TrendDirection.DownTrend )
                {
                    _direction = TrendDirection.Uptrend;
                    _retExtremeIndex = 0;
                    _retExtremeValue = 0;
                    _hasRetracement = false;
                }
                else
                {
                    _direction = TrendDirection.Uptrend;
                }
            }
        }
    }
}
