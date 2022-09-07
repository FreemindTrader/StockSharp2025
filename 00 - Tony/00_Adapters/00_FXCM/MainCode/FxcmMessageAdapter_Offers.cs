namespace StockSharp.FxConnectFXCM
{
	using System;
    using System.Collections.Generic; 
    using System.Linq;
    using System.Threading.Tasks;
    using Ecng.Common;
    using Ecng.ComponentModel;
    using fx.Messages;
    using fxcore2;
    using StockSharp.Algo;
    using StockSharp.BusinessEntities;
    using StockSharp.FxConnectFXCM.Freemind;    
    using StockSharp.Localization;
    using StockSharp.Logging;
    using StockSharp.Messages;
    using fx.Base;


    partial class FxConnectFxcmMsgAdapter
	{
        private readonly FxOffersCollection _offersCollection      = new FxOffersCollection( ); // The collection of the offers available for the trading session
        private Dictionary< string, long > _levelOneSubscription = new Dictionary< string, long >( );

        private DateTime _lastOfferUpdateTime = DateTime.MinValue;

        private void OnOfferTableLoaded( ISubscriptionMessage msg, O2GResponse response )
        {
            var o2Gsession = GetSession( );
            if ( o2Gsession == null )
                return;

            var factory        = o2Gsession.getResponseReaderFactory( );            
            var loginRules     = o2Gsession.getLoginRules( );
            var tradingSetting = loginRules.getTradingSettingsProvider();

            if ( factory == null )
            {
                throw new InvalidOperationException( );
            }

            if ( response.Type == O2GResponseType.GetOffers )
            {
                var reader = factory.createOffersTableReader( response );

                _offersCollection.Clear( );

                var symbolsOffer = GFMgr.GetSymbolsRepo( );

                var subscribedSymbol = GFMgr.GetSubscribedSymbolsByAccountName( Login );

                GFMgr.CreateOfferCache( reader.Count );

                this.AddInfoLog( "Initial Symbol Table loaded, Total Symbols = {0}", reader.Count );

                for ( int index = 0; index < reader.Count; ++index )
                {
                    var row = reader.getRow( index );

                    var instrumentOffers = new DbSymbolsInfo( row.OfferID, row.Instrument, row.BuyInterest, row.SellInterest, row.Digits, row.PointSize, row.SubscriptionStatus, row.TradingStatus, row.InstrumentType.To<int>(), row.PointSize * 50 );

                    GFMgr.UpdateOfferCache( int.Parse( row.OfferID ), row.Bid, row.Ask );

                    _lastOfferUpdateTime = DateTime.UtcNow;

                    SecurityId secId   = row.OfferID.ToSecurityId( );
                    secId.SecurityCode = row.Instrument;

                    bool isSubscribed = row.SubscriptionStatus == "T";

                    if ( isSubscribed && row.isInstrumentValid && row.isLowValid && row.isHighValid && row.isAskValid && row.isBidValid && row.isPointSizeValid )
                    {
                        this.AddInfoLog( "Add Symbol {0}, Subscribed = {1}", row.Instrument, isSubscribed );
                        subscribedSymbol.Add( new DBSubscribedSymbols( row.OfferID, Login ) );                        
                    }

                    this.AddDebugLog( "Add Symbol {0}, Subscribed = {1}", row.Instrument, isSubscribed );

                    symbolsOffer.Add( instrumentOffers );

                    if ( !StringHelper.IsEmpty( secId.SecurityCode ) )
                    {
                        secId.BoardCode              = ExchangeBoard.Fxcm.Code;
                        var secMsg                   = new SecurityMessage( );
                        secMsg.SecurityId            = secId;
                        secMsg.OriginalTransactionId = msg.TransactionId;

                        if ( row.isInstrumentTypeValid )
                        {
                            secMsg.SecurityType = row.InstrumentType.ToSecurityType( );
                        }

                        if ( row.isContractMultiplierValid )
                        {
                            secMsg.Multiplier = ( new decimal?( ( decimal ) row.ContractMultiplier ) );
                        }

                        if ( row.isContractCurrencyValid )
                        {
                            secMsg.Currency = ( new CurrencyTypes?( row.ContractCurrency.To<CurrencyTypes>( ) ) );
                        }

                        if ( row.isDigitsValid )
                        {
                            secMsg.Decimals = ( new int?( row.Digits ) );
                        }

                        if ( row.isPointSizeValid )
                        {
                            secMsg.PriceStep = ( new decimal?( ( decimal ) row.PointSize ) );
                        }

                        SendOutMessage( secMsg );
                    }
                }                

                if ( _currentState == StateMachine.WAIT_FOR_LOOKUP_DONE )
                {
                    ProcessPortfolioLookupMessage( _lookupMsg );
                }
                else
                {
                    _currentState = StateMachine.SECURITY_LOOKUP_DONE;
                }
                
                SendSubscriptionResult( msg );
            }
        }

        private void OnOfferTableUpdate( O2GOfferRow row, O2GTableUpdateType updateType, long transactionId )
        {
            string instrument  = null;

            if ( row.isInstrumentValid )
            {
                instrument = row.Instrument;
            }
            //else
            //{
            //    if ( row.isOfferIDValid )
            //    {
            //        instrument = row.OfferID;
            //    }
            //}

            if ( instrument == null )
            {
                return;
            }

            _lastOfferUpdateTime = DateTime.UtcNow;

            GFMgr.UpdateOfferCache( int.Parse( row.OfferID ), row.Bid, row.Ask );

            if ( _levelOneSubscription.ContainsKey( row.OfferID ) )
            {
                SecurityId secId   = row.OfferID.ToSecurityId( );
                secId.SecurityCode = row.Instrument;

                if ( !StringHelper.IsEmpty( secId.SecurityCode ) )
                {

                    var offerTime              = row.Time;
                    var utcOffer               = DateTime.SpecifyKind( offerTime, DateTimeKind.Utc );

                    var lvl1Msg                = new Level1ChangeMessage( );
                    lvl1Msg.SecurityId         = secId;
                    lvl1Msg.SubscriptionId     = _levelOneSubscription[ row.OfferID ];
                    lvl1Msg.LocalTime          = utcOffer;
                    lvl1Msg.ServerTime         = utcOffer;
                    lvl1Msg.IsReloadFromServer = false;

                    if ( row.isContractMultiplierValid )
                    {
                        lvl1Msg.Add( Level1Fields.Multiplier, ( decimal ) row.ContractMultiplier );
                    }

                    if ( row.isHighValid )
                    {
                        lvl1Msg.Add( Level1Fields.HighBidPrice, ( decimal ) row.High );
                    }

                    if ( row.isLowValid )
                    {
                        lvl1Msg.Add( Level1Fields.LowAskPrice, ( decimal ) row.Low );
                    }

                    if ( row.isBidValid )
                    {
                        lvl1Msg.Add( Level1Fields.BestBidPrice, ( decimal ) row.Bid );
                    }

                    if ( row.isAskValid )
                    {
                        lvl1Msg.Add( Level1Fields.BestAskPrice, ( decimal ) row.Ask );
                    }

                    if ( row.isVolumeValid )
                    {
                        lvl1Msg.Add( Level1Fields.Volume, ( decimal ) row.Volume );
                    }

                    if ( row.isTradingStatusValid )
                    {
                        lvl1Msg.Add( Level1Fields.State, ( SecurityStates ) row.TradingStatus.ToSecurityStates( ) );
                    }

                    if ( row.isPointSizeValid )
                    {
                        lvl1Msg.Add( Level1Fields.PriceStep, ( decimal ) row.PointSize );
                    }

                    decimal openInterest = new decimal( );
                    if ( row.isBuyInterestValid )
                    {
                        openInterest = ( decimal ) row.BuyInterest;
                    }

                    if ( row.isSellInterestValid )
                    {
                        openInterest += ( decimal ) row.SellInterest;
                    }

                    if ( openInterest != decimal.Zero )
                    {
                        lvl1Msg.Add( Level1Fields.OpenInterest, openInterest );
                    }

                    lvl1Msg.Add( Level1Fields.MedianPrice, ( decimal ) ( ( row.Bid + row.Ask ) / 2 ) );

                    SendOutMessage( lvl1Msg );
                }
            }

            

            
        }

        private void ProcessMarketDataMessage( MarketDataMessage mdMsg )
        {
            if ( !TraderHelper.IsAssociated( mdMsg.SecurityId, ExchangeBoard.Fxcm ) )
            {                
                SendSubscriptionNotSupported( mdMsg.TransactionId );
            }
            else
            {                
                MarketDataTypes dataType = mdMsg.DataType;                

                if ( dataType == MarketDataTypes.Level1 )
                {                    
                    if ( mdMsg.IsSubscribe )
                    {
                        var symbol = ( string ) mdMsg.SecurityId.Native;        
                        
                        if ( ! _levelOneSubscription.ContainsKey( symbol ) )
                        {
                            _levelOneSubscription.Add( symbol, mdMsg.TransactionId );
                        }

                        
                        
                    }
                    else
                    {
                        var symbol = ( string ) mdMsg.SecurityId.Native;
                        _levelOneSubscription.Remove( symbol );
                    }
                }
                else if ( dataType == MarketDataTypes.News )
                {
                    if ( mdMsg.IsSubscribe )
                    {
                        SetupTableResponseHandler( O2GTableType.Messages, mdMsg, null, new Action<ISubscriptionMessage, O2GResponse>( OnReceiveNews ) );
                    }
                }
                else if ( dataType == MarketDataTypes.CandleTimeFrame )
                {
                    var symbol = mdMsg.SecurityId.SecurityCode;
                    var period = ( TimeSpan ) mdMsg.DataType2.Arg;

                    if ( mdMsg.IsSubscribe )
                    {
                        //bool fullDownload = false;

                        if ( mdMsg.ExtensionInfo != null )
                        {
                            var info = mdMsg.ExtensionInfo;

                            if ( info.ContainsKey( "FullDownload" )  )
                            {
                                if ( !_candlesLiveDownload.ContainsKey( (symbol, period) ) )
                                {
                                    FullDownloadHistoricBars( mdMsg );
                                }

                                return;
                            }

                            if ( info.ContainsKey( "ReloadCandles" ) || info.ContainsKey( "DownloadBackward" ) )
                            {
                                ReloadHistoricBars( mdMsg );
                                return;
                            }
                        }
                        else
                        {
                            if ( !_candlesLiveDownload.ContainsKey( (symbol, period) ) )
                            {
                                if ( period >= TimeSpan.FromDays( 1 ) )
                                {
                                    DownloadHistoricBars( mdMsg );
                                }
                                else if ( period >= TimeSpan.FromHours( 1 ) )
                                {
                                    DownloadIntradayHoulyHistoricBars( mdMsg );
                                }
                                else
                                {
                                    DownloadIntradayHistoricBars( mdMsg );
                                }
                            }
                        }
                    }
                    else
                    {                                               
                        if ( _candlesLiveDownload.ContainsKey( (symbol, period) ) )
                        {
                            _candlesLiveDownload.Remove( (symbol, period) );
                        }
                    }
                }
                else if ( dataType == MarketDataTypes.Trades )
                {
                    if ( mdMsg.IsSubscribe )
                    {
                        Task first = new Task( ( ) => DownloadHistoricTicks( mdMsg ) );
                        first.Start( );
                    }
                }
                else
                {
                    SendSubscriptionNotSupported( mdMsg.TransactionId );
                    return;
                }

                SendSubscriptionReply( mdMsg.TransactionId, null );                
            }
        }

        private void OnReceiveNews( ISubscriptionMessage msg, O2GResponse response )
        {
            var o2Gsession = GetSession( );
            if ( o2Gsession == null )
                return;

            O2GResponseReaderFactory responseReaderFactory = o2Gsession.getResponseReaderFactory( );
            if ( responseReaderFactory == null )
            {
                throw new InvalidOperationException( );
            }

            O2GMessagesTableResponseReader messagesTableReader = responseReaderFactory.createMessagesTableReader( response );
            for ( int index = 0; index < messagesTableReader.Count; ++index )
            {
                OnMessagesTableUdate( messagesTableReader.getRow( index ), msg );
            }
        }

        private void OnMessagesTableUdate( O2GMessageRow msgRow, ISubscriptionMessage msg )
        {
            NewsMessage newsMessage = new NewsMessage( );
            newsMessage.Headline              = msgRow.Subject;
            newsMessage.Story                 = msgRow.Text;
            newsMessage.ServerTime            = msgRow.Time;
            newsMessage.Source                = msgRow.From;
            newsMessage.Id                    = msgRow.MsgID;
            newsMessage.OriginalTransactionId = msg != null ? msg.TransactionId : 0;

            SendOutMessage( newsMessage );
        }

        public void ReloadHistoricBars( MarketDataMessage mdMsg )
        {
            var o2Gsession          = GetSession( );

            if ( o2Gsession == null )
                return;


            var factory             = o2Gsession.getRequestFactory( );

            if ( factory == null )
            {
                throw new InvalidOperationException();
            }

            bool candlesNeedLiveUpdate = false;

            var sameDate            = DateTime.MinValue;
            var securityId          = mdMsg.SecurityId;
            var symbol              = securityId.SecurityCode;
            var period              = ( TimeSpan ) mdMsg.DataType2.Arg;
            var o2Gtimeframe        = period.ToFxcmTimeFrame( factory.Timeframes );
            var barCount            = ( int )( mdMsg.Count ?? 300L );
            var transactionId       = mdMsg.TransactionId;
            var lowerBound          = mdMsg.From.HasValue ? mdMsg.From.GetValueOrDefault( ).UtcDateTime : factory.ZERODATE;

            DateTime upperBound = DateTime.MinValue;

            if ( mdMsg.To.HasValue )
            {
                upperBound = mdMsg.To.GetValueOrDefault().UtcDateTime;
            }
            else
            {
                upperBound = DateTime.UtcNow;
            }

            candlesNeedLiveUpdate = mdMsg.IsSubscribe;

            var latestBarTime       = lowerBound;
            var request             = factory.createMarketDataSnapshotRequestInstrument( symbol, o2Gtimeframe, barCount );
            var dtFirst             = upperBound;
            var backwardFirst       = DateTime.MinValue;

            string prefixString = "";

            prefixString = "<<<<<<";

            DateTime barDate = DateTime.MinValue;

            int noMoreData = 0;

            try
            {
                List<TimeFrameCandleMessage> buffer = new List<TimeFrameCandleMessage>( );

                // cause there is limit for returned candles amount
                do
                {
                    // We are no longer in the Started State
                    if ( _canProcess != null && !_canProcess( false ) )
                    {
                        break;
                    }

                    backwardFirst = dtFirst;

                    if ( lowerBound >= dtFirst )
                    {
                        break;
                    }


                    factory.fillMarketDataSnapshotRequestTime( request, lowerBound, dtFirst, true, O2GCandleOpenPriceMode.PreviousClose );

                    if ( _pricelistener != null )
                    {
                        _pricelistener.SetRequestID( request.RequestID, period, symbol );
                    }

                    o2Gsession.sendRequest( request );


                    if ( _pricelistener != null )
                    {
                        if ( period == TimeSpan.FromTicks( 1 ) )
                        {
                            _pricelistener.Wait3Mins( request.RequestID );
                        }
                        else
                        {
                            _pricelistener.Wait( request.RequestID );
                        }
                    }


                    O2GMarketDataSnapshotResponseReader reader = null;


                    // shift "to" bound to oldest datetime of returned data
                    O2GResponse response = null;

                    if ( _pricelistener != null )
                    {
                        response = _pricelistener.GetResponse( request.RequestID );
                    }

                    if ( response != null && response.Type == O2GResponseType.MarketDataSnapshot )
                    {
                        O2GResponseReaderFactory readerFactory = o2Gsession.getResponseReaderFactory( );

                        if ( readerFactory != null )
                        {
                            reader = readerFactory.createMarketDataSnapshotReader( response );

                            if ( reader.Count > 0 )
                            {
                                // Check if the first returned bar is the same as the upper bound
                                if ( DateTime.Compare( dtFirst, reader.getDate( 0 ) ) != 0 )
                                {
                                    dtFirst = reader.getDate( 0 ); // earliest datetime of returned data

                                    if ( sameDate == DateTime.MinValue )
                                    {
                                        sameDate = reader.getDate( reader.Count - 1 );
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                LoggingHelper.AddWarningLog( this, "0 rows received" );
                                break;
                            }
                        }


                        for ( int index = reader.Count - 1; index >= 0; index-- )
                        {
                            if ( reader.isBar )
                            {
                                var msg                   = new TimeFrameCandleMessage( );

                                msg.Arg                   = period;
                                msg.SecurityId            = mdMsg.SecurityId;
                                msg.OpenPrice             = reader.getBidOpen( index ).ToDecimal() ?? decimal.Zero;
                                msg.HighPrice             = reader.getBidHigh( index ).ToDecimal() ?? decimal.Zero;
                                msg.LowPrice              = reader.getBidLow( index ).ToDecimal() ?? decimal.Zero;
                                msg.ClosePrice            = reader.getBidClose( index ).ToDecimal() ?? decimal.Zero;

                                barDate                   = reader.getDate( index );
                                var utcTime               = DateTime.SpecifyKind( barDate, DateTimeKind.Utc );

                                msg.OpenTime              = utcTime;
                                msg.TotalVolume           = reader.getVolume( index );
                                msg.OriginalTransactionId = transactionId;
                                msg.SubscriptionId        = transactionId;
                                msg.State                 = ( upperBound >= barDate + period ) ? CandleStates.Finished : CandleStates.Active;
                                msg.BatchStatus           = fxBatchStatus.Batching;

                                if ( barDate > latestBarTime )
                                {
                                    latestBarTime = barDate;
                                }

                                buffer.Add( msg );


                            }
                        }

                        string message = string.Format( "{5}[{0}] ({1}) : Count = {4} : Buffering Databars Range [ {2:g} - {3:g} ] ..... ", symbol, period.ToReadable(), dtFirst, backwardFirst, reader.Count, prefixString );

                        LoggingHelper.AddInfoLog( this, message );
                    }
                    else
                    {
                        noMoreData++;

                        var diff = dtFirst - lowerBound;
                        string message = string.Format( "{4}[{0}] Period : ({1}) !! No more data [ {2:g} - {3:g} ]", symbol, period.ToReadable(), lowerBound, dtFirst, prefixString );
                        ProcessErrors( request, symbol, period, diff, message );

                        dtFirst = dtFirst - period;

                        if ( noMoreData > 20 )
                        {
                            break;
                        }
                    }
                }
                while ( dtFirst > lowerBound );

                if ( buffer.Count > 1 )
                {
                    var acending      = buffer.OrderBy( x => x.OpenTime ).ToList( );

                    if ( period < TimeSpan.FromHours( 1 ) )
                    {
                        acending.RemoveAt( acending.Count - 1 );
                    }

                    var begin         = acending.First( );
                    begin.BatchStatus = fxBatchStatus.BeginBatch;
                    var end           = acending.Last( );
                    end.BatchStatus = fxBatchStatus.EndBatch;

                    foreach ( var candleMsg in acending )
                    {
                        SendOutMessage( candleMsg );
                    }

                    string sendMsg = string.Format( "**** [{0}] ({1}) : Send Batch Download Msg [ {2:g} - {3:g} ] ...Total= {4} **** ", symbol, period.ToReadable(), begin.OpenTime, end.OpenTime, buffer.Count );

                    LoggingHelper.AddInfoLog( this, sendMsg );

                    buffer.Clear();
                }
                else if ( buffer.Count == 1 )
                {
                    foreach ( var candleMsg in buffer )
                    {
                        candleMsg.BatchStatus = fxBatchStatus.FromStorage;
                        SendOutMessage( candleMsg );
                    }

                    buffer.Clear();
                }
            }
            catch ( Exception e )
            {
                LoggingHelper.AddErrorLog( this, e );
            }
        }

        public void FullDownloadHistoricBars( MarketDataMessage mdMsg )
        {
            var o2Gsession          = GetSession( );

            if ( o2Gsession == null )
                return;


            var factory             = o2Gsession.getRequestFactory( );

            if ( factory == null )
            {
                throw new InvalidOperationException();
            }

            bool candlesNeedLiveUpdate = false;

            var sameDate            = DateTime.MinValue;
            var securityId          = mdMsg.SecurityId;
            var symbol              = securityId.SecurityCode;
            var period              = ( TimeSpan ) mdMsg.DataType2.Arg;
            var o2Gtimeframe        = period.ToFxcmTimeFrame( factory.Timeframes );
            var barCount            = ( int )( mdMsg.Count ?? 300L );
            var transactionId       = mdMsg.TransactionId;
            var lowerBound          = mdMsg.From.HasValue ? mdMsg.From.GetValueOrDefault( ).UtcDateTime : factory.ZERODATE;

            DateTime upperBound = DateTime.MinValue;

            if ( mdMsg.To.HasValue )
            {
                upperBound = mdMsg.To.GetValueOrDefault().UtcDateTime;
            }
            else
            {
                upperBound = DateTime.UtcNow;
            }

            candlesNeedLiveUpdate = mdMsg.IsSubscribe;

            var latestBarTime       = lowerBound;
            var request             = factory.createMarketDataSnapshotRequestInstrument( symbol, o2Gtimeframe, barCount );
            var dtFirst             = upperBound;
            var backwardFirst       = DateTime.MinValue;

            var prefixString = "--------->";

            DateTime barDate = DateTime.MinValue;

            int noMoreData = 0;

            try
            {
                List<TimeFrameCandleMessage> buffer = new List<TimeFrameCandleMessage>( );

                // cause there is limit for returned candles amount
                do
                {
                    // We are no longer in the Started State
                    if ( _canProcess != null && !_canProcess( false ) )
                    {
                        break;
                    }

                    backwardFirst = dtFirst;

                    if ( lowerBound >= dtFirst )
                    {
                        break;
                    }


                    factory.fillMarketDataSnapshotRequestTime( request, lowerBound, dtFirst, true, O2GCandleOpenPriceMode.PreviousClose );

                    if ( _pricelistener != null )
                    {
                        _pricelistener.SetRequestID( request.RequestID, period, symbol );
                    }

                    o2Gsession.sendRequest( request );


                    if ( _pricelistener != null )
                    {
                        if ( period == TimeSpan.FromTicks( 1 ) )
                        {
                            _pricelistener.Wait3Mins( request.RequestID );
                        }
                        else
                        {
                            _pricelistener.Wait( request.RequestID );
                        }
                    }


                    O2GMarketDataSnapshotResponseReader reader = null;


                    // shift "to" bound to oldest datetime of returned data
                    O2GResponse response = null;

                    if ( _pricelistener != null )
                    {
                        response = _pricelistener.GetResponse( request.RequestID );
                    }

                    if ( response != null && response.Type == O2GResponseType.MarketDataSnapshot )
                    {
                        O2GResponseReaderFactory readerFactory = o2Gsession.getResponseReaderFactory( );

                        if ( readerFactory != null )
                        {
                            reader = readerFactory.createMarketDataSnapshotReader( response );

                            if ( reader.Count > 0 )
                            {
                                // Check if the first returned bar is the same as the upper bound
                                if ( DateTime.Compare( dtFirst, reader.getDate( 0 ) ) != 0 )
                                {
                                    dtFirst = reader.getDate( 0 ); // earliest datetime of returned data

                                    if ( sameDate == DateTime.MinValue )
                                    {
                                        sameDate = reader.getDate( reader.Count - 1 );
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                LoggingHelper.AddWarningLog( this, "0 rows received" );
                                break;
                            }
                        }


                        for ( int index = reader.Count - 1; index >= 0; index-- )
                        {
                            if ( reader.isBar )
                            {
                                var msg                   = new TimeFrameCandleMessage( );

                                msg.Arg                   = period;
                                msg.SecurityId            = mdMsg.SecurityId;
                                msg.OpenPrice             = reader.getBidOpen( index ).ToDecimal() ?? decimal.Zero;
                                msg.HighPrice             = reader.getBidHigh( index ).ToDecimal() ?? decimal.Zero;
                                msg.LowPrice              = reader.getBidLow( index ).ToDecimal() ?? decimal.Zero;
                                msg.ClosePrice            = reader.getBidClose( index ).ToDecimal() ?? decimal.Zero;

                                barDate                   = reader.getDate( index );
                                var utcTime               = DateTime.SpecifyKind( barDate, DateTimeKind.Utc );

                                msg.OpenTime              = utcTime;
                                msg.TotalVolume           = reader.getVolume( index );
                                msg.OriginalTransactionId = transactionId;
                                msg.SubscriptionId        = transactionId;

                                if ( upperBound >= barDate + period )
                                {
                                    msg.State = CandleStates.Finished;
                                }
                                else
                                {
                                    msg.State = CandleStates.Active;
                                }

                                 
                                
                                msg.BatchStatus = fxBatchStatus.Batching;

                                if ( barDate > latestBarTime )
                                {
                                    latestBarTime = barDate;
                                }

                                buffer.Add( msg );
                            }
                        }

                        string message = string.Format( "{5}[{0}] ({1}) : Buffering Full Download.... Count = {4} Range = [ {2:g} - {3:g} ] ..... ", symbol, period.ToReadable(), dtFirst, backwardFirst, reader.Count, prefixString );

                        LoggingHelper.AddInfoLog( this, message );
                    }
                    else
                    {
                        noMoreData++;

                        var diff = dtFirst - lowerBound;
                        string message = string.Format( "{4}[{0}] Period : ({1}) !! No more data [ {2:g} - {3:g} ]", symbol, period.ToReadable(), lowerBound, dtFirst, prefixString );
                        ProcessErrors( request, symbol, period, diff, message );

                        dtFirst = dtFirst - period;

                        if ( noMoreData > 20 )
                        {
                            break;
                        }
                    }
                }
                while ( dtFirst > lowerBound );

                if ( buffer.Count > 1 )
                {
                    var acending      = buffer.OrderBy( x => x.OpenTime ).ToList( );

                    if ( period < TimeSpan.FromHours( 1 ) )
                    {
                        acending.RemoveAt( acending.Count - 1 );
                    }

                    var begin         = acending.First( );
                    begin.BatchStatus = fxBatchStatus.BeginBatch;
                    var end           = acending.Last( );
                    end.BatchStatus = fxBatchStatus.EndBatch;

                    foreach ( var candleMsg in acending )
                    {
                        SendOutMessage( candleMsg );
                    }

                    string sendMsg = string.Format( "**** [{0}] ({1}) : Send Full Download Msg [ {2:g} - {3:g} ] ...Total= {4} **** ", symbol, period.ToReadable(), begin.OpenTime, end.OpenTime, buffer.Count );

                    LoggingHelper.AddInfoLog( this, sendMsg );

                    buffer.Clear();
                }
                else if ( buffer.Count == 1 )
                {
                    foreach ( var candleMsg in buffer )
                    {
                        candleMsg.BatchStatus = fxBatchStatus.FromStorage;
                        SendOutMessage( candleMsg );
                    }

                    buffer.Clear();
                }
            }
            catch ( Exception e )
            {
                LoggingHelper.AddErrorLog( this, e );
            }

            if ( candlesNeedLiveUpdate )
            {
                if ( !_candlesLiveDownload.ContainsKey( (symbol, period) ) )
                {
                    _candlesLiveDownload.Add( (symbol, period), (mdMsg, latestBarTime) );

                    if ( !_timerStarted )
                    {
                        StartTimer();

                        _timerStarted = true;
                    }
                }

                SendSubscriptionOnline( mdMsg.OriginalTransactionId );
            }
            else
            {
                SendSubscriptionFinished( mdMsg.TransactionId, new DateTimeOffset?() );
            }
        }

        public void DownloadHistoricBars( MarketDataMessage mdMsg )
        {
            var o2Gsession          = GetSession( );

            if( o2Gsession == null )
                return;


            var factory             = o2Gsession.getRequestFactory( );

            if ( factory == null )
            {
                throw new InvalidOperationException( );
            }

            bool candlesNeedLiveUpdate = false;

            var sameDate            = DateTime.MinValue;
            var securityId          = mdMsg.SecurityId;
            var symbol              = securityId.SecurityCode;
            var period              = ( TimeSpan ) mdMsg.DataType2.Arg;
            var o2Gtimeframe        = period.ToFxcmTimeFrame( factory.Timeframes );
            var barCount            = ( int )( mdMsg.Count ?? 300L );
            var transactionId       = mdMsg.TransactionId;
            var lowerBound          = mdMsg.From.HasValue ? mdMsg.From.GetValueOrDefault( ).UtcDateTime : factory.ZERODATE;

            DateTime upperBound = DateTime.MinValue;

            if ( mdMsg.To.HasValue )
            {
                upperBound = mdMsg.To.GetValueOrDefault( ).UtcDateTime;
            }
            else
            {
                upperBound = DateTime.UtcNow;                
            }

            candlesNeedLiveUpdate = mdMsg.IsSubscribe;

            var latestBarTime       = lowerBound;
            var request             = factory.createMarketDataSnapshotRequestInstrument( symbol, o2Gtimeframe, barCount );
            var dtFirst             = upperBound;
            var backwardFirst       = DateTime.MinValue;

            var prefixString = "--------->";

            DateTime barDate = DateTime.MinValue;

            int noMoreData = 0;

            try
            {
                List<TimeFrameCandleMessage> buffer = new List<TimeFrameCandleMessage>( );

                // cause there is limit for returned candles amount
                do
                {
                    // We are no longer in the Started State
                    if ( _canProcess != null && !_canProcess( false ) )
                    {
                        break;
                    }

                    backwardFirst = dtFirst;

                    if ( lowerBound >= dtFirst )
                    {
                        break;
                    }

                    
                    factory.fillMarketDataSnapshotRequestTime( request, lowerBound, dtFirst, true, O2GCandleOpenPriceMode.PreviousClose );

                    if ( _pricelistener != null )
                    {
                        _pricelistener.SetRequestID( request.RequestID, period, symbol );
                    }
                    
                    o2Gsession.sendRequest( request );


                    if ( _pricelistener != null )
                    {
                        if ( period == TimeSpan.FromTicks( 1 ) )
                        {
                            _pricelistener.Wait3Mins( request.RequestID );
                        }
                        else
                        {
                            _pricelistener.Wait( request.RequestID );
                        }
                    }
                    

                    O2GMarketDataSnapshotResponseReader reader = null;


                    // shift "to" bound to oldest datetime of returned data
                    O2GResponse response = null;

                    if ( _pricelistener != null )
                    {
                        response = _pricelistener.GetResponse( request.RequestID );
                    }
                        
                    if ( response != null && response.Type == O2GResponseType.MarketDataSnapshot )
                    {
                        O2GResponseReaderFactory readerFactory = o2Gsession.getResponseReaderFactory( );

                        if ( readerFactory != null )
                        {
                            reader = readerFactory.createMarketDataSnapshotReader( response );

                            if ( reader.Count > 0 )
                            {
                                // Check if the first returned bar is the same as the upper bound
                                if ( DateTime.Compare( dtFirst, reader.getDate( 0 ) ) != 0 )
                                {
                                    dtFirst = reader.getDate( 0 ); // earliest datetime of returned data

                                    if ( sameDate == DateTime.MinValue )
                                    {
                                        sameDate = reader.getDate( reader.Count - 1 );
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                LoggingHelper.AddWarningLog( this, "0 rows received" );
                                break;
                            }
                        }


                        for ( int index = reader.Count - 1; index >= 0; index-- )
                        {
                            if ( reader.isBar )
                            {
                                var msg                   = new TimeFrameCandleMessage( );

                                msg.Arg                   = period;
                                msg.SecurityId            = mdMsg.SecurityId;
                                msg.OpenPrice             = reader.getBidOpen( index ).ToDecimal( ) ?? decimal.Zero;
                                msg.HighPrice             = reader.getBidHigh( index ).ToDecimal( ) ?? decimal.Zero;
                                msg.LowPrice              = reader.getBidLow( index ).ToDecimal( ) ?? decimal.Zero;
                                msg.ClosePrice            = reader.getBidClose( index ).ToDecimal( ) ?? decimal.Zero;

                                barDate                   = reader.getDate( index );
                                var utcTime               = DateTime.SpecifyKind( barDate, DateTimeKind.Utc );

                                msg.OpenTime              = utcTime;
                                msg.TotalVolume           = reader.getVolume( index );
                                msg.OriginalTransactionId = transactionId;
                                msg.SubscriptionId        = transactionId;
                                msg.State = ( upperBound >= barDate + period ) ? CandleStates.Finished : CandleStates.Active;
                                msg.BatchStatus           = fxBatchStatus.Batching;

                                if ( barDate > latestBarTime )
                                {
                                    latestBarTime = barDate;
                                }

                                buffer.Add( msg );
                            }
                        }

                        string message = string.Format( "{5}[{0}] ({1}) : Done Downlad Count = {4} Range = [ {2:g} - {3:g} ] ..... ", symbol, period.ToReadable(), dtFirst, backwardFirst, reader.Count, prefixString );

                        LoggingHelper.AddInfoLog( this, message );
                    }
                    else
                    {
                        noMoreData++;

                        var diff = dtFirst - lowerBound;
                        string message = string.Format( "{4}[{0}] Period : ({1}) !! No more data [ {2:g} - {3:g} ]", symbol, period.ToReadable(), lowerBound, dtFirst, prefixString );
                        ProcessErrors( request, symbol, period, diff, message );

                        dtFirst = dtFirst - period;

                        if ( noMoreData > 20 )
                        {
                            break;
                        }
                    }
                }
                while ( dtFirst > lowerBound );

                if ( buffer.Count > 1 )
                {
                    var acending      = buffer.OrderBy( x => x.OpenTime ).ToList( );

                    if ( period < TimeSpan.FromHours( 1 ) )
                    {
                        acending.RemoveAt( acending.Count - 1 );
                    }

                    var begin         = acending.First( );
                    begin.BatchStatus = fxBatchStatus.BeginBatch;
                    var end           = acending.Last( );
                    end.BatchStatus = fxBatchStatus.EndBatch;

                    foreach ( var candleMsg in acending )
                    {
                        SendOutMessage( candleMsg );
                    }

                    string sendMsg = string.Format( "**** [{0}] ({1}) : Send Batch Download Msg [ {2:g} - {3:g} ] ...Total= {4} **** ", symbol, period.ToReadable(), begin.OpenTime, end.OpenTime, buffer.Count );

                    LoggingHelper.AddInfoLog( this, sendMsg );

                    buffer.Clear( );
                }
                else if ( buffer.Count == 1 )
                {
                    foreach ( var candleMsg in buffer )
                    {
                        candleMsg.BatchStatus = fxBatchStatus.FromStorage;
                        SendOutMessage( candleMsg );
                    }

                    buffer.Clear( );
                }
            }
            catch ( Exception e )
            {
                LoggingHelper.AddErrorLog( this, e );
            }

            if ( candlesNeedLiveUpdate )
            {                
                if ( !_candlesLiveDownload.ContainsKey( (symbol, period) ) )
                {
                    _candlesLiveDownload.Add( (symbol, period), ( mdMsg, latestBarTime ) );

                    if ( !_timerStarted )
                    {
                        StartTimer( );

                        _timerStarted = true;
                    }
                }

                SendSubscriptionOnline( mdMsg.OriginalTransactionId );
            }
            else
            {
                SendSubscriptionFinished( mdMsg.TransactionId, new DateTimeOffset?( ) );
            }
        }

        public void DownloadIntradayHistoricBars( MarketDataMessage mdMsg )
        {
            var o2Gsession = GetSession( );

            if ( o2Gsession == null )
                return;


            var factory = o2Gsession.getRequestFactory( );

            if ( factory == null )
            {
                throw new InvalidOperationException();
            }

            bool candlesNeedLiveUpdate = false;

            var sameDate            = DateTime.MinValue;
            var securityId          = mdMsg.SecurityId;
            var symbol              = securityId.SecurityCode;
            var period              = ( TimeSpan ) mdMsg.DataType2.Arg;
            var o2Gtimeframe        = period.ToFxcmTimeFrame( factory.Timeframes );
            var barCount            = ( int )( mdMsg.Count ?? 300L );
            var transactionId       = mdMsg.TransactionId;
            var lowerBound          = mdMsg.From.HasValue ? mdMsg.From.GetValueOrDefault( ).UtcDateTime : factory.ZERODATE;

            DateTime upperBound = DateTime.MinValue;

            if ( mdMsg.To.HasValue )
            {
                upperBound = mdMsg.To.GetValueOrDefault().UtcDateTime;
            }
            else
            {
                upperBound = DateTime.UtcNow;
            }

            candlesNeedLiveUpdate = mdMsg.IsSubscribe;

            var latestBarTime       = lowerBound;
            var request             = factory.createMarketDataSnapshotRequestInstrument( symbol, o2Gtimeframe, barCount );
            var dtFirst             = upperBound;
            var backwardFirst       = DateTime.MinValue;

            DateTime barDate = DateTime.MinValue;

            int noMoreData = 0;

            try
            {
                List<TimeFrameCandleMessage> buffer = new List<TimeFrameCandleMessage>( );

                // cause there is limit for returned candles amount
                do
                {
                    // We are no longer in the Started State
                    if ( _canProcess != null && !_canProcess( false ) )
                    {
                        break;
                    }

                    backwardFirst = dtFirst;

                    if ( lowerBound >= dtFirst )
                    {
                        break;
                    }


                    factory.fillMarketDataSnapshotRequestTime( request, lowerBound, dtFirst, true, O2GCandleOpenPriceMode.PreviousClose );

                    if ( _pricelistener != null )
                    {
                        _pricelistener.SetRequestID( request.RequestID, period, symbol );
                    }

                    o2Gsession.sendRequest( request );


                    if ( _pricelistener != null )
                    {
                        if ( period == TimeSpan.FromTicks( 1 ) )
                        {
                            _pricelistener.Wait3Mins( request.RequestID );
                        }
                        else
                        {
                            _pricelistener.Wait( request.RequestID );
                        }
                    }


                    O2GMarketDataSnapshotResponseReader reader = null;


                    // shift "to" bound to oldest datetime of returned data
                    O2GResponse response = null;

                    if ( _pricelistener != null )
                    {
                        response = _pricelistener.GetResponse( request.RequestID );
                    }

                    if ( response != null && response.Type == O2GResponseType.MarketDataSnapshot )
                    {
                        O2GResponseReaderFactory readerFactory = o2Gsession.getResponseReaderFactory( );

                        if ( readerFactory != null )
                        {
                            reader = readerFactory.createMarketDataSnapshotReader( response );

                            if ( reader.Count > 0 )
                            {
                                // Check if the first returned bar is the same as the upper bound
                                if ( DateTime.Compare( dtFirst, reader.getDate( 0 ) ) != 0 )
                                {
                                    dtFirst = reader.getDate( 0 ); // earliest datetime of returned data

                                    if ( sameDate == DateTime.MinValue )
                                    {
                                        sameDate = reader.getDate( reader.Count - 1 );
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                LoggingHelper.AddWarningLog( this, "0 rows received" );
                                break;
                            }
                        }


                        for ( int index = reader.Count - 1; index >= 0; index-- )
                        {
                            if ( reader.isBar )
                            {
                                var msg                   = new TimeFrameCandleMessage( );

                                msg.Arg                   = period;
                                msg.SecurityId            = mdMsg.SecurityId;
                                msg.OpenPrice             = reader.getBidOpen( index ).ToDecimal() ?? decimal.Zero;
                                msg.HighPrice             = reader.getBidHigh( index ).ToDecimal() ?? decimal.Zero;
                                msg.LowPrice              = reader.getBidLow( index ).ToDecimal() ?? decimal.Zero;
                                msg.ClosePrice            = reader.getBidClose( index ).ToDecimal() ?? decimal.Zero;

                                barDate                   = reader.getDate( index );
                                var utcTime               = DateTime.SpecifyKind( barDate, DateTimeKind.Utc );

                                msg.OpenTime              = utcTime;
                                msg.TotalVolume           = reader.getVolume( index );
                                msg.OriginalTransactionId = transactionId;
                                msg.SubscriptionId        = transactionId;
                                msg.State = ( upperBound >= barDate + period ) ? CandleStates.Finished : CandleStates.Active;
                                msg.BatchStatus           = fxBatchStatus.Batching;

                                if ( barDate > latestBarTime )
                                {
                                    latestBarTime = barDate;
                                }

                                
                                
                                if ( sameDate.Date == barDate.Date )
                                {
                                    buffer.Add( msg );
                                }
                                else
                                {
                                    if ( buffer.Count > 1 )
                                    {
                                        var acending = buffer.OrderBy( x => x.OpenTime ).ToList( );



                                        var begin         = acending.First( );
                                        begin.BatchStatus = fxBatchStatus.BeginBatch;
                                        var end           = acending.Last( );
                                        end.BatchStatus = fxBatchStatus.EndBatch;

                                        foreach ( var candleMsg in acending )
                                        {
                                            SendOutMessage( candleMsg );
                                        }

                                        string sendMsg = string.Format( "[{0}] ({1}) : Send Batch Download Msg [ {2:g} - {3:g} ] ...", symbol, period.ToReadable(), begin.OpenTime, end.OpenTime );

                                        LoggingHelper.AddInfoLog( this, sendMsg );

                                        buffer.Clear();
                                    }
                                    else if ( buffer.Count == 1 )
                                    {
                                        foreach ( var candleMsg in buffer )
                                        {
                                            candleMsg.BatchStatus = fxBatchStatus.FromStorage;
                                            SendOutMessage( candleMsg );
                                        }

                                        buffer.Clear();
                                    }

                                    sameDate = barDate;

                                    buffer.Add( msg );
                                }
                                


                            }
                        }

                        string message = string.Format( "{5}[{0}] ({1}) : Done Downlad Count = {4} Range = [ {2:g} - {3:g} ] ..... ", symbol, period.ToReadable(), dtFirst, backwardFirst, reader.Count, "<----" );

                        LoggingHelper.AddInfoLog( this, message );
                    }
                    else
                    {
                        noMoreData++;

                        var diff = dtFirst - lowerBound;
                        string message = string.Format( "{4}[{0}] Period : ({1}) !! No more data [ {2:g} - {3:g} ]", symbol, period.ToReadable(), lowerBound, dtFirst, "<--D--D--" );
                        ProcessErrors( request, symbol, period, diff, message );

                        dtFirst = dtFirst - period;

                        if ( noMoreData > 20 )
                        {
                            break;
                        }
                    }
                }
                while ( dtFirst > lowerBound );

                if ( buffer.Count > 1 )
                {
                    var acending      = buffer.OrderBy( x => x.OpenTime ).ToList( );

                    if ( period < TimeSpan.FromHours( 1 ) )
                    {
                        acending.RemoveAt( acending.Count - 1 );
                    }

                    var begin         = acending.First( );
                    begin.BatchStatus = fxBatchStatus.BeginBatch;
                    var end           = acending.Last( );
                    end.BatchStatus = fxBatchStatus.EndBatch;

                    foreach ( var candleMsg in acending )
                    {
                        SendOutMessage( candleMsg );
                    }

                    string sendMsg = string.Format( "**** [{0}] ({1}) : Send Batch Download Msg [ {2:g} - {3:g} ] ...Total= {4} **** ", symbol, period.ToReadable(), begin.OpenTime, end.OpenTime, buffer.Count );

                    LoggingHelper.AddInfoLog( this, sendMsg );

                    buffer.Clear();
                }
                else if ( buffer.Count == 1 )
                {
                    foreach ( var candleMsg in buffer )
                    {
                        candleMsg.BatchStatus = fxBatchStatus.FromStorage;
                        SendOutMessage( candleMsg );
                    }

                    buffer.Clear();
                }
            }
            catch ( Exception e )
            {
                LoggingHelper.AddErrorLog( this, e );
            }

            if ( candlesNeedLiveUpdate )
            {
                if ( !_candlesLiveDownload.ContainsKey( (symbol, period) ) )
                {
                    _candlesLiveDownload.Add( (symbol, period), (mdMsg, latestBarTime) );

                    if ( !_timerStarted )
                    {
                        StartTimer();

                        _timerStarted = true;
                    }
                }

                SendSubscriptionOnline( mdMsg.OriginalTransactionId );
            }
            else
            {
                SendSubscriptionFinished( mdMsg.TransactionId, new DateTimeOffset?() );
            }

            //MarketDataMessage marketDataMessage = new MarketDataMessage( );
            //marketDataMessage.OriginalTransactionId = ( mdMsg.TransactionId );
            //SendOutMessage( marketDataMessage );

        }

        public void DownloadIntradayHoulyHistoricBars( MarketDataMessage mdMsg )
        {
            var o2Gsession = GetSession( );

            if ( o2Gsession == null )
                return;


            var factory = o2Gsession.getRequestFactory( );

            if ( factory == null )
            {
                throw new InvalidOperationException();
            }

            bool candlesNeedLiveUpdate = false;

            var sameMonth            = DateTime.MinValue;
            var securityId          = mdMsg.SecurityId;
            var symbol              = securityId.SecurityCode;
            var period              = ( TimeSpan ) mdMsg.DataType2.Arg;
            var o2Gtimeframe        = period.ToFxcmTimeFrame( factory.Timeframes );
            var barCount            = ( int )( mdMsg.Count ?? 300L );
            var transactionId       = mdMsg.TransactionId;
            var lowerBound          = mdMsg.From.HasValue ? mdMsg.From.GetValueOrDefault( ).UtcDateTime : factory.ZERODATE;

            DateTime upperBound = DateTime.MinValue;

            if ( mdMsg.To.HasValue )
            {
                upperBound = mdMsg.To.GetValueOrDefault().UtcDateTime;
            }
            else
            {
                upperBound = DateTime.UtcNow;
            }

            candlesNeedLiveUpdate = mdMsg.IsSubscribe;

            var latestBarTime       = lowerBound;
            var request             = factory.createMarketDataSnapshotRequestInstrument( symbol, o2Gtimeframe, barCount );
            var dtFirst             = upperBound;
            var backwardFirst       = DateTime.MinValue;

            var extendedInfo        = mdMsg.ExtensionInfo;

            DateTime barDate = DateTime.MinValue;

            int noMoreData = 0;

            try
            {
                List<TimeFrameCandleMessage> buffer = new List<TimeFrameCandleMessage>( );

                // cause there is limit for returned candles amount
                do
                {
                    // We are no longer in the Started State
                    if ( _canProcess != null && !_canProcess( false ) )
                    {
                        break;
                    }

                    backwardFirst = dtFirst;

                    if ( lowerBound >= dtFirst )
                    {
                        break;
                    }


                    factory.fillMarketDataSnapshotRequestTime( request, lowerBound, dtFirst, true, O2GCandleOpenPriceMode.PreviousClose );

                    if ( _pricelistener != null )
                    {
                        _pricelistener.SetRequestID( request.RequestID, period, symbol );
                    }

                    o2Gsession.sendRequest( request );


                    if ( _pricelistener != null )
                    {
                        if ( period == TimeSpan.FromTicks( 1 ) )
                        {
                            _pricelistener.Wait3Mins( request.RequestID );
                        }
                        else
                        {
                            _pricelistener.Wait( request.RequestID );
                        }
                    }


                    O2GMarketDataSnapshotResponseReader reader = null;


                    // shift "to" bound to oldest datetime of returned data
                    O2GResponse response = null;

                    if ( _pricelistener != null )
                    {
                        response = _pricelistener.GetResponse( request.RequestID );
                    }

                    if ( response != null && response.Type == O2GResponseType.MarketDataSnapshot )
                    {
                        O2GResponseReaderFactory readerFactory = o2Gsession.getResponseReaderFactory( );

                        if ( readerFactory != null )
                        {
                            reader = readerFactory.createMarketDataSnapshotReader( response );

                            if ( reader.Count > 0 )
                            {
                                // Check if the first returned bar is the same as the upper bound
                                if ( DateTime.Compare( dtFirst, reader.getDate( 0 ) ) != 0 )
                                {
                                    dtFirst = reader.getDate( 0 ); // earliest datetime of returned data

                                    if ( sameMonth == DateTime.MinValue )
                                    {
                                        sameMonth = reader.getDate( reader.Count - 1 );
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                LoggingHelper.AddWarningLog( this, "0 rows received" );
                                break;
                            }
                        }


                        for ( int index = reader.Count - 1; index >= 0; index-- )
                        {
                            if ( reader.isBar )
                            {
                                var msg                   = new TimeFrameCandleMessage( );

                                msg.Arg                   = period;
                                msg.SecurityId            = mdMsg.SecurityId;
                                msg.OpenPrice             = reader.getBidOpen( index ).ToDecimal() ?? decimal.Zero;
                                msg.HighPrice             = reader.getBidHigh( index ).ToDecimal() ?? decimal.Zero;
                                msg.LowPrice              = reader.getBidLow( index ).ToDecimal() ?? decimal.Zero;
                                msg.ClosePrice            = reader.getBidClose( index ).ToDecimal() ?? decimal.Zero;

                                barDate                   = reader.getDate( index );
                                var utcTime               = DateTime.SpecifyKind( barDate, DateTimeKind.Utc );

                                msg.OpenTime              = utcTime;
                                msg.TotalVolume           = reader.getVolume( index );
                                msg.OriginalTransactionId = transactionId;
                                msg.SubscriptionId        = transactionId;
                                msg.State = ( upperBound >= barDate + period ) ? CandleStates.Finished : CandleStates.Active;
                                msg.BatchStatus           = fxBatchStatus.Batching;

                                if ( barDate > latestBarTime )
                                {
                                    latestBarTime = barDate;
                                }


                               
                                if ( sameMonth.Month == barDate.Month  )
                                {
                                    buffer.Add( msg );
                                }
                                else
                                {
                                    if ( buffer.Count > 1 )
                                    {
                                        var acending = buffer.OrderBy( x => x.OpenTime ).ToList( );

                                        var begin         = acending.First( );
                                        begin.BatchStatus = fxBatchStatus.BeginBatch;
                                        var end           = acending.Last( );
                                        end.BatchStatus   = fxBatchStatus.EndBatch;

                                        foreach ( var candleMsg in acending )
                                        {
                                            SendOutMessage( candleMsg );
                                        }

                                        string sendMsg = string.Format( "[{0}] ({1}) : Send Batch Download Msg [ {2:g} - {3:g} ] ...", symbol, period.ToReadable(), begin.OpenTime, end.OpenTime );

                                        LoggingHelper.AddInfoLog( this, sendMsg );

                                        buffer.Clear();
                                    }
                                    else if ( buffer.Count == 1 )
                                    {
                                        foreach ( var candleMsg in buffer )
                                        {
                                            candleMsg.BatchStatus = fxBatchStatus.FromStorage;
                                            SendOutMessage( candleMsg );
                                        }

                                        buffer.Clear();
                                    }

                                    sameMonth = barDate;

                                    buffer.Add( msg );
                                }



                            }
                        }

                        string message = string.Format( "{5}[{0}] ({1}) : Done Downlad Count = {4} Range = [ {2:g} - {3:g} ] ..... ", symbol, period.ToReadable(), dtFirst, backwardFirst, reader.Count, "<--M--M--" );

                        LoggingHelper.AddInfoLog( this, message );
                    }
                    else
                    {
                        noMoreData++;

                        var diff = dtFirst - lowerBound;
                        string message = string.Format( "{4}[{0}] Period : ({1}) !! No more data [ {2:g} - {3:g} ]", symbol, period.ToReadable(), lowerBound, dtFirst, "<----" );
                        ProcessErrors( request, symbol, period, diff, message );

                        dtFirst = dtFirst - period;

                        if ( noMoreData > 20 )
                        {
                            break;
                        }
                    }
                }
                while ( dtFirst > lowerBound );

                if ( buffer.Count > 1 )
                {
                    var acending      = buffer.OrderBy( x => x.OpenTime ).ToList( );

                    if ( period < TimeSpan.FromHours( 1 ) )
                    {
                        acending.RemoveAt( acending.Count - 1 );
                    }

                    var begin         = acending.First( );
                    begin.BatchStatus = fxBatchStatus.BeginBatch;
                    var end           = acending.Last( );
                    end.BatchStatus = fxBatchStatus.EndBatch;

                    foreach ( var candleMsg in acending )
                    {
                        SendOutMessage( candleMsg );
                    }

                    string sendMsg = string.Format( "**** [{0}] ({1}) : Send Batch Download Msg [ {2:g} - {3:g} ] ...Total= {4} **** ", symbol, period.ToReadable(), begin.OpenTime, end.OpenTime, buffer.Count );

                    LoggingHelper.AddInfoLog( this, sendMsg );

                    buffer.Clear();
                }
                else if ( buffer.Count == 1 )
                {
                    foreach ( var candleMsg in buffer )
                    {
                        candleMsg.BatchStatus = fxBatchStatus.FromStorage;
                        SendOutMessage( candleMsg );
                    }

                    buffer.Clear();
                }
            }
            catch ( Exception e )
            {
                LoggingHelper.AddErrorLog( this, e );
            }

            if ( candlesNeedLiveUpdate )
            {
                if ( !_candlesLiveDownload.ContainsKey( (symbol, period) ) )
                {
                    _candlesLiveDownload.Add( (symbol, period), (mdMsg, latestBarTime) );

                    if ( !_timerStarted )
                    {
                        StartTimer();

                        _timerStarted = true;
                    }
                }

                SendSubscriptionOnline( mdMsg.OriginalTransactionId );
            }
            else
            {
                SendSubscriptionFinished( mdMsg.TransactionId, new DateTimeOffset?() );
            }
        }

        public DateTime DownloadLastestFewBars( (string, TimeSpan) symbolTF, (MarketDataMessage, DateTime)  symbolPayload,  DateTime upperBound )
        {
            var symbol          = symbolTF.Item1;
            var period          = symbolTF.Item2;
            var msg             = symbolPayload.Item1;

            DateTime lowerBound = symbolPayload.Item2;
            long transactionId  = msg.TransactionId;

            if ( _fxSessionId == null )
            {
                return DateTime.MinValue;
            }


            var factory = _fxSessionId.getRequestFactory( );

            if ( factory == null )
            {
                return DateTime.MinValue;
            }

            var sameDate        = DateTime.MinValue;
            var latestBarTime   = DateTime.MinValue;
            var o2Gtimeframe    = period.ToFxcmTimeFrame( factory.Timeframes );
            var request         = factory.createMarketDataSnapshotRequestInstrument( symbol, o2Gtimeframe, 300 );
            var dtFirst         = upperBound;
            var backwardFirst   = DateTime.MinValue;
            string prefixString = "----->";
            var barDate         = DateTime.MinValue;

            try
            {
                List<TimeFrameCandleMessage> buffer = new List<TimeFrameCandleMessage>( );

                // cause there is limit for returned candles amount
                do
                {
                    // We are no longer in the Started State
                    if ( ( _canProcess != null && !_canProcess( false ) ) )
                    {
                        break;
                    }

                    backwardFirst = dtFirst;

                    if ( lowerBound >= dtFirst )
                    {
                        break;
                    }

                    factory.fillMarketDataSnapshotRequestTime( request, lowerBound, dtFirst, true, O2GCandleOpenPriceMode.PreviousClose );

                    if ( _pricelistener != null )
                    {
                        _pricelistener.SetRequestID( request.RequestID, period, symbol );
                    }
                        

                    _fxSessionId?.sendRequest( request );

                    if ( _pricelistener != null )
                    {
                        _pricelistener.Wait( request.RequestID );
                    }
                        
                    O2GMarketDataSnapshotResponseReader reader = null;

                    // shift "to" bound to oldest datetime of returned data
                    O2GResponse response = null;

                    if ( _pricelistener != null )
                    {
                        response = _pricelistener.GetResponse( request.RequestID );
                    }

                    if ( response != null && response.Type == O2GResponseType.MarketDataSnapshot )
                    {
                        O2GResponseReaderFactory readerFactory = _fxSessionId?.getResponseReaderFactory( );

                        if ( readerFactory != null )
                        {
                            reader = readerFactory.createMarketDataSnapshotReader( response );

                            if ( reader.Count > 0 )
                            {
                                // Check if the first returned bar is the same as the upper bound
                                if ( DateTime.Compare( dtFirst, reader.getDate( 0 ) ) != 0 )
                                {
                                    dtFirst = reader.getDate( 0 ); // earliest datetime of returned data

                                    if ( sameDate == DateTime.MinValue )
                                    {
                                        sameDate = reader.getDate( reader.Count - 1 );
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                LoggingHelper.AddWarningLog( this, "0 rows received" );
                                break;
                            }
                        }

                        for ( int index = reader.Count - 1; index >= 0; index-- )
                        {
                            if ( reader.isBar )
                            {
                                var candleMsg        = new TimeFrameCandleMessage( );

                                candleMsg.OpenPrice  = reader.getBidOpen( index ).ToDecimal( ) ?? decimal.Zero;
                                candleMsg.HighPrice  = reader.getBidHigh( index ).ToDecimal( ) ?? decimal.Zero;
                                candleMsg.LowPrice   = reader.getBidLow( index ).ToDecimal( ) ?? decimal.Zero;
                                candleMsg.ClosePrice = reader.getBidClose( index ).ToDecimal( ) ?? decimal.Zero;

                                barDate              = reader.getDate( index );
                                var utcTime          = DateTime.SpecifyKind( barDate, DateTimeKind.Utc );

                                if ( barDate > latestBarTime )
                                {
                                    latestBarTime = barDate;
                                }

                                candleMsg.OpenTime                = utcTime;
                                candleMsg.TotalVolume             = reader.getVolume( index );
                                candleMsg.OriginalTransactionId   = transactionId;
                                //candleMsg.State                   = CandleStates.Finished ;
                                candleMsg.State                 = ( upperBound >= barDate + period ) ? CandleStates.Finished : CandleStates.Active;
                                candleMsg.BatchStatus             = fxBatchStatus.Latest;

                                if ( sameDate.Date == barDate.Date )
                                {
                                    buffer.Add( candleMsg );
                                }
                                else
                                {
                                    if ( buffer.Count > 0 )
                                    {
                                        var acending = buffer.OrderBy( x => x.OpenTime ).ToList( );

                                        foreach ( var newCandleMsg in acending )
                                        {
                                            SendOutMessage( newCandleMsg );
                                        }

                                        buffer.Clear( );
                                    }

                                    sameDate = barDate;

                                    buffer.Add( candleMsg );
                                }
                            }
                            else
                            {

                            }
                        }

                        string message = string.Format( "{5}[{0}] ({1}) : Done Downlad [ {2:g} - {3:g} ] ..... Count = {4}", symbol, period.ToReadable(), dtFirst, backwardFirst, reader.Count, prefixString );

                        LoggingHelper.AddInfoLog( this, message );
                    }
                    else
                    {
                        var diff = dtFirst - lowerBound;
                        string message = string.Format( "{4}[{0}] Period : ({1}) !! No more data [ {2:g} - {3:g} ]", symbol, period.ToReadable(), lowerBound, dtFirst, prefixString );
                        ProcessErrors( request, symbol, period, diff, message );

                        dtFirst = dtFirst - period;
                    }
                }
                while ( dtFirst > lowerBound );

                if ( buffer.Count > 0 )
                {
                    var acending = buffer.OrderBy( x => x.OpenTime ).ToList( );

                    acending.RemoveAt( acending.Count - 1 );

                    foreach ( var candleMsg in acending )
                    {
                        SendOutMessage( candleMsg );
                    }

                    buffer.Clear( );
                }
            }
            catch ( Exception e )
            {
                LoggingHelper.AddErrorLog( this, e );
            }

            //var mktDataMsg = new MarketDataMessage( );
            //mktDataMsg.OriginalTransactionId = transactionId;

            //SendOutMessage( mktDataMsg );

            return latestBarTime;
        }

        public DateTime DownloadLastestFewTicks( (string, TimeSpan) symbolTF, (MarketDataMessage, DateTime) symbolPayload,  DateTime upperBound )
        {
            var symbol          = symbolTF.Item1;
            var period          = symbolTF.Item2;
            var msg             = symbolPayload.Item1;
            var lastBar         = symbolPayload.Item2;

            string log = string.Format( "[{0}] ({1}) Start Download Thread. From {2} to {3}", symbol, period.ToReadable( ), lastBar, upperBound );
            this.AddInfoLog( log );

            DateTime lowerBound = lastBar;
            var latestTickTime  = lowerBound;
            long transactionId  = msg.TransactionId;

            var o2Gsession      = GetSession( );

            if ( o2Gsession == null )
                return DateTime.MinValue;

            var factory         = o2Gsession.getRequestFactory( );

            if ( factory == null )
            {
                throw new InvalidOperationException( );
            }

            var sameDate        = DateTime.MinValue;
            var o2Gtimeframe    = period.ToFxcmTimeFrame( factory.Timeframes );
            var request         = factory.createMarketDataSnapshotRequestInstrument( symbol, o2Gtimeframe, 300 );
            var dtFirst         = upperBound;
            var backwardFirst   = DateTime.MinValue;
            string prefixString = "----->";
            DateTime tickBar = DateTime.MinValue;

            try
            {
                var buffer = new List<Level1ChangeMessage>( );

                // cause there is limit for returned candles amount
                do
                {
                    // We are no longer in the Started State
                    if ( _canProcess != null && !_canProcess( false ) )
                    {
                        break;
                    }

                    backwardFirst = dtFirst;

                    if ( lowerBound >= dtFirst )
                    {
                        break;
                    }

                    factory.fillMarketDataSnapshotRequestTime( request, lowerBound, dtFirst, true, O2GCandleOpenPriceMode.PreviousClose );

                    if ( _pricelistener != null )
                    {
                        _pricelistener.SetRequestID( request.RequestID, period, symbol );
                    }

                    o2Gsession.sendRequest( request );

                    if ( _pricelistener != null )
                    {
                        _pricelistener.Wait3Mins( request.RequestID );
                    }

                    O2GMarketDataSnapshotResponseReader reader = null;
                    O2GResponse response = null;

                    if ( _pricelistener != null )
                    {
                        // shift "to" bound to oldest datetime of returned data
                        response = _pricelistener.GetResponse( request.RequestID );
                    }                        

                    if ( response != null && response.Type == O2GResponseType.MarketDataSnapshot )
                    {
                        O2GResponseReaderFactory readerFactory = o2Gsession.getResponseReaderFactory( );

                        if ( readerFactory != null )
                        {
                            reader = readerFactory.createMarketDataSnapshotReader( response );

                            if ( reader.Count > 0 )
                            {
                                // Check if the first returned bar is the same as the upper bound
                                if ( DateTime.Compare( dtFirst, reader.getDate( 0 ) ) != 0 )
                                {
                                    dtFirst = reader.getDate( 0 ); // earliest datetime of returned data

                                    if ( sameDate == DateTime.MinValue )
                                    {
                                        sameDate = reader.getDate( reader.Count - 1 );
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                LoggingHelper.AddWarningLog( this, "0 rows received" );
                                break;
                            }
                        }

                        for ( int index = reader.Count - 1; index >= 0; index-- )
                        {
                            if ( !reader.isBar )
                            {
                                tickBar = reader.getDate( index );
                                var utcTime = DateTime.SpecifyKind( tickBar, DateTimeKind.Utc );

                                var lvl1Msg                = new Level1ChangeMessage( );
                                lvl1Msg.SecurityId = msg.SecurityId;
                                lvl1Msg.ServerTime = utcTime;
                                lvl1Msg.LocalTime = utcTime;
                                lvl1Msg.IsReloadFromServer = true;

                                lvl1Msg.Add( Level1Fields.BestBidPrice, ( decimal ) reader.getBid( index ) );
                                lvl1Msg.Add( Level1Fields.BestAskPrice, ( decimal ) reader.getBid( index ) );

                                if ( tickBar > latestTickTime )
                                {
                                    latestTickTime = tickBar;
                                }

                                if ( sameDate.Date == tickBar.Date )
                                {
                                    buffer.Add( lvl1Msg );
                                }
                                else
                                {
                                    if ( buffer.Count > 0 )
                                    {
                                        var acending = buffer.OrderBy( x => x.ServerTime ).ToList( );

                                        foreach ( var tickMsg in acending )
                                        {
                                            SendOutMessage( tickMsg );
                                        }

                                        buffer.Clear( );
                                    }

                                    sameDate = tickBar;

                                    buffer.Add( lvl1Msg );
                                }
                            }
                        }

                        string message = string.Format( "{5}[{0}] ({1}) : Done Downlad [ {2:g} - {3:g} ] ..... Count = {4}", symbol, period.ToReadable(), dtFirst, backwardFirst, reader.Count, prefixString );

                        LoggingHelper.AddInfoLog( this, message );
                    }
                    else
                    {
                        var diff = dtFirst - lowerBound;
                        string message = string.Format( "{4}[{0}] Period : ({1}) !! No more data [ {2:g} - {3:g} ]", symbol, period.ToReadable(), lowerBound, dtFirst, prefixString );
                        ProcessErrors( request, symbol, period, diff, message );                        
                    }
                }
                while ( dtFirst > lowerBound );

                if ( buffer.Count > 0 )
                {
                    var acending = buffer.OrderBy( x => x.ServerTime ).ToList( );

                    foreach ( var tickMsg in acending )
                    {
                        SendOutMessage( tickMsg );
                    }

                    buffer.Clear( );
                }
            }
            catch ( Exception e )
            {
                LoggingHelper.AddErrorLog( this, e );
            }

            var mktDataMsg = new MarketDataMessage( );
            mktDataMsg.OriginalTransactionId = transactionId;

            SendOutMessage( mktDataMsg );

            return latestTickTime;
        }

        private void ProcessErrors( O2GRequest request, string symbol, TimeSpan period, TimeSpan diff, string message )
        {
            string eror = "";

            if ( _pricelistener != null )
            {
                eror = _pricelistener.GetErrorString( request.RequestID );
            }

            if ( !string.IsNullOrEmpty( eror ) )
            {
                if ( eror.ContainsIgnoreCase( "unsupported scope" ) )
                {
                    if ( diff > period )
                    {                        
                        LoggingHelper.AddWarningLog( this, message );
                    }
                }
                else
                {
                    LoggingHelper.AddWarningLog( this, eror );
                }                
            }
        }

        public void DownloadHistoricTicks( MarketDataMessage mdMsg )
        {
            var o2Gsession = GetSession( );

            if( o2Gsession == null )
                return;

            var factory    = o2Gsession.getRequestFactory( );

            if ( factory   == null )
            {
                throw new InvalidOperationException( );
            }

            DateTime dTo   = DateTime.MinValue;
            DateTime dFrom = DateTime.MinValue;

            if ( mdMsg.From == null && mdMsg.To == null )
            {
                dTo = DateTime.UtcNow;
                dFrom = dTo.AddMinutes( -5 );
            }
            else
            {
                dTo = mdMsg.To.HasValue ? mdMsg.To.GetValueOrDefault( ).UtcDateTime : DateTime.UtcNow;
                dFrom = mdMsg.From.HasValue ? mdMsg.From.GetValueOrDefault( ).UtcDateTime : factory.ZERODATE;
            }
            
            //ThreadHelper.UpdateThreadName( "DownloadHistoricTicks" );
                       

            var securityId          = mdMsg.SecurityId;
            var transId             = mdMsg.OriginalTransactionId;
            var symbol              = securityId.SecurityCode;
            var period              = TimeSpan.FromTicks( 1 ) ;
            var o2Gtimeframe        = period.ToFxcmTimeFrame( factory.Timeframes );
            var barCount            = ( int )( mdMsg.Count ?? 300L );
            var transactionId       = mdMsg.TransactionId;
            var lowerBound          = dFrom;
            var upperBound          = dTo;
            var latestTickTime      = lowerBound;
            var extendedInfo        = mdMsg.ExtensionInfo;
            bool isDownloadBackward = false;

            if ( extendedInfo != null )
            {
                if ( extendedInfo.ContainsKey( "DownloadBackward" ) )
                {
                    isDownloadBackward = true;
                }
            }

            string prefixString = "";

            if ( isDownloadBackward )
            {
                prefixString = "<<<<<<";
            }

            var hours = GetHourlyBreakdown( lowerBound, upperBound );

            foreach ( var hour in hours )
            {
                latestTickTime = DownloadHourlyTicks( period, symbol, securityId, transId, hour.Min, hour.Max, barCount, prefixString );
            }


            if ( !isDownloadBackward )
            {
                if ( !_candlesLiveDownload.ContainsKey( (symbol, period) ) )
                {
                    _candlesLiveDownload.Add( (symbol, period), (mdMsg, latestTickTime) );

                    if ( !_timerStarted )
                    {
                        StartTimer( );

                        _timerStarted = true;
                    }
                }                
            }

            SendSubscriptionFinished( mdMsg.TransactionId, new DateTimeOffset?( ) );            
        }

        private DateTime DownloadHourlyTicks( TimeSpan period, string symbol, SecurityId securityId, long transID, DateTime lowerBound, DateTime upperBound, int barCount, string prefixString )
        {
            var backwardFirst  = DateTime.MinValue;
            var o2Gsession     = GetSession( );

            if ( o2Gsession == null )
                return DateTime.MinValue;

            var factory        = o2Gsession.getRequestFactory( );
            var o2Gtimeframe   = period.ToFxcmTimeFrame( factory.Timeframes );
            var dtFirst        = upperBound;
            var sameDate       = DateTime.MinValue;
            DateTime tickTime   = DateTime.MinValue;
            var latestTickTime = lowerBound;

            if ( factory == null )
            {
                throw new InvalidOperationException( );
            }

            var request = factory.createMarketDataSnapshotRequestInstrument( symbol, o2Gtimeframe, barCount );

            try
            {
                List<AskBidBar> buffer = new List<AskBidBar>( );

                // cause there is limit for returned candles amount
                do
                {
                    // We are no longer in the Started State
                    if ( _canProcess != null && !_canProcess( false ) )
                    {
                        break;
                    }

                    backwardFirst = dtFirst;

                    if ( lowerBound >= dtFirst )
                    {
                        break;
                    }

                    if ( ( dtFirst - lowerBound ) < TimeSpan.FromSeconds( 1 ) )
                    {
                        break;
                    }

                    factory.fillMarketDataSnapshotRequestTime( request, lowerBound, dtFirst, true, O2GCandleOpenPriceMode.PreviousClose );
                    
                    if ( _pricelistener != null )
                    {
                        _pricelistener.SetRequestID( request.RequestID, period, symbol );
                    }

                    o2Gsession.sendRequest( request );

                    if ( _pricelistener != null )
                    {
                        _pricelistener.Wait3Mins( request.RequestID );
                    }

                    O2GMarketDataSnapshotResponseReader reader = null;


                    // shift "to" bound to oldest datetime of returned data
                    O2GResponse response = null;
                    if ( _pricelistener != null )
                    {
                        response = _pricelistener.GetResponse( request.RequestID );
                    }

                    if ( response != null && response.Type == O2GResponseType.MarketDataSnapshot )
                    {
                        O2GResponseReaderFactory readerFactory = o2Gsession.getResponseReaderFactory( );

                        if ( readerFactory != null )
                        {
                            reader = readerFactory.createMarketDataSnapshotReader( response );

                            if ( reader.Count > 0 )
                            {
                                // Check if the first returned bar is the same as the upper bound
                                if ( DateTime.Compare( dtFirst, reader.getDate( 0 ) ) != 0 )
                                {
                                    dtFirst = reader.getDate( 0 ); // earliest datetime of returned data

                                    if ( sameDate == DateTime.MinValue )
                                    {
                                        sameDate = reader.getDate( reader.Count - 1 );
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                LoggingHelper.AddWarningLog( this, "0 rows received" );
                                break;
                            }
                        }

                        

                        for ( int i = reader.Count - 1; i >= 0; i-- )
                        {
                            if ( !reader.isBar )
                            {
                                tickTime = reader.getDate( i );                                

                                var tmpBar = new AskBidBar( tickTime, reader.getBid( i ), reader.getAsk( i ) );                                

                                if ( tickTime > latestTickTime )
                                {
                                    latestTickTime = tickTime;
                                }

                                if ( sameDate.Date == tickTime.Date )
                                {
                                    buffer.Add( tmpBar );
                                }
                                else
                                {
                                    if ( buffer.Count > 0 )
                                    {
                                        var acending = buffer.OrderBy( x => x.BarTime ).ToList( );

                                        var lastPrice = acending[ 0 ].Price;

                                        foreach ( var tickBar in acending )
                                        {
                                            var tickMsg           = new ExecutionMessage( );
                                            tickMsg.SecurityId    = securityId;
                                            tickMsg.ExecutionType = ExecutionTypes.Tick;
                                            tickMsg.ServerTime    = tickBar.BarTime;
                                            tickMsg.TradePrice    = tickBar.Price;  
                                            
                                            if ( tickBar.Price > lastPrice )
                                            {
                                                tickMsg.IsUpTick = true;
                                            }
                                            else if ( tickBar.Price < lastPrice )
                                            {
                                                tickMsg.IsUpTick = false;
                                            }

                                            lastPrice = tickBar.Price;


                                            SendOutMessage( tickMsg );
                                        }

                                        buffer.Clear( );
                                    }

                                    sameDate = tickTime;

                                    //buffer.AddRange( notUsed );
                                    buffer.Add( tmpBar );
                                }
                            }
                        }
                    }
                    else
                    {
                        var diff = dtFirst - lowerBound;
                        string message = string.Format( "{4}[{0}] Period : ({1}) !! No more data [ {2:g} - {3:g} ]", symbol, period.ToReadable( ), lowerBound, dtFirst, prefixString );
                        ProcessErrors( request, symbol, period, diff, message );                        
                    }
                }
                while ( dtFirst > lowerBound );

                if ( buffer.Count > 0 )
                {
                    string message = string.Format( "{5}[{0}] ({1}) : Done Downlad TICK [ {2:g} - {3:g} ] ..... Count = {4}", symbol, period.ToReadable( ), buffer.First(), buffer.Last(), buffer.Count, prefixString );

                    LoggingHelper.AddInfoLog( this, message );

                    var acending = buffer.OrderBy( x => x.BarTime ).ToList( );

                    var lastPrice = acending[ 0 ].Price;

                    foreach ( var tickBar in acending )
                    {
                        var tickMsg = new ExecutionMessage( );
                        tickMsg.SecurityId = securityId;
                        tickMsg.ExecutionType = ExecutionTypes.Tick;
                        tickMsg.ServerTime = tickBar.BarTime;
                        tickMsg.TradePrice = tickBar.Price;

                        if ( tickBar.Price > lastPrice )
                        {
                            tickMsg.IsUpTick = true;
                        }
                        else if ( tickBar.Price < lastPrice )
                        {
                            tickMsg.IsUpTick = false;
                        }

                        lastPrice = tickBar.Price;

                        SendOutMessage( tickMsg );
                    }

                    buffer.Clear( );
                }                
            }
            catch ( Exception e )
            {
                LoggingHelper.AddErrorLog( this, e );
            }

            return latestTickTime;
        }

        private List<AskBidBar> BuildOneSecondBarFromTicks( string symbol, SecurityId secId, long transId, List<AskBidBar> tempBuffer )
        {
            var ticksNotUsed        = new List< AskBidBar >( );
            var count = 0;

            if ( tempBuffer.Count > 0 )
            {
                var targetBars        = new List< TickCandleMessage >( );
                var lastBarTime       = DateTime.MinValue;

                var targetBar         = tempBuffer[ 0 ].BarTime;
                var lastTargetBarTime = targetBar.AddTicks( -( targetBar.Ticks % TimeSpan.TicksPerSecond ) );

                TickCandleMessage newBar      = null;

                foreach ( var tickBar in tempBuffer )
                {
                    count++;

                    if ( tickBar.BarTime > ( lastTargetBarTime + TimeSpan.FromSeconds( 1 ) ) )
                    {
                        if ( newBar == null )
                        {
                            // need to round the tick to whole second.
                            var wholeSecond    = tickBar.BarTime.AddTicks( -( tickBar.BarTime.Ticks % TimeSpan.TicksPerSecond ) );

                            newBar                       = new TickCandleMessage( );
                            newBar.SecurityId            = secId;
                            newBar.OriginalTransactionId = transId;
                            newBar.OpenTime              = wholeSecond;
                            newBar.CloseTime             = wholeSecond;
                            newBar.OpenPrice             = tickBar.Price;
                            newBar.ClosePrice            = tickBar.Price;
                            newBar.HighPrice             = ( decimal ) tickBar.Ask;
                            newBar.LowPrice              = ( decimal ) tickBar.Bid;
                            lastBarTime                  = wholeSecond;
                            lastTargetBarTime            = wholeSecond;
                        }
                        else
                        {
                            if ( targetBars.Count == 0 || targetBars[ targetBars.Count - 1 ].CloseTime < newBar.OpenTime )
                            {
                                if ( newBar != null )
                                {
                                    newBar.State = CandleStates.Finished;
                                    targetBars.Add( newBar );
                                }

                                var wholeSecond   = tickBar.BarTime.AddTicks( -( tickBar.BarTime.Ticks % TimeSpan.TicksPerSecond ) );

                                newBar                       = new TickCandleMessage( );
                                newBar.SecurityId            = secId;
                                newBar.OriginalTransactionId = transId;
                                newBar.OpenTime              = wholeSecond;
                                newBar.CloseTime             = wholeSecond;
                                newBar.OpenPrice             = tickBar.Price;
                                newBar.ClosePrice            = tickBar.Price;
                                newBar.HighPrice             = ( decimal ) tickBar.Ask;
                                newBar.LowPrice              = ( decimal ) tickBar.Bid;
                                lastBarTime                  = wholeSecond;
                                lastTargetBarTime            = wholeSecond;
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {
                        if ( newBar == null )
                        {
                            // If we get here, that means the first bar is not divisible by the time frame. We need to construct the next lowest bar from the give time.
                            ticksNotUsed.Add( tickBar );
                            continue;
                        }
                        else
                        {
                            if ( ( tickBar.BarTime - lastBarTime ) <= TimeSpan.FromSeconds( 1 ) )
                            {
                                lastBarTime = tickBar.BarTime;

                                if ( (decimal) tickBar.Ask > newBar.HighPrice )
                                {
                                    newBar.HighPrice = ( decimal ) tickBar.Ask;
                                    newBar.HighTime = tickBar.BarTime;
                                }
                                
                                if ( ( decimal ) tickBar.Bid < newBar.LowPrice )
                                {
                                    newBar.LowPrice = ( decimal ) tickBar.Bid;
                                    newBar.LowTime = tickBar.BarTime;
                                }                                

                                newBar.ClosePrice = tickBar.Price;
                                newBar.CloseTime  = tickBar.BarTime;
                                
                            }
                        }
                    }
                }

                foreach ( var bar in targetBars )
                {
                    SendOutMessage( bar );
                }

                //WriteSecondBars2InfluxDbAsync( symbol, TimeSpan.FromSeconds( 1 ), targetBars );
            }

            return ticksNotUsed;
        }

        public IEnumerable<Range<DateTime>> GetHourlyBreakdown( DateTime startDate, DateTime endDate )
        {
            var hours = new List<Range<DateTime>>( );

            if ( ( endDate - startDate ) < TimeSpan.FromHours( 1 ) )
            {
                hours.Add( new Range<DateTime>( startDate, endDate ) );

                return hours;
            }

            var rangeBegin = startDate;
            var rangeEnd   = new DateTime( startDate.Year, startDate.Month, startDate.Day, startDate.Hour, 0, 0 ).AddHours( 1 ).AddSeconds( -1 );

            hours.Add( new Range<DateTime>( startDate, rangeEnd ) );

            rangeBegin = rangeEnd.AddSeconds( 1 );
            rangeEnd = rangeEnd.AddHours( 1 );


            while ( rangeEnd < endDate )
            {
                hours.Add( new Range<DateTime>( rangeBegin, rangeEnd ) );
                rangeBegin = rangeEnd.AddSeconds( 1 );
                rangeEnd = rangeEnd.AddHours( 1 );
            }



            hours.Add( new Range<DateTime>( rangeBegin, endDate ) );
            return hours;
        }

  //      private const string _eurusd = "eurusd";

		//private void SessionOnNewTrade(string pair, Trade trade)
		//{
		//	SendOutMessage(new ExecutionMessage
		//	{
		//		ExecutionType = ExecutionTypes.Tick,
		//		SecurityId = pair.ToStockSharp(),
		//		TradeId = trade.Id,
		//		TradePrice = (decimal)trade.Price,
		//		TradeVolume = (decimal)trade.Amount,
		//		ServerTime = trade.Time,
		//		OriginSide = trade.Type.ToSide(),
		//	});
		//}

		//private void SessionOnNewOrderBook(string pair, OrderBook book)
		//{
		//	SendOutMessage(new QuoteChangeMessage
		//	{
		//		SecurityId = pair.ToStockSharp(),
		//		Bids = book.Bids.Select(e => new QuoteChange(e.Price, e.Size)).ToArray(),
		//		Asks = book.Asks.Select(e => new QuoteChange(e.Price, e.Size)).ToArray(),
		//		ServerTime = book.Time,
		//	});
		//}

		//private void SessionOnNewOrderLog(string pair, OrderStates state, Order order)
		//{
		//	SendOutMessage(new ExecutionMessage
		//	{
		//		ExecutionType = ExecutionTypes.OrderLog,
		//		SecurityId = pair.ToStockSharp(),
		//		ServerTime = order.Time,
		//		OrderVolume = (decimal)order.Amount,
		//		OrderPrice = (decimal)order.Price,
		//		OrderId = order.Id,
		//		Side = order.Type.ToSide(),
		//		OrderState = state,
		//	});
		//}

		//private void ProcessMarketData(MarketDataMessage mdMsg)
		//{
		//	if (!mdMsg.SecurityId.IsAssociated())
		//	{
		//		SendSubscriptionNotSupported(mdMsg.TransactionId);
		//		return;
		//	}

		//	var currency = mdMsg.SecurityId.ToCurrency();

		//	if (mdMsg.DataType2 == DataType.OrderLog)
		//	{
		//		if (mdMsg.IsSubscribe)
		//			_pusherClient.SubscribeOrderLog(currency);
		//		else
		//			_pusherClient.UnSubscribeOrderLog(currency);
		//	}
		//	else if (mdMsg.DataType2 == DataType.MarketDepth)
		//	{
		//		if (mdMsg.IsSubscribe)
		//			_pusherClient.SubscribeOrderBook(currency);
		//		else
		//			_pusherClient.UnSubscribeOrderBook(currency);
		//	}
		//	else if (mdMsg.DataType2 == DataType.Ticks)
		//	{
		//		if (mdMsg.IsSubscribe)
		//		{
		//			if (mdMsg.To != null)
		//			{
		//				SendSubscriptionReply(mdMsg.TransactionId);

		//				var diff = DateTimeOffset.Now - (mdMsg.From ?? DateTime.Today);

		//				string interval;

		//				if (diff.TotalMinutes < 1)
		//					interval = "minute";
		//				else if (diff.TotalDays < 1)
		//					interval = "hour";
		//				else
		//					interval = "day";

		//				var trades = _httpClient.RequestTransactions(currency, interval);

		//				foreach (var trade in trades.OrderBy(t => t.Time))
		//				{
		//					SendOutMessage(new ExecutionMessage
		//					{
		//						ExecutionType = ExecutionTypes.Tick,
		//						SecurityId = mdMsg.SecurityId,
		//						TradeId = trade.Id,
		//						TradePrice = (decimal)trade.Price,
		//						TradeVolume = trade.Amount.ToDecimal(),
		//						ServerTime = trade.Time,
		//						OriginSide = trade.Type.ToSide(),
		//						OriginalTransactionId = mdMsg.TransactionId
		//					});
		//				}

		//				SendSubscriptionResult(mdMsg);
		//				return;
		//			}
		//			else
		//				_pusherClient.SubscribeTrades(currency);
		//		}
		//		else
		//		{
		//			_pusherClient.UnSubscribeTrades(currency);
		//		}
		//	}
		//	else
		//	{
		//		SendSubscriptionNotSupported(mdMsg.TransactionId);
		//		return;
		//	}

		//	SendSubscriptionReply(mdMsg.TransactionId);
		//}

        
        
    }
}