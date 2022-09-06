using System;
using System.ComponentModel;
using DevExpress.Mvvm;
using fx.Definitions;

namespace fx.Algorithm
{
    public class FxCurrentWaveInfo : BindableBase, ICurrentWaveInfo, IEquatable< FxCurrentWaveInfo >, IComparable, IComparable< FxCurrentWaveInfo >
    {
        ElliottWaveCycle _cycle;

        public ElliottWaveCycle Cycle
        {
            get
            {
                return _cycle;
            }
            set
            {
                SetValue( ref _cycle, value );
            }
        }

        TrendDirection _waveTrend;

        public TrendDirection WaveDirection
        {
            get
            {
                return _waveTrend;
            }
            set
            {
                SetValue( ref _waveTrend, value );
            }
        }

        ElliottWaveEnum _previousWave;

        public ElliottWaveEnum PreviousWave
        {
            get
            {
                return _previousWave;
            }
            set
            {
                SetValue( ref _previousWave, value );
            }
        }

        ElliottWaveEnum _currentWave;

        public ElliottWaveEnum CurrentWave
        {
            get
            {
                return _currentWave;
            }
            set
            {
                SetValue( ref _currentWave, value );
            }
        }

        float _nextLevel;

        public float NextLevel
        {
            get
            {
                return _nextLevel;
            }
            set
            {
                SetValue( ref _nextLevel, value );
            }
        }

        float _price;

        public float Price
        {
            get
            {
                return _price;
            }
            set
            {
                SetValue( ref _price, value );
            }
        }

        string _specialNumber;

        public string SpecialNumber
        {
            get
            {
                return _specialNumber;
            }
            set
            {
                SetValue( ref _specialNumber, value );
            }
        }

        int _waveRotationBar;

        public int WaveRotationBar
        {
            get
            {
                return _waveRotationBar;
            }
            set
            {
                SetValue( ref _waveRotationBar, value );
            }
        }

        public override bool Equals( object obj )
        {
            if( obj is FxCurrentWaveInfo )
            {
                return Equals( ( FxCurrentWaveInfo )obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( FxCurrentWaveInfo first, FxCurrentWaveInfo second )
        {
            if( ( object )first == null )
            {
                return ( object )second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( FxCurrentWaveInfo first, FxCurrentWaveInfo second )
        {
            return !( first == second );
        }

        public bool Equals( FxCurrentWaveInfo other )
        {
            if( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _cycle.Equals( other._cycle ) && _previousWave.Equals( other._previousWave ) && _currentWave.Equals( other._currentWave ) && _nextLevel.Equals( other._nextLevel ) && _price.Equals( other._price ) && Equals( _specialNumber, other._specialNumber ) && _waveRotationBar.Equals( other._waveRotationBar );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ ( int )_cycle;
                hashCode = ( hashCode * 53 ) ^ ( int )_previousWave;
                hashCode = ( hashCode * 53 ) ^ ( int )_currentWave;
                hashCode = ( hashCode * 53 ) ^ _nextLevel.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _price.GetHashCode( );
                if( _specialNumber != null )
                {
                    hashCode = ( hashCode * 53 ) ^ _specialNumber.GetHashCode( );
                }

                hashCode = ( hashCode * 53 ) ^ _waveRotationBar.GetHashCode( );

                return hashCode;
            }
        }

        public int CompareTo( object obj )
        {
            if( obj == null )
            {
                return 1;
            }

            FxCurrentWaveInfo other = obj as FxCurrentWaveInfo;
            if( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( FxCurrentWaveInfo ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( FxCurrentWaveInfo other )
        {
            if( other == null )
            {
                return 1;
            }

            int result = 0;
            result = _cycle.CompareTo( other._cycle );
            if( result != 0 )
            {
                return result;
            }

            result = _previousWave.CompareTo( other._previousWave );
            if( result != 0 )
            {
                return result;
            }

            result = _currentWave.CompareTo( other._currentWave );
            if( result != 0 )
            {
                return result;
            }

            result = _nextLevel.CompareTo( other._nextLevel );
            if( result != 0 )
            {
                return result;
            }

            result = _price.CompareTo( other._price );
            if( result != 0 )
            {
                return result;
            }

            result = _specialNumber.CompareTo( other._specialNumber );
            if( result != 0 )
            {
                return result;
            }

            result = _waveRotationBar.CompareTo( other._waveRotationBar );
            if( result != 0 )
            {
                return result;
            }

            return result;
        }
    }
}
