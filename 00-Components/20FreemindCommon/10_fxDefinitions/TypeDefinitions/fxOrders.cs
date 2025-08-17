using System;


namespace fx.Definitions
{
    public enum FxOrderSide
    {
        INVALID,
        Buy,
        Sell,
        Both,
        LongSafetyNet,
        ShortSafetyNet
    }

    public enum ChangeOrderType
    {
        INVALID,
        NEW,
        MODIFIED,
        CANCELLED
    }

    public enum FxOrderType
    {
        Unknown,

        OpenMarket,             // An open market order opens a position at any currently available market rate.
        Open,                   // An open order opens a position at the specified market rate in case such rate is available on the market.
        OpenLimit,              // An open order opens a position at the specified market rate or at a more favorable rate in case such rate is available on the market.
        OpenRange,              // An open range order opens a position at the available market rate in case this rate is in the range specified in the command.

        CloseMarket,            //A close market order closes a position at any currently available market rate. Please note that close orders must be permitted for the account in order to be used.
        Close,                  // A close order closes a position at the specified market rate in case such rate is available on the market.
        CloseLimit,             // A close order closes a position at the specified market rate or at a more favorable price in case such rate is available on the market.
        CloseRange,             // A close range order closes a position at the available market rate in case this rate is in the range specified in the command.

        EntryStop,              // A regular entry order opens a position when the specified market condition is met.
                                // A netting entry order closes all positions for the specified instrument and account which are in the direction (buy or sell) opposite to the direction of the entry order.
                                // A stop entry order with a sell direction is filled when the market is below the rate specified in the order.
                                // A stop entry order with a buy direction is filled when the market is above the rate specified in the order.

        EntryLimit,             // A regular entry order opens a position when the specified market condition is met.
                                // A netting entry order closes all positions for the specified instrument and account which are in the direction (buy or sell) opposite to the direction of the entry order.
                                // A limit entry order with a sell direction is filled when the market is above the rate specified in the order.
                                // A limit entry order with a buy direction is filled when the market is below the rate specified in the order.

        Entry,
                                //A regular entry order opens a position when the specified market condition is met.
                                //Please note that if hedging is disabled for the account, the order, first, closes existing opposite positions for the same account and instrument and only then opens a new position for the remaining amount.
                                //A netting entry order closes all positions for the specified instrument and account which are in the direction (buy or sell) opposite to the direction of the entry order.
                                //There are two types of Entry orders : Limit Entry and Stop Entry. This command allows you to create an entry order without specifying order type. The system will determine order type automatically, based on three parameters:
                                //      Order direction (Buy or Sell).
                                //      Desired order rate.
                                //      Current market price of a trading instrument.

                                //The system will create a Limit Entry order if:
                                //      Rate for a buy order is below current market price.
                                //      Rate for a sell order is above current market price.

                                //The system will create a Stop Entry order if:
                                //      Rate for a buy order is above current market price.
                                //      Rate for a sell order is below current market price.
                                //
        
        Limit,                  //  A limit order is used for locking in profit of the existing position when the market condition is met.
                                //  Limit orders can be created for existing trades as well as for existing entry orders. Limit orders created for entry orders remain inactive until the trade is created by the entry order.
                                //  Only one limit order can be attached to a position or an entry order.
                                //  Please note that close orders must be permitted for the account in order to be used.
                                //  Please note that stop and limit orders cannot be attached to netting entry orders.


        Stop,                   //  A stop order is used for limiting losses of the existing position when the market condition is met.
                                //  Stop orders can be created for existing trades as well as for existing entry orders. Stop orders created for entry orders remain inactive until the trade is created by the entry order.
                                //  Only one stop order can be attached to a position or an the entry order.
                                //  Please note that close orders must be permitted for the account in order to be used.
                                //  Please note that stop and limit orders cannot be attached to netting entry orders.
   
        TrailingStop,               
        TrailingEntryStop,
        TrailingEntryLimit,        
        MarginCall,
        SafetyNet,
        SpecificOrder,
        All
    }

    
    public enum FxPositionType
    {
        NetShortHedge  = -99,
        ShortPosition3 = -3,
        ShortPosition2 = -2,
        ShortPosition1 = -1,               
        Unknown        = 0,
        LongPosition1  = 1,
        LongPosition2  = 2,
        LongPosition3  = 3,
        NetLongHedge   = 99,
    }

}