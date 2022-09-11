#if DEFINE_SQL
CREATE TABLE [DBClosedTrade] (
[Id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
[AccountID]             NVARCHAR(64)    NOT NULL,
[AccountKind]           NVARCHAR(64)    NOT NULL,
[AccountName]           NVARCHAR(64)    NOT NULL,
[Amount]                INTEGER         NOT NULL,
[BuySell]               NVARCHAR(8)     NOT NULL,
[OrderID]               NVARCHAR(64)    NOT NULL,
[RequestID]             NVARCHAR(64)    NOT NULL,
[Commission]            FLOAT           NULL,
[OfferID]               NVARCHAR(64)    NOT NULL,
[OpenOrderID]           NVARCHAR(64)    NOT NULL,
[OpenOrderParties]      NVARCHAR(128)   NOT NULL,
[OpenOrderReqID]        NVARCHAR(64)    NOT NULL,
[OpenOrderRequestTXT]   NVARCHAR(128)   NOT NULL,
[OpenQuoteID]           NVARCHAR(64)    NOT NULL,
[OpenRate]              FLOAT           NOT NULL,
[OpenTime]              DATETIME        NOT NULL,
[RolloverInterest]      FLOAT           NOT NULL,
[TradeID]               NVARCHAR(64)    NOT NULL,
[TradeIDOrigin]         NVARCHAR(64)    NOT NULL,
[CloseOrderID]          NVARCHAR(64)    NOT NULL,
[CloseOrderParties]     NVARCHAR(128)   NOT NULL,
[CloseOrderReqID]       NVARCHAR(64)    NOT NULL,
[CloseOrderRequestTXT]  NVARCHAR(128)   NOT NULL,
[CloseQuoteID]          NVARCHAR(64)    NOT NULL,
[CloseRate]             FLOAT           NOT NULL,
[CloseTime]             DATETIME        NOT NULL,
[GrossPL]               FLOAT           NOT NULL,
[TradeIDRemain]         NVARCHAR(128)    NOT NULL
)
#endif
namespace fx.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using fx.Definitions;

    public partial class DbClosedTrade : IFxcm, IEquatable< DbClosedTrade >
    {
        public bool Equals( DbClosedTrade other )
        {
            if ( ReferenceEquals( null, other ) ) return false;
            if ( ReferenceEquals( this, other ) ) return true;
            return string.Equals( AccountID, other.AccountID ) && string.Equals( AccountKind, other.AccountKind ) && string.Equals( AccountName, other.AccountName ) && Amount == other.Amount && string.Equals( BuySell, other.BuySell ) && Commission.Equals( other.Commission ) && string.Equals( OfferID, other.OfferID ) && string.Equals( OpenOrderID, other.OpenOrderID ) && string.Equals( OpenOrderParties, other.OpenOrderParties ) && string.Equals( OpenOrderReqID, other.OpenOrderReqID ) && string.Equals( OpenOrderRequestTXT, other.OpenOrderRequestTXT ) && string.Equals( OpenQuoteID, other.OpenQuoteID ) && OpenRate.Equals( other.OpenRate ) && OpenTime == other.OpenTime && RolloverInterest.Equals( other.RolloverInterest ) && string.Equals( TradeID, other.TradeID ) && string.Equals( TradeIDOrigin, other.TradeIDOrigin ) && string.Equals( CloseOrderID, other.CloseOrderID ) && string.Equals( CloseOrderParties, other.CloseOrderParties ) && string.Equals( CloseOrderReqID, other.CloseOrderReqID ) && string.Equals( CloseOrderRequestTXT, other.CloseOrderRequestTXT ) && string.Equals( CloseQuoteID, other.CloseQuoteID ) && CloseRate.Equals( other.CloseRate ) && CloseTime == other.CloseTime && GrossPL.Equals( other.GrossPL ) && string.Equals( TradeIDRemain, other.TradeIDRemain );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            if ( ReferenceEquals( this, obj ) ) return true;
            if ( obj.GetType( ) != GetType( ) ) return false;
            return Equals( ( DbClosedTrade ) obj );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                var hashCode = ( AccountID != null ? AccountID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( AccountKind != null ? AccountKind.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( AccountName != null ? AccountName.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ Amount;
                hashCode = ( hashCode*397 ) ^ ( BuySell != null ? BuySell.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ Commission.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ ( OfferID != null ? OfferID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( OpenOrderID != null ? OpenOrderID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( OpenOrderParties != null ? OpenOrderParties.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( OpenOrderReqID != null ? OpenOrderReqID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( OpenOrderRequestTXT != null ? OpenOrderRequestTXT.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( OpenQuoteID != null ? OpenQuoteID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ OpenRate.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ OpenTime.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ RolloverInterest.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ ( TradeID != null ? TradeID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( TradeIDOrigin != null ? TradeIDOrigin.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( CloseOrderID != null ? CloseOrderID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( CloseOrderParties != null ? CloseOrderParties.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( CloseOrderReqID != null ? CloseOrderReqID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( CloseOrderRequestTXT != null ? CloseOrderRequestTXT.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( CloseQuoteID != null ? CloseQuoteID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ CloseRate.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ CloseTime.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ GrossPL.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ ( TradeIDRemain != null ? TradeIDRemain.GetHashCode( ) : 0 );
                return hashCode;
            }
        }

        public static bool operator == ( DbClosedTrade left, DbClosedTrade right)
        {
            // If we used == to check for null instead of Object.ReferenceEquals(), we'd
            // get a StackOverflowException. Can you figure out why?
            if ( ReferenceEquals( left, null ) )
                return false;
            else
                return left.Equals( right );
        }

        public static bool operator !=( DbClosedTrade left, DbClosedTrade  right)
        {
            // Since we've already defined ==, we can just invert it for !=.
            return !( left == right );
        }

        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public long Id                      { get; set; }     

        public long StartDate               { get; set; }

        public string AccountID             { get; set; }
        public string AccountKind           { get; set; }
        public string  AccountName          { get; set; }
        public int  Amount                  { get; set; }
        public string  BuySell              { get; set; }

        public double  Commission           { get; set; }

        public string  OfferID              { get; set; }

        public string  OpenOrderID          { get; set; }

        public string  OpenOrderParties     { get; set; }

        public string  OpenOrderReqID       { get; set; }

        public string  OpenOrderRequestTXT  { get; set; } 

        public string  OpenQuoteID          { get; set; }

        public double  OpenRate             { get; set; }
        public long OpenTime                { get; set; }

        public double  RolloverInterest     { get; set; }

        public string  TradeID              { get; set; }

        public string  TradeIDOrigin        { get; set; }        

        public string  CloseOrderID         { get; set; }

        public string  CloseOrderParties    { get; set; }

        public string  CloseOrderReqID      { get; set; }

        public string  CloseOrderRequestTXT { get; set; }

        public string  CloseQuoteID         { get; set; } 

        public double  CloseRate            { get; set; }
        public long  CloseTime              { get; set; }

        public double  GrossPL              { get; set; }    
        
        public string  TradeIDRemain        { get; set; }    

        public DbClosedTrade( )
        {

        }

        public DbClosedTrade( DbClosedTrade other )
        {
            Id                   = other.Id;
            AccountID            = other.AccountID;
            AccountKind          = other.AccountKind;
            AccountName          = other.AccountName;
            Amount               = other.Amount;
            BuySell              = other.BuySell;
            Commission           = other.Commission;
            OfferID              = other.OfferID;
            OpenOrderID          = other.OpenOrderID;
            OpenOrderParties     = other.OpenOrderParties;
            OpenOrderReqID       = other.OpenOrderReqID;
            OpenOrderRequestTXT  = other.OpenOrderRequestTXT;
            OpenQuoteID          = other.OpenQuoteID;
            OpenRate             = other.OpenRate;
            OpenTime             = other.OpenTime;
            RolloverInterest     = other.RolloverInterest;
            TradeID              = other.TradeID;
            TradeIDOrigin        = other.TradeIDOrigin;            
            CloseOrderID         = other.CloseOrderID;
            CloseOrderParties    = other.CloseOrderParties;
            CloseOrderReqID      = other.CloseOrderReqID;
            CloseOrderRequestTXT = other.CloseOrderRequestTXT;
            CloseQuoteID         = other.CloseQuoteID;
            CloseRate            = other.CloseRate;
            CloseTime            = other.CloseTime;
            GrossPL              = other.GrossPL;
            TradeIDRemain        = other.TradeIDRemain;              
        }


        public DbClosedTrade(   string accountID,
                               string accountKind,
                               string accountName,
                               int amount,
                               string buySell,
                               double commission,
                               string offerID,
                               string openOrderID,
                               string openOrderParties,
                               string openOrderReqID,
                               string openOrderRequestTXT,
                               string openQuoteID,
                               double openRate,
                               DateTime openTime,
                               double rolloverInterest,
                               string tradeID,                            
                               string tradeIDOrigin, 
                               //double usedMargin,
                               string closeOrderID,
                               string closeOrderParties,
                               string closeOrderReqID,
                               string closeOrderRequestTXT,
                               string closeQuoteID,
                               double closeRate,
                               DateTime closeTime,
                               double grossPL,
                               string tradeIDRemain

        )
        {
            AccountID            = accountID;
            AccountKind          = accountKind;
            AccountName          = accountName;
            Amount               = amount;
            BuySell              = buySell;
            Commission           = commission;
            OfferID              = offerID;
            OpenOrderID          = openOrderID;
            OpenOrderParties     = openOrderParties;
            OpenOrderReqID       = openOrderReqID;
            OpenOrderRequestTXT  = openOrderRequestTXT;
            OpenQuoteID          = openQuoteID;
            OpenRate             = openRate;
            OpenTime             = openTime.ToLinuxTime();;
            RolloverInterest     = rolloverInterest;
            TradeID              = tradeID;
            TradeIDOrigin        = tradeIDOrigin;
            //UsedMargin           = usedMargin;
            CloseOrderID         = closeOrderID;
            CloseOrderParties    = closeOrderParties;
            CloseOrderReqID      = closeOrderReqID;
            CloseOrderRequestTXT = closeOrderRequestTXT;
            CloseQuoteID         = closeQuoteID;
            CloseRate            = closeRate;
            CloseTime            = closeTime.ToLinuxTime();;
            GrossPL              = grossPL;
            TradeIDRemain        = tradeIDRemain;

               
        }
        
        
        public int CompareTo( DbClosedTrade other )
        {
            int result  = AccountID.CompareTo( other.AccountID );if ( result != 0 ){return result;}

            result      = OpenOrderID.CompareTo( other.OpenOrderID );

            if ( result != 0 )
            {
                return result;
            }

            result      = TradeID.CompareTo( other.TradeID );

            if ( result != 0 )
            {
                return result;
            }

            result      = Amount.CompareTo( other.Amount );

            if ( result != 0 )
            {
                return result;
            }

            result      = RolloverInterest.CompareTo( other.RolloverInterest );

            if ( result != 0 )
            {
                return result;
            }
            
            result      = CloseOrderID.CompareTo( other.CloseOrderID );

            if ( result != 0 )
            {
                return result;
            }             

            result      = GrossPL.CompareTo( other.GrossPL );

            if ( result != 0 )
            {
                return result;
            }

            // -----------------------------------------------------------------------------
            // The followings are something that I consider won't change.

            result  = AccountKind.CompareTo( other.AccountKind );

            if ( result != 0 )
            {
                return result;
            }

            result  = AccountName.CompareTo( other.AccountName );

            if ( result != 0 )
            {
                return result;
            }

            result  = BuySell.CompareTo( other.BuySell );

            if ( result != 0 )
            {
                return result;
            }

            result  = Commission.CompareTo( other.Commission );

            if ( result != 0 )
            {
                return result;
            }

            result  = OfferID.CompareTo( other.OfferID );

            if ( result != 0 )
            {
                return result;
            }

            result  = OpenOrderParties.CompareTo( other.OpenOrderParties );

            if ( result != 0 )
            {
                return result;
            }

            result  = OpenOrderReqID.CompareTo( other.OpenOrderReqID );

            if ( result != 0 )
            {
                return result;
            }

            result  = OpenOrderRequestTXT.CompareTo( other.OpenOrderRequestTXT );

            if ( result != 0 )
            {
                return result;
            }

            result  = OpenQuoteID.CompareTo( other.OpenQuoteID );

            if ( result != 0 )
            {
                return result;
            }

            result  = OpenRate.CompareTo( other.OpenRate );

            if ( result != 0 )
            {
                return result;
            }

            result  = OpenTime.CompareTo( other.OpenTime );

            if ( result != 0 )
            {
                return result;
            }

            result  = TradeIDOrigin.CompareTo( other.TradeIDOrigin );

            if ( result != 0 )
            {
                return result;
            }


            result  = CloseOrderParties.CompareTo( other.CloseOrderParties );

            if ( result != 0 )
            {
                return result;
            }

            result  = CloseOrderReqID.CompareTo( other.CloseOrderReqID );

            if ( result != 0 )
            {
                return result;
            }

            result  = CloseOrderRequestTXT.CompareTo( other.CloseOrderRequestTXT );

            if ( result != 0 )
            {
                return result;
            }

            result  = CloseQuoteID.CompareTo( other.CloseQuoteID );

            if ( result != 0 )
            {
                return result;
            }

            result  = CloseRate.CompareTo( other.CloseRate );

            if ( result != 0 )
            {
                return result;
            }

            result  = OpenTime.CompareTo( other.OpenTime );

            if ( result != 0 )
            {
                return result;
            }

            return TradeIDRemain.CompareTo( other.TradeIDRemain );
        }
        

        //public DBClosedTrade( FxClosedTrade other )            
        //{
        //    Id                   = 0;
        //    AccountID            = other.AccountID;
        //    AccountKind          = other.AccountKind;
        //    AccountName          = other.AccountName;
        //    Amount               = other.Amount;
        //    BuySell              = other.BuySell;
        //    Commission           = other.Commission;
        //    OfferID              = other.OfferID;
        //    OpenOrderID          = other.OpenOrderID;
        //    OpenOrderParties     = other.OpenOrderParties;
        //    OpenOrderReqID       = other.OpenOrderReqID;
        //    OpenOrderRequestTXT  = other.OpenOrderRequestTXT;
        //    OpenQuoteID          = other.OpenQuoteID;
        //    OpenRate             = other.OpenRate;
        //    OpenTime             = other.OpenTime.ToLinuxTime();
        //    RolloverInterest     = other.RolloverInterest;
        //    TradeID              = other.TradeID;
        //    TradeIDOrigin        = other.TradeIDOrigin;            
        //    CloseOrderID         = other.CloseOrderID;
        //    CloseOrderParties    = other.CloseOrderParties;
        //    CloseOrderReqID      = other.CloseOrderReqID;
        //    CloseOrderRequestTXT = other.CloseOrderRequestTXT;
        //    CloseQuoteID         = other.CloseQuoteID;
        //    CloseRate            = other.CloseRate;
        //    CloseTime            = other.CloseTime.ToLinuxTime();;
        //    GrossPL              = other.GrossPL;
        //    TradeIDRemain        = other.TradeIDRemain;                       
        //}        
    }
}
