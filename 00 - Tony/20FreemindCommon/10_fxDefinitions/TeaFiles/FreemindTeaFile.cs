using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fx.Definitions
{
    public class DatabarDB : IEquatable<DatabarDB>,
                             IComparable,
                             IComparable<DatabarDB>
    {
        public int CompareTo( object obj )
        {
            if ( obj == null )
                return 1;
            DatabarDB other = obj as DatabarDB;
            if ( other == null )
                throw new ArgumentException( "obj is not a DatabarDB" );
            return CompareTo( other );
        }

        public int CompareTo( DatabarDB other )
        {
            if ( other == null )
                return 1;
            int result = 0;
            result = BarTime.CompareTo( other.BarTime );
            if ( result != 0 )
                return result;
            result = Open.CompareTo( other.Open );
            if ( result != 0 )
                return result;
            result = High.CompareTo( other.High );
            if ( result != 0 )
                return result;
            result = Low.CompareTo( other.Low );
            if ( result != 0 )
                return result;
            result = Close.CompareTo( other.Close );
            if ( result != 0 )
                return result;
            result = Volume.CompareTo( other.Volume );
            if ( result != 0 )
                return result;
            result = Macd.CompareTo( other.Macd );
            if ( result != 0 )
                return result;
            result = MacdSignal.CompareTo( other.MacdSignal );
            if ( result != 0 )
                return result;
            result = K.CompareTo( other.K );
            if ( result != 0 )
                return result;
            result = D.CompareTo( other.D );
            if ( result != 0 )
                return result;
            result = Sma.CompareTo( other.Sma );
            if ( result != 0 )
                return result;
            result = BBMean.CompareTo( other.BBMean );
            if ( result != 0 )
                return result;
            result = InnerBBUpper.CompareTo( other.InnerBBUpper );
            if ( result != 0 )
                return result;
            result = InnerBBLower.CompareTo( other.InnerBBLower );
            if ( result != 0 )
                return result;
            result = OuterBBUpper.CompareTo( other.OuterBBUpper );
            if ( result != 0 )
                return result;
            result = OuterBBLower.CompareTo( other.OuterBBLower );
            if ( result != 0 )
                return result;
            return result;
        }

        public override bool Equals( object obj )
        {
            if ( obj is DatabarDB )
                return Equals( ( DatabarDB ) obj );
            return base.Equals( obj );
        }

        public static bool operator ==( DatabarDB first,
                                        DatabarDB second )
        {
            if ( ( object ) first == null )
                return ( object ) second == null;
            return first.Equals( second );
        }

        public static bool operator !=( DatabarDB first,
                                        DatabarDB second )
        {
            return !( first == second );
        }

        public bool Equals( DatabarDB other )
        {
            if ( ReferenceEquals( null, other ) )
                return false;
            if ( ReferenceEquals( this, other ) )
                return true;
            return BarTime.Equals( other.BarTime ) && Open.Equals( other.Open ) && High.Equals( other.High ) && Low.Equals( other.Low ) && Close.Equals( other.Close ) && Volume.Equals( other.Volume ) && Macd.Equals( other.Macd ) && MacdSignal.Equals( other.MacdSignal ) && K.Equals( other.K ) && D.Equals( other.D ) && Sma.Equals( other.Sma ) && BBMean.Equals( other.BBMean ) && InnerBBUpper.Equals( other.InnerBBUpper ) && InnerBBLower.Equals( other.InnerBBLower ) && OuterBBUpper.Equals( other.OuterBBUpper ) && OuterBBLower.Equals( other.OuterBBLower );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ BarTime.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ Open.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ High.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ Low.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ Close.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ Volume.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ Macd.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ MacdSignal.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ K.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ D.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ Sma.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ BBMean.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ InnerBBUpper.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ InnerBBLower.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ OuterBBUpper.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ OuterBBLower.GetHashCode( );
                return hashCode;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaBars"/> class.
        /// </summary>
        /// <param name="barTime"></param>
        /// <param name="open"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        /// <param name="close"></param>
        /// <param name="volume"></param>
        public DatabarDB( DateTime barTime,
                          double open,
                          double high,
                          double low,
                          double close,
                          int volume )
        {
            BarTime = barTime;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            Macd = 0d;
            MacdSignal = 0d;
            K = 0d;
            D = 0d;
            Sma = 0d;
            BBMean = 0d;
            InnerBBUpper = 0d;
            InnerBBLower = 0d;
            OuterBBUpper = 0d;
            OuterBBLower = 0d;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabarDB"/> class.
        /// </summary>
        public DatabarDB( )
        {
            BarTime = DateTime.MinValue;
            Open = 0d;
            High = 0d;
            Low = 0d;
            Close = 0d;
            Volume = 0;
            Macd = 0d;
            MacdSignal = 0d;
            K = 0d;
            D = 0d;
            Sma = 0d;
            BBMean = 0d;
            InnerBBUpper = 0d;
            InnerBBLower = 0d;
            OuterBBUpper = 0d;
            OuterBBLower = 0d;
        }

        public DateTime BarTime { get; set; }

        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Close { get; set; }

        public int Volume { get; set; }

        public double Macd { get; set; }

        public double MacdSignal { get; set; }

        public double K { get; set; }

        public double D { get; set; }

        public double Sma { get; set; }

        public double BBMean { get; set; }

        public double InnerBBUpper { get; set; }

        public double InnerBBLower { get; set; }

        public double OuterBBUpper { get; set; }

        public double OuterBBLower { get; set; }
    }

    public class TaSignalBar
    {
        public DateTime BarTime { get; set; }

        public int Signal { get; set; }

        public long HewBits { get; set; }

        public long HewExtraBits { get; set; }

        public int CandleSticks { get; set; }

        public int WaveImportance { get; set; }

        public int GannImportance { get; set; }
    }
}
