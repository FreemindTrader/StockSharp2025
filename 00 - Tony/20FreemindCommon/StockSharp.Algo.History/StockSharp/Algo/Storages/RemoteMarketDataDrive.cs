using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo.History.Hydra;
using StockSharp.Messages;
using System;
using System.Collections.Generic;

namespace StockSharp.Algo.Storages
{
    public class RemoteMarketDataDrive : BaseMarketDataDrive
    {
        private RemoteStorageClient remoteStorageClient_0;

        public RemoteMarketDataDrive( IExchangeInfoProvider exchangeInfoProvider ) : this( new RemoteStorageClient( exchangeInfoProvider ) )
        {
        }

        public RemoteMarketDataDrive( RemoteStorageClient client )
        {
            this.Client = client;
            this.Client.method_1( this );
        }

        public RemoteStorageClient Client
        {
            get
            {
                return this.remoteStorageClient_0;
            }
            set
            {
                if( value == null )
                {
                    throw new ArgumentNullException( nameof( value ) );
                }

                if( value == this.remoteStorageClient_0 )
                {
                    return;
                }

                this.remoteStorageClient_0?.Dispose( );
                this.remoteStorageClient_0 = value;
            }
        }

        public override string Path
        {
            get
            {
                return this.Client.Address.ToString( );
            }
            set
            {
                this.Client = new RemoteStorageClient( this.remoteStorageClient_0.ExchangeInfoProvider, value.To< Uri >( ), true );
            }
        }

        public override IEnumerable< SecurityId > AvailableSecurities
        {
            get
            {
                return this.Client.AvailableSecurities;
            }
        }

        public override IEnumerable< DataType > GetAvailableDataTypes(
      SecurityId securityId,
      StorageFormats format )
        {
            return this.Client.GetAvailableDataTypes( securityId, format );
        }

        public override IMarketDataStorageDrive GetStorageDrive(
      SecurityId securityId,
      Type dataType,
      object arg,
      StorageFormats format )
        {
            return this.Client.GetRemoteStorage( securityId, dataType, arg, format );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.Client.Credentials.Load( storage.GetValue< SettingsStorage >( "Credentials", ( SettingsStorage ) null ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue< SettingsStorage >( "Credentials", this.Client.Credentials.Save( ) );
        }

        protected override void DisposeManaged( )
        {
            this.remoteStorageClient_0.Dispose( );
            base.DisposeManaged( );
        }
    }
}
