// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.StudioConnector
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.Common;
using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Risk;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Testing;
using StockSharp.BusinessEntities;
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
          IPortfolioMessageAdapterProvider portfolioAdapterProvider,
          StorageModes storageMode )
          : base( securityStorage, positionStorage, exchangeInfoProvider, storageRegistry, snapshotRegistry, new StorageBuffer()
          {
              FilterSubscription = true,
              EnabledLevel1 = false,
              EnabledOrderBook = false
          }, false, false )
        {
            StudioUserConfig instance = StudioUserConfig.Instance;
            this.InMessageChannel = ( IMessageChannel ) new PassThroughMessageChannel();
            this.OutMessageChannel = ( IMessageChannel ) new PassThroughMessageChannel();
            this.BasketSecurityProcessorProvider = instance.ProcessorProvider;
            BasketMessageAdapter basketMessageAdapter1 = new BasketMessageAdapter((IdGenerator) new MillisecondIncrementalIdGenerator(), new CandleBuilderProvider(exchangeInfoProvider), securityAdapterProvider, portfolioAdapterProvider, this.Buffer);
            basketMessageAdapter1.NativeIdStorage = nativeIdStorage;
            basketMessageAdapter1.SecurityMappingStorage = securityMappingStorage;
            ( ( BaseLogSource ) basketMessageAdapter1 ).Parent = ( ( ILogSource ) this );
            basketMessageAdapter1.SupportOffline = true;
            basketMessageAdapter1.SupportStorage = false;
            basketMessageAdapter1.StorageSettings.StorageRegistry = storageRegistry;
            basketMessageAdapter1.StorageSettings.Mode = storageMode;
            basketMessageAdapter1.StorageSettings.Format = instance.GetStorageFormat();
            basketMessageAdapter1.StorageSettings.Drive = storageRegistry.DefaultDrive;
            basketMessageAdapter1.Level1Extend = true;
            BasketMessageAdapter basketMessageAdapter2 = basketMessageAdapter1;
            RiskMessageAdapter riskMessageAdapter = new RiskMessageAdapter((IMessageAdapter) basketMessageAdapter2);
            riskMessageAdapter.RiskManager = ServicesRegistry.RiskManager;
            riskMessageAdapter.OwnInnerAdapter = true;
            StorageMetaInfoMessageAdapter infoMessageAdapter = new StorageMetaInfoMessageAdapter((IMessageAdapter) riskMessageAdapter, securityStorage, positionStorage, exchangeInfoProvider, basketMessageAdapter2.StorageProcessor);
            infoMessageAdapter.OwnInnerAdapter = true;
            ChannelMessageAdapter channelMessageAdapter = new ChannelMessageAdapter((IMessageAdapter) new BufferMessageAdapter((IMessageAdapter) infoMessageAdapter, basketMessageAdapter2.StorageSettings, this.Buffer, snapshotRegistry), (IMessageChannel) new InMemoryMessageChannel((IMessageQueue) new MessageByOrderQueue(), "Connector In", new Action<Exception>(RaiseError)), (IMessageChannel) new InMemoryMessageChannel((IMessageQueue) new MessageByOrderQueue(), "Connector Out", new Action<Exception>( RaiseError)));
            channelMessageAdapter.OwnInnerAdapter = true;
            this.InnerAdapter = ( IMessageAdapter ) channelMessageAdapter;
            this.LookupMessagesOnConnect.Clear();
            this.SupportBasketSecurities = true;
            this.UpdatePortfolioByChange = false;
        }

        public new StorageMetaInfoMessageAdapter StorageAdapter
        {
            get
            {
                return this.InnerAdapter.FindAdapter<StorageMetaInfoMessageAdapter>();
            }
        }

        public void UpdateEmulatorSettings( MarketEmulatorSettings settings )
        {
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );
            EmulationMessageAdapter adapter = this.InnerAdapter.FindAdapter<EmulationMessageAdapter>();
            if ( adapter == null )
                return;
            PersistableHelper.Apply<MarketEmulatorSettings>(  adapter.Settings,  settings );
        }
    }
}
