using Ecng.Collections;
using Ecng.Common;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace StockSharp.Algo.History.Russian.Rts
{
    public static class FortsDailyData
    {
        private static readonly DateTime dateTime_0 = new DateTime( 2009, 11, 2 );

        public static Level1ChangeMessage GetSecurityYesterdayEndOfDay(
      SecurityId securityId,
      string securityName )
        {
            DateTime dateTime = TimeHelper.Now - TimeSpan.FromDays( 1.0 );
            return FortsDailyData.GetSecurityEndOfDay( securityId, securityName, dateTime, dateTime ).FirstOrDefault< Level1ChangeMessage >( );
        }

        public static IEnumerable< Level1ChangeMessage > GetSecurityEndOfDay(
      SecurityId securityId,
      string securityName,
      DateTime fromDate,
      DateTime toDate )
        {
            FortsDailyData.Class137 class137 = new FortsDailyData.Class137( );
            class137.securityId_0 = securityId;
            if( fromDate > toDate )
            {
                throw new ArgumentOutOfRangeException( nameof( fromDate ), fromDate, LocalizedStrings.Str2111Params.Put( ( object )fromDate, ( object )toDate ) );
            }

            using( WebClient webClient = new WebClient( ) )
            {
                FortsDailyData.Class138 class138 = new FortsDailyData.Class138( );
                class138.class137_0 = class137;
                string address = "https://moex.com/en/derivatives/contractresults-exp.aspx?day1={0:yyyyMMdd}&day2={1:yyyyMMdd}&code={2}".Put( fromDate.Date, toDate.Date, securityName );
                class138.stream_0 = webClient.OpenRead( address );
                if( class138.stream_0 == null )
                {
                    throw new InvalidOperationException( LocalizedStrings.Str2112 );
                }

                return CultureInfo.InvariantCulture.DoInCulture<List<Level1ChangeMessage>>( new Func<List<Level1ChangeMessage>>( class138.method_0 ) );
            }
        }

        private static Decimal smethod_0( string string_0 )
        {
            Decimal result;
            if( Decimal.TryParse( string_0, out result ) )
            {
                return result;
            }

            return Decimal.Zero;
        }

        public static DateTime UsdRateMinAvailableTime
        {
            get
            {
                return FortsDailyData.dateTime_0;
            }
        }

        public static IDictionary< DateTimeOffset, Decimal > GetRate(
      Security security,
      DateTime fromDate,
      DateTime toDate )
        {
            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            if( fromDate > toDate )
            {
                throw new ArgumentOutOfRangeException( nameof( fromDate ), fromDate, LocalizedStrings.Str2111Params.Put( ( object )fromDate, ( object )toDate ) );
            }

            using( WebClient webClient = new WebClient( ) )
            {
                FortsDailyData.Class140 class140 = new FortsDailyData.Class140( );
                string address = "https://moex.com/export/derivatives/currency-rate.aspx?language=en&currency={0}&moment_start={1:yyyy-MM-dd}&moment_end={2:yyyy-MM-dd}".Put( security.Id.ToSecurityId( null ).SecurityCode.Replace( "/", "__" ), fromDate, toDate );
                class140.stream_0 = webClient.OpenRead( address );
                if( class140.stream_0 == null )
                {
                    throw new InvalidOperationException( LocalizedStrings.Str2112 );
                }

                return CultureInfo.InvariantCulture.DoInCulture< IDictionary< DateTimeOffset, Decimal > >( new Func< IDictionary< DateTimeOffset, Decimal > >( class140.method_0 ) );
            }
        }

        private sealed class Class137
        {
            public SecurityId securityId_0;
        }

        private sealed class Class138
        {
            public Stream stream_0;
            public FortsDailyData.Class137 class137_0;

            internal List< Level1ChangeMessage > method_0( )
            {
                List< Level1ChangeMessage > level1ChangeMessageList1 = new List< Level1ChangeMessage >( );
                using( StreamReader streamReader = new StreamReader( this.stream_0, StringHelper.WindowsCyrillic ) )
                {
                    streamReader.ReadLine( );
                    string str;
                    while( ( str = streamReader.ReadLine( ) ) != null )
                    {
                        string[] strArray = str.Split( ',' );
                        DateTime dateTime = strArray[ 0 ].ToDateTime( "dd.MM.yyyy", null );
                        List< Level1ChangeMessage > level1ChangeMessageList2 = level1ChangeMessageList1;
                        Level1ChangeMessage message = new Level1ChangeMessage( );
                        message.ServerTime = dateTime.EndOfDay( ).ApplyTimeZone( TimeHelper.Moscow );
                        message.SecurityId = this.class137_0.securityId_0;
                        Level1ChangeMessage level1ChangeMessage = message.TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.SettlementPrice, FortsDailyData.smethod_0( strArray[ 1 ] ), false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.AveragePrice, FortsDailyData.smethod_0( strArray[ 2 ] ), false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.OpenPrice, FortsDailyData.smethod_0( strArray[ 3 ] ), false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.HighPrice, FortsDailyData.smethod_0( strArray[ 4 ] ), false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.LowPrice, FortsDailyData.smethod_0( strArray[ 5 ] ), false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.ClosePrice, FortsDailyData.smethod_0( strArray[ 6 ] ), false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.Change, FortsDailyData.smethod_0( strArray[ 7 ] ), false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.LastTradeVolume, FortsDailyData.smethod_0( strArray[ 8 ] ), false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.Volume, FortsDailyData.smethod_0( strArray[ 11 ] ), false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.OpenInterest, FortsDailyData.smethod_0( strArray[ 13 ] ), false );
                        level1ChangeMessageList2.Add( level1ChangeMessage );
                    }
                }
                return level1ChangeMessageList1;
            }
        }

        [Serializable]
    private sealed class Class139
    {
        public static readonly FortsDailyData.Class139 class139_0 = new FortsDailyData.Class139( );
        public static Func< XElement, KeyValuePair< DateTimeOffset, Decimal > > func_0;
        public static Func< KeyValuePair< DateTimeOffset, Decimal >, DateTimeOffset > func_1;

        internal KeyValuePair< DateTimeOffset, Decimal > method_0(
        XElement xelement_0 )
        {
            return new KeyValuePair< DateTimeOffset, Decimal >( xelement_0.GetAttributeValue< string >( "moment", null ).ToDateTime( "yyyy-MM-dd HH:mm:ss", null ).ApplyTimeZone( TimeHelper.Moscow ), xelement_0.GetAttributeValue< Decimal >( "value", new Decimal( ) ) );
        }

        internal DateTimeOffset method_1(
        KeyValuePair< DateTimeOffset, Decimal > keyValuePair_0 )
        {
            return keyValuePair_0.Key;
        }
    }

        private sealed class Class140
        {
            public Stream stream_0;

            internal IDictionary< DateTimeOffset, Decimal > method_0( )
            {
                return XDocument.Load( this.stream_0 ).Descendants( ( XName ) "rate" ).Select< XElement, KeyValuePair< DateTimeOffset, Decimal > >( FortsDailyData.Class139.func_0 ?? ( FortsDailyData.Class139.func_0 = new Func< XElement, KeyValuePair< DateTimeOffset, Decimal > >( FortsDailyData.Class139.class139_0.method_0 ) ) ).OrderBy< KeyValuePair< DateTimeOffset, Decimal >, DateTimeOffset >( FortsDailyData.Class139.func_1 ?? ( FortsDailyData.Class139.func_1 = new Func< KeyValuePair< DateTimeOffset, Decimal >, DateTimeOffset >( FortsDailyData.Class139.class139_0.method_1 ) ) ).ToDictionary< DateTimeOffset, Decimal >( );
            }
        }
    }
}
