// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.EquityCurveChartPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Drawing;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Studio.Core.Commands;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "EquityCurveChartPanel", Name = "PnL", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Chart1" )]
    [Doc( "topics/designer/user_interface/components/pnl_equity.html" )]
    public partial class EquityCurveChartPanel : BaseStudioControl
    {
        private static readonly Guid _totalPnlId = (Guid) Converter.To<Guid>((object) "E969F589-8A8D-4024-BCE2-694E90F5A4EC");
        private static readonly Guid _realizedPnLId = (Guid) Converter.To<Guid>((object) "21EC0F4D-5C6F-43FA-9AAB-2D3BF6D74A1D");
        private static readonly Guid _unrealizedPnLId = (Guid) Converter.To<Guid>((object) "1FD8934F-E575-4BAE-895F-BF60E8796631");
        private static readonly Guid _commissionId = (Guid) Converter.To<Guid>((object) "25533681-6933-476A-8A52-D962A0A64D65");
        private IChartBandElement _totalPnL;
        private IChartBandElement _realizedPnL;
        private IChartBandElement _unrealizedPnL;
        private IChartBandElement _commission;
        private readonly SubscriptionManager _subscriptionManager;
        
        public EquityCurveChartPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl ) this );
            this._totalPnL = this.EquityChart.CreateCurve( LocalizedStrings.PnL, Colors.Green, Colors.Red, DrawStyles.Area, EquityCurveChartPanel._totalPnlId );
            this._realizedPnL = this.EquityChart.CreateCurve( LocalizedStrings.PnLRealized, Colors.Black, DrawStyles.Line, EquityCurveChartPanel._realizedPnLId );
            this._unrealizedPnL = this.EquityChart.CreateCurve( LocalizedStrings.PnLUnreal, Colors.DarkGray, DrawStyles.Line, EquityCurveChartPanel._unrealizedPnLId );
            this._commission = this.EquityChart.CreateCurve( LocalizedStrings.Commission, Colors.Red, DrawStyles.DashedLine, EquityCurveChartPanel._commissionId );
            DateTimeOffset? lastTime = new DateTimeOffset?();
            this.Register<ResetedCommand>(  this, false, ( Action<ResetedCommand> ) ( cmd =>
            {
                lastTime = new DateTimeOffset?();
                this.EquityChart.Reset( ( IEnumerable<IChartBandElement> ) new IChartBandElement [4]
          {
          this._totalPnL,
          this._realizedPnL,
          this._unrealizedPnL,
          this._commission
              } );
            } ), ( Func<ResetedCommand, bool> ) null );
            this.Register<PnLChangedCommand>(  this, false, ( Action<PnLChangedCommand> ) ( cmd =>
            {
                if ( cmd.Time == new DateTimeOffset() || lastTime.HasValue && cmd.Time < lastTime.Value )
                    return;
                lastTime = new DateTimeOffset?( cmd.Time );
                IChartDrawData data = this.EquityChart.CreateData();
                IChartDrawData.IChartDrawDataItem chartDrawDataItem1 = data.Group(cmd.Time);
                bool flag = false;
                Decimal? nullable1 = cmd.RealizedPnL;
                if ( nullable1.HasValue )
                {
                    IChartDrawData.IChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
                    IChartBandElement totalPnL = this._totalPnL;
                    nullable1 = cmd.RealizedPnL;
                    Decimal? nullable2 = cmd.Commission;
                    Decimal valueOrDefault = nullable2.GetValueOrDefault();
                    Decimal? nullable3;
                    if ( !nullable1.HasValue )
                    {
                        nullable2 = new Decimal?();
                        nullable3 = nullable2;
                    }
                    else
                        nullable3 = new Decimal?( nullable1.GetValueOrDefault() - valueOrDefault );
                    // ISSUE: variable of a boxed type
                    var local = (ValueType) nullable3;
                    chartDrawDataItem2.Add( ( IChartElement ) totalPnL,  local );
                    IChartDrawData.IChartDrawDataItem chartDrawDataItem3 = chartDrawDataItem1;
                    IChartBandElement realizedPnL = this._realizedPnL;
                    nullable1 = cmd.RealizedPnL;
                    Decimal num = nullable1.Value;
                    chartDrawDataItem3.Add( realizedPnL, num );
                    flag = true;
                }
                nullable1 = cmd.UnrealizedPnL;
                if ( nullable1.HasValue )
                {
                    IChartDrawData.IChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
                    IChartBandElement unrealizedPnL = this._unrealizedPnL;
                    nullable1 = cmd.UnrealizedPnL;
                    Decimal num = nullable1.Value;
                    chartDrawDataItem2.Add( unrealizedPnL, num );
                    flag = true;
                }
                nullable1 = cmd.Commission;
                if ( nullable1.HasValue )
                {
                    IChartDrawData.IChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
                    IChartBandElement commission = this._commission;
                    nullable1 = cmd.Commission;
                    Decimal num = nullable1.Value;
                    chartDrawDataItem2.Add( commission, num );
                    flag = true;
                }
                if ( !flag )
                    return;
                this.EquityChart.Draw( data );
            } ), ( Func<PnLChangedCommand, bool> ) null );
            this.WhenLoaded( ( Action ) ( () => this._subscriptionManager.CreateSubscription( StockSharp.Messages.DataType.PositionChanges, ( Action<Subscription> ) null ) ) );
        }

        public override void Dispose( CloseReason reason )
        {
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            if ( !PersistableHelper.LoadIfNotNull( ( IPersistable ) this.EquityChart, storage, "EquityChart" ) )
                return;
            this._totalPnL = this.EquityChart.Elements.First<IChartBandElement>( ( Func<IChartBandElement, bool> ) ( e => e.Id == EquityCurveChartPanel._totalPnlId ) );
            this._realizedPnL = this.EquityChart.Elements.First<IChartBandElement>( ( Func<IChartBandElement, bool> ) ( e => e.Id == EquityCurveChartPanel._realizedPnLId ) );
            this._unrealizedPnL = this.EquityChart.Elements.First<IChartBandElement>( ( Func<IChartBandElement, bool> ) ( e => e.Id == EquityCurveChartPanel._unrealizedPnLId ) );
            this._commission = this.EquityChart.Elements.First<IChartBandElement>( ( Func<IChartBandElement, bool> ) ( e => e.Id == EquityCurveChartPanel._commissionId ) );
            if ( StringHelper.IsEmpty( this._totalPnL.FullTitle ) )
                this._totalPnL.FullTitle = LocalizedStrings.PnL;
            if ( StringHelper.IsEmpty( this._realizedPnL.FullTitle ) )
                this._realizedPnL.FullTitle = LocalizedStrings.PnLUnreal;
            if ( StringHelper.IsEmpty( this._unrealizedPnL.FullTitle ) )
                this._unrealizedPnL.FullTitle = LocalizedStrings.PnLUnreal;
            if ( !StringHelper.IsEmpty( this._commission.FullTitle ) )
                return;
            this._commission.FullTitle = LocalizedStrings.Commission;
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "EquityChart",  PersistableHelper.Save( ( IPersistable ) this.EquityChart ) );
        }

        
    }
}
