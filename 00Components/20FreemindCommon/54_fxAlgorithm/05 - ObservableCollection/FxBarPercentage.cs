using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace fx.Algorithm
{
    public class FxBarPercentage : BindableBase, IEquatable< FxBarPercentage >
    {
        public FxBarPercentage( int barNumber )
        {
            _barNumber = barNumber;
            _barPercent = 0;
        }

        int _barColor;
        int _barNumber;
        double _barPercent;

        public double BarPercent
        {
            get
            {
                return _barPercent;
            }
            set
            {
                SetValue( ref _barPercent, value );
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
            if( obj is FxBarPercentage )
            {
                return Equals( ( FxBarPercentage )obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( FxBarPercentage first, FxBarPercentage second )
        {
            if( ( object )first == null )
            {
                return ( object )second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( FxBarPercentage first, FxBarPercentage second )
        {
            return !( first == second );
        }

        public bool Equals( FxBarPercentage other )
        {
            if( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _barNumber.Equals( other._barNumber ) && _barPercent.Equals( other._barPercent );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _barNumber.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _barPercent.GetHashCode( );

                return hashCode;
            }
        }
    }
}

