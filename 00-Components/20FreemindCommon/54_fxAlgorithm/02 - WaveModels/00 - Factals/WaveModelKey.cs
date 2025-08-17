using fx.Database;
using fx.Bars;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockSharp.BusinessEntities;

#pragma warning disable 414

namespace fx.Algorithm
{
    public class WaveModelKey : IEquatable<WaveModelKey>, IComparable, IComparable<WaveModelKey>
    {
        public WaveModelKey( Security sec, TimeSpan period, int waveScenarioNo, long rawBeginTime, long rawEndTime, ElliottWaveCycle waveCycle )
        {
            _security       = sec;
            _period         = period;
            _waveScenarioNo = waveScenarioNo;
            _rawBeginTime   = rawBeginTime;
            _rawEndTime     = rawEndTime;
            _waveCycle      = waveCycle;

            _keyTime        = GetKeyTime( rawBeginTime, waveCycle );
        }

        
        public int WaveScenarioNo
        {
            get => _waveScenarioNo;
            set => _waveScenarioNo = value;
        }


        long _rawEndTime;
        int _waveScenarioNo;
        long _rawBeginTime;

        long _keyTime;

        ElliottWaveCycle _waveCycle;

        public long RawBeginTime
        {
            get => _rawBeginTime;
            set => _rawBeginTime = value;
        }

        
        public long RawEndTime
        {
            get => _rawEndTime;
            set => _rawEndTime = value;
        }

        public ElliottWaveCycle WaveCycle
        {
            get => _waveCycle;
            set => _waveCycle = value;
        }


        TimeSpan _period;

        public TimeSpan Period
        {
            get => _period;
            set => _period = value;
        }

        Security _security;

        public Security Security
        {
            get => _security;
            set => _security = value;
        }

        public override bool Equals( object obj )
        {
            if ( obj is WaveModelKey waveModelKey )
            {
                return Equals( waveModelKey );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( WaveModelKey first, WaveModelKey second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( WaveModelKey first, WaveModelKey second )
        {
            return !( first == second );
        }

        public bool Equals( WaveModelKey other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _keyTime.Equals( other._keyTime ) && _period.Equals( other._period ) && Equals( _security, other._security );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _rawBeginTime.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<TimeSpan>.Default.GetHashCode( _period );
                if ( _security != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<Security>.Default.GetHashCode( _security );
                }

                return hashCode;
            }
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return 1;
            }

            WaveModelKey other = obj as WaveModelKey;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( WaveModelKey ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( WaveModelKey other )
        {
            if ( other == null )
            {
                return 1;
            }

            int result = 0;

            result = _keyTime.CompareTo( other._keyTime );
            if ( result != 0 )
            {
                return result;
            }

            return result;
        }

        private long GetKeyTime( long rawTime, ElliottWaveCycle waveCycle )
        {
            if ( rawTime > -1 )
            {
                var utcTime = rawTime.FromLinuxTime( );

                if ( waveCycle >= ElliottWaveCycle.Primary )
                {
                    var onlyDate = utcTime.Date;

                    return onlyDate.ToLinuxTime( );
                }

                return rawTime;
            }

            return -1;
        }
    }
}
