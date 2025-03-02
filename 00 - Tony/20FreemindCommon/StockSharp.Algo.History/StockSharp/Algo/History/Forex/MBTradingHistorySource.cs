using Ecng.Common;
using Ecng.Serialization;
using HtmlAgilityPack;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security;
using System.Text;
using xNet;

#pragma warning disable 649

namespace StockSharp.Algo.History.Forex
{
    public class MBTradingHistorySource : BaseDumpableHistorySource, ISecurityDownloader
    {
        private readonly CookieDictionary _cookie = new CookieDictionary( false );
        private int _maxAttempt = 10;
        private string _login;
        private SecureString _password;
        private SecureString _pin;

        public MBTradingHistorySource( INativeIdStorage nativeIdStorage, IExchangeInfoProvider exchangeInfoProvider ) : base( nativeIdStorage, exchangeInfoProvider )
        {
        }

        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
            }
        }

        public SecureString Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public SecureString Pin
        {
            get
            {
                return _pin;
            }
            set
            {
                _pin = value;
            }
        }

        public int MaxAttempt
        {
            get
            {
                return _maxAttempt;
            }
            set
            {
                if( value < 1 )
                {
                    throw new ArgumentOutOfRangeException( );
                }

                _maxAttempt = value;
            }
        }

        public void Refresh(
                                ISecurityStorage securityStorage,
                                StockSharp.BusinessEntities.Security criteria,
                                Action< StockSharp.BusinessEntities.Security > newSecurity,
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

            var html = LoginWithPassword( );
            var doc = new HtmlDocument( );

            doc.LoadHtml( html );

            foreach( var htmlNode in doc.DocumentNode.SelectSingleNode( "//select[contains(@id, 'content_TickHistory_ddlCurrencyPairs')]" ).ChildNodes.Where< HtmlNode >( n => n.Name == "#text" ) )
            {
                if( isCancelled( ) )
                {
                    break;
                }

                string[ ] strArray = htmlNode.OuterHtml.Split( ':' );

                if( strArray.Length >= 2 )
                {
                    string str = strArray[ 0 ];
                    string str1 = strArray[ 1 ].Substring( 1 );

                    if( ( criteria.Code.IsEmpty( ) || str.ContainsIgnoreCase( criteria.Code ) ) && ( criteria.Name.IsEmpty( ) || str1.ContainsIgnoreCase( criteria.Name ) ) )
                    {
                        string id = SecurityIdGenerator.GenerateId( str, ExchangeBoard.MBTrading );

                        if( securityStorage.LookupById( id ) == null )
                        {
                            StockSharp.BusinessEntities.Security security = new StockSharp.BusinessEntities.Security( )
                            {
                                Id = id,
                                Code = str,
                                Name = str1,
                                PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, 8 ) ),
                                Board = ExchangeBoard.MBTrading,
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
                                              StockSharp.BusinessEntities.Security security,
                                              DateTime from,
                                              DateTime to,
                                              Type dataType,
                                              object arg )
        {
            return GetDumpFilePth( security.ToSecurityId( null ), from, to, dataType );
        }

        private string GetDumpFilePth(
                                            SecurityId sec,
                                            DateTime from,
                                            DateTime to,
                                            Type dateType )
        {
            if( dateType == null )
            {
                throw new ArgumentNullException( "dataType" );
            }

            if( dateType != typeof( Level1ChangeMessage ) )
            {
                throw new ArgumentOutOfRangeException( "dataType", dateType, LocalizedStrings.Str1655 );
            }

            return Path.Combine( DumpFolder, "{0}_{1}_{2:yyyy_MM_dd}_{3:yyyy_MM_dd}.zip".Put( sec.SecurityCode.SecurityIdToFolderName( ), dateType.Name.ToLowerInvariant( ), from, to ) );
        }

        public IEnumerable< MarketDepth > LoadTicks(
                                                        StockSharp.BusinessEntities.Security security,
                                                        DateTime from,
                                                        DateTime to )
        {
            return LoadTickMessages( security.ToSecurityId( ), from, to ).Select( msg => msg.ToMarketDepth( security ) );
        }

        public IEnumerable< Level1ChangeMessage > LoadTickMessages(
                                                                      SecurityId securityId,
                                                                      DateTime from,
                                                                      DateTime to )
        {
            MBTradingHistorySource.Class52 class52 = new MBTradingHistorySource.Class52( )
            {
                securityId = securityId,
                from = from,
                to = to,
                _this = this
            };
            string dumpFilePath = CanDump ? GetDumpFilePth( securityId, from, to, typeof( Level1ChangeMessage ) ) : null;

            if( dumpFilePath != null && File.Exists( dumpFilePath ) )
            {
                return MBTradingHistorySource.GetLocalTicks( class52.securityId, File.ReadAllBytes( dumpFilePath ) );
            }

            class52.string_0 = LoginWithPassword( );

            return SendAndGetResponse< IEnumerable< Level1ChangeMessage > >( "https://www.mbtrading.com/clientArea/TickDownload.aspx", new Action< HttpRequest >( class52.method_0 ), true, new Func< HttpResponse, IEnumerable< Level1ChangeMessage > >( class52.method_1 ) );
        }

        private static IEnumerable< Level1ChangeMessage > GetLocalTicks(
                                                                          SecurityId security,
                                                                          byte[ ] byteArray )
        {
            var level1List = new List< Level1ChangeMessage >( );

            CultureInfo.InvariantCulture.DoInCulture( ( ) =>
            {
                foreach( Stream tickStream in MBTradingHistorySource.smethod_1( byteArray.To< Stream >( ) ) )
                {
                    foreach( Stream stream in MBTradingHistorySource.smethod_1( tickStream ) )
                    {
                        level1List.AddRange( stream.EnumerateLines( null ).Skip< string >( 1 ).Select( r => MBTradingHistorySource.smethod_2( r, security ) ) );
                    }
                }
            } );

            return level1List;
        }

        private static IEnumerable< Stream > smethod_1( Stream stream_0 )
        {
            throw new NotImplementedException( );
            //return ( IEnumerable<Stream> )new MBTradingHistorySource.Class55( -2 )
            //{
            //    stream_2 = stream_0
            //};
        }

        private T SendAndGetResponse< T >(
                                            string url,
                                            Action< HttpRequest > actionHandler,
                                            bool postOfGet,
                                            Func< HttpResponse, T > responseHandler )
        {
            if( actionHandler == null )
            {
                throw new ArgumentNullException( "action" );
            }

            if( responseHandler == null )
            {
                throw new ArgumentNullException( "response" );
            }

            using( HttpRequest httpRequest = new HttpRequest( )
            {
                Cookies = _cookie,
                UserAgent = Http.ChromeUserAgent( )
            } )
            {
                actionHandler( httpRequest );
                return responseHandler( postOfGet ? httpRequest.Post( url ) : httpRequest.Get( url, null ) );
            }
        }

        private string GetEnterPinPage( string string_2 )
        {
            Action< HttpRequest > actionHandler = r =>
            {
                r.AddParam( "__LASTFOCUS", string.Empty );
                r.AddParam( "__EVENTTARGET", string.Empty );
                r.AddParam( "__EVENTARGUMENT", string.Empty );
                r.AddParam( "__VIEWSTATE", string_2.Substrings( "id=\"__VIEWSTATE\" value=\"", "\"", StringComparison.Ordinal )[ 0 ] );
                r.AddParam( "__EVENTVALIDATION", string_2.Substrings( "id=\"__EVENTVALIDATION\" value=\"", "\"", StringComparison.Ordinal )[ 0 ] );
                r.AddParam( "ctl00$ctl00$content$main$modLogin$ctl01$tbUsername", this.Login );
                r.AddParam( "ctl00$ctl00$content$main$modLogin$ctl01$tbPassword", this.Password );
                r.AddParam( "ctl00$ctl00$content$main$modLogin$ctl01$btnLogin", "Login" );
            };

            return SendAndGetResponse< string >( "https://www.mbtrading.com/clientarea/Login.aspx", actionHandler, true, r => r.ToString( ) );
        }

        private void TryLogin( string string_2 )
        {
            SendAndGetResponse< string >( "https://www.mbtrading.com/clientarea/Login.aspx", new Action< HttpRequest >( new MBTradingHistorySource.Class53( )
            {
                string_0 = string_2,
                mbtradingHistorySource_0 = this
            }.method_0 ), true, MBTradingHistorySource.Class56.func_2 ?? ( MBTradingHistorySource.Class56.func_2 = new Func< HttpResponse, string >( MBTradingHistorySource.Class56.class56_0.method_2 ) ) );
        }

        private string GetDownloadLink( )
        {
            return SendAndGetResponse< string >( "https://www.mbtrading.com/clientArea/TickDownload.aspx", MBTradingHistorySource.Class56.action_0 ?? ( MBTradingHistorySource.Class56.action_0 = new Action< HttpRequest >( MBTradingHistorySource.Class56.class56_0.method_3 ) ), false, MBTradingHistorySource.Class56.func_3 ?? ( MBTradingHistorySource.Class56.func_3 = new Func< HttpResponse, string >( MBTradingHistorySource.Class56.class56_0.method_4 ) ) );
        }

        private string LoginWithPassword( )
        {
            string destHtml = GetDownloadLink( );

            int num = 0;
            for( ; !destHtml.ContainsIgnoreCase( "<li>Download tick history</li>" ); destHtml = GetDownloadLink( ) )
            {
                ++num;
                if( num > MaxAttempt )
                {
                    throw new UnauthorizedAccessException( );
                }

                string str2 = GetEnterPinPage( destHtml );

                if( str2.ContainsIgnoreCase( "Verify your 4-digit PIN" ) )
                {
                    TryLogin( str2 );
                }
            }
            return destHtml;
        }

        private static Level1ChangeMessage smethod_2(
          string string_2,
          SecurityId securityId_0 )
        {
            string[ ] array = ( ( IEnumerable< string > )string_2.Split( ',' ) ).Select< string, string >( MBTradingHistorySource.Class56.func_4 ?? ( MBTradingHistorySource.Class56.func_4 = new Func< string, string >( MBTradingHistorySource.Class56.class56_0.method_5 ) ) ).ToArray< string >( );
            Level1ChangeMessage message = new Level1ChangeMessage( );
            message.SecurityId = securityId_0;
            message.ServerTime = array[ 1 ].ToDateTime( "yyyy-MM-dd HH:mm:ss.fff", null ).ApplyTimeZone( TimeZoneInfo.Utc );
            return message.TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.BestBidPrice, array[ 2 ].To< Decimal >( ), false ).TryAdd< Level1ChangeMessage, Level1Fields >( Level1Fields.BestAskPrice, array[ 3 ].To< Decimal >( ), false );
        }

        private static string smethod_3( DateTime dateTime_0, DateTime dateTime_1, string string_2 )
        {
            string[ ] strArray = string_2.Split( ',' );
            if( strArray.Length < 3 )
            {
                return string.Empty;
            }

            DateTime dateTime = ( strArray[ 1 ].Trim( ).Replace( ' ', '/' ) + "/" + strArray[ 2 ].Trim( ) ).To< DateTime >( );
            if( !( dateTime >= dateTime_0 ) || !( dateTime <= dateTime_1 ) )
            {
                return string.Empty;
            }

            return "ctl00_content_TickHistory_trvFilesn" + string_2.Split( '"' )[ 0 ] + "CheckBox";
        }

        private sealed class Class51
        {
            public string string_0;
            public MBTradingHistorySource mbtradingHistorySource_0;

            internal void method_0( HttpRequest r )
            {
                r.AddParam( "__LASTFOCUS", string.Empty );
                r.AddParam( "__EVENTTARGET", string.Empty );
                r.AddParam( "__EVENTARGUMENT", string.Empty );
                r.AddParam( "__VIEWSTATE", string_0.Substrings( "id=\"__VIEWSTATE\" value=\"", "\"", StringComparison.Ordinal )[ 0 ] );
                r.AddParam( "__EVENTVALIDATION", string_0.Substrings( "id=\"__EVENTVALIDATION\" value=\"", "\"", StringComparison.Ordinal )[ 0 ] );
                r.AddParam( "ctl00$ctl00$content$main$modLogin$ctl01$tbUsername", mbtradingHistorySource_0.Login );
                r.AddParam( "ctl00$ctl00$content$main$modLogin$ctl01$tbPassword", mbtradingHistorySource_0.Password );
                r.AddParam( "ctl00$ctl00$content$main$modLogin$ctl01$btnLogin", "Login" );
            }
        }

        private sealed class Class52
        {
            public string string_0;
            public SecurityId securityId;
            public DateTime from;
            public DateTime to;
            public MBTradingHistorySource _this;
            public string string_1;

            internal void method_0( HttpRequest httpRequest_0 )
            {
                httpRequest_0.AddParam( "__EVENTTARGET", string.Empty );
                httpRequest_0.AddParam( "__EVENTARGUMENT", string.Empty );
                httpRequest_0.AddParam( "__VIEWSTATE", string_0.Substrings( "id=\"__VIEWSTATE\" value=\"", "\"", StringComparison.Ordinal )[ 0 ] );
                httpRequest_0.AddParam( "__EVENTVALIDATION", string_0.Substrings( "id=\"__EVENTVALIDATION\" value=\"", "\"", StringComparison.Ordinal )[ 0 ] );
                httpRequest_0.AddParam( "ctl00$content$TickHistory$ddlCurrencyPairs", securityId.SecurityCode );
                httpRequest_0.AddParam( "ctl00$content$TickHistory$btn_Submit", "Download" );
                string[ ] strArray = string_0.Substrings( "<span class=\"ctl00_content_TickHistory_trvFiles_0 ctl00_content_TickHistory_trvFiles_5\" id=\"ctl00_content_TickHistory_trvFilest", "</span>", StringComparison.Ordinal );
                if( strArray.Length == 0 )
                {
                    throw new InvalidOperationException( );
                }

                foreach( string string_2 in strArray )
                {
                    string str = MBTradingHistorySource.smethod_3( from, to, string_2 );
                    if( !str.IsEmpty( ) )
                    {
                        httpRequest_0.AddParam( str, "on" );
                    }
                }
            }

            internal IEnumerable< Level1ChangeMessage > method_1(
              HttpResponse httpResponse_0 )
            {
                if( httpResponse_0.ContentType == "text/html" )
                {
                    return Enumerable.Empty< Level1ChangeMessage >( );
                }

                byte[ ] bytes = httpResponse_0.ToBytes( );
                if( _this.CanDump )
                {
                    string_1.CreateDirIfNotExists( );
                    bytes.Save( string_1 );
                }
                return MBTradingHistorySource.GetLocalTicks( securityId, bytes );
            }
        }

        private sealed class Class53
        {
            public string string_0;
            public MBTradingHistorySource mbtradingHistorySource_0;

            internal void method_0( HttpRequest httpRequest_0 )
            {
                httpRequest_0.AddParam( "__LASTFOCUS", string.Empty );
                httpRequest_0.AddParam( "__EVENTTARGET", string.Empty );
                httpRequest_0.AddParam( "__EVENTARGUMENT", string.Empty );
                httpRequest_0.AddParam( "__VIEWSTATE", string_0.Substrings( "id=\"__VIEWSTATE\" value=\"", "\"", StringComparison.Ordinal )[ 0 ] );
                httpRequest_0.AddParam( "__EVENTVALIDATION", string_0.Substrings( "id=\"__EVENTVALIDATION\" value=\"", "\"", StringComparison.Ordinal )[ 0 ] );
                httpRequest_0.AddParam( "ctl00_ctl00_content_main_modEnterPIN_ctl01_tbPin", mbtradingHistorySource_0.Pin );
                httpRequest_0.AddParam( "ctl00$ctl00$content$main$modEnterPIN$ctl01$btnVerifyPIN", "Verify" );
            }
        }

        [Serializable]
        private sealed class Class56
        {
            public static readonly MBTradingHistorySource.Class56 class56_0 = new MBTradingHistorySource.Class56( );
            public static Func< HtmlNode, bool > func_0;
            public static Func< HttpResponse, string > func_1;
            public static Func< HttpResponse, string > func_2;
            public static Action< HttpRequest > action_0;
            public static Func< HttpResponse, string > func_3;
            public static Func< string, string > func_4;

            internal bool method_0( HtmlNode n )
            {
                return n.Name == "#text";
            }

            internal string method_1( HttpResponse r )
            {
                return r.ToString( );
            }

            internal string method_2( HttpResponse httpResponse_0 )
            {
                return null;
            }

            internal void method_3( HttpRequest httpRequest_0 )
            {
            }

            internal string method_4( HttpResponse httpResponse_0 )
            {
                return httpResponse_0.ToString( );
            }

            internal string method_5( string string_0 )
            {
                return string_0.Trim( );
            }
        }
    }
}
