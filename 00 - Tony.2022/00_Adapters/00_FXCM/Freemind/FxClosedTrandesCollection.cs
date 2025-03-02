using Ecng.Collections;
using Ecng.Common;
using System.Linq;
using fxcore2;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Text;


namespace StockSharp.FxConnectFXCM.Freemind
{
    public class FxClosedTradesCollection //: IClosedTradesCollection
    {
        #region Data fields
        private SynchronizedDictionary< string, List< string > >  _instrumentToTradeIdCollection = new SynchronizedDictionary< string, List< string > >( );
        private SynchronizedDictionary< string, DbClosedTrade >   _tradeIdToPosition             = new SynchronizedDictionary< string, DbClosedTrade >( );
        private List< DbClosedTrade >                             _allClosedTrades               = new List< DbClosedTrade >( );

        #endregion

        #region IOfferCollection members

        public int Count => _allClosedTrades.Count;

        public IList<DbClosedTrade> AsIList
        {
            get
            {
                return _allClosedTrades;
            }
        }

        public List<DbClosedTrade> this[ string instrument ]
        {
            get
            {
                if ( _instrumentToTradeIdCollection.ContainsKey( instrument ) )
                {
                    List< DbClosedTrade > foundResult = new List< DbClosedTrade >( );

                    List< string > tradeIDs = _instrumentToTradeIdCollection[ instrument ];

                    foreach ( var tradeId in tradeIDs )
                    {
                        if ( _tradeIdToPosition.ContainsKey( tradeId ) )
                        {
                            foundResult.Add( _tradeIdToPosition[ tradeId ] );
                        }
                    }

                    return foundResult;
                }


                return null;
            }
        }
        public List<ExecutionMessage> GetClosedTradesMessage( string mainLoginName, string accountName )
        {            
            List< ExecutionMessage > msgs = new List< ExecutionMessage >( );

            SubaccountTradeDataRepo tradeMgr = GFMgr.CreateSubaccountTradeDataRepoByAccountName( mainLoginName, accountName );

            for ( int i = 0; i < _allClosedTrades.Count; ++i )
            {
                var pos = _allClosedTrades[ i ];

                if ( string.Equals( pos.MainLoginName, mainLoginName, StringComparison.CurrentCultureIgnoreCase ) &&
                     string.Equals( pos.AccountName, accountName, StringComparison.CurrentCultureIgnoreCase )
                   )
                {
                    var msg                   = new ExecutionMessage( );
                    msg.DataTypeEx         = DataType.Transactions;
                    msg.SecurityId            = pos.OfferID.ToSecurityId( );                    

                    var customId = pos.OpenOrderRequestTXT.Split(',').Select(x => x.Split('=')).Where(x => x.Length > 1 && !String.IsNullOrEmpty(x[0].Trim()) && !String.IsNullOrEmpty(x[1].Trim())).ToDictionary(x => x[0].Trim(), x => x[1].Trim());

                    long myRequestId = 0;

                    if ( customId.ContainsKey( "ID" ) )
                    {
                        myRequestId = customId[ "ID" ].To<int>( );
                    }

                    msg.OriginalTransactionId = myRequestId;
                    msg.ServerTime            = pos.CloseTime;
                    msg.PortfolioName         = pos.AccountName;
                    msg.Side                  = pos.BuySell.ToSides( );

                    msg.HasOrderInfo          = true;
                    msg.OrderStringId         = pos.CloseOrderID;
                    msg.OrderPrice            = ( decimal ) pos.OpenRate;

                    msg.HasTradeInfo          = true;
                    msg.TradeId               = new long?( pos.TradeID.To<long>( ) );
                    msg.TradeStringId         = pos.TradeID;
                    msg.TradePrice            = ( decimal ) pos.CloseRate;
                    msg.PnL                   = ( decimal ) pos.GrossPL;
                    msg.PositionEffect        = OrderPositionEffects.CloseOnly;
                    msg.Position              = tradeMgr.GetPositionForSymbol( accountName, pos.OfferID );

                    var subscribedSymbol = GFMgr.GetSubscribedSymbolsByAccountNameAndId( mainLoginName, pos.OfferID.To<int>() );

                    int baseUnit = 1;

                    if ( subscribedSymbol != null )
                    {
                        baseUnit = subscribedSymbol.BaseUnitSize;
                    }

                    msg.TradeVolume           = new decimal?( ( ( decimal ) pos.Amount ) / baseUnit );
                    msg.Commission            = ( decimal ) pos.Commission;
                    msg.Yield                 = ( decimal ) pos.RolloverInterest;

                    msgs.Add( msg );
                }
            }



            return msgs;
        }
        



        #endregion

        /// <summary>
        /// Adds a new offer
        /// </summary>
        /// <param name="offerid">OfferId</param>
        /// <param name="instrument">The name of the instrument</param>
        /// <param name="lastChange">The date/time of the last change of the offer</param>
        /// <param name="bid">The current bid price</param>
        /// <param name="ask">The current ask price</param>
        /// <param name="minuteVolume">The current accumulated minute tick volume</param>
        /// <param name="digits">The precision</param>
        public void Add( string mainLoginName, string symbolName, O2GClosedTradeRow row )
        {
            DbClosedTrade fxPosition = new DbClosedTrade( mainLoginName, row );

            var tradeId = row.TradeID;

            if ( _instrumentToTradeIdCollection.ContainsKey( symbolName ) )
            {
                if ( !_tradeIdToPosition.ContainsKey( tradeId ) )
                {
                    // Since we don't have This TradeID, so we addd this tradeID to instrument map and also put it into all Positions
                    List< string >  tradeIDs = _instrumentToTradeIdCollection[ symbolName ];

                    tradeIDs.Add( tradeId );

                    _allClosedTrades.Add( fxPosition );

                    _tradeIdToPosition.TryAdd2( tradeId, fxPosition );
                }
                else
                {
                    // Since this trade ID exist, we need to replace it in the _allClosedTrades                    
                    int index = _allClosedTrades.FindIndex( x => x.TradeID == tradeId );

                    if ( index >= 0 )
                    {
                        DbClosedTrade foundItem = _allClosedTrades[ index ];

                        if ( !foundItem.Equals( fxPosition ) )
                        {
                            _allClosedTrades[ index ] = fxPosition;

                            _tradeIdToPosition[ tradeId ] = fxPosition;
                        }
                    }
                }                
            }
            else
            {
                _tradeIdToPosition.TryAdd2( tradeId, fxPosition );

                List< string > tradeIDs = new List< string >();

                tradeIDs.Add( tradeId );

                _instrumentToTradeIdCollection.Add( symbolName, tradeIDs );

                _allClosedTrades.Add( fxPosition );
            }
        }


        
        public void TryUpdateClosedTrades( string mainLoginName, string symbolName, O2GClosedTradeRow row )
        {
            var tradeId = row.TradeID;

            if ( _tradeIdToPosition.ContainsKey( tradeId ) )
            {
                int index = _allClosedTrades.FindIndex( x => x.TradeID == tradeId );

                if ( index >= 0 )
                {
                    DbClosedTrade fxPosition = new DbClosedTrade( mainLoginName, row );
                    DbClosedTrade foundItem = _allClosedTrades[ index ];

                    if ( !foundItem.Equals( fxPosition ) )
                    {
                        _allClosedTrades[ index ] = fxPosition;
                        _tradeIdToPosition.TryAdd2( tradeId, fxPosition );
                    }
                }
            }
        }
        /// <summary>
        /// Removes all offers from the collection
        /// </summary>
        public void Clear( )
        {

            _tradeIdToPosition.Clear( );
            _instrumentToTradeIdCollection.Clear( );
            _allClosedTrades.Clear( );
        }
    }
}

