using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using fx.Collections;

namespace fx.Definitions
{    
    public interface IPositionsSummary :IPositionCalculatedValue
    {
        string MainLoginName { get; set; }  // The unique identification number of the instrument traded.
        string AccountName                       { get; set; }  // The unique identification number of the instrument traded.
        string  OfferId                          { get; set; }  // The unique identification number of the instrument traded.
        int     DefaultSortOrder                 { get; set; }  // The sequence number of the instrument. It defines the instrument place in the dealing rates list of the FX Trading Station.
        string  Symbol                           { get; set; }  //The symbol of the instrument traded. For example, EUR/USD, USD/JPY, GBP/USD.        
        double? SellAvgOpen                      { get; set; }  //The average open price of short (sell) positions. In the case of FX instruments, it is expressed in the instrument counter currency per one unit of base currency. In the case of CFD instruments, it is expressed in the instrument native currency per one contract. If no short positions exist for the instrument, the value of this field is 0.0.
        
        double? SellAmount                       { get; set; }  //The amount of short (sell) positions. In the case of FX instruments, it is expressed in the instrument base currency. In the case of CFD instruments, it is expressed in contracts. If short positions exist for the instrument traded, the value of this field is positive. Otherwise, the value of this field is 0.0.
        
        double? BuyAmount                        { get; set; }  //The amount of long (buy) positions. In the case of FX instruments, it is expressed in the instrument base currency. In the case of CFD instruments, it is expressed in contracts. If long positions exist for the instrument traded, the value of this field is positive. Otherwise, the value of this field is 0.0.         
                
        double? BuyAvgOpen                       { get; set; }  //The average open price of long (buy) positions. In the case of FX instruments, it is expressed in the instrument counter currency per one unit of base currency. In the case of CFD instruments, it is expressed in the instrument native currency per one contract. If no long positions exist for the instrument, the value of this field is 0.0.
        
        double Amount                            { get; set; }  //The amount of all positions (both long and short). In the case of FX instruments, it is expressed in the instrument base currency. In the case of CFD instruments, it is expressed in contracts. If the amount of long positions exceeds the amount of short positions for the instrument traded, the value of this field is positive. If the amount of short positions exceeds the amount of long positions for the instrument traded, the value of this field is negative.
                

        // -------------------- The following are calcuated values ----------------------------------------

        double? Result                           { get; set; }
        
        double? Commission                       { get; set; }

        IPositionCalculatedValue CalculatedValue { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        void CopyFrom( IPositionsSummary other );

        void AddDetailPositionInfos( PooledList< IOpenPositionAndOrders > detailInfos );

        void AddDetailPositionInfos( IOpenPositionAndOrders detailPendingOrdersInfo );

        IList< IOpenPositionAndOrders > DetailPositionInfos { get; }        
    }
}
