using Ecng.Collections;
using StockSharp.Messages;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fxcore2;
using Ecng.Common;
using fx.Messages;

#pragma warning disable CS0168

namespace StockSharp.FxConnectFXCM.Freemind
{
    

    public class SubaccountTradeDataRepo : ISubaccountTradeDataRepo
    {
        #region Data fields

        private readonly SynchronizedDictionary< string, IDetailedPosition >     _tradeIdToPosition             = new SynchronizedDictionary< string, IDetailedPosition >( );
        private readonly SynchronizedDictionary< string, List< string > >        _instrumentToTradeIdCollection = new SynchronizedDictionary< string, List< string > >( );
        private readonly SynchronizedDictionary< string, IDetailedOrder >        _orderIdToOrder                = new SynchronizedDictionary< string, IDetailedOrder >( );
        private readonly SynchronizedDictionary< string, List< string > >        _instrumentToOrderIdCollection = new SynchronizedDictionary< string, List< string > >( );

        private FxDetailedAccountsCollection                                     _detailedAccountsCollection    = null;

        private string _accountCurrency = string.Empty;

        public static event ItemEventHandler< string >                          PositionBindingListChangedEvent;        // Raised on a thread pool call.
        public static event ItemEventHandler< string >                          OrdersBindingListChangedEvent;          // Raised on a thread pool call.
        public static event ItemEventHandler< string >                          SummaryBindingListChangedEvent;          // Raised on a thread pool call.

        string _mainLoginName = string.Empty;

        double _usableMarginPercentage = 0d;

        public SubaccountTradeDataRepo( string mainLoginName )
        {
            _mainLoginName = mainLoginName;

            //_unitOfWorkFactory = UnitOfWorkSource.GetUnitOfWorkFactory( );

            //_unitOfWork = _unitOfWorkFactory.CreateUnitOfWork( );
            //_subscribedSymbolsRepo = _unitOfWork.SUBSCRIBEDSYMBOLS;

            var accountInfo = GFMgr.GetAccountInfoByAccountName( _mainLoginName );

            if ( accountInfo != null )
            {
                _usableMarginPercentage = accountInfo.UsableMarginPercentage * 100;                
            }

            if ( _usableMarginPercentage == 0 )
            {
                _usableMarginPercentage = 60;
            }
        }

        

        public int OrdersCount( )
        {
            return GFMgr.GetDetailedOrdersBindingList( ).Count;
        }

        //public int PositionsCount( )
        //{
        //    return detailedPositionsBindingList.Count;
        //}

        public List<IDetailedOrder> LimitedOrdersList
        {
            get
            {
                var output = new List< IDetailedOrder >();
                var bindingList = GFMgr.GetDetailedOrdersBindingList( );

                foreach ( var item in bindingList )
                {
                    if ( !item.isMarketOrder( ) )
                    {
                        output.Add( item );
                    }
                }

                return output;
            }
        }



        #endregion

        public void Clear( )
        {
            // _accountsSummaryBindingList.Clear();
            // detailedPositionsBindingList.Clear();
            // fxOpenPositionPendingOrderInfoBindingList.Clear();
            //fxPositionsSummaryBindingList.Clear();

            // detailedOrdersBindingList.Clear( );
            // myOrdersForNewPositionBindingList.Clear();
            _orderIdToOrder.Clear( );
            _instrumentToOrderIdCollection.Clear( );

            _tradeIdToPosition.Clear( );
            _instrumentToTradeIdCollection.Clear( );
        }

        public decimal? GetPositionForSymbol( string accountName, string offerId )
        {
            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

            int index = fxPositionsSummaryBindingList.FindIndex( x => x.OfferId == offerId && x.AccountName == accountName );

            if ( index >= 0 )
            {
                var pos = fxPositionsSummaryBindingList[ index ];

                return pos.Position;
            }
            
            return 0;
        }

        public IDetailedPosition GetPositionCopy( string accountName, string instrument, string tradeId )
        {
            var detailedPosistion = GFMgr.GetDetailedPositionsBindingList( );

            int index = detailedPosistion.FindIndex( x => x.TradeID == tradeId );

            if ( index >= 0 )
            {
                var output = detailedPosistion[ index ].Clone();

                return output;
            }


            return null;
        }


        #region Binding List
        // public FxAccountSummaryBindingList AccountSummaryBindingList                       => _accountsSummaryBindingList;
        // public FxDetailedPositionsBindingList DetailedPositionsBindingList                 => detailedPositionsBindingList;       
        // public FxOpenPositionAndOrdersBindingList OpenPositionsPendingOrderInfoBindingList => fxOpenPositionPendingOrderInfoBindingList;
        //public FxPositionsSummaryBindingList PositionsSummaryBindingList                   => fxPositionsSummaryBindingList;
        // public FxDetailedOrdersBindingList DetailedOrdersBindingList                       => detailedOrdersBindingList;
        // public FxOrdersForNewPositionBindingList OrdersForNewPositionsBinding              => myOrdersForNewPositionBindingList;        

        #endregion



        #region IOfferCollection members


        //public IList<string> GetSymbols( )
        //{
        //    return _instrumentToTradeIdCollection.CopyKeys( );
        //}

        public bool PositionsHaveSymbol( string instrument )
        {
            if ( _instrumentToTradeIdCollection.ContainsKey( instrument ) )
            {
                return true;
            }

            return false;
        }


        //public IDetailedPosition this[ int index ]
        //{
        //    get
        //    {
        //        if ( index > 0 && index < detailedPositionsBindingList.Count )
        //        {
        //            return detailedPositionsBindingList[ index ];    
        //        }

        //        return null;
        //    }
        //}

        //
        //

        //public IList< IDetailedPosition > this[ string instrument ]
        //{
        //    get
        //    {
        //        if ( _instrumentToTradeIdCollection.ContainsKey( instrument ) )
        //        {
        //            FastList< IDetailedPosition > foundResult = new FastList< IDetailedPosition >( );

        //            List< string > tradeIDs = _instrumentToTradeIdCollection[ instrument ];

        //            foreach ( var tradeId in tradeIDs )
        //            {
        //                if ( _tradeIdToPosition.ContainsKey( tradeId ) )
        //                {
        //                    foundResult.Add( _tradeIdToPosition[ tradeId ]);
        //                }
        //            }

        //            return foundResult.AsMutableList();
        //        }
        //        

        //        return null;
        //    }
        //}

        //public IEnumerator< IDetailedPosition > GetEnumerator( )
        //{
        //    return detailedPositionsBindingList.GetEnumerator( );            
        //}
        //
        //IEnumerator IEnumerable.GetEnumerator( )
        //{
        //    return _tradeIdToPosition.GetEnumerator( );
        //}

        #endregion



        public void AddPosition( IDetailedPosition iPosition )
        {
            FxDetailedPosition fxPosition = ( FxDetailedPosition ) iPosition;            

            string symbolName             = GFMgr.GetSymbolFromOfferId( fxPosition.OfferID );
            string tradeId                = fxPosition.TradeID;

            var detailedPositionsBindingList = GFMgr.GetDetailedPositionsBindingList();

            var fxOpenPositionPendingOrderInfoBindingList = GFMgr.GetOpenPositionAndOrdersBindingList( );


            if ( _instrumentToTradeIdCollection.ContainsKey( symbolName ) )
            {
                if ( !_tradeIdToPosition.ContainsKey( tradeId ) )
                {
                    // Since we don't have This TradeID, so we addd this tradeID to instrument map and also put it into all Positions
                    List< string >  tradeIDs = _instrumentToTradeIdCollection[ symbolName ];

                    tradeIDs.Add( tradeId );

                    detailedPositionsBindingList.Add( fxPosition );

                    FxOpenPositionAndOrders openPosition = new FxOpenPositionAndOrders( fxPosition );

                    AddPositionToBindingList( openPosition );
                }
                else
                {
                    // Since this trade ID exist, we need to replace it in the detailedPositionsBindingList                    
                    int index = detailedPositionsBindingList.FindIndex( x => x.TradeID == tradeId );

                    if ( index >= 0 )
                    {
                        IDetailedPosition foundItem = detailedPositionsBindingList[ index ];

                        if ( !foundItem.Equals( fxPosition ) )
                        {
                            detailedPositionsBindingList[ index ] = fxPosition;
                        }
                    }

                    index = fxOpenPositionPendingOrderInfoBindingList.FindIndex( x => x.Ticket == tradeId );

                    if ( index >= 0 )
                    {
                        IOpenPositionAndOrders foundItem = fxOpenPositionPendingOrderInfoBindingList[ index ];

                        if ( !foundItem.Equals( fxPosition ) )
                        {
                            fxOpenPositionPendingOrderInfoBindingList[ index ] = new FxOpenPositionAndOrders( fxPosition );                            
                        }
                    }
                }

                _tradeIdToPosition[ tradeId ] = fxPosition;
            }
            else
            {
                _tradeIdToPosition.Add( tradeId, fxPosition );

                List< string > tradeIDs = new List< string >();

                tradeIDs.Add( tradeId );

                _instrumentToTradeIdCollection.Add( symbolName, tradeIDs );

                detailedPositionsBindingList.Add( fxPosition );

                FxOpenPositionAndOrders openPosition = new FxOpenPositionAndOrders(fxPosition);

                AddPositionToBindingList( openPosition );
            }

            CalculatePositionsSummary( iPosition.AccountName, iPosition.AccountID );
        }

        public void AddPosition( string symbolName,
                                    string mainLoginName,
                                   string accountId,
                                   string accountKind,
                                   string accountName,
                                   int amount,
                                   string buySell,
                                   double commission,
                                   string offerId,
                                   string openOrderId,
                                   string openOrderParties,
                                   string openOrderReqId,
                                   string openOrderRequestTxt,
                                   string openQuoteId,
                                   double openRate,
                                   DateTime openTime,
                                   double rolloverInterest,
                                   string tradeId,
                                   string tradeIdOrigin,
                                   string valueDate,
                                   double dividend

                       )
        {
            FxDetailedPosition fxPosition = new FxDetailedPosition( mainLoginName,
                                                                    accountId,
                                                                    accountKind,
                                                                    accountName,
                                                                    amount,
                                                                    buySell,
                                                                    commission,
                                                                    offerId,
                                                                    openOrderId,
                                                                    openOrderParties,
                                                                    openOrderReqId,
                                                                    openOrderRequestTxt,
                                                                    openQuoteId,
                                                                    openRate,
                                                                    openTime,
                                                                    rolloverInterest,
                                                                    tradeId,
                                                                    tradeIdOrigin,
                                                                    valueDate,
                                                                    dividend );



            AddPosition( fxPosition );
        }

        private void UiTryUpdatePosition( string tradeId, FxDetailedPosition fxPosition )
        {
            var fxOpenPositionPendingOrderInfoBindingList = GFMgr.GetOpenPositionAndOrdersBindingList( );

            var positionBindingListCopy = fxOpenPositionPendingOrderInfoBindingList;

            int index = positionBindingListCopy.FindIndex( x => x.Ticket == tradeId );

            if ( index >= 0 )
            {
                IOpenPositionAndOrders foundItem = positionBindingListCopy[ index ];

                if ( !foundItem.Equals( fxPosition ) )
                {
                    FxOpenPositionAndOrders openPosition = new FxOpenPositionAndOrders(fxPosition);

                    positionBindingListCopy[ index ] = openPosition;
                };
            }
        }


        public void TryUpdatePosition( IDetailedPosition iPosition )
        {
            var detailedPositionsBindingList = GFMgr.GetDetailedPositionsBindingList();

            FxDetailedPosition fxPosition = ( FxDetailedPosition ) iPosition;            

            string tradeId = fxPosition.TradeID;

            if ( _tradeIdToPosition.ContainsKey( tradeId ) )
            {
                var fxOpenPositionPendingOrderInfoBindingList = GFMgr.GetOpenPositionAndOrdersBindingList( );

                int index = detailedPositionsBindingList.FindIndex( x => x.TradeID == tradeId );

                if ( index >= 0 )
                {
                    IDetailedPosition foundItem = detailedPositionsBindingList[ index ];

                    if ( !foundItem.Equals( fxPosition ) )
                    {
                        detailedPositionsBindingList[ index ] = fxPosition;
                        _tradeIdToPosition[ tradeId ] = fxPosition;
                    }
                }

                UiTryUpdatePosition( tradeId, fxPosition );                
            }


        }


        public void TryUpdatePosition(

                                        string symbolName,
                                        string mainLoginName,
                                       string accountId,
                                       string accountKind,
                                       string accountName,
                                       int amount,
                                       string buySell,
                                       double commission,
                                       string offerId,
                                       string openOrderId,
                                       string openOrderParties,
                                       string openOrderReqId,
                                       string openOrderRequestTxt,
                                       string openQuoteId,
                                       double openRate,
                                       DateTime openTime,
                                       double rolloverInterest,
                                       string tradeId,
                                       string tradeIdOrigin,
                                       string valueDate,
                                       double dividend

                       )
        {
            FxDetailedPosition fxPosition = new FxDetailedPosition(
                                                                    mainLoginName,
                                                                    accountId,
                                                                    accountKind,
                                                                    accountName,
                                                                    amount,
                                                                    buySell,
                                                                    commission,
                                                                    offerId,
                                                                    openOrderId,
                                                                    openOrderParties,
                                                                    openOrderReqId,
                                                                    openOrderRequestTxt,
                                                                    openQuoteId,
                                                                    openRate,
                                                                    openTime,
                                                                    rolloverInterest,
                                                                    tradeId,
                                                                    tradeIdOrigin,
                                                                    valueDate,
                                                                    dividend );


            TryUpdatePosition( fxPosition );

        }

        private void UiRemovePosition( string tradeId )
        {
            var fxOpenPositionPendingOrderInfoBindingList = GFMgr.GetOpenPositionAndOrdersBindingList( );

            int index = fxOpenPositionPendingOrderInfoBindingList.FindIndex( x => x.Ticket == tradeId );

            if ( index >= 0 )
            {
                var newOpenPositionPendingOrderInfoBindingList = new List< FxOpenPositionAndOrders>( );

                string accountName = string.Empty;


                for ( int i = 0; i < fxOpenPositionPendingOrderInfoBindingList.Count; i++ )
                {
                    accountName = fxOpenPositionPendingOrderInfoBindingList[ i ].AccountName;

                    if ( i != index )
                    {
                        newOpenPositionPendingOrderInfoBindingList.Add( fxOpenPositionPendingOrderInfoBindingList[ i ] );
                    }
                }                

                GFMgr.SetOpenPositionAndOrdersBindingList( newOpenPositionPendingOrderInfoBindingList );

                PositionBindingListChangedEvent?.Invoke( this, accountName );
            }
            else
            {

            }


        }

        private void UiRemovePositionFromSummaryBindingList( string instrument, string tradeId )
        {
            if ( _instrumentToTradeIdCollection.Count == 0 )
            {

            }


        }

        private FxPositionsSummary UiRemoveInstrumentFromSummaryBindingList( string instrument )
        {
            FxPositionsSummary deleted = null;
            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

            int index = fxPositionsSummaryBindingList.FindIndex( x => x.Symbol == instrument );

            if ( index >= 0 )
            {
                var positionSummaryBindingList = new List< FxPositionsSummary >( );

                string accountName = string.Empty;

                deleted = fxPositionsSummaryBindingList[ index ];

                for ( int i = 0; i < fxPositionsSummaryBindingList.Count; i++ )
                {
                    accountName = fxPositionsSummaryBindingList[ i ].AccountName;

                    if ( i != index )
                    {
                        positionSummaryBindingList.Add( fxPositionsSummaryBindingList[ i ] );
                    }
                }                

                GFMgr.SetPositionSummaryBindingList( positionSummaryBindingList );

                SummaryBindingListChangedEvent?.Invoke( this, accountName );
            }
            else
            {
                //throw new InvalidDataException();
            }

            if ( _instrumentToTradeIdCollection.Count == 0 )
            {
                var accountsSummaryBindingList = GFMgr.GetAccountSummaryBindingList();

                foreach ( var account in _detailedAccountsCollection )
                {
                    TaskCalcAccountSummary( accountsSummaryBindingList, account.AccountName, _usableMarginPercentage, null, account.AccountCurrency, GFMgr.HasSmartMargin( account.LeverageProfileID ) );
                }
            }

            return deleted;
        }

        public void TaskCalcAccountSummary( List<FxAccountSummary> items, string accountName, double usableMargin, OpenPositionsPl grossPl, string accountCurrency, bool smartMarginWatch )
        {
            foreach ( FxAccountSummary accountSummary in items )
            {
                if ( accountSummary != null && accountSummary.AccountName == accountName )
                {
                    if ( grossPl != null && accountSummary.UsedMargin == 0 )
                    {
                        var tradeDataRepo = GFMgr.GetSubaccountTradeDataRepoByName( accountName );

                        tradeDataRepo.CalculateMargin( accountName );
                    }

                    double grossProfitLoss                = ( grossPl == null ? 0 : grossPl.ProfitLoss );
                    var calcValue                         = new AccountSummaryCalculatedValue( );
                    calcValue.GrossPl                     = grossProfitLoss;
                    calcValue.DayPl                       = Math.Round( ( accountSummary.Balance - accountSummary.BeginningEquity + grossProfitLoss ), 2 );
                    calcValue.Equity                      = Math.Round( ( accountSummary.Balance + grossProfitLoss ), 2 );
                    calcValue.UsableMargin                = calcValue.Equity - accountSummary.UsedMargin;
                    calcValue.UsableMaintMargin           = calcValue.Equity - accountSummary.UsedMaintMargin * ( smartMarginWatch ? 2 : 1 );
                    calcValue.PreMarginCallPercentage     = Math.Round( ( calcValue.UsableMaintMargin / calcValue.Equity * 100 ), 2 );
                    calcValue.PreLiquidationPercentage    = Math.Round( ( calcValue.UsableMargin / calcValue.Equity * 100 ), 2 );

                    if ( calcValue.PreLiquidationPercentage < usableMargin )
                    {
                        GFMgr.ProcessDangerouslyOverLeverage( accountName, calcValue );
                    }
                    else if ( calcValue.PreLiquidationPercentage < usableMargin + 10 )
                    {
                        GFMgr.ProcessOverLeverage( accountName, calcValue );
                    }

                    accountSummary.CalculatedValue = calcValue;

                    return;
                }

            }
        }


        public FxPositionsSummary RemovePosition( string accountName, string accountId, string instrument, string tradeId )
        {
            FxPositionsSummary deleted = null;

            if ( _tradeIdToPosition.ContainsKey( tradeId ) )
            {
                var detailedPositionsBindingList = GFMgr.GetDetailedPositionsBindingList();

                _tradeIdToPosition.Remove( tradeId );

                int index = detailedPositionsBindingList.FindIndex( x => x.TradeID == tradeId );

                if ( index >= 0 )
                {
                    detailedPositionsBindingList.RemoveAt( index );
                }

                var fxOpenPositionPendingOrderInfoBindingList = GFMgr.GetOpenPositionAndOrdersBindingList( );

                UiRemovePosition( tradeId );                


                var tradeIDs = _instrumentToTradeIdCollection[ instrument ];

                index = tradeIDs.IndexOf( tradeId );

                if ( index >= 0 )
                {
                    tradeIDs.RemoveAt( index );
                }

                if ( tradeIDs.Count == 0 )
                {
                    var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

                    _instrumentToTradeIdCollection.Remove( instrument );

                    deleted = UiRemoveInstrumentFromSummaryBindingList( instrument );                    
                }
                else
                {
                    CalculatePositionsSummary( accountName, accountId );
                }
            }

            return deleted;
        }

        public void InternalRemoveOrder( string orderid )
        {
            var myOrdersForNewPositionBindingList = GFMgr.GetOrdersForNewPositionBindingList();

            int index = myOrdersForNewPositionBindingList.FindIndex( x => x.OrderId == orderid );

            if ( index >= 0 )
            {
                var ordersForNewPositionBindingList = new List<FxOrdersForNewPosition>( );

                string accountName = string.Empty;


                for ( int i = 0; i < myOrdersForNewPositionBindingList.Count; i++ )
                {
                    accountName = myOrdersForNewPositionBindingList[ i ].AccountName;

                    if ( i != index )
                    {
                        ordersForNewPositionBindingList.Add( myOrdersForNewPositionBindingList[ i ] );
                    }
                }                

                GFMgr.SetOrdersForNewPositionBindingList( ordersForNewPositionBindingList );

                OrdersBindingListChangedEvent?.Invoke( this, accountName );
            }

            var myOrdersForExisingPositions = GFMgr.GetOpenPositionAndOrdersBindingList( );

            foreach ( var position in myOrdersForExisingPositions )
            {
                if ( position.LimitOrder != null )
                {
                    if ( position.LimitOrder.OrderID == orderid )
                    {
                        position.LimitOrder = null;
                    }                    
                }

                if ( position.StopOrder != null )
                {
                    if ( position.StopOrder.OrderID == orderid )
                    {
                        position.StopOrder = null;
                    }
                }
            }



        }

        public void InternalRemoveDetailedOrders( string orderid )
        {
            var detailedOrdersBindingList = GFMgr.GetDetailedOrdersBindingList();

            int index = detailedOrdersBindingList.FindIndex( x => x.OrderID == orderid );

            if ( index >= 0 )
            {
                var newDetailedBindingList = new List<DetailedOrderDB>( );

                string accountName = string.Empty;


                for ( int i = 0; i < detailedOrdersBindingList.Count; i++ )
                {
                    accountName = detailedOrdersBindingList[ i ].AccountName;

                    if ( i != index )
                    {
                        newDetailedBindingList.Add( detailedOrdersBindingList[ i ] );
                    }
                }



                GFMgr.SetDetailedOrderBindingList( newDetailedBindingList );

                OrdersBindingListChangedEvent?.Invoke( this, accountName );
            }
        }

        public void RemoveOrder( string instrument, string orderid )
        {
            var detailedOrdersBindingList = GFMgr.GetDetailedOrdersBindingList( );

            if ( _orderIdToOrder.ContainsKey( orderid ) )
            {
                _orderIdToOrder.Remove( orderid );

                InternalRemoveDetailedOrders( orderid );

                var myOrdersForNewPositionBindingList = GFMgr.GetOrdersForNewPositionBindingList();

                InternalRemoveOrder( orderid );

                if ( _instrumentToOrderIdCollection.ContainsKey( instrument ) )
                {
                    List< string > orderIDs = _instrumentToOrderIdCollection[ instrument ];

                    var index = orderIDs.IndexOf( orderid );

                    if ( index >= 0 )
                    {
                        orderIDs.RemoveAt( index );
                    }
                }                
            }
        }

        public List<CloseSomeOrdersData> GetCloseSomePositionsOrdersData( string accountName, Messages.ClosePositionsType posType )
        {
            List<CloseSomeOrdersData> newOrdersData = new List< CloseSomeOrdersData >( );

            var fxOpenPositionPendingOrderInfoBindingList = GFMgr.GetOpenPositionAndOrdersBindingList( );

            foreach ( var position in fxOpenPositionPendingOrderInfoBindingList )
            {
                if ( position.AccountName == accountName )
                {
                    string offerid   = GFMgr.GetOfferId( position.Symbol ).ToString();
                    FxOrderSide side = ( position.IsBuy ? FxOrderSide.Sell : FxOrderSide.Buy ); // Set opposite side

                    var customId     = position.Comment.Split(',').Select(x => x.Split('=')).Where(x => x.Length > 1 && !String.IsNullOrEmpty(x[0].Trim()) && !String.IsNullOrEmpty(x[1].Trim())).ToDictionary(x => x[0].Trim(), x => x[1].Trim());

                    int posEffect    = 0;
                    var opEffect     = fxOrderPositionEffects.Default;

                    if ( customId.ContainsKey( "PE" ) )
                    {
                        posEffect = customId[ "PE" ].To<int>( );

                        if ( posEffect > 0 )
                        {
                            opEffect = ( fxOrderPositionEffects ) posEffect;
                        }

                    }



                    switch ( posType )
                    {
                        case Messages.ClosePositionsType.Long:
                        {
                            if ( position.IsBuy )
                            {
                                CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                                newOrdersData.Add( data );
                            }
                        }
                        break;

                        case Messages.ClosePositionsType.Short:
                        {
                            if ( !position.IsBuy )
                            {
                                CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                                newOrdersData.Add( data );
                            }
                        }
                        break;

                        case Messages.ClosePositionsType.AllHedge:
                        {
                            if ( opEffect == fxOrderPositionEffects.HedgeLong || opEffect == fxOrderPositionEffects.HedgeShort )
                            {
                                CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                                newOrdersData.Add( data );
                            }
                        }
                            break;

                        case Messages.ClosePositionsType.LongHedge:
                        {
                            if ( opEffect == fxOrderPositionEffects.HedgeLong )
                            {
                                CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                                newOrdersData.Add( data );
                            }
                        }
                        break;

                        case Messages.ClosePositionsType.ShortHedge:
                        {
                            if ( opEffect == fxOrderPositionEffects.HedgeShort )
                            {
                                CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                                newOrdersData.Add( data );
                            }
                        }
                        break;

                        case Messages.ClosePositionsType.Lossing:
                        {
                            if ( position.PipProfit < 0 )
                            {
                                CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                                newOrdersData.Add( data );
                            }
                        }
                        break;

                        case Messages.ClosePositionsType.Winning:
                        {
                            if ( position.PipProfit > 0 )
                            {
                                CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                                newOrdersData.Add( data );
                            }
                        }
                        break;

                        case Messages.ClosePositionsType.LossingHedge:
                        {
                            if ( position.PipProfit < 0 && ( opEffect == fxOrderPositionEffects.HedgeLong || opEffect == fxOrderPositionEffects.HedgeShort ) )
                            {
                                CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                                newOrdersData.Add( data );
                            }
                        }
                        break;

                        case Messages.ClosePositionsType.WinningHedge:
                        {
                            if ( position.PipProfit > 0 && ( opEffect == fxOrderPositionEffects.HedgeLong || opEffect == fxOrderPositionEffects.HedgeShort ) )
                            {
                                CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                                newOrdersData.Add( data );
                            }
                        }
                        break;
                    }

                }

            }

            return newOrdersData;
        }

        public List<CloseSomeOrdersData> GetCloseSomePositionsOrdersData( string accountName, string symbol, Sides ? buyOrSell )
        {
            List<CloseSomeOrdersData> newOrdersData = new List< CloseSomeOrdersData >( );

            var fxOpenPositionPendingOrderInfoBindingList = GFMgr.GetOpenPositionAndOrdersBindingList( );

            foreach ( var position in fxOpenPositionPendingOrderInfoBindingList )
            {
                if ( position.AccountName == accountName && position.Symbol == symbol)
                {
                    string offerid   = GFMgr.GetOfferId( position.Symbol ).ToString();
                    FxOrderSide side = ( position.IsBuy ? FxOrderSide.Sell : FxOrderSide.Buy ); // Set opposite side

                    CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                    newOrdersData.Add( data );

                    //switch ( posType )
                    //{
                    //    case StockSharp.Messages.ClosePositionsType.Long:
                    //    {
                    //        if ( position.IsBuy )
                    //        {
                    //            
                    //        }
                    //    }
                    //    break;

                    //    case StockSharp.Messages.ClosePositionsType.Short:
                    //    {
                    //        if ( !position.IsBuy )
                    //        {
                    //            CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                    //            newOrdersData.Add( data );
                    //        }
                    //    }
                    //    break;

                    //    case StockSharp.Messages.ClosePositionsType.LongHedge:
                    //    {
                    //        if ( position.Comment == "HedgeLong" )
                    //        {
                    //            CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                    //            newOrdersData.Add( data );
                    //        }
                    //    }
                    //    break;

                    //    case StockSharp.Messages.ClosePositionsType.ShortHedge:
                    //    {
                    //        if ( position.Comment == "HedgeShort" )
                    //        {
                    //            CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                    //            newOrdersData.Add( data );
                    //        }
                    //    }
                    //    break;

                    //    case StockSharp.Messages.ClosePositionsType.Lossing:
                    //    {
                    //        if ( position.PipProfit < 0 )
                    //        {
                    //            CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                    //            newOrdersData.Add( data );
                    //        }
                    //    }
                    //    break;

                    //    case StockSharp.Messages.ClosePositionsType.Winning:
                    //    {
                    //        if ( position.PipProfit > 0 )
                    //        {
                    //            CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                    //            newOrdersData.Add( data );
                    //        }
                    //    }
                    //    break;

                    //    case StockSharp.Messages.ClosePositionsType.LossingHedge:
                    //    {
                    //        if ( position.PipProfit < 0 && position.Comment.Contains( "Hedge" ) )
                    //        {
                    //            CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                    //            newOrdersData.Add( data );
                    //        }
                    //    }
                    //    break;

                    //    case StockSharp.Messages.ClosePositionsType.WinningHedge:
                    //    {
                    //        if ( position.PipProfit > 0 && position.Comment.Contains( "Hedge" ) )
                    //        {
                    //            CloseSomeOrdersData data    = new CloseSomeOrdersData( position.AccountID, position.Ticket, offerid, side, position.Amount);
                    //            newOrdersData.Add( data );
                    //        }
                    //    }
                    //    break;
                    //}

                }

            }

            return newOrdersData;
        }


        public List<BreakEvenStopLossOrdersData> GetBreakEvenStopLossOrdersData( string accountName, double howManyPipsProfit )
        {
            List< BreakEvenStopLossOrdersData > newOrdersData = new List< BreakEvenStopLossOrdersData >( );

            var fxOpenPositionPendingOrderInfoBindingList = GFMgr.GetOpenPositionAndOrdersBindingList( );

            foreach ( var position in fxOpenPositionPendingOrderInfoBindingList )
            {
                if ( position.AccountName == accountName )
                {
                    string offerid   = GFMgr.GetOfferId( position.Symbol ).ToString();
                    FxOrderSide side = ( position.IsBuy ? FxOrderSide.Sell : FxOrderSide.Buy ); // Set opposite side

                    if ( position.PipProfit >= howManyPipsProfit )
                    {
                        var accountId = GFMgr.GetAccountIdFromName( _mainLoginName, accountName );

                        BreakEvenStopLossOrdersData data    = new BreakEvenStopLossOrdersData( accountId, position.Ticket, offerid, side, position.Amount, position.OpenPrice);
                        newOrdersData.Add( data );
                    }
                }
            }

            return newOrdersData;
        }

        public List<RemoveOrdersData> GetRemoveOrdersData( string accountName, FxOrderType orderType, string symbol, Sides? orderSide )
        {
            var detailedOrdersBindingList = GFMgr.GetDetailedOrdersBindingList( );

            int offerId = 0;

            List< RemoveOrdersData > newOrdersData = new List< RemoveOrdersData >( );

            foreach ( IDetailedOrder detailedOrder in detailedOrdersBindingList )
            {
                if ( detailedOrder.AccountName == accountName )
                {
                    bool orderSelected = false;

                    if ( orderSide.HasValue )
                    {
                        var buyOrSell = orderSide.Value == Sides.Buy ? "B" : "S";

                        if ( detailedOrder.BuySell == buyOrSell )
                        {
                            orderSelected = true;
                        }
                    }
                    else
                    {
                        orderSelected = true;
                    }
                    
                    switch ( orderType )
                    {
                        case FxOrderType.All:
                        {                            
                            if ( orderSelected )
                            {
                                RemoveOrdersData data = new RemoveOrdersData( detailedOrder.AccountID, detailedOrder.OrderID );
                                newOrdersData.Add( data );
                            }
                            
                        }
                        break;

                        case FxOrderType.Limit:
                        {
                            if ( orderSelected )
                            {
                                if ( ( detailedOrder.Type == "L" ) || ( detailedOrder.Type == "LE" ) || ( detailedOrder.Type == "LTE" ) )
                                {
                                    RemoveOrdersData data = new RemoveOrdersData( detailedOrder.AccountID, detailedOrder.OrderID );
                                    newOrdersData.Add( data );
                                }

                            }
                        }
                        break;

                        case FxOrderType.Stop:
                        {
                            if ( orderSelected )
                            {
                                if ( ( detailedOrder.Type == "S" ) || ( detailedOrder.Type == "SE" ) || ( detailedOrder.Type == "STE" ) )
                                {
                                    RemoveOrdersData data = new RemoveOrdersData( detailedOrder.AccountID, detailedOrder.OrderID );
                                    newOrdersData.Add( data );
                                }
                            }
                        }
                        break;

                        case FxOrderType.SafetyNet:
                        {
                            if ( orderSelected )
                            {
                                if ( !string.IsNullOrEmpty( symbol ) )
                                {
                                    offerId = GFMgr.GetOfferId( symbol );

                                    if ( detailedOrder.RequestTXT.Contains( "SafetyNet" ) && detailedOrder.OfferID == offerId.ToString( ) )
                                    {
                                        RemoveOrdersData data = new RemoveOrdersData( detailedOrder.AccountID, detailedOrder.OrderID );
                                        newOrdersData.Add( data );
                                    }
                                }
                                else
                                {
                                    if ( detailedOrder.RequestTXT.Contains( "SafetyNet" ) )
                                    {
                                        RemoveOrdersData data = new RemoveOrdersData( detailedOrder.AccountID, detailedOrder.OrderID );
                                        newOrdersData.Add( data );
                                    }
                                }
                            }
                        }
                        break;
                    }
                }


            }

            return newOrdersData;
        }



        public Dictionary<string, CloseOrdersData> GetCloseAllPositionsOrdersData( string accountName )
        {
            Dictionary< string, CloseOrdersData > newOrdersData = new Dictionary< string, CloseOrdersData >( );

            var detailedPositionsBindingList = GFMgr.GetDetailedPositionsBindingList();

            foreach ( var position in detailedPositionsBindingList )
            {
                if ( position.AccountName == accountName )
                {
                    string offerid   = position.OfferID;
                    string buyOrSell = position.BuySell;

                    // Set opposite side
                    FxOrderSide side = ( buyOrSell.Equals( "B" ) ? FxOrderSide.Sell : FxOrderSide.Buy );

                    if ( newOrdersData.ContainsKey( offerid ) )
                    {
                        FxOrderSide currentSide = newOrdersData[ offerid ].Side;

                        if ( currentSide != FxOrderSide.Both && currentSide != side )
                        {
                            newOrdersData[ offerid ].Side = FxOrderSide.Both;
                        }
                    }
                    else
                    {
                        var accountId = GFMgr.GetAccountIdFromName( _mainLoginName, accountName );
                        CloseOrdersData data    = new CloseOrdersData( accountId, side );
                        newOrdersData.Add( offerid, data );
                    }
                }
            }

            return newOrdersData;
        }

        public List<ReverseAndEscapeOrdersData> GetReverseAndEsacpeOrdersData( string accountName, string selectedOfferId, bool shouldEscape )
        {
            var symbolSummaries = GFMgr.GetPositionSummaryBindingList();

            List< ReverseAndEscapeOrdersData > needReversedPositions = new List< ReverseAndEscapeOrdersData >( );

            foreach ( var summary in symbolSummaries )
            {                
                string offerid   = summary.OfferId;

                if ( selectedOfferId == offerid )
                {
                    int amount       =  ( int ) ( summary.BuyAmount.Value - summary.SellAmount.Value );

                    // Set opposite side

                    if ( amount == 0 )
                    {
                        // means all positions have been hedged
                        continue;
                    }

                    FxOrderSide side = ( amount > 0 ) ? FxOrderSide.Sell : FxOrderSide.Buy;

                    ReverseAndEscapeOrdersData reverseData = new ReverseAndEscapeOrdersData( accountName, offerid, side, Math.Abs( amount ) );

                    needReversedPositions.Add( reverseData );
                }                
            }
            

            var bindingList     = GFMgr.GetOpenPositionAndOrdersByAccountName( accountName );

            foreach ( var reverseSymbol in needReversedPositions )
            {                
                // Depends on whethere we need to escape the trade            
                if ( shouldEscape )
                {
                    // If we need to escape the trade, we will set our limit price to be our entry price and 
                    // the stop price will be the current offer minus 15 pips (max)

                    foreach ( var existingPosition in bindingList )
                    {
                        var offerid   = GFMgr.GetOfferId( existingPosition.Symbol ).ToString( );

                        if ( reverseSymbol.OfferId == offerid )
                        {
                            if (existingPosition.PipProfit >= 0 )
                            {
                                CreateClosedPosition( existingPosition, reverseSymbol );
                            }
                            else
                            {
                                ChangeOrCreateStopLossAndTakeProfit( offerid, existingPosition, reverseSymbol );
                            }
                            
                        }
                    }
                }
                else
                {
                    // If we don't need to escape the trade, we can call all opened trades.

                    foreach ( var existingPosition in bindingList )
                    {
                        CreateClosedPosition( existingPosition, reverseSymbol );
                    }
                }
            }

            return needReversedPositions;
        }

        private void CreateClosedPosition( FxOpenPositionAndOrders existingPosition, ReverseAndEscapeOrdersData reverseSymbol )
        {
            var offerid = GFMgr.GetOfferId( existingPosition.Symbol ).ToString( );

            if ( reverseSymbol.OfferId == offerid )
            {
                var newOrder = new PriceLevelsOrdersData(
                                                                                    existingPosition.AccountID,
                                                                                    offerid,
                                                                                    existingPosition.Ticket,
                                                                                    ChangeOrderType.CLOSED,
                                                                                    PriceLevelsType.NO_CHANGE,
                                                                                    0,
                                                                                    existingPosition.IsBuy ? FxOrderSide.Sell : FxOrderSide.Buy,
                                                                                    existingPosition.Amount );

                //var side = ( position.IsBuy ? FxOrderSide.Sell : FxOrderSide.Buy ); // Set opposite side

                reverseSymbol.NewOrders.Add( newOrder );
                //reverseSymbol.AddOrder( position.AccountID, position.Ticket, offerid, side, position.Amount, 0, 0, false );
            }
        }


        private void ChangeOrCreateStopLossAndTakeProfit( string offerid, FxOpenPositionAndOrders existingPosition, ReverseAndEscapeOrdersData reverseSymbol )
        {            
            var pointSize = GFMgr.GetInstrumentPointSize( existingPosition.Symbol ) == 0 ? GFMgr.GetInstrumentPointSize( existingPosition.Symbol ) : 0.0001;

            var offer     = GFMgr.GetOffer( existingPosition.Symbol );
            var spreadPip = offer.Spread ;

            var side = ( existingPosition.IsBuy ? FxOrderSide.Sell : FxOrderSide.Buy );
            // Set opposite side

            var pip20 = ( 20 + spreadPip / pointSize );
            var pip15 = ( 15 + spreadPip / pointSize );
            var pip10 = ( 10 + spreadPip / pointSize );

            var plus15Pips = ( Math.Abs( existingPosition.PipProfit ) + pip15 ) * pointSize;
            var plus10Pips = ( Math.Abs( existingPosition.PipProfit ) - pip10 ) * pointSize;

            var stopLoss15 = existingPosition.IsBuy ? existingPosition.OpenPrice - plus15Pips : existingPosition.OpenPrice + plus15Pips;

            // If we can recover 10 pips, we should be really happy
            var limitRate10 = existingPosition.IsBuy ? existingPosition.OpenPrice - plus10Pips : existingPosition.OpenPrice + plus10Pips;

            double takeProfitPrice = 0;
            double stopLossPrice = 0;

            if ( existingPosition.PipProfit < 0 && existingPosition.PipProfit > -pip10 ) // We have a loss of less than 10 pips
            {
                takeProfitPrice = existingPosition.OpenPrice;
                stopLossPrice = stopLoss15;
            }
            else if ( existingPosition.PipProfit <= -pip10 && existingPosition.PipProfit > -pip20 ) // We have a loss of less than 20 pips
            {
                // If we can recover 10 pips, we should be really happy    
                takeProfitPrice = limitRate10;
                stopLossPrice = stopLoss15;
                //reverseSymbol.AddOrder( existingPosition.AccountID, existingPosition.Ticket, offerid, side, existingPosition.Amount, stopLoss15, limitRate10, true );
            }
            else if ( existingPosition.PipProfit < -pip20 ) // Since are net loss a lot already, it is kinda hard to bounce by too much here. It is better to close this position out.
            {
                takeProfitPrice = limitRate10;
                stopLossPrice = stopLoss15;
                //reverseSymbol.AddOrder( existingPosition.AccountID, existingPosition.Ticket, offerid, side, existingPosition.Amount, stopLoss15, limitRate10, true );
            }

            if ( existingPosition.LimitOrder != null )
            {
                var modifiedOrder = new PriceLevelsOrdersData(
                                                                                    existingPosition.AccountID,
                                                                                    offerid,
                                                                                    existingPosition.LimitOrder.TradeID,
                                                                                    existingPosition.LimitOrder.OrderID,
                                                                                    ChangeOrderType.MODIFIED,
                                                                                    PriceLevelsType.TAKE_PROFIT,
                                                                                    takeProfitPrice,
                                                                                    existingPosition.IsBuy ? FxOrderSide.Sell : FxOrderSide.Buy,
                                                                                    existingPosition.Amount );

                reverseSymbol.ModifiedOrders.Add( modifiedOrder );
            }
            else
            {
                var newOrder = new PriceLevelsOrdersData(
                                                                                    existingPosition.AccountID,
                                                                                    offerid,
                                                                                    existingPosition.Ticket,
                                                                                    ChangeOrderType.NEW,
                                                                                    PriceLevelsType.TAKE_PROFIT,
                                                                                    takeProfitPrice,
                                                                                    existingPosition.IsBuy ? FxOrderSide.Sell : FxOrderSide.Buy,
                                                                                    existingPosition.Amount );

                reverseSymbol.NewOrders.Add( newOrder );
            }

            if ( existingPosition.StopOrder != null )
            {
                var modifiedOrder = new PriceLevelsOrdersData(
                                                                                    existingPosition.AccountID,
                                                                                    offerid,
                                                                                    existingPosition.StopOrder.TradeID,
                                                                                    existingPosition.StopOrder.OrderID,
                                                                                    ChangeOrderType.MODIFIED,
                                                                                    PriceLevelsType.STOP_LOSS,
                                                                                    stopLossPrice,
                                                                                    existingPosition.IsBuy ? FxOrderSide.Sell : FxOrderSide.Buy,
                                                                                    existingPosition.Amount
                                                                        );

                reverseSymbol.ModifiedOrders.Add( modifiedOrder );
            }
            else
            {
                var newOrder = new PriceLevelsOrdersData(
                                                                                    existingPosition.AccountID,
                                                                                    offerid,
                                                                                    existingPosition.Ticket,
                                                                                    ChangeOrderType.NEW,
                                                                                    PriceLevelsType.STOP_LOSS,
                                                                                    stopLossPrice,
                                                                                    existingPosition.IsBuy ? FxOrderSide.Sell : FxOrderSide.Buy,
                                                                                    existingPosition.Amount );

                reverseSymbol.NewOrders.Add( newOrder );
            }
        }

        public Dictionary<string, HedgeOrdersData> GetHedgeOrdersData( string accountName )
        {
            Dictionary< string, HedgeOrdersData > newOrdersData = new Dictionary< string, HedgeOrdersData >( );

            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

            foreach ( var summary in fxPositionsSummaryBindingList )
            {
                if ( summary.AccountName == accountName )
                {
                    string offerid   = summary.OfferId;

                    int amount =  (int) ( summary.BuyAmount.Value - summary.SellAmount.Value );

                    // Set opposite side

                    if ( amount == 0 )
                    {

                        // means all positions have been hedged
                        continue;
                    }

                    FxOrderSide side = ( amount > 0 ) ? FxOrderSide.Sell : FxOrderSide.Buy;

                    if ( newOrdersData.ContainsKey( offerid ) )
                    {
                        throw new InvalidProgramException( );
                    }
                    else
                    {
                        string accountId = GFMgr.GetAccountIdFromName( _mainLoginName, accountName );

                        HedgeOrdersData data    = new HedgeOrdersData( accountId, side, Math.Abs( amount ) );
                        newOrdersData.Add( offerid, data );
                    }

                    //return newOrdersData;
                }

            }

            return newOrdersData;
        }

        public Dictionary<string, HedgeOrdersData> GetHedgeLongOrdersData( string accountName )
        {
            Dictionary< string, HedgeOrdersData > newOrdersData = new Dictionary< string, HedgeOrdersData >( );

            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

            foreach ( var summary in fxPositionsSummaryBindingList )
            {
                if ( summary.AccountName == accountName )
                {
                    string offerid   = summary.OfferId;

                    int amount =  (int) ( summary.BuyAmount.Value - summary.SellAmount.Value );

                    // Set opposite side

                    if ( amount <= 0 )
                    {

                        // means all positions have been hedged
                        continue;
                    }

                    FxOrderSide side = FxOrderSide.Sell;

                    if ( newOrdersData.ContainsKey( offerid ) )
                    {
                        throw new InvalidProgramException( );
                    }
                    else
                    {
                        string accountId = GFMgr.GetAccountIdFromName( _mainLoginName, accountName );

                        HedgeOrdersData data    = new HedgeOrdersData( accountId, side, Math.Abs( amount ) );
                        newOrdersData.Add( offerid, data );
                    }

                    return newOrdersData;
                }

            }

            return newOrdersData;
        }

        public Dictionary<string, HedgeOrdersData> GetHedgeShortOrdersData( string accountName )
        {
            Dictionary< string, HedgeOrdersData > newOrdersData = new Dictionary< string, HedgeOrdersData >( );

            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

            foreach ( var summary in fxPositionsSummaryBindingList )
            {
                if ( summary.AccountName == accountName )
                {
                    string offerid   = summary.OfferId;

                    int amount =  (int) ( summary.BuyAmount.Value - summary.SellAmount.Value );

                    // We will ignore all the instrument that is net Long
                    if ( amount >= 0 )
                    {                        
                        continue;
                    }

                    FxOrderSide side = FxOrderSide.Buy;

                    if ( newOrdersData.ContainsKey( offerid ) )
                    {
                        throw new InvalidProgramException( );
                    }
                    else
                    {
                        string accountId = GFMgr.GetAccountIdFromName( _mainLoginName, accountName );

                        HedgeOrdersData data    = new HedgeOrdersData( accountId, side, Math.Abs( amount ) );
                        newOrdersData.Add( offerid, data );
                    }

                    return newOrdersData;
                }

            }

            return newOrdersData;
        }


        public bool HasSafetyNet( string accountName )
        {
            var myOrdersForNewPositionBindingList = GFMgr.GetOrdersForNewPositionBindingList();

            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

            foreach ( var summary in fxPositionsSummaryBindingList )
            {
                if ( summary.AccountName == accountName )
                {
                    string offerid   = summary.OfferId;

                    var currentAskBid = GFMgr.GetOffer( Int32.Parse( offerid ) );

                    int amount =  (int) ( summary.BuyAmount.Value - summary.SellAmount.Value );

                    // Set opposite side

                    if ( amount == 0 )
                    {
                        // means all positions have been hedged
                        return true;
                    }

                    FxOrderSide side = ( amount > 0 ) ? FxOrderSide.Sell : FxOrderSide.Buy;


                    if ( side == FxOrderSide.Sell )
                    {
                        foreach ( var order in myOrdersForNewPositionBindingList )
                        {
                            if ( order.AccountName == accountName && order.Comment.Contains( "LongSafetyNet" ) )
                            {
                                return true;
                            }
                        }
                    }
                    else if ( side == FxOrderSide.Buy )
                    {
                        foreach ( var order in myOrdersForNewPositionBindingList )
                        {
                            if ( order.AccountName == accountName && order.Comment.Contains( "ShortSafetyNet" ) )
                            {
                                return true;
                            }
                        }

                    }
                    else
                    {
                        throw new InvalidProgramException( );
                    }


                }

            }




            return false;
        }


        public UpdateSafetyNetOrdersData GetUpdateSafetyNetOrdersData( string accountName, string symbol, FxOrderSide orderSide, double adjustedVol )
        {
            var output = new UpdateSafetyNetOrdersData();
            var pendingOrders = GFMgr.GetOrdersForNewPositionBindingListByAccountName( accountName );

            string safetyNetOrderSearch = orderSide.ToString();

            double largestVolume = 0.0;
            double total = 0.0;
            FxOrdersForNewPosition safetyOrder = null;

            foreach ( var order in pendingOrders )
            {
                if ( order.Comment.Contains( safetyNetOrderSearch ) )
                {
                    total += order.Amount;

                    if ( order.Amount > largestVolume )
                    {
                        largestVolume = order.Amount;
                        safetyOrder = order;
                    }
                }
            }


            if ( safetyOrder != null )
            {
                int newAmount = safetyOrder.Amount + ( int ) ( adjustedVol - total );
                output.OrderSide = orderSide;
                output.OrderToBeChanged = safetyOrder;
                output.NewAmount = newAmount > 0 ? newAmount : 0;

            }

            return output;
        }




        public Dictionary<string, SafetyNetOrdersData> GetSafetyNetOrdersData( string accountName, double safety )
        {
            Dictionary< string, SafetyNetOrdersData > newOrdersData = new Dictionary< string, SafetyNetOrdersData >( );

            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

            foreach ( var summary in fxPositionsSummaryBindingList )
            {
                if ( summary.AccountName == accountName )
                {
                    string offerid   = summary.OfferId;
                    string symbol = GFMgr.GetSymbolFromOfferId( offerid );

                    var currentAskBid = GFMgr.GetOffer( Int32.Parse( offerid ) );

                    int amount =  (int) ( summary.BuyAmount.Value - summary.SellAmount.Value );

                    var pointSize = GFMgr.GetInstrumentPointSize( symbol );                    

                    var safetyPips = safety * pointSize;

                    // Set opposite side

                    if ( amount == 0 )
                    {
                        // means all positions have been hedged
                        return null;
                    }

                    FxOrderSide side = ( amount > 0 ) ? FxOrderSide.Sell : FxOrderSide.Buy;

                    if ( newOrdersData.ContainsKey( offerid ) )
                    {
                        throw new InvalidProgramException( );
                    }
                    else
                    {
                        double safetyeNetPrice = 0;                        

                        if ( side == FxOrderSide.Sell && summary.BuyAvgOpen.HasValue )
                        {
                            if ( summary.BuyNetPl > safetyPips )
                            {
                                var pipProfit = currentAskBid.Bid - summary.BuyAvgOpen.Value;

                                if ( pipProfit > safetyPips )
                                {
                                    safetyeNetPrice = summary.BuyAvgOpen.Value;
                                }
                                else
                                {
                                    safetyeNetPrice = currentAskBid.Bid - safetyPips;
                                }
                            }
                            else
                            {
                                safetyeNetPrice = summary.BuyAvgOpen.Value - safetyPips;
                            }

                        }
                        else if ( side == FxOrderSide.Buy && summary.SellAvgOpen.HasValue )
                        {
                            if ( summary.SellNetPl > 0 )
                            {
                                var pipProfit = summary.SellAvgOpen.Value - currentAskBid.Ask;

                                if ( pipProfit > safetyPips )
                                {
                                    safetyeNetPrice = summary.SellAvgOpen.Value;
                                }
                                else
                                {
                                    safetyeNetPrice = currentAskBid.Ask + safetyPips;
                                }


                            }
                            else
                            {
                                safetyeNetPrice = summary.SellAvgOpen.Value + safetyPips;
                            }


                        }
                        else
                        {
                            throw new InvalidProgramException( );
                        }

                        string accountId = _detailedAccountsCollection.FindAccountIdByName( accountName );

                        var data    = new SafetyNetOrdersData( accountId, side, Math.Abs( amount ), safetyeNetPrice );
                        newOrdersData.Add( offerid, data );
                    }

                    return newOrdersData;
                }

            }

            return newOrdersData;
        }


        public Dictionary<string, SafetyNetOrdersData> GetSafetyNetOrdersData( string accountName, string symbol, double highest, double lowest, out double safetyPrice )
        {
            safetyPrice = 0;

            Dictionary< string, SafetyNetOrdersData > newOrdersData = new Dictionary< string, SafetyNetOrdersData >( );

            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

            var pointSize = GFMgr.GetInstrumentPointSize( symbol );

            foreach ( var summary in fxPositionsSummaryBindingList )
            {
                if ( summary.AccountName == accountName && summary.Symbol == symbol )
                {
                    string offerid    = summary.OfferId;

                    var currentAskBid = GFMgr.GetOffer( Int32.Parse( offerid ) );

                    int amount        =  (int) ( summary.BuyAmount.Value - summary.SellAmount.Value );

                    if ( amount == 0 )                                                                  // Set opposite side
                    {
                        return null;                                                                    // means all positions have been hedged
                    }

                    FxOrderSide side = ( amount > 0 ) ? FxOrderSide.Sell : FxOrderSide.Buy;

                    if ( newOrdersData.ContainsKey( offerid ) )
                    {
                        throw new InvalidProgramException( );
                    }
                    else
                    {
                        double safetyeNetPrice = 0;

                        if ( side == FxOrderSide.Sell && summary.BuyAvgOpen.HasValue )
                        {
                            safetyeNetPrice = lowest - ( pointSize * 2 );
                        }
                        else if ( side == FxOrderSide.Buy && summary.SellAvgOpen.HasValue )
                        {
                            safetyeNetPrice = highest + ( pointSize * 2 );
                        }
                        else
                        {
                            throw new InvalidProgramException( );
                        }

                        safetyPrice = safetyeNetPrice;

                        string accountId = _detailedAccountsCollection.FindAccountIdByName( accountName );

                        var data    = new SafetyNetOrdersData( accountId, side, Math.Abs( amount ), safetyeNetPrice );
                        newOrdersData.Add( offerid, data );
                    }

                    return newOrdersData;
                }

            }

            return newOrdersData;
        }


        public void CalculateMargin( string accountName )
        {
            double symbolNetPosition = 0;

            double emr               = 0;
            double mmr               = 0;
            double lmr               = 0;

            double totalEmr          = 0;
            double totalMMr          = 0;
            double totalLmr          = 0;
            int baseUnit = 0;

            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

            foreach ( var posSummary in fxPositionsSummaryBindingList )
            {
                if ( posSummary.AccountName == accountName )
                {
                    int offerId = Int32.Parse( posSummary.OfferId );

                    var subscribedSymbol = GFMgr.GetSubscribedSymbolsByAccountNameAndId( _mainLoginName, offerId );                    

                    if ( subscribedSymbol != null )
                    {
                        emr      = subscribedSymbol.EMR;
                        mmr      = subscribedSymbol.MMR;
                        lmr      = subscribedSymbol.LMR;
                        baseUnit = subscribedSymbol.BaseUnitSize;
                    }

                    symbolNetPosition = Math.Max( posSummary.BuyAmount.Value, posSummary.SellAmount.Value );
                    symbolNetPosition /= baseUnit;

                    totalEmr += symbolNetPosition * emr;
                    totalMMr += symbolNetPosition * mmr;
                    totalLmr += symbolNetPosition * lmr;

                    posSummary.UsedMargin = symbolNetPosition * lmr;
                }
            }

            var accountSummaryBindingList = GFMgr.GetAccountSummaryBindingList();

            int index = accountSummaryBindingList.FindIndex( x => x.AccountName == accountName );

            if ( index > -1 )
            {
                accountSummaryBindingList[ index ].UsedMargin = totalLmr;
                accountSummaryBindingList[ index ].UsedMaintMargin = totalLmr;
            }            
        }


        /// <summary>
        /// Now that we have all the DetailedPositions Info collected, we need to calculate the symbolPositionSummary Summary per instrutment.
        /// </summary>
        /// <param name="accountName"></param>
        public void CalculatePositionsSummary( string accountName, string accountId )
        {           
            foreach ( var keyValuePairString in _instrumentToTradeIdCollection )
            {
                string instrument   = keyValuePairString.Key;
                var tradeIds        = keyValuePairString.Value;

                var offerId         = GFMgr.GetOfferId( instrument );

                double avgSellPrice = 0.0;
                double avgBuyPrice  = 0.0;
                double sellAmount   = 0.0;
                double buyAmount    = 0.0;

                int buyCount        = 0;
                int sellCount       = 0;

                foreach ( string tradeId in tradeIds )
                {
                    if ( _tradeIdToPosition.ContainsKey( tradeId ) )
                    {
                        var position     = _tradeIdToPosition[ tradeId ];

                        if ( position.IsBuy )
                        {
                            buyCount++;
                            buyAmount += position.Amount;
                            avgBuyPrice += position.OpenRate;

                        }
                        else
                        {
                            sellCount++;
                            sellAmount += position.Amount;
                            avgSellPrice += position.OpenRate;
                        }
                    }
                }

                if ( buyCount > 0 )
                {
                    avgBuyPrice /= buyCount;
                }

                if ( sellCount > 0 )
                {
                    avgSellPrice /= sellCount;
                }

                FxPositionsSummary newSummary = new FxPositionsSummary( _mainLoginName,
                                                                        accountName,
                                                                        accountId,
                                                                        offerId.ToString(),
                                                                        offerId,
                                                                        instrument,
                                                                        sellAmount,
                                                                        avgSellPrice,
                                                                        buyAmount,
                                                                        avgBuyPrice,
                                                                        buyAmount - sellAmount,
                                                                        0,
                                                                        0
                                                                      );

                UiAddPositionToSummaryBindingList( newSummary );                
            }


        }

        private void UiAddPositionToSummaryBindingList( FxPositionsSummary newSummary )
        {
            var posSummary = GFMgr.GetPositionSummaryBindingList();

            int index = posSummary.FindIndex( x => x.MainLoginName == newSummary.MainLoginName && x.AccountName == newSummary.AccountName && x.OfferId == newSummary.OfferId );

            if ( index > -1 )
            {
                // symbolPositionSummary Summary for the Symbol already exist, we need to update it.

                if ( !newSummary.Equals( posSummary[ index ] ) )
                {
                    posSummary[ index ].CopyFrom( newSummary );
                }
            }
            else
            {
                // Since symbolPositionSummary summary is not there, we need to add to it.

                posSummary.Add( newSummary );
            }
        }

        //public int FindIndexByMainLoginNameAndAccount( List<FxPositionsSummary> items, string mainLoginName, string accountName )
        //{
        //    if ( items.Count > 0 )
        //    {                
        //        for ( int i = 0; i < items.Count; ++i )
        //        {
        //            var positionSummary = items[ i ];
        //            
        //            if ( string.Equals( positionSummary.MainLoginName, mainLoginName, StringComparison.CurrentCultureIgnoreCase ) &&
        //                 string.Equals( positionSummary.AccountName, accountName, StringComparison.CurrentCultureIgnoreCase ) &&
        //                 positionSummary.OfferId == 
        //               )
        //            {
        //                return i;
        //            }
        //        }

        //        return -1;

        //        //return FindCore( myProperty, symbol );
        //    }

        //    return -1;
        //}


        private void AddPositionToBindingList( FxOpenPositionAndOrders openPosition )
        {
            var fxOpenPositionPendingOrderInfoBindingList = GFMgr.GetOpenPositionAndOrdersBindingList( );

            UiAddPositionToBindingList( openPosition );

            CalculatePositionsSummary( openPosition.AccountName, openPosition.AccountID );

            CalculateMargin( openPosition.AccountName );
        }

        private void UiAddPositionToBindingList( FxOpenPositionAndOrders openPosition )
        {
            var fxOpenPositionPendingOrderInfoBindingList = GFMgr.GetOpenPositionAndOrdersBindingList( );

            int index = fxOpenPositionPendingOrderInfoBindingList.FindIndex( x => x.Ticket == openPosition.Ticket );

            if ( index == -1 )
            {
                var newOpenPositionPendingOrderInfoBindingList = new List<FxOpenPositionAndOrders>( );


                for ( int i = 0; i < fxOpenPositionPendingOrderInfoBindingList.Count; i++ )
                {
                    newOpenPositionPendingOrderInfoBindingList.Add( fxOpenPositionPendingOrderInfoBindingList[ i ] );
                }

                newOpenPositionPendingOrderInfoBindingList.Add( openPosition );                

                GFMgr.SetOpenPositionAndOrdersBindingList( newOpenPositionPendingOrderInfoBindingList );

                PositionBindingListChangedEvent?.Invoke( this, openPosition.AccountName );



            }
            else
            {
                fxOpenPositionPendingOrderInfoBindingList[ index ] = openPosition;
            }
        }


        public void AddOrder( DetailedOrderDB fxOrder )
        {
            var detailedOrdersBindingList = GFMgr.GetDetailedOrdersBindingList( );            

            string instrument = GFMgr.GetSymbolFromOfferId(  fxOrder.OfferID );

            if ( _instrumentToOrderIdCollection.ContainsKey( instrument ) )
            {
                if ( !_orderIdToOrder.ContainsKey( fxOrder.OrderID ) )
                {
                    // Since we don't have This TradeID, so we addd this tradeID to instrument map and also put it into all Positions
                    List< string >  orderIDs = _instrumentToOrderIdCollection[ instrument ];

                    orderIDs.Add( fxOrder.OrderID );

                    detailedOrdersBindingList.Add( fxOrder );
                }
                else
                {
                    // Since this trade ID exist, we need to replace it in the detailedOrdersBindingList                    
                    int index = detailedOrdersBindingList.FindIndex( x => x.OrderID == fxOrder.OrderID );

                    if ( index >= 0 )
                    {
                        IDetailedOrder foundItem = detailedOrdersBindingList[ index ];

                        if ( !foundItem.Equals( fxOrder ) )
                        {
                            detailedOrdersBindingList[ index ] = fxOrder;
                        }
                    }
                }

                _orderIdToOrder[ fxOrder.OrderID ] = fxOrder;
            }
            else
            {
                _orderIdToOrder.Add( fxOrder.OrderID, fxOrder );

                List< string > orderIDs = new List< string >();

                orderIDs.Add( fxOrder.OrderID );

                _instrumentToOrderIdCollection.Add( instrument, orderIDs );

                detailedOrdersBindingList.Add( fxOrder );


            }
        }

        public void AddOrder( string accountId, string accountKind, string mainLoginName, string accountName, int amount, double atMarket, string buySell, int contingencyType, string contingentOrderId, double executionRate, long expireDate, int filledAmount, bool netQuantity, string offerId, string orderId, int originAmount, string parties, double pegOffset, string pegType, string primaryId, double rate, double rateMax, double rateMin, string requestId, string requestTxt, string stage, string status, long statusTime, string timeInForce, string tradeId, double trailRate, int trailStep, string type, string valueDate, bool workingIndicator )
        {


            DetailedOrderDB fxOrder = new DetailedOrderDB(  accountId,
                                                            accountKind,
                                                            mainLoginName,
                                                            accountName,
                                                            amount,
                                                            atMarket,
                                                            buySell,
                                                            contingencyType,
                                                            contingentOrderId,
                                                            executionRate,
                                                            expireDate,
                                                            filledAmount,
                                                            netQuantity,
                                                            offerId,
                                                            orderId,
                                                            originAmount,
                                                            parties,
                                                            pegOffset,
                                                            pegType,
                                                            primaryId,
                                                            rate,
                                                            rateMax,
                                                            rateMin,
                                                            requestId,
                                                            requestTxt,
                                                            stage,
                                                            status,
                                                            statusTime,
                                                            timeInForce,
                                                            tradeId,
                                                            trailRate,
                                                            trailStep,
                                                            type,
                                                            valueDate,
                                                            workingIndicator
                                         );

            AddOrder( fxOrder );
        }

        public void TryUpdateOrder( string accountId, string accountKind, string mainLoginName, string accountName, int amount, double atMarket, string buySell, int contingencyType, string contingentOrderId, double executionRate, long expireDate, int filledAmount, bool netQuantity, string offerId, string orderId, int originAmount, string parties, double pegOffset, string pegType, string primaryId, double rate, double rateMax, double rateMin, string requestId, string requestTxt, string stage, string status, long statusTime, string timeInForce, string tradeId, double trailRate, int trailStep, string type, string valueDate, bool workingIndicator )
        {


            DetailedOrderDB fxOrder = new DetailedOrderDB(  accountId,
                                                            accountKind,
                                                            mainLoginName,
                                                            accountName,
                                                            amount,
                                                            atMarket,
                                                            buySell,
                                                            contingencyType,
                                                            contingentOrderId,
                                                            executionRate,
                                                            expireDate,
                                                            filledAmount,
                                                            netQuantity,
                                                            offerId,
                                                            orderId,
                                                            originAmount,
                                                            parties,
                                                            pegOffset,
                                                            pegType,
                                                            primaryId,
                                                            rate,
                                                            rateMax,
                                                            rateMin,
                                                            requestId,
                                                            requestTxt,
                                                            stage,
                                                            status,
                                                            statusTime,
                                                            timeInForce,
                                                            tradeId,
                                                            trailRate,
                                                            trailStep,
                                                            type,
                                                            valueDate,
                                                            workingIndicator
                                         );

            TryUpdateOrder( fxOrder );


        }

        public void TryUpdateOrder( IDetailedOrder fxOrder )
        {            
            if ( _orderIdToOrder.ContainsKey( fxOrder.OrderID ) )
            {
                var detailedOrdersBindingList = GFMgr.GetDetailedOrdersBindingList( );

                int index = detailedOrdersBindingList.FindIndex( x => x.OrderID == fxOrder.OrderID );

                if ( index >= 0 )
                {
                    IDetailedOrder foundItem = detailedOrdersBindingList[ index ];

                    if ( !foundItem.Equals( fxOrder ) )
                    {
                        // Tony Lam: Instead of just reference the whole thing, I set each and every property indidividually.
                        //detailedOrdersBindingList [ index ] = fxOrder;

                        detailedOrdersBindingList[ index ].CopyFrom( fxOrder );

                        _orderIdToOrder[ fxOrder.OrderID ] = fxOrder;

                        TryUpdateSimpleOrder( fxOrder );
                    }
                }
            }
        }


        public bool TryUpdateSimpleOrder( IDetailedOrder simpleOrder )
        {
            var myOrdersForNewPositionBindingList = GFMgr.GetOrdersForNewPositionBindingList();

            var updatedOrder = new FxOrdersForNewPosition(  simpleOrder.OrderID,
                                                            simpleOrder.AccountID,
                                                            simpleOrder.MainLoginName,
                                                            simpleOrder.AccountName,
                                                            GFMgr.GetOrderTypeEnum( simpleOrder.Type ),
                                                            simpleOrder.Status,
                                                            new fxSymbol( GFMgr.GetSymbolFromOfferId( simpleOrder.OfferID ) ),
                                                            simpleOrder.Amount,
                                                            simpleOrder.BuySell == "S" ? simpleOrder.Rate : 0,
                                                            simpleOrder.BuySell == "B" ? simpleOrder.Rate : 0,
                                                            0,
                                                            0,
                                                            simpleOrder.StatusTime.FromLinuxTime( ),
                                                            simpleOrder.ExpireDate.FromLinuxTime( ),
                                                            simpleOrder.RequestTXT,
                                                            simpleOrder.TradeID, null, null );

            int index = myOrdersForNewPositionBindingList.FindIndex( x => x.OrderId == updatedOrder.OrderId );

            if ( index > -1 )
            {
                myOrdersForNewPositionBindingList[ index ] = updatedOrder;
            }            

            return false;
        }

        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\OrdesAndPos.png"/>
        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\DatabaseOrdersAndPos.png"/>

        public void DetailedOrdersToExistingPositionsOrderOrNewPositionOrders( )
        {
            var detailedOrdersBindingList = GFMgr.GetDetailedOrdersBindingList( );

            var primaryOrders = detailedOrdersBindingList.Where( x => x.Stage == "O" )
                                                         .Select( x => new FxOrdersForNewPosition(  x.OrderID,
                                                                                                    x.AccountID,
                                                                                                    x.MainLoginName,
                                                                                                    x.AccountName,
                                                                                                    GFMgr.GetOrderTypeEnum( x.Type ),
                                                                                                    x.Status,
                                                                                                    new fxSymbol(  GFMgr.GetSymbolFromOfferId(x.OfferID  ) ),
                                                                                                    x.Amount,
                                                                                                    x.BuySell == "S" ? x.Rate : 0,
                                                                                                    x.BuySell == "B" ? x.Rate : 0,
                                                                                                    0,
                                                                                                    0,
                                                                                                    x.StatusTime.FromLinuxTime( ),
                                                                                                    x.ExpireDate.FromLinuxTime( ),
                                                                                                    x.RequestTXT,
                                                                                                    x.TradeID, null, null) ).ToList();

            foreach ( var primaryOrder in primaryOrders )
            {
                var conditionalOrders = detailedOrdersBindingList.Where( x => x.Stage == "C" && x.TradeID == primaryOrder.TradeId  );

                if ( conditionalOrders.Count( ) > 0 )
                {
                    foreach ( var conditionalOrder in conditionalOrders )
                    {
                        if ( conditionalOrder.BuySell == "B" && primaryOrder.SellPrice > 0 && conditionalOrder.Stage == "C" )
                        {
                            if ( conditionalOrder.Rate <= primaryOrder.SellPrice && conditionalOrder.Type == "L" )
                            {
                                primaryOrder.LimitOrder = conditionalOrder;
                                
                            }
                            else if ( conditionalOrder.Rate > primaryOrder.SellPrice && ( conditionalOrder.Type == "S" || conditionalOrder.Type == "ST" ) )
                            {
                                primaryOrder.StopOrder = conditionalOrder;
                                
                            }
                        }
                        else if ( conditionalOrder.BuySell == "S" && primaryOrder.BuyPrice > 0 && conditionalOrder.Stage == "C" )
                        {
                            if ( conditionalOrder.Rate >= primaryOrder.BuyPrice && conditionalOrder.Type == "L" )
                            {
                                primaryOrder.LimitOrder = conditionalOrder;
                                
                            }
                            else if ( conditionalOrder.Rate < primaryOrder.BuyPrice && ( conditionalOrder.Type == "S" || conditionalOrder.Type == "ST" ) )
                            {
                                primaryOrder.StopOrder = conditionalOrder;
                                
                            }
                        }
                    }
                }

                AddSimpleOrderToBindingList( primaryOrder );
            }

            // Now we want to find out all the orders for the Existing positions.

            var primaryOrdersTradeId    = primaryOrders.Select( x => x.TradeId ).ToArray( );
            var existingPositionsOrders = detailedOrdersBindingList.Where( x => ! primaryOrdersTradeId.Contains( x.TradeID  ));

            var fxOpenPositionPendingOrderInfoBindingList = GFMgr.GetOpenPositionAndOrdersBindingList( );

            foreach ( var existingPositionsOrder in existingPositionsOrders )
            {
                var openPositionAndOrdersBindingList = fxOpenPositionPendingOrderInfoBindingList;

                var index3 = openPositionAndOrdersBindingList.FindIndex( x => x.Ticket == existingPositionsOrder.TradeID );

                if ( index3 > -1 )
                {
                    var existingPosition = openPositionAndOrdersBindingList[ index3 ];

                    if ( existingPositionsOrder.Type == "L" )
                    {
                        existingPosition.LimitOrder = existingPositionsOrder;                       
                    }
                    else if ( existingPositionsOrder.Type == "S" || existingPositionsOrder.Type == "ST" )
                    {
                        existingPosition.StopOrder = existingPositionsOrder;                        
                    }
                }
                else
                {
                    // In a way, this order has caused the position to be closed. Like when the positions are closed due to margin call.
                    string message = string.Format( "Trade {0} has been closed, the status is {1}", existingPositionsOrder.TradeID, GFMgr.GetOrderTypeEnum( existingPositionsOrder.Type ).ToString() );                    
                }
            }

        }


        

        private void AddSimpleOrderToBindingList( FxOrdersForNewPosition simpleOrder )
        {
            var myOrdersForNewPositionBindingList = GFMgr.GetOrdersForNewPositionBindingList();

            int index = myOrdersForNewPositionBindingList.FindIndex( x => x.OrderId == simpleOrder.OrderId );

            if ( index == -1 )
            {
                myOrdersForNewPositionBindingList.Add( simpleOrder );
            }
            else
            {
                myOrdersForNewPositionBindingList[ index ] = simpleOrder;
            }            
        }

        public void HandleOffer( FxOffer offerEvent )
        {
            var accountsSummaryBindingLists = GFMgr.GetAccountSummaryBindingList();

            foreach ( var account in _detailedAccountsCollection )
            {
                _accountCurrency = account.AccountCurrency;

                var grossPl = CalcOpenPositions( offerEvent, account.AccountName, _accountCurrency );

                TaskCalcAccountSummary( accountsSummaryBindingLists, account.AccountName, _usableMarginPercentage, grossPl, _accountCurrency, GFMgr.HasSmartMargin( account.LeverageProfileID ) );

                TaskCalcPositionSummary( account.AccountName, offerEvent, grossPl, _accountCurrency );
            }
        }

        public void HandleInitialOffer( FxOffer offerEvent )
        {
            var accountsSummaryBindingLists = GFMgr.GetAccountSummaryBindingList();

            foreach ( var account in _detailedAccountsCollection )
            {
                _accountCurrency = account.AccountCurrency;

                var grossPl = CalcOpenPositions( offerEvent, account.AccountName, _accountCurrency );

                TaskCalcAccountSummary( accountsSummaryBindingLists, account.AccountName, _usableMarginPercentage, grossPl, _accountCurrency, GFMgr.HasSmartMargin( account.LeverageProfileID ) );

                TaskCalcPositionSummary( account.AccountName, offerEvent, grossPl, _accountCurrency );
            }
        }

        /// <summary>
        /// http://blogs.msdn.com/b/pfxteam/archive/2008/05/28/8556655.aspx
        /// Multiple thread-local state elements in a loop
        /// 
        /// TODO: Profit and loss is also based on the underlying currency
        /// </summary>
        /// <param name="offerEvent"></param>
        /// <returns></returns>
        protected OpenPositionsPl CalcOpenPositions( FxOffer offerEvent, string accountName, string accountCurrency )
        {
            object lockObject = new object();

            var openPositionsProfitLossReturn = new OpenPositionsPl();

            var tempList = GFMgr.GetOpenPositionAndOrdersByAccountName( accountName );


            Parallel.ForEach
            (
                tempList
                ,
                ( ) => new OpenPositionsPl { ProfitLoss = 0 },

                ( position, loopState, localObject ) =>
                {
                    if ( position.Symbol == offerEvent.Instrument )
                    {
                        var calcValue = new FxPositionOrderCalculatedValue( );

                        double toUsdRate = 1;

                        if ( position.IsBuy )
                        {
                            toUsdRate = GFMgr.GetAccountCurrencyToUsdRate( accountCurrency, true );
                            double profitPerPip      = GFMgr.GetPipValuePerAmount( offerEvent.Instrument, offerEvent.Bid, position.Amount, true );

                            calcValue.ClosePrice = offerEvent.Bid;
                            calcValue.PipProfit = Math.Round( ( offerEvent.Bid - position.OpenPrice ) / GFMgr.GetInstrumentPointSize( offerEvent.Instrument ), 1 );
                            calcValue.GrossProfit = Math.Round( ( calcValue.PipProfit * profitPerPip * toUsdRate ), 2 );

                            position.CalculatedValue = calcValue;

                            if ( localObject.SymbolToNetBuyProfit.ContainsKey( position.Symbol ) )
                            {
                                localObject.SymbolToNetBuyProfit[ position.Symbol ] += calcValue.GrossProfit;
                            }
                            else
                            {
                                localObject.SymbolToNetBuyProfit[ position.Symbol ] = calcValue.GrossProfit;
                            }
                        }
                        else
                        {
                            toUsdRate = GFMgr.GetAccountCurrencyToUsdRate( accountCurrency, false );

                            double profitPerPip      = GFMgr.GetPipValuePerAmount( offerEvent.Instrument, offerEvent.Ask, position.Amount, false );

                            calcValue.ClosePrice = offerEvent.Ask;
                            calcValue.PipProfit = Math.Round( ( position.OpenPrice - offerEvent.Ask ) / GFMgr.GetInstrumentPointSize( offerEvent.Instrument ), 1 );

                            calcValue.GrossProfit = Math.Round( ( calcValue.PipProfit * profitPerPip * toUsdRate ), 2 );

                            position.CalculatedValue = calcValue;

                            if ( localObject.SymbolToNetSellProfit.ContainsKey( position.Symbol ) )
                            {
                                localObject.SymbolToNetSellProfit[ position.Symbol ] += calcValue.GrossProfit;
                            }
                            else
                            {
                                localObject.SymbolToNetSellProfit[ position.Symbol ] = calcValue.GrossProfit;
                            }
                        }

                        localObject.ProfitLoss += calcValue.GrossProfit;



                        return localObject;
                    }

                    localObject.ProfitLoss += position.GrossProfit;

                    if ( position.IsBuy )
                    {
                        if ( localObject.SymbolToNetBuyProfit.ContainsKey( position.Symbol ) )
                        {
                            localObject.SymbolToNetBuyProfit[ position.Symbol ] += position.GrossProfit;
                        }
                        else
                        {
                            localObject.SymbolToNetBuyProfit[ position.Symbol ] = position.GrossProfit;
                        }
                    }
                    else
                    {
                        if ( localObject.SymbolToNetSellProfit.ContainsKey( position.Symbol ) )
                        {
                            localObject.SymbolToNetSellProfit[ position.Symbol ] += position.GrossProfit;
                        }
                        else
                        {
                            localObject.SymbolToNetSellProfit[ position.Symbol ] = position.GrossProfit;
                        }
                    }


                    return localObject;
                },

                ( partial ) =>
                {

                    // Enforce serial access to single, shared result
                    lock ( lockObject )
                    {
                        openPositionsProfitLossReturn.ProfitLoss += partial.ProfitLoss;

                        foreach ( KeyValuePair<string, double> keyValuePairString in partial.SymbolToNetBuyProfit )
                        {
                            if ( openPositionsProfitLossReturn.SymbolToNetBuyProfit.ContainsKey( keyValuePairString.Key ) )
                            {
                                openPositionsProfitLossReturn.SymbolToNetBuyProfit[ keyValuePairString.Key ] += keyValuePairString.Value;
                            }
                            else
                            {
                                openPositionsProfitLossReturn.SymbolToNetBuyProfit[ keyValuePairString.Key ] = keyValuePairString.Value;
                            }
                        }

                        foreach ( KeyValuePair<string, double> keyValuePairString in partial.SymbolToNetSellProfit )
                        {
                            if ( openPositionsProfitLossReturn.SymbolToNetSellProfit.ContainsKey( keyValuePairString.Key ) )
                            {
                                openPositionsProfitLossReturn.SymbolToNetSellProfit[ keyValuePairString.Key ] += keyValuePairString.Value;
                            }
                            else
                            {
                                openPositionsProfitLossReturn.SymbolToNetSellProfit[ keyValuePairString.Key ] = keyValuePairString.Value;
                            }
                        }
                    }
                }
            );

            return openPositionsProfitLossReturn;
        }

        protected void TaskCalcPositionSummary( string accountName, FxOffer offerEvent, OpenPositionsPl openPositionsProfit, string accountCurrency )
        {
            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

            try
            {
                foreach ( IPositionsSummary positionsSummary in fxPositionsSummaryBindingList )
                {
                    if ( ( positionsSummary.AccountName == accountName ) && ( positionsSummary.Symbol == offerEvent.Instrument ) )
                    {
                        string symbol = positionsSummary.Symbol;

                        var calcValue = new PositionCalculatedValue( );

                        double toUsdRate = 1;

                        if ( positionsSummary.BuyAmount > 0 )
                        {
                            toUsdRate = GFMgr.GetAccountCurrencyToUsdRate( accountCurrency, true );

                            calcValue.BuyClose = offerEvent.Bid;

                            if ( openPositionsProfit.SymbolToNetBuyProfit.ContainsKey( symbol ) )
                            {
                                calcValue.BuyNetPl = Math.Round( openPositionsProfit.SymbolToNetBuyProfit[ symbol ], 2 );
                                calcValue.GrossPl += calcValue.BuyNetPl.Value * toUsdRate;
                            }
                        }

                        if ( positionsSummary.SellAmount > 0 )
                        {
                            toUsdRate = GFMgr.GetAccountCurrencyToUsdRate( accountCurrency, false );

                            calcValue.SellClose = offerEvent.Ask;

                            if ( openPositionsProfit.SymbolToNetSellProfit.ContainsKey( symbol ) )
                            {
                                calcValue.SellNetPl = Math.Round( openPositionsProfit.SymbolToNetSellProfit[ symbol ], 2 );
                                calcValue.GrossPl += calcValue.SellNetPl.Value * toUsdRate;
                            }
                        }

                        positionsSummary.CalculatedValue = calcValue;
                    }
                }
            }
            catch ( Exception ex )
            {
                
            }


        }

        protected void InitialTaskCalcPositionSummary( string accountName, FxOffer offerEvent, OpenPositionsPl openPositionsProfit, string accountCurrency )
        {
            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList().ToList();



            try
            {
                foreach ( IPositionsSummary positionsSummary in fxPositionsSummaryBindingList )
                {
                    if ( ( positionsSummary.AccountName == accountName ) && ( positionsSummary.Symbol == offerEvent.Instrument ) )
                    {
                        string symbol = positionsSummary.Symbol;

                        var calcValue = new PositionCalculatedValue( );

                        double toUsdRate = 1;

                        if ( positionsSummary.BuyAmount > 0 )
                        {
                            toUsdRate = GFMgr.GetAccountCurrencyToUsdRate( accountCurrency, true );

                            calcValue.BuyClose = offerEvent.Bid;

                            if ( openPositionsProfit.SymbolToNetBuyProfit.ContainsKey( symbol ) )
                            {
                                calcValue.BuyNetPl = Math.Round( openPositionsProfit.SymbolToNetBuyProfit[ symbol ], 2 );
                                calcValue.GrossPl += calcValue.BuyNetPl.Value * toUsdRate;
                            }
                        }

                        if ( positionsSummary.SellAmount > 0 )
                        {
                            toUsdRate = GFMgr.GetAccountCurrencyToUsdRate( accountCurrency, false );

                            calcValue.SellClose = offerEvent.Ask;

                            if ( openPositionsProfit.SymbolToNetSellProfit.ContainsKey( symbol ) )
                            {
                                calcValue.SellNetPl = Math.Round( openPositionsProfit.SymbolToNetSellProfit[ symbol ], 2 );
                                calcValue.GrossPl += calcValue.SellNetPl.Value * toUsdRate;
                            }
                        }

                        positionsSummary.CalculatedValue = calcValue;
                    }
                }
            }
            catch ( Exception ex )
            {

            }


        }

        public void AddDetailedAccountsCollection( FxDetailedAccountsCollection accountCollection )
        {
            if ( _detailedAccountsCollection == null )
            {
                _detailedAccountsCollection = accountCollection;
            }

            BuildAccountSummaryBindingList( );
        }

        public void BuildAccountSummaryBindingList( )
        {
            if ( _detailedAccountsCollection.Count == 0 ) return;

            foreach ( IDetailedAccount detailedAccount in _detailedAccountsCollection )
            {
                FxAccountSummary accountSummary = new FxAccountSummary( detailedAccount );

                AddAccountToBindingList( accountSummary );
            }
        }

        private void UiAddAccountToBindingList( FxAccountSummary accountSummary )
        {
            var accountsSummaryBindingList = GFMgr.GetAccountSummaryBindingList();


            int index = accountsSummaryBindingList.FindIndex( x => x.AccountName == accountSummary.AccountName );

            accountSummary.MainLoginName = _mainLoginName;            

            if ( index == -1 )
            {
                accountsSummaryBindingList.Add( accountSummary );
            }
            else
            {
                accountsSummaryBindingList[ index ] = accountSummary;
            }

            TaskCalcAccountSummary( accountsSummaryBindingList, accountSummary.AccountName, _usableMarginPercentage, null, _accountCurrency, accountSummary.SmartMargin );
        }

        private void AddAccountToBindingList( FxAccountSummary accountSummary )
        {
            var accountsSummaryBindingList = GFMgr.GetAccountSummaryBindingList();

            UiAddAccountToBindingList( accountSummary );
        }

        private void UiUpdateAccountSummaryBindingList( FxDetailedAccount fxAccount )
        {
            var accountsSummaryBindingList = GFMgr.GetAccountSummaryBindingList();

            int index = accountsSummaryBindingList.FindIndex( x => x.AccountName == fxAccount.AccountName );

            if ( index > -1 )
            {
                accountsSummaryBindingList[ index ].CopyFrom( _mainLoginName, fxAccount );
            }
        }

        private void UpdateMargin( string accountName )
        {
            var bindingList = GFMgr.GetAccountSummaryBindingList();

            int index = bindingList.FindIndex( x => x.AccountName == accountName );

            if ( index > -1 )
            {
                bindingList[ index ].UpdateMargin( _mainLoginName, accountName );
            }
        }


        public void UpdateAccountSummaryBindingList( string mainLoginName, FxDetailedAccount fxAccount )
        {
            var accountsSummaryBindingList = GFMgr.GetAccountSummaryBindingList();

            UiUpdateAccountSummaryBindingList( fxAccount );
        }

        public void RemoveAccountFromBindingList( string accountId )
        {

        }
        public void StartInitialPnLTask( string accountName )
        {
            //var fxPositionsSummaryBindingList = SymbolsMgr.Instance.GetPositionSummaryBindingList();
            var accountsSummaryBindingLists = GFMgr.GetAccountSummaryBindingList();

            foreach ( KeyValuePair<string, List<string>> keyValuePairString in _instrumentToTradeIdCollection )
            {
                string instrument = keyValuePairString.Key;
                List< string > tradeIds = keyValuePairString.Value;
                int offerId = GFMgr.GetOfferId( instrument );

                var rates = GFMgr.GetOffer( instrument );

                var initialOffer = new FxOffer();

                initialOffer.Ask        = rates.Ask;
                initialOffer.Bid        = rates.Bid;
                initialOffer.Instrument = instrument;
                initialOffer.Digits     = GFMgr.GetInstrumentDigits( instrument );

                foreach ( var account in _detailedAccountsCollection )
                {
                    _accountCurrency = account.AccountCurrency;

                    var grossPl = CalcOpenPositions( initialOffer, account.AccountName, _accountCurrency );

                    TaskCalcAccountSummary( accountsSummaryBindingLists, account.AccountName, _usableMarginPercentage, grossPl, _accountCurrency, GFMgr.HasSmartMargin( account.LeverageProfileID ) );

                    InitialTaskCalcPositionSummary( account.AccountName, initialOffer, grossPl, _accountCurrency );
                }
            }
        }

        public void FinalizeAccountSummary( string accountName, O2GClosedTradeRow row )
        {
            //var fxPositionsSummaryBindingList = SymbolsMgr.Instance.GetPositionSummaryBindingList();
            var accountsSummaryBindingLists = GFMgr.GetAccountSummaryBindingList();

            int index = -1;

            index = accountsSummaryBindingLists.FindIndex( x => x.AccountName == accountName );

            if ( index > -1 )
            {
                var grossPl = row.GrossPL;

                var accountSummary = accountsSummaryBindingLists[ index ];
                
                var calcValue = accountSummary.CalculatedValue;                


                if ( calcValue.GrossPl  > 0 )
                {
                    calcValue.GrossPl -= grossPl;
                }
                
                calcValue.DayPl = Math.Round( ( accountSummary.Balance - accountSummary.BeginningEquity + grossPl ), 2 );
                calcValue.Equity = Math.Round( ( accountSummary.Balance + grossPl ), 2 );
                calcValue.UsableMargin = calcValue.Equity - accountSummary.UsedMargin;
                calcValue.UsableMaintMargin = calcValue.Equity - accountSummary.UsedMaintMargin * ( accountSummary.SmartMargin ? 2 : 1 );
                calcValue.PreMarginCallPercentage = Math.Round( ( calcValue.UsableMaintMargin / calcValue.Equity * 100 ), 2 );
                calcValue.PreLiquidationPercentage = Math.Round( ( calcValue.UsableMargin / calcValue.Equity * 100 ), 2 );
            }            
        }

        public bool HasPositions( string symbol )
        {
            if ( _instrumentToTradeIdCollection.ContainsKey( symbol ) )
            {
                return true;
            }

            return false;
        }




        public ChangePriceLevelsOrdersData GetChangePriceLevelsOrdersData( string accountName, ChangeStopLimitSafetyMsg priceLevels )
        {
            var ordersData = new ChangePriceLevelsOrdersData();

            var bindingList     = GFMgr.GetOpenPositionAndOrdersByAccountName( accountName );

            var priceTargets    = priceLevels.PriceLevels;

            var targetCount     = priceTargets.Count;

            var ascendingPrice  = priceTargets.OrderBy(i => i).ToList();

            var descendingOrder = priceTargets.OrderByDescending(i => i).ToList();

            float priceTarget   = 0.0f;

            int count = 0;

            foreach ( var existingPosition in bindingList )
            {
                int offerId = GFMgr.GetOfferId( existingPosition.Symbol );

                if ( existingPosition.IsBuy )
                {
                    priceTarget = ascendingPrice[ count % targetCount ];

                    if ( priceTarget > existingPosition.OpenPrice && priceLevels.MessageType == PriceLevelsType.TAKE_PROFIT )
                    {
                        if ( existingPosition.LimitOrder != null )
                        {
                            var modifiedOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.LimitOrder.TradeID, existingPosition.LimitOrder.OrderID, ChangeOrderType.MODIFIED, PriceLevelsType.TAKE_PROFIT, priceTarget, FxOrderSide.Sell, existingPosition.Amount );

                            ordersData.ModifiedOrders.Add( modifiedOrder );
                        }
                        else
                        {
                            var newOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.Ticket, ChangeOrderType.NEW, PriceLevelsType.TAKE_PROFIT, priceTarget, FxOrderSide.Sell, existingPosition.Amount );

                            ordersData.NewOrders.Add( newOrder );
                        }
                    }
                    else if ( priceTarget < existingPosition.OpenPrice && priceLevels.MessageType == PriceLevelsType.STOP_LOSS )
                    {
                        if ( existingPosition.StopOrder != null )
                        {
                            var modifiedOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.StopOrder.TradeID, existingPosition.LimitOrder.OrderID, ChangeOrderType.MODIFIED, PriceLevelsType.TAKE_PROFIT, priceTarget, FxOrderSide.Sell, existingPosition.Amount );

                            ordersData.ModifiedOrders.Add( modifiedOrder );
                        }
                        else
                        {
                            var newOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.Ticket, ChangeOrderType.NEW, PriceLevelsType.STOP_LOSS, priceTarget, FxOrderSide.Sell, existingPosition.Amount );

                            ordersData.NewOrders.Add( newOrder );
                        }
                    }

                    count++;
                }
                else
                {
                    priceTarget = descendingOrder[ count % targetCount ];

                    if ( priceTarget < existingPosition.OpenPrice && priceLevels.MessageType == PriceLevelsType.TAKE_PROFIT )
                    {
                        if ( existingPosition.LimitOrder != null )
                        {
                            var modifiedOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.LimitOrder.TradeID, existingPosition.LimitOrder.OrderID, ChangeOrderType.MODIFIED, PriceLevelsType.TAKE_PROFIT, priceTarget, FxOrderSide.Buy, existingPosition.Amount );

                            ordersData.ModifiedOrders.Add( modifiedOrder );
                        }
                        else
                        {
                            var newOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.Ticket, ChangeOrderType.NEW, PriceLevelsType.TAKE_PROFIT, priceTarget, FxOrderSide.Buy, existingPosition.Amount );

                            ordersData.NewOrders.Add( newOrder );
                        }
                    }
                    else if ( priceTarget > existingPosition.OpenPrice && priceLevels.MessageType == PriceLevelsType.STOP_LOSS )
                    {
                        if ( existingPosition.StopOrder != null )
                        {
                            var modifiedOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.StopOrder.TradeID, existingPosition.LimitOrder.OrderID, ChangeOrderType.MODIFIED, PriceLevelsType.TAKE_PROFIT, priceTarget, FxOrderSide.Sell, existingPosition.Amount );

                            ordersData.ModifiedOrders.Add( modifiedOrder );
                        }
                        else
                        {
                            var newOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.Ticket, ChangeOrderType.NEW, PriceLevelsType.STOP_LOSS, priceTarget, FxOrderSide.Buy, existingPosition.Amount );

                            ordersData.NewOrders.Add( newOrder );
                        }
                    }

                    count++;
                }
            }

            return ordersData;
        }

        public bool SafetyNetBroken( string accountName, string symbol, double safetyPip, out double brokenAmount )
        {
            brokenAmount = 0;

            var myOrdersForNewPositionBindingList = GFMgr.GetOrdersForNewPositionBindingList();

            var fxPositionsSummaryBindingList = GFMgr.GetPositionSummaryBindingList();

            foreach ( var summary in fxPositionsSummaryBindingList )
            {
                if ( summary.AccountName == accountName )
                {
                    string offeridstring   = summary.OfferId;

                    int offerId = int.Parse( offeridstring );

                    var currentAskBid = GFMgr.GetOffer( Int32.Parse( offeridstring ) );

                    int amount =  (int) ( summary.BuyAmount.Value - summary.SellAmount.Value );

                    // Set opposite side

                    if ( amount == 0 )
                    {
                        // means all positions have been hedged
                        return false;
                    }

                    FxOrderSide positionSide = ( amount > 0 ) ? FxOrderSide.Buy : FxOrderSide.Sell;


                    if ( positionSide == FxOrderSide.Sell )
                    {

                        foreach ( var order in myOrdersForNewPositionBindingList )
                        {
                            if ( order.AccountName == accountName && order.Comment == "ShortSafetyNet" )
                            {
                                return false;
                            }
                        }


                    }
                    else if ( positionSide == FxOrderSide.Buy )
                    {
                        foreach ( var order in myOrdersForNewPositionBindingList )
                        {
                            if ( order.AccountName == accountName && order.Comment == "LongSafetyNet" )
                            {
                                return false;
                            }
                        }

                    }
                    else
                    {
                        throw new InvalidProgramException( );
                    }

                    double currentPrice = 0;
                    double safetyeNetPrice = 0;
                    //double avgPrice = 0;


                    if ( positionSide == FxOrderSide.Buy )
                    {
                        currentPrice = GFMgr.GetRate( offerId, false );
                        safetyeNetPrice = summary.BuyAvgOpen.Value - safetyPip;

                        if ( currentPrice < safetyeNetPrice )
                        {
                            brokenAmount = safetyeNetPrice - currentPrice;
                            return true;
                        }
                    }
                    else
                    {
                        currentPrice = GFMgr.GetRate( offerId, true );

                        safetyeNetPrice = summary.SellAvgOpen.Value + safetyPip;

                        if ( currentPrice > safetyeNetPrice )
                        {
                            brokenAmount = currentPrice - safetyeNetPrice;
                            return true;
                        }
                    }
                }

            }


            return false;
        }
    }


    
}
