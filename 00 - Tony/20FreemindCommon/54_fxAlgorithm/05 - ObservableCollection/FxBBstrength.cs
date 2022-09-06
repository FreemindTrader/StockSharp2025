using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Mvvm;
using fx.Definitions;

namespace fx.Algorithm
{
    public class FxBBstrength : BindableBase
    {
        public FxBBstrength( TimeSpan period, int barNumber )
        {
            _period = period;
            _barNumber = barNumber;
            _bbStrength = 0;
        }

        int _barColor;
        int _barNumber;
        int _bbStrength;
        private TimeSpan _period;

        public int Strength
        {
            get
            {
                return _bbStrength;
            }
            set
            {
                SetValue( ref _bbStrength, value );
            }
        }

        public int BarNumber
        {
            get
            {
                return _barNumber;
            }
            set
            {
                SetValue( ref _barNumber, value );
            }
        }

        public int BarColor
        {
            get
            {
                return _barColor;
            }
            set
            {
                SetValue( ref _barColor, value );
            }
        }

        public override bool Equals( object obj )
        {
            if( obj is FxBBstrength )
            {
                return Equals( ( FxBBstrength )obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( FxBBstrength first, FxBBstrength second )
        {
            if( ( object )first == null )
            {
                return ( object )second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( FxBBstrength first, FxBBstrength second )
        {
            return !( first == second );
        }

        public bool Equals( FxBBstrength other )
        {
            if( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _barNumber.Equals( other._barNumber ) && _bbStrength.Equals( other._bbStrength );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _barNumber.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _bbStrength.GetHashCode( );

                return hashCode;
            }
        }
    }
}

