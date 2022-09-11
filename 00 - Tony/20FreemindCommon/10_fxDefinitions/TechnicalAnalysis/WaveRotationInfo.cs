using System;
using System.Collections.Generic;


namespace fx.Definitions
{
    /// <summary>
    ///<image url="$(SolutionDir)\..\..\30 - CommonImages\FibLucasDouble.png" />

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
    public enum TimeSpecialNumbersType
    {
        NONE                      = 0,
        FibSeq                    = 1,
        LucasSeq                  = 2,
        FibRatio                  = 3,
        FibsSeqNearby             = 4,
        LucasSeqNearby            = 5,
        DoubleFibsSeq             = 6,
        DoubleLucasSeq            = 7,
        GannNumbers               = 8,
        SqRootNumbers             = 9,
        DerRootNumbers            = 10,
        Numbers144                = 11
        
    }

    
    public enum TaWaveRotation
    {
        NoWaveRotation                            = 0,
        IMPORTANT_HIGH_HIGH                       = 1,
        IMPORTANT_HIGH_LOW                        = 2,
        IMPORTANT_LOW_HIGH                        = 3,
        IMPORTANT_LOW_LOW                         = 4,

        LOCAL_BEAR_TOTALBARS                      = 5,
        LOCAL_BEAR_LOW_LOW                        = 6,
        LOCAL_BEAR_CORRECTION                     = 7,
        LOCAL_BULL_TOTALBARS                      = 8,
        LOCAL_BULL_HIGH_HIGH                      = 9,
        LOCAL_BULL_CORRECTION                     = 10,
       
    }


    public class WaveRotationInfo : IEquatable<WaveRotationInfo>, IComparable, IComparable<WaveRotationInfo>
    {        
        private double                    _barDiff;
        private double                    _optionOne;
        private double                    _optionTwo;        
        private int                    _beginBarIndex;
        private int                    _parentBarIndex;
        private TimeSpan               _period;
        private TaWaveRotation    _priceTimeType;
        private TimeSpecialNumbersType _numberType;

        public WaveRotationInfo( TimeSpan period,
                                      int parentBarIndex,
                                      TaWaveRotation waveRotation
                                    )
        {
            _period         = period;
            _parentBarIndex = parentBarIndex;
            _priceTimeType  = waveRotation;
        }

        public static string GetPriceTimeTypeDesc( TaWaveRotation priceTimeType )
        {
            string output = " Bars.";

            if ( isGannPriceTimeType( priceTimeType ) )
            {
                output = " Degrees.";                
            }

            return output;
        }

        public static bool isGannPriceTimeType( TaWaveRotation priceTimeType )
        {
            int tobechecked  = ( int ) priceTimeType;

            if ( tobechecked >= 120 )
            {
                return true;
            }

            return false;
        }
        

        public TaWaveRotation GannPriceTimeType
        {
            get { return _priceTimeType; }
            set
            {
                _priceTimeType = value;
            }
        }

        public TimeSpecialNumbersType WaveTimeType
        {
            get { return _numberType; }
            set
            {
                _numberType = value;
            }
        }

        public static TaWaveRotation GetWaveRotationType ( TASignal first, TASignal second )
        {
            if ( first == TASignal.WAVE_PEAK && second == TASignal.WAVE_PEAK )
            {
                return TaWaveRotation.IMPORTANT_HIGH_HIGH;
            }
            else if ( first == TASignal.WAVE_PEAK && second == TASignal.WAVE_TROUGH )
            {
                return TaWaveRotation.IMPORTANT_HIGH_LOW;
            }
            else if ( first == TASignal.WAVE_TROUGH && second == TASignal.WAVE_TROUGH )
            {
                return TaWaveRotation.IMPORTANT_LOW_LOW;
            }
            else if ( first == TASignal.WAVE_TROUGH && second == TASignal.WAVE_PEAK )
            {
                return TaWaveRotation.IMPORTANT_LOW_HIGH;
            }

            throw new InvalidOperationException( );
        }

        public static TaWaveRotation GetLocalWaveRotationType( bool isBull, TASignal first, TASignal second )
        {
            if ( isBull )
            {
                if ( first == TASignal.WAVE_PEAK && second == TASignal.WAVE_PEAK )
                {
                    return TaWaveRotation.LOCAL_BULL_HIGH_HIGH;
                }
                else if ( first == TASignal.WAVE_PEAK && second == TASignal.WAVE_TROUGH )
                {
                    return TaWaveRotation.LOCAL_BULL_CORRECTION;
                }                
            }
            else
            {
                if ( first == TASignal.WAVE_TROUGH && second == TASignal.WAVE_TROUGH )
                {
                    return TaWaveRotation.LOCAL_BEAR_LOW_LOW;
                }
                else if ( first == TASignal.WAVE_TROUGH && second == TASignal.WAVE_PEAK )
                {
                    return TaWaveRotation.LOCAL_BEAR_CORRECTION;
                }
            }
            

            throw new InvalidOperationException( );
        }

        public override string ToString( )
        {
            var priceTimeType = GannPriceTimeType;
            string output = "[" + _beginBarIndex + "-> " + _parentBarIndex + "] " + _barDiff + GetPriceTimeTypeDesc( priceTimeType ) + "\r\nType = " + priceTimeType.ToDescription() + "\r\nN=" + WaveTimeType.ToString();

            if ( isGannPriceTimeType( priceTimeType ) )
            {
                output += ( "\r\n1st Degree = " + _optionOne );
                output += ( "\r\n2nd Degree = " + _optionTwo );
            }

            return output;
        }


        public double BarDiffOrDegree
        {
            get { return _barDiff; }
            set
            {
                _barDiff = value;
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
            if ( obj is WaveRotationInfo )
            {
                return Equals( ( WaveRotationInfo ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( WaveRotationInfo first, WaveRotationInfo second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( WaveRotationInfo first, WaveRotationInfo second )
        {
            return !( first == second );
        }

        public bool Equals( WaveRotationInfo other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return  _barDiff.Equals( other._barDiff ) && _optionOne.Equals( other._optionOne ) && _optionTwo.Equals( other._optionTwo )  && _beginBarIndex.Equals( other._beginBarIndex ) && _parentBarIndex.Equals( other._parentBarIndex );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                
                hashCode = ( hashCode * 53 ) ^ _barDiff.GetHashCode( );
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

            WaveRotationInfo other = obj as WaveRotationInfo;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( WaveRotationInfo ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( WaveRotationInfo other )
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

            result = _numberType.CompareTo( other._numberType );
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



