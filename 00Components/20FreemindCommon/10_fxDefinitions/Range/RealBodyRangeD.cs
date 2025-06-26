using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class RealBodyRangeD : RangeEx<double>
    {
        internal RealBodyRangeD( double lowerBound, double upperBound ) : base( lowerBound, upperBound )
        {

        }

        public bool HighestPointExactTouch( double pivotValue )
        {            
            if ( Math.Abs( UpperBound - pivotValue ) < 0.00005 )
            {
                return true;
            }

            return false;
        }

        public bool LowestPointExactTouch( double pivotValue )
        {
            if ( Math.Abs( LowerBound - pivotValue ) < 0.00005 )
            {
                return true;
            }

            return false;
        }

        public static RealBodyRangeD Create( double from, double to )
        {
            return new RealBodyRangeD( from, to );
        }

        public bool HighestPointAlmostTouch( double pivotValue )
        {
            var moment = pivotValue - UpperBound;

            if ( ( moment <= 0.00015 ) && ( moment > 0 ) )
            {
                return true;
            }

            return false;
        }


        public bool LowestPointAlmostTouch( double pivotValue )
        {
            var moment = LowerBound - pivotValue;

            if ( ( moment <= 0.00015 ) && ( moment > 0 ) )
            {
                return true;
            }

            return false;
        }
    }

    public class RealBodyRangeF : RangeEx<float>
    {
        internal RealBodyRangeF( float lowerBound, float upperBound ) : base( lowerBound, upperBound )
        {

        }

        public bool HighestPointExactTouch( float pivotValue )
        {
            if ( Math.Abs( UpperBound - pivotValue ) < 0.00005 )
            {
                return true;
            }

            return false;
        }

        public bool LowestPointExactTouch( float pivotValue )
        {
            if ( Math.Abs( LowerBound - pivotValue ) < 0.00005 )
            {
                return true;
            }

            return false;
        }

        public static RealBodyRangeF Create( float from, float to )
        {
            return new RealBodyRangeF( from, to );
        }

        public bool HighestPointAlmostTouch( float pivotValue )
        {
            var moment = pivotValue - UpperBound;

            if ( ( moment <= 0.00015 ) && ( moment > 0 ) )
            {
                return true;
            }

            return false;
        }


        public bool LowestPointAlmostTouch( float pivotValue )
        {
            var moment = LowerBound - pivotValue;

            if ( ( moment <= 0.00015 ) && ( moment > 0 ) )
            {
                return true;
            }

            return false;
        }
    }
}
