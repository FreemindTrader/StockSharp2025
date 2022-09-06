using Ecng.Collections;
using Ecng.Common;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace StockSharp.Algo.History
{
    public class GoogleHistorySource : BaseHistorySource
    {
        private static readonly CachedSynchronizedDictionary< TimeSpan, string > cachedSynchronizedDictionary_0;

        static GoogleHistorySource( )
        {
            CachedSynchronizedDictionary< TimeSpan, string > synchronizedDictionary = new CachedSynchronizedDictionary< TimeSpan, string >( );
            synchronizedDictionary.Add( TimeSpan.FromDays( 1.0 ), "daily" );
            synchronizedDictionary.Add( TimeSpan.FromDays( 7.0 ), "weekly" );
            GoogleHistorySource.cachedSynchronizedDictionary_0 = synchronizedDictionary;
        }

        public static IEnumerable< TimeSpan > TimeFrames
        {
            get
            {
                return cachedSynchronizedDictionary_0.CachedKeys;
            }
        }

        public IEnumerable< TimeFrameCandleMessage > GetCandles(
      Security security,
      TimeSpan timeFrame,
      DateTime beginDate,
      DateTime endDate )
        {
            GoogleHistorySource.Class65 class65 = new GoogleHistorySource.Class65( )
      {
        googleHistorySource_0 = this,
        security_0 = security,
        dateTime_0 = beginDate,
        dateTime_1 = endDate,
        timeSpan_0 = timeFrame
      };
            class65.string_0 = GoogleHistorySource.cachedSynchronizedDictionary_0.TryGetValue< TimeSpan, string >( class65.timeSpan_0 );
            if( class65.string_0 == null )
            {
                throw new ArgumentOutOfRangeException( nameof( timeFrame ), class65.timeSpan_0, LocalizedStrings.Str2079 );
            }

            class65.securityId_0 = class65.security_0.ToSecurityId( this.SecurityIdGenerator );
            return CultureInfo.InvariantCulture.DoInCulture<List<TimeFrameCandleMessage>>( new Func<List<TimeFrameCandleMessage>>( class65.method_0 ) );
        }

        private static Decimal smethod_0( string string_0 )
        {
            if( !( string_0 == "-" ) )
            {
                return string_0.To< Decimal >( );
            }

            return Decimal.Zero;
        }

        [Serializable]
    private sealed class Class64
    {
        public static readonly GoogleHistorySource.Class64 class64_0 = new GoogleHistorySource.Class64( );
        public static Comparison< TimeFrameCandleMessage > comparison_0;

        internal int method_0(
        TimeFrameCandleMessage timeFrameCandleMessage_0,
        TimeFrameCandleMessage timeFrameCandleMessage_1 )
        {
            return timeFrameCandleMessage_0.OpenTime.CompareTo( timeFrameCandleMessage_1.OpenTime );
        }
    }

        private sealed class Class65
        {
            public GoogleHistorySource googleHistorySource_0;
            public Security security_0;
            public DateTime dateTime_0;
            public DateTime dateTime_1;
            public string string_0;
            public TimeSpan timeSpan_0;
            public SecurityId securityId_0;

            internal List< TimeFrameCandleMessage > method_0( )
            {
                List< TimeFrameCandleMessage > frameCandleMessageList1 = new List< TimeFrameCandleMessage >( );
                using( WebClient webClient = new WebClient( )
        {
          Encoding = Encoding.ASCII
        } )
                {
                    foreach( string str in webClient.DownloadString( "http://www.google.com/finance/historical?q={0}&startdate={1:MMM dd, yyyy}&enddate={2:MMM dd, yyyy}&histperiod={3}&output=csv".Put( this.googleHistorySource_0.GetSecurityCode( this.security_0 ).Remove( "/", false ), dateTime_0, dateTime_1, string_0 ) ).Split( "\n", true ).Skip< string >( 1 ) )
                    {
                        string[] strArray = str.Split( ',' );
                        DateTimeOffset dateTimeOffset = strArray[ 0 ].To< DateTime >( ).ApplyTimeZone( TimeHelper.Est );
                        Decimal num1 = GoogleHistorySource.smethod_0( strArray[ 1 ] );
                        Decimal num2 = GoogleHistorySource.smethod_0( strArray[ 2 ] );
                        Decimal num3 = GoogleHistorySource.smethod_0( strArray[ 3 ] );
                        Decimal num4 = GoogleHistorySource.smethod_0( strArray[ 4 ] );
                        Decimal num5 = GoogleHistorySource.smethod_0( strArray[ 5 ] );
                        if( !( num1 == Decimal.Zero ) || !( num2 == Decimal.Zero ) || !( num3 == Decimal.Zero ) )
                        {
                            List< TimeFrameCandleMessage > frameCandleMessageList2 = frameCandleMessageList1;
                            TimeFrameCandleMessage frameCandleMessage = new TimeFrameCandleMessage( );
                            frameCandleMessage.OpenPrice = num1;
                            frameCandleMessage.HighPrice = num2;
                            frameCandleMessage.LowPrice = num3;
                            frameCandleMessage.ClosePrice = num4;
                            frameCandleMessage.TimeFrame = this.timeSpan_0;
                            frameCandleMessage.OpenTime = dateTimeOffset;
                            frameCandleMessage.CloseTime = dateTimeOffset + this.timeSpan_0;
                            frameCandleMessage.TotalVolume = num5;
                            frameCandleMessage.SecurityId = this.securityId_0;
                            frameCandleMessage.State = CandleStates.Finished;
                            frameCandleMessageList2.Add( frameCandleMessage );
                        }
                    }
                }
                frameCandleMessageList1.Sort( GoogleHistorySource.Class64.comparison_0 ?? ( GoogleHistorySource.Class64.comparison_0 = new Comparison< TimeFrameCandleMessage >( GoogleHistorySource.Class64.class64_0.method_0 ) ) );
                return frameCandleMessageList1;
            }
        }
    }
}
