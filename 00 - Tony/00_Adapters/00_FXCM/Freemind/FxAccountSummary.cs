using System;
using System.Collections.Generic; 
using System.Text;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public interface IAccountSummary : IAccountSummaryCalculatedValue
    {
        string MainLoginName { get; set; }
        string AccountName { get; set; }

        double Balance { get; set; }
        double BeginningEquity { get; set; }
        double UsedMargin { get; set; }
        double UsedMaintMargin { get; set; }
        string MarginCallFlag { get; set; }
        string MaintenanceType { get; set; }
        int SubAccountType { get; set; }
        string AccountCurrency { get; set; }

        IAccountSummaryCalculatedValue CalculatedValue { get; set; }        


        void CopyFrom( string mainLogin, IDetailedAccount other );


        IDetailedAccount ThisAccount { get; set; }
    }


    public interface IAccountSummaryCalculatedValue
    {
        double Equity { get; set; }
        double DayPl { get; set; }
        double GrossPl { get; set; }

        double UsableMargin { get; set; }               // is Usable Margin. All positions are automatically liquidated when this reaches zero.
        double PreLiquidationPercentage { get; set; }
        double UsableMaintMargin { get; set; }  // is Usable Maintenance Margin. This is the margin deposit available for opening new positions. A Maintenance Margin Warning is triggered when this reaches zero.
        double PreMarginCallPercentage { get; set; }

        void SetParent( IAccountSummary parent );
    }
    public class FxAccountSummary : IAccountSummary, IEquatable<FxAccountSummary>
    {
        private IAccountSummaryCalculatedValue _calculatedValue;        
        private string                         _login           = String.Empty;
        private string                         _account         = String.Empty;
        private double                         _balance         = 0d;
        private double                         _beginningEquity = 0d;
        private double                         _usedMargin      = 0d;
        private double                         _usedMaintMargin = 0d;
        private string                         _marginCallFlag  = String.Empty;
        private string                         _maintenanceType = String.Empty;
        private int                            _subaccountType  = 1;
        private string                         _accountCurrency = "USD";
        private bool _smartMargin = false;



        

        public FxAccountSummary( IDetailedAccount myAccount )
        {
            ThisAccount     = myAccount;

            Balance         = myAccount.Balance;
            AccountName     = myAccount.AccountName;
            UsedMargin      = myAccount.UsedMargin;
            UsedMaintMargin = myAccount.UsedMargin;
            BeginningEquity = myAccount.M2MEquity;
            MarginCallFlag  = myAccount.MarginCallFlag;
            MaintenanceType = myAccount.MaintenanceType;
            SubAccountType  = myAccount.SubAccountType;
            AccountCurrency = myAccount.AccountCurrency;
            SmartMargin     = GFMgr.HasSmartMargin( myAccount.LeverageProfileID );
        }


        public void SetParent( IAccountSummary parent )
        {
        }


        public string MainLoginName
        {
            get { return _login; }
            set
            {
                if ( _login == value )
                    return;
                _login = value;                
            }
        }

        public string AccountName
        {
            get { return _account; }
            set
            {
                if ( _account == value )
                    return;
                _account = value;                
            }
        }


        public bool SmartMargin
        {
            get { return _smartMargin; }
            set
            {
                if ( _smartMargin == value )
                    return;
                _smartMargin = value;                
            }
        }


        public double Balance
        {
            get { return _balance; }
            set
            {
                if ( _balance == value )
                    return;
                _balance = value;                
            }
        }


        public double BeginningEquity
        {
            get { return _beginningEquity; }
            set
            {
                if ( _beginningEquity == value )
                    return;

                _beginningEquity = value;                
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



        public double UsedMaintMargin
        {
            get { return _usedMaintMargin; }
            set
            {
                if ( _usedMaintMargin == value )
                    return;
                _usedMaintMargin = value;                
            }
        }



        public string MarginCallFlag
        {
            get { return _marginCallFlag; }
            set
            {
                if ( _marginCallFlag == value )
                    return;
                _marginCallFlag = value;                
            }
        }



        public string MaintenanceType
        {
            get { return _maintenanceType; }

            set
            {
                if ( _maintenanceType == value )
                    return;
                _maintenanceType = value;                
            }
        }

        public string AccountCurrency
        {
            get { return _accountCurrency; }

            set
            {
                if ( _accountCurrency == value )
                    return;

                _accountCurrency = value;                
            }
        }

        public int SubAccountType
        {
            get { return _subaccountType; }

            set
            {
                if ( _subaccountType == value )
                    return;

                _subaccountType = value;                
            }
        }


        public bool Equals( FxAccountSummary other )
        {
            if ( ReferenceEquals( null, other ) ) return false;
            if ( ReferenceEquals( this, other ) ) return true;
            return string.Equals( AccountName, other.AccountName ) && Equity.Equals( other.Equity ) && DayPl.Equals( other.DayPl ) && UsedMargin.Equals( other.UsedMargin ) && UsableMargin.Equals( other.UsableMargin ) && PreLiquidationPercentage.Equals( other.PreLiquidationPercentage ) && UsedMaintMargin.Equals( other.UsedMaintMargin ) && UsableMaintMargin.Equals( other.UsableMaintMargin ) && PreMarginCallPercentage.Equals( other.PreMarginCallPercentage ) && GrossPl.Equals( other.GrossPl ) && string.Equals( MarginCallFlag, other.MarginCallFlag ) && string.Equals( MaintenanceType, other.MaintenanceType );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) ) return false;
            if ( ReferenceEquals( this, obj ) ) return true;
            if ( obj.GetType( ) != GetType( ) ) return false;
            return Equals( ( FxAccountSummary ) obj );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                var hashCode = ( AccountName != null ? AccountName.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ Equity.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ DayPl.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ UsedMargin.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ UsableMargin.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ PreLiquidationPercentage.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ UsedMaintMargin.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ UsableMaintMargin.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ PreMarginCallPercentage.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ GrossPl.GetHashCode( );
                hashCode = ( hashCode * 397 ) ^ ( MarginCallFlag != null ? MarginCallFlag.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( MaintenanceType != null ? MaintenanceType.GetHashCode( ) : 0 );

                return hashCode;
            }
        }

        public IAccountSummaryCalculatedValue CalculatedValue
        {
            get { return _calculatedValue; }

            set
            {
                _calculatedValue = value;

                _calculatedValue.SetParent( this );                
            }
        }

        

        

        public void UpdateMargin( string mainLogin, string accountName )
        {

        }

        public void CopyFrom( string mainLogin, IDetailedAccount other )
        {
            if ( !string.Equals( AccountName, other.AccountName ) )
            {
                AccountName = other.AccountName;
            }

            if ( Balance != other.Balance )
            {
                Balance = other.Balance;
            }

            if ( BeginningEquity != other.M2MEquity )
            {
                BeginningEquity = other.M2MEquity;
            }

            if ( UsedMargin != other.UsedMargin )
            {
                UsedMargin = other.UsedMargin;
                UsedMaintMargin = other.UsedMargin;
            }

            if ( !string.Equals( MarginCallFlag, other.MarginCallFlag ) )
            {
                MarginCallFlag = other.MarginCallFlag;
            }

            if ( !string.Equals( MaintenanceType, other.MaintenanceType ) )
            {
                MaintenanceType = other.MaintenanceType;
            }

            if ( !string.Equals( AccountCurrency, other.AccountCurrency ) )
            {
                AccountCurrency = other.AccountCurrency;
            }

            if ( SubAccountType != other.SubAccountType )
            {
                SubAccountType = other.SubAccountType;
            }

            if ( _calculatedValue != null )
            {
                var calcValue                         = new AccountSummaryCalculatedValue( );
                calcValue.GrossPl                     = 0;
                calcValue.DayPl                       = Math.Round( ( Balance - BeginningEquity ), 2 );
                calcValue.Equity                      = Math.Round( ( Balance ), 2 );
                calcValue.UsableMargin                = calcValue.Equity - UsedMargin;
                calcValue.UsableMaintMargin           = calcValue.Equity - UsedMaintMargin;
                calcValue.PreMarginCallPercentage     = Math.Round( ( calcValue.UsableMargin / calcValue.Equity * 100 ), 2 );
                calcValue.PreLiquidationPercentage    = Math.Round( ( calcValue.UsableMargin / calcValue.Equity * 100 ), 2 );

                CalculatedValue                  = calcValue;

            }
        }


        public IAccountSummary Clone( )
        {
            var output                         =  new FxAccountSummary( );

            var calcValue                      = new AccountSummaryCalculatedValue( );

            calcValue.GrossPl                  = _calculatedValue.GrossPl;
            calcValue.DayPl                    = _calculatedValue.DayPl;
            calcValue.Equity                   = _calculatedValue.Equity;
            calcValue.UsableMargin             = _calculatedValue.UsableMaintMargin;
            calcValue.UsableMaintMargin        = _calculatedValue.UsableMaintMargin;
            calcValue.PreMarginCallPercentage  = _calculatedValue.PreMarginCallPercentage;
            calcValue.PreLiquidationPercentage = _calculatedValue.PreLiquidationPercentage;

            output._login                      = _login;
            output._account                    = _account;
            output._balance                    = _balance;
            output._beginningEquity            = _beginningEquity;
            output._usedMargin                 = _usedMargin;
            output._usedMaintMargin            = _usedMaintMargin;
            output._marginCallFlag             = _marginCallFlag;
            output._maintenanceType            = _maintenanceType;
            output._subaccountType             = _subaccountType;
            output._accountCurrency            = _accountCurrency;
            output._smartMargin                = _smartMargin;
            output._calculatedValue            = calcValue;

            return output;
        }



        public IDetailedAccount ThisAccount { get; set; }

        #region Calculateded Value
        public double Equity
        {
            get
            {
                if ( _calculatedValue != null )
                {
                    return _calculatedValue.Equity;
                }

                return 0;
            }

            set {; }
        }
        public double DayPl
        {
            get
            {
                if ( _calculatedValue != null )
                {
                    return _calculatedValue.DayPl;
                }

                return 0;
            }

            set {; }
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

        public double UsableMargin
        {
            get
            {
                if ( _calculatedValue != null )
                {
                    return _calculatedValue.UsableMargin;
                }

                return 0;
            }

            set {; }
        }

        public double PreLiquidationPercentage
        {
            get
            {
                if ( _calculatedValue != null )
                {
                    return _calculatedValue.PreLiquidationPercentage;
                }

                return 0;
            }

            set {; }
        }

        public double UsableMaintMargin
        {
            get
            {
                if ( _calculatedValue != null )
                {
                    return _calculatedValue.UsableMaintMargin;
                }

                return 0;
            }

            set {; }
        }

        public double PreMarginCallPercentage
        {
            get
            {
                if ( _calculatedValue != null )
                {
                    return _calculatedValue.UsableMaintMargin;
                }

                return 0;
            }

            set {; }
        }
        #endregion

        public FxAccountSummary( )
        {
            _calculatedValue = new AccountSummaryCalculatedValue( this );
        }        
    }

    public class AccountSummaryCalculatedValue : IAccountSummaryCalculatedValue
    {
        private IAccountSummary _parent;

        public AccountSummaryCalculatedValue( )
        {

        }

        public AccountSummaryCalculatedValue( FxAccountSummary parent )
        {
            _parent = parent;
        }

        public void SetParent( IAccountSummary parent )
        {
            _parent = parent;
        }


        public double Equity { get; set; }
        public double DayPl { get; set; }
        public double GrossPl { get; set; }

        public double UsableMargin { get; set; }
        public double PreLiquidationPercentage { get; set; }
        public double UsableMaintMargin { get; set; }
        public double PreMarginCallPercentage { get; set; }

    }
}
