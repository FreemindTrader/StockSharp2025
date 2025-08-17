using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class RealBodyRange : RangeEx<double>
    {
        internal RealBodyRange( double lowerBound, double upperBound ) : base( lowerBound, upperBound )
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

        public static RealBodyRange Create( double from, double to )
        {
            return new RealBodyRange( from, to );
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
}
