using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class WholeCandleRangeD : RangeEx<double>
    {
        internal WholeCandleRangeD( double lowerBound, double upperBound ) : base( lowerBound, upperBound )
        {

        }

        public static WholeCandleRangeD Create( double from, double to )
        {
            return new WholeCandleRangeD( from, to );
        }

        public bool ExactTouch( double pivotValue )
        {
            if ( Math.Abs( UpperBound - pivotValue ) < 0.00005 )
            {
                return true;
            }

            return false;
        }

        public bool AlmostTouch( double pivotValue )
        {
            var moment = pivotValue - UpperBound;

            if ( ( moment <= 0.00015 ) && ( moment > 0 ) )
            {
                return true;
            }

            return false;
        }

        public bool WithUpperRange( double pivotValue )
        {
            if ( Math.Abs( UpperBound - pivotValue ) < 0.00029 )
            {
                return true;
            }

            return false;
        }

        public bool WithLowerRange( double pivotValue )
        {
            if ( Math.Abs( LowerBound - pivotValue ) < 0.00029 )
            {
                return true;
            }

            return false;
        }
    }

    public class WholeCandleRangeF : RangeEx<float>
    {
        internal WholeCandleRangeF( float lowerBound, float upperBound ) : base( lowerBound, upperBound )
        {

        }

        public static WholeCandleRangeF Create( float from, float to )
        {
            return new WholeCandleRangeF( from, to );
        }

        public bool ExactTouch( float pivotValue )
        {
            if ( Math.Abs( UpperBound - pivotValue ) < 0.00005 )
            {
                return true;
            }

            return false;
        }

        public bool AlmostTouch( float pivotValue )
        {
            var moment = pivotValue - UpperBound;

            if ( ( moment <= 0.00015 ) && ( moment > 0 ) )
            {
                return true;
            }

            return false;
        }

        public bool WithUpperRange( float pivotValue )
        {
            if ( Math.Abs( UpperBound - pivotValue ) < 0.00029 )
            {
                return true;
            }

            return false;
        }

        public bool WithLowerRange( float pivotValue )
        {
            if ( Math.Abs( LowerBound - pivotValue ) < 0.00029 )
            {
                return true;
            }

            return false;
        }        
    }
}


