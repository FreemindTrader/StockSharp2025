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


#nullable disable
namespace StockSharp.Studio.Core;

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
      StorageModes storageMode)
      : base(securityStorage, positionStorage, exchangeInfoProvider, storageRegistry, snapshotRegistry, new StorageBuffer()
      {
          FilterSubscription = true,
          EnabledLevel1 = false,
          EnabledOrderBook = false
      }, false, false)
    {
        StudioUserConfig instance = StudioUserConfig.Instance;
        this.InMessageChannel = (IMessageChannel)new PassThroughMessageChannel();
        this.OutMessageChannel = (IMessageChannel)new PassThroughMessageChannel();
        this.BasketSecurityProcessorProvider = instance.ProcessorProvider;
        BasketMessageAdapter basketMessageAdapter = new BasketMessageAdapter((IdGenerator)new MillisecondIncrementalIdGenerator(), new CandleBuilderProvider(exchangeInfoProvider), securityAdapterProvider, portfolioAdapterProvider, this.Buffer);
        basketMessageAdapter.NativeIdStorage = nativeIdStorage;
        basketMessageAdapter.SecurityMappingStorage = securityMappingStorage;
        ((BaseLogSource)basketMessageAdapter).Parent = (ILogSource)this;
        basketMessageAdapter.SupportOffline = true;
        basketMessageAdapter.SupportStorage = false;
        basketMessageAdapter.StorageSettings.StorageRegistry = storageRegistry;
        basketMessageAdapter.StorageSettings.Mode = storageMode;
        basketMessageAdapter.StorageSettings.Format = instance.GetStorageFormat();
        basketMessageAdapter.StorageSettings.Drive = storageRegistry.DefaultDrive;
        basketMessageAdapter.Level1Extend = true;
        BasketMessageAdapter innerAdapter1 = basketMessageAdapter;
        RiskMessageAdapter innerAdapter2 = new RiskMessageAdapter((IMessageAdapter)innerAdapter1, ServicesRegistry.RiskManager);
        innerAdapter2.OwnInnerAdapter = true;
        StorageMetaInfoMessageAdapter innerAdapter3 = new StorageMetaInfoMessageAdapter((IMessageAdapter)innerAdapter2, securityStorage, positionStorage, exchangeInfoProvider, innerAdapter1.StorageProcessor);
        innerAdapter3.OwnInnerAdapter = true;
        ChannelMessageAdapter channelMessageAdapter = new ChannelMessageAdapter((IMessageAdapter)new BufferMessageAdapter((IMessageAdapter)innerAdapter3, innerAdapter1.StorageSettings, this.Buffer, snapshotRegistry), (IMessageChannel)new InMemoryMessageChannel((IMessageQueue)new MessageByOrderQueue(), "Connector In", new Action<Exception>(RaiseError)), (IMessageChannel)new InMemoryMessageChannel((IMessageQueue)new MessageByOrderQueue(), "Connector Out", new Action<Exception>(RaiseError)));
        ((MessageAdapterWrapper)channelMessageAdapter).OwnInnerAdapter = true;
        this.InnerAdapter = (IMessageAdapter)channelMessageAdapter;
        this.SubscriptionsOnConnect.Clear();
        this.SupportBasketSecurities = true;
        this.UpdatePortfolioByChange = false;
    }

    public new StorageMetaInfoMessageAdapter StorageAdapter
    {
        get => Extensions.FindAdapter<StorageMetaInfoMessageAdapter>(this.InnerAdapter);
    }

    public void UpdateEmulatorSettings(MarketEmulatorSettings settings)
    {
        if (settings == null)
            throw new ArgumentNullException(nameof(settings));
        EmulationMessageAdapter adapter = Extensions.FindAdapter<EmulationMessageAdapter>(this.InnerAdapter);
        if (adapter == null)
            return;
        PersistableHelper.Apply<MarketEmulatorSettings>(adapter.Settings, settings);
    }
}
