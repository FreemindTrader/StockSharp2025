using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public interface IClosedTrade : ITrade 
    {               
        string  CloseOrderID         { get; }

        string  CloseOrderParties    { get; }

        string  CloseOrderReqID      { get; }

        string  CloseOrderRequestTXT { get; }

        string  CloseQuoteID         { get;  } 

        double  CloseRate            { get; }
        DateTime  CloseTime          { get; }

        double  GrossPL              { get; }    
        
        string  TradeIDRemain        { get; }    

        IClosedTrade Clone( );
    }
}
