using fx.Collections;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace fx.Bars
{
    public enum SRLineResponseType
    {
        NoRelationShip           = 0,
        [Description( "Exact Touch" )]
        ExactTouch               = 1,
        [Description( "Almost Touch" )]
        AlmostTouch = 2,
        [Description( "Penetrated and closed Above" )]
        PenetratedAndClosedAbove = 20,            // This is used for downtrend, pivot point is tested and broken a little and promptly close on top of it
        [Description( "Penetrated and closed Below" )]
        PenetratedAndClosedBelow = 30,            // This is used for Uptrend,   pivot point is tested and broken a little and promptly close back below it
        [Description( "Broken and closed Above" )]
        BrokenAndClosedAbove     = 40,            // This is used for Uptrend,   pivot point is broken and close above it by certain pips
        [Description( "Tested Resistance and failed" )]
        TestedAndFailedBelow     = 45,            // This is used for Uptrend,   pivot point is broken and close above it by certain pips
        [Description( "Broken Support and closed below" )]
        BrokenAndClosedBelow     = 50,            // This is used for downtrend, pivot point is broken and close below it by certain pips
        [Description( "Broken Support and Recovered" )]
        BrokenAndRecoverAbove    = 55,            // This is used for downtrend, pivot point is broken and close below it by certain pips
        [Description( "Holding above Resistance" )]
        HoldingAbove             = 60,
        [Description( "Holding below" )]
        HoldingBelow             = 70,
        [Description( "Significantly broken" )]
        SignificantlyBroken      = 80
    }

    public class MatchedSRinfo : IEquatable<MatchedSRinfo>, IComparable, IComparable<MatchedSRinfo>
    {
        SBar _owningBar;
        private SRLineResponseType _relationship;
        private double _levelValue;
        private string _levelName;
        private TimeSpan _timePeriod;

        public string LevelName
        {
            get { return _levelName; }
            set
            {
                _levelName = value;
            }
        }


        public double LevelValue
        {
            get { return _levelValue; }
            set
            {
                _levelValue = value;
            }
        }


        public SRLineResponseType Relationship
        {
            get { return _relationship; }
            set
            {
                _relationship = value;
            }
        }


        public TimeSpan PivotTimeSpan
        {
            get { return _timePeriod; }
            set
            {
                _timePeriod = value;
            }
        }


        public SBar OwningBar
        {
            get { return _owningBar; }
            set
            {
                _owningBar = value;
            }
        }

        public MatchedSRinfo( SBar bar, TimeSpan timePeriod, string name, double level, SRLineResponseType type )
        {
            _owningBar = bar;
            _timePeriod = timePeriod;
            _levelName = name;
            _levelValue = level;
            _relationship = type;
        }

        public override bool Equals( object obj )
        {
            if ( obj is MatchedSRinfo )
            {
                return Equals( ( MatchedSRinfo ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( MatchedSRinfo first, MatchedSRinfo second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( MatchedSRinfo first, MatchedSRinfo second )
        {
            return !( first == second );
        }

        public bool Equals( MatchedSRinfo other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _relationship.Equals( other._relationship ) && _levelValue.Equals( other._levelValue ) && Equals( _levelName, other._levelName ) && _timePeriod.Equals( other._timePeriod );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ ( int ) _relationship;
                hashCode = ( hashCode * 53 ) ^ _levelValue.GetHashCode();
                if ( _levelName != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<string>.Default.GetHashCode( _levelName );
                }

                hashCode = ( hashCode * 53 ) ^ EqualityComparer<TimeSpan>.Default.GetHashCode( _timePeriod );
                return hashCode;
            }
        }

        public int CompareTo( object obj )
        {
            if ( obj == null )
            {
                return 1;
            }

            MatchedSRinfo other = obj as MatchedSRinfo;
            if ( other == null )
            {
                throw new ArgumentException( nameof( obj ) + " is not a " + nameof( MatchedSRinfo ) );
            }

            return CompareTo( other );
        }

        public int CompareTo( MatchedSRinfo other )
        {
            if ( other == null )
            {
                return 1;
            }

            int result = 0;
            result = _relationship.CompareTo( other._relationship );
            if ( result != 0 )
            {
                return result;
            }

            result = _levelValue.CompareTo( other._levelValue );
            if ( result != 0 )
            {
                return result;
            }

            result = _levelName.CompareTo( other._levelName );
            if ( result != 0 )
            {
                return result;
            }

            result = _timePeriod.CompareTo( other._timePeriod );
            if ( result != 0 )
            {
                return result;
            }

            return result;
        }
    }

    public interface IPeriodXTaManager
    {
        PooledList<WaveRotationInfo> GetWaveImportantTimeInfo( ref SBar bar );
        PooledList<GannPriceTimeInfo> GetGannPriceTimeInfo( ref SBar bar );
        PooledList<DivergenceInfo> GetDivergenceInfo( ref SBar bar );
        PooledList<MatchedSRinfo> GetPivotReltations( ref SBar bar );
    }
}
