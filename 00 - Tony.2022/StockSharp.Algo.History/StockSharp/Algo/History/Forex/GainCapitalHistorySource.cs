using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using HtmlAgilityPack;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using xNet;

namespace StockSharp.Algo.History.Forex
{
    public class GainCapitalHistorySource : BaseDumpableHistorySource, ISecurityDownloader
    {
        private static readonly Regex regex_0 = new Regex( ".\\\\[A-Z][A-Z][A-Z]_[A-Z][A-Z][A-Z]_Week(\\d|\\d-\\d).zip", RegexOptions.IgnoreCase | RegexOptions.Compiled );
        private readonly SynchronizedSet< string > synchronizedSet_0 = new SynchronizedSet< string >( StringComparer.InvariantCultureIgnoreCase );

        public GainCapitalHistorySource(
      INativeIdStorage nativeIdStorage,
      IExchangeInfoProvider exchangeInfoProvider ) : base( nativeIdStorage, exchangeInfoProvider )
        {
        }

        private static Uri smethod_0( DateTime dateTime_0 )
        {
            if( dateTime_0.Year <= 2002 )
            {
                return new Uri( "http://ratedata.gaincapital.com/" + dateTime_0.Year );
            }

            return new Uri( "{0}{1:yyyy}/{1:MM}%20{2}".Put( "http://ratedata.gaincapital.com/", dateTime_0, dateTime_0.ToString( "MMMM", CultureInfo.InvariantCulture ) ) );
        }

        private static SortedDictionary< string, SortedDictionary< DateTime, string > > smethod_1(
      DateTime dateTime_0 )
        {
            GainCapitalHistorySource.Class50 class50 = new GainCapitalHistorySource.Class50( );
            class50.dateTime_0 = dateTime_0;
            SortedDictionary< string, SortedDictionary< DateTime, string > > dictionary = new SortedDictionary< string, SortedDictionary< DateTime, string > >( StringComparer.InvariantCultureIgnoreCase );
            if( class50.dateTime_0.Year <= 2002 )
            {
                throw new NotSupportedException( "Year of the requsted date is before 2003!" );
            }

            using( HttpRequest httpRequest = new HttpRequest( ) )
            {
                try
                {
                    httpRequest.Get( "http://ratedata.gaincapital.com/{0:yyyy}/".Put( class50.dateTime_0 ), null );
                }
                catch( HttpException ex )
                {
                    if( ex.HttpStatusCode == HttpStatusCode.NotFound || class50.dateTime_0.Year == 2013 )
                    {
                        return dictionary;
                    }

                    throw new InvalidOperationException( LocalizedStrings.Str2078Params.Put( ex.HttpStatusCode, class50.dateTime_0 ) );
                }
            }
            if( !new HtmlWeb( ).Load( "http://ratedata.gaincapital.com/{0:yyyy}/".Put( class50.dateTime_0 ) ).DocumentNode.SelectNodes( "//table[@id='Table1']/tr/td[@align='left']/a" ).Any< HtmlNode >( new Func< HtmlNode, bool >( class50.method_0 ) ) )
            {
                return dictionary;
            }

            using( HttpRequest httpRequest = new HttpRequest( ) )
            {
                Uri address = GainCapitalHistorySource.smethod_0( class50.dateTime_0 );
                string str1 = httpRequest.Get( address, null ).ToString( );
                if( str1.IsEmpty( ) )
                {
                    return dictionary;
                }

                foreach( object match in GainCapitalHistorySource.regex_0.Matches( str1 ) )
                {
                    string str2 = match.ToString( ).Remove( ".\\", false );
                    string[] strArray = str2.Split( '_', '.' );
                    string key = "{0}/{1}".Put( strArray[0], strArray[1] );
                    string lowerInvariant = strArray[ 2 ].ToLowerInvariant( );
                    int num = !lowerInvariant.StartsWith( "week1" ) ? ( !lowerInvariant.StartsWith( "week2" ) ? ( !lowerInvariant.StartsWith( "week3" ) ? ( !lowerInvariant.StartsWith( "week4" ) ? 4 : 3 ) : 2 ) : 1 ) : 0;
                    dictionary.SafeAdd< string, SortedDictionary< DateTime, string > >( key )[ new DateTime( class50.dateTime_0.Year, class50.dateTime_0.Month, class50.dateTime_0.DaysInMonth( ).Min( 1 + 7 * num ) ) ] = "{0}/{1}".Put( address.AbsoluteUri, str2 );
                }
            }
            return dictionary;
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

            for( DateTime dateTime_0 = new DateTime( 2003, 1, 1 ); dateTime_0.Year < DateTime.Now.Year; dateTime_0 = dateTime_0.AddYears( 1 ) )
            {
                foreach( string key in GainCapitalHistorySource.smethod_1( dateTime_0 ).Keys )
                {
                    if( isCancelled( ) )
                    {
                        return;
                    }

                    if( criteria.Code.IsEmpty( ) || key.ContainsIgnoreCase( criteria.Code ) )
                    {
                        string id = this.SecurityIdGenerator.GenerateId( key, ExchangeBoard.GainCapital );
                        if( securityStorage.LookupById( id ) == null )
                        {
                            Security security = new Security( )
              {
                Id = id,
                Code = key,
                Board = ExchangeBoard.GainCapital,
                PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, 5 ) ),
                Type = new SecurityTypes?( SecurityTypes.Currency )
              };
                            securityStorage.Save( security, false );
                            newSecurity( security );
                        }
                    }
                }
            }
        }

        public override string GetDumpFile(
      Security security,
      DateTime from,
      DateTime to,
      Type dataType,
      object arg )
        {
            return this.method_0( security.ToSecurityId( null ), from, to, dataType );
        }

        private string method_0(
      SecurityId securityId_0,
      DateTime dateTime_0,
      DateTime dateTime_1,
      Type type_0 )
        {
            if( type_0 == null )
            {
                throw new ArgumentNullException( "dataType" );
            }

            if( type_0 != typeof( Level1ChangeMessage ) )
            {
                throw new ArgumentOutOfRangeException( "dataType", type_0, LocalizedStrings.Str1655 );
            }

            int num = dateTime_0.Day > 7 ? ( dateTime_0.Day > 14 ? ( dateTime_0.Day > 21 ? ( dateTime_0.Day > 28 ? 5 : 4 ) : 3 ) : 2 ) : 1;
            return Path.Combine( this.DumpFolder, "{0}_{1}_{2}.zip".Put( securityId_0.SecurityCode.SecurityIdToFolderName(), type_0.Name.ToLowerInvariant(), dateTime_0.Year <= 2002 ? dateTime_0.ToString( "yyyy" ) : ( object ) ( dateTime_0.ToString( "yyyyMM" ) + "week" + num ) ) );
        }

        public IEnumerable< MarketDepth > LoadTicks(
      Security security,
      DateTime date )
        {
            GainCapitalHistorySource.Class47 class47 = new GainCapitalHistorySource.Class47( );
            class47.security_0 = security;
            return this.LoadTickMessages( class47.security_0.ToSecurityId( null ), date ).Select< Level1ChangeMessage, MarketDepth >( new Func< Level1ChangeMessage, MarketDepth >( class47.method_0 ) );
        }

        public IEnumerable< Level1ChangeMessage > LoadTickMessages(
      SecurityId securityId,
      DateTime date )
        {
            if( date.Year > 2002 )
            {
                return this.method_2( securityId, date );
            }

            return this.method_1( securityId, date );
        }

        private IEnumerable< Level1ChangeMessage > method_1(
      SecurityId securityId_0,
      DateTime dateTime_0 )
        {
            if( dateTime_0.Year < 2000 )
            {
                return Enumerable.Empty< Level1ChangeMessage >( );
            }

            return this.method_4( "{0}{2}/{1}_{2}.zip".Put( "http://ratedata.gaincapital.com/", securityId_0.SecurityCode.Replace( '/', '_' ), dateTime_0.Year ), securityId_0, dateTime_0, dateTime_0 );
        }

        private IEnumerable< Level1ChangeMessage > method_2(
      SecurityId securityId_0,
      DateTime dateTime_0 )
        {
            GainCapitalHistorySource.Class48 class48 = new GainCapitalHistorySource.Class48( );
            SortedDictionary< string, SortedDictionary< DateTime, string > > sortedDictionary = GainCapitalHistorySource.smethod_1( dateTime_0 );
            if( !sortedDictionary.ContainsKey( securityId_0.SecurityCode ) )
            {
                return Enumerable.Empty< Level1ChangeMessage >( );
            }

            SortedDictionary< DateTime, string > source1 = sortedDictionary[ securityId_0.SecurityCode ];
            class48.keyValuePair_0 = new KeyValuePair< DateTime, string >( );
            foreach( KeyValuePair< DateTime, string > keyValuePair in source1 )
            {
                if( !( dateTime_0 < keyValuePair.Key ) )
                {
                    class48.keyValuePair_0 = keyValuePair;
                }
                else
                {
                    break;
                }
            }
            if( class48.keyValuePair_0.Value.IsEmpty( ) )
            {
                DateTime dateTime = dateTime_0.AddMonths( -1 );
                dateTime = new DateTime( dateTime.Year, dateTime.Month, dateTime.DaysInMonth( ) );
                return this.method_3( securityId_0, dateTime, dateTime_0 );
            }
            Level1ChangeMessage[] source2 = this.method_4( class48.keyValuePair_0.Value, securityId_0, dateTime_0, dateTime_0 );
            if( !source2.IsEmpty< Level1ChangeMessage >( ) )
            {
                return source2;
            }

            foreach( KeyValuePair< DateTime, string > keyValuePair in source1.Where< KeyValuePair< DateTime, string > >( class48.func_0 ?? ( class48.func_0 = new Func< KeyValuePair< DateTime, string >, bool >( class48.method_0 ) ) ) )
            {
                Level1ChangeMessage[] source3 = this.method_4( keyValuePair.Value, securityId_0, keyValuePair.Key, dateTime_0 );
                if( !source3.IsEmpty< Level1ChangeMessage >( ) )
                {
                    return source3;
                }
            }
            DateTime dateTime_0_1 = dateTime_0.AddMonths( 1 );
            dateTime_0_1 = new DateTime( dateTime_0_1.Year, dateTime_0_1.Month, 1 );
            return this.method_3( securityId_0, dateTime_0_1, dateTime_0 );
        }

        private IEnumerable< Level1ChangeMessage > method_3(
      SecurityId securityId_0,
      DateTime dateTime_0,
      DateTime dateTime_1 )
        {
            SortedDictionary< string, SortedDictionary< DateTime, string > > sortedDictionary = GainCapitalHistorySource.smethod_1( dateTime_0 );
            if( !sortedDictionary.ContainsKey( securityId_0.SecurityCode ) )
            {
                return Enumerable.Empty< Level1ChangeMessage >( );
            }

            string empty = string.Empty;
            foreach( KeyValuePair< DateTime, string > keyValuePair in sortedDictionary[ securityId_0.SecurityCode ] )
            {
                if( !( dateTime_0 < keyValuePair.Key ) )
                {
                    empty = keyValuePair.Value;
                }
                else
                {
                    break;
                }
            }
            if( !empty.IsEmpty( ) )
            {
                return this.method_4( empty, securityId_0, dateTime_0, dateTime_1 );
            }

            return Enumerable.Empty< Level1ChangeMessage >( );
        }

        private Level1ChangeMessage[] method_4(
      string string_1,
      SecurityId securityId_0,
      DateTime dateTime_0,
      DateTime dateTime_1 )
        {
            GainCapitalHistorySource.Class46 class46 = new GainCapitalHistorySource.Class46( );
            class46.securityId_0 = securityId_0;
            class46.dateTime_0 = dateTime_1;
            if( string_1.IsEmpty( ) )
            {
                throw new ArgumentNullException( "url" );
            }

            if( this.synchronizedSet_0.Contains( string_1 ) )
            {
                return ArrayHelper.Empty< Level1ChangeMessage >( );
            }

            string str = this.CanDump ? this.method_0( class46.securityId_0, dateTime_0, dateTime_0, typeof( Level1ChangeMessage ) ) : null;
            if( str != null && File.Exists( str ) )
            {
                class46.byte_0 = File.ReadAllBytes( str );
            }
            else
            {
                using( HttpRequest httpRequest = new HttpRequest( ) )
                {
                    try
                    {
                        class46.byte_0 = httpRequest.Get( string_1, null ).ToBytes( );
                    }
                    catch( HttpException ex )
                    {
                        if( ex.HttpStatusCode != HttpStatusCode.NotFound )
                        {
                            throw new InvalidOperationException( LocalizedStrings.Str2078Params.Put( ex.HttpStatusCode, dateTime_0 ) );
                        }

                        this.synchronizedSet_0.Add( string_1 );
                        return ArrayHelper.Empty< Level1ChangeMessage >( );
                    }
                }
                if( str != null )
                {
                    str.CreateDirIfNotExists( );
                    class46.byte_0.Save( str );
                }
            }
            return CultureInfo.InvariantCulture.DoInCulture< Level1ChangeMessage[] >( new Func< Level1ChangeMessage[] >( class46.method_0 ) );
        }

        private static Level1ChangeMessage smethod_2(
      SecurityId securityId_0,
      string string_1,
      int int_0,
      int int_1,
      int int_2 )
        {
            string[] strArray = string_1.Remove( "\"", false ).Split( ',' );
            string str = strArray[ int_0 ];
            string format = "yyyy-MM-dd HH:mm:ss";
            if( str.Contains( "." ) )
            {
                str = str.Substring( 0, str.IndexOf( '.' ) + 4 );
                format = "yyyy-MM-dd HH:mm:ss.fff";
            }
            Level1ChangeMessage message = new Level1ChangeMessage( );
            message.SecurityId = securityId_0;
            message.ServerTime = str.ToDateTime( format, null ).ApplyTimeZone( TimeZoneInfo.Utc );
            return message.TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.BestBidPrice, strArray[ int_1 ].Trim( ).To< Decimal >( ), false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.BestAskPrice, strArray[ int_2 ].Trim( ).To< Decimal >( ), false );
        }

        private sealed class Class46
        {
            public byte[] byte_0;
            public SecurityId securityId_0;
            public DateTime dateTime_0;

            internal Level1ChangeMessage[] method_0( )
            {
                List< Level1ChangeMessage > level1ChangeMessageList = new List< Level1ChangeMessage >( );
                using( ZipArchive zipArchive = new ZipArchive( this.byte_0.To< Stream >( ) ) )
                {
                    foreach( ZipArchiveEntry entry in zipArchive.Entries )
                    {
                        if( entry.Name.ToLowerInvariant( ).EndsWith( ".csv" ) )
                        {
                            using( Stream stream = entry.Open( ) )
                            {
                                using( StreamReader streamReader = new StreamReader( stream ) )
                                {
                                    string str1 = streamReader.ReadLine( );
                                    if( !str1.IsEmpty( ) )
                                    {
                                        string str2 = str1.Remove( "\"", false );
                                        int int_0;
                                        int int_1;
                                        int int_2;
                                        if( str2.ContainsIgnoreCase( "lTid" ) )
                                        {
                                            string[] array = str2.Split( ',' ).Select< string, string >( GainCapitalHistorySource.Class49.func_0 ?? ( GainCapitalHistorySource.Class49.func_0 = new Func< string, string >( GainCapitalHistorySource.Class49.class49_0.method_0 ) ) ).ToArray< string >( );
                                            int_0 = array.IndexOf< string >( "ratedatetime" );
                                            int_1 = array.IndexOf< string >( "ratebid" );
                                            int_2 = array.IndexOf< string >( "rateask" );
                                            str2 = streamReader.ReadLine( );
                                        }
                                        else
                                        {
                                            int_0 = 2;
                                            int_1 = 3;
                                            int_2 = 4;
                                        }
                                        for( ; str2 != null; str2 = streamReader.ReadLine( ) )
                                        {
                                            Level1ChangeMessage level1ChangeMessage = GainCapitalHistorySource.smethod_2( this.securityId_0, str2, int_0, int_1, int_2 );
                                            DateTimeOffset serverTime = level1ChangeMessage.ServerTime;
                                            if( serverTime.Date == this.dateTime_0 )
                                            {
                                                level1ChangeMessageList.Add( level1ChangeMessage );
                                            }
                                            else
                                            {
                                                serverTime = level1ChangeMessage.ServerTime;
                                                if( serverTime.Date > this.dateTime_0 )
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return level1ChangeMessageList.ToArray( );
            }
        }

        private sealed class Class47
        {
            public Security security_0;

            internal MarketDepth method_0( Level1ChangeMessage level1ChangeMessage_0 )
            {
                return level1ChangeMessage_0.ToMarketDepth( this.security_0 );
            }
        }

        private sealed class Class48
        {
            public KeyValuePair< DateTime, string > keyValuePair_0;
            public Func< KeyValuePair< DateTime, string >, bool > func_0;

            internal bool method_0( KeyValuePair< DateTime, string > keyValuePair_1 )
            {
                return keyValuePair_1.Key != this.keyValuePair_0.Key;
            }
        }

        [Serializable]
    private sealed class Class49
    {
        public static readonly GainCapitalHistorySource.Class49 class49_0 = new GainCapitalHistorySource.Class49( );
        public static Func< string, string > func_0;

        internal string method_0( string string_0 )
        {
            return string_0.ToLowerInvariant( );
        }
    }

        private sealed class Class50
        {
            public DateTime dateTime_0;

            internal bool method_0( HtmlNode htmlNode_0 )
            {
                DateTime result;
                if( DateTime.TryParseExact( htmlNode_0.InnerText.Trim( ), "dd MMMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out result ) )
                {
                    return result.Month == this.dateTime_0.Month;
                }

                return false;
            }
        }
    }
}
