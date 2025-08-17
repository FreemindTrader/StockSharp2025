// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OptionVolatilitySmilePanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.Charting;

#nullable enable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "VolatilitySmile", Description = "VolatilitySmileChart")]
[VectorIcon("Parabola")]
[Doc("topics/terminal/user_interface/components/smile_volatility.html")]
public partial class OptionVolatilitySmilePanel : BaseStudioControl, IComponentConnector
{
    private static readonly Guid _putBidSmileId = Converter.To<Guid>((object)"E969F589-8A8D-4024-BCE2-694E90F5A4EC");
    private static readonly Guid _putAskSmileId = Converter.To<Guid>((object)"1FD8934F-E575-4BAE-895F-BF60E8796631");
    private static readonly Guid _putLastSmileId = Converter.To<Guid>((object)"25533681-6933-476A-8A52-D962A0A64D65");
    private static readonly Guid _callBidSmileId = Converter.To<Guid>((object)"248D8DD8-F261-408E-A02A-BBFB2BEDCDC7");
    private static readonly Guid _callAskSmileId = Converter.To<Guid>((object)"547B3BCC-6CF0-406F-A84F-956C46C8B304");
    private static readonly Guid _callLastSmileId = Converter.To<Guid>((object)"E4541E58-245B-4BDE-9FB9-ADC79B39D74F");
    private readonly
#nullable disable
    OptionDeskModel _model;
    private ICollection<LineData<double>> _putBidSmile;
    private ICollection<LineData<double>> _putAskSmile;
    private ICollection<LineData<double>> _putLastSmile;
    private ICollection<LineData<double>> _callBidSmile;
    private ICollection<LineData<double>> _callAskSmile;
    private ICollection<LineData<double>> _callLastSmile;
    private readonly SyncObject _needRefreshLock = new SyncObject();
    private bool _needRefresh;
    private readonly DispatcherTimer _refreshTimer;
    
    public OptionVolatilitySmilePanel()
    {
        this.InitializeComponent();
        this.FilterPanel.SubscriptionManager = new SubscriptionManager((IStudioControl)this);
        this.Register<ResetedCommand>((object)this, true, (Action<ResetedCommand>)(cmd => this.ClearSmiles()));
        this.Register<EntitiesRemovedCommand<Security>>((object)this, false, (Action<EntitiesRemovedCommand<Security>>)(cmd =>
        {
            Security[] array = this.FilterPanel.RemoveOptions(cmd.Entities).ToArray<Security>();
            if (array.Length == 0)
                return;
            foreach (Security security in array)
                this._model.Remove(security);
            this.MakeDirty();
            this.RaiseChangedCommand();
        }));
        this._model = new OptionDeskModel()
        {
            MarketDataProvider = BaseStudioControl.MarketDataProvider,
            ExchangeInfoProvider = BaseStudioControl.ExchangeInfoProvider
        };
        this.FilterPanel.SecurityProvider = BaseStudioControl.SecurityProvider;
        this.FilterPanel.MarketDataProvider = BaseStudioControl.MarketDataProvider;
        this._putBidSmile = this.SmileChart.CreateSmile("Put (B)", Colors.DarkRed, id: OptionVolatilitySmilePanel._putBidSmileId);
        this._putAskSmile = this.SmileChart.CreateSmile("Put (A)", Colors.Red, id: OptionVolatilitySmilePanel._putAskSmileId);
        this._putLastSmile = this.SmileChart.CreateSmile("Put (L)", Colors.OrangeRed, id: OptionVolatilitySmilePanel._putLastSmileId);
        this._callBidSmile = this.SmileChart.CreateSmile("Call (B)", Colors.GreenYellow, id: OptionVolatilitySmilePanel._callBidSmileId);
        this._callAskSmile = this.SmileChart.CreateSmile("Call (A)", Colors.DarkGreen, id: OptionVolatilitySmilePanel._callAskSmileId);
        this._callLastSmile = this.SmileChart.CreateSmile("Call (L)", Colors.DarkOliveGreen, id: OptionVolatilitySmilePanel._callLastSmileId);
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
            this.RefreshModel();
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

    private static void TryAddSmileItem(
      ICollection<LineData<double>> smile,
      Decimal strike,
      Decimal? iv)
    {
        if (!iv.HasValue)
            return;
        smile.Add(new LineData<double>()
        {
            X = (double)strike,
            Y = iv.Value
        });
    }

    private void ClearSmiles()
    {
        this._putBidSmile.Clear();
        this._putAskSmile.Clear();
        this._putLastSmile.Clear();
        this._callBidSmile.Clear();
        this._callAskSmile.Clear();
        this._callLastSmile.Clear();
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.FilterPanel, storage, "FilterPanel");
        PersistableHelper.LoadIfNotNull((IPersistable)this.SmileChart, storage, "SmileChart");
        KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement> keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>((Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool>)(e => e.Value.Id == OptionVolatilitySmilePanel._putBidSmileId));
        this._putBidSmile = keyValuePair.Key;
        keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>((Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool>)(e => e.Value.Id == OptionVolatilitySmilePanel._putAskSmileId));
        this._putAskSmile = keyValuePair.Key;
        keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>((Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool>)(e => e.Value.Id == OptionVolatilitySmilePanel._putLastSmileId));
        this._putLastSmile = keyValuePair.Key;
        keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>((Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool>)(e => e.Value.Id == OptionVolatilitySmilePanel._callBidSmileId));
        this._callBidSmile = keyValuePair.Key;
        keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>((Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool>)(e => e.Value.Id == OptionVolatilitySmilePanel._callAskSmileId));
        this._callAskSmile = keyValuePair.Key;
        keyValuePair = this.SmileChart.Elements.First<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>>((Func<KeyValuePair<ICollection<LineData<double>>, IChartVolatilitySmileElement>, bool>)(e => e.Value.Id == OptionVolatilitySmilePanel._callLastSmileId));
        this._callLastSmile = keyValuePair.Key;
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("FilterPanel", PersistableHelper.Save((IPersistable)this.FilterPanel));
        storage.SetValue<SettingsStorage>("SmileChart", PersistableHelper.Save((IPersistable)this.SmileChart));
    }

    private void RefreshModel()
    {
        OptionDeskModel model = this._model;
        DateTime? currentDate = this.FilterPanel.CurrentDate;
        DateTimeOffset? currentTime = currentDate.HasValue ? new DateTimeOffset?((DateTimeOffset)currentDate.GetValueOrDefault()) : new DateTimeOffset?();
        Decimal? assetPrice = this.FilterPanel.AssetPrice;
        model.Refresh(currentTime, assetPrice);
        this.ClearSmiles();
        foreach (OptionDeskRow row in this._model.Rows)
        {
            Decimal? strike = row.Strike;
            if (strike.HasValue)
            {
                OptionVolatilitySmilePanel.TryAddSmileItem(this._callBidSmile, strike.Value, (Decimal?)row.Call?.ImpliedVolatilityBestBid);
                OptionVolatilitySmilePanel.TryAddSmileItem(this._callAskSmile, strike.Value, (Decimal?)row.Call?.ImpliedVolatilityBestAsk);
                OptionVolatilitySmilePanel.TryAddSmileItem(this._callLastSmile, strike.Value, (Decimal?)row.Call?.ImpliedVolatilityLastTrade);
                OptionVolatilitySmilePanel.TryAddSmileItem(this._putBidSmile, strike.Value, (Decimal?)row.Put?.ImpliedVolatilityBestBid);
                OptionVolatilitySmilePanel.TryAddSmileItem(this._putAskSmile, strike.Value, (Decimal?)row.Put?.ImpliedVolatilityBestAsk);
                OptionVolatilitySmilePanel.TryAddSmileItem(this._putLastSmile, strike.Value, (Decimal?)row.Put?.ImpliedVolatilityLastTrade);
            }
        }
    }

    protected override void OnActiveLanguageChanged() => this.UpdateTitle();

    private void UpdateTitle()
    {
        this.Title = $"{LocalizedStrings.VolatilitySmile} {((object)this._model?.UnderlyingAsset)?.ToString()}";
    }

    private void FilterPanel_OnUnderlyingAssetChanged()
    {
        this._model.UnderlyingAsset = this.FilterPanel.UnderlyingAsset;
        if (this._model.UnderlyingAsset != null)
            CollectionHelper.ForEach<Security>((IEnumerable<Security>)this.FilterPanel.Options, new Action<Security>(this._model.Add));
        this.UpdateTitle();
        this.RefreshModel();
        this.RaiseChangedCommand();
    }

    private void FilterPanel_OnFilterChanged()
    {
        this.RefreshModel();
        this.RaiseChangedCommand();
    }

    private void FilterPanel_OnUseBlackModelChanged()
    {
        this._model.UseBlackModel = this.FilterPanel.UseBlackModel;
        this.RefreshModel();
        this.RaiseChangedCommand();
    }

    private void FilterPanel_OnOptionsChanged()
    {
        this._model.Clear();
        CollectionHelper.ForEach<Security>((IEnumerable<Security>)this.FilterPanel.Options, new Action<Security>(this._model.Add));
        this.RefreshModel();
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
