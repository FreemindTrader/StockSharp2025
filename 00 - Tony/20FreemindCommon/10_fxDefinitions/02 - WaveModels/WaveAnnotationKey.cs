using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public struct WaveAnnotationKey : IEquatable<WaveAnnotationKey>
    {
        private long _barTime;

        private WaveLabelPosition _labelPos;

        public WaveAnnotationKey( long barTime, WaveLabelPosition pos )
        {
            _barTime = barTime;
            _labelPos = pos;
        }

        public override bool Equals( object obj )
        {
            if ( obj is WaveAnnotationKey )
            {
                return Equals( ( WaveAnnotationKey ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( WaveAnnotationKey first, WaveAnnotationKey second )
        {
            return first.Equals( second );
        }

        public static bool operator !=( WaveAnnotationKey first, WaveAnnotationKey second )
        {
            return !( first == second );
        }

        public bool Equals( WaveAnnotationKey other )
        {
            return _barTime.Equals( other._barTime ) && _labelPos.Equals( other._labelPos );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _barTime.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ ( int ) _labelPos;
                return hashCode;
            }
        }
    }
}
