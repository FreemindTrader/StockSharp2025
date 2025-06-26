using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class UpperShadowRangeD : RangeEx< double >
    {
        private decimal? _priceStep;

        internal UpperShadowRangeD( double lowerBound, double upperBound, decimal? priceStep ) : base( lowerBound, upperBound )
        {
            _priceStep = priceStep;    
        }

        public static UpperShadowRangeD Create( double from, double to, decimal? priceStep )
        {
            return new UpperShadowRangeD( from, to, priceStep );
        }

        public bool ExactTouch( double pivotValue )
        {
            var exactNumber = _priceStep.HasValue ? 0.5 * (double)_priceStep.Value : 0.00005;

            if ( Math.Abs( UpperBound - pivotValue ) < exactNumber)
            {
                return true;
            }

            return false;
        }

        public bool AlmostTouch( double pivotValue )
        {
            var almostTouch = _priceStep.HasValue ? 1.5 * (double)_priceStep.Value : 0.00015;

            var moment = pivotValue - UpperBound;

            if ( ( moment <= almostTouch) && ( moment > 0 ) )
            {
                return true;
            }            

            return false;
        }

        //public bool JustBreak( double pivotValue )
        //{
        //    var moment = pivotValue - UpperBound;

        //    if ( ( moment < 0.0002 ) && ( moment > 0 ) )
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }

    public class UpperShadowRangeF : RangeEx<float>
    {
        private double? _priceStep;

        internal UpperShadowRangeF( float lowerBound, float upperBound, double? priceStep ) : base( lowerBound, upperBound )
        {
            _priceStep = priceStep;
        }

        public static UpperShadowRangeF Create( float from, float to, double? priceStep )
        {
            return new UpperShadowRangeF( from, to, priceStep );
        }

        public bool ExactTouch( float pivotValue )
        {
            var exactNumber = _priceStep.HasValue ? 0.5 * (float)_priceStep.Value : 0.00005;

            if ( Math.Abs( UpperBound - pivotValue ) < exactNumber )
            {
                return true;
            }

            return false;
        }

        public bool AlmostTouch( float pivotValue )
        {
            var almostTouch = _priceStep.HasValue ? 1.5 * (float)_priceStep.Value : 0.00015;

            var moment = pivotValue - UpperBound;

            if ( ( moment <= almostTouch ) && ( moment > 0 ) )
            {
                return true;
            }

            return false;
        }

        //public bool JustBreak( double pivotValue )
        //{
        //    var moment = pivotValue - UpperBound;

        //    if ( ( moment < 0.0002 ) && ( moment > 0 ) )
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}
