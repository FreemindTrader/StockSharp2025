// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OptionPositionChartPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Threading;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Derivatives;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml.Charting;

#nullable enable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "OptionsPositions", Description = "OptionsPositionsElement")]
[VectorIcon("OptionChart")]
[Doc("topics/terminal/user_interface/components/positions_options.html")]
public partial class OptionPositionChartPanel : BaseStudioControl, IComponentConnector
{
    private readonly
#nullable disable
    SyncObject _needRefreshLock = new SyncObject();
    private bool _needRefresh;
    private readonly DispatcherTimer _refreshTimer;


    public OptionPositionChartPanel()
    {
        this.InitializeComponent();
        this.FilterPanel.SubscriptionManager = new SubscriptionManager((IStudioControl)this);
        this.Register<ResetedCommand>((object)this, true, (Action<ResetedCommand>)(cmd => { }));
        this.Register<EntitiesRemovedCommand<Security>>((object)this, false, (Action<EntitiesRemovedCommand<Security>>)(cmd =>
        {
            if (this.FilterPanel.RemoveOptions(cmd.Entities).ToArray<Security>().Length == 0)
                return;
            this.MakeDirty();
            this.RaiseChangedCommand();
        }));
        this.FilterPanel.SecurityProvider = BaseStudioControl.SecurityProvider;
        this.FilterPanel.MarketDataProvider = BaseStudioControl.MarketDataProvider;
        this._refreshTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(5.0)
        };
        this._refreshTimer.Tick += (EventHandler)((s, e) =>
        {
            lock (this._needRefreshLock)
            {
                if (!this._needRefresh)
                    return;
                this._needRefresh = false;
            }
            this.RefreshChart();
        });
        this._refreshTimer.Start();
    }

    private void MakeDirty()
    {
        lock (this._needRefreshLock)
            this._needRefresh = true;
    }

    public override void Dispose(CloseReason reason)
    {
        this._refreshTimer.Stop();
        this.FilterPanel.Dispose();
        base.Dispose(reason);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("FilterPanel", PersistableHelper.Save((IPersistable)this.FilterPanel));
        storage.SetValue<SettingsStorage>("PosChart", PersistableHelper.Save((IPersistable)this.PosChart));
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.FilterPanel, storage, "FilterPanel");
        PersistableHelper.LoadIfNotNull((IPersistable)this.PosChart, storage, "PosChart");
    }

    private void RefreshChart()
    {
        if (this.FilterPanel.UnderlyingAsset == null)
            return;
        OptionPositionChart posChart = this.PosChart;
        Decimal? assetPrice = this.FilterPanel.AssetPrice;
        DateTime? nullable = this.FilterPanel.CurrentDate;
        DateTimeOffset? currentTime = nullable.HasValue ? new DateTimeOffset?((DateTimeOffset)nullable.GetValueOrDefault()) : new DateTimeOffset?();
        nullable = this.FilterPanel.ExpiryDate;
        DateTimeOffset? expiryDate = nullable.HasValue ? new DateTimeOffset?((DateTimeOffset)nullable.GetValueOrDefault()) : new DateTimeOffset?();
        posChart.Refresh(assetPrice, currentTime, expiryDate);
    }

    protected override void OnActiveLanguageChanged() => this.UpdateTitle();

    private void UpdateTitle()
    {
        this.Title = $"{LocalizedStrings.OptionsPositions} {((object)this.FilterPanel?.UnderlyingAsset)?.ToString()}";
    }

    private void FilterPanel_OnUnderlyingAssetChanged()
    {
        BasketBlackScholes basketBlackScholes = (BasketBlackScholes)null;
        if (this.FilterPanel.UnderlyingAsset != null)
        {
            basketBlackScholes = new BasketBlackScholes(this.FilterPanel.UnderlyingAsset, BaseStudioControl.MarketDataProvider, BaseStudioControl.ExchangeInfoProvider, ServicesRegistry.PositionProvider);
            foreach (Security option in this.FilterPanel.Options)
                basketBlackScholes.InnerModels.Add(this.FilterPanel.UseBlackModel ? (BlackScholes)new Black(option, BaseStudioControl.SecurityProvider, BaseStudioControl.MarketDataProvider, BaseStudioControl.ExchangeInfoProvider) : new BlackScholes(option, BaseStudioControl.SecurityProvider, BaseStudioControl.MarketDataProvider, BaseStudioControl.ExchangeInfoProvider));
        }
        this.PosChart.Model = basketBlackScholes;
        this.UpdateTitle();
        this.RefreshChart();
        this.RaiseChangedCommand();
    }

    private void FilterPanel_OnFilterChanged()
    {
        this.RefreshChart();
        this.RaiseChangedCommand();
    }

    private void FilterPanel_OnUseBlackModelChanged()
    {
        this.PosChart.UseBlackModel = this.FilterPanel.UseBlackModel;
        this.RefreshChart();
        this.RaiseChangedCommand();
    }

    private void FilterPanel_OnOptionsChanged()
    {
        this.PosChart.Model = (BasketBlackScholes)null;
        this.RefreshChart();
        this.RaiseChangedCommand();
    }

    private void FilterPanel_OnSecurityChanged(
      bool isOption,
      Security security,
      IEnumerable<KeyValuePair<Level1Fields, object>> values)
    {
        this.MakeDirty();
    }


}
