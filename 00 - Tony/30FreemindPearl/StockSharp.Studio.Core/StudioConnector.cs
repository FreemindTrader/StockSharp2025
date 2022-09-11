// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.StudioConnector
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Common;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Risk;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using System;

namespace StockSharp.Studio.Core
{
    public class StudioConnector : Connector
    {
        public StudioConnector(
          ISecurityStorage securityStorage,
          IPositionStorage positionStorage,
          IStorageRegistry storageRegistry,
          SnapshotRegistry snapshotRegistry,
          INativeIdStorage nativeIdStorage,
          ISecurityMappingStorage securityMappingStorage,
          IExchangeInfoProvider exchangeInfoProvider,
          ISecurityMessageAdapterProvider securityAdapterProvider,
          IPortfolioMessageAdapterProvider portfolioAdapterProvider )
          : base( securityStorage, positionStorage, exchangeInfoProvider, storageRegistry, snapshotRegistry, new StorageBuffer() { FilterSubscription = true }, false, false )
        {
            StudioUserConfig instance = BaseUserConfig<StudioUserConfig>.Instance;
            InMessageChannel = new PassThroughMessageChannel();
            OutMessageChannel = new PassThroughMessageChannel();
            BasketSecurityProcessorProvider = instance.ProcessorProvider;
            BasketMessageAdapter basketMessageAdapter1 = new BasketMessageAdapter( new MillisecondIncrementalIdGenerator(), new CandleBuilderProvider( exchangeInfoProvider ), securityAdapterProvider, portfolioAdapterProvider );
            basketMessageAdapter1.NativeIdStorage = nativeIdStorage;
            basketMessageAdapter1.SecurityMappingStorage = securityMappingStorage;
            basketMessageAdapter1.Parent = this;
            basketMessageAdapter1.SupportOffline = true;
            basketMessageAdapter1.StorageSettings.StorageRegistry = storageRegistry;
            basketMessageAdapter1.StorageSettings.Mode = StorageModes.Incremental | StorageModes.Snapshot;
            basketMessageAdapter1.StorageSettings.Format = instance.GetStorageFormat();
            basketMessageAdapter1.StorageSettings.DaysLoad = instance.GetDaysLoad();
            basketMessageAdapter1.StorageSettings.Drive = storageRegistry.DefaultDrive;
            basketMessageAdapter1.Level1Extend = true;
            BasketMessageAdapter basketMessageAdapter2 = basketMessageAdapter1;
            RiskMessageAdapter riskMessageAdapter = new RiskMessageAdapter( basketMessageAdapter2 );
            riskMessageAdapter.RiskManager = ServicesRegistry.RiskManager;
            riskMessageAdapter.OwnInnerAdapter = true;
            StorageMetaInfoMessageAdapter infoMessageAdapter = new StorageMetaInfoMessageAdapter( riskMessageAdapter, securityStorage, positionStorage, exchangeInfoProvider, basketMessageAdapter2.StorageProcessor );
            infoMessageAdapter.OwnInnerAdapter = true;
            ChannelMessageAdapter channelMessageAdapter = new ChannelMessageAdapter( new BufferMessageAdapter( infoMessageAdapter, basketMessageAdapter2.StorageSettings, Buffer, snapshotRegistry ), new InMemoryMessageChannel( new MessageByOrderQueue(), "Connector In", new Action<Exception>( RaiseError ) ), new InMemoryMessageChannel( new MessageByOrderQueue(), "Connector Out", new Action<Exception>( RaiseError ) ) );
            channelMessageAdapter.OwnInnerAdapter = true;
            InnerAdapter = channelMessageAdapter;
            LookupMessagesOnConnect.Clear();
        }

        public override bool SupportBasketSecurities
        {
            get
            {
                return true;
            }
        }

        public new StorageMetaInfoMessageAdapter StorageAdapter
        {
            get
            {
                return InnerAdapter.FindAdapter<StorageMetaInfoMessageAdapter>();
            }
        }

        public void UpdateEmulatorSettings( MarketEmulatorSettings settings )
        {
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );
            EmulationMessageAdapter adapter = InnerAdapter.FindAdapter<EmulationMessageAdapter>();
            if ( adapter == null )
                return;
            adapter.Settings.Apply( settings );
        }
    }
}
