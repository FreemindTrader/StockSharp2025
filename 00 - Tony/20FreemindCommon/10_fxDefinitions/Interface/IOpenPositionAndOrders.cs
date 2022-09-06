using System;
using System.Collections.Generic;
using System.Linq;



namespace fx.Definitions
{
    /// <summary>
    /// This is the interface for the one individual Position with the Pending Limit and Stop
    /// </summary>
    public interface IOpenPositionAndOrders 
    {
        string      AccountID                         { get; set;  }
        string      AccountName                       { get; set;  }
        string MainLoginName { get; set; }
        int         Amount                            { get; set;  }
        string      BuySell                           { get; set;  }
        string      Comment                           { get; set;  }
        double?     ClosePrice                        { get; set;  }
        double      Commission                        { get; set;  }
        double      GrossProfit                       { get; set;  }
        double?     LimitPrice                        { get; set;  }
        double      OpenPrice                         { get; set;  }
        DateTime    OpenTime                          { get; set;  }
        double      PipProfit                         { get; set;  }
        double      RolloverInterest                  { get; set;  }
        int?        StopMove                          { get; set;  }
        double?     StopPrice                         { get; set;  }
        string      Symbol                            { get; set;  }
        string      Ticket                            { get; set;  }
        double      UsedMargin                        { get; set;  }
        bool        IsBuy                             { get;       }

        SymbolGroup SymbolGroup                       { get;       }

        IDetailedOrder ThisOrder                      { get; set;  }
        IDetailedOrder StopOrder                      { get; set;  }
        IDetailedOrder LimitOrder                     { get; set;  }        

        void RaiseStopLimitChanged( );

        IPositionOrderCalculatedValue CalculatedValue { get; set; }

        string      GetPositionType( );
        IOpenPositionAndOrders Clone( );

        bool Equals( IDetailedPosition other );


    }
}
