using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Net;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace StockSharp.Algo.History.Russian.Finam
{
    public class FinamHistorySource : BaseDumpableHistorySource, ISecurityDownloader
    {
        private static readonly TimeSpan[ ] timeSpan_0 = ( new int[7]
        {
      1,
      5,
      10,
      15,
      30,
      60,
      1440
        } ).Select< int, TimeSpan >( new Func< int, TimeSpan >( FinamHistorySource.Class114.class114_0.method_2 ) ).ToArray< TimeSpan >( );
        private static readonly SyncObject syncObject_0 = new SyncObject( );
        public const string NativeIdStorageName = "Finam";
        private static FinamHistorySource.Class113 class113_0;

        public FinamHistorySource(
          INativeIdStorage nativeIdStorage,
          IExchangeInfoProvider exchangeInfoProvider ) : base( nativeIdStorage, exchangeInfoProvider )
        {
        }

        public static IEnumerable< TimeSpan > TimeFrames
        {
            get
            {
                return timeSpan_0;
            }
        }

        private IEnumerable< T > method_0< T >(
          Security security_0,
          DateTime dateTime_0,
          DateTime dateTime_1,
          Type type_0,
          object object_0,
          Func< string > func_0,
          Func< FastCsvReader, T > func_1 )
        {
            FinamHistorySource.Class110< T > class110 = new FinamHistorySource.Class110< T >( );
            class110.func_0 = func_1;
            class110.finamHistorySource_0 = this;
            class110.security_0 = security_0;
            class110.dateTime_0 = dateTime_0;
            class110.dateTime_1 = dateTime_1;
            class110.type_0 = type_0;
            class110.object_0 = object_0;
            if( class110.func_0 == null )
            {
                throw new ArgumentNullException( "converter" );
            }

            TextReader reader = this.Process( class110.security_0, class110.dateTime_0, class110.dateTime_1, class110.type_0, class110.object_0, func_0 );
            using( reader )
            {
                FinamHistorySource.Class112< T > class112 = new FinamHistorySource.Class112< T >( );
                class112.class110_0 = class110;
                class112.fastCsvReader_0 = new FastCsvReader( reader );
                if( !class112.fastCsvReader_0.NextLine( ) )
                {
                    return Enumerable.Empty< T >( );
                }

                return CultureInfo.InvariantCulture.DoInCulture<List<T>>( new Func<List<T>>( class112.method_0 ) );
            }
        }

        public static IEnumerable< FinamSecurityInfo > DownloadSecurityInfo( )
        {
            string str1 = FinamHistorySource.smethod_5( "https://www.finam.ru/profile/moex-akcii/sberbank/export/".To< Uri >( ) );
            string str2 = string.Empty;
            int startIndex1 = str1.IndexOfIgnoreCase( "icharts/icharts.js" );
            if( startIndex1 != -1 )
            {
                int num = str1.LastIndexOf( "/cache/", startIndex1, StringComparison.InvariantCultureIgnoreCase );
                if( num == -1 )
                {
                    throw new InvalidOperationException( );
                }

                int startIndex2 = num + 7;
                str2 = str1.Substring( startIndex2, startIndex1 - startIndex2 - 1 );
                if( !str2.IsEmpty( ) )
                {
                    str2 += "/";
                }
            }
            string str3 = FinamHistorySource.smethod_5( ( "https://www.finam.ru/cache/" + str2 + "icharts/icharts.js" ).To< Uri >( ) );
            string[ ] strArray1 = str3.Split( Environment.NewLine, true );
            if( strArray1.Length < 5 )
            {
                throw new InvalidOperationException( str3 );
            }

            string[ ] strArray2 = FinamHistorySource.smethod_7( strArray1[ 0 ], "Id", "," );
            string[ ] strArray3 = FinamHistorySource.smethod_7( strArray1[ 1 ], "Name", "','" );
            string[ ] strArray4 = FinamHistorySource.smethod_7( strArray1[ 2 ], "Code", "','" );
            string[ ] strArray5 = FinamHistorySource.smethod_7( strArray1[ 3 ], "Market", "," );
            IDictionary< string, int > dictionary = strArray1[4].Remove( "var aEmitentDecp = {", false ).Remove( "};", false ).Split( ',' ).Select< string, KeyValuePair< string, int > >( FinamHistorySource.Class114.func_0 ?? ( FinamHistorySource.Class114.func_0 = new Func< string, KeyValuePair< string, int > >( FinamHistorySource.Class114.class114_0.method_0 ) ) ).Distinct< KeyValuePair< string, int > >( ).ToDictionary< string, int >( );
            List< FinamSecurityInfo > source = new List< FinamSecurityInfo >( strArray2.Length );
            for( int index = 0; index < strArray2.Length; ++index )
            {
                long result;
                if( long.TryParse( strArray2[ index ], out result ) )
                {
                    string str4 = strArray3[ index ];
                    int startIndex2 = str4.IndexOf( '(' );
                    int num = str4.IndexOf( ')' );
                    if( startIndex2 != -1 && num > startIndex2 )
                    {
                        str4 = str4.Remove( startIndex2, num - startIndex2 + 1 ).Trim( );
                    }

                    source.Add( new FinamSecurityInfo( )
                    {
                        FinamSecurityId = result,
                        FinamMarketId = strArray5[ index ].To< long >( ),
                        Code = strArray4[ index ],
                        Name = str4,
                        Decimals = dictionary[ strArray2[ index ] ]
                    } );
                }
            }
            return source.OrderByDescending<FinamSecurityInfo, long>( FinamHistorySource.Class114.func_1 ?? ( FinamHistorySource.Class114.func_1 = new Func<FinamSecurityInfo, long>( FinamHistorySource.Class114.class114_0.method_1 ) ) );
        }

        public override string GetDumpFile(
          Security security,
          DateTime from,
          DateTime to,
          Type dataType,
          object arg )
        {
            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            if( dataType == null )
            {
                throw new ArgumentNullException( nameof( dataType ) );
            }

            if( dataType != typeof( ExecutionMessage ) && !dataType.IsCandleMessage( ) )
            {
                throw new ArgumentOutOfRangeException( nameof( dataType ), dataType, LocalizedStrings.Str1655 );
            }

            return Path.Combine( this.DumpFolder, FinamHistorySource.smethod_4( security, this.NativeIdStorage ).ToString( ) + "_" + this.GetSecurityCode( security ).SecurityIdToFolderName( ), "{0}_{1}_{2:yyyy_MM_dd}_{3:yyyy_MM_dd}.txt".Put( ( object )dataType.Name.Remove( "Message", false ).ToLowerInvariant( ), arg is ExecutionTypes ? null : ( object )TraderHelper.CandleArgToFolderName( arg ), ( object )from, ( object )to ) );
        }

        private static Decimal smethod_0( double double_0, Security security_0 )
        {
            int? nullable = security_0.Decimals;
            if( !nullable.HasValue && security_0.PriceStep.HasValue )
            {
                nullable = new int?( security_0.PriceStep.Value.GetCachedDecimals( ) );
            }

            return Decimal.Round( ( Decimal )double_0, nullable ?? 4 );
        }

        public IEnumerable< ExecutionMessage > GetTicks(
          Security security,
          DateTime from,
          DateTime to,
          bool includeOrigin = false )
        {
            FinamHistorySource.Class109 class109 = new FinamHistorySource.Class109( )
            {
                security_0 = security,
                finamHistorySource_0 = this,
                dateTime_0 = from,
                dateTime_1 = to,
                bool_0 = includeOrigin
            };
            class109.securityId_0 = class109.security_0.ToSecurityId( this.SecurityIdGenerator );
            return this.method_0< ExecutionMessage >( class109.security_0, class109.dateTime_0, class109.dateTime_1, typeof( ExecutionMessage ), ExecutionTypes.Tick, new Func< string >( class109.method_0 ), new Func< FastCsvReader, ExecutionMessage >( class109.method_1 ) );
        }

        private static Sides? smethod_1( string string_1 )
        {
            string lowerInvariant = string_1?.ToLowerInvariant( );
            if( lowerInvariant == "b" )
            {
                return new Sides?( Sides.Buy );
            }

            if( !( lowerInvariant == "s" ) )
            {
                return new Sides?( );
            }

            return new Sides?( Sides.Sell );
        }

        private static DateTime smethod_2( FastCsvReader fastCsvReader_0 )
        {
            return fastCsvReader_0.ReadDateTime( "yyyyMMdd" ) + fastCsvReader_0.ReadTimeSpan( "hhmmss" );
        }

        private static long? smethod_3( long long_0 )
        {
            if( long_0 < 0L )
            {
                long num = int.MaxValue + long_0;
                long_0 = num >= 0L ? num : 42949672940L + long_0;
            }
            if( long_0 < 0L )
            {
                return new long?( );
            }

            return new long?( long_0 );
        }

        public IEnumerable< TimeFrameCandleMessage > GetCandles(
          Security security,
          TimeSpan timeFrame,
          DateTime from,
          DateTime to )
        {
            FinamHistorySource.Class111 class111 = new FinamHistorySource.Class111( )
            {
                security_0 = security,
                finamHistorySource_0 = this,
                dateTime_0 = from,
                dateTime_1 = to,
                timeSpan_0 = timeFrame
            };
            class111.exchangeBoard_0 = this.GetSecurityBoard( class111.security_0 );
            class111.securityId_0 = class111.security_0.ToSecurityId( this.SecurityIdGenerator );
            DateTime dateTime = DateTime.Today.AddDays( 1.0 );
            if( class111.dateTime_0 > dateTime )
            {
                return Enumerable.Empty< TimeFrameCandleMessage >( );
            }

            if( class111.dateTime_1 > dateTime )
            {
                class111.dateTime_1 = dateTime;
            }

            return this.method_0< TimeFrameCandleMessage >( class111.security_0, class111.dateTime_0, class111.dateTime_1, typeof( TimeFrameCandleMessage ), class111.timeSpan_0, new Func< string >( class111.method_0 ), new Func< FastCsvReader, TimeFrameCandleMessage >( class111.method_1 ) );
        }

        public void Refresh(
          ISecurityStorage securityStorage,
          Security criteria,
          Action< Security > newSecurity,
          Func< bool > isCancelled )
        {
            if( securityStorage == null )
            {
                throw new ArgumentNullException( nameof( securityStorage ) );
            }

            if( criteria == null )
            {
                throw new ArgumentNullException( nameof( criteria ) );
            }

            if( newSecurity == null )
            {
                throw new ArgumentNullException( nameof( newSecurity ) );
            }

            if( isCancelled == null )
            {
                throw new ArgumentNullException( nameof( isCancelled ) );
            }

            IEnumerable< FinamSecurityInfo > finamSecurityInfos = FinamHistorySource.DownloadSecurityInfo( );
            Dictionary< string, int > dictionary = new Dictionary< string, int >( );
            foreach( FinamSecurityInfo finamSecurityInfo in finamSecurityInfos )
            {
                if( isCancelled( ) )
                {
                    break;
                }

                if( finamSecurityInfo.FinamMarketId != 200L )
                {
                    string code = finamSecurityInfo.Code;
                    string name = finamSecurityInfo.Name;
                    if( ( criteria.Code.IsEmpty( ) || code.ContainsIgnoreCase( criteria.Code ) ) && ( criteria.Name.IsEmpty( ) || name.ContainsIgnoreCase( criteria.Name ) ) )
                    {
                        Tuple< long, long > tuple = Tuple.Create< long, long >( finamSecurityInfo.FinamMarketId, finamSecurityInfo.FinamSecurityId );
                        Security security1 = securityStorage.LookupByNativeId( this.NativeIdStorage, "Finam", tuple );
                        if( security1 == null )
                        {
                            Security security2 = new Security( )
                            {
                                Id = null,
                                Code = code,
                                Name = name
                            };
                            long finamMarketId = finamSecurityInfo.FinamMarketId;
                            SecurityTypes? type1;
                            if( finamMarketId <= 38L )
                            {
                                if( finamMarketId <= 20L )
                                {
                                    //switch ( finamMarketId - 1L )
                                    //{
                                    //    case 0:
                                    //        MoexDownloader.GetSecurities( security2.Code, new SecurityTypes?( SecurityTypes.Stock ), true ).FirstOrDefault<SecurityInfo>( )?.FillTo( security2, this.ExchangeInfoProvider );
                                    //        type1 = security2.Type;
                                    //        if ( !type1.HasValue )
                                    //            security2.Type = new SecurityTypes?( SecurityTypes.Stock );
                                    //        if ( ( Equatable<ExchangeBoard> )security2.Board == ( ExchangeBoard )null )
                                    //        {
                                    //            security2.Board = ExchangeBoard.Micex;
                                    //            goto label_41;
                                    //        }
                                    //        else
                                    //            goto label_41;
                                    //    case 1:
                                    //        security2.Type = new SecurityTypes?( SecurityTypes.Bond );
                                    //        security2.Board = ExchangeBoard.Micex;
                                    //        goto label_41;
                                    //    case 2:
                                    //        break;
                                    //    default:
                                    //        switch ( finamMarketId - 10L )
                                    //        {
                                    //            case 0:
                                    //            case 1:
                                    //            case 8:
                                    //            case 10:
                                    //                break;
                                    //            case 4:
                                    //            case 7:
                                    //                security2.Type = new SecurityTypes?( SecurityTypes.Future );
                                    //                security2.Board = ExchangeBoard.Forts;
                                    //                SecurityInfo securityInfo = MoexDownloader.GetSecurities( security2.Code, security2.Type, true ).FirstOrDefault<SecurityInfo>( );
                                    //                if ( securityInfo != null )
                                    //                {
                                    //                    securityInfo.FillTo( security2, this.ExchangeInfoProvider );
                                    //                    goto label_41;
                                    //                }
                                    //                else
                                    //                    goto label_41;
                                    //            default:
                                    //                goto label_37;
                                    //        }
                                    //}
                                }
                                else if( finamMarketId != 29L )
                                {
                                    if( finamMarketId != 38L )
                                    {
                                        goto label_37;
                                    }
                                }
                                else
                                {
                                    security2.Type = new SecurityTypes?( SecurityTypes.Fund );
                                    security2.Board = ExchangeBoard.Micex;
                                    goto label_41;
                                }
                                security2.Board = ExchangeBoard.Forts;
                                goto label_41;
                            }
                            else if( finamMarketId <= 45L )
                            {
                                if( finamMarketId == 41L || finamMarketId == 45L )
                                {
                                    security2.Type = new SecurityTypes?( SecurityTypes.Currency );
                                    security2.Board = ExchangeBoard.Micex;
                                    goto label_41;
                                }
                            }
                            else if( finamMarketId != 91L )
                            {
                                if( finamMarketId != 517L )
                                {
                                    if( finamMarketId == 520L )
                                    {
                                        security2.Type = new SecurityTypes?( SecurityTypes.CryptoCurrency );
                                        security2.Board = ExchangeBoard.Finam;
                                        goto label_41;
                                    }
                                }
                                else
                                {
                                    security2.Type = new SecurityTypes?( SecurityTypes.Stock );
                                    security2.Board = ExchangeBoard.Spb;
                                    goto label_41;
                                }
                            }
                            else
                            {
                                security2.Type = new SecurityTypes?( SecurityTypes.Index );
                                security2.Board = ExchangeBoard.Micex;
                                goto label_41;
                            }
                            label_37:
security2.Board = ExchangeBoard.Finam;
                            label_41:
type1 = criteria.Type;
                            if( type1.HasValue )
                            {
                                type1 = security2.Type;
                                SecurityTypes? type2 = criteria.Type;
                                if( !( type1.GetValueOrDefault( ) == type2.GetValueOrDefault( ) & type1.HasValue == type2.HasValue ) )
                                {
                                    continue;
                                }
                            }
                            security2.Id = this.SecurityIdGenerator.GenerateId( security2.Code, security2.Board );
                            int num;
                            if( dictionary.TryGetValue( security2.Id, out num ) )
                            {
                                ++num;
                                dictionary[ security2.Id ] = num;
                                security2.Code += ( string )( object )num;
                                security2.Id = this.SecurityIdGenerator.GenerateId( security2.Code, security2.Board );
                            }
                            else
                            {
                                dictionary.Add( security2.Id, 0 );
                            }

                            Security security3 = securityStorage.LookupById( security2.Id );
                            if( security3 != null )
                            {
                                security2 = security3;
                            }
                            else
                            {
                                newSecurity( security2 );
                            }

                            securityStorage.Save( security2, false );
                            this.NativeIdStorage.TryAdd( "Finam", security2.ToSecurityId( null ), tuple, true );
                        }
                        else if( finamSecurityInfo.FinamMarketId != FinamHistorySource.smethod_4( security1, this.NativeIdStorage ) )
                        {
                            securityStorage.Save( security1, false );
                            this.NativeIdStorage.TryAdd( "Finam", security1.ToSecurityId( null ), tuple, true );
                        }
                    }
                }
            }
        }

        private static long smethod_4( Security security_0, INativeIdStorage inativeIdStorage_1 )
        {
            if( security_0 == null )
            {
                throw new ArgumentNullException( "security" );
            }

            if( inativeIdStorage_1 == null )
            {
                throw new ArgumentNullException( "nativeIdStorage" );
            }

            Tuple< long, long > bySecurityId = ( Tuple< long, long > )inativeIdStorage_1.TryGetBySecurityId( "Finam", security_0.ToSecurityId( null ) );
            if( bySecurityId == null )
            {
                throw new ArgumentException( LocalizedStrings.Str2099Params.Put( security_0, "Finam (market)" ) );
            }

            return bySecurityId.Item1;
        }

        private static string smethod_5( Uri uri_0 )
        {
            if( uri_0 == null )
            {
                throw new ArgumentNullException( "url" );
            }

            lock( FinamHistorySource.syncObject_0 )
            {
                if( FinamHistorySource.class113_0 == null )
                {
                    FinamHistorySource.class113_0 = new FinamHistorySource.Class113( );
                }
            }
            return FinamHistorySource.class113_0.method_0( uri_0 );
        }

        private static Range< DateTimeOffset > smethod_6(
          TimeSpan timeSpan_1,
          DateTime dateTime_0,
          ExchangeBoard exchangeBoard_0 )
        {
            if( exchangeBoard_0 == null )
            {
                throw new ArgumentNullException( "board" );
            }

            WorkingTimePeriod period = exchangeBoard_0.WorkingTime.GetPeriod( dateTime_0 );
            if( period != null && period.Times.Count > 0 )
            {
                Range< TimeSpan > range = period.Times.First< Range< TimeSpan > >( );
                if( dateTime_0.TimeOfDay < range.Min )
                {
                    dateTime_0 = dateTime_0.Date + range.Min;
                }
            }
            return timeSpan_1.GetCandleBounds( dateTime_0.ApplyTimeZone( TimeHelper.Moscow ), exchangeBoard_0 );
        }

        internal static string[ ] smethod_7( string string_1, string string_2, string string_3 )
        {
            string[ ] strArray = string_1.Remove( "var aEmitent{0}s = [".Put( string_2 ), false ).Remove( "];", false ).Split( string_3, false );
            if( strArray.Length == 0 )
            {
                throw new InvalidOperationException( string_1 );
            }

            if( strArray[ 0 ].StartsWith( "'" ) )
            {
                strArray[ 0 ] = strArray[ 0 ].Substring( 1 );
            }

            int index = strArray.Length - 1;
            if( strArray[ index ].EndsWith( "'" ) )
            {
                strArray[ index ] = strArray[ index ].Substring( 0, strArray[ index ].Length - 1 );
            }

            return strArray;
        }

        private sealed class Class109
        {
            public Security security_0;
            public FinamHistorySource finamHistorySource_0;
            public DateTime dateTime_0;
            public DateTime dateTime_1;
            public bool bool_0;
            public SecurityId securityId_0;

            internal string method_0( )
            {
                return FinamHistorySource.smethod_5( new Class27( ).method_4( this.security_0, this.finamHistorySource_0.NativeIdStorage ).method_1( this.dateTime_0 ).method_2( this.dateTime_1 ).method_3( TimeSpan.Zero, this.bool_0 ).method_0( ) );
            }

            internal ExecutionMessage method_1( FastCsvReader fastCsvReader_0 )
            {
                return new ExecutionMessage( )
                {
                    ServerTime = FinamHistorySource.smethod_2( fastCsvReader_0 ).ApplyTimeZone( TimeHelper.Moscow ),
                    TradePrice = new Decimal?( FinamHistorySource.smethod_0( fastCsvReader_0.ReadDouble( ), this.security_0 ) ),
                    TradeVolume = new Decimal?( fastCsvReader_0.ReadDecimal( ) ),
                    ExecutionType = new ExecutionTypes?( ExecutionTypes.Tick ),
                    TradeId = FinamHistorySource.smethod_3( fastCsvReader_0.ReadLong( ) ),
                    SecurityId = this.securityId_0,
                    OriginSide = !this.bool_0 || fastCsvReader_0.ColumnCount <= 5 ? new Sides?( ) : FinamHistorySource.smethod_1( fastCsvReader_0.ReadString( ) )
                };
            }
        }

        private sealed class Class110< T >
        {
            public Func< FastCsvReader, T > func_0;
            public FinamHistorySource finamHistorySource_0;
            public Security security_0;
            public DateTime dateTime_0;
            public DateTime dateTime_1;
            public Type type_0;
            public object object_0;
        }

        private sealed class Class111
        {
            public Security security_0;
            public FinamHistorySource finamHistorySource_0;
            public DateTime dateTime_0;
            public DateTime dateTime_1;
            public TimeSpan timeSpan_0;
            public ExchangeBoard exchangeBoard_0;
            public SecurityId securityId_0;

            internal string method_0( )
            {
                return FinamHistorySource.smethod_5( new Class27( ).method_4( this.security_0, this.finamHistorySource_0.NativeIdStorage ).method_1( this.dateTime_0 ).method_2( this.dateTime_1 ).method_3( this.timeSpan_0, false ).method_0( ) );
            }

            internal TimeFrameCandleMessage method_1( FastCsvReader fastCsvReader_0 )
            {
                Range< DateTimeOffset > range = FinamHistorySource.smethod_6( this.timeSpan_0, FinamHistorySource.smethod_2( fastCsvReader_0 ), this.exchangeBoard_0 );
                TimeFrameCandleMessage frameCandleMessage = new TimeFrameCandleMessage( );
                frameCandleMessage.SecurityId = this.securityId_0;
                frameCandleMessage.OpenTime = range.Min;
                frameCandleMessage.CloseTime = range.Max;
                frameCandleMessage.OpenPrice = FinamHistorySource.smethod_0( fastCsvReader_0.ReadDouble( ), this.security_0 );
                frameCandleMessage.HighPrice = FinamHistorySource.smethod_0( fastCsvReader_0.ReadDouble( ), this.security_0 );
                frameCandleMessage.LowPrice = FinamHistorySource.smethod_0( fastCsvReader_0.ReadDouble( ), this.security_0 );
                frameCandleMessage.ClosePrice = FinamHistorySource.smethod_0( fastCsvReader_0.ReadDouble( ), this.security_0 );
                frameCandleMessage.TotalVolume = fastCsvReader_0.ReadDecimal( );
                frameCandleMessage.Arg = timeSpan_0;
                frameCandleMessage.State = CandleStates.Finished;
                return frameCandleMessage;
            }
        }

        private sealed class Class112< T >
        {
            public FastCsvReader fastCsvReader_0;
            public FinamHistorySource.Class110< T > class110_0;

            internal List< T > method_0( )
            {
                List< T > objList = new List< T >( );
                do
                {
                    try
                    {
                        objList.Add( this.class110_0.func_0( this.fastCsvReader_0 ) );
                    }
                    catch( Exception ex )
                    {
                        string dumpFile = this.class110_0.finamHistorySource_0.GetDumpFile( this.class110_0.security_0, this.class110_0.dateTime_0, this.class110_0.dateTime_1, this.class110_0.type_0, this.class110_0.object_0 );
                        if( this.class110_0.finamHistorySource_0.CanDump )
                        {
                            System.IO.File.Delete( dumpFile );
                        }

                        throw new InvalidOperationException( LocalizedStrings.Str2098Params.Put( dumpFile, fastCsvReader_0.CurrentLine ), ex );
                    }
                }
                while ( this.fastCsvReader_0.NextLine( ) );
                return objList;
            }
        }

        private sealed class Class113 : MarshalByRefObject
        {
            public Class113( )
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }

            public string method_0( Uri uri_0 )
            {
                using( WebClientEx webClientEx = new WebClientEx( )
                {
                    Timeout = TimeSpan.FromMinutes( 3.0 ),
                    DecompressionMethods = DecompressionMethods.GZip | DecompressionMethods.Deflate
                } )
                {
                    webClientEx.Headers.Add( HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch" );
                    return webClientEx.DownloadString( uri_0 );
                }
            }

            public override object InitializeLifetimeService( )
            {
                return null;
            }
        }

        [Serializable]
        private sealed class Class114
        {
            public static readonly FinamHistorySource.Class114 class114_0 = new FinamHistorySource.Class114( );
            public static Func< string, KeyValuePair< string, int > > func_0;
            public static Func< FinamSecurityInfo, long > func_1;

            internal KeyValuePair< string, int > method_0( string string_0 )
            {
                return new KeyValuePair< string, int >( string_0.Split( ':' )[ 0 ], string_0.Split( ':' )[ 1 ].To< int >( ) );
            }

            internal long method_1( FinamSecurityInfo finamSecurityInfo_0 )
            {
                return finamSecurityInfo_0.FinamSecurityId;
            }

            internal TimeSpan method_2( int int_0 )
            {
                return TimeSpan.FromMinutes( int_0 );
            }
        }
    }
}
