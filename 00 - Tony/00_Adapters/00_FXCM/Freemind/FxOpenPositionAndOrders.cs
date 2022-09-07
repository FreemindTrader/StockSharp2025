using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public class FxOpenPositionAndOrders : IOpenPositionAndOrders, IEquatable<IOpenPositionAndOrders>
    {
        private IPositionOrderCalculatedValue _calculatedValue;

        private DetailedOrderDB               _stopOrder;
        private DetailedOrderDB               _limitOrder;
        private DetailedOrderDB               _thisOrder;

        private double                        _usedMargin;
        private string                        _ticket;
        private string                        _symbol;
        private double                        _rolloverInterest;

        private DateTime                      _openTime;
        private double                        _openPrice;

        private double                        _commission;
        private string                        _comment;

        private string                        _buySell;
        private int                           _amount;
        private string                        _accountName;
        private string                        _accountId;
        private string                        _mainLoginName;

        public SymbolGroup SymbolGroup
        {
            get { return TonyHelper.FindGroupBySymbol( _symbol ); }
        }

        
        
        public string AccountID
        {
            get { return _accountId; }
            set
            {
                if ( _accountId == value )
                    return;
                _accountId = value;
            }
        }


        public string AccountName
        {
            get { return _accountName; }
            set
            {
                if ( _accountName == value )
                    return;
                _accountName = value;
            }
        }

        public string MainLoginName
        {
            get { return _mainLoginName; }
            set
            {
                if ( _mainLoginName == value )
                    return;
                _mainLoginName = value;                
            }
        }


        public int Amount
        {
            get { return _amount; }
            set
            {
                if ( _amount == value )
                    return;
                _amount = value;
            }
        }


        public string BuySell
        {
            get { return _buySell; }
            set
            {
                if ( _buySell == value )
                    return;
                _buySell = value;                
            }
        }


        public double? ClosePrice
        {
            get { return _calculatedValue?.ClosePrice; }
            set {; }
        }


        public string Comment
        {
            get { return _comment; }
            set
            {
                if ( _comment == value )
                    return;
                _comment = value;

            }
        }


        public double Commission
        {
            get { return _commission; }
            set
            {
                if ( _commission == value )
                    return;
                _commission = value;

            }
        }


        public double GrossProfit
        {
            get
            {
                if ( _calculatedValue != null )
                {
                    return _calculatedValue.GrossProfit;
                }

                return 0;
            }
            set {; }
        }


        public double? LimitPrice
        {
            get
            {
                return LimitOrder?.Rate;
            }

            set
            {
                if ( LimitOrder != null )
                {
                    if ( value.HasValue )
                    {
                        LimitOrder.Rate = value.Value;

                       
                    }
                }
            }
        }

        public double OpenPrice
        {
            get { return _openPrice; }
            set
            {
                if ( _openPrice == value )
                    return;
                _openPrice = value;

               
            }
        }


        public DateTime OpenTime
        {
            get { return _openTime; }
            set
            {
                if ( _openTime == value )
                    return;
                _openTime = value;

            }
        }


        public double PipProfit
        {
            get
            {
                if ( _calculatedValue != null )
                {
                    return _calculatedValue.PipProfit;
                }

                return 0;
            }
            set {; }
        }


        public double RolloverInterest
        {
            get { return _rolloverInterest; }
            set
            {
                if ( _rolloverInterest == value )
                    return;
                _rolloverInterest = value;

            }
        }


        public int? StopMove
        {
            get
            {
                return StopOrder?.TrailStep;
            }

            set
            {
                if ( StopOrder != null )
                {
                    if ( value.HasValue )
                    {
                        StopOrder.Rate = value.Value;

                    }
                }
            }
        }


        public double? StopPrice
        {
            get
            {
                return StopOrder?.Rate;
            }

            set
            {
                if ( StopOrder != null )
                {
                    if ( value.HasValue )
                    {
                        StopOrder.Rate = value.Value;

                    }
                }
            }
        }


        public string Symbol
        {
            get { return _symbol; }
            set
            {
                if ( _symbol == value )
                    return;
                _symbol = value;

            }
        }


        public string Ticket
        {
            get { return _ticket; }
            set
            {
                if ( _ticket == value )
                    return;
                _ticket = value;

            }
        }

        public double UsedMargin
        {
            get { return _usedMargin; }
            set
            {
                if ( _usedMargin == value )
                    return;
                _usedMargin = value;

            }
        }


        public IDetailedOrder ThisOrder
        {
            get { return _thisOrder; }
            set
            {
                _thisOrder = ( DetailedOrderDB ) value;

            }
        }


        public IDetailedOrder LimitOrder
        {
            get { return _limitOrder; }
            set
            {
                _limitOrder = ( DetailedOrderDB ) value;

            }
        }
        

        public bool IsBuy
        {
            get { return BuySell == "B"; }
        }

        public bool IsSell
        {
            get
            {
                return _buySell == "S";
            }
        }


        public IDetailedOrder StopOrder
        {
            get { return _stopOrder; }
            set
            {
                _stopOrder = ( DetailedOrderDB ) value;

            }
        }


        public FxOpenPositionAndOrders( IDetailedPosition position )
        {
            Ticket = position.TradeID;
            AccountID = position.AccountID;
            MainLoginName = position.MainLoginName;
            AccountName = position.AccountName;
            Symbol = GFMgr.GetSymbolFromOfferId( position.OfferID );
            UsedMargin = position.Dividends;
            Amount = position.Amount;
            BuySell = position.BuySell;
            OpenPrice = position.OpenRate;
            Commission = position.Commission;
            RolloverInterest = position.RolloverInterest;
            OpenTime = position.OpenTime;
            Comment = position.OpenOrderRequestTXT;

            // --------- The followings are caculated Values ---------------
            // ClosePrice will be from the offer
            // Stop will be from the order
            // Limite will be also from the order
            ClosePrice = 0;
            StopPrice = 0;
            StopMove = 0;
            LimitPrice = 0;
            PipProfit = 0;
            GrossProfit = 0;

        }


        public FxOpenPositionAndOrders( string ticket,
                                                string accountId,
                                                string mainLoginName,
                                                string accountName,

                                                string symbol,
                                                double usedMargin,
                                                int amount,
                                                string buySell,
                                                double open,
                                                double? close,
                                                double? stop,
                                                int? stopMove,
                                                double? limit,
                                                double pipProfit,
                                                double grossProfit,
                                                double commission,
                                                double rolloverInterest,
                                                DateTime openTime,
                                                string comment )
        {
            Ticket = ticket;
            AccountID = accountId;
            MainLoginName = mainLoginName;
            AccountName = accountName;
            Symbol = symbol;
            UsedMargin = usedMargin;
            Amount = amount;
            BuySell = buySell;
            OpenPrice = open;
            ClosePrice = close;
            StopPrice = stop;
            StopMove = stopMove;
            LimitPrice = limit;
            PipProfit = pipProfit;
            GrossProfit = grossProfit;
            Commission = commission;
            RolloverInterest = rolloverInterest;
            OpenTime = openTime;
            Comment = comment;
        }

        public IOpenPositionAndOrders Clone( )
        {
            var output = new FxOpenPositionAndOrders(  Ticket,
                                                    AccountID,
                                                    MainLoginName,
                                                    AccountName,
                                                    Symbol,
                                                    UsedMargin,
                                                    Amount,
                                                    BuySell,
                                                    OpenPrice,
                                                    ClosePrice,
                                                    StopPrice,
                                                    StopMove,
                                                    LimitPrice,
                                                    PipProfit,
                                                    GrossProfit,
                                                    Commission,
                                                    RolloverInterest,
                                                    OpenTime,
                                                    Comment
                                                );

            output.StopOrder = _stopOrder;
            output.LimitOrder = _limitOrder;
            output.ThisOrder = _thisOrder;


            return output;
        }

        public bool Equals( IOpenPositionAndOrders other )
        {
            return string.Equals( Ticket, other.Ticket ) && string.Equals( AccountID, other.AccountID ) &&
                   string.Equals( AccountName, other.AccountName ) && string.Equals( Symbol, other.Symbol ) &&
                   UsedMargin.Equals( other.UsedMargin ) && Amount == other.Amount &&
                   string.Equals( BuySell, other.BuySell ) && OpenPrice.Equals( other.OpenPrice ) &&
                   ClosePrice.Equals( other.ClosePrice ) && StopPrice.Equals( other.StopPrice ) &&
                   StopMove.Equals( other.StopMove ) && LimitPrice.Equals( other.LimitPrice ) &&
                   PipProfit.Equals( other.PipProfit ) && GrossProfit.Equals( other.GrossProfit ) &&
                   Commission.Equals( other.Commission ) && RolloverInterest.Equals( other.RolloverInterest ) &&
                   OpenTime.Equals( other.OpenTime ) && string.Equals( Comment, other.Comment );
        }

        public bool Equals( IDetailedPosition other )
        {
            string otherSymbol           = GFMgr.GetSymbolFromOfferId( other.OfferID );

            return string.Equals( Ticket, other.TradeID ) && string.Equals( AccountID, other.AccountID ) &&
                   string.Equals( AccountName, other.AccountName ) && string.Equals( Symbol, otherSymbol ) &&
                   UsedMargin.Equals( other.Dividends ) && Amount == other.Amount &&
                   string.Equals( BuySell, other.BuySell ) && OpenPrice.Equals( other.OpenRate ) &&
                   Commission.Equals( other.Commission ) && RolloverInterest.Equals( other.RolloverInterest ) &&
                   OpenTime.Equals( other.OpenTime ) && string.Equals( Comment, other.OpenOrderRequestTXT );
        }



        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            if ( ReferenceEquals( this, obj ) ) return true;
            if ( obj.GetType( ) != GetType( ) ) return false;

            return Equals( ( IOpenPositionAndOrders ) obj );
        }

        public string GetPositionType( )
        {
            if ( Comment == "1" ) return "Market";
            else if ( Comment == "2" ) return "Mean symbolPositionSummary";
            else if ( Comment == "3" ) return "Inner Bollinger";
            else if ( Comment == "4" ) return "Hedge";

            return "Manual";

        }
        

        public override int GetHashCode( )
        {
            unchecked
            {
                var hashCode = ( Ticket != null ? Ticket.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( AccountID != null ? AccountID.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( AccountName != null ? AccountName.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( Symbol != null ? Symbol.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ UsedMargin.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ Amount;
                hashCode = ( hashCode * 397 ) ^ ( BuySell != null ? BuySell.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ OpenPrice.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ ClosePrice.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ StopPrice.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ StopMove.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ LimitPrice.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ PipProfit.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ GrossProfit.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ Commission.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ RolloverInterest.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ OpenTime.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ ( Comment != null ? Comment.GetHashCode( ) : 0 );

                return hashCode;
            }
        }


        public IPositionOrderCalculatedValue CalculatedValue
        {
            get { return _calculatedValue; }

            set
            {
                _calculatedValue = value;

                _calculatedValue.SetParent( this );                
            }
        }        
    }
}
