using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class LowerShadowRangeD : RangeEx<double>
    {
        private decimal? _priceStep;
        internal LowerShadowRangeD( double lowerBound, double upperBound, decimal ? priceStep ) : base( lowerBound, upperBound )
        {
            _priceStep = priceStep;
        }

        public bool ExactTouch( double pivotValue )
        {
            var exactNumber = _priceStep.HasValue ? 0.5 * (double)_priceStep.Value : 0.00005;

            if ( Math.Abs( LowerBound - pivotValue ) < exactNumber)
            {
                return true;
            }

            return false;
        }

        public static LowerShadowRangeD Create( double from, double to, decimal? priceStep ) 
        {            
            return new LowerShadowRangeD( from, to, priceStep);
        }

        public bool AlmostTouch( double pivotValue )
        {
            var almostTouch = _priceStep.HasValue ? 1.5 * (double)_priceStep.Value : 0.00015;

            var moment = LowerBound - pivotValue;

            if ( ( moment <= almostTouch) && ( moment > 0 ) )
            {
                return true;
            }

            return false;
        }

        
    }

    public class LowerShadowRangeF : RangeEx<float>
    {
        private double? _priceStep;
        internal LowerShadowRangeF( float lowerBound, float upperBound, double? priceStep ) : base( lowerBound, upperBound )
        {
            _priceStep = priceStep;
        }

        public bool ExactTouch( float pivotValue )
        {
            var exactNumber = _priceStep.HasValue ? 0.5 * (float)_priceStep.Value : 0.00005;

            if ( Math.Abs( LowerBound - pivotValue ) < exactNumber )
            {
                return true;
            }

            return false;
        }

        public static LowerShadowRangeF Create( float from, float to, double? priceStep )
        {
            return new LowerShadowRangeF( from, to, priceStep );
        }

        public bool AlmostTouch( float pivotValue )
        {
            var almostTouch = _priceStep.HasValue ? 1.5 * (float)_priceStep.Value : 0.00015;

            var moment = LowerBound - pivotValue;

            if ( ( moment <= almostTouch ) && ( moment > 0 ) )
            {
                return true;
            }

            return false;
        }


    }
}
