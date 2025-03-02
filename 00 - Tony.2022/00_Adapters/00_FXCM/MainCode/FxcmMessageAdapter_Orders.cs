namespace StockSharp.FxConnectFXCM
{
    using System;
    using System.Collections.Generic; 
    using System.Linq;

    using Ecng.Collections;
    using Ecng.Common;
    using fx.Messages;
    using fxcore2;
    using MoreLinq;
    using StockSharp.Algo.Testing;
    using StockSharp.FxConnectFXCM.Freemind;
    using StockSharp.Localization;
    using StockSharp.Logging;
    using StockSharp.Messages;

    partial class FxConnectFxcmMsgAdapter
    {
        private string PortfolioName => "FxConnectFxcmMsgAdapter" + "_" + Login;

        #region Order Processing
        private void OnOrdersTableLoaded( ISubscriptionMessage msg, O2GResponse response )
        {
            var factory = GetSession( ).getResponseReaderFactory( );
            if ( factory == null )
            {
                throw new InvalidOperationException( );
            }

            var reader      = factory.createOrdersTableReader( response );
            var accountName = string.Empty;
            var accountId   = string.Empty;

            SubaccountTradeDataRepo tradeManager = null;

            for ( int i = 0; i < reader.Count; i++ )
            {
                O2GOrderRow row = reader.getRow( i );

                accountName = row.AccountName;

                DetailedOrderDB fxOrder = new DetailedOrderDB(  row.AccountID,
                                                                row.AccountKind,
                                                                Login,
                                                                row.AccountName,
                                                                row.Amount,
                                                                row.AtMarket,
                                                                row.BuySell,
                                                                row.ContingencyType.To<int>(),
                                                                row.ContingentOrderID,
                                                                row.ExecutionRate,
                                                                row.ExpireDate.ToLinuxTime(),
                                                                row.FilledAmount,
                                                                row.NetQuantity,
                                                                row.OfferID,
                                                                row.OrderID,
                                                                row.OriginAmount,
                                                                row.Parties,
                                                                row.PegOffset,
                                                                row.PegType,
                                                                row.PrimaryID,
                                                                row.Rate,
                                                                row.RateMax,
                                                                row.RateMin,
                                                                row.RequestID,
                                                                row.RequestTXT,
                                                                row.Stage,
                                                                row.Status,
                                                                row.StatusTime.ToLinuxTime(),
                                                                row.TimeInForce,
                                                                row.TradeID,
                                                                row.TrailRate,
                                                                row.TrailStep,
                                                                row.Type,
                                                                row.ValueDate,
                                                                row.WorkingIndicator );
                if ( tradeManager == null )
                {
                    tradeManager = GFMgr.CreateSubaccountTradeDataRepoByAccountName( Login, accountName );
                }

                tradeManager.AddOrder( fxOrder );
                // 				
            }

            if ( reader.Count > 0 )
            {
                tradeManager.DetailedOrdersToExistingPositionsOrderOrNewPositionOrders( );

                var ordersMsg = GFMgr.GetOrdersMessage( Login, accountName );

                foreach ( var posMsg in ordersMsg )
                {
                    SendOutMessage( posMsg );
                }
            }
        }
        private void OnOrdersTableUpdate( O2GOrderRow row, O2GTableUpdateType updateType )
        {
            string accountName = row.AccountName;

            if ( string.IsNullOrEmpty( accountName ) )
            {
                return;
            }

            string instrument = GFMgr.GetSymbolFromOfferId( row.OfferID );

            var customId = row.RequestTXT.Split(',').Select(x => x.Split('=')).Where(x => x.Length > 1 && !String.IsNullOrEmpty(x[0].Trim()) && !String.IsNullOrEmpty(x[1].Trim())).ToDictionary(x => x[0].Trim(), x => x[1].Trim());

            long myRequestId = 0;

            if ( customId.ContainsKey( "ID" ) )
            {
                myRequestId = customId[ "ID" ].To< int >( );
            }


            var fxOrder      = new DetailedOrderDB( Login, row );

            var orderMsg     = GFMgr.GetOrderMessage( fxOrder, myRequestId );

            var tradeManager = GFMgr.CreateSubaccountTradeDataRepoByAccountName( Login, accountName );

            if ( updateType == O2GTableUpdateType.Insert )
            {
                tradeManager.AddOrder( fxOrder );

                tradeManager.DetailedOrdersToExistingPositionsOrderOrNewPositionOrders( );
            }
            else if ( updateType == O2GTableUpdateType.Update )
            {
                tradeManager.TryUpdateOrder( fxOrder );

                tradeManager.TryUpdateSimpleOrder( fxOrder );
            }
            else if ( updateType == O2GTableUpdateType.Delete )
            {
                tradeManager.RemoveOrder( instrument, row.OrderID );

                orderMsg.OrderState = OrderStates.Done;
            }

            SendOutMessage( orderMsg );
        }

        #endregion

        #region Order Group Cancel
        private void ProcessOrderGroupCancel( OrderGroupCancelMessage cancelMsg )
        {
            var accountName = cancelMsg.PortfolioName;

            if ( !string.IsNullOrEmpty( accountName ) )
            {
                FxOrderType type = FxOrderType.All;
                string symbol = "";
                Sides? orderSide = null;

                if ( cancelMsg.SecurityId.SecurityCode != null )
                {
                    symbol = cancelMsg.SecurityId.SecurityCode;
                }

                if ( cancelMsg.IsStop.HasValue )
                {
                    type = FxOrderType.Stop;
                }

                if ( cancelMsg.Side.HasValue )
                {
                    orderSide = cancelMsg.Side;
                }


                if ( RemoveGroupOrders( accountName, cancelMsg.OriginalTransactionId, type, symbol, orderSide ) )
                {
                    SendOutMessage(
                                    new ExecutionMessage
                                    {
                                        ServerTime = CurrentTime.ConvertToUtc( ),
                                        DataTypeEx = DataType.Transactions,
                                        OriginalTransactionId = cancelMsg.TransactionId,
                                        HasOrderInfo = true,
                                    }
                              );
                }



                // ProcessOrderStatus( null );
                // ProcessPortfolioLookup( null );
            }
        }

        //public bool CloseXPositions( string accountName, PositionsType posType, long transId )
        //{
        //	if ( !string.IsNullOrEmpty( accountName ) )
        //	{
        //		SubaccountTradeDataRepo tradeDataRepo = SymbolsMgr.Instance.GetSubaccountTradeDataRepoByName( accountName );

        //		O2GRequest request = null;

        //		if ( tradeDataRepo != null )
        //		{
        //			switch ( posType )
        //			{
        //				case PositionsType.All:
        //				{

        //					Dictionary<string, CloseOrdersData> newOrdersData = tradeDataRepo.GetCloseAllPositionsOrdersData( accountName );

        //					if ( newOrdersData.Values.Count == 0 )
        //					{								
        //						return false;
        //					}

        //					request = CreateCloseAllPositionsRequest( newOrdersData );
        //				}
        //				break;

        //				case PositionsType.Lossing:
        //				case PositionsType.Winning:
        //				case PositionsType.Long:
        //				case PositionsType.Short:
        //				case PositionsType.LossingHedge:
        //				case PositionsType.WinningHedge:
        //				{
        //					List<CloseSomeOrdersData> newOrdersData = tradeDataRepo.GetCloseSomePositionsOrdersData( accountName, posType );

        //					if ( newOrdersData.Count == 0 )
        //					{								
        //						return false;
        //					}

        //					request = CreateCloseSomePositionsRequest( newOrdersData );
        //				}
        //				break;

        //			}

        //			O2GSession o2Gsession = GetSession( );
        //			O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );

        //			if ( requestFactory == null )
        //			{
        //				throw new InvalidOperationException( );
        //			}

        //			if ( request == null )
        //			{
        //				var error = requestFactory.getLastError( );

        //				throw new InvalidOperationException( error );
        //			}

        //			_requestIdToTransactionId.Add( request.RequestID, transId );
        //			o2Gsession.sendRequest( request );
        //		}
        //	}

        //	return false;


        //}

        public bool RemoveGroupOrders( string accountName, long transId, FxOrderType type, string symbol, Sides? orderSide )
        {
            SubaccountTradeDataRepo tradeDataRepo = GFMgr.GetSubaccountTradeDataRepoByName( accountName );

            if ( tradeDataRepo != null )
            {
                List<RemoveOrdersData> newOrdersData = tradeDataRepo.GetRemoveOrdersData( accountName, type, symbol, orderSide );

                if ( newOrdersData == null || newOrdersData.Count == 0 )
                {
                    //SystemMonitor.Error( "There are no opened positions" );

                    return false;
                }

                O2GRequest request = CreateRemoveXOrdersRequest( newOrdersData, transId );

                O2GSession o2Gsession = GetSession( );

                if ( o2Gsession == null )
                    return false;

                O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );

                if ( requestFactory == null )
                {
                    throw new InvalidOperationException( );
                }

                if ( request == null )
                {
                    var error = requestFactory.getLastError( );

                    throw new InvalidOperationException( error );
                }

                _requestIdToTransactionId.Add( request.RequestID, transId );
                o2Gsession.sendRequest( request );

                return true;
            }

            return false;

        }

        public O2GRequest CreateRemoveXOrdersRequest( List<RemoveOrdersData> someOrdersData, long transId )
        {
            O2GRequestFactory requestFactory = _fxSessionId.getRequestFactory();
            O2GRequest request = null;

            if ( requestFactory == null )
            {
                throw new Exception( "Cannot create request factory" );
            }

            O2GValueMap batchValuemap = requestFactory.createValueMap();
            batchValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.DeleteOrder );

            O2GValueMap childValuemap;

            foreach ( RemoveOrdersData order in someOrdersData )
            {
                childValuemap = requestFactory.createValueMap( );
                childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.DeleteOrder );
                childValuemap.setString( O2GRequestParamsEnum.AccountID, order.AccountId );
                childValuemap.setString( O2GRequestParamsEnum.OrderID, order.OrderId );
                childValuemap.setString( O2GRequestParamsEnum.CustomID, "ID=" + transId.To<string>( ) );

                batchValuemap.appendChild( childValuemap );
            }

            request = requestFactory.createOrderRequest( batchValuemap );

            if ( request == null )
            {
                // SystemMonitor.Error( requestFactory.getLastError( ) );
            }

            return request;
        }

        private void ProcessOrderGroupCancelMessage( OrderGroupCancelMessage msg )
        {
            //foreach ( var myOrder in _session.GetOrders( ) )
            //{
            //    var deleteOrder = new DeleteOrderParams( ) { OrderId = myOrder.OrderId };

            //    _session.DeleteOrder( deleteOrder );
            //}
        }

        #endregion



        private void ProcessOrderStatusMessage( OrderStatusMessage orderMsg )
        {
            //if ( StringHelper.IsEmpty( orderMsg.PortfolioName ) )
            //{
            //	return;
            //}

            SetupTableResponseHandler( O2GTableType.Orders, orderMsg, orderMsg.PortfolioName, new Action<ISubscriptionMessage, O2GResponse>( OnOrdersTableLoaded ) );
        }

        private void ProcessOrderRegisterMessage( OrderRegisterMessage orderMsg )
        {
            var posEffect = orderMsg.PositionEffect;

            if ( posEffect.HasValue )
            {
                switch ( posEffect.Value )
                {
                    case OrderPositionEffects.OpenOnly:
                    {
                        OpenPosition( orderMsg );
                    }
                    break;

                    case OrderPositionEffects.CloseOnly:
                    {
                        ClosePosition( orderMsg );
                    }
                    break;

                    case OrderPositionEffects.HedgeLong:
                    {
                        HedgeAllLongPositions( orderMsg );
                    }
                    break;

                    case OrderPositionEffects.HedgeShort:
                    {
                        HedgeAllShortPositions( orderMsg );
                    }
                    break;

                    case OrderPositionEffects.HedgeAll:
                    {
                        HedgeAllPositions( orderMsg );
                    }
                    break;

                    case OrderPositionEffects.SetSafety:
                    {
                        SetSafetyNet( orderMsg );
                    }
                    break;

                    case OrderPositionEffects.SetTakeProfit:
                    {
                        SetTakeProfit( orderMsg );
                    }
                    break;

                    case OrderPositionEffects.SetBreakEven:
                    {                        
                        SetBreakEven( orderMsg );
                    }
                    break;

                    case OrderPositionEffects.ReverseDirection:
                    {
                        ReverseDirection( orderMsg );
                    }
                    break;

                    case OrderPositionEffects.EscapeWithoutLoss:
                    {
                        EscapeWithoutLoss( orderMsg );
                    }
                    break;
                }
            }
            else
            {
                OpenPosition( orderMsg );
            }
        }

        private void EscapeWithoutLoss( OrderRegisterMessage orderMsg )
        {
            var accountName    = orderMsg.PortfolioName;

            if ( !string.IsNullOrEmpty( accountName ) )
            {
                SubaccountTradeDataRepo tradeDataRepo = GFMgr.GetSubaccountTradeDataRepoByName( accountName );

                if ( tradeDataRepo != null )
                {
                }
            }
        }

        private bool ReverseDirection( OrderRegisterMessage orderMsg )
        {
            var accountName  = orderMsg.PortfolioName;
            var offerId = (string) orderMsg.SecurityId.Native;
            var fxOrder      = ( FreemindOrderCondition ) orderMsg.Condition;
            var withEscape   = fxOrder.WithEscape.HasValue ? fxOrder.WithEscape.Value : true;

            if ( !string.IsNullOrEmpty( accountName ) )
            {
                SubaccountTradeDataRepo tradeDataRepo = GFMgr.GetSubaccountTradeDataRepoByName( accountName );

                if ( tradeDataRepo != null )
                {
                    tradeDataRepo.StartInitialPnLTask( accountName );

                    var newOrdersData = tradeDataRepo.GetReverseAndEsacpeOrdersData( accountName, offerId, withEscape );

                    if ( newOrdersData.Count == 0 )
                    {
                        this.AddErrorLog( "There are no opened positions to Reverse Direction" );

                        return false;
                    }

                    CreateAndSendReverseAllPositionsRequest( newOrdersData, orderMsg.TransactionId );                    
                }
            }

            return false;
        }

        public bool CreateAndSendReverseAllPositionsRequest( List<ReverseAndEscapeOrdersData> newOrdersData, long transId )
        {
            O2GSession o2Gsession = GetSession( );

            if ( o2Gsession == null )
                return false;

            O2GRequestFactory requestFactory = o2Gsession.getRequestFactory();

            if ( requestFactory == null )
            {
                this.AddErrorLog( "o2Gsession.getRequestFactory return null" );
                return false;
            }
            
            O2GRequest reverseOrderRequest = null;
            

            var customId = "ID=" + transId.To< string >( ) + "," + "PE=" + ( ( int )OrderPositionEffects.ReverseDirection ).To< string >( );

            // There are two parts to the orders, first we will create the opposite direction to original buy/sell
            foreach ( var reverseOrder in newOrdersData )
            {
                O2GValueMap batchValuemap = requestFactory.createValueMap();
                batchValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );

                var sOfferId   = reverseOrder.OfferId;
                var sAccountId = reverseOrder.AccountId;
                var side       = reverseOrder.Side;
                var amount     = reverseOrder.Amount;

                O2GValueMap childValuemap;

                if ( amount > 0 )
                {
                    switch ( side )
                    { 
                        // Here we create the reverse order at market 
                        case FxOrderSide.Buy:
                        {
                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketOpen );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Buy );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );
                            batchValuemap.appendChild( childValuemap );
                        }
                        break;

                        case FxOrderSide.Sell:
                        {
                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketOpen );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Sell );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );
                            batchValuemap.appendChild( childValuemap );
                        }
                        break;
                    }

                    reverseOrderRequest = requestFactory.createOrderRequest( batchValuemap );

                    if ( reverseOrderRequest == null )
                    {
                        this.AddErrorLog( requestFactory.getLastError( ) );

                        return false;
                    }

                    _requestIdToTransactionId.Add( reverseOrderRequest.RequestID, transId );
                    o2Gsession.sendRequest( reverseOrderRequest );
                }
               
                O2GRequest modifiedRequest = null;                

                if ( reverseOrder.ModifiedOrders.Count > 0 )
                {
                    modifiedRequest = CreateModifiedStopLossTakeProfitRequest( reverseOrder.ModifiedOrders, transId );

                    if ( modifiedRequest == null )
                    {
                        this.AddErrorLog( requestFactory.getLastError( ) );

                        return false;
                    }

                    _requestIdToTransactionId.Add( modifiedRequest.RequestID, transId );
                    o2Gsession.sendRequest( modifiedRequest );
                }

                O2GRequest newRequest = null;

                if ( reverseOrder.NewOrders.Count > 0 )
                {
                    newRequest = CreateNewStopLossRequest( reverseOrder.NewOrders, transId );

                    if ( newRequest == null )
                    {
                        this.AddErrorLog( requestFactory.getLastError( ) );

                        return false;
                    }

                    _requestIdToTransactionId.Add( newRequest.RequestID, transId );
                    o2Gsession.sendRequest( newRequest );
                }                
            }
            
            return true;
        }

        private O2GValueMap AddOrModifiedStopLossAndTakeProfit( EscapeOrdersData order )
        {
            throw new NotImplementedException( );
        }

        private bool SetBreakEven( OrderRegisterMessage orderMsg )
        {
            var accountName = orderMsg.PortfolioName;
            var fxOrder     = ( FreemindOrderCondition ) orderMsg.Condition;            
            var profitPip   = fxOrder.TakeProfitPips.HasValue ? fxOrder.TakeProfitPips.Value.To<double>() : 50;

            if ( !string.IsNullOrEmpty( accountName ) )
            {
                SubaccountTradeDataRepo tradeDataRepo = GFMgr.GetSubaccountTradeDataRepoByName( accountName );                

                if ( tradeDataRepo != null )
                {
                    tradeDataRepo.StartInitialPnLTask( accountName );

                    List<BreakEvenStopLossOrdersData> newOrdersData = tradeDataRepo.GetBreakEvenStopLossOrdersData( accountName, profitPip );

                    if ( newOrdersData == null || newOrdersData.Count == 0 )
                    {
                        string errorMsg = string.Format( "There are no opened positions with Profit pips of {0}", profitPip );
                        
                        this.AddErrorLog( errorMsg );

                        return false;
                    }

                    O2GRequest request = CreateBreakEvenStopLossRequest( newOrdersData, orderMsg.TransactionId );

                    O2GSession o2Gsession = GetSession( );

                    if ( o2Gsession == null )
                        return false;

                    O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );

                    if ( requestFactory == null )
                    {
                        throw new InvalidOperationException( );
                    }

                    if ( request == null )
                    {
                        var error = requestFactory.getLastError( );

                        throw new InvalidOperationException( error );
                    }

                    _requestIdToTransactionId.Add( request.RequestID, orderMsg.TransactionId );
                    o2Gsession.sendRequest( request ); 
                    
                    return true;                    
                }
            }

            return false;
        }

        public O2GRequest CreateBreakEvenStopLossRequest( List<BreakEvenStopLossOrdersData> someOrdersData, long transId )
        {
            O2GRequestFactory requestFactory = _fxSessionId.getRequestFactory();
            O2GRequest request = null;

            if ( requestFactory == null )
            {
                throw new Exception( "Cannot create request factory" );
            }

            O2GValueMap batchValuemap = requestFactory.createValueMap();
            batchValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );

            var enumerator = someOrdersData.GetEnumerator();

            foreach ( BreakEvenStopLossOrdersData order in someOrdersData )
            {
                string sInstrument = GFMgr.GetSymbolFromOfferId( order.OfferId );

                O2GValueMap childValuemap;

                var customId = "ID=" + transId.To< string >( ) + "," + "PE=" + ( ( int )OrderPositionEffects.SetBreakEven ).To< string >( );

                switch ( order.Side )
                {
                    case FxOrderSide.Buy:
                    {
                        childValuemap = requestFactory.createValueMap( );
                        childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                        childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.Stop );
                        childValuemap.setString( O2GRequestParamsEnum.TradeID, order.TradeId );
                        childValuemap.setString( O2GRequestParamsEnum.AccountID, order.AccountId );
                        childValuemap.setString( O2GRequestParamsEnum.OfferID, order.OfferId );
                        childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Buy );
                        childValuemap.setInt( O2GRequestParamsEnum.Amount, order.Amount );
                        childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                        childValuemap.setDouble( O2GRequestParamsEnum.Rate, order.StopLossRate );
                        batchValuemap.appendChild( childValuemap );
                    }
                    break;

                    case FxOrderSide.Sell:
                    {
                        childValuemap = requestFactory.createValueMap( );
                        childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                        childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.Stop );
                        childValuemap.setString( O2GRequestParamsEnum.TradeID, order.TradeId );
                        childValuemap.setString( O2GRequestParamsEnum.AccountID, order.AccountId );
                        childValuemap.setString( O2GRequestParamsEnum.OfferID, order.OfferId );
                        childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Sell );
                        childValuemap.setInt( O2GRequestParamsEnum.Amount, order.Amount );
                        childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                        childValuemap.setDouble( O2GRequestParamsEnum.Rate, order.StopLossRate );
                        batchValuemap.appendChild( childValuemap );
                    }
                    break;

                    default:
                        throw new NotSupportedException( "Orders cannot be other types" );

                }
            }

            request = requestFactory.createOrderRequest( batchValuemap );

            if ( request == null )
            {
                this.AddErrorLog( requestFactory.getLastError( ) );
            }

            return request;
        }


        private bool SetSafetyNet( OrderRegisterMessage orderMsg )
        {
            var accountName    = orderMsg.PortfolioName;
            var symbol         = orderMsg.SecurityId.SecurityCode;

            var fxOrder      = ( FreemindOrderCondition ) orderMsg.Condition;
            var safetyPip      = fxOrder.StopLossPips.HasValue ? fxOrder.StopLossPips.Value.To<double>() : 50;

            if ( !string.IsNullOrEmpty( accountName ) )
            {
                SubaccountTradeDataRepo tradeDataRepo = GFMgr.GetSubaccountTradeDataRepoByName( accountName );

                if ( tradeDataRepo != null )
                {
                    if ( !tradeDataRepo.HasSafetyNet( accountName ) )
                    {
                        double brokenAmount = 0.0;

                        if ( tradeDataRepo.SafetyNetBroken( accountName, symbol, safetyPip, out brokenAmount ) )
                        {
                            string ErrorMessage = string.Format( "[{0}] SYMBOL={1} Safety Net Broken by {2} PIPS", accountName, symbol, brokenAmount );

                            this.AddErrorLog( ErrorMessage );                            

                            return false;
                        }

                        var orders = GFMgr.GetOrdersForNewPositionBindingListByAccountName( accountName );

                        var positionSummary = GFMgr.GetPositionSummaryBindingListByAccountNameAndSymbol( accountName, symbol );

                        if ( orders.Count == 0 && positionSummary == null )
                        {
                            return false;
                        }

                        if ( positionSummary.IsNetLong( ) )
                        {
                            foreach ( var order in orders )
                            {
                                if ( order.isSell( ) )
                                {
                                    if ( order.Amount >= positionSummary.BuyAmount )
                                    {
                                        return true;
                                    }
                                }
                            }
                        }

                        if ( positionSummary.IsNetShort( ) )
                        {
                            foreach ( var order in orders )
                            {
                                if ( order.isBuy( ) )
                                {
                                    if ( order.Amount >= positionSummary.SellAmount )
                                    {
                                        return true;
                                    }
                                }
                            }
                        }


                        var newOrdersData = tradeDataRepo.GetSafetyNetOrdersData( accountName, safetyPip );

                        if ( newOrdersData == null || newOrdersData.Count == 0 )
                        {
                            this.AddErrorLog( "There are no opened positions" );

                            return false;
                        }

                        O2GRequest request = CreateSetSafetyNetRequest( newOrdersData, orderMsg.TransactionId );

                        O2GSession o2Gsession = GetSession( );

                        if ( o2Gsession == null )
                            return false;

                        O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );

                        if ( requestFactory == null )
                        {
                            throw new InvalidOperationException( );
                        }

                        if ( request == null )
                        {
                            var error = requestFactory.getLastError( );

                            throw new InvalidOperationException( error );
                        }

                        _requestIdToTransactionId.Add( request.RequestID, orderMsg.TransactionId );
                        o2Gsession.sendRequest( request );
                    }
                }
            }

            return false;
        }

        public O2GRequest CreateSetSafetyNetRequest( Dictionary<string, SafetyNetOrdersData> hedgeOrdersData, long transId )
        {
            O2GRequestFactory requestFactory = _fxSessionId.getRequestFactory();
            O2GRequest request = null;

            if ( requestFactory == null )
            {
                throw new Exception( "Cannot create request factory" );
            }

            O2GValueMap batchValuemap = requestFactory.createValueMap();
            batchValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );

            Dictionary<string, SafetyNetOrdersData>.Enumerator enumerator = hedgeOrdersData.GetEnumerator();

            while ( enumerator.MoveNext( ) )
            {
                string sOfferId   = enumerator.Current.Key;
                string sAccountId = enumerator.Current.Value.AccountId;
                FxOrderSide side  = enumerator.Current.Value.Side;
                int amount        = enumerator.Current.Value.Amount;
                double rate       = enumerator.Current.Value.Price;

                if ( amount > 0 )
                {
                    O2GValueMap childValuemap;

                    var customId = "ID=" + transId.To< string >( ) + "," + "PE=" + ( ( int )OrderPositionEffects.SetSafety ).To< string >( );

                    switch ( side )
                    {
                        case FxOrderSide.Buy:
                        {                            
                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.StopEntry );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Buy );
                            childValuemap.setDouble( O2GRequestParamsEnum.Rate, rate );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );
                            batchValuemap.appendChild( childValuemap );
                        }
                            
                            break;

                        case FxOrderSide.Sell:
                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.StopEntry );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Sell );
                            childValuemap.setDouble( O2GRequestParamsEnum.Rate, rate );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );
                            batchValuemap.appendChild( childValuemap );
                            break;
                    }
                }

            }

            request = requestFactory.createOrderRequest( batchValuemap );

            if ( request == null )
            {
                this.AddErrorLog( requestFactory.getLastError( ) );
            }

            return request;
        }

        private bool SetTakeProfit( OrderRegisterMessage orderMsg )
        {
            bool output     = false;
            var accountName = orderMsg.PortfolioName;
            var fxOrder     = ( FreemindOrderCondition ) orderMsg.Condition;
            var profitPip   = fxOrder.TakeProfitPips.HasValue ? fxOrder.TakeProfitPips.Value.To<double>() : 50;
            
            if ( !string.IsNullOrEmpty( accountName ) )
            {
                var newOrdersData     = GetChangePriceLevelsOrdersData( accountName, PriceLevelsType.TAKE_PROFIT, profitPip );

                if ( newOrdersData == null || newOrdersData.Count == 0 )
                {
                    this.AddErrorLog( "There are no opened positions" );

                    return false;
                }

                O2GRequest modifiedRequest = null;
                O2GRequest newRequest = null;

                if ( newOrdersData.ModifiedOrders.Count > 0 )
                {
                    modifiedRequest = CreateModifiedStopLossTakeProfitRequest( newOrdersData.ModifiedOrders, orderMsg.TransactionId );

                    if ( modifiedRequest != null )
                    {
                        O2GSession o2Gsession = GetSession( );

                        if ( o2Gsession == null )
                            return false;

                        O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );

                        if ( requestFactory == null )
                        {
                            throw new InvalidOperationException( );
                        }

                        if ( modifiedRequest == null )
                        {
                            var error = requestFactory.getLastError( );

                            throw new InvalidOperationException( error );
                        }

                        _requestIdToTransactionId.Add( modifiedRequest.RequestID, orderMsg.TransactionId );
                        o2Gsession.sendRequest( modifiedRequest );
                    }
                }
                
                if ( newOrdersData.NewOrders.Count > 0 )
                {
                    newRequest = CreateNewStopLossRequest( newOrdersData.NewOrders, orderMsg.TransactionId );

                    if ( newRequest != null )
                    {
                        O2GSession o2Gsession = GetSession( );

                        if ( o2Gsession == null )
                            return false;

                        O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );

                        if ( requestFactory == null )
                        {
                            throw new InvalidOperationException( );
                        }

                        if ( newRequest == null )
                        {
                            var error = requestFactory.getLastError( );

                            throw new InvalidOperationException( error );
                        }

                        _requestIdToTransactionId.Add( newRequest.RequestID, orderMsg.TransactionId );
                        o2Gsession.sendRequest( newRequest );
                    }
                }

                
            }

            return output;
        }

        private O2GRequest CreateModifiedStopLossTakeProfitRequest( List<PriceLevelsOrdersData> newOrdersData, long transId )
        {
            O2GRequestFactory requestFactory = _fxSessionId.getRequestFactory();
            O2GRequest request = null;

            if ( requestFactory == null )
            {
                throw new Exception( "Cannot create request factory" );
            }

            O2GValueMap batchValuemap = requestFactory.createValueMap();
            batchValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.EditOrder );

            foreach ( PriceLevelsOrdersData data in newOrdersData )
            {
                string sOfferId   = data.OfferId;
                string sAccountId = data.AccountId;
                FxOrderSide side  = data.Side;
                int amount        = data.Amount;
                double rate       = data.Price;
                string tradeId    = data.TradeId;
                string orderId    = data.OrderId;

                var targetType    = data.PriceType == PriceLevelsType.STOP_LOSS ? Constants.Orders.Stop: Constants.Orders.Limit;

                var peType        = data.PriceType == PriceLevelsType.STOP_LOSS ? ( int )OrderPositionEffects.SetSafety : ( int )OrderPositionEffects.SetTakeProfit;

                if ( amount > 0 )
                {
                    O2GValueMap childValuemap;

                    var customId = "ID=" + transId.To< string >( ) + "," + "PE=" + peType.To< string >( );

                    switch ( side )
                    {
                        case FxOrderSide.Buy:
                        {
                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.EditOrder );
                            childValuemap.setString( O2GRequestParamsEnum.TradeID, tradeId );
                            childValuemap.setString( O2GRequestParamsEnum.OrderID, orderId );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, targetType );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Buy );
                            childValuemap.setDouble( O2GRequestParamsEnum.Rate, rate );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                            batchValuemap.appendChild( childValuemap );
                        }
                        break;

                        case FxOrderSide.Sell:
                        {
                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.EditOrder );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.EditOrder );
                            childValuemap.setString( O2GRequestParamsEnum.TradeID, tradeId );
                            childValuemap.setString( O2GRequestParamsEnum.OrderID, orderId );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, targetType );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Sell );
                            childValuemap.setDouble( O2GRequestParamsEnum.Rate, rate );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                            batchValuemap.appendChild( childValuemap );
                        }
                        break;

                    }
                }
            }

            request = requestFactory.createOrderRequest( batchValuemap );

            if ( request == null )
            {
                this.AddErrorLog( requestFactory.getLastError( ) );
            }

            return request;
        }

        private O2GRequest CreateNewStopLossRequest( List<PriceLevelsOrdersData> newOrdersData, long transId )
        {
            O2GRequestFactory requestFactory = _fxSessionId.getRequestFactory();
            O2GRequest request = null;

            if ( requestFactory == null )
            {
                throw new Exception( "Cannot create request factory" );
            }

            O2GValueMap batchValuemap = requestFactory.createValueMap();
            batchValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );

            foreach ( PriceLevelsOrdersData data in newOrdersData )
            {
                if ( data.ChangePriceOrderType == ChangeOrderType.CLOSED && data.PriceType == PriceLevelsType.NO_CHANGE )
                {
                    
                    string sInstrument = GFMgr.GetSymbolFromOfferId( data.OfferId );

                    var o2Gsession = GetSession( );
                    if ( o2Gsession == null )
                        return null;

                    var permissionChecker = o2Gsession.getLoginRules().getPermissionChecker();

                    if ( permissionChecker.canCreateMarketCloseOrder( sInstrument ) != O2GPermissionStatus.PermissionEnabled )
                    {
                        throw new NotSupportedException( "Need capability to close Market order" );
                    }

                    O2GValueMap childValuemap;

                    switch ( data.Side )
                    {
                        case FxOrderSide.Buy:
                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketClose );
                            childValuemap.setString( O2GRequestParamsEnum.TradeID, data.TradeId );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, data.AccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, data.OfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Buy );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, data.Amount );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, "ID=" + transId.To<string>( ) );
                            batchValuemap.appendChild( childValuemap );
                            break;

                        case FxOrderSide.Sell:
                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketClose );
                            childValuemap.setString( O2GRequestParamsEnum.TradeID, data.TradeId );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, data.AccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, data.OfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Sell );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, data.Amount );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, "ID=" + transId.To<string>( ) );
                            batchValuemap.appendChild( childValuemap );
                            break;

                        default:
                            throw new NotSupportedException( "Orders cannot be other types" );

                    }
                    
                }
                else
                {
                    string sOfferId   = data.OfferId;
                    string sAccountId = data.AccountId;
                    FxOrderSide side  = data.Side;
                    int amount        = data.Amount;
                    double rate       = data.Price;
                    string tradeId    = data.TradeId;

                    var targetType    = data.PriceType == PriceLevelsType.STOP_LOSS ? Constants.Orders.Stop: Constants.Orders.Limit;


                    var peType        = data.PriceType == PriceLevelsType.STOP_LOSS ? ( int )OrderPositionEffects.SetSafety : ( int )OrderPositionEffects.SetTakeProfit;


                    if ( amount > 0 )
                    {
                        O2GValueMap childValuemap;

                        var customId = "ID=" + transId.To< string >( ) + "," + "PE=" + peType.To< string >( );

                        switch ( side )
                        {
                            case FxOrderSide.Buy:
                            {
                                childValuemap = requestFactory.createValueMap( );
                                childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                                childValuemap.setString( O2GRequestParamsEnum.OrderType, targetType );
                                childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                                childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                                childValuemap.setString( O2GRequestParamsEnum.TradeID, tradeId );
                                childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Buy );
                                childValuemap.setDouble( O2GRequestParamsEnum.Rate, rate );
                                childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                                childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );

                                batchValuemap.appendChild( childValuemap );
                            }
                            break;

                            case FxOrderSide.Sell:
                            {
                                childValuemap = requestFactory.createValueMap( );
                                childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                                childValuemap.setString( O2GRequestParamsEnum.OrderType, targetType );
                                childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                                childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                                childValuemap.setString( O2GRequestParamsEnum.TradeID, tradeId );
                                childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Sell );
                                childValuemap.setDouble( O2GRequestParamsEnum.Rate, rate );
                                childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                                childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );
                                batchValuemap.appendChild( childValuemap );
                            }
                            break;
                        }
                    }
                }

                
            }

            request = requestFactory.createOrderRequest( batchValuemap );

            if ( request == null )
            {
                this.AddErrorLog( requestFactory.getLastError( ) );
            }

            return request;
        }


        public ChangePriceLevelsOrdersData GetChangePriceLevelsOrdersData( string accountName, PriceLevelsType priceLevelsType, double pips )
        {
            var ordersData = new ChangePriceLevelsOrdersData();

            var bindingList     = GFMgr.GetOpenPositionAndOrdersByAccountName( accountName );            

            foreach ( var existingPosition in bindingList )
            {
                int offerId = GFMgr.GetOfferId( existingPosition.Symbol );

                var pointSize = GFMgr.GetInstrumentPointSize( existingPosition.Symbol );

                if ( existingPosition.IsBuy )
                {                    
                    if ( priceLevelsType == PriceLevelsType.TAKE_PROFIT )
                    {
                        double priceTarget = existingPosition.OpenPrice + pips * pointSize;

                        if ( existingPosition.LimitOrder != null )
                        {
                            var modifiedOrder = new PriceLevelsOrdersData( 
                                                                            existingPosition.AccountID, 
                                                                            offerId.ToString(), 
                                                                            existingPosition.LimitOrder.TradeID, 
                                                                            existingPosition.LimitOrder.OrderID, 
                                                                            ChangeOrderType.MODIFIED, 
                                                                            PriceLevelsType.TAKE_PROFIT, priceTarget,FxOrderSide.Sell, existingPosition.Amount );

                            ordersData.ModifiedOrders.Add( modifiedOrder );
                        }
                        else
                        {
                            var newOrder = new PriceLevelsOrdersData( 
                                                                            existingPosition.AccountID, 
                                                                            offerId.ToString(), 
                                                                            existingPosition.Ticket, 
                                                                            ChangeOrderType.NEW, 
                                                                            PriceLevelsType.TAKE_PROFIT, priceTarget,FxOrderSide.Sell, existingPosition.Amount );

                            ordersData.NewOrders.Add( newOrder );
                        }
                    }
                    else if ( priceLevelsType == PriceLevelsType.STOP_LOSS )
                    {
                        double priceTarget = existingPosition.OpenPrice - pips * pointSize;

                        if ( existingPosition.StopOrder != null )
                        {
                            var modifiedOrder = new PriceLevelsOrdersData( 
                                                                            existingPosition.AccountID, 
                                                                            offerId.ToString(), 
                                                                            existingPosition.StopOrder.TradeID, 
                                                                            existingPosition.LimitOrder.OrderID, 
                                                                            ChangeOrderType.MODIFIED, 
                                                                            PriceLevelsType.TAKE_PROFIT, 
                                                                            priceTarget,
                                                                            FxOrderSide.Sell, 
                                                                            existingPosition.Amount 
                                                                        );

                            ordersData.ModifiedOrders.Add( modifiedOrder );
                        }
                        else
                        {
                            var newOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.Ticket, ChangeOrderType.NEW, PriceLevelsType.STOP_LOSS, priceTarget,FxOrderSide.Sell, existingPosition.Amount );

                            ordersData.NewOrders.Add( newOrder );
                        }
                    }                    
                }
                else
                {                    
                    if ( priceLevelsType == PriceLevelsType.TAKE_PROFIT )
                    {
                        double priceTarget = existingPosition.OpenPrice - pips * pointSize;

                        if ( existingPosition.LimitOrder != null )
                        {
                            var modifiedOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.LimitOrder.TradeID, existingPosition.LimitOrder.OrderID, ChangeOrderType.MODIFIED, PriceLevelsType.TAKE_PROFIT, priceTarget,FxOrderSide.Buy, existingPosition.Amount );

                            ordersData.ModifiedOrders.Add( modifiedOrder );
                        }
                        else
                        {
                            var newOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.Ticket, ChangeOrderType.NEW, PriceLevelsType.TAKE_PROFIT, priceTarget,FxOrderSide.Buy, existingPosition.Amount );

                            ordersData.NewOrders.Add( newOrder );
                        }
                    }
                    else if ( priceLevelsType == PriceLevelsType.STOP_LOSS )
                    {
                        double priceTarget = existingPosition.OpenPrice + pips * pointSize;

                        if ( existingPosition.StopOrder != null )
                        {
                            var modifiedOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.StopOrder.TradeID, existingPosition.LimitOrder.OrderID, ChangeOrderType.MODIFIED, PriceLevelsType.TAKE_PROFIT, priceTarget,FxOrderSide.Sell, existingPosition.Amount );

                            ordersData.ModifiedOrders.Add( modifiedOrder );
                        }
                        else
                        {
                            var newOrder = new PriceLevelsOrdersData( existingPosition.AccountID, offerId.ToString(), existingPosition.Ticket, ChangeOrderType.NEW, PriceLevelsType.STOP_LOSS, priceTarget,FxOrderSide.Buy, existingPosition.Amount );

                            ordersData.NewOrders.Add( newOrder );
                        }
                    }                    
                }
            }

            return ordersData;
        }

        private void OpenPosition( OrderRegisterMessage orderMsg )
        {
            OrderTypes? oType = orderMsg.OrderType;
            if ( oType.HasValue && oType.GetValueOrDefault( ) > OrderTypes.Conditional )
            {
                throw new NotSupportedException( StringHelper.Put( LocalizedStrings.Str1601Params, new object[ 2 ]
                                                                                                            {
                                                                                                                orderMsg.OrderType,
                                                                                                                orderMsg.TransactionId
                                                                                                            } ) );
            }

            O2GSession o2Gsession = GetSession( );

            if ( o2Gsession == null )
                return;

            O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );

            if ( requestFactory == null )
            {
                throw new InvalidOperationException( );
            }

            string orderType = "LE";
            FxcmOrderCondition condition = ( FxcmOrderCondition )orderMsg.Condition;

            if ( oType.HasValue && oType.GetValueOrDefault( ) == OrderTypes.Conditional )
            {
                if ( condition != null && condition.ExtendedType.HasValue )
                {
                    orderType = condition.ExtendedType.Value.ToShortName( );
                }
            }
            else if ( orderMsg.Price == decimal.Zero )
            {
                orderType = "OM";
            }

            string tif = orderMsg.TimeInForce.ToFxcmTIF( orderMsg.TillDate );
            O2GValueMap valueMap = requestFactory.createValueMap( );

            var accountId = GFMgr.GetAccountIdFromName( Login, orderMsg.PortfolioName );

            valueMap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
            valueMap.setString( O2GRequestParamsEnum.OrderType, orderType );
            valueMap.setString( O2GRequestParamsEnum.AccountID, accountId );
            valueMap.setString( O2GRequestParamsEnum.OfferID, orderMsg.SecurityId.ToNativeString( ) );
            valueMap.setString( O2GRequestParamsEnum.BuySell, orderMsg.Side.ToSidesString( ) );
            valueMap.setString( O2GRequestParamsEnum.TimeInForce, tif );
            valueMap.setString( O2GRequestParamsEnum.CustomID, "ID=" + orderMsg.TransactionId.To<string>( ) );

            if ( tif == "GTD" )
            {
                valueMap.setString( O2GRequestParamsEnum.ExpireDayTime, orderMsg.TillDate.Value.UtcDateTime.ToString( "yyyyMMdd-HH:mm:ss.SSS" ) );
            }

            if ( orderMsg.Price != decimal.Zero )
            {
                valueMap.setDouble( O2GRequestParamsEnum.Rate, ( double ) orderMsg.Price );
            }

            int offerId = Int32.Parse( ( string ) orderMsg.SecurityId.Native );

            var subscribedSymbol = GFMgr.GetSubscribedSymbolsByAccountNameAndId( Login, offerId );

            int baseUnit = 1;

            if ( subscribedSymbol != null )
            {
                baseUnit = subscribedSymbol.BaseUnitSize;
            }

            valueMap.setInt( O2GRequestParamsEnum.Amount, ( int ) orderMsg.Volume * baseUnit );
            valueMap.setString( O2GRequestParamsEnum.CustomID, "ID=" + orderMsg.TransactionId.To<string>( ) );

            if ( condition != null )
            {
                if ( condition.RateMin.HasValue )
                {
                    O2GValueMap o2GvalueMap = valueMap;
                    o2GvalueMap.setDouble( O2GRequestParamsEnum.RateMin, ( double ) condition.RateMin.Value );
                }

                if ( condition.RateMax.HasValue )
                {
                    O2GValueMap o2GvalueMap = valueMap;
                    o2GvalueMap.setDouble( O2GRequestParamsEnum.RateMax, ( double ) condition.RateMax.Value );
                }

                if ( condition.TrailStep.HasValue )
                {
                    valueMap.setInt( O2GRequestParamsEnum.TrailStep, condition.TrailStep.Value );
                }

                if ( condition.ContingentOrderId != null )
                {
                    valueMap.setString( O2GRequestParamsEnum.ContingencyID, condition.ContingentOrderId );
                }

                if ( condition.ContingencyType.HasValue )
                {
                    valueMap.setInt( O2GRequestParamsEnum.ContingencyGroupType, ( int ) condition.ContingencyType.Value );
                }

                if ( condition.PegOffset.HasValue )
                {
                    O2GValueMap o2GvalueMap = valueMap;
                    o2GvalueMap.setDouble( O2GRequestParamsEnum.PegOffset, ( double ) condition.PegOffset.Value );
                }

                if ( condition.PegType != null )
                {
                    valueMap.setString( O2GRequestParamsEnum.PegType, condition.PegType );
                }
            }

            O2GRequest orderRequest = requestFactory.createOrderRequest( valueMap );
            if ( orderRequest == null )
            {
                var error = requestFactory.getLastError( );

                throw new InvalidOperationException( error );
            }

            _requestIdToTransactionId.Add( orderRequest.RequestID, orderMsg.TransactionId );
            o2Gsession.sendRequest( orderRequest );
        }

        private bool ClosePosition( OrderRegisterMessage orderMsg )
        {
            var accountName = orderMsg.PortfolioName;

            var posType = orderMsg.ClosePositionType;

            if ( !string.IsNullOrEmpty( accountName ) && posType.HasValue )
            {
                SubaccountTradeDataRepo tradeDataRepo = GFMgr.GetSubaccountTradeDataRepoByName( accountName );

                O2GRequest request = null;

                if ( tradeDataRepo != null )
                {
                    switch ( posType.Value )
                    {
                        case Messages.ClosePositionsType.All:
                        {
                            Dictionary<string, CloseOrdersData> newOrdersData = tradeDataRepo.GetCloseAllPositionsOrdersData( accountName );

                            if ( newOrdersData.Values.Count == 0 )
                            {
                                this.AddWarningLog( "There are no positions to be closed" );

                                return false;
                            }

                            request = CreateCloseAllPositionsRequest( newOrdersData, orderMsg.TransactionId );
                        }
                        break;

                        case Messages.ClosePositionsType.Lossing:
                        case Messages.ClosePositionsType.Winning:
                        case Messages.ClosePositionsType.LossingHedge:
                        case Messages.ClosePositionsType.WinningHedge:
                        {
                            // Since I am not streaming for the Offers, so we have to caculate the latest profit and loss
                            tradeDataRepo.StartInitialPnLTask( accountName );
                            List<CloseSomeOrdersData> newOrdersData = tradeDataRepo.GetCloseSomePositionsOrdersData( accountName, posType.Value );

                            if ( newOrdersData.Count == 0 )
                            {
                                this.AddWarningLog( "There are no positions to be closed" );

                                return false;
                            }

                            request = CreateCloseSomePositionsRequest( newOrdersData, orderMsg.TransactionId );
                        }
                        break;

                        case Messages.ClosePositionsType.Long:
                        case Messages.ClosePositionsType.Short:
                        case Messages.ClosePositionsType.AllHedge:
                        case Messages.ClosePositionsType.LongHedge:
                        case Messages.ClosePositionsType.ShortHedge:
                        {                            
                            List<CloseSomeOrdersData> newOrdersData = tradeDataRepo.GetCloseSomePositionsOrdersData( accountName, posType.Value );

                            if ( newOrdersData.Count == 0 )
                            {
                                this.AddWarningLog( "There are no positions to be closed" );

                                return false;
                            }

                            request = CreateCloseSomePositionsRequest( newOrdersData, orderMsg.TransactionId );
                        }
                        break;

                    }

                    O2GSession o2Gsession = GetSession( );

                    if ( o2Gsession == null )
                        return false;

                    O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );

                    if ( requestFactory == null )
                    {
                        throw new InvalidOperationException( );
                    }

                    if ( request == null )
                    {
                        var error = requestFactory.getLastError( );

                        throw new InvalidOperationException( error );
                    }

                    _requestIdToTransactionId.Add( request.RequestID, orderMsg.TransactionId );
                    o2Gsession.sendRequest( request );

                    return true;


                }
            }

            return false;
        }

        private bool HedgeAllLongPositions( OrderRegisterMessage orderMsg )
        {
            string accountName = orderMsg.PortfolioName;

            if ( !string.IsNullOrEmpty( accountName ) )
            {
                SubaccountTradeDataRepo tradeDataRepo = GFMgr.GetSubaccountTradeDataRepoByName( accountName );

                if ( tradeDataRepo != null )
                {

                    Dictionary<string, HedgeOrdersData> newOrdersData = tradeDataRepo.GetHedgeLongOrdersData( accountName );

                    if ( newOrdersData == null || newOrdersData.Values.Count == 0 )
                    {
                        this.AddWarningLog( "There are no opened positions" );

                        return false;
                    }

                    O2GRequest request = CreateHedgeAllLongPositionsRequest( newOrdersData, orderMsg.TransactionId );

                    O2GSession o2Gsession = GetSession( );

                    if ( o2Gsession == null )
                        return false;

                    O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );

                    if ( requestFactory == null )
                    {
                        throw new InvalidOperationException( );
                    }

                    if ( request == null )
                    {
                        var error = requestFactory.getLastError( );

                        throw new InvalidOperationException( error );
                    }

                    _requestIdToTransactionId.Add( request.RequestID, orderMsg.TransactionId );
                    o2Gsession.sendRequest( request );
                }
            }

            return false;
        }

        private bool HedgeAllShortPositions( OrderRegisterMessage orderMsg )
        {
            string accountName = orderMsg.PortfolioName;

            if ( !string.IsNullOrEmpty( accountName ) )
            {
                var tradeDataRepo = GFMgr.GetSubaccountTradeDataRepoByName( accountName );

                if ( tradeDataRepo != null )
                {
                    var newOrdersData = tradeDataRepo.GetHedgeShortOrdersData( accountName );

                    if ( newOrdersData == null || newOrdersData.Values.Count == 0 )
                    {
                        this.AddWarningLog( "There are no opened positions" );

                        return false;
                    }

                    var request    = CreateHedgeAllShortPositionsRequest( newOrdersData, orderMsg.TransactionId );
                    var o2Gsession = GetSession( );

                    if ( o2Gsession == null )
                        return false;

                    var factory    = o2Gsession.getRequestFactory( );

                    if ( factory == null )
                    {
                        throw new InvalidOperationException( );
                    }

                    if ( request == null )
                    {
                        var error = factory.getLastError( );

                        throw new InvalidOperationException( error );
                    }

                    _requestIdToTransactionId.Add( request.RequestID, orderMsg.TransactionId );
                    o2Gsession.sendRequest( request );
                }
            }

            return false;
        }

        private bool HedgeAllPositions( OrderRegisterMessage orderMsg )
        {
            string accountName = orderMsg.PortfolioName;

            if ( !string.IsNullOrEmpty( accountName ) )
            {
                SubaccountTradeDataRepo tradeDataRepo = GFMgr.GetSubaccountTradeDataRepoByName( accountName );

                if ( tradeDataRepo != null )
                {

                    Dictionary<string, HedgeOrdersData> newOrdersData = tradeDataRepo.GetHedgeOrdersData( accountName );

                    if ( newOrdersData == null || newOrdersData.Values.Count == 0 )
                    {
                        this.AddWarningLog( "There are no opened positions" );

                        return false;
                    }

                    O2GRequest request = CreateHedgeAllPositionsRequest( newOrdersData, orderMsg.TransactionId );

                    O2GSession o2Gsession = GetSession( );

                    if ( o2Gsession == null )
                        return false;

                    O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );

                    if ( requestFactory == null )
                    {
                        throw new InvalidOperationException( );
                    }

                    if ( request == null )
                    {
                        var error = requestFactory.getLastError( );

                        throw new InvalidOperationException( error );
                    }

                    _requestIdToTransactionId.Add( request.RequestID, orderMsg.TransactionId );
                    o2Gsession.sendRequest( request );
                }
            }

            return false;
        }

        public O2GRequest CreateHedgeAllPositionsRequest( Dictionary<string, HedgeOrdersData> hedgeOrdersData, long transId )
        {
            O2GRequestFactory requestFactory = _fxSessionId.getRequestFactory();
            O2GRequest request = null;

            if ( requestFactory == null )
            {
                throw new Exception( "Cannot create request factory" );
            }

            O2GValueMap batchValuemap = requestFactory.createValueMap();
            batchValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );

            Dictionary<string, HedgeOrdersData>.Enumerator enumerator = hedgeOrdersData.GetEnumerator();

            while ( enumerator.MoveNext( ) )
            {
                string sOfferId = enumerator.Current.Key;
                string sAccountId = enumerator.Current.Value.AccountId;
                FxOrderSide side = enumerator.Current.Value.Side;
                int amount = enumerator.Current.Value.Amount;

                if ( amount > 0 )
                {
                    O2GValueMap childValuemap;

                    switch ( side )
                    {
                        case FxOrderSide.Buy:
                        {
                            var customId = "ID=" + transId.To< string >( ) + "," + "PE=" + ( ( int )OrderPositionEffects.HedgeShort ).To< string >( );

                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketOpen );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Buy );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );
                            batchValuemap.appendChild( childValuemap );
                        }
                            
                            break;

                        case FxOrderSide.Sell:
                        {
                            var customId = "ID=" + transId.To< string >( ) + "," + "PE=" + ( ( int )OrderPositionEffects.HedgeLong ).To< string >( );

                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketOpen );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Sell );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );
                            batchValuemap.appendChild( childValuemap );
                        }
                        break;
                    }
                }

            }

            request = requestFactory.createOrderRequest( batchValuemap );

            if ( request == null )
            {
                this.AddErrorLog( requestFactory.getLastError( ) );
            }

            return request;
        }

        public O2GRequest CreateHedgeAllLongPositionsRequest( Dictionary<string, HedgeOrdersData> hedgeOrdersData, long transId )
        {
            O2GRequestFactory requestFactory = _fxSessionId.getRequestFactory();
            O2GRequest request = null;

            if ( requestFactory == null )
            {
                throw new Exception( "Cannot create request factory" );
            }

            O2GValueMap batchValuemap = requestFactory.createValueMap();
            batchValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );

            Dictionary<string, HedgeOrdersData>.Enumerator enumerator = hedgeOrdersData.GetEnumerator();

            while ( enumerator.MoveNext( ) )
            {
                var sOfferId   = enumerator.Current.Key;
                var sAccountId = enumerator.Current.Value.AccountId;
                var side       = enumerator.Current.Value.Side;
                var amount     = enumerator.Current.Value.Amount;

                if ( amount > 0 )
                {
                    O2GValueMap childValuemap;

                    switch ( side )
                    {
                        case FxOrderSide.Buy:
                        {
                            throw new InvalidOperationException( );
                        }



                        case FxOrderSide.Sell:
                        {
                            var customId = "ID=" + transId.To< string >( ) + "," + "PE=" + ( ( int )OrderPositionEffects.HedgeLong ).To< string >( );

                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketOpen );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Sell );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );
                            batchValuemap.appendChild( childValuemap );
                        }
                        break;
                    }
                }

            }

            request = requestFactory.createOrderRequest( batchValuemap );

            if ( request == null )
            {
                this.AddErrorLog( requestFactory.getLastError( ) );
            }

            return request;
        }

        public O2GRequest CreateHedgeAllShortPositionsRequest( Dictionary<string, HedgeOrdersData> hedgeOrdersData, long transId )
        {
            O2GRequestFactory requestFactory = _fxSessionId.getRequestFactory();
            O2GRequest request = null;

            if ( requestFactory == null )
            {
                throw new Exception( "Cannot create request factory" );
            }

            O2GValueMap batchValuemap = requestFactory.createValueMap();
            batchValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );

            Dictionary<string, HedgeOrdersData>.Enumerator enumerator = hedgeOrdersData.GetEnumerator();

            while ( enumerator.MoveNext( ) )
            {
                string sOfferId = enumerator.Current.Key;
                string sAccountId = enumerator.Current.Value.AccountId;
                FxOrderSide side = enumerator.Current.Value.Side;
                int amount = enumerator.Current.Value.Amount;

                if ( amount > 0 )
                {
                    O2GValueMap childValuemap;

                    switch ( side )
                    {
                        case FxOrderSide.Buy:
                        {
                            var customId = "ID=" + transId.To< string >( ) + "," + "PE=" + ( ( int )OrderPositionEffects.HedgeShort ).To< string >( );

                            childValuemap = requestFactory.createValueMap( );
                            childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                            childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketOpen );
                            childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                            childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                            childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Buy );
                            childValuemap.setString( O2GRequestParamsEnum.CustomID, customId );
                            childValuemap.setInt( O2GRequestParamsEnum.Amount, amount );
                            batchValuemap.appendChild( childValuemap );
                        }
                            break;


                        case FxOrderSide.Sell:
                        {
                            throw new InvalidOperationException( );                            
                        }                        
                    }
                }

            }

            request = requestFactory.createOrderRequest( batchValuemap );

            if ( request == null )
            {
                this.AddErrorLog( requestFactory.getLastError( ) );
            }

            return request;
        }

        public O2GRequest CreateCloseAllPositionsRequest( Dictionary<string, CloseOrdersData> closeAllOrdersData, long transId )
        {
            O2GRequestFactory requestFactory = _fxSessionId.getRequestFactory();
            O2GRequest request = null;

            if ( requestFactory == null )
            {
                throw new Exception( "Cannot create request factory" );
            }

            O2GValueMap batchValuemap = requestFactory.createValueMap();
            batchValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );

            Dictionary<string, CloseOrdersData>.Enumerator enumerator = closeAllOrdersData.GetEnumerator();

            while ( enumerator.MoveNext( ) )
            {
                string sOfferId   = enumerator.Current.Key;
                string sAccountId = enumerator.Current.Value.AccountId;
                FxOrderSide side  = enumerator.Current.Value.Side;

                O2GValueMap childValuemap;

                switch ( side )
                {
                    case FxOrderSide.Buy:
                        childValuemap = requestFactory.createValueMap( );
                        childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                        childValuemap.setString( O2GRequestParamsEnum.NetQuantity, "Y" );
                        childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketClose );
                        childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                        childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                        childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Buy );
                        childValuemap.setString( O2GRequestParamsEnum.CustomID, "ID=" + transId.To<string>( ) );
                        batchValuemap.appendChild( childValuemap );
                        break;

                    case FxOrderSide.Sell:
                        childValuemap = requestFactory.createValueMap( );
                        childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                        childValuemap.setString( O2GRequestParamsEnum.NetQuantity, "Y" );
                        childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketClose );
                        childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                        childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                        childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Sell );
                        childValuemap.setString( O2GRequestParamsEnum.CustomID, "ID=" + transId.To<string>( ) );
                        batchValuemap.appendChild( childValuemap );
                        break;

                    case FxOrderSide.Both:
                        childValuemap = requestFactory.createValueMap( );
                        childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                        childValuemap.setString( O2GRequestParamsEnum.NetQuantity, "Y" );
                        childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketClose );
                        childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                        childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                        childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Buy );
                        childValuemap.setString( O2GRequestParamsEnum.CustomID, "ID=" + transId.To<string>( ) );
                        batchValuemap.appendChild( childValuemap );

                        childValuemap = requestFactory.createValueMap( );
                        childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                        childValuemap.setString( O2GRequestParamsEnum.NetQuantity, "Y" );
                        childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketClose );
                        childValuemap.setString( O2GRequestParamsEnum.AccountID, sAccountId );
                        childValuemap.setString( O2GRequestParamsEnum.OfferID, sOfferId );
                        childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Sell );
                        childValuemap.setString( O2GRequestParamsEnum.CustomID, "ID=" + transId.To<string>( ) );
                        batchValuemap.appendChild( childValuemap );
                        break;
                }
            }

            request = requestFactory.createOrderRequest( batchValuemap );

            if ( request == null )
            {
                this.AddErrorLog( requestFactory.getLastError( ) );
            }

            return request;
        }

        public O2GRequest CreateCloseSomePositionsRequest( List<CloseSomeOrdersData> someOrdersData, long transId )
        {
            O2GRequestFactory requestFactory = _fxSessionId.getRequestFactory();
            O2GRequest request = null;

            if ( requestFactory == null )
            {
                throw new Exception( "Cannot create request factory" );
            }

            O2GValueMap batchValuemap = requestFactory.createValueMap();
            batchValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );

            var enumerator = someOrdersData.GetEnumerator();

            foreach ( CloseSomeOrdersData order in someOrdersData )
            {
                string sInstrument = GFMgr.GetSymbolFromOfferId( order.OfferId );

                var o2Gsession = GetSession( );
                if ( o2Gsession == null )
                    return null;

                var permissionChecker = o2Gsession.getLoginRules().getPermissionChecker();

                if ( permissionChecker.canCreateMarketCloseOrder( sInstrument ) != O2GPermissionStatus.PermissionEnabled )
                {
                    throw new NotSupportedException( "Need capability to close Market order" );
                }

                O2GValueMap childValuemap;

                switch ( order.Side )
                {
                    case FxOrderSide.Buy:
                        childValuemap = requestFactory.createValueMap( );
                        childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                        childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketClose );
                        childValuemap.setString( O2GRequestParamsEnum.TradeID, order.TradeId );
                        childValuemap.setString( O2GRequestParamsEnum.AccountID, order.AccountId );
                        childValuemap.setString( O2GRequestParamsEnum.OfferID, order.OfferId );
                        childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Buy );
                        childValuemap.setInt( O2GRequestParamsEnum.Amount, order.Amount );
                        childValuemap.setString( O2GRequestParamsEnum.CustomID, "ID=" + transId.To<string>( ) );
                        batchValuemap.appendChild( childValuemap );
                        break;

                    case FxOrderSide.Sell:
                        childValuemap = requestFactory.createValueMap( );
                        childValuemap.setString( O2GRequestParamsEnum.Command, Constants.Commands.CreateOrder );
                        childValuemap.setString( O2GRequestParamsEnum.OrderType, Constants.Orders.TrueMarketClose );
                        childValuemap.setString( O2GRequestParamsEnum.TradeID, order.TradeId );
                        childValuemap.setString( O2GRequestParamsEnum.AccountID, order.AccountId );
                        childValuemap.setString( O2GRequestParamsEnum.OfferID, order.OfferId );
                        childValuemap.setString( O2GRequestParamsEnum.BuySell, Constants.Sell );
                        childValuemap.setInt( O2GRequestParamsEnum.Amount, order.Amount );
                        childValuemap.setString( O2GRequestParamsEnum.CustomID, "ID=" + transId.To<string>( ) );
                        batchValuemap.appendChild( childValuemap );
                        break;

                    default:
                        throw new NotSupportedException( "Orders cannot be other types" );

                }
            }

            request = requestFactory.createOrderRequest( batchValuemap );

            if ( request == null )
            {
                this.AddErrorLog( requestFactory.getLastError( ) );
            }

            return request;
        }

        private void ProcessOrderReplaceMessage( OrderReplaceMessage ocMsg )
        {
            O2GSession o2Gsession = GetSession( );

            if ( o2Gsession == null )
                return;

            O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );
            if ( requestFactory == null )
            {
                throw new InvalidOperationException( );
            }

            string orderId = string.Empty;

            if ( ocMsg.OldOrderId != null )
            {
                orderId = ocMsg.OldOrderId.To<string>( );
            }
            else if ( !string.IsNullOrEmpty( ocMsg.OldOrderStringId ) )
            {
                orderId = ocMsg.OldOrderStringId;
            }

            var accountId = GFMgr.GetAccountIdFromName( Login, ocMsg.PortfolioName );

            O2GValueMap valueMap = requestFactory.createValueMap( );
            valueMap.setString( O2GRequestParamsEnum.Command, "EditOrder" );
            valueMap.setString( O2GRequestParamsEnum.OrderID, orderId );
            valueMap.setString( O2GRequestParamsEnum.AccountID, accountId );
            valueMap.setDouble( O2GRequestParamsEnum.Rate, ( double ) ocMsg.Price );

            int offerId = Int32.Parse( ( string ) ocMsg.SecurityId.Native );

            var subscribedSymbol = GFMgr.GetSubscribedSymbolsByAccountNameAndId( Login, offerId );

            int baseUnit = 1;

            if ( subscribedSymbol != null )
            {
                baseUnit = subscribedSymbol.BaseUnitSize;
            }

            valueMap.setInt( O2GRequestParamsEnum.Amount, ( int ) ocMsg.Volume * baseUnit );
            valueMap.setString( O2GRequestParamsEnum.CustomID, "ID=" + ocMsg.TransactionId.To<string>( ) );

            O2GRequest orderRequest = requestFactory.createOrderRequest( valueMap );
            if ( orderRequest == null )
            {
                throw new InvalidOperationException( requestFactory.getLastError( ) );
            }


            _requestIdToTransactionId.Add( orderRequest.RequestID, ocMsg.TransactionId );
            o2Gsession.sendRequest( orderRequest );
        }

        private void ProcessOrderCancelMessage( OrderCancelMessage ocMsg )
        {
            O2GSession o2Gsession = GetSession( );

            if ( o2Gsession == null )
                return;


            O2GRequestFactory requestFactory = o2Gsession.getRequestFactory( );
            if ( requestFactory == null )
            {
                throw new InvalidOperationException( );
            }

            string orderId = string.Empty;

            if ( ocMsg.OrderId != null )
            {
                orderId = ocMsg.OrderId.To<string>( );
            }
            else if ( !string.IsNullOrEmpty( ocMsg.OrderStringId ) )
            {
                orderId = ocMsg.OrderStringId;
            }

            var accountId = GFMgr.GetAccountIdFromName( Login, ocMsg.PortfolioName );

            O2GValueMap valueMap = requestFactory.createValueMap( );
            valueMap.setString( O2GRequestParamsEnum.Command, "DeleteOrder" );
            valueMap.setString( O2GRequestParamsEnum.OrderID, orderId );
            valueMap.setString( O2GRequestParamsEnum.AccountID, accountId );
            valueMap.setString( O2GRequestParamsEnum.CustomID, "ID=" + ocMsg.TransactionId.To<string>( ) );

            O2GRequest orderRequest = requestFactory.createOrderRequest( valueMap );

            if ( orderRequest == null )
            {
                throw new InvalidOperationException( requestFactory.getLastError( ) );
            }

            _requestIdToTransactionId.Add( orderRequest.RequestID, ocMsg.TransactionId );
            o2Gsession.sendRequest( orderRequest );

            //if (cancelMsg.OrderId == null)
            //	throw new InvalidOperationException(LocalizedStrings.Str2252Params.Put(cancelMsg.OriginalTransactionId));

            //_httpClient.CancelOrder(cancelMsg.OrderId.Value);

            ////SendOutMessage(new ExecutionMessage
            ////{
            ////	ServerTime = CurrentTime.ConvertToUtc(),
            ////	ExecutionType = ExecutionTypes.Transaction,
            ////	OriginalTransactionId = cancelMsg.TransactionId,
            ////	OrderState = OrderStates.Done,
            ////	HasOrderInfo = true,
            ////});

            //ProcessOrderStatus(null);
            //ProcessPortfolioLookup(null);
        }

        private void ProcessProtfolioMessage( PortfolioMessage pfMsg )
        {
            if ( !pfMsg.IsSubscribe )
            {
                return;
            }

            SetupTableResponseHandler( O2GTableType.Orders, pfMsg, pfMsg.PortfolioName, new Action<ISubscriptionMessage, O2GResponse>( OnOrdersTableLoaded ) );
        }
















        //private void ProcessOrderGroupCancel(OrderGroupCancelMessage cancelMsg)
        //{
        //	_httpClient.CancelAllOrders();
        //
        //	SendOutMessage(new ExecutionMessage
        //	{
        //		ServerTime = CurrentTime.ConvertToUtc(),
        //		ExecutionType = ExecutionTypes.Transaction,
        //		OriginalTransactionId = cancelMsg.TransactionId,
        //		HasOrderInfo = true,
        //	});

        //	ProcessOrderStatus(null);
        //	ProcessPortfolioLookup(null);
        //}

        //private void ProcessOrder(UserOrder order, decimal balance, long transId, long origTransId)
        //{
        //	SendOutMessage(new ExecutionMessage
        //	{
        //		ExecutionType         = ExecutionTypes.Transaction,
        //		OrderId               = order.Id,
        //		TransactionId         = transId,
        //		OriginalTransactionId = origTransId,
        //		OrderPrice            = (decimal)order.Price,
        //		Balance               = balance,
        //		OrderVolume           = (decimal)order.Amount,
        //		Side                  = order.Type.ToSide(),
        //		SecurityId            = order.CurrencyPair.ToStockSharp(),
        //		ServerTime            = transId != 0 ? order.Time : CurrentTime.ConvertToUtc(),
        //		PortfolioName         = PortfolioName,
        //		OrderState            = OrderStates.Active,
        //		HasOrderInfo          = true,
        //	});
        //}

        //private void ProcessTrade(UserTransaction transaction)
        //{
        //	// not trade
        //	if (transaction.Type != 2)
        //		throw new InvalidOperationException("type = {0}".Put(transaction.Type));

        //	var info = _orderInfo.TryGetValue(transaction.OrderId);

        //	if (info == null/* || info.Second <= 0*/)
        //		return;

        //	string pair;
        //	decimal price;
        //	decimal volume;

        //	if (transaction.BtcUsd != null)
        //	{
        //		pair = "btcusd";
        //		price = (decimal)transaction.BtcUsd.Value;
        //		volume = (decimal)transaction.BtcAmount.Value;
        //	}
        //	else if (transaction.BtcEur != null)
        //	{
        //		pair = "btceur";
        //		price = (decimal)transaction.BtcEur.Value;
        //		volume = (decimal)transaction.BtcAmount.Value;
        //	}
        //	else if (transaction.BchBtc != null)
        //	{
        //		pair = "bchbtc";
        //		price = (decimal)transaction.BchBtc.Value;
        //		volume = (decimal)transaction.BchAmount.Value;
        //	}
        //	else if (transaction.BchUsd != null)
        //	{
        //		pair = "bchusd";
        //		price = (decimal)transaction.BchUsd.Value;
        //		volume = (decimal)transaction.BchAmount.Value;
        //	}
        //	else if (transaction.BchEur != null)
        //	{
        //		pair = "btceur";
        //		price = (decimal)transaction.BchEur.Value;
        //		volume = (decimal)transaction.BchAmount.Value;
        //	}
        //	else if (transaction.EthBtc != null)
        //	{
        //		pair = "ethbtc";
        //		price = (decimal)transaction.EthBtc.Value;
        //		volume = (decimal)transaction.EthAmount.Value;
        //	}
        //	else if (transaction.EthUsd != null)
        //	{
        //		pair = "ethusd";
        //		price = (decimal)transaction.EthUsd.Value;
        //		volume = (decimal)transaction.EthAmount.Value;
        //	}
        //	else if (transaction.EthEur != null)
        //	{
        //		pair = "etheur";
        //		price = (decimal)transaction.EthEur.Value;
        //		volume = (decimal)transaction.EthAmount.Value;
        //	}
        //	else if (transaction.LtcBtc != null)
        //	{
        //		pair = "ltcbtc";
        //		price = (decimal)transaction.LtcBtc.Value;
        //		volume = (decimal)transaction.LtcAmount.Value;
        //	}
        //	else if (transaction.LtcUsd != null)
        //	{
        //		pair = "ltcusd";
        //		price = (decimal)transaction.LtcUsd.Value;
        //		volume = (decimal)transaction.LtcAmount.Value;
        //	}
        //	else if (transaction.LtcEur != null)
        //	{
        //		pair = "ltceur";
        //		price = (decimal)transaction.LtcEur.Value;
        //		volume = (decimal)transaction.LtcAmount.Value;
        //	}
        //	else if (transaction.XrpBtc != null)
        //	{
        //		pair = "xrpbtc";
        //		price = (decimal)transaction.XrpBtc.Value;
        //		volume = (decimal)transaction.XrpAmount.Value;
        //	}
        //	else if (transaction.XrpUsd != null)
        //	{
        //		pair = "xrpusd";
        //		price = (decimal)transaction.XrpUsd.Value;
        //		volume = (decimal)transaction.XrpAmount.Value;
        //	}
        //	else if (transaction.XrpEur != null)
        //	{
        //		pair = "xrpeur";
        //		price = (decimal)transaction.XrpEur.Value;
        //		volume = (decimal)transaction.XrpAmount.Value;
        //	}
        //	else
        //		throw new InvalidOperationException("Unknown pair.");

        //	volume = volume.Abs();

        //	SendOutMessage(new ExecutionMessage
        //	{
        //		ExecutionType = ExecutionTypes.Transaction,
        //		OrderId = transaction.OrderId,
        //		TradeId = transaction.Id,
        //		TradePrice = price,
        //		TradeVolume = volume,
        //		SecurityId = pair.ToStockSharp(),
        //		ServerTime = transaction.Time.ToDto(),
        //		PortfolioName = PortfolioName,
        //		HasTradeInfo = true,
        //		Commission = (decimal)transaction.Fee,
        //		OriginalTransactionId = info.First,
        //	});

        //	info.Second -= volume;

        //	if (info.Second < 0)
        //		throw new InvalidOperationException(LocalizedStrings.Str3301Params.Put(transaction.OrderId, info.Second));

        //	SendOutMessage(new ExecutionMessage
        //	{
        //		ExecutionType = ExecutionTypes.Transaction,
        //		OrderId = transaction.OrderId,
        //		Balance = info.Second,
        //		OrderState = info.Second > 0 ? OrderStates.Active : OrderStates.Done,
        //		HasOrderInfo = true,
        //		SecurityId = pair.ToStockSharp(),
        //		ServerTime = transaction.Time.ToDto(),
        //		PortfolioName = PortfolioName,
        //		OriginalTransactionId = info.First,
        //	});

        //	if (info.Second == 0)
        //		_orderInfo.Remove(transaction.OrderId);
        //}

        //private void ProcessOrderStatus(OrderStatusMessage message)
        //{
        //	if (message == null)
        //	{
        //		var portfolioRefresh = false;

        //		var orders = _httpClient.RequestOpenOrders();

        //		var ids = _orderInfo.Keys.ToHashSet();

        //		foreach (var order in orders)
        //		{
        //			ids.Remove(order.Id);

        //			var info = _orderInfo.TryGetValue(order.Id);

        //			if (info == null)
        //			{
        //				info = RefTuple.Create(TransactionIdGenerator.GetNextId(), (decimal)order.Amount);

        //				_orderInfo.Add(order.Id, info);

        //				ProcessOrder(order, (decimal)order.Amount, info.First, 0);

        //				portfolioRefresh = true;
        //			}
        //			else
        //			{
        //				// balance existing orders tracked by trades
        //			}
        //		}

        //		var trades = GetTrades();

        //		foreach (var trade in trades)
        //		{
        //			ProcessTrade(trade);
        //		}

        //		foreach (var id in ids)
        //		{
        //			// can be removed from ProcessTrade
        //			if (!_orderInfo.TryGetAndRemove(id, out var info))
        //				return;

        //			SendOutMessage(new ExecutionMessage
        //			{
        //				ExecutionType = ExecutionTypes.Transaction,
        //				HasOrderInfo = true,
        //				OrderId = id,
        //				OriginalTransactionId = info.First,
        //				ServerTime = CurrentTime.ConvertToUtc(),
        //				OrderState = OrderStates.Done,
        //			});

        //			portfolioRefresh = true;
        //		}

        //		if (portfolioRefresh)
        //			ProcessPortfolioLookup(null);
        //	}
        //	else
        //	{
        //		if (!message.IsSubscribe)
        //			return;

        //		var orders = _httpClient.RequestOpenOrders().ToArray();

        //		foreach (var order in orders)
        //		{
        //			var info = RefTuple.Create(TransactionIdGenerator.GetNextId(), (decimal)order.Amount);

        //			_orderInfo.Add(order.Id, info);

        //			ProcessOrder(order, (decimal)order.Amount, info.First, message.TransactionId);
        //		}

        //		var trades = GetTrades();

        //		foreach (var trade in trades)
        //		{
        //			ProcessTrade(trade);
        //		}
        //	
        //		SendSubscriptionResult(message);
        //	}
        //}

        //private IEnumerable<UserTransaction> GetTrades()
        //{
        //	const int pageSize = 100;
        //	const int maxOffset = 10000;

        //	var trades = new List<UserTransaction>();

        //	var offset = 0;

        //	while (true)
        //	{
        //		var batch = _httpClient.RequestUserTransactions(null, offset, offset + pageSize);

        //		var hasLess = false;
        //		trades.AddRange(batch.Where(t => t.Type == 2).Where(t =>
        //		{
        //			if (t.Id > _lastMyTradeId)
        //				return true;

        //			hasLess = true;
        //			return false;
        //		}).ToArray());

        //		if (hasLess || batch.Length < pageSize)
        //			break;

        //		offset += pageSize;

        //		if (offset >= maxOffset)
        //			break;
        //	}

        //	if (trades.Count > 0)
        //	{
        //		trades = trades.DistinctBy(t => t.Id).OrderBy(t => t.Id).ToList();
        //		_lastMyTradeId = trades.Last().Id;
        //	}

        //	return trades;
        //}

        //private void ProcessPortfolioLookup(PortfolioLookupMessage message)
        //{
        //	if (message != null)
        //	{
        //		if (!message.IsSubscribe)
        //			return;
        //	}

        //	var transactionId = message?.TransactionId ?? 0;

        //	var pfName = PortfolioName;

        //	SendOutMessage(new PortfolioMessage
        //	{
        //		PortfolioName = pfName,
        //		BoardCode = Extensions.BitStampBoard,
        //		OriginalTransactionId = transactionId,
        //	});

        //	if (message != null)
        //		SendSubscriptionResult(message);

        //	var tuple = _httpClient.GetBalances();

        //	foreach (var pair in tuple.Item1)
        //	{
        //		var currValue = pair.Value.First;
        //		var currPrice = pair.Value.Second;
        //		var blockValue = pair.Value.Third;

        //		if (currValue == null && currPrice == null && blockValue == null)
        //			continue;

        //		var msg = this.CreatePositionChangeMessage(pfName, pair.Key.ToUpperInvariant().ToStockSharp(false));

        //		msg.TryAdd(PositionChangeTypes.CurrentValue, currValue, true);
        //		msg.TryAdd(PositionChangeTypes.CurrentPrice, currPrice, true);
        //		msg.TryAdd(PositionChangeTypes.BlockedValue, blockValue, true);

        //		SendOutMessage(msg);	
        //	}

        //	foreach (var pair in tuple.Item2)
        //	{
        //		SendOutMessage(new Level1ChangeMessage
        //		{
        //			SecurityId = pair.Key.ToStockSharp(),
        //			ServerTime = CurrentTime.ConvertToUtc()
        //		}.TryAdd(Level1Fields.CommissionTaker, pair.Value));
        //	}

        //	_lastTimeBalanceCheck = CurrentTime;
        //}
    }
}
