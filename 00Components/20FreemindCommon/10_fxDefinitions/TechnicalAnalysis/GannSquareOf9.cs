using System;
using System.Collections.Generic;

// Tony Lam. Gann square of price and Time https://forums.babypips.com/t/square-root-the-eurusd/47958/4
/*
 * So far, so good. The first significant High/Low was provided for us on March, 27 with a value of 1.33849. 
 * We begin our analysis at that particular high. Each and every instrument on the market vibrates to its own frequency, 
 * same with humans. For the less esoterically inclined, 
 * this concept illustrates the correct market scaling (price/time ratio) on the charts. 
 * I will provide a fixed ratio for EUR/USD that allows us to square prices and time correctly, 
 * without getting into further details on the origin of the ratio itself, yet. 
 * The ratio is 100/10000 (1/100) [Time(Days)/Price] for 5-digit prices behind the decimal point.
 * 
 * Let’s see how we convert the current high into the correct values that we use for our analysis:
 * 
 *  High  = 1.33849 =>
 *  Time  = 1.33849100   = 133.849 -> Converted value used for timing
 *  Price = 1.3384910000 = 13384.9 -> Converted value for pricing
 * 
 * 
 * 
 * Info.
 * http://www.stjarnhimlen.se/comp/ppcomp.html To calculate the location of the planets for aspect
 * 
 * http://www.astrolog.org/astrolog/astfile.htm
 * 
 * http://technical-analysis-addins.com/how-to-square-of-9.php
 * 
 */
namespace fx.Definitions
{
    /*
     * Current price squares with time elapsed from a prior change in trend.
     * Time in a prior trend squares with the price range of the current trend.
     * Price range in a prior trend squares with time in the current trend.
     * Price ending prior trend squares with time in the current trend.
     * Price range in the current trend squares with time in the current trend
     * 
     * */
    
    public static class Degrees2Factor
    {
        public const float DEGREE_22_5     = 0.125f;
        public const float DEGREE_45       = 0.25f;
        public const float DEGREE_90       = 0.5f;
        public const float DEGREE_135      = 0.75f;
        public const float DEGREE_180      = 1f;
        public const float DEGREE_225      = 1.25f;
        public const float DEGREE_270      = 1.50f;
        public const float DEGREE_315      = 1.75f;
        public const float DEGREE_360      = 2f;
    }


    public static class TimeCycleManager
    {
        private static readonly float [ ] _geometricTimeRatio = new float [ ]
        {
                                                                14.6f,
                                                                18.7f,
                                                                23.6f,
                                                                30.0f,
                                                                38.2f,
                                                                48.6f,
                                                                52.6f,
                                                                61.8f,
                                                                78.6f,
                                                                100.0f,
                                                                127.2f,
                                                                161.8f,
                                                                190.2f,
                                                                205.8f,
                                                                261.8f,
                                                                333.0f,
                                                                423.6f,
                                                                538.8f,
                                                                685.4f
        };

        private static readonly float [ ] _harmonicTimeRatio = new float [ ]
        {
                                                                12.5f,
                                                                17.7f,
                                                                25.0f,
                                                                35.4f,
                                                                50.0f,
                                                                70.7f,
                                                                141.42f,
                                                                200.0f,
                                                                282.8f,
                                                                400.0f,
                                                                565.7f,
                                                                800.0f
        };

        private static readonly float [ ] _arithmeticTimeRatio = new float [ ]
        {
                                                                16.7f,
                                                                33.3f,
                                                                57.7f,
                                                                66.7f,
                                                                150.0f,
                                                                173.2f,
                                                                300.0f,
                                                                600.0f
        };

        private static readonly float [ ] _root5TimeRatio = new float [ ]
        {
                                                                20.0f,
                                                                44.7f,
                                                                223.6f,
                                                                500.0f
        };
    }

    public class GannSquareOf9
    {

        double _time;
        double _price;
        int _numOfDigits;
        double _basePrice;

        public double BasePrice
        {
            get { return _basePrice; }
            set
            {
                _basePrice = value;
            }
        }


        public int NumOfDigits
        {
            get { return _numOfDigits; }
            set
            {
                _numOfDigits = value;
            }
        }


        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
            }
        }


        public double Time
        {
            get { return _time; }
            set
            {
                _time = value;
            }
        }



        public GannSquareOf9( double currentPrice )
        {
            _basePrice = currentPrice;
            _numOfDigits = ( currentPrice == 0 ) ? 1 : 1 + ( int ) Math.Log10( Math.Abs( currentPrice ) );

            ConvertTo3Digits( );
        }

        /*
         * Each and every instrument on the market vibrates to its own frequency, same with humans. For the less esoterically inclined, 
         * this concept illustrates the correct market scaling (price/time ratio) on the charts. 
         * I will provide a fixed ratio for EUR/USD that allows us to square prices and time correctly, 
         * without getting into further details on the origin of the ratio itself, yet. The ratio is 100/10000 (1/100) [Time(Days)/Price] for 5-digit prices behind the decimal point. 
         * 
         * 
         */
        public void ConvertTo3Digits( )
        {
            int digitsToShifted = 3 - _numOfDigits;

            _time = _basePrice * Math.Pow( 10, digitsToShifted );

            digitsToShifted = 5 - _numOfDigits;

            _price = _basePrice * Math.Pow( 10, digitsToShifted );
        }

        


        public double TimeDegree
        {
            get
            {
                return GetDegree( _time );
            }
        }

        public double PriceDegree
        {
            get
            {
                return GetDegree( _price );
            }
        }

        public static double GetDegree( double input )
        {
            var tempDegree = ( Math.Sqrt( input ) * 180 - 225 );

            var mod = tempDegree  / 360 ;

            var result2 = ( mod - Math.Truncate(mod) ) * 360;

            if ( result2 < 0 )
            {
                result2 = result2 + 360;
            }

            double result = (int) Math.Round( result2, 1, MidpointRounding.AwayFromZero );

            return result;
        }

        public static double GetDegree( int input )
        {
            return GetDegree( ( double ) input );
        }
    }
}
