using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
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
    public class TrueFXHistorySource : BaseDumpableHistorySource, ISecurityDownloader
    {
        private readonly CookieDictionary cookieDictionary_0 = new CookieDictionary( false );
        private readonly SynchronizedDictionary< DateTime, DateTime > synchronizedDictionary_0 = new SynchronizedDictionary< DateTime, DateTime >( );
        private string string_1;
        private SecureString secureString_0;

        public TrueFXHistorySource(
          INativeIdStorage nativeIdStorage,
          IExchangeInfoProvider exchangeInfoProvider ) : base( nativeIdStorage, exchangeInfoProvider )
        {
        }

        public string Login
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public SecureString Password
        {
            get
            {
                return this.secureString_0;
            }
            set
            {
                this.secureString_0 = value;
            }
        }

        private static string smethod_0(
          SecurityId securityId_0,
          DateTime dateTime_0,
          out string string_2 )
        {
            string_2 = "http://www.truefx.com/?page=download&description={1}{0}&dir={0}/{2}-{0}".Put( dateTime_0.Year, dateTime_0.ToString( "MMMM" ).ToLowerInvariant(), dateTime_0.ToString( "MMMM" ) );
            return "http://www.truefx.com/dev/data/{0:yyyy}/{2}-{0:yyyy}/{1}-{0:yyyy}-{0:MM}.zip".Put( dateTime_0, securityId_0.SecurityCode.Remove( "/", false ), dateTime_0.ToString( "MMMM" ).ToUpperInvariant() );
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

            string[ ] strArray = new string[ 15 ]
            {
        "AUD/JPY",
        "AUD/NZD",
        "AUD/USD",
        "CAD/JPY",
        "CFH/JPY",
        "EUR/CFH",
        "EUR/GBP",
        "EUR/JPY",
        "EUR/USD",
        "GBP/JPY",
        "GBP/USD",
        "NZD/USD",
        "USD/CAD",
        "USD/CFH",
        "USD/JPY"
            };
            foreach( string str in strArray )
            {
                if( isCancelled( ) )
                {
                    break;
                }

                string id = this.SecurityIdGenerator.GenerateId( str, ExchangeBoard.TrueFX );
                if( securityStorage.LookupById( id ) == null )
                {
                    StockSharp.BusinessEntities.Security security = new StockSharp.BusinessEntities.Security( )
                    {
                        Id = id,
                        Code = str,
                        PriceStep = new Decimal?( str.ContainsIgnoreCase( "JPY" ) ? new Decimal( 1, 0, 0, false, 3 ) : new Decimal( 1, 0, 0, false, 5 ) ),
                        Board = ExchangeBoard.TrueFX,
                        Type = new SecurityTypes?( SecurityTypes.Currency )
                    };
                    securityStorage.Save( security, false );
                    newSecurity( security );
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
            return this.method_0( security.ToSecurityId( null ), from, to, dataType, arg );
        }

        private string method_0(
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

            if( type_0 != typeof( Level1ChangeMessage ) )
            {
                throw new ArgumentOutOfRangeException( "dataType", type_0, LocalizedStrings.Str1655 );
            }

            return Path.Combine( this.DumpFolder, "{0}_{1}_{2:yyyy_MM}.zip".Put( securityId_0.SecurityCode.SecurityIdToFolderName(), type_0.Name.ToLowerInvariant(), dateTime_0 ) );
        }

        public IEnumerable< MarketDepth > LoadTicks(
          StockSharp.BusinessEntities.Security security,
          DateTime date )
        {
            TrueFXHistorySource.Class63 class63 = new TrueFXHistorySource.Class63( );
            class63.security_0 = security;
            return this.LoadTickMessages( class63.security_0.ToSecurityId( null ), date ).Select< Level1ChangeMessage, MarketDepth >( new Func< Level1ChangeMessage, MarketDepth >( class63.method_0 ) );
        }

        public IEnumerable< Level1ChangeMessage > LoadTickMessages(
          SecurityId securityId,
          DateTime date )
        {
            TrueFXHistorySource.Class60 class60 = new TrueFXHistorySource.Class60( )
            {
                trueFXHistorySource_0 = this,
                securityId_0 = securityId,
                dateTime_0 = date
            };
            class60.dateTime_0 = class60.dateTime_0.Date;
            class60.dateTime_1 = new DateTime( class60.dateTime_0.Year, class60.dateTime_0.Month, 1 );
            DateTime? nullable1 = this.synchronizedDictionary_0.TryGetValue2< DateTime, DateTime >( class60.dateTime_1 );
            if( nullable1.HasValue )
            {
                DateTime? nullable2 = nullable1;
                DateTime dateTime0 = class60.dateTime_0;
                if( ( nullable2.HasValue ? ( nullable2.GetValueOrDefault( ) > dateTime0 ? 1 : 0 ) : 0 ) != 0 )
                {
                    return Enumerable.Empty< Level1ChangeMessage >( );
                }
            }
            string str = this.CanDump ? this.method_0( class60.securityId_0, class60.dateTime_1, class60.dateTime_1, typeof( Level1ChangeMessage ), null ) : null;
            if( str != null && File.Exists( str ) )
            {
                class60.byte_0 = File.ReadAllBytes( str );
            }
            else
            {
                string string_2_1;
                string string_2_2 = TrueFXHistorySource.smethod_0( class60.securityId_0, class60.dateTime_1, out string_2_1 );
                class60.byte_0 = this.method_2< byte[ ] >( string_2_2, string_2_1, TrueFXHistorySource.Class61.action_0 ?? ( TrueFXHistorySource.Class61.action_0 = new Action< HttpRequest >( TrueFXHistorySource.Class61.class61_0.method_0 ) ), false, TrueFXHistorySource.Class61.func_0 ?? ( TrueFXHistorySource.Class61.func_0 = new Func< HttpResponse, byte[ ] >( TrueFXHistorySource.Class61.class61_0.method_1 ) ) );
                if( str != null )
                {
                    str.CreateDirIfNotExists( );
                    class60.byte_0.Save( str );
                }
            }
            return CultureInfo.InvariantCulture.DoInCulture<Level1ChangeMessage[ ]>( new Func<Level1ChangeMessage[ ]>( class60.method_0 ) );
        }

        private IEnumerable< Level1ChangeMessage > method_1(
          Stream stream_0,
          SecurityId securityId_0,
          DateTime dateTime_0,
          DateTime dateTime_1 )
        {
            throw new NotImplementedException( );
            //return ( IEnumerable<Level1ChangeMessage> )new TrueFXHistorySource.Class62( -2 )
            //{
            //    trueFXHistorySource_0 = this,
            //    stream_1 = stream_0,
            //    securityId_1 = securityId_0,
            //    dateTime_3 = dateTime_0,
            //    dateTime_1 = dateTime_1
            //};
        }

        private T method_2< T >(
          string string_2,
          string string_3,
          Action< HttpRequest > action_0,
          bool bool_0,
          Func< HttpResponse, T > func_0 )
        {
            if( action_0 == null )
            {
                throw new ArgumentNullException( "action" );
            }

            if( func_0 == null )
            {
                throw new ArgumentNullException( "response" );
            }

            using( HttpRequest httpRequest = new HttpRequest( )
            {
                Cookies = this.cookieDictionary_0,
                UserAgent = Http.ChromeUserAgent( )
            } )
            {
                httpRequest.Referer = string_3;
                action_0( httpRequest );
                return func_0( bool_0 ? httpRequest.Post( string_2 ) : httpRequest.Get( string_2, null ) );
            }
        }

        private sealed class Class60
        {
            public byte[ ] byte_0;
            public TrueFXHistorySource trueFXHistorySource_0;
            public SecurityId securityId_0;
            public DateTime dateTime_0;
            public DateTime dateTime_1;
            public Func< ZipArchiveEntry, IEnumerable< Level1ChangeMessage > > func_0;

            internal Level1ChangeMessage[ ] method_0( )
            {
                using( ZipArchive zipArchive = new ZipArchive( this.byte_0.To< Stream >( ) ) )
                {
                    return zipArchive.Entries.SelectMany< ZipArchiveEntry, Level1ChangeMessage >( this.func_0 ?? ( this.func_0 = new Func< ZipArchiveEntry, IEnumerable< Level1ChangeMessage > >( this.method_1 ) ) ).ToArray< Level1ChangeMessage >( );
                }
            }

            internal IEnumerable< Level1ChangeMessage > method_1(
              ZipArchiveEntry zipArchiveEntry_0 )
            {
                using( Stream stream_0 = zipArchiveEntry_0.Open( ) )
                {
                    return this.trueFXHistorySource_0.method_1( stream_0, this.securityId_0, this.dateTime_0, this.dateTime_1 ).ToArray<Level1ChangeMessage>();
                }
            }
        }

        [Serializable]
        private sealed class Class61
        {
            public static readonly TrueFXHistorySource.Class61 class61_0 = new TrueFXHistorySource.Class61( );
            public static Action< HttpRequest > action_0;
            public static Func< HttpResponse, byte[ ] > func_0;
            public static Func< string, string > func_1;
            public static Func< string, bool > func_2;

            internal void method_0( HttpRequest httpRequest_0 )
            {
            }

            internal byte[ ] method_1( HttpResponse httpResponse_0 )
            {
                return httpResponse_0.ToBytes( );
            }

            internal string method_2( string string_0 )
            {
                return string_0.Trim( );
            }

            internal bool method_3( string string_0 )
            {
                return !string_0.IsEmpty( );
            }
        }

        //private sealed class Class62 : IEnumerable<Level1ChangeMessage>, IEnumerator<Level1ChangeMessage>, IEnumerable, IDisposable, IEnumerator
        //{
        //    private int int_0;
        //    private Level1ChangeMessage level1ChangeMessage_0;
        //    private int int_1;
        //    public TrueFXHistorySource trueFXHistorySource_0;
        //    private DateTime dateTime_0;
        //    public DateTime dateTime_1;
        //    private Stream stream_0;
        //    public Stream stream_1;
        //    private DateTime dateTime_2;
        //    public DateTime dateTime_3;
        //    private SecurityId securityId_0;
        //    public SecurityId securityId_1;
        //    private bool bool_0;
        //    private IEnumerator<string> ienumerator_0;

        //    [DebuggerHidden]
        //    public Class62( int int_2 )
        //    {
        //        this.int_0 = int_2;
        //        this.int_1 = Environment.CurrentManagedThreadId;
        //    }

        //    [DebuggerHidden]
        //    void IDisposable.Dispose( )
        //    {
        //        switch ( this.int_0 )
        //        {
        //            case -3:
        //            case 1:
        //                try
        //                {
        //                }
        //                finally
        //                {
        //                    this.method_0( );
        //                }
        //                break;
        //        }
        //    }

        //    bool IEnumerator.MoveNext( )
        //    {
        //        bool flag;
        //        // ISSUE: fault handler
        //        try
        //        {
        //            int int0 = this.int_0;
        //            TrueFXHistorySource fxHistorySource0 = this.trueFXHistorySource_0;
        //            switch ( int0 )
        //            {
        //                case 0:
        //                    this.int_0 = -1;
        //                    this.bool_0 = fxHistorySource0.synchronizedDictionary_0.ContainsKey( this.dateTime_0 );
        //                    this.ienumerator_0 = this.stream_0.EnumerateLines( ( Encoding )null ).GetEnumerator( );
        //                    this.int_0 = -3;
        //                    break;
        //                case 1:
        //                    this.int_0 = -3;
        //                    break;
        //                default:
        //                    flag = false;
        //                    goto label_14;
        //            }
        //            while ( this.ienumerator_0.MoveNext( ) )
        //            {
        //                string[ ] array = ( ( IEnumerable<string> )this.ienumerator_0.Current.Split( ',' ) ).Select<string, string>( TrueFXHistorySource.Class61.func_1 ?? ( TrueFXHistorySource.Class61.func_1 = new Func<string, string>( TrueFXHistorySource.Class61.class61_0.method_2 ) ) ).Where<string>( TrueFXHistorySource.Class61.func_2 ?? ( TrueFXHistorySource.Class61.func_2 = new Func<string, bool>( TrueFXHistorySource.Class61.class61_0.method_3 ) ) ).ToArray<string>( );
        //                if ( array.Length >= 4 )
        //                {
        //                    DateTime dateTime = array[ 1 ].ToDateTime( "yyyyMMdd HH:mm:ss.fff", ( CultureInfo )null );
        //                    DateTime date = dateTime.Date;
        //                    if ( !this.bool_0 )
        //                    {
        //                        fxHistorySource0.synchronizedDictionary_0.Add( this.dateTime_0, date );
        //                        this.bool_0 = true;
        //                    }
        //                    if ( !( date < this.dateTime_2 ) )
        //                    {
        //                        if ( date > this.dateTime_2 )
        //                        {
        //                            flag = false;
        //                            this.method_0( );
        //                            goto label_14;
        //                        }
        //                        else
        //                        {
        //                            Level1ChangeMessage message = new Level1ChangeMessage( );
        //                            message.SecurityId = this.securityId_0;
        //                            message.ServerTime = dateTime.ApplyTimeZone( TimeZoneInfo.Utc );
        //                            this.level1ChangeMessage_0 = message.TryAdd<Level1ChangeMessage, Level1Fields>( Level1Fields.BestBidPrice, array[ 2 ].To<Decimal>( ), false ).TryAdd<Level1ChangeMessage, Level1Fields>( Level1Fields.BestAskPrice, array[ 3 ].To<Decimal>( ), false );
        //                            this.int_0 = 1;
        //                            flag = true;
        //                            goto label_14;
        //                        }
        //                    }
        //                }
        //            }
        //            this.method_0( );
        //            this.ienumerator_0 = ( IEnumerator<string> )null;
        //            flag = false;
        //        }
        //        finally
        //        {

        //        }
        ////      {
        ////            this.System\u002EIDisposable\u002EDispose( );
        ////        }
        //        label_14:
        //        return flag;
        //    }

        //    private void method_0( )
        //    {
        //        this.int_0 = -1;
        //        if ( this.ienumerator_0 == null )
        //            return;
        //        this.ienumerator_0.Dispose( );
        //    }

        //    Level1ChangeMessage IEnumerator<Level1ChangeMessage>.Current
        //    {
        //        [DebuggerHidden]
        //        get
        //        {
        //            return this.level1ChangeMessage_0;
        //        }
        //    }

        //    
        //    object IEnumerator.Current
        //    {
        //        [DebuggerHidden]
        //        get
        //        {
        //            return ( object )this.level1ChangeMessage_0;
        //        }
        //    }

        //    
        //}

        private sealed class Class63
        {
            public StockSharp.BusinessEntities.Security security_0;

            internal MarketDepth method_0( Level1ChangeMessage level1ChangeMessage_0 )
            {
                return level1ChangeMessage_0.ToMarketDepth( this.security_0 );
            }
        }
    }
}
