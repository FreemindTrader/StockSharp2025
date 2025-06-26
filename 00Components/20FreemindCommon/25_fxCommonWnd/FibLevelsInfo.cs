using DevExpress.Mvvm;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace fx.Common
{
    //public class FibLevelsInfo : BindableBase, IComparable, IComparable<FibLevelsInfo>
    //{
    //    private int []    _fibonacciLevelsStrength;

    //    FibonacciType     _fibonacciType;

    //    public FibLevelsInfo(
    //                                    FibonacciType fibonacciType,
    //                                    float[ ] fibonacciLevels,
    //                                    int[ ] fibonacciLevelsStrength
    //                              )
    //    {
    //        _fibonacciType = fibonacciType;
    //        _fibonacciLevels = fibonacciLevels;
    //        _fibonacciLevelsStrength = fibonacciLevelsStrength;
    //    }

    //    private float [ ] _fibonacciLevels;

    //    public float[ ] FibLevels
    //    {
    //        get { return _fibonacciLevels; }
    //        set
    //        {
    //            SetValue( ref _fibonacciLevels, value );
    //        }
    //    }

    //    public int[ ] FibLevelsStrength
    //    {
    //        get
    //        {
    //            return _fibonacciLevelsStrength;
    //        }

    //        set
    //        {
    //            SetValue( ref _fibonacciLevelsStrength, value );
    //        }
    //    }

    //    public FibonacciType FibType
    //    {
    //        get
    //        {
    //            return _fibonacciType;
    //        }

    //        set
    //        {
    //            SetValue( ref _fibonacciType, value );
    //        }
    //    }

    //    public int CompareTo( object obj )
    //    {
    //        if ( obj == null )
    //        {
    //            return 1;
    //        }

    //        FibLevelsInfo other = obj as FibLevelsInfo;
    //        if ( other == null )
    //        {
    //            throw new ArgumentException( nameof( obj ) + " is not a " + nameof( FibLevelsInfo ) );
    //        }

    //        return CompareTo( other );
    //    }

    //    public int CompareTo( FibLevelsInfo other )
    //    {
    //        if ( other == null )
    //        {
    //            return 1;
    //        }

    //        return _fibonacciType.CompareTo( other._fibonacciType );
    //    }
    //}

    public class FibLevelInfo : BindableBase, IEquatable<FibLevelInfo>, IComparable, IComparable<FibLevelInfo>
    {
        
        private double _likelyScore;
        public double LikelyScore
        {
            get
            {
                return _likelyScore;
            }

            set
            {
                SetValue( ref _likelyScore, value );
            }
        }


        private bool _isBroken;
        public bool IsBroken
        {
            get
            {
                return _isBroken;
            }

            set
            {
                SetValue( ref _isBroken, value );
            }
        }

        private int _dynamicSRLinesRating;
        public int DynamicSRLinesRating
        {
            get
            {
                return _dynamicSRLinesRating;
            }

            set
            {
                SetValue( ref _dynamicSRLinesRating, value );
            }
        }

        private int _overlappedCount;
        public int OverlappedCount
        {
            get
            {
                return _overlappedCount;
            }

            set
            {
                SetValue( ref _overlappedCount, value );
            }
        }

        private FibPercentage _fibPrecentage;
        public FibPercentage FibPrecentage
        {
            get
            {
                return _fibPrecentage;
            }

            set
            {
                SetValue( ref _fibPrecentage, value );
            }
        }

        double _fibLevelStrengh;
        public double FibLevelStrengh
        {
            get => _fibLevelStrengh;
            set
            {
                SetValue( ref _fibLevelStrengh, value );
            }
        }

        public float _fibLevel;
        public float FibLevel
        {
            get => _fibLevel;
            set
            {
                SetValue( ref _fibLevel, value );
            }
        }

        private double _lowerBound;
        public double LowerBound
        {
            get
            {
                return _lowerBound;
            }

            set
            {
                SetValue( ref _lowerBound, value );
            }
        }

        private double _upperBound;
        public double UpperBound
        {
            get
            {
                return _upperBound;
            }

            set
            {
                SetValue( ref _upperBound, value );
            }
        }




        

        public FibLevelInfo( FibPercentage percentage, float fibLevel, double fibLevelStrengh )
        {
            FibPrecentage = percentage;
            FibLevelStrengh = fibLevelStrengh;
            FibLevel = fibLevel;
            LowerBound = fibLevel;
            UpperBound = fibLevel;
            LikelyScore = fibLevelStrengh;
            IsBroken = false;
            DynamicSRLinesRating = 0;
            OverlappedCount = 0;
        }


        public void UpdateAll( FibLevelInfo fibLevel )
        {
            if ( fibLevel.HasRange )
            {
                if ( fibLevel.UpperBound > UpperBound )
                {
                    UpperBound = fibLevel.UpperBound;
                }

                if ( fibLevel.LowerBound < LowerBound )
                {
                    LowerBound = fibLevel.LowerBound;
                }

                OverlappedCount += fibLevel.OverlappedCount;
            }
            else
            {
                if ( fibLevel.FibLevel > UpperBound )
                {
                    UpperBound = fibLevel.FibLevel;
                }

                if ( fibLevel.FibLevel < LowerBound )
                {
                    LowerBound = fibLevel.FibLevel;
                }

                OverlappedCount += 1;
            }

            LikelyScore += fibLevel.LikelyScore;
        }

        public bool WithinCluster( FibLevelInfo fibLevel, double pipAllowance )
        {
            if ( HasRange )
            {
                if ( ( fibLevel.FibLevel <= UpperBound ) && ( fibLevel.FibLevel >= LowerBound ) )
                {
                    return true;
                }
            }

            var diff = Math.Abs( fibLevel.FibLevel - FibLevel );

            if ( diff <= pipAllowance )
            {
                if ( fibLevel.FibLevel > UpperBound )
                {
                    UpperBound = fibLevel.FibLevel;
                }

                if ( fibLevel.FibLevel < LowerBound )
                {
                    LowerBound = fibLevel.FibLevel;
                }

                return true;
            }

            return false;
        }


        public bool HasRange
        {
            get
            {
                return UpperBound != LowerBound;
            }
        }

        public override string ToString()
        {
            var level = Math.Round( FibLevel, 5 );
            var lower = Math.Round( LowerBound, 5 );
            var upper = Math.Round( UpperBound, 5 );
            var score = Math.Round( LikelyScore, 1 );

            string output = "";

            if ( UpperBound == LowerBound )
            {
                output += string.Format( "Score [{2}] - {0} : {1} ", FibPrecentage.ToDescription(), level, score );
            }
            else
            {
                output += string.Format( "Score [{4}] - {0} : {1} ({2}-{3}) ", FibPrecentage.ToDescription(), level, lower, upper, score );
            }

            if ( OverlappedCount > 0 )
            {
                output += " [" + OverlappedCount + "]";
            }

            if ( DynamicSRLinesRating > 0 )
            {
                output += " <" + DynamicSRLinesRating + ">";
            }

            if ( IsBroken )
            {
                output += " [Broken] ";
            }

            return output;
        }



        public void UpdateLikelyScore( double score )
        {
            LikelyScore += score;
        }

        public override bool Equals( object obj )
        {
            if ( obj is FibLevelInfo fibLevelInfo )
            {
                return Equals( fibLevelInfo );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( FibLevelInfo first, FibLevelInfo second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( FibLevelInfo first, FibLevelInfo second )
        {
            return !( first == second );
        }

        public bool Equals( FibLevelInfo other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _likelyScore.Equals( other._likelyScore ) && _isBroken.Equals( other._isBroken ) && _dynamicSRLinesRating.Equals( other._dynamicSRLinesRating ) && _overlappedCount.Equals( other._overlappedCount ) && _fibPrecentage.Equals( other._fibPrecentage ) && _fibLevelStrengh.Equals( other._fibLevelStrengh ) && _fibLevel.Equals( other._fibLevel ) && _lowerBound.Equals( other._lowerBound ) && _upperBound.Equals( other._upperBound ) && HasRange.Equals( other.HasRange );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _likelyScore.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _isBroken.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _dynamicSRLinesRating.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _overlappedCount.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ ( int ) _fibPrecentage;
                hashCode = ( hashCode * 53 ) ^ _fibLevelStrengh.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _fibLevel.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _lowerBound.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _upperBound.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ HasRange.GetHashCode();
                return hashCode;
            }
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return 1;
            }

            FibLevelInfo other = obj as FibLevelInfo;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( FibLevelInfo ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( FibLevelInfo other )
        {
            if ( other == null )
            {
                return 1;
            }

            int result = 0;
            result = _likelyScore.CompareTo( other._likelyScore );
            if ( result != 0 )
            {
                return result;
            }

            result = _isBroken.CompareTo( other._isBroken );
            if ( result != 0 )
            {
                return result;
            }

            result = _dynamicSRLinesRating.CompareTo( other._dynamicSRLinesRating );
            if ( result != 0 )
            {
                return result;
            }

            result = _overlappedCount.CompareTo( other._overlappedCount );
            if ( result != 0 )
            {
                return result;
            }

            result = _fibPrecentage.CompareTo( other._fibPrecentage );
            if ( result != 0 )
            {
                return result;
            }

            result = _fibLevelStrengh.CompareTo( other._fibLevelStrengh );
            if ( result != 0 )
            {
                return result;
            }

            result = _fibLevel.CompareTo( other._fibLevel );
            if ( result != 0 )
            {
                return result;
            }

            result = _lowerBound.CompareTo( other._lowerBound );
            if ( result != 0 )
            {
                return result;
            }

            result = _upperBound.CompareTo( other._upperBound );
            if ( result != 0 )
            {
                return result;
            }

            result = HasRange.CompareTo( other.HasRange );
            if ( result != 0 )
            {
                return result;
            }

            return result;
        }
    }
}
