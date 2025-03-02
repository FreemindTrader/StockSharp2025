using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public class GannKey : IEquatable<GannKey>, IComparable, IComparable<GannKey>
    {
        public GannKey( double startValue, long startTime, TrendDirection trendDirection, int numberOfCycles )
        {
            _startTime      = startTime;
            _numberOfCycles = numberOfCycles;
            _startValue     = startValue;
            _trendDirection = trendDirection;
        }
        long _startTime;
        int _numberOfCycles;
        double _startValue;
        TrendDirection _trendDirection;
        public TrendDirection TrendDirection
        {
            get
            {
                return _trendDirection;
            }
            set
            {
                _trendDirection = value;
            }
        }


        public double StartValue
        {
            get { return _startValue; }
            set
            {
                _startValue = value;
            }
        }


        public int NumberOfCycles
        {
            get { return _numberOfCycles; }
            set
            {
                _numberOfCycles = value;
            }
        }

        
        public long StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
            }
        }

        public override bool Equals( object obj )
        {
            if ( obj is GannKey )
            {
                return Equals( ( GannKey ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( GannKey first, GannKey second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( GannKey first, GannKey second )
        {
            return !( first == second );
        }

        public bool Equals( GannKey other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _startTime.Equals( other._startTime ) && _numberOfCycles.Equals( other._numberOfCycles ) && _startValue.Equals( other._startValue ) && _trendDirection.Equals( other._trendDirection );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _startTime.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _numberOfCycles.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _startValue.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ ( int ) _trendDirection;
                return hashCode;
            }
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return 1;
            }

            GannKey other = obj as GannKey;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( GannKey ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( GannKey other )
        {
            if ( other == null )
            {
                return 1;
            }

            int result = 0;
            result = _startTime.CompareTo( other._startTime );
            if ( result != 0 )
            {
                return result;
            }

            result = _numberOfCycles.CompareTo( other._numberOfCycles );
            if ( result != 0 )
            {
                return result;
            }

            result = _startValue.CompareTo( other._startValue );
            if ( result != 0 )
            {
                return result;
            }

            result = _trendDirection.CompareTo( other._trendDirection );
            if ( result != 0 )
            {
                return result;
            }

            return result;
        }
    }
}
