
using System.ComponentModel;
using fx.Definitions;

using DevExpress.Mvvm;

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
    public partial class DbAccounts : BindableBase, IFxcm, IEquatable<DbAccounts>
    {        
        [Key, DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public long Id { get; set; }
        public long StartDate { get; set; }

        public string SubAccount { get; set; }

        public string MainLoginName { get; set; }
        public string AccountType { get; set; }

        public string AccountGroup { get; set; }


        public string Password { get; set; }

        public string AccountCountry { get; set; }

        public string Company { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        public bool IsActive { get; set; }

        public bool IsMainAccount { get; set; }

        public bool IsAutoStart { get; set; }

        public bool MainDataSource { get; set; }

        public int SubAccountType { get; set; }

        public bool HasFIX { get; set; }

        public string MarginProfile { get; set; }

        public int MarginLeverage { get; set; }

        public double UsableMarginPercentage { get; set; }

        public double MaxDrawdownPercentage { get; set; }

        private int _status;

        private int _isStarted = 0;

        public int Status
        {
            get { return _status; }
            set
            {
                SetValue( ref _status, value );
            }
        }

        public int IsStarted
        {
            get { return _isStarted; }
            set
            {
                SetValue( ref _isStarted, value );
            }
        }
                

        public DbAccounts( )
        {
        }

        

        public override bool Equals( object obj )
        {
            if ( obj is DbAccounts )
                return Equals( ( DbAccounts ) obj );
            return base.Equals( obj );
        }

        public static bool operator ==( DbAccounts first, DbAccounts second )
        {
            if ( ( object ) first == null )
                return ( object ) second == null;
            return first.Equals( second );
        }

        public static bool operator !=( DbAccounts first, DbAccounts second )
        {
            return !( first == second );
        }

        public bool Equals( DbAccounts other )
        {
            if ( ReferenceEquals( null, other ) )
                return false;
            if ( ReferenceEquals( this, other ) )
                return true;
            return  Id.Equals( other.Id ) && StartDate.Equals( other.StartDate ) && Equals( SubAccount, other.SubAccount ) && Equals( MainLoginName, other.MainLoginName ) && Equals( AccountType, other.AccountType ) && Equals( Password, other.Password ) && Equals( AccountCountry, other.AccountCountry );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                var hashCode = 47;
                
                hashCode = ( hashCode * 53 ) ^ Id.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ StartDate.GetHashCode( );
                if ( SubAccount != null )
                    hashCode = ( hashCode * 53 ) ^ SubAccount.GetHashCode( );
                if ( MainLoginName != null )
                    hashCode = ( hashCode * 53 ) ^ MainLoginName.GetHashCode( );
                if ( AccountType != null )
                    hashCode = ( hashCode * 53 ) ^ AccountType.GetHashCode( );
                if ( Password != null )
                    hashCode = ( hashCode * 53 ) ^ Password.GetHashCode( );
                if ( AccountCountry != null )
                    hashCode = ( hashCode * 53 ) ^ AccountCountry.GetHashCode( );
                return hashCode;
            }
        }
    }
}
