using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace fx.Definitions
{
    public interface IAccountSummary : IAccountSummaryCalculatedValue
    {
        string MainLoginName                           { get; set; }
        string AccountName                                 { get; set; }

        double Balance                                 { get; set; }
        double BeginningEquity                         { get; set; }
        double UsedMargin                              { get; set; }
        double UsedMaintMargin                         { get; set; }
        string MarginCallFlag                          { get; set; }
        string MaintenanceType                         { get; set; }
        int    SubAccountType                          { get; set; }
        string AccountCurrency                         { get; set; }
        
        IAccountSummaryCalculatedValue CalculatedValue { get; set; }

        event PropertyChangedEventHandler PropertyChanged;


        void CopyFrom( string mainLogin, IDetailedAccount other );
        

        IDetailedAccount ThisAccount  { get; set; }
    }
}
