using System;
using System.Collections.Generic;


namespace fx.Definitions
{
    /// <summary>
    

    /// </summary>
    /*
     This EURUSD (M) above graphically depicts how every turn since Oct 2008 had bar counts that can be found in both the Fibonacci Series, Lucas Series & The Doubles ( Doubling each number in each series ), I'll let you guys look up Edouard Lucas (1842-1891). The French Mathematician who identified a second series of number beginning with 2 and 1 ( in that order which are added to arrive at the next number in the series 3 ( See Table on chart ) The math that proves the relationship of both series can be shown below.

    1. Dividing a Lucas number by the next number in series will get you closer to 0.618 ( Same as Fib Series )
    2. Take a Fibonacci number and double it, now add it to it's neighbouring Lucas number and see what you get. ( See table on chart eg. 2 x (Fib5) = 10 + (Lucas11) = Fib21! and so on. )
    3. What do you notice about the two Fibonacci numbers either side of the neighbouring Lucas number ( See table on chart eg. (Fib5) + (Fib13) = (Lucas18) and so on.)
    4. Multiply a Lucas number by it's Fibonacci Neighbour in the Table on the chart eg. (Fib3) x (Luc7) = Fib21 and so on.
    5. This one shows the importance of the Doubles of the two series, Begin with (Fib5) and its Lucas Neighbour (Luc11).
    
        Now locate the 3rd Fib number on either side of (Fib5) ie. (Fib1 & Fib21). Now, adding them together gives you 22 which is 2 x (Luc11) = Double Lucas 22. 
        Now try (Fib8) & it's Lucas Neighbour (Luc18)...........I'll let you figure this one out. Can you see these Double Lucas numbers on the chart?
        Therefore we can conclude that almost always when we see a Fibonacci Bar count there is a Lucas, Lucas Double or Fibonacci Double nearby. 
        Now here's the kicker, what do you think happens when you get multiple counts from previous major tops & 
        bottoms all terminating on a particular bar that also lines up with a major range retracement level like the 50%, LIKE LAST MONTH:)

        I'll let the chart answer the question. 
         */
    public enum GannDegreesType
    {        
        Gann_45Dg_Multiple = 12,
        Gann_30Dg_Multiple = 13,
        Gann_SpecialSeq = 14,
        Gann_45Dg_Calendar = 15,
        Gann_30Dg_Calendar = 16,
        Gann_SpecialSeq_Cal = 17,
        Gann_45Dg_Nearby = 18,
        Gann_30Dg_Nearby = 19,
        Gann_45Dg_Nearby_Calendar = 20,
        Gann_30Dg_Nearby_Calendar = 21,
    }


    public enum TaGannPriceTimeType
    {
        NotSquared = 0,

        CurrentPrice_TimeElapsed_TYPE             = 100,
        CurrentPrice_TimeElapsed_Local            = 101,
        CurrentPrice_TimeElapsed_RedDot           = 102,
        CurrentPrice_TimeElapsed_Major            = 103,

        PriorTrendTime_CurrentTrendRange_TYPE     = 200,
        PriorTrendTime_CurrentTrendRange_Local    = 201,
        PriorTrendTime_CurrentTrendRange_RedDot   = 202,
        PriorTrendTime_CurrentTrendRange_Major    = 203,

        PriorPriceRange_CurrentTrendTime_TYPE     = 300,
        PriorPriceRange_CurrentTrendTime_Local    = 301,
        PriorPriceRange_CurrentTrendTime_RedDot   = 302,
        PriorPriceRange_CurrentTrendTime_Major    = 303,

        PriorTrendEndPrice_CurrentTrendTime_TYPE  = 400,
        PriorTrendEndPrice_CurrentTrendTime_Local = 401,
        PriorEndingPrice_CurrentTrendTime_RedDot  = 402,
        PriorEndingPrice_CurrentTrendTime_Major   = 403,

        CurrentTrendRange_CurrentTrendTime_TYPE   = 500,
        CurrentTrendRange_CurrentTrendTime_Local  = 501,
        CurrentTrendRange_CurrentTrendTime_RedDot = 502,
        CurrentTrendRange_CurrentTrendTime_Major  = 503,
    }

    

    public class GannPriceTimeInfo : IEquatable<GannPriceTimeInfo>, IComparable, IComparable<GannPriceTimeInfo>
    {
        private double                    _degreesDiff;
        private double                    _optionOne;
        private double                    _optionTwo;
        private int                    _beginBarIndex;
        private int                    _parentBarIndex;
        private TimeSpan               _period;
        private TaGannPriceTimeType    _priceTimeType;
        private GannDegreesType _degreesType;

        public GannPriceTimeInfo( TimeSpan period,
                                  int parentBarIndex,
                                  TaGannPriceTimeType waveRotation
                                )
        {
            _period = period;
            _parentBarIndex = parentBarIndex;
            _priceTimeType = waveRotation;
        }

        public TaGannPriceTimeType GetGeneralType()
        {
            int output = ( int ) _priceTimeType;

            int newi = (int)Math.Round(output / 100d)*100;

            TaGannPriceTimeType newOuput = ( TaGannPriceTimeType ) newi;

            return newOuput;
        }



        public static bool isGannPriceTimeType( TaGannPriceTimeType priceTimeType )
        {
            int tobechecked  = ( int ) priceTimeType;

            if ( tobechecked >= 120 )
            {
                return true;
            }

            return false;
        }


        public TaGannPriceTimeType GannPriceTimeType
        {
            get { return _priceTimeType; }
            set
            {
                _priceTimeType = value;
            }
        }

        public GannDegreesType GannPriceTimeDegrees
        {
            get { return _degreesType; }
            set
            {
                _degreesType = value;
            }
        }

        

        public override string ToString( )
        {
            var priceTimeType = GannPriceTimeType;
            string output = "[" + _beginBarIndex + "-> " + _parentBarIndex + "] " + _degreesDiff + "Degrees.\r\nType = " + priceTimeType.ToDescription() + "\r\nN=" + GannPriceTimeDegrees.ToString();

            if ( isGannPriceTimeType( priceTimeType ) )
            {
                output += ( "\r\n1st Degree = " + _optionOne );
                output += ( "\r\n2nd Degree = " + _optionTwo );
            }

            return output;
        }


        public double BarDiffOrDegree
        {
            get { return _degreesDiff; }
            set
            {
                _degreesDiff = value;
            }
        }


        public int ParentBarIndex
        {
            get { return _parentBarIndex; }
            set
            {
                _parentBarIndex = value;
            }
        }

        public int BeginBarIndex
        {
            get { return _beginBarIndex; }
            set
            {
                _beginBarIndex = value;
            }
        }

        public double OptionOne
        {
            get { return _optionOne; }
            set
            {
                _optionOne = value;
            }
        }


        public double OptionTwo
        {
            get { return _optionTwo; }
            set
            {
                _optionTwo = value;
            }



        }

        public override bool Equals( object obj )
        {
            if ( obj is GannPriceTimeInfo )
            {
                return Equals( ( GannPriceTimeInfo ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( GannPriceTimeInfo first, GannPriceTimeInfo second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( GannPriceTimeInfo first, GannPriceTimeInfo second )
        {
            return !( first == second );
        }

        public bool Equals( GannPriceTimeInfo other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _degreesDiff.Equals( other._degreesDiff ) && _optionOne.Equals( other._optionOne ) && _optionTwo.Equals( other._optionTwo ) && _beginBarIndex.Equals( other._beginBarIndex ) && _parentBarIndex.Equals( other._parentBarIndex );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;

                hashCode = ( hashCode * 53 ) ^ _degreesDiff.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _optionOne.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _optionTwo.GetHashCode( );

                hashCode = ( hashCode * 53 ) ^ _beginBarIndex.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _parentBarIndex.GetHashCode( );
                return hashCode;
            }
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return 1;
            }

            GannPriceTimeInfo other = obj as GannPriceTimeInfo;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( GannPriceTimeInfo ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( GannPriceTimeInfo other )
        {
            if ( other == null )
            {
                return 1;
            }

            int result = 0;

            result = _parentBarIndex.CompareTo( other._parentBarIndex );
            if ( result != 0 )
            {
                return result;
            }

            result = _priceTimeType.CompareTo( other._priceTimeType );
            if ( result != 0 )
            {
                return result;
            }

            result = _degreesType.CompareTo( other._degreesType );
            if ( result != 0 )
            {
                return result;
            }

            result = _beginBarIndex.CompareTo( other._beginBarIndex );
            if ( result != 0 )
            {
                return result;
            }

            result = _optionOne.CompareTo( other._optionOne );
            if ( result != 0 )
            {
                return result;
            }

            result = _optionTwo.CompareTo( other._optionTwo );
            if ( result != 0 )
            {
                return result;
            }



            return result;
        }


    }
}



