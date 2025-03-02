using fx.Database;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Algorithm
{
    public class WaveVector : IComparable, IComparable< WaveVector >, IEquatable< WaveVector >
    {
        private WavePointInfo _from;
        private WavePointInfo _to;

        private TimeSpan _period;

        public WaveVector( TimeSpan period,
                            WavePointInfo fromWpi,
                            WavePointInfo toWpi )
        {
            _period = period;
            _from = fromWpi;
            _to = toWpi;
        }

        //private void InternalGetWaves( long fromTime, long toTime )
        //{
        //    var hewManager = AdvancedAnalysisManager.GetCurrentElliottWaveManager( );

        //    var selectedDictionary = hewManager.GetElliottWavesDictionary( _period );

        //    if( selectedDictionary.ContainsKey( fromTime ) )
        //    {
        //        var from = selectedDictionary[ fromTime ].ElliottWave;
        //    }

        //    if( selectedDictionary.ContainsKey( toTime ) )
        //    {
        //        var to = selectedDictionary[ toTime ].ElliottWave;
        //    }
        //}

        public override bool Equals( Object obj )
        {
            return obj is WaveVector && this == ( WaveVector )obj;
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _period.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _from.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _to.GetHashCode( );

                return hashCode;
            }
        }

        public static bool operator ==( WaveVector x, WaveVector y )
        {
            return x._period == y._period && x._from == y._from && x._to == y._to;
        }
        public static bool operator !=( WaveVector x, WaveVector y )
        {
            return !( x == y );
        }

        public override string ToString( )
        {
            return _from.ToString( ) + ":" + _to.ToString( );
        }

        public bool Equals( WaveVector other )
        {
            return other._period == _period && other._from == _from && other._to == _to;
        }

        public int CompareTo( object obj )
        {
            if( obj == null )
            {
                return 1;
            }

            WaveVector other = ( WaveVector )obj;
            if( other == null )
            {
                throw new ArgumentException( "obj is not a WaveVector" );
            }

            return CompareTo( other );
        }

        public int CompareTo( WaveVector other )
        {
            if( other == null )
            {
                return 1;
            }

            int result = 0;

            result = _period.CompareTo( other._period );
            if( result != 0 )
            {
                return result;
            }

            result = _from.CompareTo( other._from );
            if( result != 0 )
            {
                return result;
            }

            return _to.CompareTo( other._to );
            ;
        }

        public bool IsEmpty( )
        {
            return _period == TimeSpan.Zero;
        }
    }
}
