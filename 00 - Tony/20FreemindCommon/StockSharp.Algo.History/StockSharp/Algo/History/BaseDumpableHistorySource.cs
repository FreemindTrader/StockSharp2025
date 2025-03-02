using Ecng.Common;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using System;
using System.IO;

namespace StockSharp.Algo.History
{
    public abstract class BaseDumpableHistorySource : BaseHistorySource
    {
        private string string_0 = string.Empty;
        private readonly INativeIdStorage inativeIdStorage_0;
        private readonly IExchangeInfoProvider iexchangeInfoProvider_0;

        protected BaseDumpableHistorySource(
      INativeIdStorage nativeIdStorage,
      IExchangeInfoProvider exchangeInfoProvider )
        {
            INativeIdStorage nativeIdStorage1 = nativeIdStorage;
            if( nativeIdStorage1 == null )
            {
                throw new ArgumentNullException( nameof( nativeIdStorage ) );
            }

            this.inativeIdStorage_0 = nativeIdStorage1;
            IExchangeInfoProvider exchangeInfoProvider1 = exchangeInfoProvider;
            if( exchangeInfoProvider1 == null )
            {
                throw new ArgumentNullException( nameof( exchangeInfoProvider ) );
            }

            this.iexchangeInfoProvider_0 = exchangeInfoProvider1;
        }

        public INativeIdStorage NativeIdStorage
        {
            get
            {
                return this.inativeIdStorage_0;
            }
        }

        public IExchangeInfoProvider ExchangeInfoProvider
        {
            get
            {
                return this.iexchangeInfoProvider_0;
            }
        }

        protected bool CanDump
        {
            get
            {
                return !this.DumpFolder.IsEmpty( );
            }
        }

        protected TextReader Process(
      Security security,
      DateTime from,
      DateTime to,
      Type dataType,
      object arg,
      Func< string > download )
        {
            if( download == null )
            {
                throw new ArgumentNullException( nameof( download ) );
            }

            string dumpFile = this.GetDumpFile( security, from, to, dataType, arg );
            if( this.CanDump && File.Exists( dumpFile ) )
            {
                return ( TextReader )new StreamReader( dumpFile );
            }

            string str = download( );
            if( this.CanDump )
            {
                dumpFile.CreateDirIfNotExists( );
                File.WriteAllText( dumpFile, str );
            }
            return ( TextReader ) new StringReader( str );
        }

        public string DumpFolder
        {
            get
            {
                return this.string_0;
            }
            set
            {
                string str = value;
                if( str == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                this.string_0 = str;
            }
        }

        public abstract string GetDumpFile(
      Security security,
      DateTime from,
      DateTime to,
      Type dataType,
      object arg );

        protected ExchangeBoard GetSecurityBoard( Security security )
        {
            if( security == null )
            {
                throw new ArgumentNullException( nameof( security ) );
            }

            if( ( Equatable< ExchangeBoard > ) security.Board != ( ExchangeBoard ) null )
            {
                return security.Board;
            }

            return this.ExchangeInfoProvider.GetOrCreateBoard( this.SecurityIdGenerator.Split( security.Id, false ).BoardCode, ( Func< string, ExchangeBoard > ) null );
        }
    }
}
