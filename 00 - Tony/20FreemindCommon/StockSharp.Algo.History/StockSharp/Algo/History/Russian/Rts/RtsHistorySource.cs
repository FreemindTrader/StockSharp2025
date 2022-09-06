using Ecng.Collections;
using Ecng.Common;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace StockSharp.Algo.History.Russian.Rts
{
    public class RtsHistorySource : BaseDumpableHistorySource
    {
        public static readonly DateTime RtsMinAvailableTime = new DateTime( 2003, 1, 4 );
        public static readonly DateTime UxMinAvailableTime = new DateTime( 2010, 5, 27 );
        private TimeZoneInfo timeZoneInfo_0 = TimeHelper.Moscow;
        private readonly Class22 class22_0;
        private readonly Class23 class23_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private bool bool_0;

        public RtsHistorySource(
      INativeIdStorage nativeIdStorage,
      IExchangeInfoProvider exchangeInfoProvider ) : base( nativeIdStorage, exchangeInfoProvider )
        {
            this.class22_0 = new Class22( this );
            this.class23_0 = new Class23( this );
            this.ExchangeBoard = ExchangeBoard.Forts;
            this.SaveRtsStdTrades = false;
            this.SaveRtsStdCombinedOnly = false;
            this.UserName = "anonymous";
            this.Password = "anonymous";
        }

        public TimeZoneInfo TimeZone
        {
            get
            {
                return this.timeZoneInfo_0;
            }
            set
            {
                TimeZoneInfo timeZoneInfo = value;
                if( timeZoneInfo == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                this.timeZoneInfo_0 = timeZoneInfo;
            }
        }

        public string Host
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

        public string UserName
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public string Password
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public ExchangeBoard ExchangeBoard
        {
            get
            {
                return this.class22_0.method_5( );
            }
            set
            {
                if( ( Equatable< ExchangeBoard > ) value == ( ExchangeBoard ) null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                this.class22_0.method_6( value );
                this.class23_0.method_6( value );
                if( ( Equatable< ExchangeBoard > ) value == ExchangeBoard.Forts )
                {
                    this.Host = "ftp.moex.com";
                    this.DbfDirectory = "pub/FORTS/pubstat/";
                    this.TxtDirectory = "pub/info/stats/history/F";
                    this.TimeZone = TimeHelper.Moscow;
                }
                else
                {
                    if( !( ( Equatable< ExchangeBoard > ) value == ExchangeBoard.Ux ) )
                    {
                        throw new ArgumentOutOfRangeException( nameof( value ), ( object )value, LocalizedStrings.IncorrectTimeZone );
                    }

                    this.Host = "ftp.ux.ua";
                    this.DbfDirectory = "pub/info/statforts/";
                    this.TxtDirectory = string.Empty;
                    this.TimeZone = UxHistorySource.timeZoneInfo_0;
                }
            }
        }

        public string DbfDirectory
        {
            get
            {
                return this.class22_0.method_3( );
            }
            set
            {
                this.class22_0.method_4( value );
            }
        }

        public string TxtDirectory
        {
            get
            {
                return this.class23_0.method_3( );
            }
            set
            {
                this.class23_0.method_4( value );
            }
        }

        public override string GetDumpFile(
      Security security,
      DateTime from,
      DateTime to,
      Type dataType,
      object arg )
        {
            if( dataType != typeof( ExecutionMessage ) )
            {
                throw new ArgumentOutOfRangeException( nameof( dataType ), ( object )dataType, LocalizedStrings.Str1655 );
            }

            return Path.Combine( this.DumpFolder, "{0:yyyy_MM_dd}".Put( ( object ) from ) );
        }

        public bool SaveRtsStdTrades
        {
            get
            {
                return this.class22_0.method_9( );
            }
            set
            {
                this.class22_0.method_10( value );
            }
        }

        public bool SaveRtsStdCombinedOnly
        {
            get
            {
                return this.class22_0.method_11( );
            }
            set
            {
                this.class22_0.method_12( value );
            }
        }

        public bool IsSystemOnly
        {
            get
            {
                return this.class22_0.method_7( );
            }
            set
            {
                this.class22_0.method_8( value );
                this.class23_0.method_8( value );
            }
        }

        public bool LoadEveningSession
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public IDictionary< Security, IEnumerable< ExecutionMessage > > LoadTicks(
      ISecurityStorage storage,
      DateTime date )
        {
            RtsHistorySource.Class142 class142 = new RtsHistorySource.Class142( );
            class142.rtsHistorySource_0 = this;
            class142.isecurityStorage_0 = storage;
            class142.dateTime_0 = date;
            if( class142.isecurityStorage_0 == null )
            {
                throw new ArgumentNullException( nameof( storage ) );
            }

            return ( IDictionary< Security, IEnumerable< ExecutionMessage > > ) CultureInfo.InvariantCulture.DoInCulture< Dictionary< Security, IEnumerable< ExecutionMessage > > >( new Func< Dictionary< Security, IEnumerable< ExecutionMessage > > >( class142.method_0 ) );
        }

        private static void smethod_0(
      IDictionary< Security, Dictionary< long, ExecutionMessage > > idictionary_0,
      IDictionary< Security, List< ExecutionMessage > > idictionary_1 )
        {
            foreach( KeyValuePair< Security, List< ExecutionMessage > > keyValuePair in ( IEnumerable< KeyValuePair< Security, List< ExecutionMessage > > > ) idictionary_1 )
            {
                foreach( ExecutionMessage executionMessage in keyValuePair.Value )
                {
                    idictionary_0.SafeAdd< Security, Dictionary< long, ExecutionMessage > >( keyValuePair.Key ).TryAdd< long, ExecutionMessage >( executionMessage.TradeId.Value, executionMessage );
                }
            }
        }

        [Serializable]
    private sealed class Class141
    {
        public static readonly RtsHistorySource.Class141 class141_0 = new RtsHistorySource.Class141( );
        public static Func< KeyValuePair< Security, Dictionary< long, ExecutionMessage > >, Security > func_0;
        public static Func< KeyValuePair< Security, Dictionary< long, ExecutionMessage > >, IEnumerable< ExecutionMessage > > func_1;

        internal Security method_0(
        KeyValuePair< Security, Dictionary< long, ExecutionMessage > > keyValuePair_0 )
        {
            return keyValuePair_0.Key;
        }

        internal IEnumerable< ExecutionMessage > method_1(
        KeyValuePair< Security, Dictionary< long, ExecutionMessage > > keyValuePair_0 )
        {
            return ( IEnumerable< ExecutionMessage > ) keyValuePair_0.Value.Values;
        }
    }

        private sealed class Class142
        {
            public RtsHistorySource rtsHistorySource_0;
            public ISecurityStorage isecurityStorage_0;
            public DateTime dateTime_0;

            internal Dictionary< Security, IEnumerable< ExecutionMessage > > method_0( )
            {
                Dictionary< Security, Dictionary< long, ExecutionMessage > > source = new Dictionary< Security, Dictionary< long, ExecutionMessage > >( );
                if( !this.rtsHistorySource_0.DumpFolder.IsEmpty( ) )
                {
                    Directory.CreateDirectory( this.rtsHistorySource_0.DumpFolder );
                }

                using( System.Net.FtpClient.FtpClient ftpClient_0 = new System.Net.FtpClient.FtpClient( ) )
                {
                    ftpClient_0.Host = this.rtsHistorySource_0.Host;
                    ftpClient_0.Credentials = new NetworkCredential( this.rtsHistorySource_0.UserName, this.rtsHistorySource_0.Password );
                    ftpClient_0.Connect( );
                    RtsHistorySource.smethod_0( ( IDictionary< Security, Dictionary< long, ExecutionMessage > > ) source, this.rtsHistorySource_0.class22_0.method_13( this.isecurityStorage_0, ftpClient_0, this.dateTime_0 ) );
                    RtsHistorySource.smethod_0( ( IDictionary< Security, Dictionary< long, ExecutionMessage > > ) source, this.rtsHistorySource_0.class23_0.method_9( this.isecurityStorage_0, ftpClient_0, this.dateTime_0, false ) );
                    if( this.rtsHistorySource_0.LoadEveningSession )
                    {
                        RtsHistorySource.smethod_0( ( IDictionary< Security, Dictionary< long, ExecutionMessage > > )source, this.rtsHistorySource_0.class23_0.method_9( this.isecurityStorage_0, ftpClient_0, this.dateTime_0, true ) );
                    }
                }
                return source.ToDictionary< KeyValuePair< Security, Dictionary< long, ExecutionMessage > >, Security, IEnumerable< ExecutionMessage > >( RtsHistorySource.Class141.func_0 ?? ( RtsHistorySource.Class141.func_0 = new Func< KeyValuePair< Security, Dictionary< long, ExecutionMessage > >, Security >( RtsHistorySource.Class141.class141_0.method_0 ) ), RtsHistorySource.Class141.func_1 ?? ( RtsHistorySource.Class141.func_1 = new Func< KeyValuePair< Security, Dictionary< long, ExecutionMessage > >, IEnumerable< ExecutionMessage > >( RtsHistorySource.Class141.class141_0.method_1 ) ) );
            }
        }
    }
}
