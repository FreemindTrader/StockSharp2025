using System;
using fx.Definitions;


namespace fx.Algorithm
{
    /// <summary>
    /// Trading baseCurrency information structure.
    /// </summary>
    [Serializable]
    public class fxSymbol : ISymbol, IEquatable< ISymbol >
    {
        public fxSymbol( string symbol )
        {
            Source         = string.Empty;
            SymbolString   = symbol.ToUpper( );
            IsForexPair    = false;
            _firstOfCross  = string.Empty;
            _secondOfCross = string.Empty;
            SymbolGroup    = SymbolGroup.NA;

            Classify( );
        }

        public fxSymbol( SymbolGroup group, string symbol )
        {
            Source         = string.Empty;
            SymbolString   = symbol;
            SymbolGroup    = group;

            IsForexPair    = false;
            _firstOfCross  = string.Empty;
            _secondOfCross = string.Empty;

            Classify( );
        }

        public string UnitName( )
        {
            string unitName = "Units";

            switch( SymbolGroup )
            {
                case SymbolGroup.Forex:
                    unitName = "Pips";
                    break;

                case SymbolGroup.Indices:
                    unitName = "Pts";
                    break;

                case SymbolGroup.Commodity:
                    break;

                case SymbolGroup.Treasury:
                    break;

                case SymbolGroup.Bullion:
                    unitName = "Cents";
                    break;

                case SymbolGroup.Shares:
                    break;

                case SymbolGroup.FXIndex:
                    break;

                case SymbolGroup.Crypto:
                    break;
            }

            return unitName;
        }

        public SymbolsEnum SymbolEnum
        {
            get
            {
                return _symbolEnum;
            }

            set
            {
                _symbolEnum = value;
            }
        }

        public string Source
        {
            get;
            set;
        }

        public string SymbolString
        {
            get;
            set;
        }

        public SymbolGroup SymbolGroup
        {
            get;
            set;
        }

        public bool IsForexPair
        {
            get;
            set;
        }

        private string _firstOfCross;

        private SymbolsEnum _symbolEnum;

        /// <summary>
        /// Only available in Forex Pair symbols.
        /// </summary>
        public string FirstOfCross
        {
            get
            {
                return _firstOfCross;
            }

            set
            {
                _firstOfCross = value;
            }
        }

        private string _secondOfCross;

        /// <summary>
        /// Only available in Forex Pair symbols.
        /// </summary>
        public string SecondOfCross
        {
            get
            {
                return _secondOfCross;
            }

            set
            {
                _secondOfCross = value;
            }
        }

        public bool Equals( ISymbol other )
        {
            if( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return string.Equals( SymbolString, other.SymbolString ) && string.Equals( Source, other.Source ) && SymbolGroup == other.SymbolGroup;
        }

        public const char DefaultSeparatorSymbol = '/';

        /// <summary>
        /// Group here is optional, and there could be symbols of Forex or Stocks but with different group symbol (like
        /// FX etc.)
        /// </summary>

        public enum Currency
        {
            /// <remarks/>
            AFA,

            /// <remarks/>
            ALL,

            /// <remarks/>
            DZD,

            /// <remarks/>
            ARS,

            /// <remarks/>
            AWG,

            /// <remarks/>
            AUD,

            /// <remarks/>
            BSD,

            /// <remarks/>
            BHD,

            /// <remarks/>
            BDT,

            /// <remarks/>
            BBD,

            /// <remarks/>
            BZD,

            /// <remarks/>
            BMD,

            /// <remarks/>
            BTN,

            /// <remarks/>
            BOB,

            /// <remarks/>
            BWP,

            /// <remarks/>
            BRL,

            /// <remarks/>
            GBP,

            /// <remarks/>
            BND,

            /// <remarks/>
            BIF,

            /// <remarks/>
            XOF,

            /// <remarks/>
            XAF,

            /// <remarks/>
            KHR,

            /// <remarks/>
            CAD,

            /// <remarks/>
            CVE,

            /// <remarks/>
            KYD,

            /// <remarks/>
            CLP,

            /// <remarks/>
            CNY,

            /// <remarks/>
            COP,

            /// <remarks/>
            KMF,

            /// <remarks/>
            CRC,

            /// <remarks/>
            HRK,

            /// <remarks/>
            CUP,

            /// <remarks/>
            CYP,

            /// <remarks/>
            CZK,

            /// <remarks/>
            DKK,

            /// <remarks/>
            DJF,

            /// <remarks/>
            DOP,

            /// <remarks/>
            XCD,

            /// <remarks/>
            EGP,

            /// <remarks/>
            SVC,

            /// <remarks/>
            EEK,

            /// <remarks/>
            ETB,

            /// <remarks/>
            EUR,

            /// <remarks/>
            FKP,

            /// <remarks/>
            GMD,

            /// <remarks/>
            GHC,

            /// <remarks/>
            GIP,

            /// <remarks/>
            XAU,

            /// <remarks/>
            GTQ,

            /// <remarks/>
            GNF,

            /// <remarks/>
            GYD,

            /// <remarks/>
            HTG,

            /// <remarks/>
            HNL,

            /// <remarks/>
            HKD,

            /// <remarks/>
            HUF,

            /// <remarks/>
            ISK,

            /// <remarks/>
            INR,

            /// <remarks/>
            IDR,

            /// <remarks/>
            IQD,

            /// <remarks/>
            ILS,

            /// <remarks/>
            JMD,

            /// <remarks/>
            JPY,

            /// <remarks/>
            JOD,

            /// <remarks/>
            KZT,

            /// <remarks/>
            KES,

            /// <remarks/>
            KRW,

            /// <remarks/>
            KWD,

            /// <remarks/>
            LAK,

            /// <remarks/>
            LVL,

            /// <remarks/>
            LBP,

            /// <remarks/>
            LSL,

            /// <remarks/>
            LRD,

            /// <remarks/>
            LYD,

            /// <remarks/>
            LTL,

            /// <remarks/>
            MOP,

            /// <remarks/>
            MKD,

            /// <remarks/>
            MGF,

            /// <remarks/>
            MWK,

            /// <remarks/>
            MYR,

            /// <remarks/>
            MVR,

            /// <remarks/>
            MTL,

            /// <remarks/>
            MRO,

            /// <remarks/>
            MUR,

            /// <remarks/>
            MXN,

            /// <remarks/>
            MDL,

            /// <remarks/>
            MNT,

            /// <remarks/>
            MAD,

            /// <remarks/>
            MZM,

            /// <remarks/>
            MMK,

            /// <remarks/>
            NAD,

            /// <remarks/>
            NPR,

            /// <remarks/>
            ANG,

            /// <remarks/>
            NZD,

            /// <remarks/>
            NIO,

            /// <remarks/>
            NGN,

            /// <remarks/>
            KPW,

            /// <remarks/>
            NOK,

            /// <remarks/>
            OMR,

            /// <remarks/>
            XPF,

            /// <remarks/>
            PKR,

            /// <remarks/>
            XPD,

            /// <remarks/>
            PAB,

            /// <remarks/>
            PGK,

            /// <remarks/>
            PYG,

            /// <remarks/>
            PEN,

            /// <remarks/>
            PHP,

            /// <remarks/>
            XPT,

            /// <remarks/>
            PLN,

            /// <remarks/>
            QAR,

            /// <remarks/>
            ROL,

            /// <remarks/>
            RUB,

            /// <remarks/>
            WST,

            /// <remarks/>
            STD,

            /// <remarks/>
            SAR,

            /// <remarks/>
            SCR,

            /// <remarks/>
            SLL,

            /// <remarks/>
            XAG,

            /// <remarks/>
            SGD,

            /// <remarks/>
            SKK,

            /// <remarks/>
            SIT,

            /// <remarks/>
            SBD,

            /// <remarks/>
            SOS,

            /// <remarks/>
            ZAR,

            /// <remarks/>
            LKR,

            /// <remarks/>
            SHP,

            /// <remarks/>
            SDD,

            /// <remarks/>
            SRG,

            /// <remarks/>
            SZL,

            /// <remarks/>
            SEK,

            /// <remarks/>
            CHF,

            /// <remarks/>
            SYP,

            /// <remarks/>
            TWD,

            /// <remarks/>
            TZS,

            /// <remarks/>
            THB,

            /// <remarks/>
            TOP,

            /// <remarks/>
            TTD,

            /// <remarks/>
            TND,

            /// <remarks/>
            TRL,

            /// <remarks/>
            USD,

            /// <remarks/>
            AED,

            /// <remarks/>
            UGX,

            /// <remarks/>
            UAH,

            /// <remarks/>
            UYU,

            /// <remarks/>
            VUV,

            /// <remarks/>
            VEB,

            /// <remarks/>
            VND,

            /// <remarks/>
            YER,

            /// <remarks/>
            YUM,

            /// <remarks/>
            ZMK,

            /// <remarks/>
            ZWD,

            /// <remarks/>
            TRY,
        }

        /// <summary>
        /// Name of the source providing this symbol, optional and applicable where multiple sources provide service
        /// trough a single provider.
        /// </summary>

        /// <summary>
        /// The two involved currencies, in case this is a forex pair.
        /// </summary>
        public string[ ] ForexCurrencies
        {
            get
            {
                return new string[ ] { FirstOfCross, SecondOfCross };
            }
        }

        /// <summary>
        /// Empty baseCurrency instance.
        /// </summary>
        public static fxSymbol Empty
        {
            get
            {
                return new fxSymbol( string.Empty );
            }
        }

        #region Operators

        /// <summary>
        ///
        /// </summary>
        public static bool operator ==( fxSymbol a, fxSymbol b )
        {
            if( ( ( object )a ) == null &&
                 ( ( object )b ) == null )
            {
                return true;
            }

            if( ( ( object )a ) == null )
            {
                return false;
            }

            return a.Equals( b );
        }

        /// <summary>
        ///
        /// </summary>
        public static bool operator !=( fxSymbol a, fxSymbol b )
        {
            return !( a == b );
        }

        /// <summary>
        ///
        /// </summary>
        public override bool Equals( object obj )
        {
            if( ReferenceEquals( null, obj ) )
            {
                return false;
            }

            if( ReferenceEquals( this, obj ) )
            {
                return true;
            }

            if( obj.GetType( ) != GetType( ) )
            {
                return false;
            }

            return Equals( ( fxSymbol )obj );
        }

        /// <summary>
        ///
        /// </summary>
        public override int GetHashCode( )
        {
            unchecked
            {
                var hashCode = ( SymbolString != null ? SymbolString.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( Source != null ? Source.GetHashCode( ) : 0 );
                hashCode = ( hashCode * 397 ) ^ ( int )SymbolGroup;
                return hashCode;
            }
        }

        #endregion

        #region 00 - Constructor

        private void Classify( )
        {
            if( SymbolGroup == SymbolGroup.NA )
            {
                SymbolGroup = FindGroupBySymbol( SymbolString );
            }

            if( SymbolString.Contains( "/" ) )
            {
                string[ ] currencies = SymbolString.Split( '/' );
                _firstOfCross = currencies[ 0 ];
                _secondOfCross = currencies[ 1 ];

                // SplitSymbolToCrosses( out _firstOfCross, out _secondOfCross );
            }

            if( SymbolGroup == SymbolGroup.Forex )
            {
                IsForexPair = true;
            }

            _symbolEnum = FinancialHelper.GetSymbolEnum( SymbolString );
        }

        #endregion
        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty( SymbolString ) || this == Empty;
            }
        }

        public void FixSymbol( )
        {
            SymbolGroup = FindGroupBySymbol( SymbolString );
        }

        public static SymbolGroup FindGroupBySymbol( string symbolName )
        {
            SymbolGroup symbolGroup = SymbolGroup.NA;

            symbolName = symbolName.ToUpper( );

            switch( symbolName )
            {
                case "EUR/USD":
                case "CHF/JPY":
                case "GBP/CHF":
                case "EUR/AUD":
                case "EUR/CAD":
                case "AUD/CAD":
                case "CAD/JPY":
                case "NZD/JPY":
                case "GBP/CAD":
                case "AUD/NZD":
                case "USD/SEK":
                case "USD/DDK":
                case "EUR/SEK":
                case "EUR/NOK":
                case "USD/NOK":
                case "USD/MXN":
                case "AUD/CHF":
                case "EUR/NZD":
                case "EUR/PLN":
                case "USD/PLN":
                case "EUR/CZK":
                case "USD/CZK":
                case "USD/ZAR":
                case "USD/SGD":
                case "USD/HKD":
                case "EUR/DKK":
                case "GBP/SEK":
                case "NOK/JPY":
                case "SEK/JPY":
                case "SGD/JPY":
                case "HKD/JPY":
                case "ZAR/JPY":
                case "USD/TRY":
                case "EUR/TRY":
                case "NZD/CHF":
                case "CAD/CHF":
                case "NZD/CAD":
                case "CHF/SEK":
                case "CHF/NOK":
                case "EUR/HUF":
                case "USD/HUF":
                case "TRY/JPY":
                case "GBP/USD":
                case "USD/CNH":
                case "EUR/JPY":
                case "USD/JPY":
                case "GBP/JPY":
                case "AUD/JPY":
                case "USD/CHF":
                case "AUD/USD":
                case "EUR/CHF":
                case "EUR/GBP":
                case "NZD/USD":
                case "USD/CAD":
                case "GBP/AUD":
                    symbolGroup = SymbolGroup.Forex;
                    break;

                case "XAG/USD":
                case "XAU/USD":
                case "XPD/USD":
                case "XPT/USD":
                    symbolGroup = SymbolGroup.Bullion;
                    break;

                case "UK100":
                case "GER30":
                case "FRA40":
                case "AUS200":
                case "ESP35":
                case "HKG33":
                case "ITA40":
                case "JPN225":
                case "NAS100":
                case "SPX500":
                case "SUI20":
                case "COPPER":
                case "EUSTX50":
                case "US30":
                case "CHN50":
                    symbolGroup = SymbolGroup.Indices;
                    break;

                case "USDOLLAR":
                    symbolGroup = SymbolGroup.FXIndex;
                    break;

                case "USOIL":
                case "UKOIL":
                case "NGAS":
                case "SOYF":
                case "WHEATF":
                case "CORNF":
                    symbolGroup = SymbolGroup.Commodity;
                    break;

                case "BUND":
                    symbolGroup = SymbolGroup.Treasury;
                    break;

                case "BTC/USD":
                case "ETH/USD":
                case "LTC/USD":
                    symbolGroup = SymbolGroup.Crypto;
                    break;

                default:
                    symbolGroup = SymbolGroup.NA;
                    break;
            }

            return symbolGroup;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetForexPairQuoteCurrency( string symbol )
        {
            return GetForexPairQuoteCurrency( symbol, DefaultSeparatorSymbol );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetForexPairQuoteCurrency( string symbol, char separator )
        {
            string[ ] splits = symbol.Split( separator );

            if( splits.Length != 2 )
            {
                return null;
            }

            return splits[ 1 ];
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetForexPairBaseCurrency( string symbol )
        {
            return GetForexPairBaseCurrency( symbol, DefaultSeparatorSymbol );
        }

        static public bool IsActiveHour( string symbol, DateTime barTime )
        {
            string baseCurrency = GetForexPairBaseCurrency( symbol, DefaultSeparatorSymbol );
            string quoteCurrency = GetForexPairQuoteCurrency( symbol, DefaultSeparatorSymbol );

            if( baseCurrency == "EUR" ||
                    baseCurrency == "GBP" ||
                    quoteCurrency == "CHF" ||
                    quoteCurrency == "CAD" )
            {
                // Region 	City 	        Open (GMT) 	Close (GMT)
                // Europe 	London 	         8:00 am 	 5:00 pm
                //          Frankfurt 	     7:00 am 	 4:00 pm
                // America 	New York 	     1:00 pm 	10:00 pm
                //          Chicago 	     2:00 pm 	11:00 pm
                // Asia 	Tokyo 	        midnight 	 9:00 am
                //          Hong Kong 	     1:00 am 	10:00 am
                // Pacific 	Sydney 	        10:00 pm 	 7:00 am
                //          Wellington 	    10:00 pm 	 6:00 am                 

                TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
                DateTime barTimeEst = TimeZoneInfo.ConvertTimeFromUtc( barTime, est );

                if( barTimeEst.Hour >= 8 &&
                     barTimeEst.Hour <= 18 )
                {
                    return true;
                }

                TimeZoneInfo cest = TimeZoneInfo.FindSystemTimeZoneById( "Central Europe Standard Time" );
                DateTime barTimeCest = TimeZoneInfo.ConvertTimeFromUtc( barTime, cest );

                if( barTimeCest.Hour >= 8 &&
                     barTimeCest.Hour <= 18 )
                {
                    return true;
                }

                return ( false );
            }
            else if( quoteCurrency == "JPY" ||
                      quoteCurrency == "CNY" ||
                      baseCurrency == "AUD" )
            {
                if( ( barTime.Hour <= 10 ) &&
                     ( barTime.Hour >= 22 ) )
                {
                    return true;
                }

                return ( false );
            }

            return true;
        }

        /// <summary>
        /// Helper, automates the establishment of the base currency of a forex pair. (for ex. EUR/USD = USD)
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetForexPairBaseCurrency( string symbol, char separator )
        {
            string[ ] splits = symbol.Split( separator );

            if( splits.Length != 2 )
            {
                return null;
            }

            return splits[ 0 ];
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static fxSymbol CreateForexPairSymbol( string symbol )
        {
            return CreateForexPairSymbol( symbol, DefaultSeparatorSymbol );
        }

        /// <summary>
        /// Helper, helps create a Symbol from a forex pair mixed symbol.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static fxSymbol CreateForexPairSymbol( string symbol, char separator )
        {
            string[ ] splits = symbol.Split( separator );

            if( splits.Length != 2 )
            {
                return null;
            }

            return new fxSymbol( symbol );
        }

        /// <summary>
        /// This will try to split the current symbol in 2 currencies for a forex pair.
        /// </summary>
        /// <param name="currency1"></param>
        /// <param name="currency2"></param>
        /// <returns></returns>
        public bool SplitSymbolToCrosses( out string currency1, out string currency2 )
        {
            currency1 = string.Empty;
            currency2 = string.Empty;

            if( string.IsNullOrEmpty( SymbolString ) )
            {
                return false;
            }

            string name = SymbolString.ToUpper( );
            string[ ] currencyNames = Enum.GetNames( typeof( Currency ) );

            foreach( string currencyName in currencyNames )
            {
                if( name.StartsWith( currencyName.ToUpper( ) ) )
                {
                    // Found 1, try the other.
                    string subName = name.Substring( currencyName.Length );

                    foreach( string currencyName2 in currencyNames )
                    {
                        if( subName.Contains( currencyName2 ) )
                        {
                            // Found 2, we have a forex pair.
                            currency1 = currencyName;
                            currency2 = currencyName2;
                            return true;
                        }
                    }

                    // Failed to find second part.
                    return false;
                }
            }

            return false;
        }

        public bool MatchesSearchCriteria( string nameMatch )
        {
            if( nameMatch == "*" || string.IsNullOrEmpty( nameMatch ) )
            {
                return true;
            }
            return SymbolString.ToLower( ).Contains( nameMatch.ToLower( ) );
        }

        public override string ToString( )
        {
            return "Symbol [" + SymbolString + "]";
        }
    }
}