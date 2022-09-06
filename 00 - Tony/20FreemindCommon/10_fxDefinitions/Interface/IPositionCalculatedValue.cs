using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IPositionCalculatedValue
    {
        double? SellNetPl         { get; set;  }  //The current profit/loss of all short (sell) positions. It is expressed in the account currency. It does not include commissions and interests. If no short positions exist for the instrument, the value of this field is 0.0.
        double? SellClose         { get; set; }  //The current market price, at which long (buy) positions can be closed. In the case of FX instruments, it is expressed in the instrument counter currency per one unit of base currency. In the case of CFD instruments, it is expressed in the instrument native currency per one contract. If no long positions exist for the instrument, the value of this field is 0.0.
        double? BuyNetPl          { get; set; }  //The current profit/loss of all long (buy) positions. It is expressed in the account currency. It does not include commissions and interests. If no long positions exist for the instrument, the value of this field is 0.0.
        double? BuyClose          { get; set; }  // The current market price, at which short (sell) positions can be closed. In the case of FX instruments, it is expressed in the instrument counter currency per one unit of base currency. In the case of CFD instruments, it is expressed in the instrument native currency per one contract. If no short positions exist for the instrument, the value of this field is 0.0.        
        double GrossPl            { get; set; }  // The current profit/loss of all positions (both long and short). It is expressed in the account currency. It does not include commissions and interests. 
        double NetPl              { get; set; }  // The current profit/loss of all positions (both long and short). It is expressed in the account currency. It includes commissions and interests.

        int? PendingSellVolume    { get; set; }   
        
        int? PendingBuyVolume     { get; set; }     
        
        double? Basis             { get; set; } 

        double? MarketValue       { get; set; }

        void SetParent( IPositionsSummary parent );

    }
}
