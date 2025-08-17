using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public class IndicatorsCrossEventArgs : EventArgs, IEquatable<IndicatorsCrossEventArgs>
    {
        protected int             _barCount;
        protected string          _security;
        protected int             _crossIndex;
        protected MarketDirection _direction;
        protected DateTime        _crossTime;
        protected TimeSpan        _period;

        public IndicatorsCrossEventArgs( string security, TimeSpan period, DateTime crossTime, int crossIndex, MarketDirection direction, int barCount )
        {
            _security   = security;
            _crossIndex = crossIndex;
            _direction  = direction;
            _crossTime  = crossTime;
            _period     = period;
            _barCount   = barCount;
        }

        
        public string Security
        {
            get => _security;
            set => _security = value;
        }

        
        public DateTime CrossTime
        {
            get => _crossTime;
            set => _crossTime = value;
        }

        

        public TimeSpan Period
        {
            get => _period;
            set => _period = value;
        }


        public MarketDirection Direction
        {
            get => _direction;
            set => _direction = value;
        }


        public int CrossIndex
        {
            get => _crossIndex;
            set => _crossIndex = value;
        }


        public int BarCount
        {
            get => _barCount;
            set => _barCount = value;
        }

        public override bool Equals( object obj )
        {
            if ( obj is IndicatorsCrossEventArgs )
            {
                return Equals( ( IndicatorsCrossEventArgs ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( IndicatorsCrossEventArgs first, IndicatorsCrossEventArgs second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( IndicatorsCrossEventArgs first, IndicatorsCrossEventArgs second )
        {
            return !( first == second );
        }

        public bool Equals( IndicatorsCrossEventArgs other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _security.Equals( other._security ) && _period.Equals( other._period ) && _crossIndex.Equals( other._crossIndex ) && _direction.Equals( other._direction );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _barCount.GetHashCode( );
                if ( _security != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<string>.Default.GetHashCode( _security );
                }

                hashCode = ( hashCode * 53 ) ^ _crossIndex.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ ( int ) _direction;
                hashCode = ( hashCode * 53 ) ^ _crossTime.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<TimeSpan>.Default.GetHashCode( _period );
                return hashCode;
            }
        }
    }

    public class SmaCrossEventArgs : IndicatorsCrossEventArgs, IEquatable<SmaCrossEventArgs>
    {
        public SmaCrossEventArgs( string security, TimeSpan period, DateTime crossTime, int crossIndex, MarketDirection direction, int barCount, double sma55, int aboveCount, int belowCount ) : base( security, period, crossTime, crossIndex, direction, barCount )
        {
            SmaAboveCount = aboveCount;
            SmaBelowCount = belowCount;
            Sma55Value = sma55;
        }


        double _sma55Value;
        int _smaBelowCount;
        int _smaAboveCount;

        public int SmaAboveCount
        {
            get => _smaAboveCount;
            set => _smaAboveCount = value;
        }


        public int SmaBelowCount
        {
            get => _smaBelowCount;
            set => _smaBelowCount = value;
        }

        
        public double Sma55Value
        {
            get => _sma55Value;
            set => _sma55Value = value;
        }

        public override bool Equals( object obj )
        {
            if ( obj is SmaCrossEventArgs )
            {
                return Equals( ( SmaCrossEventArgs ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( SmaCrossEventArgs first, SmaCrossEventArgs second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( SmaCrossEventArgs first, SmaCrossEventArgs second )
        {
            return !( first == second );
        }

        public bool Equals( SmaCrossEventArgs other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return base.Equals( other );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _sma55Value.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _smaBelowCount.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _smaAboveCount.GetHashCode();
                return hashCode;
            }
        }
    }

    public class EmaCrossEventArgs : IndicatorsCrossEventArgs, IEquatable<EmaCrossEventArgs>
    {
        public EmaCrossEventArgs( string security, TimeSpan period, DateTime crossTime, int crossIndex, MarketDirection direction, int barCount, double ema5, double ema13, double ema144 ) : base( security, period, crossTime, crossIndex, direction, barCount )
        {
            Ema5   = ema5;
            Ema13  = ema13;
            Ema144 = ema144;
        }


        double _ema144;
        double _ema13;
        double _ema5;

        public double Ema5
        {
            get => _ema5;
            set => _ema5 = value;
        }


        public double Ema13
        {
            get => _ema13;
            set => _ema13 = value;
        }

        
        public double Ema144
        {
            get => _ema144;
            set => _ema144 = value;
        }

        public override bool Equals( object obj )
        {
            if ( obj is EmaCrossEventArgs )
            {
                return Equals( ( EmaCrossEventArgs ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( EmaCrossEventArgs first, EmaCrossEventArgs second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( EmaCrossEventArgs first, EmaCrossEventArgs second )
        {
            return !( first == second );
        }

        public bool Equals( EmaCrossEventArgs other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return base.Equals( other );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _ema144.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _ema13.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _ema5.GetHashCode();
                return hashCode;
            }
        }
    }

    public class OscillatorCrossEventArgs : IndicatorsCrossEventArgs, IEquatable<OscillatorCrossEventArgs>
    {
        public OscillatorCrossEventArgs( string security, TimeSpan period, DateTime crossTime, int crossIndex, TASignal signal, MarketDirection direction, int exitOB, int exitOS, int barCount, double kValue, double dValue ) : base( security, period, crossTime, crossIndex, direction, barCount )
        {
            TaSignal = signal;
            KValue   = kValue;
            DValue   = dValue;
            ExitOverBoughtIndex = exitOB;
            ExitOverSoldIndex = exitOS;
        }

        int _exitOverBoughtIndex;
        int _exitOverSoldIndex;        
        TASignal _lastOscillatorSignal;
        double _dValue;
        double _kValue;


        
        public int ExitOverBoughtIndex
        {
            get => _exitOverBoughtIndex;
            set => _exitOverBoughtIndex = value;
        }
        


        public int ExitOverSoldIndex
        {
            get => _exitOverSoldIndex;
            set => _exitOverSoldIndex = value;
        }
        

        public TASignal TaSignal
        {
            get => _lastOscillatorSignal;
            set => _lastOscillatorSignal = value;
        }

        public double KValue
        {
            get => _kValue;
            set => _kValue = value;
        }

        
        public double DValue
        {
            get => _dValue;
            set => _dValue = value;
        }

        public override bool Equals( object obj )
        {
            if ( obj is OscillatorCrossEventArgs )
            {
                return Equals( ( OscillatorCrossEventArgs ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( OscillatorCrossEventArgs first, OscillatorCrossEventArgs second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( OscillatorCrossEventArgs first, OscillatorCrossEventArgs second )
        {
            return !( first == second );
        }

        public bool Equals( OscillatorCrossEventArgs other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _lastOscillatorSignal.Equals( other._lastOscillatorSignal ) &&
                   _exitOverSoldIndex.Equals( other._exitOverSoldIndex ) &&
                   _exitOverBoughtIndex.Equals( other._exitOverBoughtIndex );
                   
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _dValue.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ _kValue.GetHashCode();
                return hashCode;
            }
        }
    }

    public class MacdCrossEventArgs : IndicatorsCrossEventArgs, IEquatable<MacdCrossEventArgs>
    {
        public MacdCrossEventArgs( string security, TimeSpan period, DateTime crossTime, int crossIndex, MarketDirection direction, int barCount, double fastMacd, double slowMacd ) : base( security, period, crossTime, crossIndex, direction, barCount )
        {
            FastMacd = fastMacd;
            SlowMacd = slowMacd;
        }

        double _slowMacd;
        double _fastMacd;

        public double FastMacd
        {
            get => _fastMacd;
            set => _fastMacd = value;
        }


        public double SlowMacd
        {
            get => _slowMacd;
            set => _slowMacd = value;
        }

        public override bool Equals( object obj )
        {
            if ( obj is MacdCrossEventArgs )
            {
                return Equals( ( MacdCrossEventArgs ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( MacdCrossEventArgs first, MacdCrossEventArgs second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( MacdCrossEventArgs first, MacdCrossEventArgs second )
        {
            return !( first == second );
        }

        public bool Equals( MacdCrossEventArgs other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return base.Equals( other );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _slowMacd.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ _fastMacd.GetHashCode( );
                return hashCode;
            }
        }
    }

    public class TaValueEventArgs: EventArgs
    {
        public TaValueEventArgs( string security, TimeSpan period, double taValue, DateTime valueTime )
        {
            Security  = security;
            Period    = period;
            TaValue   = taValue;
            ValueTime = valueTime;
        }

        DateTime _valueTime;
        double _taValue;
        string _security;

        public string Security
        {
            get => _security;
            set => _security = value;
        }

        TimeSpan _period;

        public TimeSpan Period
        {
            get => _period;
            set => _period = value;
        }


        public double TaValue
        {
            get => _taValue;
            set => _taValue = value;
        }

        
        public DateTime ValueTime
        {
            get => _valueTime;
            set => _valueTime = value;
        }
        
    }

    public class MacdValueEventArgs : EventArgs
    {
        public MacdValueEventArgs( string security, TimeSpan period, (double macd, double macdSig) taValue, DateTime valueTime )
        {
            Security   = security;
            Period     = period;
            Macd       = taValue.macd;
            MacdSignal = taValue.macdSig;
            ValueTime  = valueTime;
        }

        DateTime _valueTime;
        double _macd;
        double _macdSignal;
        string _security;

        public string Security
        {
            get => _security;
            set => _security = value;
        }

        TimeSpan _period;

        public TimeSpan Period
        {
            get => _period;
            set => _period = value;
        }


        public double Macd
        {
            get => _macd;
            set => _macd = value;
        }

        public double MacdSignal
        {
            get => _macdSignal;
            set => _macdSignal = value;
        }

        public DateTime ValueTime
        {
            get => _valueTime;
            set => _valueTime = value;
        }

    }
}
