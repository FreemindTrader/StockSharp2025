// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PositionChartPanel
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
using Ecng.ComponentModel;
using Ecng.Drawing;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml.Charting;
using DataType = StockSharp.Messages.DataType;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "PositionChart", Description = "PositionChartPanel")]
[VectorIcon("Portfolio")]
[Doc("topics/designer/user_interface/components/positions.html")]
public partial class PositionChartPanel : BaseStudioControl, IComponentConnector
{
    private readonly SynchronizedDictionary<SecurityId, IChartBandElement> _curves = new SynchronizedDictionary<SecurityId, IChartBandElement>();
    private readonly SubscriptionManager _subscriptionManager;
    private const string CurveIds = "CurveIds";
    
    public PositionChartPanel()
    {
        this.InitializeComponent();
        this._subscriptionManager = new SubscriptionManager((IStudioControl)this);
        DateTimeOffset? lastTime = new DateTimeOffset?();
        this.Register<ResetedCommand>((object)this, false, (Action<ResetedCommand>)(cmd =>
        {
            lastTime = new DateTimeOffset?();
            this.EquityChart.Reset((IEnumerable<IChartBandElement>)this._curves.Values);
        }));
        this.Register<EntityCommand<Position>>((object)this, false, (Action<EntityCommand<Position>>)(cmd =>
        {
            Position entity = cmd.Entity;
            DateTimeOffset time = entity.LastChangeTime;
            SecurityId secId = entity.Security.Id.ToSecurityId((SecurityIdGenerator)null);
            if (time == new DateTimeOffset() || lastTime.HasValue && time < lastTime.Value || secId.IsMoney())
                return;
            Decimal? v = entity.CurrentValue;
            if (!v.HasValue)
                return;
            lastTime = new DateTimeOffset?(time);
            ((DispatcherObject)this).GuiAsync((Action)(() =>
        {
            IChartDrawData data = this.EquityChart.CreateData();
            IChartBandElement curve;
            if (!this._curves.TryGetValue(secId, out curve))
            {
                if (v.Value == 0M)
                    return;
                SynchronizedDictionary<SecurityId, IChartBandElement> curves = this._curves;
                SecurityId securityId = secId;
                EquityCurveChart equityChart = this.EquityChart;
                string title = secId.ToString();
                Color steelBlue = Colors.SteelBlue;
                Guid id = new Guid();
                IChartBandElement chartBandElement = curve = equityChart.CreateCurve(title, steelBlue, DrawStyles.Line, id);
                curves[securityId] = chartBandElement;
            }
            if (this.EquityChart.Elements.FirstOrDefault<IChartBandElement>((Func<IChartBandElement, bool>)(e => e.Id == curve.Id)) == null)
                return;
            data.Group(time).Add(curve, v.Value);
            this.EquityChart.Draw(data);
        }));
        }));
        this.WhenLoaded((Action)(() => this._subscriptionManager.CreateSubscription(DataType.PositionChanges)));
    }

    public override void Dispose(CloseReason reason)
    {
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        SettingsStorage[] settingsStorageArray = storage.GetValue<SettingsStorage[]>("CurveIds", (SettingsStorage[])null);
        SettingsStorage storage1 = storage.GetValue<SettingsStorage>("EquityChart", (SettingsStorage)null);
        if (storage1 == null || settingsStorageArray == null)
            return;
        this.EquityChart.Clear();
        this._curves.Clear();
        this.EquityChart.Load(storage1);
        foreach (SettingsStorage settingsStorage in settingsStorageArray)
        {
            string str = settingsStorage.GetValue<string>("SecurityId", (string)null);
            
            SecurityId? nullable = str != null ? new SecurityId?(str.ToSecurityId( (SecurityIdGenerator)null)) : new SecurityId?();
            Guid id = settingsStorage.GetValue<Guid>("Id", new Guid());
            if (nullable.HasValue && !(id == new Guid()))
            {
                IEnumerable<IChartBandElement> elements = this.EquityChart.Elements;
                IChartBandElement chartBandElement = elements != null ? elements.FirstOrDefault<IChartBandElement>((Func<IChartBandElement, bool>)(e => e.Id == id)) : (IChartBandElement)null;
                if (chartBandElement != null)
                    this._curves[nullable.Value] = chartBandElement;
            }
        }
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("EquityChart", PersistableHelper.Save((IPersistable)this.EquityChart));
        storage.SetValue<SettingsStorage[]>("CurveIds", CollectionHelper.WhereNotNull<SettingsStorage>(((IEnumerable<KeyValuePair<SecurityId, IChartBandElement>>)this._curves).Select<KeyValuePair<SecurityId, IChartBandElement>, SettingsStorage>((Func<KeyValuePair<SecurityId, IChartBandElement>, SettingsStorage>)(kv =>
        {
            (SecurityId key2, IChartBandElement chartBandElement2) = kv;
            SettingsStorage settingsStorage = new SettingsStorage();
            if (this.EquityChart.Elements.FirstOrDefault<IChartBandElement>((Func<IChartBandElement, bool>)(e => e.Id == chartBandElement2.Id)) == null)
                return (SettingsStorage)null;
            settingsStorage.SetValue<string>("SecurityId", key2.ToString());
            settingsStorage.SetValue<Guid>("Id", chartBandElement2.Id);
            return settingsStorage;
        }))).ToArray<SettingsStorage>());
    }

    
}
