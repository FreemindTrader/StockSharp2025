using Ecng.Collections;
using Ecng.Common;
using fxcore2;
using StockSharp.Messages;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.FxConnectFXCM.Freemind
{
    public static class GFMgr
    {
        private static volatile CancellationTokenSource _globalExitSource = new CancellationTokenSource();

        public static CancellationToken GlobalExitToken( )
        {
            return _globalExitSource.Token;
        }

        private readonly static List< DbSymbolsInfo > SymbolsOffer                                                                      = new List< DbSymbolsInfo >( 65 );

        private static SynchronizedDictionary< int, FxBidAsk >                               _offerCache                                = new SynchronizedDictionary< int, FxBidAsk >(   );               

        private readonly static SynchronizedDictionary< string, SubaccountTradeDataRepo >    _tradeDataOfSubaccount                     = new SynchronizedDictionary< string, SubaccountTradeDataRepo >( 5 );
        private readonly static SynchronizedDictionary< string,  OverLeverageStatus >        _overLeverageAccounts                      = new SynchronizedDictionary< string,  OverLeverageStatus >( );

        private static List< DbAccounts >                                                    _accountsBindingList                       = new List< DbAccounts >( );

        private static List<DetailedOrderDB>                                                 _detailedOrdersBindingList                 = new List<DetailedOrderDB>( );
        private static List<FxDetailedPosition>                                              _detailedPositionsBindingList              = new List<FxDetailedPosition>();

        private static List<FxOpenPositionAndOrders>                                         _fxOpenPositionPendingOrderInfoBindingList = new List<FxOpenPositionAndOrders>();

        private static List<FxPositionsSummary>                                              _fxPositionsSummaryBindingList             = new List<FxPositionsSummary>( );
        private static List<FxAccountSummary>                                                _accountsSummaryBindingList                = new List<FxAccountSummary>();

        private readonly static SynchronizedDictionary< string, MainLoginToSubAccountsRepo > _subaccountsOfMainLoginName                = new SynchronizedDictionary< string, MainLoginToSubAccountsRepo >( 5 );
        private readonly static SynchronizedDictionary< string, List<DBSubscribedSymbols>>   _subscribedSymbolsOfSubaccount             = new SynchronizedDictionary< string, List< DBSubscribedSymbols > >( 5 );

        
        public static List<DbSymbolsInfo> GetSymbolsRepo( )
        {
            return SymbolsOffer;
        }

        public static void CreateOfferCache( int count )
        {
            _offerCache = new SynchronizedDictionary<int, FxBidAsk>( count );
        }

        public static FxPositionsSummary GetPositionSummaryBindingListByAccountNameAndSymbol( string accountName, string symbol )
        {
            var positionSummary = _fxPositionsSummaryBindingList.ToList();

            foreach ( var posSummary in positionSummary )
            {
                if ( posSummary.AccountName == accountName && posSummary.Symbol == symbol )
                {
                    return posSummary;
                }
            }


            return null;
        }

        public static MainLoginToSubAccountsRepo GetOrCreateSubAccountsRepo( string mainLoginName )
        {
            MainLoginToSubAccountsRepo output = null;

            if ( _subaccountsOfMainLoginName.TryGetValue( mainLoginName, out output ) )
            {
                return output;
            }

            var accountRepo = new MainLoginToSubAccountsRepo( mainLoginName );

            _subaccountsOfMainLoginName.Add( mainLoginName, accountRepo );

            return accountRepo;

        }

        public static void UpdateOfferCache( int offerId, double bid, double ask )
        {

            FxBidAsk newOffer = new FxBidAsk( bid, ask );
            _offerCache[ offerId ] = newOffer;

        }

        public static List<DBSubscribedSymbols> GetSubscribedSymbolsByAccountName( string accountName )
        {
            List< DBSubscribedSymbols > output = null;

            if ( _subscribedSymbolsOfSubaccount.TryGetValue( accountName, out output ) )
            {
                return output;
            }

            var susbcribedSymbols = new List< DBSubscribedSymbols >();

            _subscribedSymbolsOfSubaccount.Add( accountName, susbcribedSymbols );

            return susbcribedSymbols;
        }
        public static DBSubscribedSymbols GetSubscribedSymbolsByAccountNameAndId(string accountName, int offerId )
        {
            List< DBSubscribedSymbols > output = null;

            if ( _subscribedSymbolsOfSubaccount.TryGetValue( accountName, out output ) )
            {
                int index = output.FindIndex( x => x.OfferID == offerId );

                if ( index > -1 )
                {
                    return output[ index ];
                }
            }

            return null;
        }

        

        

        public static bool HasSmartMargin( string leverageProfileID )
        {
            bool output = true;

            if ( leverageProfileID == "31136" )
            {
                output = false;
            }
            else if ( leverageProfileID == "3203" )
            {
                output = true;
            }
            else if ( leverageProfileID == "2401" )
            {
                output = false;
            }
            else if ( leverageProfileID == "5141" )
            {
                output = true;
            }
            else if ( leverageProfileID == "967" )
            {
                output = true;
            }

            return output;
        }

        public static double GetPipCost( string instrument )
        {
            double pipCost = 0d;

            var linqResult = from a in SymbolsOffer where a.Instrument == instrument select a;

            if ( linqResult.Any( ) )
            {
                pipCost = linqResult.First( ).PipCost;
            }

            return pipCost;
        }



        public static int GetInstrumentDigits( string instrument )
        {
            int i = 0;

            var linqResult = from a in SymbolsOffer where a.Instrument == instrument select a;

            if ( linqResult.Any( ) )
            {
                i = linqResult.First( ).Digits;
            }

            return ( i );
        }

        public static double GetInstrumentPointSize( string instrument )
        {
            double i = 0;

            var linqResult = from a in SymbolsOffer
                             where a.Instrument == instrument
                             select a;

            if ( linqResult.Any( ) )
            {
                i = linqResult.First( ).PointSize;
            }

            return ( i );
        }

        public static int GetOfferId( string instrument )
        {
            int i = 0;

            var linqResult = from a in SymbolsOffer where a.Instrument == instrument select a;

            if ( linqResult.Any( ) )
            {
                string offerId = linqResult.First( ).OfferID;
                i = int.Parse( offerId );
            }

            return ( i );
        }

        public static string GetSymbolFromOfferId( string offerId )
        {
            string symbol = "NoFound";

            var linqResult = from a in SymbolsOffer where a.OfferID == offerId select a;

            if ( linqResult.Any( ) )
            {
                symbol = linqResult.First( ).Instrument;
            }

            return symbol;
        }


        public static DbAccounts GetAccountInfoByAccountName( string accountName )
        {
            var accounts = _accountsBindingList;

            foreach ( var account in accounts )
            {
                if ( account.MainLoginName == accountName )
                {
                    return account;
                }
            }

            return null;
        }

        public static string GetAccountIdFromName( string mainLoginName, string accountName )
        {
            MainLoginToSubAccountsRepo subAccountsRepo = null;

            if ( _subaccountsOfMainLoginName.TryGetValue( mainLoginName, out subAccountsRepo ) )
            {
                return ( subAccountsRepo.DetailedAccountsCollection.FindAccountIdByName( accountName ) );
            }

            return string.Empty;
        }

        public static List<DetailedOrderDB> GetDetailedOrdersBindingList( )
        {
            return _detailedOrdersBindingList;
        }

        public static void SetDetailedOrderBindingList( List<DetailedOrderDB> newOne )
        {
            _detailedOrdersBindingList = newOne;
        }


        public static List<FxDetailedPosition> GetDetailedPositionsBindingList( )
        {
            return _detailedPositionsBindingList;
        }

        public static List<FxOpenPositionAndOrders> GetOpenPositionAndOrdersBindingList( )
        {
            return _fxOpenPositionPendingOrderInfoBindingList;
        }

        public static List<FxPositionsSummary> GetPositionSummaryBindingList( )
        {
            return _fxPositionsSummaryBindingList;
        }

        public static void SetPositionSummaryBindingList( List<FxPositionsSummary> newOne )
        {
            _fxPositionsSummaryBindingList = newOne;
        }

        public static void SetOpenPositionAndOrdersBindingList( List<FxOpenPositionAndOrders> newList )
        {
            _fxOpenPositionPendingOrderInfoBindingList = newList;
        }

        public static List<FxAccountSummary> GetAccountSummaryBindingList( )
        {
            return _accountsSummaryBindingList;
        }

        public static SubaccountTradeDataRepo GetSubaccountTradeDataRepoByName( string accountName )
        {
            SubaccountTradeDataRepo result = null;
            if ( _tradeDataOfSubaccount.TryGetValue( accountName, out result ) )
            {
                return result;
            }

            return null; ;
        }

        public static void ProcessDangerouslyOverLeverage( string accountName, AccountSummaryCalculatedValue calcValue )
        {
            if ( !_overLeverageAccounts.TryAdd2( accountName, OverLeverageStatus.DANGEROUS ) )
            {
                Task detectTask  = new Task(
                                                () =>
                                                {
                                                    ProcessDangerouslyOverLeverageTask( accountName, calcValue );

                                                }, GlobalExitToken()
                                        );

                detectTask.Start( );
            }
            else
            {
                var status = _overLeverageAccounts [ accountName ];

                if ( status == OverLeverageStatus.PROCESSING || status == OverLeverageStatus.PROCESSED )
                {
                    return;
                }

                _overLeverageAccounts[ accountName ] = OverLeverageStatus.DANGEROUS;
            }
        }

        private static void ProcessDangerouslyOverLeverageTask( string accountName, AccountSummaryCalculatedValue calcValue )
        {

        }

        public static void ProcessOverLeverage( string accountName, AccountSummaryCalculatedValue calcValue )
        {
            if ( !_overLeverageAccounts.TryAdd2( accountName, OverLeverageStatus.WARNING ) )
            {
                Task detectTask  = new Task(
                                                () =>
                                                {
                                                    ProcessDangerouslyOverLeverageTask( accountName, calcValue );

                                                }, GlobalExitToken()
                                        );

                detectTask.Start( );
            }
            else
            {
                var status = _overLeverageAccounts [ accountName ];

                if ( status == OverLeverageStatus.PROCESSING || status == OverLeverageStatus.PROCESSED )
                {
                    return;
                }

                _overLeverageAccounts[ accountName ] = OverLeverageStatus.WARNING;
            }
        }

        public static SubaccountTradeDataRepo CreateSubaccountTradeDataRepoByAccountName( string mainLoginName, string accountName )
        {
            SubaccountTradeDataRepo result = null;

            if ( _tradeDataOfSubaccount.TryGetValue( accountName, out result ) )
            {
                return result;
            }

            SubaccountTradeDataRepo openPositionManager = new SubaccountTradeDataRepo( mainLoginName );

            if ( _tradeDataOfSubaccount.TryAdd2( accountName, openPositionManager ) )
            {
                return openPositionManager;
            }
            else
            {
                if ( _tradeDataOfSubaccount.TryGetValue( accountName, out result ) )
                {
                    return result;
                }
            }

            throw new KeyNotFoundException( );
        }

        private static List<FxOrdersForNewPosition> _ordersForNewPositionBindingList     = new List<FxOrdersForNewPosition>( );
        public static List<FxOrdersForNewPosition> GetOrdersForNewPositionBindingList( )
        {
            return _ordersForNewPositionBindingList;
        }

        public static void SetOrdersForNewPositionBindingList( List<FxOrdersForNewPosition> newOne )
        {
            _ordersForNewPositionBindingList = newOne;
        }

        public static FxBidAsk GetOffer( int offerId )
        {
            return _offerCache.TryGetValue( offerId );
        }

        public static FxBidAsk GetOffer( string symbol )
        {
            return GetOffer( GetOfferId( symbol ) );
        }

        public static List<FxOrdersForNewPosition> GetOrdersForNewPositionBindingListByAccountName( string accountName )
        {
            var output = new List<FxOrdersForNewPosition>();

            var orders = _ordersForNewPositionBindingList.ToList();

            foreach ( var order in orders )
            {
                if ( order.AccountName == accountName )
                {
                    output.Add( order );
                }
            }

            return output;
        }

        public static FxOrderType GetOrderTypeEnum( string orderType )
        {
            FxOrderType sOrderType = FxOrderType.Unknown;

            if ( orderType == "S" )
            {
                sOrderType = FxOrderType.Stop;
            }
            else if ( orderType == "ST" )
            {
                sOrderType = FxOrderType.TrailingStop;
            }
            else if ( orderType == "L" )
            {
                sOrderType = FxOrderType.Limit;
            }
            else if ( orderType == "SE" )
            {
                sOrderType = FxOrderType.EntryStop;
            }
            else if ( orderType == "LE" )
            {
                sOrderType = FxOrderType.EntryLimit;
            }
            else if ( orderType == "STE" )
            {
                sOrderType = FxOrderType.TrailingEntryStop;
            }
            else if ( orderType == "LTE" )
            {
                sOrderType = FxOrderType.TrailingEntryLimit;
            }
            else if ( orderType == "C" )
            {
                sOrderType = FxOrderType.Close;
            }
            else if ( orderType == "CM" )
            {
                sOrderType = FxOrderType.CloseMarket;
            }
            else if ( orderType == "CR" )
            {
                sOrderType = FxOrderType.CloseRange;
            }
            else if ( orderType == "O" )
            {
                sOrderType = FxOrderType.Open;
            }
            else if ( orderType == "OM" )
            {
                sOrderType = FxOrderType.OpenMarket;
            }
            else if ( orderType == "OR" )
            {
                sOrderType = FxOrderType.OpenRange;
            }
            else if ( orderType == "M" )
            {
                sOrderType = FxOrderType.MarginCall;
            }

            return sOrderType;
        }

        public static SymbolsEnum GetSymbolEnum( string input )
        {
            SymbolsEnum mysmbol;

            switch ( input )
            {
                case "EUR/USD":
                    mysmbol = SymbolsEnum.EURUSD;
                    break;

                case "CHF/JPY":
                    mysmbol = SymbolsEnum.CHFJPY;
                    break;

                case "GBP/CHF":
                    mysmbol = SymbolsEnum.GBPCHF;
                    break;

                case "EUR/AUD":
                    mysmbol = SymbolsEnum.EURAUD;
                    break;

                case "EUR/CAD":
                    mysmbol = SymbolsEnum.EURCAD;
                    break;

                case "AUD/CAD":
                    mysmbol = SymbolsEnum.AUDCAD;
                    break;

                case "CAD/JPY":
                    mysmbol = SymbolsEnum.CADJPY;
                    break;
                case "NZD/JPY":
                    mysmbol = SymbolsEnum.NZDJPY;
                    break;

                case "GBP/CAD":
                    mysmbol = SymbolsEnum.GBPCAD;
                    break;

                case "AUD/NZD":
                    mysmbol = SymbolsEnum.AUDNZD;

                    break;
                case "USD/SEK":
                    mysmbol = SymbolsEnum.USDSEK;

                    break;
                case "USD/DDK":
                    mysmbol = SymbolsEnum.USDDDK;

                    break;
                case "EUR/SEK":
                    mysmbol = SymbolsEnum.EURSEK;
                    break;

                case "EUR/NOK":
                    mysmbol = SymbolsEnum.EURNOK;

                    break;

                case "USD/NOK":
                    mysmbol = SymbolsEnum.USDNOK;

                    break;
                case "USD/MXN":
                    mysmbol = SymbolsEnum.USDMXN;

                    break;
                case "AUD/CHF":
                    mysmbol = SymbolsEnum.AUDCHF;

                    break;
                case "EUR/NZD":
                    mysmbol = SymbolsEnum.EURNZD;

                    break;
                case "EUR/PLN":
                    mysmbol = SymbolsEnum.EURPLN;

                    break;
                case "USD/PLN":
                    mysmbol = SymbolsEnum.USDPLN;

                    break;
                case "EUR/CZK":
                    mysmbol = SymbolsEnum.EURCZK;

                    break;
                case "USD/CZK":
                    mysmbol = SymbolsEnum.USDCZK;

                    break;
                case "USD/ZAR":
                    mysmbol = SymbolsEnum.USDZAR;
                    break;

                case "USD/SGD":
                    mysmbol = SymbolsEnum.USDSGD;
                    break;

                case "USD/HKD":
                    mysmbol = SymbolsEnum.USDHKD;
                    break;

                case "EUR/DKK":
                    mysmbol = SymbolsEnum.EURDKK;
                    break;

                case "GBP/SEK":
                    mysmbol = SymbolsEnum.GBPSEK;
                    break;

                case "NOK/JPY":
                    mysmbol = SymbolsEnum.NOKJPY;
                    break;

                case "SEK/JPY":
                    mysmbol = SymbolsEnum.SEKJPY;
                    break;

                case "SGD/JPY":
                    mysmbol = SymbolsEnum.SGDJPY;
                    break;

                case "HKD/JPY":
                    mysmbol = SymbolsEnum.HKDJPY;
                    break;

                case "ZAR/JPY":
                    mysmbol = SymbolsEnum.ZARJPY;
                    break;

                case "USD/TRY":
                    mysmbol = SymbolsEnum.USDTRY;
                    break;

                case "EUR/TRY":
                    mysmbol = SymbolsEnum.EURTRY;
                    break;

                case "NZD/CHF":
                    mysmbol = SymbolsEnum.NZDCHF;
                    break;

                case "CAD/CHF":
                    mysmbol = SymbolsEnum.CADCHF;
                    break;

                case "NZD/CAD":
                    mysmbol = SymbolsEnum.NZDCAD;
                    break;


                case "CHF/SEK":
                    mysmbol = SymbolsEnum.CHFSEK;
                    break;

                case "CHF/NOK":
                    mysmbol = SymbolsEnum.CHFNOK;
                    break;

                case "EUR/HUF":
                    mysmbol = SymbolsEnum.EURHUF;
                    break;

                case "USD/HUF":
                    mysmbol = SymbolsEnum.USDHUF;
                    break;

                case "TRY/JPY":
                    mysmbol = SymbolsEnum.TRYJPY;
                    break;

                case "GBP/USD":
                    mysmbol = SymbolsEnum.GBPUSD;
                    break;

                case "USD/CNH":
                    mysmbol = SymbolsEnum.USDCNH;

                    break;

                case "EUR/JPY":
                    mysmbol = SymbolsEnum.EURJPY;
                    break;

                case "USD/JPY":
                    mysmbol = SymbolsEnum.USDJPY;
                    break;

                case "GBP/JPY":
                    mysmbol = SymbolsEnum.GBPJPY;
                    break;

                case "AUD/JPY":
                    mysmbol = SymbolsEnum.AUDJPY;
                    break;

                case "USD/CHF":
                    mysmbol = SymbolsEnum.USDCHF;
                    break;

                case "AUD/USD":
                    mysmbol = SymbolsEnum.AUDUSD;
                    break;

                case "EUR/CHF":
                    mysmbol = SymbolsEnum.EURCHF;
                    break;

                case "EUR/GBP":
                    mysmbol = SymbolsEnum.EURGBP;
                    break;

                case "NZD/USD":
                    mysmbol = SymbolsEnum.NZDUSD;
                    break;

                case "USD/CAD":
                    mysmbol = SymbolsEnum.USDCAD;
                    break;

                case "GBP/AUD":
                    mysmbol = SymbolsEnum.GBPAUD;
                    break;

                case "XAG/USD":
                    mysmbol = SymbolsEnum.XAGUSD;
                    break;

                case "XAU/USD":
                    mysmbol = SymbolsEnum.XAUUSD;
                    break;

                case "UK100":
                    mysmbol = SymbolsEnum.UK100;
                    break;

                case "USDOLLAR":
                    mysmbol = SymbolsEnum.USDOLLAR;
                    break;

                case "GER30":
                    mysmbol = SymbolsEnum.GER30;
                    break;

                case "FRA40":
                    mysmbol = SymbolsEnum.FRA40;
                    break;

                case "AUS200":
                    mysmbol = SymbolsEnum.AUS200;
                    break;

                case "ESP35":
                    mysmbol = SymbolsEnum.ESP35;
                    break;

                case "HKG33":
                    mysmbol = SymbolsEnum.HKG33;
                    break;

                case "ITA40":
                    mysmbol = SymbolsEnum.ITA40;
                    break;

                case "JPN225":
                    mysmbol = SymbolsEnum.JPN225;
                    break;

                case "NAS100":
                    mysmbol = SymbolsEnum.NAS100;
                    break;

                case "SPX500":
                    mysmbol = SymbolsEnum.SPX500;
                    break;

                case "SUI20":
                    mysmbol = SymbolsEnum.SUI20;
                    break;

                case "Copper":
                    mysmbol = SymbolsEnum.COPPER;
                    break;

                case "EUSTX50":
                    mysmbol = SymbolsEnum.EUSTX50;
                    break;

                case "US30":
                    mysmbol = SymbolsEnum.US30;
                    break;

                case "USOIL":
                    mysmbol = SymbolsEnum.USOIL;
                    break;

                case "UKOIL":
                    mysmbol = SymbolsEnum.UKOIL;
                    break;

                case "NGAS":
                    mysmbol = SymbolsEnum.NGAS;
                    break;

                case "XPD/USD":
                    mysmbol = SymbolsEnum.XPDUSD;
                    break;

                case "XPT/USD":
                    mysmbol = SymbolsEnum.XPTUSD;
                    break;

                case "BUND":
                    mysmbol = SymbolsEnum.BUND;
                    break;

                case "CHN50":
                    mysmbol = SymbolsEnum.CHN50;
                    break;

                case "US2000":
                    mysmbol = SymbolsEnum.US2000;
                    break;

                case "SOYF":
                    mysmbol = SymbolsEnum.SOYF;
                    break;

                case "WHEATF":
                    mysmbol = SymbolsEnum.WHEATF;
                    break;

                case "CORNF":
                    mysmbol = SymbolsEnum.CORNF;
                    break;

                case "BTC/USD":
                    mysmbol = SymbolsEnum.BTCUSD;
                    break;

                case "ETH/USD":
                    mysmbol = SymbolsEnum.ETHUSD;
                    break;

                case "LTC/USD":
                    mysmbol = SymbolsEnum.LTCUSD;
                    break;



                default:
                    mysmbol = SymbolsEnum.EURUSD;
                    break;
            }

            return mysmbol;

        }

        public static List<FxOpenPositionAndOrders> GetOpenPositionAndOrdersByAccountName( string accountName )
        {
            var output = new List<FxOpenPositionAndOrders>();

            var openpos = _fxOpenPositionPendingOrderInfoBindingList.ToList();


            foreach ( var openPositionAndOrders in openpos )
            {
                if ( openPositionAndOrders != null && openPositionAndOrders.AccountName == accountName )
                {
                    output.Add( openPositionAndOrders );
                }
            }


            return output;
        }

        public static double GetAccountCurrencyToUsdRate( string accountCurrency, bool isBuy )
        {
            double rate = 1;

            if ( accountCurrency == "USD" )
            {
                rate = 1;
            }
            else if ( accountCurrency == "EUR" )
            {
                var pair = GetOffer( "EUR/USD" );
                rate = isBuy ? pair.Ask : pair.Bid;

                if ( rate > 0 ) rate = 1 / rate;
            }
            else if ( accountCurrency == "GBP" )
            {
                var pair = GetOffer( "GBP/USD" );
                rate = isBuy ? pair.Ask : pair.Bid;

                if ( rate > 0 ) rate = 1 / rate;
            }
            else if ( accountCurrency == "JPY" )
            {
                var pair = GetOffer( "USD/JPY" );
                rate = isBuy ? pair.Ask : pair.Bid;
            }

            else if ( accountCurrency == "CHF" )
            {
                var pair = GetOffer( "USD/CHF" );
                rate = isBuy ? pair.Ask : pair.Bid;
            }
            else if ( accountCurrency == "AUD" )
            {
                var pair = GetOffer( "AUD/USD" );
                rate = isBuy ? pair.Ask : pair.Bid;

                if ( rate > 0 ) rate = 1 / rate;
            }
            else if ( accountCurrency == "CAD" )
            {
                var pair = GetOffer( "USD/CAD" );
                rate = isBuy ? pair.Ask : pair.Bid;
            }
            else if ( accountCurrency == "NZD" )
            {
                var pair = GetOffer( "NZD/USD" );
                rate = isBuy ? pair.Ask : pair.Bid;

                if ( rate > 0 ) rate = 1 / rate;
            }
            else if ( accountCurrency == "SEK" )
            {
                var pair = GetOffer( "USD/SEK" );
                rate = isBuy ? pair.Ask : pair.Bid;
            }
            else if ( accountCurrency == "NOK" )
            {
                var pair = GetOffer( "USD/NOK" );
                rate = isBuy ? pair.Ask : pair.Bid;
            }
            else if ( accountCurrency == "MXN" )
            {
                var pair = GetOffer( "USD/MXN" );
                rate = isBuy ? pair.Ask : pair.Bid;
            }
            else if ( accountCurrency == "CZK" )
            {
                var pair = GetOffer( "USD/CZK" );
                rate = isBuy ? pair.Ask : pair.Bid;
            }
            else if ( accountCurrency == "ZAR" )
            {
                var pair = GetOffer( "USD/ZAR" );
                rate = isBuy ? pair.Ask : pair.Bid;
            }
            else if ( accountCurrency == "HKD" )
            {
                var pair = GetOffer( "USD/HKD" );
                rate = isBuy ? pair.Ask : pair.Bid;
            }
            else if ( accountCurrency == "TRY" )
            {
                var pair = GetOffer( "USD/TRY" );
                rate = isBuy ? pair.Ask : pair.Bid;
            }
            else if ( accountCurrency == "CNH" )
            {
                var pair = GetOffer( "USD/CNH" );
                rate = isBuy ? pair.Ask : pair.Bid;
            }

            return rate;
        }

        public static double GetPipValuePerAmount( string symbol, double currentRate, double volume, bool isBuy )
        {
            fxSymbol desiredSymbol = new fxSymbol( symbol );

            double pipCost = GetPipCost( symbol );

            double pipCostXVolume = 0d;

            switch ( desiredSymbol.SymbolGroup )
            {
                case SymbolGroup.Forex:
                {
                    if ( desiredSymbol.FirstOfCross == "USD" )
                    {
                        pipCostXVolume = GetInstrumentPointSize( symbol ) / currentRate * volume;
                    }
                    else if ( desiredSymbol.SecondOfCross == "USD" )
                    {
                        pipCostXVolume = volume / 10000;
                    }
                    else
                    {
                        /// <image url="$(SolutionDir)\..\..\30 - CommonImages\CrossRates.PNG"/>

                        string baseRateSymbol = string.Format( "{0}/USD", desiredSymbol.FirstOfCross );

                        int offerId = GetOfferId( baseRateSymbol );

                        if ( offerId > 0 )
                        {
                            double baseRate = GetRate( offerId, isBuy );

                            if ( desiredSymbol.SecondOfCross == "JPY" )
                            {
                                pipCostXVolume = baseRate / currentRate * volume / 10000 * 100;
                            }
                            else
                            {
                                /*
                                 * 
                                 * Here is an example of how to calculate the value of 1 Pip for one 10k lot of EUR/USD where the base currency of the account is USD:
                                 *      Start with 10,000.  Multiply 10,000 by .0001 (since 1/10,000th is a pip for all pairs [except JPY pairs]). 10,000* .0001 = 1
                                 *      You now know each pip is worth 1 USD. That will be valued in the “counter currency” (second currency) of the pair. 
                                 *      In this example, we are using the EUR/USD, so USD is the counter currency of the pair.  Here, 1 pip is worth 1 USD dollar for 1 - 10K lot of EUR/USD. 
                                 *      To learn how to calculate Pip value when your base currency is not the same as the second currency in the pair, see the next example.
                                 *      
                                 * Here is an example of how you can calculate the value of 1 Pip for 1 - 10K lot of EUR/GBP where the base currency of the account is USD:
                                 *      Start with 10,000. Multiple 10,000 by .0001 (since 1/10,000th is a pip for all pairs [except JPY pairs]). 10,000* .0001 = “1”.
                                 *      
                                 *          1) volume * 0.0001 = volume / 10000
                                 *      
                                 *      You now know each pip is worth “1”. That will be valued in the “counter currency” (second currency) of the pair.  
                                 *      In this example, we are using the EUR/GBP, so GBP is the counter currency of the pair. 
                                 *      
                                 *      Take the current exchange rate of the GBP/USD and multiply it by “1” to calculate the value of 1 pip in your base currency.
                                 *      In this example, GBP/USD is trading at $1.66 and 1 Pip for EUR/GBP would be equal to $1.66 USD.
                                 *      
                                 *          2) Base Rate = EUR/USD
                                 *          3) Counter Rate = GBP/USD
                                 *          4) EUR      EUR    USD    EUR       1
                                 *             ---  =   --- *  --- =  --- *  -------
                                 *             GBP      USD    GBP    USD    GBP/USD
                                 * 
                                 * */
                                pipCostXVolume = baseRate / currentRate * volume / 10000;
                            }

                        }
                    }
                }
                break;


                case SymbolGroup.Indices:
                {
                    switch ( symbol )
                    {
                        case "GER30":
                        case "FRA40":
                        case "ESP35":
                        case "ITA40":
                        case "EUSTX50":
                        {
                            int offerId = GetOfferId( "EUR/USD" );
                            double baseRate = GetRate( offerId, isBuy );
                            pipCostXVolume = volume * baseRate;
                        }
                        break;

                        case "UK100":
                        {
                            int offerId = GetOfferId( "GBP/USD" );
                            double baseRate = GetRate( offerId, isBuy );
                            pipCostXVolume = volume * baseRate;
                        }
                        break;

                        case "AUS200":
                        {
                            int offerId = GetOfferId( "AUD/USD" );
                            double baseRate = GetRate( offerId, isBuy );
                            pipCostXVolume = volume * baseRate;
                        }
                        break;

                        case "HKG33":
                        {
                            int offerId = GetOfferId( "USD/HKD" );
                            double baseRate = GetRate( offerId, isBuy );
                            pipCostXVolume = volume / baseRate;
                        }
                        break;

                        case "JPN225":
                        {
                            int offerId = GetOfferId( "USD/JPY" );
                            double baseRate = GetRate( offerId, isBuy );
                            pipCostXVolume = volume / baseRate;
                        }
                        break;

                        case "SUI20":
                        {
                            int offerId = GetOfferId( "USD/CHF" );
                            double baseRate = GetRate( offerId, isBuy );
                            pipCostXVolume = volume / baseRate;
                        }
                        break;

                        case "NAS100":
                        case "SPX500":
                        case "COPPER":
                        case "US30":
                            pipCostXVolume = volume * pipCost;
                            break;
                    }


                }
                break;

                case SymbolGroup.Commodity:
                {
                    pipCostXVolume = volume * GetInstrumentPointSize( symbol );
                }
                break;

                case SymbolGroup.Treasury:
                {
                    pipCostXVolume = volume * GetInstrumentPointSize( symbol );
                }
                break;

                case SymbolGroup.Bullion:
                {
                    if ( desiredSymbol.FirstOfCross == "USD" )
                    {
                        pipCostXVolume = GetInstrumentPointSize( symbol ) / currentRate * volume;
                    }
                    else if ( desiredSymbol.SecondOfCross == "USD" )
                    {
                        pipCostXVolume = volume * GetInstrumentPointSize( symbol );
                    }                    
                }
                break;

                case SymbolGroup.Shares:
                {
                    pipCostXVolume = volume * GetInstrumentPointSize( symbol );
                }
                break;

                case SymbolGroup.FXIndex:
                {
                    pipCostXVolume = volume * GetInstrumentPointSize( symbol );
                }
                break;

            }

            return pipCostXVolume;
        }

        //public static double GetReversePosPips( string symbol, double currentRate, double volume, bool isBuy )
        //{
        //    fxSymbol desiredSymbol = new fxSymbol( symbol );

        //    double pipCost = SymbolsMgr.Instance.GetPipCost( symbol );

        //                

        //    return pipCostXVolume;
        //}

        public static double GetRate( int offerId, bool isBuy )
        {
            FxBidAsk output = GetOffer( offerId );

            return isBuy ? output.Bid : output.Ask;
        }
        
        public static List<PositionChangeMessage> GetPositionSummaryMessage( string mainLoginName, string accountName )
        {
            List< PositionChangeMessage > msgs = new List< PositionChangeMessage >( );

            var posSummary = GetPositionSummaryBindingList();

            for ( int i = 0; i < posSummary.Count; ++i )
            {
                var positionSummary = posSummary[ i ];

                if ( string.Equals( positionSummary.MainLoginName, mainLoginName, StringComparison.CurrentCultureIgnoreCase ) &&
                     string.Equals( positionSummary.AccountName, accountName, StringComparison.CurrentCultureIgnoreCase )
                   )
                {
                    var newPos           = new PositionChangeMessage( );
                    newPos.SecurityId    = positionSummary.OfferId.ToSecurityId( );
                    newPos.PortfolioName = positionSummary.AccountName;

                    int offerId = Int32.Parse( positionSummary.OfferId );

                    var subscribedSymbol = GetSubscribedSymbolsByAccountNameAndId( mainLoginName, offerId );

                    int baseUnit = 1000;

                    if ( subscribedSymbol != null )
                    {
                        baseUnit = subscribedSymbol.BaseUnitSize > 0 ? subscribedSymbol.BaseUnitSize : 1000;
                    }

                    if ( positionSummary.IsNetLong( ) )
                    {
                        newPos.Side = Sides.Buy;
                        newPos.TryAdd( PositionChangeTypes.AveragePrice,       ( decimal ) positionSummary.BuyAvgOpen );
                        newPos.TryAdd( PositionChangeTypes.UnrealizedPnL,      ( decimal ) positionSummary.BuyNetPl.Value );
                        newPos.TryAdd( PositionChangeTypes.CurrentPrice,       ( decimal ) positionSummary.BuyClose.Value );
                        newPos.TryAdd( PositionChangeTypes.CurrentValueInLots, ( decimal ) positionSummary.BuyAmount.Value / baseUnit );
                    }
                    else
                    {
                        newPos.Side = Sides.Sell;
                        newPos.TryAdd( PositionChangeTypes.AveragePrice,       ( decimal ) positionSummary.SellAvgOpen );
                        newPos.TryAdd( PositionChangeTypes.UnrealizedPnL,      ( decimal ) positionSummary.SellNetPl.Value );
                        newPos.TryAdd( PositionChangeTypes.CurrentPrice,       ( decimal ) positionSummary.SellClose.Value );
                        newPos.TryAdd( PositionChangeTypes.CurrentValueInLots, ( decimal ) positionSummary.SellAmount.Value / baseUnit );
                    }

                    newPos.TryAdd( PositionChangeTypes.BlockedValue, ( decimal ) positionSummary.UsedMargin );

                    msgs.Add( newPos );
                }
            }



            return msgs;
        }

        public static PositionChangeMessage GetAccountSummaryMessage( string mainLoginName, string accountName, DateTimeOffset currentTime )
        {
            SubaccountTradeDataRepo tradeManager = CreateSubaccountTradeDataRepoByAccountName( mainLoginName, accountName );

            var accountsSummaryBindingList = GetAccountSummaryBindingList();

            var msg           = new PositionChangeMessage();
            msg.SecurityId    = SecurityId.Money;
            msg.PortfolioName = accountName;
            msg.ServerTime    = currentTime;

            int index = accountsSummaryBindingList.FindIndex( x => x.AccountName == accountName );

            if ( index > -1 )
            {
                var summary = accountsSummaryBindingList[ index ];

                msg.TryAdd( PositionChangeTypes.BeginValue, ( decimal ) summary.BeginningEquity );

                var calc = summary.CalculatedValue;

                if ( calc != null )
                {
                    msg.TryAdd( PositionChangeTypes.CurrentValue, ( decimal ) summary.Equity );
                    msg.TryAdd( PositionChangeTypes.BlockedValue, ( decimal ) summary.UsedMargin, true );
                    msg.TryAdd( PositionChangeTypes.RealizedPnL, ( decimal ) summary.DayPl, true );
                    msg.TryAdd( PositionChangeTypes.UnrealizedPnL, ( decimal ) summary.GrossPl, true );
                }
            }

            return msg;
        }

        public static ExecutionMessage GetTradeMessage( FxDetailedPosition pos, long transId, O2GTableUpdateType updateType )
        {
            var msg                   = new ExecutionMessage( );
            msg.DataTypeEx         = DataType.Transactions;
            msg.SecurityId            = pos.OfferID.ToSecurityId( );
            msg.OriginalTransactionId = transId;
            msg.ServerTime            = pos.OpenTime;
            msg.PortfolioName         = pos.AccountName;            

            msg.HasOrderInfo          = true;
            msg.OrderStringId         = pos.OpenOrderID;

            msg.HasTradeInfo          = true;
            msg.TradeId               = new long?( pos.TradeID.To<long>( ) );
            msg.TradeStringId         = pos.TradeID;
            msg.TradePrice            = ( decimal ) pos.OpenRate;

            var subscribedSymbol = GetSubscribedSymbolsByAccountNameAndId( pos.MainLoginName, pos.OfferID.To<int>() );

            int baseUnit = 1000;

            if ( subscribedSymbol != null )
            {
                baseUnit = subscribedSymbol.BaseUnitSize > 0 ? subscribedSymbol.BaseUnitSize : 1000;
            }

            msg.TradeVolume           = new decimal?( ( ( decimal ) pos.Amount ) / baseUnit );
            msg.Commission            = ( decimal ) pos.Commission;
            msg.Yield                 = ( decimal ) pos.RolloverInterest;

            if ( updateType == O2GTableUpdateType.Insert )
            {
                msg.PositionEffect = OrderPositionEffects.OpenOnly;
                msg.Side           = pos.BuySell.ToSides( );
                msg.OriginSide     = pos.BuySell.ToSides( );
            }            
            else if ( updateType == O2GTableUpdateType.Delete )
            {
                msg.PositionEffect = OrderPositionEffects.CloseOnly;
                msg.OriginSide     = pos.BuySell.ToSides( );
                msg.Side           = pos.BuySell.ToOppSides( );
            }

            return msg;
        }

        public static ExecutionMessage GetOrderMessage( DetailedOrderDB detailedOrder, long transId )
        {
            FxcmExtendedOrderTypes? fxcmOrder;

            var subscribedSymbol = GetSubscribedSymbolsByAccountNameAndId( detailedOrder.MainLoginName, detailedOrder.OfferID.To<int>() );

            int baseUnit = 1;

            if ( subscribedSymbol != null )
            {
                baseUnit = subscribedSymbol.BaseUnitSize;
            }

            var msg                   = new ExecutionMessage( );
            msg.DataTypeEx         = DataType.Transactions;
            msg.HasOrderInfo          = true;
            msg.SecurityId            = detailedOrder.OfferID.ToSecurityId( );
            msg.OriginalTransactionId = transId;
            msg.ServerTime            = detailedOrder.StatusTime.FromLinuxTime( );
            msg.TimeInForce           = new TimeInForce?( detailedOrder.TimeInForce.ToSxTIF( ) );
            msg.OrderVolume           = new decimal?( ( ( decimal ) detailedOrder.OriginAmount ) / baseUnit );
            msg.Balance               = new decimal?( ( decimal ) ( detailedOrder.OriginAmount - detailedOrder.FilledAmount ) / baseUnit );
            msg.PortfolioName         = detailedOrder.AccountName;
            
            msg.OrderPrice            = detailedOrder.Rate.ToDecimal( ) ?? decimal.Zero;
            msg.OrderStringId         = detailedOrder.OrderID;
            msg.OrderState            = new OrderStates?( detailedOrder.Status.ToSxOrderStates( ) );
            msg.SystemComment         = detailedOrder.Parties;            
            msg.OrderType             = new OrderTypes?( detailedOrder.Type.ToSxOrderType( out fxcmOrder ) );
            msg.TransactionId         = 0L;

            if ( detailedOrder.Type == "CM" || detailedOrder.Type == "CR" || detailedOrder.Type == "CL" )
            {
                if ( detailedOrder.BuySell == "B" )
                {
                    msg.Side = Sides.Buy;
                }
                else
                {
                    msg.Side = Sides.Sell;
                }

            }
            else
            {
                msg.Side = detailedOrder.BuySell.ToSides( );
            }

            OrderTypes? orderType     = msg.OrderType;

            if ( ( orderType.GetValueOrDefault( ) == OrderTypes.Conditional ? ( orderType.HasValue ? 1 : 0 ) : 0 ) != 0 )
            {

                var order                 = new FxcmOrderCondition( );
                order.ExtendedType        = fxcmOrder;
                order.RateMin             = detailedOrder.RateMin.ToDecimal( );
                order.RateMax             = detailedOrder.RateMax.ToDecimal( );
                order.AtMarket            = detailedOrder.AtMarket.ToDecimal( );

                order.ContingencyType     = detailedOrder.ContingencyType.ToFxcmContingencyType( );
                order.ContingentOrderId   = detailedOrder.ContingentOrderID;
                order.PegType             = detailedOrder.PegType;
                order.PegOffset           = detailedOrder.PegOffset.ToDecimal( );
                //order.PegOffsetMin      = row.PegOffsetMin.ToDecimal( );
                //order.PegOffsetMax      = row.PegOffsetMax.ToDecimal( );
                order.TrailRate           = detailedOrder.TrailRate.ToDecimal( );
                order.TrailStep           = detailedOrder.TrailStep.ToNullableInt( );
                order.Parties             = detailedOrder.Parties;
                msg.Condition             = order;
            }
            if ( detailedOrder.TimeInForce == "GTD" )
            {
                msg.ExpiryDate = new DateTimeOffset?( detailedOrder.ExpireDate.FromLinuxTime( ) );
            }
            else if ( detailedOrder.TimeInForce == "DAY" )
            {
                msg.ExpiryDate = new DateTimeOffset?( DateTime.Today );
            }

            //if ( !StringHelper.IsEmpty( detailedOrder.TradeID ) && detailedOrder.ExecutionRate > 0 )
            //{
            //    msg.HasTradeInfo = true;
            //    msg.TradeId      = new long?( detailedOrder.TradeID.To<long>( ) );
            //    msg.TradePrice   = new decimal?( ( decimal ) detailedOrder.ExecutionRate );
            //}

            return msg;
        }

        public static List<ExecutionMessage> GetOrdersMessage( string mainLoginName, string accountName )
        {
            List< ExecutionMessage > msgs = new List< ExecutionMessage >( );

            var orders = GetDetailedOrdersBindingList();

            for ( int i = 0; i < orders.Count; ++i )
            {
                var row = orders[ i ];

                if ( string.Equals( row.MainLoginName, mainLoginName, StringComparison.CurrentCultureIgnoreCase ) &&
                     string.Equals( row.AccountName, accountName, StringComparison.CurrentCultureIgnoreCase )
                   )
                {
                    ExecutionMessage exeMsg      = new ExecutionMessage( );
                    exeMsg.DataTypeEx         = DataType.Transactions;
                    exeMsg.HasOrderInfo          = true;
                    exeMsg.SecurityId            = row.OfferID.ToSecurityId( );

                    var subscribedSymbol = GetSubscribedSymbolsByAccountNameAndId( mainLoginName, row.OfferID.To<int>() );

                    int baseUnit = 1000;

                    if ( subscribedSymbol != null )
                    {
                        baseUnit = subscribedSymbol.BaseUnitSize > 0 ? subscribedSymbol.BaseUnitSize : 1000;
                    }

                    var customId = row.RequestTXT.Split(',').Select(x => x.Split('=')).Where(x => x.Length > 1 && !String.IsNullOrEmpty(x[0].Trim()) && !String.IsNullOrEmpty(x[1].Trim())).ToDictionary(x => x[0].Trim(), x => x[1].Trim());

                    long myRequestId = 0;

                    if ( customId.ContainsKey( "ID" ) )
                    {
                        myRequestId = customId[ "ID" ].To<int>( );
                    }

                    exeMsg.OriginalTransactionId = myRequestId;
                    exeMsg.ServerTime            = row.StatusTimeDT;
                    exeMsg.TimeInForce           = ( new TimeInForce?( row.TimeInForce.ToSxTIF( ) ) );

                    exeMsg.OrderVolume           = new decimal?( ( ( decimal ) row.OriginAmount ) / baseUnit );
                    exeMsg.Balance               = new decimal?( ( decimal ) ( row.OriginAmount - row.FilledAmount ) / baseUnit );
                    
                    exeMsg.PortfolioName         = ( row.AccountName );

                    if ( row.Type == "CM" || row.Type == "CR" || row.Type == "CL" )
                    {
                        if ( row.BuySell == "B" )
                        {
                            exeMsg.Side = Sides.Buy;
                        }
                        else
                        {
                            exeMsg.Side = Sides.Sell;
                        }

                    }
                    else
                    {
                        exeMsg.Side = row.BuySell.ToSides( );
                    }

                    exeMsg.Side                  = ( row.BuySell.ToSides( ) );
                    exeMsg.OrderPrice            = ( row.Rate.ToDecimal( ) ?? decimal.Zero );
                    exeMsg.OrderStringId         = ( row.OrderID );
                    exeMsg.OrderState            = ( new OrderStates?( row.Status.ToSxOrderStates( ) ) );

                    FxcmExtendedOrderTypes? fxcmOrder;
                    exeMsg.OrderType = ( new OrderTypes?( row.Type.ToSxOrderType( out fxcmOrder ) ) );
                    exeMsg.TransactionId = 0L;

                    OrderTypes? orderType = exeMsg.OrderType;
                    if ( ( orderType.GetValueOrDefault( ) == OrderTypes.Conditional ? ( orderType.HasValue ? 1 : 0 ) : 0 ) != 0 )
                    {

                        var order               = new FxcmOrderCondition( );
                        order.ExtendedType      = fxcmOrder;
                        order.RateMin           = row.RateMin.ToDecimal( );
                        order.RateMax           = row.RateMax.ToDecimal( );
                        order.AtMarket          = row.AtMarket.ToDecimal( );

                        order.ContingencyType   = row.ContingencyType.ToFxcmContingencyType( );
                        order.ContingentOrderId = row.ContingentOrderID;
                        order.PegType           = row.PegType;
                        order.PegOffset         = row.PegOffset.ToDecimal( );
                        //order.PegOffsetMin      = row.PegOffsetMin.ToDecimal( );
                        //order.PegOffsetMax      = row.PegOffsetMax.ToDecimal( );
                        order.TrailRate         = row.TrailRate.ToDecimal( );
                        order.TrailStep         = row.TrailStep.ToNullableInt( );
                        order.Parties           = row.Parties;
                        exeMsg.Condition        = order;
                    }
                    if ( row.TimeInForce == "GTD" )
                    {
                        exeMsg.ExpiryDate = ( new DateTimeOffset?( row.ExpireDate.FromLinuxTime() ));
                    }
                    else if ( row.TimeInForce == "DAY" )
                    {
                        exeMsg.ExpiryDate = ( new DateTimeOffset?( DateTime.Today ) );
                    }

                    //if ( !StringHelper.IsEmpty( row.TradeID ) )
                    //{
                    //    exeMsg.HasTradeInfo = ( true );
                    //    exeMsg.TradeId = ( new long?( row.TradeID.To<long>( ) ) );
                    //    exeMsg.TradePrice = ( new decimal?( ( decimal ) row.ExecutionRate ) );
                    //}

                    msgs.Add( exeMsg );
                }
            }



            return msgs;
        }

    }
}


