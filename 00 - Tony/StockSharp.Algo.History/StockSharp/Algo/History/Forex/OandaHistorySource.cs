using Ecng.Common;
using Ecng.Net;
using Ecng.Web;
using HtmlAgilityPack;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using xNet;

namespace StockSharp.Algo.History.Forex
{
    public class OandaHistorySource : BaseHistorySource, ISecurityDownloader
    {
        private static string smethod_0( DateTime dateTime_0 )
        {
            return "{0}-{1}-{2}".Put( dateTime_0.Year.ToString( "0000" ), dateTime_0.Month.ToString( "#0" ), dateTime_0.Day.ToString( "#0" ) );
        }

        public IEnumerable< Level1ChangeMessage > LoadRates(
      Security security,
      DateTime from,
      DateTime to )
        {
            OandaHistorySource.Class58 class58_1 = new OandaHistorySource.Class58( );
            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            string[ ] strArray1 = this.GetSecurityCode( security ).Split( '/' );
            if( strArray1.Length != 2 )
            {
                throw new ArgumentException( LocalizedStrings.Str2094Params.Put( security.Id ), nameof( security ) );
            }

            Url url = new Url( "http://www.oanda.com/lang/ru/currency/historical-rates/download?period=daily&display=absolute&rate=0&data_range=d30&price=bid&view=table&base_currency_1=&base_currency_2=&base_currency_3=&base_currency_4=&download=csv&" );
            url.QueryString.Append( "base_currency_0", strArray1[0] ).Append( "quote_currency", strArray1[1] ).Append( "start_date", OandaHistorySource.smethod_0( from ) ).Append( "end_date", OandaHistorySource.smethod_0( to ) );
            using( HttpRequest httpRequest = new HttpRequest( ) )
            {
                httpRequest.UserAgent = Http.ChromeUserAgent( );
                httpRequest.Referer = "http://www.oanda.com/lang/ru/currency/historical-rates/";
                string str = Encoding.UTF8.GetString( httpRequest.Get( url, null ).ToBytes( ) );
                OandaHistorySource.Class58 class58_2 = class58_1;
                string[] strArray2;
                if( !str.IsEmpty( ) )
                {
                    strArray2 = str.Split( '\n' );
                }
                else
                {
                    strArray2 = ArrayHelper.Empty< string >( );
                }

                class58_2.string_0 = strArray2;
            }
            class58_1.securityId_0 = security.ToSecurityId( null );
            return CultureInfo.InvariantCulture.DoInCulture<List<Level1ChangeMessage>>( new Func<List<Level1ChangeMessage>>( class58_1.method_0 ) );
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

            foreach( HtmlNode selectNode in new HtmlWeb()
            {
                OverrideEncoding = Encoding.UTF8
            }.Load( "https://www.oanda.com/forex-trading/markets/live" ).DocumentNode.SelectNodes( "//div[@class='rtspreads-container']" ) )
            {
                string lowerInvariant = selectNode.Attributes[ "id" ].Value.ToLowerInvariant( );
                SecurityTypes securityTypes;
                if( !( lowerInvariant == "forex_block" ) )
                {
                    if( !( lowerInvariant == "indices_block" ) )
                    {
                        if( !( lowerInvariant == "commodities_block" ) )
                        {
                            if( !( lowerInvariant == "bonds_block" ) )
                            {
                                if( lowerInvariant == "metals_block" )
                                {
                                    securityTypes = SecurityTypes.Commodity;
                                }
                                else
                                {
                                    throw new InvalidOperationException( LocalizedStrings.Str2140Params.Put( lowerInvariant ) );
                                }
                            }
                            else
                            {
                                securityTypes = SecurityTypes.Bond;
                            }
                        }
                        else
                        {
                            securityTypes = SecurityTypes.Commodity;
                        }
                    }
                    else
                    {
                        securityTypes = SecurityTypes.Index;
                    }
                }
                else
                {
                    securityTypes = SecurityTypes.Currency;
                }

                foreach( string secCode in selectNode.ChildNodes["input"].Attributes["value"].Value.Split( ",", true ).Select< string, string >( OandaHistorySource.Class59.func_0 ?? ( OandaHistorySource.Class59.func_0 = new Func< string, string >( OandaHistorySource.Class59.class59_0.method_0 ) ) ).Where< string >( OandaHistorySource.Class59.func_1 ?? ( OandaHistorySource.Class59.func_1 = new Func< string, bool >( OandaHistorySource.Class59.class59_0.method_1 ) ) ) )
                {
                    if( isCancelled( ) )
                    {
                        return;
                    }

                    string id = this.SecurityIdGenerator.GenerateId( secCode, ExchangeBoard.Ond );
                    if( securityStorage.LookupById( id ) == null )
                    {
                        Security security = new Security( )
            {
              Id = id,
              Code = secCode,
              Board = ExchangeBoard.Ond,
              Type = new SecurityTypes?( securityTypes ),
              PriceStep = new Decimal?( new Decimal( 1, 0, 0, false, 5 ) )
            };
                        securityStorage.Save( security, false );
                        newSecurity( security );
                    }
                }
            }
        }

        private sealed class Class58
        {
            public string[] string_0;
            public SecurityId securityId_0;

            internal List< Level1ChangeMessage > method_0( )
            {
                List< Level1ChangeMessage > level1ChangeMessageList1 = new List< Level1ChangeMessage >( );
                for( int index = 5; index < this.string_0.Length - 5; ++index )
                {
                    string[] strArray = this.string_0[ index ].Split( ';' );
                    DateTime dt = strArray[ 0 ].Trim( '"' ).To< DateTime >( );
                    Decimal num = strArray[ 1 ].Trim( '"' ).To< Decimal >( );
                    List< Level1ChangeMessage > level1ChangeMessageList2 = level1ChangeMessageList1;
                    Level1ChangeMessage message = new Level1ChangeMessage( );
                    message.ServerTime = dt.ApplyTimeZone( TimeZoneInfo.Utc );
                    message.LocalTime = ( DateTimeOffset ) dt;
                    message.SecurityId = this.securityId_0;
                    Level1ChangeMessage level1ChangeMessage = message.Add< Level1ChangeMessage, Level1Fields >( Level1Fields.LastTradePrice, num );
                    level1ChangeMessageList2.Add( level1ChangeMessage );
                }
                return level1ChangeMessageList1;
            }
        }

        [Serializable]
    private sealed class Class59
    {
        public static readonly OandaHistorySource.Class59 class59_0 = new OandaHistorySource.Class59( );
        public static Func< string, string > func_0;
        public static Func< string, bool > func_1;

        internal string method_0( string string_0 )
        {
            return string_0.Trim( );
        }

        internal bool method_1( string string_0 )
        {
            return !string_0.IsEmpty( );
        }
    }
    }
}
