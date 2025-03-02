using fx.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class CandleFormation : IEquatable<CandleFormation>
    {
        PooledDictionary< int, TACandle > _candle = new PooledDictionary< int, TACandle >( );
        TACandle _candleType;
        int _strength;
        int _endIndex;
        int _beginIndex;        

        public CandleFormation( TimeSpan period, TACandle candleType, int strength, int beginIndex, int endIndex )
        {
            _candleType = candleType;
            _strength   = strength;
            _endIndex   = endIndex;
            _beginIndex = beginIndex;
            _period     = period;
        }

        public CandleFormation( TimeSpan period )
        {
            _candleType = TACandle.NONE;
            _strength   = -1;
            _endIndex   = -1;
            _beginIndex = -1;
            _period     = period;
        }

        public void DescribeCandle( int index, TACandle desc )
        {
            if ( _candle.ContainsKey( index ) )
            {
                _candle[ index ] = desc;
            }
            else
            {
                _candle.Add( index, desc );
            }
        }

        public int BeginIndex
        {
            get { return _beginIndex; }
            set
            {
                _beginIndex = value;
            }
        }


        public int EndIndex
        {
            get { return _endIndex; }
            set
            {
                _endIndex = value;
            }
        }


        public int Strength
        {
            get { return _strength; }
            set
            {
                _strength = value;
            }
        }

        
        public TACandle CandleType
        {
            get { return _candleType; }
            set
            {
                _candleType = value;
            }
        }

        TimeSpan _period;
        
        public TimeSpan Period
        {
            get { return _period; }
            set
            {
                _period = value;
            }
        }

        public override bool Equals( object obj )
        {
            if ( obj is CandleFormation )
            {
                return Equals( ( CandleFormation ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( CandleFormation first, CandleFormation second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( CandleFormation first, CandleFormation second )
        {
            return !( first == second );
        }

        public bool Equals( CandleFormation other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _candleType.Equals( other._candleType ) && _strength.Equals( other._strength ) && _endIndex.Equals( other._endIndex ) && _beginIndex.Equals( other._beginIndex ) && _period.Equals( other._period );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ ( int ) _candleType;
                hashCode = ( hashCode * 53 ) ^ _strength;
                hashCode = ( hashCode * 53 ) ^ _endIndex.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _beginIndex.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<TimeSpan>.Default.GetHashCode( _period );
                return hashCode;
            }
        }
    }
}
