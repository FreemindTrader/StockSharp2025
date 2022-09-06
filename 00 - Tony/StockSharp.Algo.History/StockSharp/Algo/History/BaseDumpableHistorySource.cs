using Ecng.Common;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using System;
using System.IO;

namespace StockSharp.Algo.History
{
    public abstract class BaseDumpableHistorySource : BaseHistorySource
    {
        private string _dumpFolder = string.Empty;
        private readonly INativeIdStorage _storage;
        private readonly IExchangeInfoProvider _provider;

        protected BaseDumpableHistorySource( INativeIdStorage nativeIdStorage, IExchangeInfoProvider exchangeInfoProvider )
        {
            INativeIdStorage nativeIdStorage1 = nativeIdStorage;
            if( nativeIdStorage1 == null )
            {
                throw new ArgumentNullException( nameof( nativeIdStorage ) );
            }

            _storage = nativeIdStorage1;
            IExchangeInfoProvider exchangeInfoProvider1 = exchangeInfoProvider;
            if( exchangeInfoProvider1 == null )
            {
                throw new ArgumentNullException( nameof( exchangeInfoProvider ) );
            }

            _provider = exchangeInfoProvider1;
        }

        public INativeIdStorage NativeIdStorage
        {
            get
            {
                return _storage;
            }
        }

        public IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return _provider;
            }
        }

        protected bool CanDump
        {
            get
            {
                return !DumpFolder.IsEmpty( );
            }
        }

        protected TextReader Process( Security security, DateTime from, DateTime to, Type dataType, object arg, Func< string > download )
        {
            if( download == null )
            {
                throw new ArgumentNullException( nameof( download ) );
            }

            string dumpFile = GetDumpFile( security, from, to, dataType, arg );
            if( CanDump && File.Exists( dumpFile ) )
            {
                return new StreamReader( dumpFile );
            }

            string str = download( );
            if( CanDump )
            {
                dumpFile.CreateDirIfNotExists( );
                File.WriteAllText( dumpFile, str );
            }
            return new StringReader( str );
        }

        public string DumpFolder
        {
            get
            {
                return _dumpFolder;
            }
            set
            {
                string str = value;
                if( str == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                _dumpFolder = str;
            }
        }

        public abstract string GetDumpFile( Security security, DateTime from, DateTime to, Type dataType, object arg );

        protected ExchangeBoard GetSecurityBoard( Security security )
        {
            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            if( security.Board != null )
            {
                return security.Board;
            }

            return ExchangeInfoProvider.GetOrCreateBoard( SecurityIdGenerator.Split( security.Id, false ).BoardCode, null );
        }
    }
}
