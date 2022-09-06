using fxcore2;
using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public class FxDetailedPosition : IDetailedPosition, IEquatable<FxDetailedPosition>
    {
        public string MainLoginName { get; }
        public string AccountID { get; }
        public string AccountKind { get; }
        public string AccountName { get; }
        public int Amount { get; }
        public string BuySell { get; }
        public double Commission { get; }


        public bool IsBuy
        {
            get { return BuySell == "B"; }
        }





        public static bool operator ==( FxDetailedPosition left, FxDetailedPosition right )
        {
            // If we used == to check for null instead of Object.ReferenceEquals(), we'd
            // get a StackOverflowException. Can you figure out why?
            if ( ReferenceEquals( left, null ) )
                return false;
            else
                return left.Equals( right );
        }

        public static bool operator !=( FxDetailedPosition left, FxDetailedPosition right )
        {
            // Since we've already defined ==, we can just invert it for !=.
            return !( left == right );
        }

        public string OfferID { get; }

        public string OpenOrderID { get; }

        public string OpenOrderParties { get; }

        public string OpenOrderReqID { get; }

        public string OpenOrderRequestTXT { get; }

        public string OpenQuoteID { get; }

        public double OpenRate { get; }
        public DateTime OpenTime { get; }

        public double RolloverInterest { get; }

        public string TradeID { get; }

        public string ValueDate { get; }


        public string TradeIDOrigin { get; }

        public double Dividends { get; }

        public FxDetailedPosition(
                                   string mainLoginName,
                                   string accountId,
                                   string accountKind,
                                   string accountName,
                                   int amount,
                                   string buySell,
                                   double commission,
                                   string offerId,
                                   string openOrderId,
                                   string openOrderParties,
                                   string openOrderReqId,
                                   string openOrderRequestTxt,
                                   string openQuoteId,
                                   double openRate,
                                   DateTime openTime,
                                   double rolloverInterest,
                                   string tradeId,
                                   string tradeIdOrigin,
                                   string valueDate,
                                   double dividends
        )
        {
            MainLoginName = mainLoginName;
            AccountID = accountId;
            AccountKind = accountKind;
            AccountName = accountName;
            Amount = amount;
            BuySell = buySell;

            Commission = commission;

            OfferID = offerId;

            OpenOrderID = openOrderId;

            OpenOrderParties = openOrderParties;

            OpenOrderReqID = openOrderReqId;

            OpenOrderRequestTXT = openOrderRequestTxt;

            OpenQuoteID = openQuoteId;

            OpenRate = openRate;
            OpenTime = openTime;

            RolloverInterest = rolloverInterest;

            TradeID = tradeId;

            TradeIDOrigin = tradeIdOrigin;

            Dividends = dividends;
        }

        public FxDetailedPosition( string mainLoginName, O2GTradeRow row )
        {
            MainLoginName       = mainLoginName;
            AccountID           = row.AccountID;
            AccountKind         = row.AccountKind;
            AccountName         = row.AccountName;
            Amount              = row.Amount;
            BuySell             = row.BuySell;

            Commission          = row.Commission;

            OfferID             = row.OfferID;

            OpenOrderID         = row.OpenOrderID;

            OpenOrderParties    = row.Parties;

            OpenOrderReqID      = row.OpenOrderReqID;

            OpenOrderRequestTXT = row.OpenOrderRequestTXT;

            OpenQuoteID         = row.OpenOrderReqID;

            OpenRate            = row.OpenRate;
            OpenTime            = row.OpenTime;

            RolloverInterest    = row.RolloverInterest;

            TradeID             = row.TradeID;

            TradeIDOrigin       = row.TradeIDOrigin;

            Dividends           = row.Dividends;
        }

        public IDetailedPosition Clone( )
        {
            return ( new FxDetailedPosition(
                                         MainLoginName,
                                         AccountID,
                                         AccountKind,
                                         AccountName,
                                         Amount,
                                         BuySell,
                                         Commission,
                                         OfferID,
                                         OpenOrderID,
                                         OpenOrderParties,
                                         OpenOrderReqID,
                                         OpenOrderRequestTXT,
                                         OpenQuoteID,
                                         OpenRate,
                                         OpenTime,
                                         RolloverInterest,
                                         TradeID,
                                         TradeIDOrigin,
                                         ValueDate,
                                         Dividends
                                       )
                    );
        }

        public bool Equals( FxDetailedPosition other )
        {
            if ( ReferenceEquals( null, other ) ) return false;
            if ( ReferenceEquals( this, other ) ) return true;
            return string.Equals( AccountID, other.AccountID ) && string.Equals( AccountKind, other.AccountKind ) && string.Equals( AccountName, other.AccountName ) && Amount == other.Amount && string.Equals( BuySell, other.BuySell ) && Commission.Equals( other.Commission ) && string.Equals( OfferID, other.OfferID ) && string.Equals( OpenOrderID, other.OpenOrderID ) && string.Equals( OpenOrderParties, other.OpenOrderParties ) && string.Equals( OpenOrderReqID, other.OpenOrderReqID ) && string.Equals( OpenOrderRequestTXT, other.OpenOrderRequestTXT ) && string.Equals( OpenQuoteID, other.OpenQuoteID ) && OpenRate.Equals( other.OpenRate ) && OpenTime.Equals( other.OpenTime ) && RolloverInterest.Equals( other.RolloverInterest ) && string.Equals( TradeID, other.TradeID ) && string.Equals( TradeIDOrigin, other.TradeIDOrigin ) && Dividends.Equals( other.Dividends );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            if ( ReferenceEquals( this, obj ) ) return true;
            if ( obj.GetType( ) != GetType( ) ) return false;
            return Equals( ( FxDetailedPosition ) obj );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                var hashCode = ( AccountID != null ? AccountID.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( AccountKind != null ? AccountKind.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( AccountName != null ? AccountName.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ Amount;
                hashCode = ( hashCode * 397 ) ^ ( BuySell != null ? BuySell.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ Commission.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ ( OfferID != null ? OfferID.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( OpenOrderID != null ? OpenOrderID.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( OpenOrderParties != null ? OpenOrderParties.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( OpenOrderReqID != null ? OpenOrderReqID.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( OpenOrderRequestTXT != null ? OpenOrderRequestTXT.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( OpenQuoteID != null ? OpenQuoteID.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ OpenRate.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ OpenTime.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ RolloverInterest.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ ( TradeID != null ? TradeID.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( TradeIDOrigin != null ? TradeIDOrigin.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ Dividends.GetHashCode( );
                return hashCode;
            }
        }
        public int CompareTo( FxDetailedPosition other )
        {
            int result  = AccountID.CompareTo( other.AccountID );

            if ( result != 0 )
            {
                return result;
            }

            result = OpenOrderID.CompareTo( other.OpenOrderID );

            if ( result != 0 )
            {
                return result;
            }

            result = TradeID.CompareTo( other.TradeID );

            if ( result != 0 )
            {
                return result;
            }

            result = Amount.CompareTo( other.Amount );

            if ( result != 0 )
            {
                return result;
            }

            result = RolloverInterest.CompareTo( other.RolloverInterest );

            if ( result != 0 )
            {
                return result;
            }

            // -----------------------------------------------------------------------------
            // The followings are something that I consider won't change.

            result = AccountKind.CompareTo( other.AccountKind );

            if ( result != 0 )
            {
                return result;
            }

            result = AccountName.CompareTo( other.AccountName );

            if ( result != 0 )
            {
                return result;
            }

            result = BuySell.CompareTo( other.BuySell );

            if ( result != 0 )
            {
                return result;
            }

            result = Commission.CompareTo( other.Commission );

            if ( result != 0 )
            {
                return result;
            }

            result = OfferID.CompareTo( other.OfferID );

            if ( result != 0 )
            {
                return result;
            }

            result = OpenOrderParties.CompareTo( other.OpenOrderParties );

            if ( result != 0 )
            {
                return result;
            }

            result = OpenOrderReqID.CompareTo( other.OpenOrderReqID );

            if ( result != 0 )
            {
                return result;
            }

            result = OpenOrderRequestTXT.CompareTo( other.OpenOrderRequestTXT );

            if ( result != 0 )
            {
                return result;
            }

            result = OpenQuoteID.CompareTo( other.OpenQuoteID );

            if ( result != 0 )
            {
                return result;
            }

            result = OpenRate.CompareTo( other.OpenRate );

            if ( result != 0 )
            {
                return result;
            }

            result = OpenTime.CompareTo( other.OpenTime );

            if ( result != 0 )
            {
                return result;
            }

            result = TradeIDOrigin.CompareTo( other.TradeIDOrigin );

            if ( result != 0 )
            {
                return result;
            }

            return Dividends.CompareTo( other.Dividends );
        }
    }
}

