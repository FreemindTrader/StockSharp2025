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
        private TimeZoneInfo _timeZone = TimeHelper.Moscow;
        private readonly RtsInternalClass _rtsInternalClass;
        private readonly RtsHistorySourceFtp _ftp;
        private string _host;
        private string _userName;
        private string _password;
        private bool _loadEveningSession;

        public RtsHistorySource( INativeIdStorage nativeIdStorage, IExchangeInfoProvider exchangeInfoProvider ) : base( nativeIdStorage, exchangeInfoProvider )
        {
            _rtsInternalClass = new RtsInternalClass( this );
            _ftp = new RtsHistorySourceFtp( this );
            ExchangeBoard = ExchangeBoard.Forts;
            SaveRtsStdTrades = false;
            SaveRtsStdCombinedOnly = false;
            UserName = "anonymous";
            Password = "anonymous";
        }

        public TimeZoneInfo TimeZone
        {
            get
            {
                return _timeZone;
            }
            set
            {
                TimeZoneInfo timeZoneInfo = value;
                if( timeZoneInfo == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                _timeZone = timeZoneInfo;
            }
        }

        public string Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Password
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

        public ExchangeBoard ExchangeBoard
        {
            get
            {
                return _rtsInternalClass.ExchangeBoard( );
            }
            set
            {
                if( value == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                _rtsInternalClass.method_6( value );
                _ftp.method_6( value );
                if( value == ExchangeBoard.Forts )
                {
                    Host = "ftp.moex.com";
                    DbfDirectory = "pub/FORTS/pubstat/";
                    TxtDirectory = "pub/info/stats/history/F";
                    TimeZone = TimeHelper.Moscow;
                }
                else
                {
                    if( !( value == ExchangeBoard.Ux ) )
                    {
                        throw new ArgumentOutOfRangeException( nameof( value ), value, LocalizedStrings.IncorrectTimeZone );
                    }

                    Host = "ftp.ux.ua";
                    DbfDirectory = "pub/info/statforts/";
                    TxtDirectory = string.Empty;
                    TimeZone = UxHistorySource.timeZoneInfo_0;
                }
            }
        }

        public string DbfDirectory
        {
            get
            {
                return _rtsInternalClass.GetDirectory( );
            }
            set
            {
                _rtsInternalClass.SetDirectory( value );
            }
        }

        public string TxtDirectory
        {
            get
            {
                return _ftp.GetDirectory( );
            }
            set
            {
                _ftp.SetDirectory( value );
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
                throw new ArgumentOutOfRangeException( nameof( dataType ), dataType, LocalizedStrings.Str1655 );
            }

            return Path.Combine( DumpFolder, "{0:yyyy_MM_dd}".Put( from ) );
        }

        public bool SaveRtsStdTrades
        {
            get
            {
                return _rtsInternalClass.GetStdTrades( );
            }
            set
            {
                _rtsInternalClass.SetStdTrades( value );
            }
        }

        public bool SaveRtsStdCombinedOnly
        {
            get
            {
                return _rtsInternalClass.GetSaveRtsStdCombinedOnly( );
            }
            set
            {
                _rtsInternalClass.SetSaveRtsStdCombinedOnly( value );
            }
        }

        public bool IsSystemOnly
        {
            get
            {
                return _rtsInternalClass.GetIsSystemOnly( );
            }
            set
            {
                _rtsInternalClass.SetIsSystemOnly( value );
                _ftp.SetIsSystemOnly( value );
            }
        }

        public bool LoadEveningSession
        {
            get
            {
                return _loadEveningSession;
            }
            set
            {
                _loadEveningSession = value;
            }
        }

        public IDictionary< Security, IEnumerable< ExecutionMessage > > LoadTicks( ISecurityStorage storage, DateTime date )
        {
            if ( storage == null )
            {
                throw new ArgumentNullException( nameof( storage ) );
            }

            RtsHistorySource.Class142 class142 = new RtsHistorySource.Class142( );
            class142.rtsHistorySource_0 = this;
            class142.isecurityStorage_0 = storage;
            class142.dateTime_0 = date;
            

            return CultureInfo.InvariantCulture.DoInCulture<Dictionary<Security, IEnumerable<ExecutionMessage>>>( new Func<Dictionary<Security, IEnumerable<ExecutionMessage>>>( class142.method_0 ) );
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
            return keyValuePair_0.Value.Values;
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
                if( !rtsHistorySource_0.DumpFolder.IsEmpty( ) )
                {
                    Directory.CreateDirectory( rtsHistorySource_0.DumpFolder );
                }

                using( System.Net.FtpClient.FtpClient ftpClient_0 = new System.Net.FtpClient.FtpClient( ) )
                {
                    ftpClient_0.Host = rtsHistorySource_0.Host;
                    ftpClient_0.Credentials = new NetworkCredential( rtsHistorySource_0.UserName, rtsHistorySource_0.Password );
                    ftpClient_0.Connect( );
                    RtsHistorySource.smethod_0( source, rtsHistorySource_0._rtsInternalClass.method_13( isecurityStorage_0, ftpClient_0, dateTime_0 ) );
                    RtsHistorySource.smethod_0( source, rtsHistorySource_0._ftp.method_9( isecurityStorage_0, ftpClient_0, dateTime_0, false ) );
                    if( rtsHistorySource_0.LoadEveningSession )
                    {
                        RtsHistorySource.smethod_0( source, rtsHistorySource_0._ftp.method_9( isecurityStorage_0, ftpClient_0, dateTime_0, true ) );
                    }
                }
                return source.ToDictionary< KeyValuePair< Security, Dictionary< long, ExecutionMessage > >, Security, IEnumerable< ExecutionMessage > >( RtsHistorySource.Class141.func_0 ?? ( RtsHistorySource.Class141.func_0 = new Func< KeyValuePair< Security, Dictionary< long, ExecutionMessage > >, Security >( RtsHistorySource.Class141.class141_0.method_0 ) ), RtsHistorySource.Class141.func_1 ?? ( RtsHistorySource.Class141.func_1 = new Func< KeyValuePair< Security, Dictionary< long, ExecutionMessage > >, IEnumerable< ExecutionMessage > >( RtsHistorySource.Class141.class141_0.method_1 ) ) );
            }
        }
    }
}
