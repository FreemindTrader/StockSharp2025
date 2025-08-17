// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.EquityCurveChartPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Drawing;
using Ecng.Serialization;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "PnL", Description = "EquityCurveChartPanel")]
[VectorIcon("Chart1")]
[Doc("topics/designer/user_interface/components/pnl_equity.html")]
public partial class EquityCurveChartPanel : BaseStudioControl, IComponentConnector
{
    private static readonly Guid _totalPnlId = Converter.To<Guid>((object)"E969F589-8A8D-4024-BCE2-694E90F5A4EC");
    private static readonly Guid _realizedPnLId = Converter.To<Guid>((object)"21EC0F4D-5C6F-43FA-9AAB-2D3BF6D74A1D");
    private static readonly Guid _unrealizedPnLId = Converter.To<Guid>((object)"1FD8934F-E575-4BAE-895F-BF60E8796631");
    private static readonly Guid _commissionId = Converter.To<Guid>((object)"25533681-6933-476A-8A52-D962A0A64D65");
    private IChartBandElement _totalPnL;
    private IChartBandElement _realizedPnL;
    private IChartBandElement _unrealizedPnL;
    private IChartBandElement _commission;
    private readonly SubscriptionManager _subscriptionManager;

    public EquityCurveChartPanel()
    {
        this.InitializeComponent();
        this._subscriptionManager = new SubscriptionManager((IStudioControl)this);
        this._totalPnL = this.EquityChart.CreateCurve(LocalizedStrings.PnL, Colors.Green, Colors.Red, DrawStyles.Area, EquityCurveChartPanel._totalPnlId);
        this._realizedPnL = this.EquityChart.CreateCurve(LocalizedStrings.PnLRealized, Colors.Black, DrawStyles.Line, EquityCurveChartPanel._realizedPnLId);
        this._unrealizedPnL = this.EquityChart.CreateCurve(LocalizedStrings.PnLUnreal, Colors.DarkGray, DrawStyles.Line, EquityCurveChartPanel._unrealizedPnLId);
        this._commission = this.EquityChart.CreateCurve(LocalizedStrings.Commission, Colors.Red, DrawStyles.DashedLine, EquityCurveChartPanel._commissionId);
        DateTimeOffset? lastTime = new DateTimeOffset?();
        this.Register<ResetedCommand>((object)this, false, (Action<ResetedCommand>)(cmd =>
        {
            lastTime = new DateTimeOffset?();
            this.EquityChart.Reset((IEnumerable<IChartBandElement>)new IChartBandElement[4]
          {
          this._totalPnL,
          this._realizedPnL,
          this._unrealizedPnL,
          this._commission
              });
        }), (Func<ResetedCommand, bool>)null);
        this.Register<PnLChangedCommand>((object)this, false, (Action<PnLChangedCommand>)(cmd =>
        {
            if (cmd.Time == new DateTimeOffset() || lastTime.HasValue && cmd.Time < lastTime.Value)
                return;
            lastTime = new DateTimeOffset?(cmd.Time);
            IChartDrawData data = this.EquityChart.CreateData();
            IChartDrawData.IChartDrawDataItem chartDrawDataItem1 = data.Group(cmd.Time);
            bool flag = false;
            Decimal? nullable1 = cmd.RealizedPnL;
            if (nullable1.HasValue)
            {
                IChartDrawData.IChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
                IChartBandElement totalPnL = this._totalPnL;
                nullable1 = cmd.RealizedPnL;
                Decimal? nullable2 = cmd.Commission;
                Decimal valueOrDefault = nullable2.GetValueOrDefault();
                Decimal? nullable3;
                if (!nullable1.HasValue)
                {
                    nullable2 = new Decimal?();
                    nullable3 = nullable2;
                }
                else
                    nullable3 = new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault);
                // ISSUE: variable of a boxed type
                var local = (ValueType)nullable3;
                chartDrawDataItem2.Add((IChartElement)totalPnL, (object)local);
                IChartDrawData.IChartDrawDataItem chartDrawDataItem3 = chartDrawDataItem1;
                IChartBandElement realizedPnL = this._realizedPnL;
                nullable1 = cmd.RealizedPnL;
                Decimal num = nullable1.Value;
                chartDrawDataItem3.Add(realizedPnL, num);
                flag = true;
            }
            nullable1 = cmd.UnrealizedPnL;
            if (nullable1.HasValue)
            {
                IChartDrawData.IChartDrawDataItem chartDrawDataItem4 = chartDrawDataItem1;
                IChartBandElement unrealizedPnL = this._unrealizedPnL;
                nullable1 = cmd.UnrealizedPnL;
                Decimal num = nullable1.Value;
                chartDrawDataItem4.Add(unrealizedPnL, num);
                flag = true;
            }
            nullable1 = cmd.Commission;
            if (nullable1.HasValue)
            {
                IChartDrawData.IChartDrawDataItem chartDrawDataItem5 = chartDrawDataItem1;
                IChartBandElement commission = this._commission;
                nullable1 = cmd.Commission;
                Decimal num = nullable1.Value;
                chartDrawDataItem5.Add(commission, num);
                flag = true;
            }
            if (!flag)
                return;
            this.EquityChart.Draw(data);
        }));
        this.WhenLoaded((Action)(() => this._subscriptionManager.CreateSubscription(StockSharp.Messages.DataType.PositionChanges)));
    }

    public override void Dispose(CloseReason reason)
    {
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        if (!PersistableHelper.LoadIfNotNull((IPersistable)this.EquityChart, storage, "EquityChart"))
            return;
        this._totalPnL = this.EquityChart.Elements.First<IChartBandElement>((Func<IChartBandElement, bool>)(e => e.Id == EquityCurveChartPanel._totalPnlId));
        this._realizedPnL = this.EquityChart.Elements.First<IChartBandElement>((Func<IChartBandElement, bool>)(e => e.Id == EquityCurveChartPanel._realizedPnLId));
        this._unrealizedPnL = this.EquityChart.Elements.First<IChartBandElement>((Func<IChartBandElement, bool>)(e => e.Id == EquityCurveChartPanel._unrealizedPnLId));
        this._commission = this.EquityChart.Elements.First<IChartBandElement>((Func<IChartBandElement, bool>)(e => e.Id == EquityCurveChartPanel._commissionId));
        if (StringHelper.IsEmpty(this._totalPnL.FullTitle))
            this._totalPnL.FullTitle = LocalizedStrings.PnL;
        if (StringHelper.IsEmpty(this._realizedPnL.FullTitle))
            this._realizedPnL.FullTitle = LocalizedStrings.PnLUnreal;
        if (StringHelper.IsEmpty(this._unrealizedPnL.FullTitle))
            this._unrealizedPnL.FullTitle = LocalizedStrings.PnLUnreal;
        if (!StringHelper.IsEmpty(this._commission.FullTitle))
            return;
        this._commission.FullTitle = LocalizedStrings.Commission;
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("EquityChart", PersistableHelper.Save((IPersistable)this.EquityChart));
    }


}
