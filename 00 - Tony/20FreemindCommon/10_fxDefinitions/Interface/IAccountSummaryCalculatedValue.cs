using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IAccountSummaryCalculatedValue 
    {
        double Equity                           { get; set; }  
        double DayPl                            { get; set; }  
        double GrossPl                          { get; set; }

        double UsableMargin                     { get; set; }               // is Usable Margin. All positions are automatically liquidated when this reaches zero.
        double PreLiquidationPercentage           { get; set; }  
        double UsableMaintMargin                { get; set; }  // is Usable Maintenance Margin. This is the margin deposit available for opening new positions. A Maintenance Margin Warning is triggered when this reaches zero.
        double PreMarginCallPercentage      { get; set; }          

        void SetParent( IAccountSummary parent );
    }
}
