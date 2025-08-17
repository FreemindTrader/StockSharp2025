using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.Spatial;
using fx.Definitions; 

namespace fx.Database
{    
    public class FxcmDatabar : IFxcm, IComparable< FxcmDatabar >, IEquatable< FxcmDatabar >
    {        
        private long _startDate;

        public long StartDate
        {
            get
            {
                return _startDate;
            }

            set
            {

                _startDate = value;
            }
        }
        
        public DateTime StartDateUTC
        {
            get
            {
                return _startDate.FromLinuxTime( );
            }

            set
            {
                if ( value.Kind == DateTimeKind.Utc )
                {
                    _startDate  = value.ToLinuxTime( );
                }
                else
                {
                    var utcTime = value.ToUniversalTime( );
                    _startDate  = utcTime.ToLinuxTime( );
                }
            }
        }


        public double AskClose { get; set; }


        public double AskHigh  { get; set; }


        public double AskLow   { get; set; }


        public double AskOpen  { get; set; }


        public double BidClose { get; set; }


        public double BidHigh  { get; set; }


        public double BidLow   { get; set; }


        public double BidOpen  { get; set; }

        public long   Volume   { get; set; }

        public FxcmDatabar( )
        {

        }

        public FxcmDatabar( long startDate, double askClose, double askHigh, double askLow, double askopen, double bidClose, double bidHigh, double bidLow, double bidOpen, long volume )            
        {
            StartDate = startDate;
            AskClose  = askClose;
            AskHigh   = askHigh;
            AskLow    = askLow;
            AskOpen   = askopen;
            BidClose  = bidClose;
            BidHigh   = bidHigh;
            BidLow    = bidLow;
            BidOpen   = bidOpen;
            Volume    = volume;
        }
        public override string ToString( )
        {
            return base.ToString( ) + "[StartDateUTC: " + StartDateUTC + " ,AskOpen: " + AskOpen + " AskHigh: " + AskHigh + " ,AskLow: " + AskLow + " ,AskClose: " + AskClose + " BidOpen: " + BidOpen + " ,BidHigh: " + BidHigh + " ,BidLow: " + BidLow + " BidClose: " + BidClose + " ,Volume: " + Volume + "]";
        }
        
        public int CompareTo( FxcmDatabar other )
        {
            int result = StartDate.CompareTo( other.StartDate );
            if ( result != 0 )
            {
                return result;
            }

            // To When we compare two databar to see which is 

            result = AskOpen.CompareTo( other.AskOpen );
            if ( result != 0 )
            {
                return result;
            }

            result = BidOpen.CompareTo( other.BidOpen );
            if ( result != 0 )
            {
                return result;
            }

            result = AskClose.CompareTo( other.AskClose );
            if ( result != 0 )
            {
                return result;
            }

            result = BidClose.CompareTo( other.BidClose );
            if ( result != 0 )
            {
                return result;
            }

            result = AskHigh.CompareTo( other.AskHigh );
            if ( result != 0 )
            {
                return result;
            }

            result = BidHigh.CompareTo( other.BidHigh );
            if ( result != 0 )
            {
                return result;
            }

            result = AskLow.CompareTo( other.AskLow );
            if ( result != 0 )
            {
                return result;
            }

            result = BidLow.CompareTo( other.BidLow );
            if ( result != 0 )
            {
                return result;
            }

            return Volume.CompareTo( other.Volume );
        }

        public override bool Equals( object obj )
        {
            if ( !( obj is FxcmDatabar ) )
            {
                return false;
            }

            FxcmDatabar other = ( FxcmDatabar ) obj;
            return Equals( other );
        }

        public bool Equals( FxcmDatabar other )
        {
            return  StartDate == other.StartDate   &&
                    AskClose  == other.AskClose    &&
                    AskHigh   == other.AskHigh     &&
                    AskLow    == other.AskLow      &&
                    AskOpen   == other.AskOpen     &&
                    BidClose  == other.BidClose    &&
                    BidHigh   == other.BidHigh     &&
                    BidLow    == other.BidLow      &&
                    BidOpen   == other.BidOpen     &&
                    Volume    == other.Volume;
        }

        public override int GetHashCode()
        {
            return StartDate.GetHashCode();
        }
    }
}
