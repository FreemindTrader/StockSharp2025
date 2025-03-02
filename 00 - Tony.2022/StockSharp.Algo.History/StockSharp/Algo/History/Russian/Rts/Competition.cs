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
using System.Net;
using System.Net.FtpClient;
using System.Text;
using System.Web;

namespace StockSharp.Algo.History.Russian.Rts
{
    public class Competition : BaseHistorySource
    {
        private readonly SynchronizedSet< string > synchronizedSet_0 = new SynchronizedSet< string >( );
        private readonly SynchronizedDictionary< DateTime, Competition.CompetitionYear > synchronizedDictionary_0 = new SynchronizedDictionary< DateTime, Competition.CompetitionYear >( );
        private static readonly DateTime[] dateTime_0 = new DateTime[10];
        private readonly IExchangeInfoProvider iexchangeInfoProvider_0;

        static Competition( )
        {
            for( int index = 0; index < Competition.dateTime_0.Length; ++index )
            {
                Competition.dateTime_0[ index ] = new DateTime( 2006 + index, 1, 1 );
            }
        }

        public Competition( IExchangeInfoProvider exchangeInfoProvider )
        {
            IExchangeInfoProvider exchangeInfoProvider1 = exchangeInfoProvider;
            if( exchangeInfoProvider1 == null )
            {
                throw new ArgumentNullException( nameof( exchangeInfoProvider ) );
            }

            this.iexchangeInfoProvider_0 = exchangeInfoProvider1;
        }

        public static IEnumerable< DateTime > AllYears
        {
            get
            {
                return dateTime_0;
            }
        }

        public Competition.CompetitionYear Get( DateTime year )
        {
            return this.synchronizedDictionary_0.SafeAdd< DateTime, Competition.CompetitionYear >( new DateTime( year.Year, 1, 1 ), new Func< DateTime, Competition.CompetitionYear >( this.method_0 ) );
        }

        private Competition.CompetitionYear method_0( DateTime dateTime_1 )
        {
            return new Competition.CompetitionYear( this, dateTime_1 );
        }

        public class CompetitionYear
        {
            private readonly SynchronizedDictionary< string, int > synchronizedDictionary_0 = new SynchronizedDictionary< string, int >( );
            private readonly Competition competition_0;
            private readonly DateTime dateTime_0;
            private IEnumerable< string > ienumerable_0;
            private IEnumerable< DateTime > ienumerable_1;

            internal CompetitionYear( Competition competition_1, DateTime dateTime_1 )
            {
                this.competition_0 = competition_1;
                this.dateTime_0 = dateTime_1;
            }

            public DateTime Year
            {
                get
                {
                    return this.dateTime_0;
                }
            }

            public IEnumerable< string > Members
            {
                get
                {
                    if( this.ienumerable_0 == null )
                    {
                        this.method_1( );
                    }

                    return this.ienumerable_0;
                }
            }

            public IEnumerable< DateTime > Days
            {
                get
                {
                    if( this.ienumerable_1 == null )
                    {
                        this.method_1( );
                    }

                    return this.ienumerable_1;
                }
            }

            private Security method_0( ISecurityStorage isecurityStorage_0, string string_0 )
            {
                if( string_0.IsEmpty( ) )
                {
                    throw new ArgumentNullException( "code" );
                }

                Security[ ] array = isecurityStorage_0.LookupByCode( string_0 ).ToArray< Security >( );
                if( array.Length == 1 )
                {
                    return array[ 0 ];
                }

                if( array.Length > 1 )
                {
                    return array.FirstOrDefault< Security >( Competition.CompetitionYear.Class136.func_0 ?? ( Competition.CompetitionYear.Class136.func_0 = new Func< Security, bool >( Competition.CompetitionYear.Class136.class136_0.method_0 ) ) ) ?? array.FirstOrDefault< Security >( Competition.CompetitionYear.Class136.func_1 ?? ( Competition.CompetitionYear.Class136.func_1 = new Func< Security, bool >( Competition.CompetitionYear.Class136.class136_0.method_1 ) ) ) ?? array[ 0 ];
                }

                Security security1 = isecurityStorage_0.LookupByCode( string_0 ).FirstOrDefault< Security >( );
                if( security1 != null )
                {
                    return security1;
                }

                Security security2 = new Security( )
        {
          Code = string_0,
          Board = ExchangeBoard.Forts
        };
                SecurityInfo[] gparam_0 = Extension.smethod_0( string_0 );
                SecurityInfo securityInfo;
                if( gparam_0 == null )
                {
                    securityInfo = null;
                }
                else
                {
                    securityInfo = Class2.smethod_1< SecurityInfo >( gparam_0 );
                    if( securityInfo != null )
                    {
                        securityInfo.FillTo( security2, this.competition_0.iexchangeInfoProvider_0 );
                        goto label_13;
                    }
                }
                label_13:
security2.Id = this.competition_0.SecurityIdGenerator.GenerateId( security2.Code, security2.Board );
                isecurityStorage_0.Save( security2, false );
                return security2;
            }

            public IEnumerable< ExecutionMessage > GetTrades(
        ISecurityStorage securityStorage,
        string member,
        DateTime date )
            {
                Competition.CompetitionYear.Class134 class134 = new Competition.CompetitionYear.Class134( );
                class134.competitionYear_0 = this;
                class134.isecurityStorage_0 = securityStorage;
                class134.string_0 = member;
                class134.dateTime_0 = date;
                if( class134.isecurityStorage_0 == null )
                {
                    throw new ArgumentNullException( nameof( securityStorage ) );
                }

                if( class134.string_0.IsEmpty( ) )
                {
                    throw new ArgumentNullException( nameof( member ) );
                }

                if( !this.Days.Contains< DateTime >( class134.dateTime_0 ) )
                {
                    throw new ArgumentOutOfRangeException( nameof( date ), class134.dateTime_0, LocalizedStrings.Str2105 );
                }

                this.competition_0.synchronizedSet_0.Add( class134.string_0 );
                if( this.Year.Year >= 2013 )
                {
                    Competition.CompetitionYear.Class131 class131 = new Competition.CompetitionYear.Class131( );
                    class131.class134_0 = class134;
                    class131.ftpClient_0 = new System.Net.FtpClient.FtpClient( );
                    try
                    {
                        Competition.CompetitionYear.Class135 class135 = new Competition.CompetitionYear.Class135( )
            {
              class131_0 = class131
            };
                        class135.class131_0.ftpClient_0.Host = "ftp.moex.com";
                        class135.class131_0.ftpClient_0.Credentials = new NetworkCredential( "anonymous", "anonymous" );
                        class135.class131_0.ftpClient_0.ReadTimeout = ( int ) TimeSpan.FromMinutes( 1.0 ).TotalMilliseconds;
                        class135.class131_0.ftpClient_0.Connect( );
                        class135.nullable_0 = this.synchronizedDictionary_0.TryGetValue2< string, int >( class135.class131_0.class134_0.string_0 );
                        if( !class135.nullable_0.HasValue )
                        {
                            throw new ArgumentException( LocalizedStrings.Str2106Params.Put( class135.class131_0.class134_0.string_0 ), nameof( member ) );
                        }

                        return class135.class131_0.ftpClient_0.GetListing( "pub/info/stats_contest/{0:yyyy}/{0:yyyyMMdd}".Put( class135.class131_0.class134_0.dateTime_0 ) ).Where<FtpListItem>( new Func<FtpListItem, bool>( class135.method_0 ) ).SelectMany<FtpListItem, ExecutionMessage>( new Func<FtpListItem, IEnumerable<ExecutionMessage>>( class135.class131_0.method_0 ) ).ToArray<ExecutionMessage>();
                    }
                    finally
                    {
                        if( class131.ftpClient_0 != null )
                        {
                            class131.ftpClient_0.Dispose( );
                        }
                    }
                }
                else
                {
                    Competition.CompetitionYear.Class132 class132 = new Competition.CompetitionYear.Class132( )
          {
            class134_0 = class134
          };
                    class132.htmlDocument_0 = Competition.CompetitionYear.smethod_0( ).Load( "http://investor.moex.com/ru/statistics/{0}/?act=deals&nick={1}&date={2:yyyyMMdd}".Put( Year.Year, class132.class134_0.string_0, class132.class134_0.dateTime_0 ) );
                    class132.int_0 = class132.class134_0.dateTime_0.Year >= 2009 ? 1 : 0;
                    class132.int_1 = class132.class134_0.dateTime_0.Year >= 2011 ? 1 : 0;
                    return CultureInfo.InvariantCulture.DoInCulture< IEnumerable< ExecutionMessage > >( new Func< IEnumerable< ExecutionMessage > >( class132.method_0 ) );
                }
            }

            private void method_1( )
            {
                if( this.Year.Year >= 2013 )
                {
                    using( System.Net.FtpClient.FtpClient ftpClient = new System.Net.FtpClient.FtpClient( ) )
                    {
                        ftpClient.Host = "ftp.moex.com";
                        ftpClient.Credentials = new NetworkCredential( "anonymous", "anonymous" );
                        ftpClient.Connect( );
                        FtpListItem ftpListItem = ftpClient.GetListing( "pub/info/stats_contest/" ).FirstOrDefault< FtpListItem >( new Func< FtpListItem, bool >( this.method_2 ) );
                        if( ftpListItem == null )
                        {
                            throw new InvalidOperationException( "Для {0} года нет данных.".Put( Year.Year ) );
                        }

                        this.ienumerable_1 = ftpClient.GetListing( ftpListItem.FullName ).Where<FtpListItem>( Competition.CompetitionYear.Class136.func_2 ?? ( Competition.CompetitionYear.Class136.func_2 = new Func<FtpListItem, bool>( Competition.CompetitionYear.Class136.class136_0.method_2 ) ) ).Select<FtpListItem, DateTime>( Competition.CompetitionYear.Class136.func_3 ?? ( Competition.CompetitionYear.Class136.func_3 = new Func<FtpListItem, DateTime>( Competition.CompetitionYear.Class136.class136_0.method_3 ) ) ).Where<DateTime>( new Func<DateTime, bool>( this.method_3 ) ).ToArray<DateTime>();
                        System.Net.FtpClient.FtpClient ftpClient_0 = ftpClient;
                        string string_0 = "pub/info/stats_contest/{0}/trader.csv".Put( Year.Year );
                        foreach( string str in StringHelper.WindowsCyrillic.GetString( ftpClient_0.smethod_2( string_0 ).To< byte[] >( ) ).Split( Environment.NewLine, true ) )
                        {
                            char[] chArray = new char[1] { ';' };
                            string[] strArray = str.Split( chArray );
                            this.synchronizedDictionary_0.TryAdd< string, int >( strArray[ 0 ], strArray[ 1 ].To< int >( ) );
                        }
                        this.ienumerable_0 = this.synchronizedDictionary_0.Keys.ToArray<string>();
                    }
                }
                else
                {
                    HtmlDocument htmlDocument = Competition.CompetitionYear.smethod_0( ).Load( "http://investor.rts.ru/ru/statistics/{0}/default.aspx?act=deals".Put( Year.Year ) );
                    if( this.Year.Year == 2012 )
                    {
                        this.ienumerable_0 = Class38.smethod_5().Split( Environment.NewLine, true );
                    }
                    else
                    {
                        HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode( "//select[@name='nick']" );
                        this.ienumerable_0 = ( htmlNode != null ? htmlNode.Descendants( "option" ).Select<HtmlNode, string>( Competition.CompetitionYear.Class136.func_4 ?? ( Competition.CompetitionYear.Class136.func_4 = new Func<HtmlNode, string>( Competition.CompetitionYear.Class136.class136_0.method_4 ) ) ).ToArray<string>() : ( IEnumerable< string > ) ( string[] ) null ) ?? Enumerable.Empty< string >( );
                    }
                    this.ienumerable_1 = htmlDocument.DocumentNode.SelectSingleNode( "//select[@name='date']" ).Descendants( "option" ).Select<HtmlNode, DateTime>( Competition.CompetitionYear.Class136.func_5 ?? ( Competition.CompetitionYear.Class136.func_5 = new Func<HtmlNode, DateTime>( Competition.CompetitionYear.Class136.class136_0.method_5 ) ) ).OrderBy<DateTime>().ToArray<DateTime>();
                }
            }

            private static HtmlWeb smethod_0( )
            {
                return new HtmlWeb( )
        {
          OverrideEncoding = Encoding.UTF8
        };
            }

            private bool method_2( FtpListItem ftpListItem_0 )
            {
                return ftpListItem_0.Name == this.Year.Year.To< string >( );
            }

            private bool method_3( DateTime dateTime_1 )
            {
                return dateTime_1.Year == this.Year.Year;
            }

            private sealed class Class131
            {
                public System.Net.FtpClient.FtpClient ftpClient_0;
                public Competition.CompetitionYear.Class134 class134_0;

                internal IEnumerable< ExecutionMessage > method_0(
          FtpListItem ftpListItem_0 )
                {
                    return ( IEnumerable< ExecutionMessage > ) CultureInfo.InvariantCulture.DoInCulture< ExecutionMessage[] >( new Func< ExecutionMessage[] >( new Competition.CompetitionYear.Class133( )
          {
            class131_0 = this,
            stream_0 = this.ftpClient_0.smethod_2( ftpListItem_0.FullName )
          }.method_0 ) );
                }
            }

            private sealed class Class132
            {
                public HtmlDocument htmlDocument_0;
                public int int_0;
                public int int_1;
                public Competition.CompetitionYear.Class134 class134_0;
                public Func< HtmlNode, ExecutionMessage > func_0;

                internal IEnumerable< ExecutionMessage > method_0( )
                {
                    HtmlNodeCollection source = this.htmlDocument_0.DocumentNode.SelectNodes( "//table[@class='table table-bordered']" );
                    if( source == null )
                    {
                        return Enumerable.Empty< ExecutionMessage >( );
                    }

                    return source.Last<HtmlNode>().Descendants( "tr" ).Skip<HtmlNode>( 1 ).Select<HtmlNode, ExecutionMessage>( this.func_0 ?? ( this.func_0 = new Func<HtmlNode, ExecutionMessage>( this.method_1 ) ) ).ToArray<ExecutionMessage>();
                }

                internal ExecutionMessage method_1( HtmlNode htmlNode_0 )
                {
                    HtmlNode[] array = htmlNode_0.Descendants( "td" ).Skip< HtmlNode >( this.int_0 ).ToArray< HtmlNode >( );
                    string string_0 = HttpUtility.HtmlDecode( array[ this.int_1 ].InnerText ).ReplaceWhiteSpaces( ).Trim( );
                    Decimal num1 = HttpUtility.HtmlDecode( array[ 3 + this.int_1 ].InnerText ).ReplaceWhiteSpaces( ).RemoveSpaces( ).To< Decimal >( );
                    Decimal num2 = HttpUtility.HtmlDecode( array[ 2 + this.int_1 ].InnerText ).ReplaceWhiteSpaces( ).RemoveSpaces( ).Replace( ',', '.' ).To< Decimal >( );
                    string paramName = HttpUtility.HtmlDecode( array[ 1 + this.int_1 ].InnerText ).ReplaceWhiteSpaces( ).RemoveSpaces( ).Trim( );
                    string lowerInvariant = paramName.ToLowerInvariant( );
                    Sides sides;
                    if( !( lowerInvariant == "покупка" ) )
                    {
                        if( !( lowerInvariant == "продажа" ) )
                        {
                            throw new ArgumentOutOfRangeException( paramName );
                        }

                        sides = Sides.Sell;
                    }
                    else
                    {
                        sides = Sides.Buy;
                    }

                    return new ExecutionMessage( )
          {
            ExecutionType = new ExecutionTypes?( ExecutionTypes.OrderLog ),
            Side = sides,
            TradeId = new long?( 1L ),
            OrderId = new long?( 1L ),
            PortfolioName = this.class134_0.string_0,
            SecurityId = this.class134_0.competitionYear_0.method_0( this.class134_0.isecurityStorage_0, string_0 ).ToSecurityId( null ),
            TradePrice = new Decimal?( num2 ),
            OrderPrice = num2,
            OrderVolume = new Decimal?( Math.Abs( num1 ) ),
            ServerTime = ( this.class134_0.dateTime_0 + HttpUtility.HtmlDecode( array[ 4 + this.int_1 ].InnerText ).ReplaceWhiteSpaces( ).Trim( ).ToTimeSpan( "c", null ) ).ApplyTimeZone( TimeHelper.Moscow ),
            OrderState = new OrderStates?( OrderStates.Done )
          };
                }
            }

            private sealed class Class133
            {
                public Stream stream_0;
                public Competition.CompetitionYear.Class131 class131_0;

                internal ExecutionMessage[] method_0( )
                {
                    using( ZipArchive zipArchive = new ZipArchive( this.stream_0 ) )
                    {
                        return zipArchive.Entries.SelectMany< ZipArchiveEntry, ExecutionMessage >( this.class131_0.class134_0.func_1 ?? ( this.class131_0.class134_0.func_1 = new Func< ZipArchiveEntry, IEnumerable< ExecutionMessage > >( this.class131_0.class134_0.method_0 ) ) ).ToArray< ExecutionMessage >( );
                    }
                }
            }

            private sealed class Class134
            {
                public Competition.CompetitionYear competitionYear_0;
                public ISecurityStorage isecurityStorage_0;
                public string string_0;
                public DateTime dateTime_0;
                public Func< string, ExecutionMessage > func_0;
                public Func< ZipArchiveEntry, IEnumerable< ExecutionMessage > > func_1;

                internal IEnumerable< ExecutionMessage > method_0(
          ZipArchiveEntry zipArchiveEntry_0 )
                {
                    using( Stream stream = zipArchiveEntry_0.Open( ) )
                    {
                        return stream.EnumerateLines( null ).Select<string, ExecutionMessage>( this.func_0 ?? ( this.func_0 = new Func<string, ExecutionMessage>( this.method_1 ) ) ).ToArray<ExecutionMessage>();
                    }
                }

                internal ExecutionMessage method_1( string string_1 )
                {
                    string[] strArray = string_1.Split( ';' );
                    SecurityId securityId = this.competitionYear_0.method_0( this.isecurityStorage_0, strArray[ 1 ].Trim( ) ).ToSecurityId( null );
                    Decimal num1 = strArray[ 2 ].To< Decimal >( );
                    Decimal num2 = strArray[ 3 ].To< Decimal >( );
                    return new ExecutionMessage( )
          {
            ExecutionType = new ExecutionTypes?( ExecutionTypes.OrderLog ),
            TradeId = new long?( 1L ),
            OrderId = new long?( 1L ),
            PortfolioName = this.string_0,
            ServerTime = strArray[ 0 ].ToDateTime( "yyyy-MM-dd HH:mm:ss.fff", null ).ApplyTimeZone( TimeHelper.Moscow ),
            SecurityId = securityId,
            OrderVolume = new Decimal?( Math.Abs( num1 ) ),
            TradePrice = new Decimal?( num2 ),
            OrderPrice = num2,
            Side = num1 > Decimal.Zero ? Sides.Buy : Sides.Sell,
            OrderState = new OrderStates?( OrderStates.Done )
          };
                }
            }

            private sealed class Class135
            {
                public int? nullable_0;
                public Competition.CompetitionYear.Class131 class131_0;

                internal bool method_0( FtpListItem ftpListItem_0 )
                {
                    return ftpListItem_0.Name.ContainsIgnoreCase( "_{0}.zip".Put( nullable_0 ) );
                }
            }

            [Serializable]
      private sealed class Class136
      {
          public static readonly Competition.CompetitionYear.Class136 class136_0 = new Competition.CompetitionYear.Class136( );
          public static Func< Security, bool > func_0;
          public static Func< Security, bool > func_1;
          public static Func< FtpListItem, bool > func_2;
          public static Func< FtpListItem, DateTime > func_3;
          public static Func< HtmlNode, string > func_4;
          public static Func< HtmlNode, DateTime > func_5;

          internal bool method_0( Security security_0 )
          {
              if( security_0.Board != ExchangeBoard.Finam )
              {
                  return security_0.Board != ExchangeBoard.Forts;
              }

              return false;
          }

          internal bool method_1( Security security_0 )
          {
              return security_0.Board != ExchangeBoard.Finam;
          }

          internal bool method_2( FtpListItem ftpListItem_0 )
          {
              if( ftpListItem_0.Type == FtpFileSystemObjectType.Directory )
              {
                  return !ftpListItem_0.Name.CompareIgnoreCase( "all" );
              }

              return false;
          }

          internal DateTime method_3( FtpListItem ftpListItem_0 )
          {
              return ftpListItem_0.Name.ToDateTime( "yyyyMMdd", null );
          }

          internal string method_4( HtmlNode htmlNode_0 )
          {
              return htmlNode_0.Attributes[ "value" ].Value;
          }

          internal DateTime method_5( HtmlNode htmlNode_0 )
          {
              return htmlNode_0.Attributes[ "value" ].Value.ToDateTime( "yyyyMMdd", null );
          }
      }
        }
    }
}
