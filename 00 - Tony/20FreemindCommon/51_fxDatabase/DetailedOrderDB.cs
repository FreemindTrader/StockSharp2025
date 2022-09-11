
using System.ComponentModel;
using fx.Definitions; 

#if TONY
CREATE TABLE [Orders] (
[Id] INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
[AccountID]             NVARCHAR        NOT NULL,
[AccountKind]           NVARCHAR        NOT NULL,
[AccountName]           NVARCHAR        NOT NULL,
[Amount]                INTEGER         NOT NULL,
[AtMarket]              FLOAT           NOT NULL,
[BuySell]               NVARCHAR        NOT NULL,
[ContingencyType]       INTEGER         NULL,
[ContingentOrderID]     NVARCHAR        NOT NULL,
[ExecutionRate]         FLOAT           NOT NULL,
[ExpireDate]            INTEGER         NOT NULL,
[FilledAmount]          INTEGER         NOT NULL,
[NetQuantity]           BOOLEAN         NOT NULL,
[OfferID]               NVARCHAR        NOT NULL,
[OrderID]               NVARCHAR        NOT NULL,
[OriginAmount]          INTEGER         NOT NULL,
[Parties]               NVARCHAR        NOT NULL,
[PegOffset]             FLOAT           NOT NULL,
[PegType]               NVARCHAR        NOT NULL,
[PrimaryID]             NVARCHAR        NOT NULL,
[Rate]                  FLOAT           NOT NULL,
[RateMax]               FLOAT           NOT NULL,
[RateMin]               FLOAT           NOT NULL,
[RequestID]             NVARCHAR        NOT NULL,
[RequestTXT]            NVARCHAR        NOT NULL,
[Stage]                 NVARCHAR        NOT NULL,
[Status]                NVARCHAR        NOT NULL,
[StatusTime]            INTEGER         NOT NULL,
[TimeInForce]           NVARCHAR        NOT NULL,
[TradeID]               NVARCHAR        NOT NULL,
[TrailRate]             FLOAT           NOT NULL,
[TrailStep]             INTEGER         NOT NULL,
[Type]                  NVARCHAR        NOT NULL,
[ValueDate]             NVARCHAR        NOT NULL,
[WorkingIndicator ]     BOOLEAN         NOT NULL
)
#endif

namespace fx.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Serializable]
    public partial class DetailedOrderDB : INotifyPropertyChanged, IDetailedOrder, IFxcm, IEquatable< IDetailedOrder >
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double _rate;

        public DetailedOrderDB( )
        {
        }

        public DetailedOrderDB(     string accountId, 
                                    string accountKind,
                                    string mainLoginName,
                                    string accountName,                                     
                                    int amount, 
                                    double atMarket,
                                    string buySell, 
                                    int contingencyType, 
                                    string contingentOrderId, 
                                    double executionRate,
                                    long expireDate, 
                                    int filledAmount, 
                                    bool netQuantity, 
                                    string offerId, 
                                    string orderId,
                                    int originAmount, 
                                    string parties, 
                                    double pegOffset, 
                                    string pegType, 
                                    string primaryId,
                                    double rate, 
                                    double rateMax, 
                                    double rateMin, 
                                    string requestId, 
                                    string requestTxt, 
                                    string stage,
                                    string status, 
                                    long statusTime, 
                                    string timeInForce, 
                                    string tradeId, 
                                    double trailRate,
                                    int trailStep, 
                                    string type, 
                                    string valueDate, 
                                    bool workingIndicator )
        {
            AccountID         = accountId;
            AccountKind       = accountKind;
            MainLoginName = mainLoginName;
            AccountName       = accountName;
            Amount            = amount;
            AtMarket          = atMarket;
            BuySell           = buySell;
            ContingencyType   = contingencyType;
            ContingentOrderID = contingentOrderId;
            ExecutionRate     = executionRate;
            ExpireDate        = expireDate;
            FilledAmount      = filledAmount;
            NetQuantity       = netQuantity;
            OfferID           = offerId;
            OrderID           = orderId;
            OriginAmount      = originAmount;
            Parties           = parties;
            PegOffset         = pegOffset;
            PegType           = pegType;
            PrimaryID         = primaryId;
            Rate              = rate;
            RateMax           = rateMax;
            RateMin           = rateMin;
            RequestID         = requestId;
            RequestTXT        = requestTxt;
            Stage             = stage;
            Status            = status;
            StatusTime        = statusTime;
            StatusTimeDT      = StatusTime.FromLinuxTime();
            TimeInForce       = timeInForce;
            TradeID           = tradeId;
            TrailRate         = trailRate;
            TrailStep         = trailStep;
            Type              = type;
            ValueDate         = valueDate;
            WorkingIndicator  = workingIndicator;
        }

        public void CopyFrom( IDetailedOrder other )
        {
            if ( ! AccountID.Equals( other.AccountID ) )
            {
                AccountID = other.AccountID;
            }

            if ( ! AccountKind.Equals( other.AccountKind ) )
            {
                AccountKind = other.AccountKind;
            }

            if ( !MainLoginName.Equals( other.MainLoginName ) )
            {
                MainLoginName = other.MainLoginName;
            }

            if ( ! AccountName.Equals( other.AccountName ) )
            {
                AccountName = other.AccountName;
            }

            if ( Amount != other.Amount )
            {
                Amount = other.Amount;
            }

            if ( AtMarket != other.AtMarket )
            {
                AtMarket = other.AtMarket;
            }
            
            if ( ! BuySell.Equals( other.BuySell ) )
            {
                BuySell = other.BuySell;
            }
            
            if ( ContingencyType != other.ContingencyType )
            {
                ContingencyType = other.ContingencyType;
            }

            if ( ! ContingentOrderID.Equals( other.ContingentOrderID ) )
            {
                ContingentOrderID = other.ContingentOrderID;
            }

            if ( ExecutionRate != other.ExecutionRate )
            {
                ExecutionRate = other.ExecutionRate;
            }

            if ( ExpireDate != other.ExpireDate )
            {
                ExpireDate = other.ExpireDate;
            }
            
            if ( FilledAmount != other.FilledAmount )
            {
                FilledAmount = other.FilledAmount;
            }

            if ( ! OfferID.Equals( other.OfferID ) )
            {
                OfferID = other.OfferID;
            }

            NetQuantity = other.NetQuantity;

            if ( ! OfferID.Equals( other.OfferID ) )
            {
                OfferID = other.OfferID;
            }

            if ( ! OrderID.Equals( other.OrderID ) )
            {
                OrderID = other.OrderID;
            }
            
            if ( OriginAmount != other.OriginAmount )
            {
                OriginAmount = other.OriginAmount;
            }

            if ( ! Parties.Equals( other.Parties ) )
            {
                Parties = other.Parties;
            }
            
            if ( PegOffset != other.PegOffset )
            {
                PegOffset = other.PegOffset;
            }

            if ( ! PegType.Equals( other.PegType ) )
            {
                PegType = other.PegType;
            }

            if ( ! PrimaryID.Equals( other.PrimaryID ) )
            {
                PrimaryID = other.PrimaryID;
            }

            if ( ! RequestID.Equals( other.RequestID ) )
            {
                RequestID = other.RequestID;
            }

            if ( ! RequestTXT.Equals( other.RequestTXT ) )
            {
                RequestTXT = other.RequestTXT;
            }
            
            if ( ! Stage.Equals( other.Stage ) )
            {
                Stage = other.Stage;
            }

            if ( ! Status.Equals( other.Status ) )
            {
                Status = other.Status;
            }

            if ( Rate != other.Rate )
            {
                Rate = other.Rate;
            }

            if ( RateMax != other.RateMax )
            {
                RateMax = other.RateMax;
            }

            if ( RateMin != other.RateMin )
            {
                RateMin = other.RateMin;
            }

            if ( ! TimeInForce.Equals( other.TimeInForce ) )
            {
                TimeInForce = other.TimeInForce;
            }

            if ( ! TradeID.Equals( other.TradeID ) )
            {
                TradeID = other.TradeID;
            }

            if ( ! Type.Equals( other.Type ) )
            {
                Type = other.Type;
            }

            if ( ! ValueDate.Equals( other.ValueDate ) )
            {
                ValueDate = other.ValueDate;
            }
            
            if ( StatusTime != other.StatusTime )
            {
                StatusTime = other.StatusTime;
            }

            if ( StatusTimeDT != other.StatusTimeDT )
            {
                StatusTimeDT = other.StatusTimeDT;
            }

            if ( TrailRate != other.TrailRate )
            {
                TrailRate = other.TrailRate;
            }
             
            if ( TrailStep != other.TrailStep )
            {
                TrailStep = other.TrailStep;
            }       
                        
            WorkingIndicator  = other.WorkingIndicator;
            
        }

        public bool Equals( IDetailedOrder other )
        {
            if ( ReferenceEquals( null, other ) ) return false;
            if ( ReferenceEquals( this, other ) ) return true;
            return string.Equals( AccountID, other.AccountID ) && string.Equals( AccountKind, other.AccountKind ) &&
                   string.Equals( AccountName, other.AccountName ) && Amount == other.Amount &&
                   AtMarket.Equals( other.AtMarket ) && string.Equals( BuySell, other.BuySell ) &&
                   ContingencyType == other.ContingencyType &&
                   string.Equals( ContingentOrderID, other.ContingentOrderID ) &&
                   ExecutionRate.Equals( other.ExecutionRate ) && ExpireDate == other.ExpireDate &&
                   FilledAmount == other.FilledAmount && NetQuantity.Equals( other.NetQuantity ) &&
                   string.Equals( OfferID, other.OfferID ) && string.Equals( OrderID, other.OrderID ) &&
                   OriginAmount == other.OriginAmount && string.Equals( Parties, other.Parties ) &&
                   PegOffset.Equals( other.PegOffset ) && string.Equals( PrimaryID, other.PrimaryID ) &&
                   Rate.Equals( other.Rate ) && RateMax.Equals( other.RateMax ) && RateMin.Equals( other.RateMin ) &&
                   string.Equals( RequestID, other.RequestID ) && string.Equals( RequestTXT, other.RequestTXT ) &&
                   string.Equals( Stage, other.Stage ) && string.Equals( Status, other.Status ) &&
                   StatusTime == other.StatusTime && string.Equals( TimeInForce, other.TimeInForce ) &&
                   string.Equals( TradeID, other.TradeID ) && TrailRate.Equals( other.TrailRate ) &&
                   TrailStep == other.TrailStep && string.Equals( Type, other.Type ) &&
                   string.Equals( ValueDate, other.ValueDate ) && WorkingIndicator.Equals( other.WorkingIndicator );
        }

        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public long Id                { get; set; }
        public long     StartDate         { get; set; }

        public string   AccountID         { get; set; }
        public string   AccountKind       { get; set; }

        public string   MainLoginName      { get; set; }
        public string   AccountName       { get; set; }
        public int      Amount            { get; set; }
        public double   AtMarket          { get; set; }
        public string   BuySell           { get; set; }
        public int      ContingencyType   { get; set; }
        public string   ContingentOrderID { get; set; }
        public double   ExecutionRate     { get; set; }
        public long     ExpireDate        { get; set; }
        public DateTime ExpireDateDT      { get; set; }
        public int      FilledAmount      { get; set; }        
        public bool     NetQuantity       { get; set; }
        public string   OfferID           { get; set; }
        public string   OrderID           { get; set; }
        public int      OriginAmount      { get; set; }
        public string   Parties           { get; set; }
        public double   PegOffset         { get; set; }
        public string   PegType           { get; set; }
        public string   PrimaryID         { get; set; }

        public double Rate
        {
            get { return _rate; }
            set
            {
                if ( _rate == value )
                    return;
                _rate = value;

                if ( PropertyChanged != null )
                {
                    PropertyChanged( this, new PropertyChangedEventArgs( "Rate" ) );
                } 
            }
        }
        public double   RateMax           { get; set; }
        public double   RateMin           { get; set; }
        public string   RequestID         { get; set; }
        public string   RequestTXT        { get; set; }
        public string   Stage             { get; set; }
        
        public string   Status            { get; set; }
        public long     StatusTime        { get; set; }
        public DateTime StatusTimeDT      { get; set; }
        public string   TimeInForce       { get; set; }
        public string   TradeID           { get; set; }
        public double   TrailRate         { get; set; }
        public int      TrailStep         { get; set; }
        public string   Type              { get; set; }
        public string   ValueDate         { get; set; }
        public bool     WorkingIndicator  { get; set; }

        public static bool operator ==( DetailedOrderDB left, DetailedOrderDB right )
        {
            // If we used == to check for null instead of Object.ReferenceEquals(), we'd
            // get a StackOverflowException. Can you figure out why?
            if ( ReferenceEquals( left, null ) )
                return false;
            else
                return left.Equals( right );
        }

        public static bool operator !=( DetailedOrderDB left, DetailedOrderDB right )
        {
            // Since we've already defined ==, we can just invert it for !=.
            return !( left == right );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            if ( ReferenceEquals( this, obj ) ) return true;
            if ( obj.GetType( ) != GetType( ) ) return false;
            return Equals( ( DetailedOrderDB ) obj );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                var hashCode = ( AccountID != null ? AccountID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( AccountKind != null ? AccountKind.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( AccountName != null ? AccountName.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ Amount;
                hashCode = ( hashCode*397 ) ^ AtMarket.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ ( BuySell != null ? BuySell.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ContingencyType;
                hashCode = ( hashCode*397 ) ^ ( ContingentOrderID != null ? ContingentOrderID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ExecutionRate.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ ExpireDate.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ FilledAmount;
                hashCode = ( hashCode*397 ) ^ NetQuantity.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ ( OfferID != null ? OfferID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( OrderID != null ? OrderID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ OriginAmount;
                hashCode = ( hashCode*397 ) ^ ( Parties != null ? Parties.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ PegOffset.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ ( PrimaryID != null ? PrimaryID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ Rate.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ RateMax.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ RateMin.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ ( RequestID != null ? RequestID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( RequestTXT != null ? RequestTXT.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( Stage != null ? Stage.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( Status != null ? Status.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ StatusTime.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ ( TimeInForce != null ? TimeInForce.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( TradeID != null ? TradeID.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ TrailRate.GetHashCode( );
                hashCode = ( hashCode*397 ) ^ TrailStep;
                hashCode = ( hashCode*397 ) ^ ( Type != null ? Type.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ ( ValueDate != null ? ValueDate.GetHashCode( ) : 0 );
                hashCode = ( hashCode*397 ) ^ WorkingIndicator.GetHashCode( );
                return hashCode;
            }
        }


        public bool isMarketOrder()
        {
            //case FxOrderType.Close:              sOrderType = "C"; break;
            //    case FxOrderType.CloseMarket:        sOrderType = "CM"; break;
            //    case FxOrderType.CloseRange:         sOrderType = "CR"; break;
            //    case FxOrderType.Open:               sOrderType = "O"; break;
            //    case FxOrderType.OpenMarket:         sOrderType = "OM"; break;
            //    case FxOrderType.OpenRange:          sOrderType = "OR"; break;

            if ( ( Type == "O" ) || ( Type == "OM" ) || ( Type == "OR" ) || ( Type == "C" ) || ( Type == "CM" ) || ( Type == "CR" ) )
            {
                return true;
            }

            return false;
        }

        public IDetailedOrder Clone( )
        {
            return new DetailedOrderDB( AccountID ,
                                        AccountKind, 
                                        MainLoginName,
                                        AccountName       ,
                                        Amount            ,
                                        AtMarket          ,
                                        BuySell           ,
                                        ContingencyType   ,
                                        ContingentOrderID ,
                                        ExecutionRate     ,
                                        ExpireDate        ,
                                        FilledAmount      ,
                                        NetQuantity       ,
                                        OfferID           ,
                                        OrderID           ,
                                        OriginAmount      ,
                                        Parties           ,
                                        PegOffset         ,
                                        PegType           ,
                                        PrimaryID         ,
                                        Rate              ,
                                        RateMax           ,
                                        RateMin           ,
                                        RequestID         ,
                                        RequestTXT        ,
                                        Stage             ,
                                        Status            ,
                                        StatusTime        ,                                        
                                        TimeInForce       ,
                                        TradeID           ,
                                        TrailRate         ,
                                        TrailStep         ,
                                        Type              ,
                                        ValueDate         ,
                                        WorkingIndicator   );
        }
    }
}