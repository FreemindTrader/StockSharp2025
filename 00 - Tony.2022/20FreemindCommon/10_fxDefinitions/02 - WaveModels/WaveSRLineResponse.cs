//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace fx.Definitions
//{
//    public struct WaveSRLineResponse : IEquatable<WaveSRLineResponse>
//    {
//        public FibonacciType       FibType;
//        public SRLineResponseType  SRLineResponseType;
//        public FibPercentage       FibPrecentage;
//        public double              FibLevelStrengh;
//        public double              SRLineValue;
//        public TrendDirection      TrendDirection;
//        

//        public WaveSRLineResponse( TrendDirection trendDirection, double sRLineValue, FibonacciType fibType, SRLineResponseType sRLineResponse, FibPercentage fibPercentage )
//        {
//            FibType            = fibType;
//            SRLineResponseType = sRLineResponse;
//            FibPrecentage      = fibPercentage;
//            SRLineValue        = sRLineValue;
//            TrendDirection     = trendDirection;
//            FibLevelStrengh    = 0;
//        }

//        public WaveSRLineResponse( TrendDirection trendDirection, FibonacciType fibType, SRLineResponseType sRLineResponse, FibLevelInfo info )
//        {
//            FibType            = fibType;
//            SRLineResponseType = sRLineResponse;
//            FibPrecentage      = info.FibPrecentage;
//            SRLineValue        = info.FibLevel;
//            TrendDirection     = trendDirection;
//            FibLevelStrengh    = info.FibLevelStrengh;
//        }

//        public override string ToString()
//        {
//            string output = FibType.ToDescription( );

//            output += ", " + Math.Round( SRLineValue, 5 );

//            if ( FibType.isProjection() )
//            {
//                output += ", Projection = " + FibPrecentage.ToDescription();
//            }
//            else
//            {
//                output += ", Retracement = " + FibPrecentage.ToDescription();
//            }

//            output += ", Reaction = " + SRLineResponseType.ToDescription();

//            return output;
//        }

//        public override bool Equals( object obj )
//        {
//            if ( obj is WaveSRLineResponse )
//            {
//                return Equals( ( WaveSRLineResponse ) obj );
//            }

//            return base.Equals( obj );
//        }

//        public static bool operator ==( WaveSRLineResponse first, WaveSRLineResponse second )
//        {
//            return first.Equals( second );
//        }

//        public static bool operator !=( WaveSRLineResponse first, WaveSRLineResponse second )
//        {
//            return !( first == second );
//        }

//        public bool Equals( WaveSRLineResponse other )
//        {
//            return FibType.Equals( other.FibType ) && SRLineResponseType.Equals( other.SRLineResponseType ) && FibPrecentage.Equals( other.FibPrecentage ) && FibLevelStrengh.Equals( other.FibLevelStrengh ) && SRLineValue.Equals( other.SRLineValue ) && TrendDirection.Equals( other.TrendDirection );
//        }

//        public override int GetHashCode()
//        {
//            unchecked
//            {
//                int hashCode = 47;
//                hashCode = ( hashCode * 53 ) ^ ( int ) FibType;
//                hashCode = ( hashCode * 53 ) ^ ( int ) SRLineResponseType;
//                hashCode = ( hashCode * 53 ) ^ ( int ) FibPrecentage;
//                hashCode = ( hashCode * 53 ) ^ FibLevelStrengh.GetHashCode();
//                hashCode = ( hashCode * 53 ) ^ SRLineValue.GetHashCode();
//                hashCode = ( hashCode * 53 ) ^ ( int ) TrendDirection;
//                return hashCode;
//            }
//        }
//    }
//}
