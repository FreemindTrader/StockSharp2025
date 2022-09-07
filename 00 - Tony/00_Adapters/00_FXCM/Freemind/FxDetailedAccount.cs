using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public class FxDetailedAccount : IDetailedAccount, IEquatable<IDetailedAccount>
    {
        public bool Equals( IDetailedAccount other )
        {
            return string.Equals( AccountID, other.AccountID ) && string.Equals( AccountKind, other.AccountKind ) && string.Equals( AccountName, other.AccountName ) && AmountLimit == other.AmountLimit && Balance.Equals( other.Balance ) && BaseUnitSize == other.BaseUnitSize && LastMarginCallDate.Equals( other.LastMarginCallDate ) && string.Equals( LeverageProfileID, other.LeverageProfileID ) && M2MEquity.Equals( other.M2MEquity ) && MaintenanceFlag.Equals( other.MaintenanceFlag ) && string.Equals( MaintenanceType, other.MaintenanceType ) && string.Equals( ManagerAccountID, other.ManagerAccountID ) && string.Equals( MarginCallFlag, other.MarginCallFlag ) && NonTradeEquity.Equals( other.NonTradeEquity ) && UsedMargin.Equals( other.UsedMargin ) && UsedMargin3.Equals( other.UsedMargin3 );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            if ( ReferenceEquals( this, obj ) ) return true;
            if ( obj.GetType( ) != GetType( ) ) return false;
            return Equals( ( FxDetailedAccount ) obj );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                var hashCode = ( AccountID != null ? AccountID.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( AccountKind != null ? AccountKind.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( AccountName != null ? AccountName.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ AmountLimit;
                hashCode = ( hashCode * 397 ) ^ Balance.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ BaseUnitSize;
                hashCode = ( hashCode * 397 ) ^ LastMarginCallDate.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ ( LeverageProfileID != null ? LeverageProfileID.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ M2MEquity.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ MaintenanceFlag.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ ( MaintenanceType != null ? MaintenanceType.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( ManagerAccountID != null ? ManagerAccountID.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( MarginCallFlag != null ? MarginCallFlag.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ NonTradeEquity.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ UsedMargin.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ UsedMargin3.GetHashCode( );
                return hashCode;
            }
        }

        public string AccountID { get; set; }

        public string AccountKind { get; set; }
        public string AccountName { get; set; }

        public string AccountType { get; set; }

        public int AmountLimit { get; set; }

        public double Balance { get; set; }

        public int BaseUnitSize { get; set; }

        public DateTime LastMarginCallDate { get; set; }

        public string LeverageProfileID { get; set; }

        public int MarginLeverage { get; set; }

        public double M2MEquity { get; set; }

        public bool MaintenanceFlag { get; set; }
        public string MaintenanceType { get; set; }

        public string ManagerAccountID { get; set; }
        public string MarginCallFlag { get; set; }

        public double NonTradeEquity { get; set; }

        public double UsedMargin { get; set; }

        public double UsedMargin3 { get; set; }

        public int SubAccountType { get; set; }

        public string AccountCurrency { get; set; }

        public IDetailedAccount Clone( )
        {
            return new FxDetailedAccount( AccountID,
                                            AccountKind,
                                            AccountName,
                                            AmountLimit,
                                            Balance,
                                            BaseUnitSize,
                                            LastMarginCallDate,
                                            LeverageProfileID,
                                            M2MEquity,
                                            MaintenanceFlag,
                                            MaintenanceType,
                                            ManagerAccountID,
                                            MarginCallFlag,
                                            NonTradeEquity,
                                            UsedMargin,
                                            UsedMargin3,
                                            SubAccountType,
                                            AccountCurrency
                                         );
        }

        public FxDetailedAccount( string accountId,
                                    string accountKind,
                                    string accountName,
                                    int amountLimit,
                                    double balance,
                                    int baseUnitSize,
                                    DateTime lastMarginCallDate,
                                    string leverageProfileID,
                                    double m2MEquity,
                                    bool maintenanceFlag,
                                    string maintenanceType,
                                    string managerAccountID,
                                    string marginCallFlag,
                                    double nonTradeEquity,
                                    double usedMargin,
                                    double usedMargin3
                          )
        {
            AccountID = accountId;
            AccountKind = accountKind;
            AccountName = accountName;
            AmountLimit = amountLimit;
            Balance = balance;
            BaseUnitSize = baseUnitSize;
            LastMarginCallDate = lastMarginCallDate;
            LeverageProfileID = leverageProfileID;
            M2MEquity = m2MEquity;
            MaintenanceFlag = maintenanceFlag;
            MaintenanceType = maintenanceType;
            ManagerAccountID = managerAccountID;
            MarginCallFlag = marginCallFlag;
            NonTradeEquity = nonTradeEquity;
            UsedMargin = usedMargin;
            UsedMargin3 = usedMargin3;
            SubAccountType = 1;
            AccountCurrency = "USD";
        }

        public FxDetailedAccount( string accountId,
                                    string accountKind,
                                    string accountName,
                                    int amountLimit,
                                    double balance,
                                    int baseUnitSize,
                                    DateTime lastMarginCallDate,
                                    string leverageProfileID,
                                    double m2MEquity,
                                    bool maintenanceFlag,
                                    string maintenanceType,
                                    string managerAccountID,
                                    string marginCallFlag,
                                    double nonTradeEquity,
                                    double usedMargin,
                                    double usedMargin3,
                                    int subAccountType,
                                    string accountCurrency
                          )
        {
            AccountID = accountId;
            AccountKind = accountKind;
            AccountName = accountName;
            AmountLimit = amountLimit;
            Balance = balance;
            BaseUnitSize = baseUnitSize;
            LastMarginCallDate = lastMarginCallDate;
            LeverageProfileID = leverageProfileID;
            M2MEquity = m2MEquity;
            MaintenanceFlag = maintenanceFlag;
            MaintenanceType = maintenanceType;
            ManagerAccountID = managerAccountID;
            MarginCallFlag = marginCallFlag;
            NonTradeEquity = nonTradeEquity;
            UsedMargin = usedMargin;
            UsedMargin3 = usedMargin3;
            SubAccountType = subAccountType;
            AccountCurrency = accountCurrency;
        }

        public void CopyFrom( IDetailedAccount other )
        {
            AccountID = other.AccountID;
            AccountKind = other.AccountKind;
            AccountName = other.AccountName;
            AmountLimit = other.AmountLimit;
            Balance = other.Balance;
            BaseUnitSize = other.BaseUnitSize;
            LastMarginCallDate = other.LastMarginCallDate;
            LeverageProfileID = other.LeverageProfileID;
            M2MEquity = other.M2MEquity;
            MaintenanceFlag = other.MaintenanceFlag;
            MaintenanceType = other.MaintenanceType;
            ManagerAccountID = other.ManagerAccountID;
            MarginCallFlag = other.MarginCallFlag;
            NonTradeEquity = other.NonTradeEquity;
            UsedMargin = other.UsedMargin;
            UsedMargin3 = other.UsedMargin3;
            SubAccountType = other.SubAccountType;
            AccountCurrency = other.AccountCurrency;
            MarginLeverage = other.MarginLeverage;

        }
    }
}