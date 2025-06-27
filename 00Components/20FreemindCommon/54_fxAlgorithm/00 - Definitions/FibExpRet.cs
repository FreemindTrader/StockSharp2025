using fx.Collections;
using fx.TimePeriod;

using fx.Common;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Text;
using fx.Bars;

namespace fx.Algorithm
{
    public class FibKey : IEquatable<FibKey>, IComparable, IComparable<FibKey>
    {
        FibonacciType _fibType;
        ( DateTime, float ) _start;
        ( DateTime, float ) _end;
        ( DateTime, float ) _projection;
        TimeBlockEx _fibRange;

        public TimeBlockEx FibTimeBlock
        {
            get
            {
                return _fibRange;
            }
            set
            {
                _fibRange = value;
            }
        }

        public ( DateTime, float ) Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        public ( DateTime, float ) End
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
            }
        }

        public ( DateTime, float ) Projection
        {
            get
            {
                return _projection;
            }
            set
            {
                _projection = value;
            }
        }

        public FibonacciType FibType
        {
            get
            {
                return _fibType;
            }
            set
            {
                _fibType = value;
            }
        }

        public FibKey( FibonacciType fibType, ( DateTime, float ) start, ( DateTime, float ) end )
        {
            _fibType = fibType;
            _start = start;
            _end = end;
            _fibRange = new TimeBlockEx( start.Item1, end.Item1 );
        }

        public FibKey( FibonacciType fibType, ( DateTime, float ) start, ( DateTime, float ) end, ( DateTime, float ) projection )
        {
            _fibType = fibType;
            _start = start;
            _end = end;
            _projection = projection;

            _fibRange = new TimeBlockEx( start.Item1, end.Item1 );
        }

        public override bool Equals( object obj )
        {
            if ( obj is FibKey )
            {
                return Equals( ( FibKey ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( FibKey first, FibKey second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( FibKey first, FibKey second )
        {
            return !( first == second );
        }

        public bool Equals( FibKey other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _fibType.Equals( other._fibType ) && _start.Equals( other._start ) && _end.Equals( other._end ) && _projection.Equals( other._projection ) && _fibRange.Equals( other._fibRange );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ ( int ) _fibType;
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _start );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _end );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<(DateTime, float)>.Default.GetHashCode( _projection );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<TimeBlockEx>.Default.GetHashCode( _fibRange );
                return hashCode;
            }
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return 1;
            }

            FibKey other = obj as FibKey;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( FibKey ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( FibKey other )
        {
            if ( other == null )
            {
                return 1;
            }

            int result = 0;
            result = _fibType.CompareTo( other._fibType );
            if ( result != 0 )
            {
                return result;
            }

            result = _start.CompareTo( other._start );
            if ( result != 0 )
            {
                return result;
            }

            result = _end.CompareTo( other._end );
            if ( result != 0 )
            {
                return result;
            }

            result = _projection.CompareTo( other._projection );
            if ( result != 0 )
            {
                return result;
            }

            return result;
        }
    }

    public class FibExpRet : IEquatable<FibExpRet>, IComparable, IComparable<FibExpRet>
    {
        DictionarySlim< FibKey, FibLevelsCollection > _retracements = new DictionarySlim< FibKey, FibLevelsCollection >( );
        DictionarySlim< FibKey, FibLevelsCollection > _expansions = new DictionarySlim< FibKey, FibLevelsCollection >( );

        FibLevelsCollection _mainFibLevels;
        FibonacciType       _mainFibType;
        FibKey              _key;

        SBar _owningBar;
        ElliottWaveEnum _targetWaveName;

        public FibExpRet( ref SBar owningBar, ElliottWaveEnum targatWaveName )
        {
            _owningBar = owningBar;
            _targetWaveName = targatWaveName;
        }

        public void AddExpansions( FibKey key, FibLevelsCollection value )
        {
            _expansions.GetOrAddValueRef( key ) = value;
        }

        public void AddRetracements( FibKey key, FibLevelsCollection value )
        {
            _retracements.GetOrAddValueRef( key ) = value;
        }

        public FibLevelsCollection MainFibLevels
        {
            get
            {
                return _mainFibLevels;
            }
            set
            {
                _mainFibLevels = value;
            }
        }

        public PooledList< SRlevel > GetSRlevelList( )
        {
            PooledList< SRlevel > output = new PooledList< SRlevel >( );

            foreach( KeyValuePair< FibKey, FibLevelsCollection > fib in _expansions )
            {
                var fibKey = fib.Key;

                foreach( var fibLvl in fib.Value.FibLevels )
                {
                    var tb = fibKey.FibTimeBlock;
                    output.Add( new SRlevel( ref tb, 
                                                _owningBar.BarPeriod, 
                                                fibLvl.FibLevel, 
                                                ( int )fibLvl.FibLevelStrengh, 
                                                fibKey.FibType.To1stSuppRes( ),
                                                _targetWaveName.To2ndSuppRes( ), 
                                                fibKey.FibType.To3rdSuppRes( ), 
                                                fibLvl.FibPrecentage.ToString( ) ) );
                }
            }

            foreach( KeyValuePair< FibKey, FibLevelsCollection > fib in _retracements )
            {
                var fibKey = fib.Key;

                foreach( var fibLvl in fib.Value.FibLevels )
                {
                    var tb = fibKey.FibTimeBlock;
                    output.Add( new SRlevel( ref tb,
                                                _owningBar.BarPeriod,
                                                fibLvl.FibLevel,
                                                ( int )fibLvl.FibLevelStrengh,
                                                fibKey.FibType.To1stSuppRes( ),
                                                _targetWaveName.To2ndSuppRes( ),
                                                fibKey.FibType.To3rdSuppRes( ),
                                                fibLvl.FibPrecentage.ToString( ) ) );
                }
            }

            return output;
        }

        public FibonacciType MainFibType
        {
            get
            {
                return _mainFibType;
            }
            set
            {
                _mainFibType = value;
            }
        }

        public DictionarySlim< FibKey, FibLevelsCollection > FibRetracements
        {
            get
            {
                return _retracements;
            }
            set
            {
                _retracements = value;
            }
        }

        public DictionarySlim< FibKey, FibLevelsCollection > FibExpansions
        {
            get
            {
                return _expansions;
            }
            set
            {
                _expansions = value;
            }
        }

        public FibKey Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        public override bool Equals( object obj )
        {
            if ( obj is FibExpRet )
            {
                return Equals( ( FibExpRet ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( FibExpRet first, FibExpRet second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( FibExpRet first, FibExpRet second )
        {
            return !( first == second );
        }

        public bool Equals( FibExpRet other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return Equals( _retracements, other._retracements ) && Equals( _expansions, other._expansions ) && Equals( _mainFibLevels, other._mainFibLevels ) && _mainFibType.Equals( other._mainFibType ) && Equals( _key, other._key ) && _owningBar.Equals( other._owningBar ) && _targetWaveName.Equals( other._targetWaveName );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                if ( _retracements != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<DictionarySlim<FibKey, FibLevelsCollection>>.Default.GetHashCode( _retracements );
                }

                if ( _expansions != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<DictionarySlim<FibKey, FibLevelsCollection>>.Default.GetHashCode( _expansions );
                }

                if ( _mainFibLevels != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<FibLevelsCollection>.Default.GetHashCode( _mainFibLevels );
                }

                hashCode = ( hashCode * 53 ) ^ ( int ) _mainFibType;
                if ( _key != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<FibKey>.Default.GetHashCode( _key );
                }

                hashCode = ( hashCode * 53 ) ^ EqualityComparer<SBar>.Default.GetHashCode( _owningBar );
                hashCode = ( hashCode * 53 ) ^ ( int ) _targetWaveName;
                return hashCode;
            }
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return 1;
            }

            FibExpRet other = obj as FibExpRet;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( FibExpRet ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( FibExpRet other )
        {
            if ( other == null )
            {
                return 1;
            }

            int result = 0;
            result = _mainFibType.CompareTo( other._mainFibType );
            if ( result != 0 )
            {
                return result;
            }

            result = _key.CompareTo( other._key );
            if ( result != 0 )
            {
                return result;
            }

            result = _targetWaveName.CompareTo( other._targetWaveName );
            if ( result != 0 )
            {
                return result;
            }

            return result;
        }
    }
}
