using Ecng.Collections;
using Ecng.Common;
using Ecng.Web;
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
using System.Security;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace StockSharp.Algo.History
{
    public class QuandlHistorySource : BaseDumpableHistorySource, ISecurityDownloader
    {
        private static readonly TimeSpan[ ] timeSpan_0 = new TimeSpan[ 5 ]
        {
      TimeSpan.FromDays( 1.0 ),
      TimeSpan.FromDays( 7.0 ),
      TimeSpan.FromTicks( 25920000000000L ),
      TimeSpan.FromTicks( 77760000000000L ),
      TimeSpan.FromTicks( 315360000000000L )
        };
        private static readonly Dictionary< TimeSpan, string > dictionary_0 = new Dictionary< TimeSpan, string >( )
    {
      {
        QuandlHistorySource.timeSpan_0[ 0 ],
        "daily"
      },
      {
        QuandlHistorySource.timeSpan_0[ 1 ],
        "weekly"
      },
      {
        QuandlHistorySource.timeSpan_0[ 2 ],
        "monthly"
      },
      {
        QuandlHistorySource.timeSpan_0[ 3 ],
        "quarterly"
      },
      {
        QuandlHistorySource.timeSpan_0[ 4 ],
        "annual"
      }
    };
        private string string_1;
        private SecureString secureString_0;

        public QuandlHistorySource(
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

        public string DatabaseCode
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

        public string AuthToken
        {
            get
            {
                return this.secureString_0.To< string >( );
            }
            set
            {
                this.secureString_0 = value.To< SecureString >( );
            }
        }

        private string method_0( StockSharp.BusinessEntities.Security security_0 )
        {
            if( security_0 == null )
            {
                throw new ArgumentNullException( "security" );
            }

            string str = security_0.Board?.Code;
            if( str.IsEmpty( ) )
            {
                str = this.DatabaseCode;
            }

            return str;
        }

        public override string GetDumpFile(
          StockSharp.BusinessEntities.Security security,
          DateTime from,
          DateTime to,
          Type dataType,
          object arg )
        {
            if( dataType == null )
            {
                throw new ArgumentNullException( nameof( dataType ) );
            }

            if( !dataType.IsCandleMessage( ) )
            {
                throw new ArgumentOutOfRangeException( nameof( dataType ), dataType, LocalizedStrings.Str1655 );
            }

            return Path.Combine( this.DumpFolder, string.Format( "{0}_{1}_{2}_{3}_{4:yyyy_MM_dd_HH_mm}_{5:yyyy_MM_dd_HH_mm}.xml", dataType.Name.ToLowerInvariant(), ( object )TraderHelper.CandleArgToFolderName( arg ), this.method_0( security ), security.Id.SecurityIdToFolderName(), from, to ) );
        }

        public IEnumerable< TimeFrameCandleMessage > GetCandles(
          StockSharp.BusinessEntities.Security security,
          TimeSpan timeFrame,
          DateTime beginDate,
          DateTime endDate )
        {
            QuandlHistorySource.Class105 class105 = new QuandlHistorySource.Class105( );
            class105.timeSpan_0 = timeFrame;
            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            if( beginDate > endDate )
            {
                throw new ArgumentOutOfRangeException( nameof( beginDate ), LocalizedStrings.Str1119Params.Put( beginDate, endDate ) );
            }

            string str = QuandlHistorySource.dictionary_0.TryGetValue< TimeSpan, string >( class105.timeSpan_0 );
            if( str == null )
            {
                throw new ArgumentOutOfRangeException( nameof( timeFrame ), class105.timeSpan_0, LocalizedStrings.Str1219 );
            }

            string path = this.CanDump ? this.GetDumpFile( security, beginDate, endDate, typeof( TimeFrameCandleMessage ), class105.timeSpan_0 ) : null;
            if( path != null && System.IO.File.Exists( path ) )
            {
                class105.memoryStream_0 = System.IO.File.ReadAllBytes( path ).To< MemoryStream >( );
            }
            else
            {
                SecurityId securityId = security.Id.ToSecurityId( null );
                Url url = new Url( "https://www.quandl.com/api/v3/datasets/" + this.method_0( security ) + "/" + securityId.SecurityCode + ".xml" );
                QueryString queryString = url.QueryString;
                queryString.Append( "sort_order", ( object )"asc" ).Append( "collapse", ( object )str );
                if( this.AuthToken != null )
                {
                    queryString.Append( "auth_token", ( object )this.AuthToken );
                }

                if( beginDate != DateTime.MinValue )
                {
                    queryString.Append( "trim_start", ( object )beginDate.ToString( "yyyy-MM-dd" ) );
                }

                if( endDate != DateTime.MaxValue )
                {
                    queryString.Append( "trim_end", ( object )endDate.ToString( "yyyy-MM-dd" ) );
                }

                WebRequest webRequest = WebRequest.Create( ( Uri )url );
                try
                {
                    using( WebResponse response = webRequest.GetResponse( ) )
                    {
                        using( Stream responseStream = response.GetResponseStream( ) )
                        {
                            class105.memoryStream_0 = new MemoryStream( );
                            responseStream.CopyTo( class105.memoryStream_0 );
                            if( path != null )
                            {
                                System.IO.File.WriteAllBytes( path, class105.memoryStream_0.To< byte[ ] >( ) );
                            }
                        }
                    }
                }
                catch( WebException ex )
                {
                    if( ex.Response == null )
                    {
                        throw;
                    }
                    else
                    {
                        if( ( ( HttpWebResponse )ex.Response ).StatusCode == HttpStatusCode.NotFound )
                        {
                            return Enumerable.Empty< TimeFrameCandleMessage >( );
                        }

                        throw;
                    }
                }
            }
            class105.memoryStream_0.Position = 0L;
            class105.securityId_0 = security.ToSecurityId( this.SecurityIdGenerator );
            return CultureInfo.InvariantCulture.DoInCulture<List<TimeFrameCandleMessage>>( new Func<List<TimeFrameCandleMessage>>( class105.method_0 ) );
        }

        private static Decimal? smethod_0( XElement[ ] xelement_0, int int_0 )
        {
            if( int_0 == -1 )
            {
                return new Decimal?( );
            }

            return xelement_0[ int_0 ].Value.Trim( ).To< Decimal? >( );
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

            string str1 = this.method_0( criteria );
            Url url = new Url( "https://www.quandl.com/api/v3/databases/" + str1 + "/metadata" );
            QueryString queryString = url.QueryString;
            if( !criteria.Code.IsEmpty( ) )
            {
                queryString.Append( "query", ( object )criteria.Code );
            }

            if( this.AuthToken != null )
            {
                queryString.Append( "auth_token", ( object )this.AuthToken );
            }

            SecurityIdGenerator securityIdGenerator = new SecurityIdGenerator( );
            using( WebResponse response = WebRequest.Create( ( Uri )url ).GetResponse( ) )
            {
                using( Stream responseStream = response.GetResponseStream( ) )
                {
                    using( ZipArchive zipArchive = new ZipArchive( responseStream, ZipArchiveMode.Read, true ) )
                    {
                        using( Stream stream = zipArchive.Entries[ 0 ].Open( ) )
                        {
                            FastCsvReader fastCsvReader = new FastCsvReader( stream, Encoding.UTF8 )
                            {
                                ColumnSeparator = ',',
                                LineSeparator = "\n"
                            };
                            if( !fastCsvReader.NextLine( ) )
                            {
                                return;
                            }

                            while( fastCsvReader.NextLine( ) )
                            {
                                string str2 = fastCsvReader.ReadString( );
                                if( criteria.Code.IsEmpty( ) || str2.ContainsIgnoreCase( criteria.Code ) )
                                {
                                    string id = securityIdGenerator.GenerateId( str2, str1 );
                                    if( securityStorage.LookupById( id ) == null )
                                    {
                                        StockSharp.BusinessEntities.Security security = new StockSharp.BusinessEntities.Security( )
                                        {
                                            Code = str2,
                                            Id = id,
                                            ShortName = str2,
                                            Name = fastCsvReader.ReadString( ),
                                            Board = this.ExchangeInfoProvider.GetOrCreateBoard( str1, null )
                                        };
                                        newSecurity( security );
                                        securityStorage.Save( security, false );
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private sealed class Class105
        {
            public MemoryStream memoryStream_0;
            public SecurityId securityId_0;
            public TimeSpan timeSpan_0;

            internal List< TimeFrameCandleMessage > method_0( )
            {
                XElement root = XDocument.Load( memoryStream_0 ).Root;
                List< TimeFrameCandleMessage > frameCandleMessageList = new List< TimeFrameCandleMessage >( );
                Decimal num1 = new Decimal( );
                int index = -1;
                int int_0_1 = -1;
                int int_0_2 = -1;
                int int_0_3 = -1;
                int int_0_4 = -1;
                int int_0_5 = -1;
                int num2 = -1;
                int int_0_6 = -1;
                int num3 = 0;
                foreach( XElement element in root.XPathSelectElement( "//column-names" ).Elements( ) )
                {
                    //string lowerInvariant = element.Value.ToLowerInvariant( );
                    //switch ( Class24.smethod_0( lowerInvariant ) )
                    //{
                    //    case 667630371:
                    //        if ( lowerInvariant == "close" )
                    //        {
                    //            int_0_4 = num3;
                    //            break;
                    //        }
                    //        break;
                    //    case 786459023:
                    //        if ( lowerInvariant == "volume" )
                    //        {
                    //            int_0_5 = num3;
                    //            break;
                    //        }
                    //        break;
                    //    case 1037941245:
                    //        if ( lowerInvariant == "high" )
                    //        {
                    //            int_0_2 = num3;
                    //            break;
                    //        }
                    //        break;
                    //    case 1113510858:
                    //        if ( lowerInvariant == "value" )
                    //        {
                    //            num2 = num3;
                    //            break;
                    //        }
                    //        break;
                    //    case 1330735745:
                    //        if ( lowerInvariant == "low" )
                    //        {
                    //            int_0_3 = num3;
                    //            break;
                    //        }
                    //        break;
                    //    case 3546203337:
                    //        if ( lowerInvariant == "open" )
                    //        {
                    //            int_0_1 = num3;
                    //            break;
                    //        }
                    //        break;
                    //    case 3564297305:
                    //        if ( lowerInvariant == "date" )
                    //        {
                    //            index = num3;
                    //            break;
                    //        }
                    //        break;
                    //    case 3639427541:
                    //        if ( lowerInvariant == "open interest" )
                    //        {
                    //            int_0_6 = num3;
                    //            break;
                    //        }
                    //        break;
                    //}
                    ++num3;
                }
                if( num2 != -1 )
                {
                    if( int_0_1 == -1 )
                    {
                        int_0_1 = num2;
                    }

                    if( int_0_2 == -1 )
                    {
                        int_0_2 = num2;
                    }

                    if( int_0_3 == -1 )
                    {
                        int_0_3 = num2;
                    }

                    if( int_0_4 == -1 )
                    {
                        int_0_4 = num2;
                    }
                }
                foreach( XContainer xpathSelectElement in root.XPathSelectElements( "//datum[@type='array']" ) )
                {
                    XElement[ ] array = xpathSelectElement.Elements( ).ToArray< XElement >( );
                    DateTimeOffset dto = array[ index ].Value.ToDateTime( "yyyy-MM-dd", null ).ApplyTimeZone( TimeHelper.Est );
                    TimeFrameCandleMessage frameCandleMessage1 = new TimeFrameCandleMessage( );
                    frameCandleMessage1.SecurityId = this.securityId_0;
                    frameCandleMessage1.OpenTime = dto;
                    frameCandleMessage1.CloseTime = dto.EndOfDay( );
                    Decimal? nullable = QuandlHistorySource.smethod_0( array, int_0_1 );
                    frameCandleMessage1.OpenPrice = nullable ?? num1;
                    nullable = QuandlHistorySource.smethod_0( array, int_0_2 );
                    frameCandleMessage1.HighPrice = nullable ?? Decimal.Zero;
                    nullable = QuandlHistorySource.smethod_0( array, int_0_3 );
                    frameCandleMessage1.LowPrice = nullable ?? Decimal.Zero;
                    nullable = QuandlHistorySource.smethod_0( array, int_0_4 );
                    frameCandleMessage1.ClosePrice = nullable ?? Decimal.Zero;
                    nullable = QuandlHistorySource.smethod_0( array, int_0_5 );
                    frameCandleMessage1.TotalVolume = nullable ?? Decimal.Zero;
                    frameCandleMessage1.TimeFrame = this.timeSpan_0;
                    frameCandleMessage1.OpenInterest = QuandlHistorySource.smethod_0( array, int_0_6 );
                    frameCandleMessage1.State = CandleStates.Finished;
                    TimeFrameCandleMessage frameCandleMessage2 = frameCandleMessage1;
                    frameCandleMessageList.Add( frameCandleMessage2 );
                    num1 = frameCandleMessage2.ClosePrice;
                }
                return frameCandleMessageList;
            }
        }
    }
}
