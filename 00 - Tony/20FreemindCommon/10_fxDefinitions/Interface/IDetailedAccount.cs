using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IDetailedAccount
    {        
        string  AccountID            { get; set; } 

        string  AccountKind          { get; set; } 
        string  AccountName          { get; set; }

        string AccountType { get; set; }

        int     AmountLimit          { get; set; }

        double  Balance              { get; set; }

        int     BaseUnitSize         { get; set; }

        DateTime  LastMarginCallDate { get; set; }

        string  LeverageProfileID    { get; set; }

        int MarginLeverage { get; set; }

        double  M2MEquity            { get; set; }

        bool    MaintenanceFlag      { get; set; }
        string  MaintenanceType      { get; set; }

        string  ManagerAccountID     { get; set; }

        string  MarginCallFlag       { get; set; }

        double  NonTradeEquity       { get; set; }

        double  UsedMargin           { get; set; }

        double  UsedMargin3          { get; set; }

        int     SubAccountType       { get; set; }

        string  AccountCurrency       { get; set; }        

        void CopyFrom( IDetailedAccount other );
       IDetailedAccount Clone( );
    }
}