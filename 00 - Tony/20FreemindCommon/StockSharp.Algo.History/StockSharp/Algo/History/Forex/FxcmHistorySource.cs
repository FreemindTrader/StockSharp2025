using Ecng.Common;
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
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace StockSharp.Algo.History.Forex
{
    public class FxcmHistorySource : BaseDumpableHistorySource, ISecurityDownloader
    {
        private static readonly DateTime           _from     = new DateTime( 2016, 1, 4 );
        private static readonly FastDateTimeParser _parser   = new FastDateTimeParser( "MM/dd/yyyy HH:mm:ss.fff" );
        private static readonly Encoding           _encoding = Encoding.GetEncoding( 1200 );

        public FxcmHistorySource( INativeIdStorage nativeIdStorage, IExchangeInfoProvider exchangeInfoProvider ) : base( nativeIdStorage, exchangeInfoProvider )
        {
        }

        public override string GetDumpFile(
                                              Security security,
                                              DateTime from,
                                              DateTime to,
                                              Type dataType,
                                              object arg )
        {
            return this.GetDumpFilePath( security.ToSecurityId( null ), from, dataType );
        }

        private string GetDumpFilePath( SecurityId sec, DateTime from, Type dataType )
        {
            if( dataType == null )
            {
                throw new ArgumentNullException( "dataType" );
            }

            if( dataType != typeof( Level1ChangeMessage ) )
            {
                throw new ArgumentOutOfRangeException( "dataType", dataType, LocalizedStrings.Str1655 );
            }

            return Path.Combine( this.DumpFolder, string.Format( "{0}_{1}_{2:yyyy}_{3}.zip", sec.SecurityCode.SecurityIdToFolderName( ), dataType.Name.ToLowerInvariant( ), from, from.GetIso8601WeekOfYear( null ) ) );
        }

        public void Refresh(
                                ISecurityStorage storage,
                                Security security,
                                Action< Security > newSecurityHandler,
                                Func< bool > isCancelled )
        {
            if( storage == null )
            {
                throw new ArgumentNullException( nameof( storage ) );
            }

            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            if( newSecurityHandler == null )
            {
                throw new ArgumentNullException( nameof( newSecurityHandler ) );
            }

            if( isCancelled == null )
            {
                throw new ArgumentNullException( nameof( isCancelled ) );
            }

            string[ ] availableSymbols = new string[ 21 ]
                                                    {
                                                        "AUD/CAD",
                                                        "AUD/CHF",
                                                        "AUD/JPY",
                                                        "AUD/NZD",
                                                        "CAD/CHF",
                                                        "EUR/AUD",
                                                        "EUR/CHF",
                                                        "EUR/GBP",
                                                        "EUR/JPY",
                                                        "EUR/USD",
                                                        "GBP/CHF",
                                                        "GBP/JPY",
                                                        "GBP/NZD",
                                                        "GBP/USD",
                                                        "NZD/CAD",
                                                        "NZD/CHF",
                                                        "NZD/JPY",
                                                        "NZD/USD",
                                                        "USD/CAD",
                                                        "USD/CHF",
                                                        "USD/JPY"
                                                    };
            foreach( string symbol in availableSymbols )
            {
                if( isCancelled( ) )
                {
                    break;
                }

                string id = this.SecurityIdGenerator.GenerateId( symbol, ExchangeBoard.Fxcm );

                if( storage.LookupById( id ) == null )
                {
                    Security sec = new Security( )
                    {
                        Id = id,
                        Code = symbol,
                        PriceStep = new Decimal?( symbol.ContainsIgnoreCase( "JPY" ) ? new Decimal( 1, 0, 0, false, 3 ) : new Decimal( 1, 0, 0, false, 5 ) ),
                        Board = ExchangeBoard.Fxcm,
                        Type = new SecurityTypes?( SecurityTypes.Currency )
                    };

                    storage.Save( sec, false );

                    newSecurityHandler( sec );
                }
            }
        }

        public IEnumerable< MarketDepth > LoadTicks( Security security, DateTime date )
        {
            return this.LoadTickMessages( security.ToSecurityId( ), date ).Select( msg => msg.ToMarketDepth( security ) );
        }

        public IEnumerable< Level1ChangeMessage > LoadTickMessages( SecurityId id, DateTime startDate )
        {
            if( startDate < FxcmHistorySource._from )
            {
                return Enumerable.Empty< Level1ChangeMessage >( );
            }

            byte[ ] secData;

            string filePath = this.CanDump ? this.GetDumpFilePath( id, startDate, typeof( Level1ChangeMessage ) ) : null;

            if( filePath != null && System.IO.File.Exists( filePath ) )
            {
                secData = System.IO.File.ReadAllBytes( filePath );
            }
            else
            {
                string address = string.Format( "https://tickdata.fxcorporate.com/{0}/{1}/{2}.csv.gz", id.SecurityCode.Remove( "/", false ), startDate.Year, startDate.GetIso8601WeekOfYear( ) );

                using( WebClientEx web = new WebClientEx( ) { Timeout = TimeSpan.FromMinutes( 3.0 ) } )
                {
                    try
                    {
                        secData = web.DownloadData( address );
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
                                return Enumerable.Empty< Level1ChangeMessage >( );
                            }

                            throw;
                        }
                    }
                }

                if( filePath != null )
                {
                    filePath.CreateDirIfNotExists( );
                    secData.Save( filePath );
                }
            }

            var output = CultureInfo.InvariantCulture.DoInCulture< List< Level1ChangeMessage > >( ( ) =>
            {
                using( GZipStream gzipStream = new GZipStream( secData.To< Stream >( ), CompressionMode.Decompress ) )
                {
                    var reader = new FastCsvReader( gzipStream, FxcmHistorySource._encoding )
                    {
                        ColumnSeparator = ','
                    };

                    var myList = new List< Level1ChangeMessage >( );

                    if( reader.NextLine( ) )
                    {
                        while( reader.NextLine( ) )
                        {
                            DateTime dt = FxcmHistorySource._parser.Parse( reader.ReadString( ) );

                            var msg = new Level1ChangeMessage( );
                            msg.SecurityId = id;
                            msg.ServerTime = dt.ApplyTimeZone( TimeZoneInfo.Utc );

                            myList.Add( msg.TryAdd( Level1Fields.BestBidPrice, reader.ReadDecimal( ), false ).TryAdd( Level1Fields.BestAskPrice, reader.ReadDecimal( ), false ) );
                        }
                    }
                    return myList;
                }
            } );

            return output;
        }
    }
}
