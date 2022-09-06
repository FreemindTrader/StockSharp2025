using fxcore2;
using StockSharp.FxConnectFXCM.Freemind;
using System;
using System.Collections.Generic; 
using System.Globalization;
using System.Text;

namespace StockSharp.FxConnectFXCM.MainController
{
}
//    class FxOfferController : BaseController, IOfferController, IO2GResponseListener
//    {        
//        private O2GTimeConverter                               _timezoneConverter;                                 // Time zone converter 
//        private int                                            _tradingDayOffset;                                  // The offet in hours of when the trading day start in relation to the midnight     
//        private string                                         _mainLoginName;
//        private bool                                           _isMainDataSource = false;

//        public event ItemEventHandler<IOffer>                 OfferUpdatedEvent;

//        public FxOfferController( string mainLoginName, O2GSession sessionID ) : base( sessionID )
//        {
//            _mainLoginName = mainLoginName;                                   
//            _fxSessionId.subscribeResponse( this );            
//        }
//        
//        ~FxOfferController( )
//        {
//            Unsubscribe( );
//        }

//        public void Unsubscribe( )
//        {
//            _fxSessionId?.unsubscribeResponse( this );
//        }

//        public bool InitializeTimeZone( )
//        {
//            _timezoneConverter = _fxSessionId.getTimeConverter( );

//            var response       = _loginRules.getSystemPropertiesResponse( );

//            var reader         = _fxSessionId.getResponseReaderFactory( ).createSystemPropertiesReader( response );

//            string eod         = reader.Properties[ "END_TRADING_DAY" ];

//            var time           = DateTime.ParseExact( "01.01.1900_" + eod, "MM.dd.yyyy_HH:mm:ss", CultureInfo.InvariantCulture );

//            // convert Trading day start to EST time because the trading day is always closed by New York time
//            // so to avoid handling different hour depending on daylight saying time - use EST always
//            // for candle calculations

//            time = _timezoneConverter.convert( time, O2GTimeConverterTimeZone.UTC, O2GTimeConverterTimeZone.EST );

//            // here we have the date when trading day begins, e.g. 17:00:00
//            // please note that if trading day begins before noon - it begins AFTER calendar date is started,
//            // so the offset is positive (e.g. 03:00 is +3 offset).
//            // if trading day begins after noon, it begins BEFORE calendar date is istarted,
//            // so the offset is negative (e.g. 17:00 is -7 offset).

//            if ( time.Hour <= 12 )
//            {
//                _tradingDayOffset = time.Hour;
//            }
//            else
//            {
//                _tradingDayOffset = time.Hour - 24;
//            }

//            return true;
//        }

//        


//        private void GetSubscribedSymbolsOnly( string accountName, bool weekendMode )
//        {
//            List< fxSymbol > result = new List< fxSymbol >();

//            bool waitSuccess = false;

//            if ( !weekendMode )
//            {
//                StartReadingOffers( );

//                waitSuccess = Wait( 60 );
//            }
//            else
//            {
//                waitSuccess = true;
//            }

//            if ( waitSuccess )
//            {
//                bool found = false;
//                bool foundNew = false;

//                var subscribedSymbols = SymbolsMgr.Instance.GetSubscribedSymbolsByAccountNam( accountName );

//                using ( var context = new ForexDatabars( ) )
//                {
//                    var databaseSymbols =    from b in context.SUBSCRIBEDSYMBOL
//                                             where b.AccountName == accountName
//                                             select b;

//                    if ( weekendMode )
//                    {
//                        foreach ( var symbolInDatabase in databaseSymbols )
//                        {
//                            subscribedSymbols.Add( symbolInDatabase );
//                        }
//                    }
//                    else
//                    {


//                        var removedList = new List< DBSubscribedSymbols >();


//                        foreach ( var symbolInDatabase in databaseSymbols )
//                        {
//                            found = false;

//                            foreach ( var currentSubscribedSymbol in subscribedSymbols )
//                            {
//                                if ( symbolInDatabase.OfferID == currentSubscribedSymbol.OfferID )
//                                {
//                                    found = true;
//                                    break;
//                                }

//                            }

//                            // If we get here, that mean we have some symbol in database that is no longer subscribed.

//                            if ( !found ) removedList.Add( symbolInDatabase );
//                        }

//                        foreach ( var subscribedSymbol in subscribedSymbols )
//                        {


//                            var existedInDatabase =    from b in context.SUBSCRIBEDSYMBOL
//                                                       where b.AccountName == subscribedSymbol.AccountName && b.OfferID == subscribedSymbol.OfferID
//                                                       select b;

//                            if ( !existedInDatabase.Any( ) )
//                            {
//                                var newSymbol          = context.SUBSCRIBEDSYMBOL.Create();

//                                newSymbol.OfferID = subscribedSymbol.OfferID;
//                                newSymbol.AccountName = subscribedSymbol.AccountName;
//                                newSymbol.BaseUnitSize = subscribedSymbol.BaseUnitSize;

//                                foundNew = true;

//                                context.SUBSCRIBEDSYMBOL.Add( newSymbol );
//                            }
//                            else
//                            {
//                                var newSymbol          = existedInDatabase.FirstOrDefault( );
//                                newSymbol.OfferID = subscribedSymbol.OfferID;
//                                newSymbol.AccountName = subscribedSymbol.AccountName;

//                                if ( newSymbol.BaseUnitSize != subscribedSymbol.BaseUnitSize )
//                                {
//                                    newSymbol.BaseUnitSize = subscribedSymbol.BaseUnitSize;
//                                    foundNew = true;
//                                }

//                            }
//                        }

//                        foreach ( var tobeRemoved in removedList )
//                        {
//                            context.SUBSCRIBEDSYMBOL.Remove( tobeRemoved );
//                        }


//                        try
//                        {
//                            if ( foundNew ) context.SaveChanges( );
//                        }
//                        catch ( DbException ex )
//                        {
//                            SystemMonitor.Error( ex.ErrorMessage );
//                        }
//                    }

//                }

//                if ( subscribedSymbols.Count > 0 )
//                {



//                }
//            }

//            StopReadingOffers( _isMainDatasource );
//        }

//        private void GetSubscribedSymbolsAndStartStreaming( string accountName )
//        {
//            List< fxSymbol > result = new List< fxSymbol >();

//            StartLiveOfferStreaming( );

//            if ( Wait( 60 ) )
//            {
//                bool found = false;
//                bool foundNew = false;

//                var subscribedSymbols = SymbolsMgr.Instance.GetSubscribedSymbolsByAccountNam( accountName );

//                using ( var context = new ForexDatabars( ) )
//                {
//                    var databaseSymbols =    from b in context.SUBSCRIBEDSYMBOL
//                                             where b.AccountName == accountName
//                                             select b;


//                    var removedList = new List< DBSubscribedSymbols >();


//                    foreach ( var symbolInDatabase in databaseSymbols )
//                    {
//                        found = false;

//                        foreach ( var currentSubscribedSymbol in subscribedSymbols )
//                        {
//                            if ( symbolInDatabase.OfferID == currentSubscribedSymbol.OfferID )
//                            {
//                                found = true;
//                                break;
//                            }

//                        }

//                        // If we get here, that mean we have some symbol in database that is no longer subscribed.

//                        if ( !found ) removedList.Add( symbolInDatabase );
//                    }

//                    foreach ( var subscribedSymbol in subscribedSymbols )
//                    {


//                        var existedInDatabase =    from b in context.SUBSCRIBEDSYMBOL
//                                                   where b.AccountName == subscribedSymbol.AccountName && b.OfferID == subscribedSymbol.OfferID
//                                                   select b;

//                        if ( !existedInDatabase.Any( ) )
//                        {
//                            var newSymbol          = context.SUBSCRIBEDSYMBOL.Create();

//                            newSymbol.OfferID = subscribedSymbol.OfferID;
//                            newSymbol.AccountName = subscribedSymbol.AccountName;
//                            newSymbol.BaseUnitSize = subscribedSymbol.BaseUnitSize;

//                            foundNew = true;

//                            context.SUBSCRIBEDSYMBOL.Add( newSymbol );
//                        }
//                        else
//                        {
//                            var newSymbol          = existedInDatabase.FirstOrDefault( );
//                            newSymbol.OfferID = subscribedSymbol.OfferID;
//                            newSymbol.AccountName = subscribedSymbol.AccountName;

//                            if ( newSymbol.BaseUnitSize != subscribedSymbol.BaseUnitSize )
//                            {
//                                newSymbol.BaseUnitSize = subscribedSymbol.BaseUnitSize;
//                                foundNew = true;
//                            }

//                        }
//                    }

//                    foreach ( var tobeRemoved in removedList )
//                    {
//                        context.SUBSCRIBEDSYMBOL.Remove( tobeRemoved );
//                    }


//                    try
//                    {
//                        if ( foundNew ) context.SaveChanges( );
//                    }
//                    catch ( DbException ex )
//                    {
//                        SystemMonitor.Error( ex.ErrorMessage );
//                    }

//                }

//                if ( subscribedSymbols.Count > 0 )
//                {



//                }
//            }
//        }

//        protected void StartLiveOfferStreaming( )
//        {
//            O2GResponse response;

//            if ( _loginRules.isTableLoadedByDefault( O2GTableType.Offers ) )
//            {
//                // if it is already loaded - just handle them
//                response = _loginRules.getTableRefreshResponse( O2GTableType.Offers );

//                onRequestCompleted( null, response );
//            }
//            else
//            {
//                // otherwise create the request to get offers from the server
//                O2GRequestFactory factory = _fxSessionId.getRequestFactory( );
//                O2GRequest offerRequest   = factory.createRefreshTableRequest( O2GTableType.Offers );

//                _fxSessionId.sendRequest( offerRequest );
//            }
//        }


//        protected void StartReadingOffers( )
//        {
//            O2GResponse response;

//            if ( _loginRules.isTableLoadedByDefault( O2GTableType.Offers ) )
//            {
//                // if it is already loaded - just handle them
//                response = _loginRules.getTableRefreshResponse( O2GTableType.Offers );

//                onRequestCompleted( null, response );
//            }
//            else
//            {
//                // otherwise create the request to get offers from the server
//                O2GRequestFactory factory = _fxSessionId.getRequestFactory( );
//                O2GRequest offerRequest   = factory.createRefreshTableRequest( O2GTableType.Offers );

//                _fxSessionId.sendRequest( offerRequest );
//            }
//        }
//        
//        public bool SubscribeSymbol( string symbolName )
//        {
//            int offerId = SymbolsMgr.Instance.GetOfferId( symbolName );

//            if ( offerId > -1 )
//            {
//                _subscribedSymbolsToId.TryAddOrReplace( symbolName, offerId );
//            }

//            return true;
//        }


//        public List<DBSubscribedSymbols> GetTradableSymbols( string accountName )
//        {
//            var symbolsOffer = SymbolsMgr.Instance.GetSubscribedSymbolsByAccountNam( accountName );

//            return symbolsOffer;
//        }


//        public void onRequestCompleted( string requestId, O2GResponse response )
//        {
//            // we need only offer table refresh for our example
//            if ( response.Type == O2GResponseType.GetOffers )
//            {
//                var factory = _fxSessionId.getResponseReaderFactory( );

//                if ( factory != null )
//                {
//                    O2GOffersTableResponseReader reader = factory.createOffersTableReader( response );

//                    _offersCollection.Clear( );

//                    var symbolsOffer                    = SymbolsMgr.Instance.GetSymbolsRepo( );

//                    var subscribedSymbol = SymbolsMgr.Instance.GetSubscribedSymbolsByAccountNam( _mainLoginName );

//                    SymbolsMgr.Instance.CreateOfferCache( reader.Count );

//                    for ( int i = 0; i < reader.Count; i++ )
//                    {
//                        var row = reader.getRow( i );

//                        var instrumentOffers = new DbSymbolsInfo( row.OfferID, row.Instrument, row.BuyInterest, row.SellInterest, row.Digits, row.PointSize, row.SubscriptionStatus, row.TradingStatus, row.InstrumentType, row.PointSize * 50 );

//                        SymbolsMgr.Instance.UpdateOfferCache( int.Parse( row.OfferID ), row.Bid, row.Ask );

//                        if ( row.SubscriptionStatus == "T" && row.isInstrumentValid && row.isLowValid && row.isHighValid && row.isAskValid && row.isBidValid && row.isPointSizeValid )
//                        {
//                            subscribedSymbol.Add( new DBSubscribedSymbols( row.OfferID, _mainLoginName ) );


//                            if ( _isMainDatasource )
//                            {
//                                var sequencer               = SymbolsMgr.Instance.GetOfferRingBuffer( row.Instrument );

//                                long sequence               = sequencer.Next( );

//                                try
//                                {
//                                    FxOffer emptyOffer      = sequencer.ClaimAndGetPreallocated( sequence );

//                                    if ( row.Bid > 2 && row.Instrument == "EUR/USD" )
//                                    {
//                                        throw new NotImplementedException( );
//                                    }

//                                    emptyOffer.CopyFrom( row.Instrument, row.Time, row.Bid, row.Ask, row.Volume, row.Digits );
//                                }
//                                finally
//                                {
//                                    sequencer.Publish( sequence );
//                                }
//                            }

//                        }


//                        symbolsOffer.Add( instrumentOffers );
//                    }
//                }

//                SaveAvailableSymbolsToDatabase( );



//                _syncEventReceived.Set( );
//            }
//        }


//        private void StopReadingOffers( bool isMainDatasource )
//        {
//            if ( !isMainDatasource )
//            {
//                _fxSessionId?.unsubscribeResponse( this );
//            }
//        }

//        #region IPriceUpdateController methods

//        /// <summary>
//        /// Converts NYT to UTC
//        /// </summary>
//        /// <param name="time"></param>
//        /// <returns></returns>
//        public DateTime EstToUtc( DateTime time )
//        {
//            return _timezoneConverter.convert( time, O2GTimeConverterTimeZone.EST, O2GTimeConverterTimeZone.UTC );
//        }

//        /// <summary>
//        /// Converts UTC to NYT
//        /// </summary>
//        /// <param name="time"></param>
//        /// <returns></returns>
//        public DateTime UtcToEst( DateTime time )
//        {
//            return _timezoneConverter.convert( time, O2GTimeConverterTimeZone.UTC, O2GTimeConverterTimeZone.EST );
//        }

//        /// <summary>
//        /// Gets the trading day offset
//        /// </summary>
//        public int TradingDayOffset
//        {
//            get { return _tradingDayOffset; }
//        }

//        public string MainLoginName
//        {
//            get
//            {
//                return _mainLoginName;
//            }

//            set
//            {
//                _mainLoginName = value;
//            }
//        }

//        #endregion

//        /// <summary>
//        /// Listener: Forex Connect Request failed.
//        /// </summary>
//        /// <param name="requestId"></param>
//        /// <param name="error"></param>
//        public void onRequestFailed( string requestId, string error )
//        {
//        }

//        /// <summary>
//        /// Listener: Forex Connect received update for trading tables.
//        /// </summary>
//        /// <param name="data"></param>
//        public void onTablesUpdates( O2GResponse data )
//        {
//            if ( !_isMainDataSource ) return;

//            if ( data.Type == O2GResponseType.TablesUpdates )
//            {
//                var factory = _fxSessionId.getResponseReaderFactory( );

//                if ( factory == null )
//                {
//                    return;
//                }

//                O2GTablesUpdatesReader reader = factory.createTablesUpdatesReader( data );

//                for ( int i = 0; i < reader.Count; i++ )
//                {
//                    // We are looking only for updates and only for offers

//                    // NOTE: in order to support offer subscribtion, the real application also will need 
//                    // to read add/remove events and change the offer collection correspondingly
//                    if (
//                            reader.getUpdateType( i ) == O2GTableUpdateType.Update &&
//                            reader.getUpdateTable( i ) == O2GTableType.Offers
//                       )
//                    {
//                        // read the offer update
//                        O2GOfferRow row    = reader.getOfferRow( i );

//                        // find the offer in our list either by the instrument name or 
//                        // by the offer id
//                        string instrument  = null;

//                        if ( row.isInstrumentValid )
//                        {
//                            instrument = row.Instrument;
//                        }
//                        else
//                        {
//                            if ( row.isOfferIDValid )
//                            {
//                                instrument = row.OfferID;
//                            }
//                        }


//                        if ( instrument == null )
//                        {
//                            continue;
//                        }

//                        SymbolsMgr.Instance.UpdateOfferCache( int.Parse( row.OfferID ), row.Bid, row.Ask );

//                        // if ( row.SubscriptionStatus == "T" && row.isInstrumentValid && row.isLowValid && row.isHighValid && row.isAskValid && row.isBidValid && row.isPointSizeValid )
//                        if ( row.isInstrumentValid && row.isLowValid && row.isHighValid && row.isAskValid && row.isBidValid && row.isPointSizeValid )
//                        {


//                            var sequencer               = SymbolsMgr.Instance.GetOfferRingBuffer( row.Instrument );

//                            long sequence               = sequencer.Next( );

//                            try
//                            {
//                                FxOffer emptyOffer      = sequencer.ClaimAndGetPreallocated( sequence );

//                                if ( row.Instrument == "EUR/USD" )
//                                {
//                                    if ( row.Bid > 2 || row.Ask > 2 )
//                                    {
//                                        throw new NotImplementedException( );
//                                    }
//                                }

//                                //emptyOffer.CopyFrom( row.Instrument, row.Time, row.Bid, row.Ask, row.Volume, row.Digits );

//                                emptyOffer.Instrument = row.Instrument;
//                                emptyOffer.LastUpdate = row.Time;
//                                emptyOffer.Bid = row.Bid;
//                                emptyOffer.Ask = row.Ask;
//                                emptyOffer.MinuteVolume = row.Volume;
//                                emptyOffer.Digits = row.Digits;

//                                if ( ( emptyOffer.Instrument != row.Instrument ) || ( emptyOffer.Bid != row.Bid ) || ( emptyOffer.Ask != row.Ask ) || ( emptyOffer.MinuteVolume != row.Volume ) )
//                                {
//                                    //throw new NotImplementedException( );
//                                }

//                            }
//                            finally
//                            {
//                                sequencer.Publish( sequence );
//                            }

//                        }
//                    }
//                }
//            }
//        }

//        public bool StartWork( bool isMainDataSource, bool weekendMode )
//        {
//            _loginRules = _fxSessionId.getLoginRules( );

//            if ( !weekendMode )
//            {
//                InitializeTimeZone( );
//            }

//            GetTradableSymbols( _mainLoginName );

//            _isMainDatasource = isMainDataSource;

//            if ( isMainDataSource && !weekendMode )
//            {
//                GetSubscribedSymbolsAndStartStreaming( _mainLoginName );
//            }
//            else
//            {
//                GetSubscribedSymbolsOnly( _mainLoginName, weekendMode );
//            }

//            return true;
//        }

//        public void StopWork( )
//        {
//            throw new NotImplementedException( );
//        }

//        public void Dispose( )
//        {
//            throw new NotImplementedException( );
//        }
//    }
//}
