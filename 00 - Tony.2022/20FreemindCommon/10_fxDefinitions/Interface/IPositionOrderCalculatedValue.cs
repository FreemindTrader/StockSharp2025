using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    
    public interface IPositionOrderCalculatedValue
    {               
        double? ClosePrice { get; set;  }  //The current profit/loss of all short (sell) positions. It is expressed in the account currency. It does not include commissions and interests. If no short positions exist for the instrument, the value of this field is 0.0.
        double GrossProfit { get; set; }  // The current profit/loss of all positions (both long and short). It is expressed in the account currency. It does not include commissions and interests. 
        double PipProfit   { get; set; }  // The current profit/loss of all positions (both long and short). It is expressed in the account currency. It includes commissions and interests.     
        void SetParent( IOpenPositionAndOrders parent );

    }
}
