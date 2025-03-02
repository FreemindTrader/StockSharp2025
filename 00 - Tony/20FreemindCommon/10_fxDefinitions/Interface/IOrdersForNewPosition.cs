using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace fx.Definitions
{
    /// <summary>
    /// This is what the UI looks like for the order.
    /// 
    /// </summary>
    public interface IOrdersForNewPosition
    {   
        // Once a once is formed, the following will not change
        string      AccountId     { get;set;   }
        string      AccountName   { get;set;   }                     
        string      OrderId       { get; set;  }        
        FxOrderType OrderType     { get; set;  }
        ISymbol      Symbol       { get;set;   }       

        // The following properties will change according to the system or the 
        string      Status        { get; set;  }        
        int         Amount        { get; set;  }        
        double?      SellPrice    { get; set;  }
        double?      BuyPrice     { get; set;  }
        double?     StopPrice     { get; set;  }
        double?     LimitPrice    { get; set;  }        
        DateTime    OrderTime     { get; set;  }
        DateTime    ExpireDate    { get; set;  }
        string      Comment       { get; set;  }       
        IDetailedOrder ThisOrder  { get; set; }
        IDetailedOrder StopOrder  { get; set; }
        IDetailedOrder LimitOrder { get; set; }

        string TradeId            { get; set; }

        IOrdersForNewPosition Clone( );
    }
}
