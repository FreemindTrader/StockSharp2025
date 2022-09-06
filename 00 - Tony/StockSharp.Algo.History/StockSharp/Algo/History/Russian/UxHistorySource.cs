using Ecng.Common;
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
using xNet;

namespace StockSharp.Algo.History.Russian
{
    public class UxHistorySource : BaseDumpableHistorySource, ISecurityDownloader
    {
        internal static readonly TimeZoneInfo timeZoneInfo_0 = TimeZoneInfo.FindSystemTimeZoneById( "FLE Standard Time" );
        private static readonly TimeSpan[] timeSpan_0 = ( new long[7]
    {
      600000000L,
      3000000000L,
      6000000000L,
      9000000000L,
      18000000000L,
      36000000000L,
      864000000000L
    } ).Select< long, TimeSpan >( new Func< long, TimeSpan >( UxHistorySource.Class148.class148_0.method_0 ) ).ToArray< TimeSpan >( );

        public UxHistorySource(
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

            return Path.Combine( this.DumpFolder, this.GetSecurityCode( security ).SecurityIdToFolderName( ), "{0}_{1}_{2:yyyy_MM_dd}_{3:yyyy_MM_dd}.txt".Put( dataType.Name.Remove( "Message", false ).ToLowerInvariant(), ( object ) TraderHelper.CandleArgToFolderName( arg ), from, to ) );
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

            using( HttpRequest httpRequest = new HttpRequest( ) )
            {
                UxHistorySource.Class147 class147 = new UxHistorySource.Class147( );
                httpRequest.UserAgent = xNet.Http.ChromeUserAgent( );
                httpRequest.Referer = "http://www.ux.ua/";
                string content = StringHelper.WindowsCyrillic.GetString( httpRequest.Get( "http://www.ux.ua/ru/issues-csv.aspx?st=0&sby=4", null ).ToBytes( ) );
                class147.fastCsvReader_0 = new FastCsvReader( content )
        {
          ColumnSeparator = ',',
          LineSeparator = "\n"
        };
                class147.fastCsvReader_0.NextLine( );
                while( class147.fastCsvReader_0.NextLine( ) )
                {
                    UxHistorySource.Class146 class146 = new UxHistorySource.Class146( );
                    class146.class147_0 = class147;
                    if( isCancelled( ) )
                    {
                        break;
                    }

                    class146.string_1 = class146.class147_0.fastCsvReader_0.ReadString( );
                    class146.exchangeBoard_0 = class146.string_1.StartsWith( "UX" ) ? ExchangeBoard.Ux : ExchangeBoard.UxStock;
                    if( criteria.Code.IsEmpty( ) || class146.string_1.ContainsIgnoreCase( criteria.Code ) )
                    {
                        class146.string_0 = this.SecurityIdGenerator.GenerateId( class146.string_1, class146.exchangeBoard_0 );
                        class146.security_0 = securityStorage.LookupById( class146.string_0 );
                        if( class146.security_0 == null )
                        {
                            CultureInfo.InvariantCulture.DoInCulture( new Action( class146.method_0 ) );
                            securityStorage.Save( class146.security_0, false );
                            newSecurity( class146.security_0 );
                        }
                    }
                }
            }
        }

        public IEnumerable< ExecutionMessage > GetTicks(
      Security security,
      DateTime from,
      DateTime to )
        {
            UxHistorySource.Class145 class145 = new UxHistorySource.Class145( )
      {
        security_0 = security
      };
            class145.securityId_0 = class145.security_0.ToSecurityId( this.SecurityIdGenerator );
            return this.method_0< ExecutionMessage >( class145.security_0, from, to, typeof( ExecutionMessage ), ExecutionTypes.Tick, this.method_1( class145.security_0, TimeSpan.FromTicks( 1L ), from, to ), new Func< FastCsvReader, ExecutionMessage >( class145.method_0 ) );
        }

        public IEnumerable< TimeFrameCandleMessage > GetCandles(
      Security security,
      TimeSpan timeFrame,
      DateTime from,
      DateTime to )
        {
            UxHistorySource.Class149 class149 = new UxHistorySource.Class149( )
      {
        timeSpan_0 = timeFrame,
        security_0 = security
      };
            class149.securityId_0 = class149.security_0.ToSecurityId( this.SecurityIdGenerator );
            return this.method_0< TimeFrameCandleMessage >( class149.security_0, from, to, typeof( TimeFrameCandleMessage ), class149.timeSpan_0, this.method_1( class149.security_0, class149.timeSpan_0, from, to ), new Func< FastCsvReader, TimeFrameCandleMessage >( class149.method_0 ) );
        }

        private static DateTimeOffset smethod_0( FastCsvReader fastCsvReader_0 )
        {
            return ( fastCsvReader_0.ReadDateTime( "yyyyMMdd" ) + fastCsvReader_0.ReadTimeSpan( "h\\:m\\:s\\.fff" ) ).ApplyTimeZone( UxHistorySource.timeZoneInfo_0 );
        }

        private IEnumerable< T > method_0< T >(
      Security security_0,
      DateTime dateTime_0,
      DateTime dateTime_1,
      Type type_0,
      object object_0,
      string string_1,
      Func< FastCsvReader, T > func_0 )
        {
            UxHistorySource.Class144< T > class144 = new UxHistorySource.Class144< T >( );
            class144.string_0 = string_1;
            class144.func_0 = func_0;
            class144.uxHistorySource_0 = this;
            class144.security_0 = security_0;
            class144.dateTime_0 = dateTime_0;
            class144.dateTime_1 = dateTime_1;
            class144.type_0 = type_0;
            class144.object_0 = object_0;
            if( class144.string_0.IsEmpty( ) )
            {
                throw new ArgumentNullException( "url" );
            }

            if( class144.func_0 == null )
            {
                throw new ArgumentNullException( "converter" );
            }

            TextReader reader = this.Process( class144.security_0, class144.dateTime_0, class144.dateTime_1, class144.type_0, class144.object_0, new Func< string >( class144.method_0 ) );
            using( reader )
            {
                UxHistorySource.Class143< T > class143 = new UxHistorySource.Class143< T >( );
                class143.class144_0 = class144;
                class143.fastCsvReader_0 = new FastCsvReader( reader );
                do
                {
                    ;
                }
                while (class143.fastCsvReader_0.NextLine( ) && class143.fastCsvReader_0.CurrentLine.IsEmpty( ));
                if( !class143.fastCsvReader_0.CurrentLine.IsEmpty( ) && !( class143.fastCsvReader_0.CurrentLine == "������� �� ������" ) )
                {
                    return CultureInfo.InvariantCulture.DoInCulture<List<T>>( new Func<List<T>>( class143.method_0 ) );
                }

                return Enumerable.Empty< T >( );
            }
        }

        private string method_1(
      Security security_0,
      TimeSpan timeSpan_1,
      DateTime dateTime_0,
      DateTime dateTime_1 )
        {
            if( security_0 == null )
            {
                throw new ArgumentNullException( "security" );
            }

            int num1 = 0;
            if( timeSpan_1.Ticks != 1L )
            {
                int num2 = UxHistorySource.timeSpan_0.IndexOf< TimeSpan >( timeSpan_1 );
                if( num2 == -1 )
                {
                    throw new ArgumentOutOfRangeException( "timeFrame", timeSpan_1, LocalizedStrings.Str2102 );
                }

                num1 = num2 + 1;
            }
            return "http://mdata.ux.ua/qdata.aspx?code={0}&pb={1:ddMMyyyy}&pe={2:ddMMyyyy}&p={3}&mk=1&ext=0&sep=1&div=2&df=5&tf=6&ih=0".Put( this.GetSecurityCode( security_0 ), dateTime_0, dateTime_1, num1 );
        }

        private sealed class Class143< T >
        {
            public FastCsvReader fastCsvReader_0;
            public UxHistorySource.Class144< T > class144_0;

            internal List< T > method_0( )
            {
                List< T > objList = new List< T >( );
                do
                {
                    try
                    {
                        objList.Add( this.class144_0.func_0( this.fastCsvReader_0 ) );
                    }
                    catch( Exception ex )
                    {
                        string dumpFile = this.class144_0.uxHistorySource_0.GetDumpFile( this.class144_0.security_0, this.class144_0.dateTime_0, this.class144_0.dateTime_1, this.class144_0.type_0, this.class144_0.object_0 );
                        if( this.class144_0.uxHistorySource_0.CanDump )
                        {
                            System.IO.File.Delete( dumpFile );
                        }

                        throw new InvalidOperationException( LocalizedStrings.Str2098Params.Put( dumpFile, fastCsvReader_0.CurrentLine ), ex );
                    }
                }
                while (this.fastCsvReader_0.NextLine( ));
                return objList;
            }
        }

        private sealed class Class144< T >
        {
            public string string_0;
            public Func< FastCsvReader, T > func_0;
            public UxHistorySource uxHistorySource_0;
            public Security security_0;
            public DateTime dateTime_0;
            public DateTime dateTime_1;
            public Type type_0;
            public object object_0;

            internal string method_0( )
            {
                HttpWebRequest httpWebRequest = ( HttpWebRequest ) WebRequest.Create( this.string_0 );
                httpWebRequest.ProtocolVersion = HttpVersion.Version10;
                using( HttpWebResponse response = ( HttpWebResponse ) httpWebRequest.GetResponse( ) )
                {
                    using( Stream responseStream = response.GetResponseStream( ) )
                    {
                        using( StreamReader streamReader = new StreamReader( responseStream ) )
                        {
                            return streamReader.ReadToEnd( );
                        }
                    }
                }
            }
        }

        private sealed class Class145
        {
            public SecurityId securityId_0;
            public Security security_0;

            internal ExecutionMessage method_0( FastCsvReader fastCsvReader_0 )
            {
                fastCsvReader_0.Skip( 2 );
                DateTimeOffset dateTimeOffset = UxHistorySource.smethod_0( fastCsvReader_0 );
                ExecutionMessage executionMessage = new ExecutionMessage( );
                executionMessage.ExecutionType = new ExecutionTypes?( ExecutionTypes.Tick );
                executionMessage.SecurityId = this.securityId_0;
                executionMessage.ServerTime = dateTimeOffset;
                executionMessage.TradePrice = new Decimal?( fastCsvReader_0.ReadDecimal( ) );
                Decimal num = fastCsvReader_0.ReadDecimal( );
                Decimal? multiplier = this.security_0.Multiplier;
                executionMessage.TradeVolume = new Decimal?( ( multiplier.HasValue ? new Decimal?( num / multiplier.GetValueOrDefault( ) ) : new Decimal?( ) ) ?? Decimal.One );
                return executionMessage;
            }
        }

        private sealed class Class146
        {
            public Security security_0;
            public string string_0;
            public string string_1;
            public ExchangeBoard exchangeBoard_0;
            public UxHistorySource.Class147 class147_0;

            internal void method_0( )
            {
                this.class147_0.fastCsvReader_0.Skip( 1 );
                string str1 = this.class147_0.fastCsvReader_0.ReadString( );
                this.class147_0.fastCsvReader_0.Skip( 2 );
                string str2 = this.class147_0.fastCsvReader_0.ReadString( );
                this.class147_0.fastCsvReader_0.ReadDecimal( );
                int num = this.class147_0.fastCsvReader_0.ReadInt( );
                this.security_0 = new Security( )
        {
          Id = this.string_0,
          Code = this.string_1,
          Board = this.exchangeBoard_0,
          Name = str1,
          Currency = new CurrencyTypes?( CurrencyTypes.UAH ),
          ExternalId = new SecurityExternalId( )
          {
            Isin = str2
          },
          PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, 4 ) ),
          Multiplier = new Decimal?( num )
        };
            }
        }

        private sealed class Class147
        {
            public FastCsvReader fastCsvReader_0;
        }

        [Serializable]
    private sealed class Class148
    {
        public static readonly UxHistorySource.Class148 class148_0 = new UxHistorySource.Class148( );

        internal TimeSpan method_0( long long_0 )
        {
            return long_0.To< TimeSpan >( );
        }
    }

        private sealed class Class149
        {
            public SecurityId securityId_0;
            public TimeSpan timeSpan_0;
            public Security security_0;

            internal TimeFrameCandleMessage method_0( FastCsvReader fastCsvReader_0 )
            {
                fastCsvReader_0.Skip( 2 );
                DateTimeOffset dateTimeOffset = UxHistorySource.smethod_0( fastCsvReader_0 );
                TimeFrameCandleMessage frameCandleMessage = new TimeFrameCandleMessage( );
                frameCandleMessage.SecurityId = this.securityId_0;
                frameCandleMessage.OpenTime = dateTimeOffset;
                frameCandleMessage.CloseTime = dateTimeOffset + this.timeSpan_0;
                frameCandleMessage.TimeFrame = this.timeSpan_0;
                frameCandleMessage.OpenPrice = fastCsvReader_0.ReadDecimal( );
                frameCandleMessage.HighPrice = fastCsvReader_0.ReadDecimal( );
                frameCandleMessage.LowPrice = fastCsvReader_0.ReadDecimal( );
                frameCandleMessage.ClosePrice = fastCsvReader_0.ReadDecimal( );
                Decimal num = fastCsvReader_0.ReadDecimal( );
                Decimal? multiplier = this.security_0.Multiplier;
                frameCandleMessage.TotalVolume = ( multiplier.HasValue ? new Decimal?( num / multiplier.GetValueOrDefault( ) ) : new Decimal?( ) ) ?? Decimal.One;
                frameCandleMessage.State = CandleStates.Finished;
                return frameCandleMessage;
            }
        }
    }
}
