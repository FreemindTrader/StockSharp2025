using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public class PositionCalculatedValue : IPositionCalculatedValue
    {
        private IPositionsSummary _parent;

        public PositionCalculatedValue( )
        {

        }

        public PositionCalculatedValue( FxPositionsSummary parent )
        {
            _parent = parent;
        }

        public void SetParent( IPositionsSummary parent )
        {
            _parent = parent;
        }



        public double GrossPl { set; get; }
        public double NetPl
        { set; get; }

        public double? BuyNetPl
        { set; get; }

        public double? SellNetPl
        { set; get; }

        public double? SellClose
        { set; get; }

        public double? BuyClose
        { set; get; }

        public int? PendingSellVolume { get; set; }
        public int? PendingBuyVolume { get; set; }



        private double? _basis;

        public double? Basis
        {
            get
            {
                if ( ( _parent.SellAmount > 0 ) && ( _parent.BuyAmount > 0 ) )
                {
                    return ( _parent.SellAvgOpen + _parent.BuyAvgOpen ) / 2;
                }
                else if ( _parent.SellAmount > 0 )
                {
                    return _parent.SellAvgOpen;
                }
                else if ( _parent.BuyAmount > 0 )
                {
                    return _parent.BuyAvgOpen;
                }

                return 0;
            }

            set
            {
                _basis = value;
            }


        }

        private double? _result;

        /// <summary>
        /// Open Profit/Loss (PnL).
        /// </summary>
        public double? Result
        {
            get { return _result; }
            set { _result = value; }
        }

        private double? _marketValue;
        public double? MarketValue
        {
            get { return _marketValue; }

            set { _marketValue = value; }
        }

    }
    public class FxPositionsSummary : IPositionsSummary, IEquatable<FxPositionsSummary>
    {
        private double                   _amount;
        private double?                  _buyAvgOpen;
        private double?                  _buyAmount;
        private double?                  _sellAmount;
        private double?                  _sellAvgOpen;
        private string                   _symbol;
        private int                      _defaultSortOrder;
        private string                   _offerId;
        private IPositionCalculatedValue _calculatedValue;        

        public string MainLoginName { get; set; }

        public string AccountName { get; set; }

        public string AccountId { get; set; }

        public void SetParent( IPositionsSummary parent )
        {
        }




        public bool Equals( FxPositionsSummary other )
        {
            if ( ReferenceEquals( null, other ) ) return false;
            if ( ReferenceEquals( this, other ) ) return true;
            return _amount.Equals( other._amount ) && _buyAvgOpen.Equals( other._buyAvgOpen ) && _buyAmount.Equals( other._buyAmount ) && _sellAmount.Equals( other._sellAmount ) && _sellAvgOpen.Equals( other._sellAvgOpen ) && string.Equals( _symbol, other._symbol ) && _defaultSortOrder == other._defaultSortOrder && string.Equals( _offerId, other._offerId ) && Equals( _calculatedValue, other._calculatedValue ) && _result.Equals( other._result ) && _commission.Equals( other._commission ) && _closedResult.Equals( other._closedResult ) && Equals( _detailPositionInfos, other._detailPositionInfos );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            if ( ReferenceEquals( this, obj ) ) return true;
            if ( obj.GetType( ) != GetType( ) ) return false;
            return Equals( ( FxPositionsSummary ) obj );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                var hashCode = _amount.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ _buyAvgOpen.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ _buyAmount.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ _sellAmount.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ _sellAvgOpen.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ ( _symbol != null ? _symbol.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ _defaultSortOrder;
                hashCode = ( hashCode * 397 ) ^ ( _offerId != null ? _offerId.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( _calculatedValue != null ? _calculatedValue.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ _result.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ _commission.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ _closedResult.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ ( _detailPositionInfos != null ? _detailPositionInfos.GetHashCode( ) : 0 );
                return hashCode;
            }
        }

        public string OfferId
        {
            get { return _offerId; }
            set
            {
                if ( _offerId == value )
                    return;
                _offerId = value;                
            }
        }

        double _usedMargin = 0d;
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


        public int DefaultSortOrder
        {
            get { return _defaultSortOrder; }
            set
            {
                if ( _defaultSortOrder == value )
                    return;
                _defaultSortOrder = value;

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


        public double? SellNetPl
        {
            get { return _calculatedValue?.SellNetPl; }
            set {; }
        }


        public double? SellAvgOpen
        {
            get { return _sellAvgOpen; }
            set
            {
                if ( _sellAvgOpen == value )
                    return;
                _sellAvgOpen = value;

                
            }
        }


        public double? SellClose
        {
            get { return _calculatedValue?.SellClose; }
            set {; }
        }


        public double? SellAmount
        {
            get { return _sellAmount; }
            set
            {
                if ( _sellAmount == value )
                    return;
                _sellAmount = value;

                
            }
        }

        public decimal? Position
        {
            get
            {
                return (decimal)( _buyAmount - _sellAmount );
            }
        }


        public bool IsNetZero( )
        {
            return ( _sellAmount == _buyAmount );
        }

        public bool IsNetLong( )
        {
            return ( _buyAmount > _sellAmount );
        }

        public bool IsNetShort( )
        {
            return ( _buyAmount < _sellAmount );
        }

        public double? BuyAmount
        {
            get { return _buyAmount; }
            set
            {
                if ( _buyAmount == value )
                    return;
                _buyAmount = value;

                
            }
        }


        public double? BuyNetPl
        {
            get { return _calculatedValue?.BuyNetPl; }
            set {; }
        }


        public double? BuyClose
        {
            get { return _calculatedValue?.BuyClose; }
            set {; }
        }


        public double? BuyAvgOpen
        {
            get { return _buyAvgOpen; }
            set
            {
                if ( _buyAvgOpen == value )
                    return;
                _buyAvgOpen = value;

                
            }
        }


        public double Amount
        {
            get { return _amount; }
            set
            {
                if ( _amount == value )
                    return;
                _amount = value;


                
            }
        }


        public double GrossPl
        {
            get
            {
                if ( _calculatedValue != null )
                {
                    return _calculatedValue.GrossPl;
                }

                return 0;
            }
            set {; }
        }


        public double NetPl
        {
            get
            {
                if ( _calculatedValue != null )
                {
                    return _calculatedValue.NetPl;
                }

                return 0;

            }
            set {; }
        }

        

        public IPositionCalculatedValue CalculatedValue
        {
            get { return _calculatedValue; }

            set
            {
                _calculatedValue = value;

                _calculatedValue.SetParent( this );

                //UpdateCalculatedProperties( );
            }
        }


        

        private double? _result;

        /// <summary>
        /// Open Profit/Loss (PnL).
        /// </summary>
        public double? Result
        {
            get { return _result; }
            set { _result = value; }
        }

        private double? _commission;

        /// <summary>
        ///
        /// </summary>
        public double? Commission
        {
            get { return _commission; }
            set { _commission = value; }
        }

        private double? _closedResult;

        /// <summary>
        ///
        /// </summary>
        public double? ClosedResult
        {
            get { return _closedResult; }
            set { _closedResult = value; }
        }

        public int? PendingSellVolume
        {
            get
            {
                return _calculatedValue?.PendingSellVolume;
            }

            set {; }
        }
        public int? PendingBuyVolume
        {
            get
            {
                return _calculatedValue?.PendingBuyVolume;
            }

            set {; }
        }


        public double? Basis
        {
            get { return _calculatedValue?.Basis; }

            set
            {
                _calculatedValue.Basis = value;
            }
        }

        public double? MarketValue
        {
            get { return _calculatedValue?.MarketValue; }

            set
            {
                _calculatedValue.MarketValue = value;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public bool IsEmpty
        {
            get { return string.IsNullOrWhiteSpace( Symbol ); }
        }

        public static FxPositionsSummary Empty
        {
            get { return new FxPositionsSummary( ); }
        }

        public IList<IOpenPositionAndOrders> DetailPositionInfos
        {
            get { return _detailPositionInfos; }
        }

        private List< IOpenPositionAndOrders > _detailPositionInfos;

        public FxPositionsSummary( )
        {
            _calculatedValue = new PositionCalculatedValue( this );
        }

        /// <summary>
        ///
        /// </summary>
        public FxPositionsSummary(
                                    string mainLoginName,
                                    string accountName,
                                    string accountId,
                                    string offerID,
                                    int defaultSortOrder,
                                    string instrument,
                                    double? sellAmountK,
                                    double? sellAverageOpen,
                                    double? sellNetPL,
                                    double? sellClose,
                                    double? buyAmountK,
                                    double? buyAverageOpen,
                                    double? buyNetPL,
                                    double? buyClose,
                                    double amountK,
                                    double grossPL,
                                    double netPl
                                 )
        {
            MainLoginName    = mainLoginName;
            AccountName      = accountName;
            AccountId = accountId;
            OfferId          = offerID;
            DefaultSortOrder = defaultSortOrder;
            Symbol           = instrument;
            BuyAvgOpen       = buyAverageOpen;
            BuyAmount        = buyAmountK;
            BuyNetPl         = buyNetPL;
            BuyClose         = buyClose;
            SellAmount       = sellAmountK;
            SellAvgOpen      = sellAverageOpen;
            SellNetPl        = sellNetPL;
            SellClose        = sellClose;
            Amount           = amountK;
            GrossPl          = grossPL;
            NetPl            = netPl;


            _detailPositionInfos = new List<IOpenPositionAndOrders>( );
        }

        public FxPositionsSummary(
                                    string mainLoginName,
                                    string accountName,
                                    string accountId,
                                    string offerID,
                                    int defaultSortOrder,
                                    string instrument,
                                    double? sellAmountK,
                                    double? sellAverageOpen,
                                    double? buyAmountK,
                                    double? buyAverageOpen,
                                    double amountK,
                                    double grossPL,
                                    double netPl
                                 )
        {
            MainLoginName = mainLoginName;
            AccountName = accountName;
            AccountId = accountId;
            OfferId = offerID;
            DefaultSortOrder = defaultSortOrder;
            Symbol = instrument;
            BuyAvgOpen = buyAverageOpen;
            BuyAmount = buyAmountK;
            BuyNetPl = 0;
            BuyClose = 0;
            SellAmount = sellAmountK;
            SellAvgOpen = sellAverageOpen;
            SellNetPl = 0;
            SellClose = 0;
            Amount = amountK;
            GrossPl = grossPL;
            NetPl = netPl;


            _detailPositionInfos = new List<IOpenPositionAndOrders>( );
        }

        public void AddDetailPositionInfos( List<IOpenPositionAndOrders> detailInfos )
        {
            _detailPositionInfos.AddRange( detailInfos );
        }

        public void AddDetailPositionInfos( IOpenPositionAndOrders detailPendingOrdersInfo )
        {
            _detailPositionInfos.Add( detailPendingOrdersInfo );
        }



        /// <summary>
        /// CopyFrom current instance with new information.
        /// </summary>
        public void CopyFrom( IPositionsSummary other )
        {

            AccountName = other.AccountName;
            Symbol = other.Symbol;
            Amount = other.Amount;

            if ( other.Result.HasValue )
            {
                _result = other.Result;
            }

            if ( other.PendingBuyVolume.HasValue )
            {
                PendingBuyVolume = other.PendingBuyVolume;
            }

            if ( other.BuyAmount.HasValue )
            {
                BuyAmount = other.BuyAmount;
            }

            if ( other.BuyAvgOpen.HasValue )
            {
                BuyAvgOpen = other.BuyAvgOpen;
            }

            if ( other.SellAmount.HasValue )
            {
                SellAmount = other.SellAmount;
            }

            if ( other.SellAvgOpen.HasValue )
            {
                SellAvgOpen = other.SellAvgOpen;
            }

            //if ( other.ClosedResult.HasValue )
            //{
            //    _closedResult = other.ClosedResult;
            //}

            if ( other.MarketValue.HasValue )
            {
                MarketValue = other.MarketValue;
            }

            if ( other.Commission.HasValue )
            {
                _commission = other.Commission;
            }

            if ( other.PendingBuyVolume.HasValue )
            {
                PendingBuyVolume = other.PendingBuyVolume;
            }

            if ( other.PendingSellVolume.HasValue )
            {
                PendingSellVolume = other.PendingSellVolume;
            }

            _detailPositionInfos = new List<IOpenPositionAndOrders>( );

            _detailPositionInfos.AddRange( other.DetailPositionInfos );
        }




    }
}
