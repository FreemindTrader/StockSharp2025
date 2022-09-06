using fx.Collections;

using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Algorithm
{
    public class GannLevelsCollection
    {
        private GannKey _gannKey = null;
        double _time;
        double _price;
        private PooledList< GannLevelInfo > _gannLevels45 = new PooledList< GannLevelInfo >( );
        private PooledList< GannLevelInfo > _gannLevels60 = new PooledList< GannLevelInfo >( );

        int _numOfDigits;
        double _basePrice;

        public GannLevelsCollection( double price, long barIndex, TrendDirection direction, int howManyCycles )
        {
            _gannKey = new GannKey( price, barIndex, direction, howManyCycles );
            CalcGannDegrees( price, barIndex, direction, howManyCycles );
        }

        public PooledList<GannLevelInfo> GannLevels
        {
            get
            {
                return _gannLevels45;
            }
        }

        void CalcGannDegrees( double currentPrice, long barIndex, TrendDirection direction, int howManyCycles )
        {
            _basePrice = currentPrice;
            _numOfDigits = ( currentPrice == 0 ) ? 1 : 1 + ( int ) Math.Log10( Math.Abs( currentPrice ) );

            ConvertTo3Digits( );

            _gannLevels45.Clear( );
            _gannLevels60.Clear( );

            var priceSqrt = PriceSqrt;

            double baseFactor = 0.25;
            double degree = 0;

            if ( direction == TrendDirection.Uptrend )
            {
                for ( int j = 0; j < howManyCycles; j++ )
                {
                    for ( int i = 1; i <= 8; i++ )
                    {
                        var temp = priceSqrt + baseFactor * i + j * 2;

                        var gRes = Math.Pow( temp, 2 );

                        var real = ConvertBack( gRes );

                        degree += 45;

                        _gannLevels45.Add( new GannLevelInfo( degree, real, 0 ) );
                    }
                }                
            }
            else if ( direction == TrendDirection.DownTrend )
            {
                for ( int j = 0; j < howManyCycles; j++ )
                {
                    for ( int i = 1; i <= 8; i++ )
                    {
                        var temp = priceSqrt - baseFactor * i - j*2;

                        if ( temp > 0 )
                        {
                            var gRes = Math.Pow( temp , 2 );

                            var real = ConvertBack( gRes );

                            degree -= 45;

                            _gannLevels45.Add( new GannLevelInfo( degree, real, 0 ) );
                        }                        
                    }
                }
            }

            baseFactor = 1/6.0;
            degree = 0;

            if ( direction == TrendDirection.Uptrend )
            {
                for ( int j = 0; j < howManyCycles; j++ )
                {
                    for ( int i = 1; i <= 12; i++ )
                    {
                        var temp = priceSqrt + baseFactor * i + j * 2;

                        var gRes = Math.Pow( temp, 2 );

                        var real = ConvertBack( gRes );

                        degree += 30;

                        _gannLevels60.Add( new GannLevelInfo( degree, real, 0 ) );
                    }
                }
            }
            else if ( direction == TrendDirection.DownTrend )
            {
                for ( int j = 0; j < howManyCycles; j++ )
                {
                    for ( int i = 1; i <= 12; i++ )
                    {
                        var temp = priceSqrt - baseFactor * i - j*2;

                        if ( temp > 0 )
                        {
                            var gRes = Math.Pow( temp , 2 );

                            var real = ConvertBack( gRes );

                            degree -= 30;

                            _gannLevels60.Add( new GannLevelInfo( degree, real, 0 ) );
                        }
                    }
                }
            }
        }

        public double ConvertBack( double currentPrice )
        {
            double output = currentPrice;

            var digits = 1 + ( int ) Math.Log10( Math.Abs( currentPrice ) );

            output = currentPrice * Math.Pow( 10, -( digits - 1 ) );

            return output;
        }

        public void ConvertTo3Digits( )
        {
            int digitsToShifted = 3 - _numOfDigits;

            _time = _basePrice * Math.Pow( 10, digitsToShifted );

            digitsToShifted = 5 - _numOfDigits;

            _price = _basePrice * Math.Pow( 10, digitsToShifted );
        }

        public double PriceSqrt
        {
            get
            {
                return Math.Sqrt( _price );
            }
        }
    }
}
