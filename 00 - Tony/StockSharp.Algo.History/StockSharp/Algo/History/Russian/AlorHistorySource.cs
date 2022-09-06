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
using System.Linq;
using xNet;

namespace StockSharp.Algo.History.Russian
{
    public class AlorHistorySource : BaseDumpableHistorySource
    {
        private static readonly TimeSpan[] timeSpan_0 = ( new int[6]
    {
      1,
      10,
      15,
      30,
      60,
      1440
    } ).Select< int, TimeSpan >( new Func< int, TimeSpan >( AlorHistorySource.Class108.class108_0.method_0 ) ).ToArray< TimeSpan >( );
        private readonly string string_1;

        public AlorHistorySource(
      INativeIdStorage nativeIdStorage,
      IExchangeInfoProvider exchangeInfoProvider,
      string address = "http://history.alor.ru" ) : base( nativeIdStorage, exchangeInfoProvider )
        {
            if( address.IsEmpty( ) )
            {
                throw new ArgumentNullException( nameof( address ) );
            }

            this.string_1 = address;
        }

        public string Address
        {
            get
            {
                return this.string_1;
            }
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

            if( !dataType.IsCandleMessage( ) )
            {
                throw new ArgumentOutOfRangeException( nameof( dataType ), dataType, LocalizedStrings.Str1655 );
            }

            return Path.Combine( this.DumpFolder, this.method_0( security ) + "_" + this.GetSecurityCode( security ).SecurityIdToFolderName( ), "{0}_{1}_{2:yyyy_MM_dd}_{3:yyyy_MM_dd}.txt".Put( dataType.Name.Remove( "Message", false ).ToLowerInvariant(), ( object ) TraderHelper.CandleArgToFolderName( arg ), from, to ) );
        }

        public IEnumerable< TimeFrameCandleMessage > GetCandles(
      Security security,
      TimeSpan timeFrame,
      DateTime from,
      DateTime to,
      int count = 10000 )
        {
            AlorHistorySource.Class106 class106 = new AlorHistorySource.Class106( );
            class106.alorHistorySource_0 = this;
            class106.security_0 = security;
            class106.timeSpan_0 = timeFrame;
            class106.dateTime_0 = from;
            class106.dateTime_1 = to;
            class106.int_0 = count;
            TextReader reader = this.Process( class106.security_0, class106.dateTime_0, class106.dateTime_1, typeof( TimeFrameCandleMessage ), class106.timeSpan_0, new Func< string >( class106.method_0 ) );
            using( reader )
            {
                return AlorHistorySource.smethod_0( new FastCsvReader( reader )
                {
                    LineSeparator = "\n",
                    ColumnSeparator = '\t'
                }, class106.security_0.ToSecurityId( this.SecurityIdGenerator ), class106.timeSpan_0 );
            }
        }

        private string method_0( Security security_0 )
        {
            ExchangeBoard securityBoard = this.GetSecurityBoard( security_0 );
            if( securityBoard == ExchangeBoard.Forts )
            {
                SecurityTypes? type = security_0.Type;
                return type.HasValue && type.GetValueOrDefault( ) == SecurityTypes.Option ? "FORTSO" : "FORTSF";
            }
            if( !( securityBoard == ExchangeBoard.Ux ) )
            {
                return "MICEX";
            }

            SecurityTypes? type1 = security_0.Type;
            return type1.HasValue && ( uint ) ( type1.GetValueOrDefault( ) - 1 ) <= 1U ? "UXFO" : "UX";
        }

        private static IEnumerable< TimeFrameCandleMessage > smethod_0(
      FastCsvReader fastCsvReader_0,
      SecurityId securityId_0,
      TimeSpan timeSpan_1 )
        {
            AlorHistorySource.Class107 class107 = new AlorHistorySource.Class107( );
            class107.fastCsvReader_0 = fastCsvReader_0;
            class107.securityId_0 = securityId_0;
            class107.timeSpan_0 = timeSpan_1;
            do
            {
                ;
            }
            while (class107.fastCsvReader_0.NextLine( ) && class107.fastCsvReader_0.CurrentLine.IsEmpty( ));
            if( !class107.fastCsvReader_0.CurrentLine.IsEmpty( ) && class107.fastCsvReader_0.CurrentLine.ContainsIgnoreCase( "error" ) )
            {
                throw new InvalidOperationException( class107.fastCsvReader_0.CurrentLine );
            }

            if( class107.fastCsvReader_0.CurrentLine.IsEmpty( ) )
            {
                return Enumerable.Empty< TimeFrameCandleMessage >( );
            }

            return CultureInfo.InvariantCulture.DoInCulture<List<TimeFrameCandleMessage>>( new Func<List<TimeFrameCandleMessage>>( class107.method_0 ) );
        }

        private sealed class Class106
        {
            public AlorHistorySource alorHistorySource_0;
            public Security security_0;
            public TimeSpan timeSpan_0;
            public DateTime dateTime_0;
            public DateTime dateTime_1;
            public int int_0;

            internal string method_0( )
            {
                Url url = new Url( this.alorHistorySource_0.Address );
                url.QueryString.Append( "board", ( object ) this.alorHistorySource_0.method_0( this.security_0 ) ).Append( "ticker", ( object ) this.alorHistorySource_0.GetSecurityCode( this.security_0 ) ).Append( "period", ( object ) ( int ) this.timeSpan_0.TotalMinutes ).Append( "from", ( object ) this.dateTime_0.ToString( "yyyy-MM-dd HH:mm:ss" ) ).Append( "to", ( object ) this.dateTime_1.ToString( "yyyy-MM-dd HH:mm:ss" ) ).Append( "bars", ( object ) this.int_0 );
                using( HttpRequest httpRequest = new HttpRequest( ) )
                {
                    return httpRequest.Get( ( Uri )url, null ).ToString( );
                }
            }
        }

        private sealed class Class107
        {
            public FastCsvReader fastCsvReader_0;
            public SecurityId securityId_0;
            public TimeSpan timeSpan_0;

            internal List< TimeFrameCandleMessage > method_0( )
            {
                List< TimeFrameCandleMessage > frameCandleMessageList1 = new List< TimeFrameCandleMessage >( );
                do
                {
                    DateTimeOffset dateTimeOffset = this.fastCsvReader_0.ReadDateTime( "yyyy-MM-dd HH:mm:ss" ).ApplyTimeZone( TimeHelper.Moscow );
                    List< TimeFrameCandleMessage > frameCandleMessageList2 = frameCandleMessageList1;
                    TimeFrameCandleMessage frameCandleMessage = new TimeFrameCandleMessage( );
                    frameCandleMessage.SecurityId = this.securityId_0;
                    frameCandleMessage.OpenTime = dateTimeOffset;
                    frameCandleMessage.CloseTime = dateTimeOffset + this.timeSpan_0;
                    frameCandleMessage.TimeFrame = this.timeSpan_0;
                    frameCandleMessage.OpenPrice = this.fastCsvReader_0.ReadDecimal( );
                    frameCandleMessage.HighPrice = this.fastCsvReader_0.ReadDecimal( );
                    frameCandleMessage.LowPrice = this.fastCsvReader_0.ReadDecimal( );
                    frameCandleMessage.ClosePrice = this.fastCsvReader_0.ReadDecimal( );
                    frameCandleMessage.TotalVolume = this.fastCsvReader_0.ReadDecimal( );
                    frameCandleMessage.State = CandleStates.Finished;
                    frameCandleMessageList2.Add( frameCandleMessage );
                }
                while (this.fastCsvReader_0.NextLine( ));
                return frameCandleMessageList1;
            }
        }

        [Serializable]
    private sealed class Class108
    {
        public static readonly AlorHistorySource.Class108 class108_0 = new AlorHistorySource.Class108( );

        internal TimeSpan method_0( int int_0 )
        {
            return TimeSpan.FromMinutes( int_0 );
        }
    }
    }
}
