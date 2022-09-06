using Ecng.Common;
using Ecng.Interop;
using Ecng.Net;
using Ecng.Serialization;
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
using System.Runtime.Serialization;
using xNet;

namespace StockSharp.Algo.History.Forex
{
    public class DukasCopyHistorySource : BaseDumpableHistorySource, ISecurityDownloader
    {
        private static readonly TimeSpan timeSpan_0 = TimeSpan.FromTicks( 1L );
        private static readonly TimeSpan timeSpan_1 = TimeSpan.FromMinutes( 1.0 );
        private static readonly TimeSpan timeSpan_2 = TimeSpan.FromHours( 1.0 );
        private static readonly TimeSpan timeSpan_3 = TimeSpan.FromDays( 1.0 );
        private static readonly TimeSpan[ ] timeSpan_4 = new TimeSpan[ 3 ]
        {
      DukasCopyHistorySource.timeSpan_1,
      DukasCopyHistorySource.timeSpan_2,
      DukasCopyHistorySource.timeSpan_3
        };

        public DukasCopyHistorySource(
          INativeIdStorage nativeIdStorage,
          IExchangeInfoProvider exchangeInfoProvider ) : base( nativeIdStorage, exchangeInfoProvider )
        {
        }

        public static IEnumerable< TimeSpan > TimeFrames
        {
            get
            {
                return timeSpan_4;
            }
        }

        public void Refresh(
          ISecurityStorage securityStorage,
          Security criteria,
          Action< Security > newSecurity,
          Func< bool > isCancelled )
        {
            DukasCopyHistorySource.Class43 class43 = new DukasCopyHistorySource.Class43( );
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

            using( HttpRequest httpRequest = new HttpRequest( )
            {
                UserAgent = xNet.Http.ChromeUserAgent( ),
                Referer = "http://freeserv.dukascopy.com/2.0/?path=historical_data_feed/index&width=100%25&height=600"
            } )
            {
                HttpResponse httpResponse = httpRequest.Get( "http://freeserv.dukascopy.com/2.0/index.php?path=common%2Finstruments&jsonp=_callbacks_._0{0}".Put( ( ( long )TimeHelper.UnixNowMls ).ToRadix( 36 ).ToLowerInvariant() ), null );
                class43.string_0 = httpResponse.ToString( );
            }
            class43.string_0 = class43.string_0.Substring( class43.string_0.IndexOf( '(' ) + 1 );
            class43.string_0 = class43.string_0.Substring( 0, class43.string_0.Length - 1 );
            foreach( KeyValuePair< string, DukasCopyHistorySource.DukasSecurity > security1 in CultureInfo.InstalledUICulture.DoInCulture< DukasCopyHistorySource.DukasRoot >( new Func< DukasCopyHistorySource.DukasRoot >( class43.method_0 ) ).Securities )
            {
                if( isCancelled( ) )
                {
                    break;
                }

                string key = security1.Key;
                DukasCopyHistorySource.DukasSecurity dukasSecurity = security1.Value;
                if( ( criteria.Code.IsEmpty( ) || key.ContainsIgnoreCase( criteria.Code ) ) && ( criteria.Name.IsEmpty( ) || dukasSecurity.Description.ContainsIgnoreCase( criteria.Name ) ) )
                {
                    string id = this.SecurityIdGenerator.GenerateId( key, ExchangeBoard.DukasCopy );
                    if( securityStorage.LookupById( id ) == null )
                    {
                        Security security2 = new Security( )
                        {
                            Id = id,
                            Code = key,
                            Name = dukasSecurity.Description,
                            PriceStep = new Decimal?( dukasSecurity.PriceStep ),
                            Board = ExchangeBoard.DukasCopy,
                            Type = new SecurityTypes?( SecurityTypes.Currency )
                        };
                        securityStorage.Save( security2, false );
                        newSecurity( security2 );
                    }
                }
            }
        }

        private string method_0(
          Security security_0,
          TimeSpan timeSpan_5,
          DateTime dateTime_0,
          Level1Fields level1Fields_0 )
        {
            if( security_0 == null )
            {
                throw new ArgumentNullException( "security" );
            }

            string str1;
            if( timeSpan_5 == DukasCopyHistorySource.timeSpan_0 )
            {
                str1 = "{0}/{1:00}/{2:00}/{3:00}h_ticks".Put( dateTime_0.Year, dateTime_0.Month - 1, dateTime_0.Day, dateTime_0.Hour );
            }
            else
            {
                string str2 = level1Fields_0 == Level1Fields.BestBidPrice ? "BID" : "ASK";
                if( timeSpan_5 == DukasCopyHistorySource.timeSpan_1 )
                {
                    str1 = "{0}/{1:00}/{2:00}/{3}_candles_min_1".Put( dateTime_0.Year, dateTime_0.Month - 1, dateTime_0.Day, str2 );
                }
                else if( timeSpan_5 == DukasCopyHistorySource.timeSpan_2 )
                {
                    str1 = "{0}/{1:00}/{2}_candles_hour_1".Put( dateTime_0.Year, dateTime_0.Month - 1, str2 );
                }
                else
                {
                    if( !( timeSpan_5 == DukasCopyHistorySource.timeSpan_3 ) )
                    {
                        throw new ArgumentOutOfRangeException( "timeFrame", timeSpan_5, LocalizedStrings.Str2077 );
                    }

                    str1 = "{0}/{1}_candles_day_1".Put( dateTime_0.Year, str2 );
                }
            }
            return "http://www.dukascopy.com/datafeed/{0}/".Put( this.GetSecurityCode( security_0 ).Remove( "/", false ) ) + str1 + ".bi5";
        }

        public IEnumerable< MarketDepth > LoadTicks(
          Security security,
          DateTime date )
        {
            DukasCopyHistorySource.Class42 class42 = new DukasCopyHistorySource.Class42( );
            class42.security_0 = security;
            return this.LoadTickMessages( class42.security_0, date ).Select< Level1ChangeMessage, MarketDepth >( new Func< Level1ChangeMessage, MarketDepth >( class42.method_0 ) );
        }

        public IEnumerable< Level1ChangeMessage > LoadTickMessages(
          Security security,
          DateTime date )
        {
            List< Level1ChangeMessage > level1ChangeMessageList = new List< Level1ChangeMessage >( );
            for( int index = 0; index < 24; ++index )
            {
                level1ChangeMessageList.AddRange( this.method_1( security, date.Date.AddHours( index ) ) );
            }

            return level1ChangeMessageList;
        }

        private IEnumerable< Level1ChangeMessage > method_1(
          Security security_0,
          DateTime dateTime_0 )
        {
            string str = null;
            if( this.CanDump )
            {
                str = this.method_3( security_0.ToSecurityId( null ), dateTime_0, dateTime_0, typeof( Level1ChangeMessage ), null );
                if( System.IO.File.Exists( str ) )
                {
                    return DukasCopyHistorySource.smethod_0( security_0, dateTime_0, System.IO.File.ReadAllBytes( str ) );
                }
            }
            using( WebClient webClient = new WebClient( ) )
            {
                try
                {
                    byte[ ] numArray = webClient.DownloadData( this.method_0( security_0, DukasCopyHistorySource.timeSpan_0, dateTime_0, Level1Fields.BestBidPrice ) );
                    if( str != null )
                    {
                        str.CreateDirIfNotExists( );
                        numArray.Save( str );
                    }
                    return DukasCopyHistorySource.smethod_0( security_0, dateTime_0, numArray );
                }
                catch( WebException ex )
                {
                    if( ex.Response == null )
                    {
                        throw;
                    }
                    else if( ( ( HttpWebResponse )ex.Response ).StatusCode != System.Net.HttpStatusCode.NotFound )
                    {
                        throw;
                    }
                }
                return Enumerable.Empty< Level1ChangeMessage >( );
            }
        }

        public IEnumerable< TimeFrameCandleMessage > LoadCandles(
          Security security,
          TimeSpan timeFrame,
          DateTime date,
          Level1Fields field )
        {
            string str = null;
            if( this.CanDump )
            {
                str = this.method_3( security.ToSecurityId( null ), date, date, typeof( TimeFrameCandleMessage ), timeFrame );
                if( System.IO.File.Exists( str ) )
                {
                    return this.method_2( security, timeFrame, date, System.IO.File.ReadAllBytes( str ) );
                }
            }
            using( WebClient webClient = new WebClient( ) )
            {
                try
                {
                    byte[ ] numArray = webClient.DownloadData( this.method_0( security, timeFrame, date, field ) );
                    if( str != null )
                    {
                        str.CreateDirIfNotExists( );
                        numArray.Save( str );
                    }
                    return this.method_2( security, timeFrame, date, numArray );
                }
                catch( WebException ex )
                {
                    if( ex.Response == null )
                    {
                        throw;
                    }
                    else if( ( ( HttpWebResponse )ex.Response ).StatusCode != System.Net.HttpStatusCode.NotFound )
                    {
                        throw;
                    }
                }
                return Enumerable.Empty< TimeFrameCandleMessage >( );
            }
        }

        private IEnumerable< TimeFrameCandleMessage > method_2(
          Security security_0,
          TimeSpan timeSpan_5,
          DateTime dateTime_0,
          byte[ ] byte_0 )
        {
            if( security_0 == null )
            {
                throw new ArgumentNullException( "security" );
            }

            if( byte_0.Length == 0 )
            {
                return Enumerable.Empty< TimeFrameCandleMessage >( );
            }

            byte_0 = DukasCopyHistorySource.smethod_1( SevenZip.Extract( byte_0 ) );
            DateTime dateTime = new DateTime( dateTime_0.Year, timeSpan_5 == DukasCopyHistorySource.timeSpan_3 ? 1 : dateTime_0.Month, timeSpan_5 == DukasCopyHistorySource.timeSpan_2 ? 1 : dateTime_0.Day );
            TimeFrameCandleMessage[ ] frameCandleMessageArray1 = new TimeFrameCandleMessage[ byte_0.Length / 24 ];
            Decimal num1 = security_0.PriceStep ?? new Decimal( 1, 0, 0, false, 2 );
            SecurityId securityId = security_0.ToSecurityId( this.SecurityIdGenerator );
            for( int index1 = 0; index1 < frameCandleMessageArray1.Length; ++index1 )
            {
                int startIndex = index1 * 24;
                DateTime dt = dateTime.AddSeconds( BitConverter.ToUInt32( byte_0, startIndex ) );
                Decimal num2 = BitConverter.ToUInt32( byte_0, startIndex + 4 ) * num1;
                Decimal num3 = BitConverter.ToUInt32( byte_0, startIndex + 8 ) * num1;
                Decimal num4 = BitConverter.ToUInt32( byte_0, startIndex + 12 ) * num1;
                Decimal num5 = BitConverter.ToUInt32( byte_0, startIndex + 16 ) * num1;
                Decimal num6 = Math.Round( ( Decimal )( ( double )BitConverter.ToSingle( byte_0, startIndex + 20 ) ), 2, MidpointRounding.AwayFromZero );
                TimeFrameCandleMessage[ ] frameCandleMessageArray2 = frameCandleMessageArray1;
                int index2 = index1;
                TimeFrameCandleMessage frameCandleMessage = new TimeFrameCandleMessage( );
                frameCandleMessage.OpenTime = dt.ApplyTimeZone( TimeZoneInfo.Utc );
                frameCandleMessage.OpenPrice = num2;
                frameCandleMessage.HighPrice = num5;
                frameCandleMessage.LowPrice = num4;
                frameCandleMessage.ClosePrice = num3;
                frameCandleMessage.TotalVolume = num6;
                frameCandleMessage.Arg = timeSpan_5;
                frameCandleMessage.SecurityId = securityId;
                frameCandleMessage.State = CandleStates.Finished;
                frameCandleMessageArray2[ index2 ] = frameCandleMessage;
            }
            return frameCandleMessageArray1;
        }

        private static IEnumerable< Level1ChangeMessage > smethod_0(
          Security security_0,
          DateTime dateTime_0,
          byte[ ] byte_0 )
        {
            if( security_0 == null )
            {
                throw new ArgumentNullException( "security" );
            }

            if( byte_0.Length == 0 )
            {
                return Enumerable.Empty< Level1ChangeMessage >( );
            }

            byte_0 = DukasCopyHistorySource.smethod_1( SevenZip.Extract( byte_0 ) );
            DateTime dateTime = dateTime_0.Truncate( TimeSpan.FromHours( 1.0 ) );
            int capacity = byte_0.Length / 20;
            List< Level1ChangeMessage > level1ChangeMessageList1 = new List< Level1ChangeMessage >( capacity );
            Decimal? priceStep = security_0.PriceStep;
            SecurityId securityId = security_0.ToSecurityId( null );
            for( int index = 0; index < capacity; ++index )
            {
                int startIndex = index * 20;
                DateTime dt = dateTime.AddMilliseconds( BitConverter.ToUInt32( byte_0, startIndex ) );
                Decimal uint32_1 = BitConverter.ToUInt32( byte_0, startIndex + 4 );
                Decimal? nullable1 = priceStep;
                Decimal? nullable2 = nullable1.HasValue ? new Decimal?( uint32_1 * nullable1.GetValueOrDefault( ) ) : new Decimal?( );
                Decimal uint32_2 = BitConverter.ToUInt32( byte_0, startIndex + 8 );
                nullable1 = priceStep;
                Decimal? nullable3 = nullable1.HasValue ? new Decimal?( uint32_2 * nullable1.GetValueOrDefault( ) ) : new Decimal?( );
                Decimal num1 = Math.Round( ( Decimal )( ( double )BitConverter.ToSingle( byte_0, startIndex + 12 ) ), 2, MidpointRounding.AwayFromZero );
                Decimal num2 = Math.Round( ( Decimal )( ( double )BitConverter.ToSingle( byte_0, startIndex + 16 ) ), 2, MidpointRounding.AwayFromZero );
                nullable1 = nullable2;
                Decimal num3 = new Decimal( );
                if( nullable1.GetValueOrDefault( ) == num3 & nullable1.HasValue )
                {
                    nullable1 = nullable3;
                    Decimal num4 = new Decimal( );
                    if( nullable1.GetValueOrDefault( ) == num4 & nullable1.HasValue )
                    {
                        continue;
                    }
                }
                List< Level1ChangeMessage > level1ChangeMessageList2 = level1ChangeMessageList1;
                Level1ChangeMessage message = new Level1ChangeMessage( );
                message.ServerTime = dt.ApplyTimeZone( TimeZoneInfo.Utc );
                message.SecurityId = securityId;
                Level1ChangeMessage level1ChangeMessage = message.TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.BestBidPrice, nullable3, false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.BestBidVolume, num2, false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.BestAskPrice, nullable2, false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.BestAskVolume, num1, false );
                level1ChangeMessageList2.Add( level1ChangeMessage );
            }
            return level1ChangeMessageList1;
        }

        private static byte[ ] smethod_1( byte[ ] byte_0 )
        {
            for( int index = 0; index < byte_0.Length; index += 4 )
            {
                DukasCopyHistorySource.smethod_2( ref byte_0[ index ], ref byte_0[ index + 3 ] );
                DukasCopyHistorySource.smethod_2( ref byte_0[ index + 1 ], ref byte_0[ index + 2 ] );
            }
            return byte_0;
        }

        private static void smethod_2( ref byte byte_0, ref byte byte_1 )
        {
            byte num = byte_0;
            byte_0 = byte_1;
            byte_1 = num;
        }

        public override string GetDumpFile(
          Security security,
          DateTime from,
          DateTime to,
          Type dataType,
          object arg )
        {
            return this.method_3( security.ToSecurityId( null ), from, to, dataType, arg );
        }

        private string method_3(
          SecurityId securityId_0,
          DateTime dateTime_0,
          DateTime dateTime_1,
          Type type_0,
          object object_0 )
        {
            if( type_0 == null )
            {
                throw new ArgumentNullException( "dataType" );
            }

            if( type_0 != typeof( Level1ChangeMessage ) && !type_0.IsCandleMessage( ) )
            {
                throw new ArgumentOutOfRangeException( "dataType", type_0, LocalizedStrings.Str1655 );
            }

            return Path.Combine( this.DumpFolder, "{0}_{1}_{2}_{3:yyyy_MM_dd_HH_mm}.zip".Put( type_0.Name.Remove( "Message", false ).ToLowerInvariant(), ( object )TraderHelper.CandleArgToFolderName( object_0 ), securityId_0.SecurityCode.SecurityIdToFolderName(), dateTime_0 ) );
        }

        private sealed class Class42
        {
            public Security security_0;

            internal MarketDepth method_0( Level1ChangeMessage level1ChangeMessage_0 )
            {
                return level1ChangeMessage_0.ToMarketDepth( this.security_0 );
            }
        }

        private sealed class Class43
        {
            public string string_0;

            internal DukasCopyHistorySource.DukasRoot method_0( )
            {
                throw new NotFiniteNumberException( );
                //return this.string_0.DeserializeObject<DukasCopyHistorySource.DukasRoot>( );
            }
        }

        [DataContract]
        private class DukasGroup
        {
            [DataMember( Name = "id" )]
            public string Id
            {
                get;
                set;
            }

            [DataMember( Name = "title" )]
            public string Title
            {
                get;
                set;
            }

            [DataMember( Name = "instruments" )]
            public string[ ] Securities
            {
                get;
                set;
            }

            [DataMember( IsRequired = false, Name = "parent" )]
            public string Parent
            {
                get;
                set;
            }
        }

        [DataContract]
        private class DukasRoot
        {
            [DataMember( Name = "instruments" )]
            public Dictionary< string, DukasCopyHistorySource.DukasSecurity > Securities
            {
                get;
                set;
            }

            [DataMember( Name = "groups" )]
            public Dictionary< string, DukasCopyHistorySource.DukasGroup > Groups
            {
                get;
                set;
            }
        }

        [DataContract]
        private class DukasSecurity
        {
            [DataMember( Name = "title" )]
            public string Title
            {
                get;
                set;
            }

            [DataMember( Name = "description" )]
            public string Description
            {
                get;
                set;
            }

            [DataMember( Name = "historical_filename" )]
            public string HistoricalFileName
            {
                get;
                set;
            }

            [DataMember( Name = "pipValue" )]
            public Decimal PriceStep
            {
                get;
                set;
            }
        }
    }
}
