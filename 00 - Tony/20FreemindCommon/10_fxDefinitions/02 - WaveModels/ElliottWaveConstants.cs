using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

namespace fx.Definitions
{    
    public enum ElliottWaveCountType : byte
    {
        INVALID             = 0,
        PRIMARY             = 1,
        ALTERNATIVE         = 2,
        CONFIRMED           = 3
    }
    public enum FoundWaves : byte
    {
        NONE                = 0,
        FirstHalf           = 1,
        SecondHalf          = 2,
        Added               = 3
    }



    [Flags]
    public enum WaveType : ushort
    {
        UNKNOWN             = 0,
        Impulsive5Waves     = 1 ,
        TerminalImpulse     = 1 << 1,
        CompressedImpulse   = 1 << 2,
        ThreeWaves          = 1 << 3,                 // Even Wave A of an impulsive wave can also be ABC ( different from Ian's stuff )
                                        // I observe that Wave A of Impulsive 5 waves can be both 3 waves and also 5 waves.
        Correction          = 1 << 4,
        ComplexCorrection   = 1 << 5,
        RunningCorrection   = 1 << 6,
        ImptWavePattern     = 1 << 7
    }

    public enum NeelyWaveType : byte
    {
        UNKNOWN             = 0,
        MONOWAVE            = 1,
        POLYWAVE            = 2,
        MULTIWAVE           = 3,
        MACROWAVE           = 4
    }    


    public enum MonoWaveNumber : byte
    {
        NA  = 0,
        m8_ = 1,
        m7_ = 2,
        m6_ = 3,
        m5_ = 4,
        m4_ = 5,
        m3_ = 6,
        m2_ = 7,
        m1_ = 8,
        m0  = 9,
        m1  = 10,
        m2  = 11,
        m3  = 12,
        m4  = 13,
        m5  = 14,
        m6  = 15,
        m7  = 16,
        m8  = 17,
    }

    public enum CorrectionDepth : byte
    {
        UNKNOWN             = 0,
        Deep                = 1,
        Shallow             = 2,
    }

    
    
    
    public enum FibonacciType : byte
    {
        NONE                = 0,
        [Description( "Wave C Projection" )]
        WaveCProjection     = 21,
        [Description( "Wave 2 Retracement" )]
        Wave2Retracement    = 22,
        [Description( "Wave 3 Projection" )]
        Wave3Projection     = 23,
        [Description( "Wave 3C Projection" )]
        Wave3CProjection    = 24,
        [Description( "Wave 4 Retracement" )]
        Wave4Retracement    = 25,
        [Description( "Wave 5 Projection" )]
        Wave5Projection     = 26,
        [Description( "Wave C Projection" )]
        Wave5CProjection    = 27,
        [Description( "Wave C of ABC Projection" )]
        ABCWaveCProjection  = 28,
        [Description( "Wave B of ABC Retracement" )]
        ABCWaveBRetracement = 29,
        [Description( "EFB Retracement" )]
        WaveEFBRetracement  = 30,
        [Description( "^B Retracement" )]
        WaveTriBRetracement = 31,
        [Description( "^C Retracement" )]
        WaveTriCProjection  = 32,
        [Description( "^D Retracement" )]
        WaveTriDProjection  = 33,
        [Description( "^E Retracement" )]
        WaveTriEProjection  = 34,
        [Description( "1st X Projection" )]
        FirstXProjection    = 35,
        [Description( "2nd X Projection" )]
        SecondXProjection   = 36,
        [Description( "Tony Projection" )]
        TonyProjection   = 37,
        TonyRetracement   = 38,
    }

    public enum FibLevelOccurance : byte
    {
        Unknown      = 0,
        MostCommon   = 1,
        LessFrequent = 2,
        Occasional   = 3,
        Rare         = 4
        
    }

    // Here I am using 3 bits to represent the wave name. That's a total of 
    [Flags]
    public enum ElliottWaveCycle 
    {
        UNKNOWN             = 0,        
        Miniscule           = 1,
        Submicro            = 2,
        Micro               = 3,
        Subminuette         = 4,               // minutes
        Minuette            = 5,               // hours
        SubMinute           = 6,
        Minute              = 7,               // days
        SubMinor            = 8,
        Minor               = 9,               // weeks
        SubIntermediate     = 10,
        Intermediate        = 11,               // weeks to months
        Primary             = 12,               // a few months to a couple of years
        Cycle               = 13,               // one year to several years (or even several decades under an Elliott Extension)
        Supercycle          = 14,               // multi-decade (about 40–70 years)
        GrandSupercycle     = 15,                // multi-century
        MAX                 = 15
    }

    public enum WaveLabelPosition : int
    {
        UNKNOWN = -1,
        BOTTOM  = 0,
        TOP     = 1,
        BOTH    = 2
    }


    public enum PriceAction : byte
    {
        UNKNOWN        = 0,
        //![](8B0951918AC6E9DDFF21C6562628881E.png;;;0.03021,0.02890)
        Directional    = 1,
        NonDirectional = 2
    }

    


    public enum ElliottWaveEnum : byte
    {
        NONE      = 0,
        Wave1     = 1,
        Wave1C    = 2,
        Wave2     = 3,
        Wave3     = 4,
        Wave3C    = 5,
        Wave4     = 6,
        Wave5     = 7,                
        Wave5C    = 8,        
        WaveTA    = 9,
        WaveTB    = 10,
        WaveTC    = 11,
        WaveTD    = 12,
        WaveTE    = 13,
        WaveEFA   = 14,
        WaveEFB   = 15,
        WaveEFC   = 16,
        WaveA     = 17,
        WaveB     = 18,
        WaveC     = 19,
        WaveX     = 20,
        WaveY     = 21,
        WaveZ     = 22,
        WaveW     = 23,
        COMPLEX   = 31      // MAX 
    }

    public enum WavePointImportanceType : byte
    {
        GannSwing = 1,
        nenZigZag = 2
        
    }

    public enum SR1stType : byte
    {
        Unknown   = 0,
        MonthPP   = 1,
        WeekPP    = 2,
        DailyPP   = 3,
        HewEXP    = 4,
        HewRET    = 5,
        SR        = 6,
        Time      = 7,
        MA        = 8,
    }

    public enum SR2ndType : byte
    {
        NONE              = 0,
        HalfResistance    = 1,
        Resistance        = 2,
        HalfSupport       = 3,
        Support           = 4,
        Mean              = 5,
        DirectionNo       = 6,

        Impulsive         = 10,
        Correction        = 11,

        ZigZag            = 12,
        Flat              = 13,
        ExpandedFlat      = 14,
        Triangle          = 15,
        IrregularTriangle = 16,

        BarHigh           = 21,
        BarLow            = 22,
        BarClose          = 23,

        SMA01             = 30,
        SMA05             = 31,
        SMA15             = 32,
        SMA30             = 33,
        SMA1H             = 34,
        SMA2H             = 35,
        SMA4H             = 36,
        SMA1D             = 37,
        SMA7D             = 38,
        SMA1M = 39,


        EMA = 31
    }

    public enum SR3rdType : byte
    {
        NONE                = 0,
        [Description( "S3" )]
        S3                  = 1,
        [Description( "M0" )]
        M0                  = 2,
        [Description( "S2" )]
        S2                  = 3,
        [Description( "M1" )]
        M1                  = 4,
        [Description( "S1" )]
        S1                  = 5,
        [Description( "M2" )]
        M2                  = 6,
        [Description( "PP" )]
        PIVOT               = 7,
        [Description( "M3" )]
        M3                  = 8,
        [Description( "R1" )]
        R1                  = 9,
        [Description( "M4" )]
        M4                  = 10,
        [Description( "R2" )]
        R2                  = 11,
        [Description( "M5" )]
        M5                  = 12,
        [Description( "R3" )]
        R3                  = 13,
        [Description( "MD" )]
        MDN                 = 14,
        

        [Description( "Wave C Projection" )]
        WaveCProjection     = 21,
        [Description( "Wave 2 Retracement" )]
        Wave2Retracement    = 22,
        [Description( "Wave 3 Projection" )]
        Wave3Projection     = 23,
        [Description( "Wave 3C Projection" )]
        Wave3CProjection    = 24,
        [Description( "Wave 4 Retracement" )]
        Wave4Retracement    = 25,
        [Description( "Wave 5 Projection" )]
        Wave5Projection     = 26,
        [Description( "Wave C Projection" )]
        Wave5CProjection    = 27,
        [Description( "Wave C of ABC Projection" )]
        ABCWaveCProjection  = 28,
        [Description( "Wave B of ABC Retracement" )]
        ABCWaveBRetracement = 29,
        [Description( "EFB Retracement" )]
        WaveEFBRetracement  = 30,
        [Description( "^B Retracement" )]
        WaveTriBRetracement = 31,
        [Description( "^C Retracement" )]
        WaveTriCProjection  = 32,
        [Description( "^D Retracement" )]
        WaveTriDProjection  = 33,
        [Description( "^E Retracement" )]
        WaveTriEProjection  = 34,
        [Description( "1st X Projection" )]
        FirstXProjection    = 35,
        [Description( "2nd X Projection" )]
        SecondXProjection   = 36,
        [Description( "Tony Projection" )]
        TonyProjection = 37,

        [Description( "MA" )]
        SMA50               = 50,
        [Description( "MA" )]
        SMA233              = 63
    }

    /// <image url="$(SolutionDir)\..\..\30 - CommonImages\ElliottWave.png" />
    /// 




    public class WavePointImportance : IComparable, IComparable< WavePointImportance >, IEquatable< WavePointImportance >
    {
        private long                    _barLinuxTime;
        private long                    _barIndex;
        private int                     _waveImportance;
        private TimeSpan                _period;
        private TimeSpan                _highestTimeframe;
        private WavePointImportanceType _waveImportanceType;

        private TASignal                _signal;

        public override bool Equals( object obj )
        {
            if ( obj is WavePointImportance )
                return Equals( ( WavePointImportance ) obj );

            return base.Equals( obj );
        }

        public static bool operator ==( WavePointImportance first, WavePointImportance second )
        {
            if ( ( object ) first == null )
                return ( object ) second == null;
            return first.Equals( second );
        }

        public static bool operator !=( WavePointImportance first, WavePointImportance second )
        {
            return !( first == second );
        }

        public bool Equals( WavePointImportance other )
        {
            if ( ReferenceEquals( null, other ) )
                return false;

            if ( ReferenceEquals( this, other ) )
                return true;

            return _barLinuxTime.Equals( other._barLinuxTime ) && _waveImportance.Equals( other._waveImportance ) && _waveImportanceType.Equals( other._waveImportanceType ) && _signal.Equals( other._signal );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;

                hashCode     = ( hashCode * 53 ) ^ _barLinuxTime.GetHashCode( );
                hashCode     = ( hashCode * 53 ) ^ _waveImportance.GetHashCode( );
                hashCode     = ( hashCode * 53 ) ^ _waveImportanceType.GetHashCode( );
                hashCode     = ( hashCode * 53 ) ^ _signal.GetHashCode( );

                return hashCode;
            }
        }

        public override string ToString( )
        {
            //var time = _barEpochTime.FromLinuxTime();

            var output = string.Format( "[{0}] - Imp:{1} ({2}) {3}", _barIndex, _highestTimeframe.ToShortForm(), _waveImportance, _signal.ToText()  );

            return output;
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
                return 1;

            WavePointImportance other = ( WavePointImportance ) obj ;

            if ( other == null )
                throw new ArgumentException("obj is not a WavePointImportance");

            return CompareTo( other );
        }

        public int CompareTo( WavePointImportance other )
        {
            if ( other == null )
                return 1;

            int result = 0;

            result = _barLinuxTime.CompareTo( other._barLinuxTime );

            if ( result != 0 )
                return result;

            result = _waveImportanceType.CompareTo( other._waveImportanceType );

            if ( result != 0 )
                return result;

            result = _signal.CompareTo( other._signal );

            if ( result != 0 )
                return result;

            return _waveImportance.CompareTo( other._waveImportance );
        }

        

        public WavePointImportance(     TimeSpan period,
                                        long rawBarTime,
                                        long barIndex,
                                        int importance,
                                        WavePointImportanceType importanceType,
                                        TASignal signal
                                  )
        {
            _period               = period;
            _barLinuxTime         = rawBarTime;
            _barIndex             = barIndex;
            _waveImportance       = importance;
            _waveImportanceType   = importanceType;
            _signal               = signal;
            _highestTimeframe     = period;
        }

        public int WaveImportance
        {
            get { return _waveImportance; }

            set { _waveImportance = value; }
        }

        public long LinuxTime
        {
            get { return _barLinuxTime; }

            set { _barLinuxTime = value; }
        }

        public long BarIndex
        {
            get { return _barIndex; }

            set { _barIndex = value; }
        }

        public WavePointImportanceType WavePointImportanceType
        {
            get { return _waveImportanceType; }

            set { _waveImportanceType = value; }
        }

        public TASignal Signal
        {
            get { return _signal; }

            set { _signal = value; }
        }


        public TimeSpan HighestTimeframe
        {
            get { return _highestTimeframe; }

            set { _highestTimeframe = value; }
        }
    }


    


    public struct FibLevelsInfo
    {
        private float [ ] _fibonacciLevels;

        private int []    _fibonacciLevelsStrength;

        FibonacciType     _fibonacciType;

        public FibLevelsInfo( 
                                        FibonacciType fibonacciType,
                                        float [ ] fibonacciLevels,
                                        int [] fibonacciLevelsStrength
                                  )
        {
            _fibonacciType           = fibonacciType;
            _fibonacciLevels         = fibonacciLevels;
            _fibonacciLevelsStrength = fibonacciLevelsStrength;
        }

        public float [] FibLevels
        {
            get
            {
                return _fibonacciLevels;
            }
        }

        public int [ ] FibLevelsStrength
        {
            get
            {
                return _fibonacciLevelsStrength;
            }
        }

        public FibonacciType FibType
        {
            get
            {
                return _fibonacciType;
            }
        }
    }

    //public struct FibLevelInfo : IEquatable<FibLevelInfo>, IComparable, IComparable<FibLevelInfo>
    //{
    //    public double LikelyScore;
    //    public bool IsBroken;
    //    public int DynamicSRLinesRating;
    //    public int OverlappedCount;
    //    public FibPercentage FibPrecentage;
    //    public double FibLevelStrengh;
    //    public float FibLevel;
    //    public double LowerBound;
    //    public double UpperBound;

    //    public FibLevelInfo( FibPercentage percentage, float fibLevel, double fibLevelStrengh )
    //    {
    //        FibPrecentage        = percentage;
    //        FibLevelStrengh      = fibLevelStrengh;
    //        FibLevel             = fibLevel;
    //        LowerBound           = fibLevel;
    //        UpperBound           = fibLevel;
    //        LikelyScore          = fibLevelStrengh;
    //        IsBroken             = false;
    //        DynamicSRLinesRating = 0;
    //        OverlappedCount      = 0;
    //    }
    //                                                              

    //    public void UpdateAll( FibLevelInfo fibLevel )
    //    {
    //        if ( fibLevel.HasRange )
    //        {
    //            if ( fibLevel.UpperBound > UpperBound )
    //            {
    //                UpperBound = fibLevel.UpperBound;
    //            }

    //            if ( fibLevel.LowerBound < LowerBound )
    //            {
    //                LowerBound = fibLevel.LowerBound;
    //            }

    //            OverlappedCount += fibLevel.OverlappedCount;
    //        }
    //        else
    //        {
    //            if ( fibLevel.FibLevel > UpperBound )
    //            {
    //                UpperBound = fibLevel.FibLevel;
    //            }

    //            if ( fibLevel.FibLevel < LowerBound )
    //            {
    //                LowerBound = fibLevel.FibLevel;
    //            }

    //            OverlappedCount += 1;
    //        }

    //        LikelyScore += fibLevel.LikelyScore;            
    //    }

    //    public bool WithinCluster( FibLevelInfo fibLevel, double pipAllowance )
    //    {
    //        if ( this.HasRange )
    //        {
    //            if ( ( fibLevel.FibLevel <= UpperBound ) && ( fibLevel.FibLevel >= LowerBound ) )
    //            {
    //                return true;
    //            }                
    //        }

    //        var diff = Math.Abs( fibLevel.FibLevel - this.FibLevel );

    //        if ( diff <= pipAllowance )
    //        {
    //            if ( fibLevel.FibLevel > UpperBound )
    //            {
    //                UpperBound = fibLevel.FibLevel;
    //            }

    //            if ( fibLevel.FibLevel < LowerBound )
    //            {
    //                LowerBound = fibLevel.FibLevel;
    //            }

    //            return true;
    //        }

    //        return false;
    //    }


    //    public bool HasRange
    //    {
    //        get
    //        {
    //            return UpperBound != LowerBound;
    //        }
    //    }


    //    

    //    
    //     
    //    


    //    

    //    public override string ToString( )
    //    {            
    //        var level = Math.Round( FibLevel, 5 );
    //        var lower = Math.Round( LowerBound, 5 );
    //        var upper = Math.Round( UpperBound, 5 );
    //        var score = Math.Round( LikelyScore, 1 );

    //        string output = "";            

    //        if ( UpperBound == LowerBound )
    //        {
    //            output += string.Format( "Score [{2}] - {0} : {1} ", FibPrecentage.ToDescription( ), level, score );
    //        }
    //        else
    //        {
    //            output += string.Format( "Score [{4}] - {0} : {1} ({2}-{3}) ", FibPrecentage.ToDescription( ), level, lower, upper, score );
    //        }

    //        if ( OverlappedCount  > 0 )
    //        {
    //            output += " [" + OverlappedCount + "]";
    //        }

    //        if ( DynamicSRLinesRating > 0 )
    //        {
    //            output += " <" + DynamicSRLinesRating + ">";
    //        }

    //        if ( IsBroken )
    //        {
    //            output += " [Broken] ";
    //        }

    //        return output;
    //    }

    //    

    //    public void UpdateLikelyScore( double score )
    //    {
    //        LikelyScore += score;
    //    }

    //    public override bool Equals( object obj )
    //    {
    //        if ( obj is FibLevelInfo )
    //        {
    //            return Equals( ( FibLevelInfo ) obj );
    //        }

    //        return base.Equals( obj );
    //    }

    //    public static bool operator ==( FibLevelInfo first, FibLevelInfo second )
    //    {
    //        return first.Equals( second );
    //    }

    //    public static bool operator !=( FibLevelInfo first, FibLevelInfo second )
    //    {
    //        return !( first == second );
    //    }

    //    public bool Equals( FibLevelInfo other )
    //    {
    //        return LikelyScore.Equals( other.LikelyScore ) && IsBroken.Equals( other.IsBroken ) && DynamicSRLinesRating.Equals( other.DynamicSRLinesRating ) && OverlappedCount.Equals( other.OverlappedCount ) && FibPrecentage.Equals( other.FibPrecentage ) && FibLevelStrengh.Equals( other.FibLevelStrengh ) && FibLevel.Equals( other.FibLevel ) && LowerBound.Equals( other.LowerBound ) && UpperBound.Equals( other.UpperBound );
    //    }

    //    public override int GetHashCode()
    //    {
    //        unchecked
    //        {
    //            int hashCode = 47;
    //            hashCode = ( hashCode * 53 ) ^ LikelyScore.GetHashCode();
    //            hashCode = ( hashCode * 53 ) ^ IsBroken.GetHashCode();
    //            hashCode = ( hashCode * 53 ) ^ DynamicSRLinesRating.GetHashCode();
    //            hashCode = ( hashCode * 53 ) ^ OverlappedCount.GetHashCode();
    //            hashCode = ( hashCode * 53 ) ^ ( int ) FibPrecentage;
    //            hashCode = ( hashCode * 53 ) ^ FibLevelStrengh.GetHashCode();
    //            hashCode = ( hashCode * 53 ) ^ FibLevel.GetHashCode();
    //            hashCode = ( hashCode * 53 ) ^ LowerBound.GetHashCode();
    //            hashCode = ( hashCode * 53 ) ^ UpperBound.GetHashCode();
    //            return hashCode;
    //        }
    //    }

    //    public int CompareTo( object obj )
    //    {
    //        if ( !( obj is FibLevelInfo ) )
    //        {
    //            throw new ArgumentException( nameof( obj ) + " is not a " + nameof( FibLevelInfo ) );
    //        }

    //        return CompareTo( ( FibLevelInfo ) obj );
    //    }

    //    public int CompareTo( FibLevelInfo other )
    //    {
    //        int result = 0;
    //        result = LikelyScore.CompareTo( other.LikelyScore );
    //        if ( result != 0 )
    //        {
    //            return result;
    //        }

    //        result = IsBroken.CompareTo( other.IsBroken );
    //        if ( result != 0 )
    //        {
    //            return result;
    //        }

    //        result = DynamicSRLinesRating.CompareTo( other.DynamicSRLinesRating );
    //        if ( result != 0 )
    //        {
    //            return result;
    //        }

    //        result = OverlappedCount.CompareTo( other.OverlappedCount );
    //        if ( result != 0 )
    //        {
    //            return result;
    //        }

    //        result = FibPrecentage.CompareTo( other.FibPrecentage );
    //        if ( result != 0 )
    //        {
    //            return result;
    //        }

    //        result = FibLevelStrengh.CompareTo( other.FibLevelStrengh );
    //        if ( result != 0 )
    //        {
    //            return result;
    //        }

    //        result = FibLevel.CompareTo( other.FibLevel );
    //        if ( result != 0 )
    //        {
    //            return result;
    //        }

    //        result = LowerBound.CompareTo( other.LowerBound );
    //        if ( result != 0 )
    //        {
    //            return result;
    //        }

    //        result = UpperBound.CompareTo( other.UpperBound );
    //        if ( result != 0 )
    //        {
    //            return result;
    //        }

    //        return result;
    //    }
    //}

    public class GannLevelInfo
    {
        public GannLevelInfo( double degree, double gannLevel, double gannLevelStrengh )
        {
            _gannDegree       = degree;
            _gannLevelStrengh = gannLevelStrengh > 0 ? gannLevelStrengh : GetStrengthByDegree( degree ) ;
            _gannLevel        = gannLevel;
        }

        public double GetStrengthByDegree( double degree )
        {
            if ( degree % 180 == 0 )
            {
                return 40;
            }
            else if ( degree % 90 == 0 )
            {
                return 30;
            }
            else if ( degree % 45 == 0 )
            {
                return 20;
            }
            else if ( degree % 30 == 0 )
            {
                return 10;
            }

            return 0;
        }

        double _gannDegree;
        double _gannLevelStrengh;
        double _gannLevel;

        public double GannLevel
        {
            get { return _gannLevel; }
            set
            {
                _gannLevel = value;
            }
        }


        public double GannLevelStrengh
        {
            get { return _gannLevelStrengh; }
            set
            {
                _gannLevelStrengh = value;
            }
        }


        public double GannDegree
        {
            get { return _gannDegree; }
            set
            {
                _gannDegree = value;
            }
        }

        public override string ToString( )
        {            
            var degree   = Math.Round( _gannDegree, 1 );
            var level = Math.Round( _gannLevel, 5 );

            string output = string.Format( "{0}{1} - {2}" , degree, GlobalConstants.Degree, level );

            
            return output;
        }
    }

    public struct WaveInfo : IEquatable<WaveInfo>, IComparable, IComparable<WaveInfo>
    {
        private ElliottWaveCycle  _cycle;
        private ElliottWaveEnum   _waveName;
        private WaveLabelPosition _labelPosition;

        public WaveInfo( long content )
        {
            content        = content & GlobalConstants.NewHewBitsMask;

            _waveName      = ( ElliottWaveEnum ) ( content & ( GlobalConstants.NewHewContentMask ) );

            _cycle         = ( ElliottWaveCycle ) ( ( content & ( GlobalConstants.NewHewCycleMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit ) );

            _labelPosition = ( WaveLabelPosition ) ( ( content & ( GlobalConstants.NewHewLabelMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.WaveLabelBit ) );
        
        }

        public WaveInfo( uint content )
        {
            content        = content & GlobalConstants.NewHewBitsMask;

            _waveName      = ( ElliottWaveEnum ) ( content & ( GlobalConstants.NewHewContentMask ) );

            _cycle         = ( ElliottWaveCycle ) ( ( content & ( GlobalConstants.NewHewCycleMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit ) );

            _labelPosition = ( WaveLabelPosition ) ( ( content & ( GlobalConstants.NewHewLabelMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.WaveLabelBit ) );

        }

        public WaveInfo( ElliottWaveCycle WaveCycle,
                         ElliottWaveEnum WaveName,
                         WaveLabelPosition LabelPosition )
        {
            _waveName      = WaveName;

            _cycle         = WaveCycle;

            _labelPosition = LabelPosition;
        }

        public long MyHewBits( int i )
        {

            long newHew = 0;
            int bitwiseWave = 0;

            bitwiseWave = ( int ) ( _cycle );            

            bitwiseWave = bitwiseWave << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

            bitwiseWave = bitwiseWave | ( int ) _waveName;

            if ( _labelPosition == WaveLabelPosition.TOP )
            {
                bitwiseWave = bitwiseWave | GlobalConstants.NewHewLabelMask;
            }

            newHew |= ( long ) bitwiseWave << i * GlobalConstants.NewHewBits;

            return newHew;
        }

        public uint SmallHewBits( int i )
        {

            uint newHew = 0;
            int bitwiseWave = 0;

            bitwiseWave = ( int ) ( _cycle );

            bitwiseWave = bitwiseWave << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

            bitwiseWave = bitwiseWave | ( int ) _waveName;

            if ( _labelPosition == WaveLabelPosition.TOP )
            {
                bitwiseWave = bitwiseWave | GlobalConstants.NewHewLabelMask;
            }

            newHew |= ( uint ) bitwiseWave << i * GlobalConstants.NewHewBits;

            return newHew;
        }

        public long CycleDown( int i )
        {
            long newHew = 0;
            int bitwiseWave = 0;

            bitwiseWave = ( int ) ( _cycle - GlobalConstants.OneWaveCycle );

            if ( bitwiseWave < 0 )
            {
                bitwiseWave = 0;
            }

            bitwiseWave = bitwiseWave << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

            bitwiseWave = bitwiseWave | ( int ) _waveName;

            if ( _labelPosition == WaveLabelPosition.TOP )
            {
                bitwiseWave = bitwiseWave | GlobalConstants.NewHewLabelMask;
            }

            newHew |= ( long ) bitwiseWave << i * GlobalConstants.NewHewBits;

            return newHew;
        }

        public uint SmallCycleDown( int i )
        {
            uint newHew = 0;
            int bitwiseWave = 0;

            bitwiseWave = ( int ) ( _cycle - GlobalConstants.OneWaveCycle );

            if ( bitwiseWave < 0 )
            {
                bitwiseWave = 0;
            }

            bitwiseWave = bitwiseWave << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

            bitwiseWave = bitwiseWave | ( int ) _waveName;

            if ( _labelPosition == WaveLabelPosition.TOP )
            {
                bitwiseWave = bitwiseWave | GlobalConstants.NewHewLabelMask;
            }

            newHew |= ( uint ) bitwiseWave << i * GlobalConstants.NewHewBits;

            return newHew;
        }


        public long CycleUp( int i )
        {
            long newHew = 0;
            int bitwiseWave = 0;

            bitwiseWave = ( int ) ( _cycle + GlobalConstants.OneWaveCycle );

            if ( bitwiseWave > ( int ) ElliottWaveCycle.GrandSupercycle )
            {
                bitwiseWave = ( int ) ElliottWaveCycle.GrandSupercycle;
            }

            bitwiseWave = bitwiseWave << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

            bitwiseWave = bitwiseWave | ( int ) _waveName;

            if ( _labelPosition == WaveLabelPosition.TOP )
            {
                bitwiseWave = bitwiseWave | GlobalConstants.NewHewLabelMask;
            }

            newHew |= ( long ) bitwiseWave << i * GlobalConstants.NewHewBits;

            return newHew;
        }

        public uint SmallCycleUp( int i )
        {
            uint newHew = 0;
            int bitwiseWave = 0;

            bitwiseWave = ( int ) ( _cycle + GlobalConstants.OneWaveCycle );

            if ( bitwiseWave > ( int ) ElliottWaveCycle.GrandSupercycle )
            {
                bitwiseWave = ( int ) ElliottWaveCycle.GrandSupercycle;
            }

            bitwiseWave = bitwiseWave << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

            bitwiseWave = bitwiseWave | ( int ) _waveName;

            if ( _labelPosition == WaveLabelPosition.TOP )
            {
                bitwiseWave = bitwiseWave | GlobalConstants.NewHewLabelMask;
            }

            newHew |= ( uint ) bitwiseWave << i * GlobalConstants.NewHewBits;

            return newHew;
        }


        public ElliottWaveCycle WaveCycle
        {
            get { return _cycle; }

            set { _cycle = value; }
        }

        public ElliottWaveEnum WaveName
        {
            get { return _waveName; }

            set { _waveName = value; }
        }

        public WaveLabelPosition LabelPosition
        {
            get { return _labelPosition; }

            set { _labelPosition = value; }
        }

        public HewLong ElliottWave
        {
            get
            {
                return new HewLong( _cycle, _waveName, _labelPosition );
            }
            
        }

        public bool HasWave( )
        {
            return ( _waveName  != ElliottWaveEnum.NONE );
        }

        public WaveLabelPosition OppositeLabelDirection()
        {
            if ( _labelPosition == WaveLabelPosition.TOP )
            {
                return WaveLabelPosition.BOTTOM;
            }
            else if ( _labelPosition == WaveLabelPosition.BOTTOM )
            {
                return WaveLabelPosition.TOP;
            }

            return WaveLabelPosition.UNKNOWN;

        }

        public override string ToString()
        {
            return _cycle.ToString() + ":" + _waveName.ToString();
        }


        public bool Equals( ElliottWaveCycle WaveCycle,
                            ElliottWaveEnum WaveName )
        {
            if ( HasWave( ) && ( _waveName == WaveName ) && ( _cycle == WaveCycle ) )
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is WaveInfo)
            {
                return Equals((WaveInfo)obj);
            }

            return base.Equals(obj);
        }

        public static bool operator ==(WaveInfo first, WaveInfo second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(WaveInfo first, WaveInfo second)
        {
            return !(first == second);
        }

        public bool Equals(WaveInfo other)
        {
            return _cycle.Equals(other._cycle) && _waveName.Equals(other._waveName) && _labelPosition.Equals(other._labelPosition);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = (hashCode * 53) ^ (int)_cycle;
                hashCode = (hashCode * 53) ^ (int)_waveName;
                hashCode = (hashCode * 53) ^ (int)_labelPosition;
                return hashCode;
            }
        }

        public int CompareTo(object obj)
        {
            if (!(obj is WaveInfo))
            {
                throw new ArgumentException(nameof(obj) + " is not a " + nameof(WaveInfo));
            }

            return CompareTo((WaveInfo)obj);
        }

        public int CompareTo(WaveInfo other)
        {
            int result = 0;
            result = _cycle.CompareTo(other._cycle);
            if (result != 0)
            {
                return result;
            }

            result = _waveName.CompareTo(other._waveName);
            if (result != 0)
            {
                return result;
            }

            result = _labelPosition.CompareTo(other._labelPosition);
            if (result != 0)
            {
                return result;
            }

            return result;
        }
    }


    public struct WavePointInfo : IEquatable<WavePointInfo>, IComparable, IComparable<WavePointInfo>
    {
        private long _barLinuxTime;
        private WaveInfo _myWaveInfo;        

        public WavePointInfo( long rawBarTime, WaveInfo waveInfo )
        {
            _barLinuxTime = rawBarTime;
            _myWaveInfo = waveInfo;
        }
        
        public WaveInfo WaveInfo
        {
            get { return _myWaveInfo; }

            set { _myWaveInfo = value; }
        }

        public long LinuxTime
        {
            get { return _barLinuxTime; }

            set { _barLinuxTime = value; }
        }

        public DateTime BarTime
        {
            get { return _barLinuxTime.FromLinuxTime(); }
            set { _barLinuxTime = value.ToLinuxTime( ); }
        }

        public override string ToString()
        {
            return BarTime.ToString() + ":" + _myWaveInfo.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is WavePointInfo)
            {
                return Equals((WavePointInfo)obj);
            }

            return base.Equals(obj);
        }

        public static bool operator ==(WavePointInfo first, WavePointInfo second)
        {
            return first.Equals(second);
        }

        public static bool operator !=(WavePointInfo first, WavePointInfo second)
        {
            return !(first == second);
        }

        public bool Equals(WavePointInfo other)
        {
            return _barLinuxTime.Equals(other._barLinuxTime) && _myWaveInfo.Equals(other._myWaveInfo);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = (hashCode * 53) ^ _barLinuxTime.GetHashCode();
                hashCode = (hashCode * 53) ^ EqualityComparer<WaveInfo>.Default.GetHashCode(_myWaveInfo);
                return hashCode;
            }
        }

        public int CompareTo(object obj)
        {
            if (!(obj is WavePointInfo))
            {
                throw new ArgumentException(nameof(obj) + " is not a " + nameof(WavePointInfo));
            }

            return CompareTo((WavePointInfo)obj);
        }

        public int CompareTo(WavePointInfo other)
        {
            int result = 0;
            result = _barLinuxTime.CompareTo(other._barLinuxTime);
            if (result != 0)
            {
                return result;
            }

            result = _myWaveInfo.CompareTo(other._myWaveInfo);
            if (result != 0)
            {
                return result;
            }

            return result;
        }
    }

    public static class WaveTypeHelper
    {
        public static SR2ndType To2ndSuppRes( this ElliottWaveEnum waveName )
        {
            switch ( waveName )
            {
                case ElliottWaveEnum.NONE:
                    return SR2ndType.NONE;

                case ElliottWaveEnum.Wave1:
                case ElliottWaveEnum.Wave1C:
                case ElliottWaveEnum.Wave3:
                case ElliottWaveEnum.Wave3C:
                case ElliottWaveEnum.Wave5:
                case ElliottWaveEnum.Wave5C:
                    return SR2ndType.Impulsive;

                case ElliottWaveEnum.WaveA:
                case ElliottWaveEnum.WaveB:
                case ElliottWaveEnum.WaveC:
                case ElliottWaveEnum.WaveW:
                case ElliottWaveEnum.WaveX:
                case ElliottWaveEnum.WaveY:
                case ElliottWaveEnum.WaveZ:
                case ElliottWaveEnum.Wave2:
                case ElliottWaveEnum.Wave4:
                    return SR2ndType.Correction;


                case ElliottWaveEnum.WaveTA:
                case ElliottWaveEnum.WaveTB:
                case ElliottWaveEnum.WaveTC:
                case ElliottWaveEnum.WaveTD:
                case ElliottWaveEnum.WaveTE:
                    return SR2ndType.Triangle;

                case ElliottWaveEnum.WaveEFA:
                case ElliottWaveEnum.WaveEFB:
                case ElliottWaveEnum.WaveEFC:
                    return SR2ndType.ExpandedFlat;
            }

            return SR2ndType.NONE;
        }

        public static SR1stType To1stSuppRes( this FibonacciType signal )
        {
            switch ( signal )
            {
                case FibonacciType.WaveCProjection:
                    return SR1stType.HewEXP;

                case FibonacciType.Wave2Retracement:
                    return SR1stType.HewRET;

                case FibonacciType.Wave3Projection:
                    return SR1stType.HewEXP;


                case FibonacciType.Wave3CProjection:
                    return SR1stType.HewEXP;


                case FibonacciType.Wave4Retracement:
                    return SR1stType.HewRET;


                case FibonacciType.Wave5Projection:
                    return SR1stType.HewEXP;


                case FibonacciType.Wave5CProjection:
                    return SR1stType.HewEXP;


                case FibonacciType.ABCWaveCProjection:
                    return SR1stType.HewEXP;


                case FibonacciType.ABCWaveBRetracement:
                    return SR1stType.HewRET;

                case FibonacciType.WaveEFBRetracement:
                    return SR1stType.HewRET;

                case FibonacciType.WaveTriBRetracement:
                    return SR1stType.HewRET;

                case FibonacciType.WaveTriCProjection:
                    return SR1stType.HewEXP;

                case FibonacciType.WaveTriDProjection:
                    return SR1stType.HewEXP;

                case FibonacciType.WaveTriEProjection:
                    return SR1stType.HewEXP;

                case FibonacciType.FirstXProjection:
                    return SR1stType.HewEXP;

                case FibonacciType.SecondXProjection:
                    return SR1stType.HewEXP;
            }

            return SR1stType.Unknown;
        }
        public static SR3rdType To3rdSuppRes( this FibonacciType signal )
        {
            switch ( signal )
            {
                case FibonacciType.WaveCProjection:
                    return SR3rdType.WaveCProjection;

                case FibonacciType.Wave2Retracement:
                    return SR3rdType.Wave2Retracement;

                case FibonacciType.Wave3Projection:
                    return SR3rdType.Wave3Projection;


                case FibonacciType.Wave3CProjection:
                    return SR3rdType.Wave3CProjection;


                case FibonacciType.Wave4Retracement:
                    return SR3rdType.Wave4Retracement;


                case FibonacciType.Wave5Projection:
                    return SR3rdType.Wave5Projection;


                case FibonacciType.Wave5CProjection:
                    return SR3rdType.Wave5CProjection;


                case FibonacciType.ABCWaveCProjection:
                    return SR3rdType.ABCWaveCProjection;


                case FibonacciType.ABCWaveBRetracement:
                    return SR3rdType.ABCWaveBRetracement;

                case FibonacciType.WaveEFBRetracement:
                    return SR3rdType.WaveEFBRetracement;

                case FibonacciType.WaveTriBRetracement:
                    return SR3rdType.WaveTriBRetracement;

                case FibonacciType.WaveTriCProjection:
                    return SR3rdType.WaveTriCProjection;

                case FibonacciType.WaveTriDProjection:
                    return SR3rdType.WaveTriDProjection;

                case FibonacciType.WaveTriEProjection:
                    return SR3rdType.WaveTriEProjection;

                case FibonacciType.FirstXProjection:
                    return SR3rdType.FirstXProjection;

                case FibonacciType.SecondXProjection:
                    return SR3rdType.SecondXProjection;
            }

            return SR3rdType.NONE;
        }
    }





}
