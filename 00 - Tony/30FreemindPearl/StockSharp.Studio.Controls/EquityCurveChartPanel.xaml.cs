// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.EquityCurveChartPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml.Charting;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "PnL" )]
    [DescriptionLoc( "Str3260", false )]
    [VectorIcon( "Chart1" )]
    [Doc( "topics/Designer_Panel_Market_depth.html" )]
    public partial class EquityCurveChartPanel : BaseStudioControl, IComponentConnector
    {
        private static readonly Guid _totalPnlId = "E969F589-8A8D-4024-BCE2-694E90F5A4EC".To<Guid>();
        private static readonly Guid _unrealizedPnLId = "1FD8934F-E575-4BAE-895F-BF60E8796631".To<Guid>();
        private static readonly Guid _commissionId = "25533681-6933-476A-8A52-D962A0A64D65".To<Guid>();
        private IChartBandElement _totalPnL;
        private IChartBandElement _unrealizedPnL;
        private IChartBandElement _commission;
        private readonly SubscriptionManager _subscriptionManager;
        
        public EquityCurveChartPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            this._totalPnL = this.EquityChart.CreateCurve( LocalizedStrings.PnL, Colors.Green, Colors.Red, ChartIndicatorDrawStyles.Area, EquityCurveChartPanel._totalPnlId );
            this._unrealizedPnL = this.EquityChart.CreateCurve( LocalizedStrings.PnLUnreal, Colors.Black, ChartIndicatorDrawStyles.Line, EquityCurveChartPanel._unrealizedPnLId );
            this._commission = this.EquityChart.CreateCurve( LocalizedStrings.Str159, Colors.Red, ChartIndicatorDrawStyles.DashedLine, EquityCurveChartPanel._commissionId );
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            DateTimeOffset? lastTime = new DateTimeOffset?();
            commandService.Register<ResetedCommand>( ( object )this, false, ( Action<ResetedCommand> )( cmd =>
                {
                    lastTime = new DateTimeOffset?();
                    this.EquityChart.Reset( ( IEnumerable<IChartBandElement> )new IChartBandElement[3]
          {
          this._totalPnL,
          this._unrealizedPnL,
          this._commission
                  } );
                } ), ( Func<ResetedCommand, bool> )null );
            commandService.Register<PnLChangedCommand>( ( object )this, false, ( Action<PnLChangedCommand> )( cmd =>
                {
                    if ( cmd.Time.IsDefault<DateTimeOffset>() || lastTime.HasValue && cmd.Time < lastTime.Value )
                        return;
                    lastTime = new DateTimeOffset?( cmd.Time );
                    IChartDrawData data = this.EquityChart.CreateData();
                    IChartDrawData.IChartDrawDataItem chartDrawDataItem1 = data.Group( cmd.Time );
                    bool flag = false;
                    Decimal? nullable = cmd.TotalPnL;
                    if ( nullable.HasValue )
                    {
                        IChartDrawData.IChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
                        IChartBandElement totalPnL = this._totalPnL;
                        nullable = cmd.TotalPnL;
                        Decimal num = nullable.Value;
                        chartDrawDataItem2.Add( totalPnL, num );
                        flag = true;
                    }
                    nullable = cmd.UnrealizedPnL;
                    if ( nullable.HasValue )
                    {
                        IChartDrawData.IChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
                        IChartBandElement unrealizedPnL = this._unrealizedPnL;
                        nullable = cmd.UnrealizedPnL;
                        Decimal num = nullable.Value;
                        chartDrawDataItem2.Add( unrealizedPnL, num );
                        flag = true;
                    }
                    nullable = cmd.Commission;
                    if ( nullable.HasValue )
                    {
                        IChartDrawData.IChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
                        IChartBandElement commission = this._commission;
                        nullable = cmd.Commission;
                        Decimal num = nullable.Value;
                        chartDrawDataItem2.Add( commission, num );
                        flag = true;
                    }
                    if ( !flag )
                        return;
                    this.EquityChart.Draw( data );
                } ), ( Func<PnLChangedCommand, bool> )null );
            this.WhenLoaded( ( Action )( () => this._subscriptionManager.CreateSubscription( DataType.PositionChanges, ( Action<Subscription> )null ) ) );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.UnRegister<ResetedCommand>( ( object )this );
            commandService.UnRegister<PnLChangedCommand>( ( object )this );
            this._subscriptionManager.Dispose();
            base.Dispose();
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "EquityChart", ( SettingsStorage )null );
            if ( storage1 == null )
                return;
            this.EquityChart.Load( storage1 );
            this._totalPnL = this.EquityChart.Elements.First<IChartBandElement>( ( Func<IChartBandElement, bool> )( e => e.Id == EquityCurveChartPanel._totalPnlId ) );
            this._unrealizedPnL = this.EquityChart.Elements.First<IChartBandElement>( ( Func<IChartBandElement, bool> )( e => e.Id == EquityCurveChartPanel._unrealizedPnLId ) );
            this._commission = this.EquityChart.Elements.First<IChartBandElement>( ( Func<IChartBandElement, bool> )( e => e.Id == EquityCurveChartPanel._commissionId ) );
            if ( this._totalPnL.FullTitle.IsEmpty() )
                this._totalPnL.FullTitle = LocalizedStrings.PnL;
            if ( this._unrealizedPnL.FullTitle.IsEmpty() )
                this._unrealizedPnL.FullTitle = LocalizedStrings.PnLUnreal;
            if ( !this._commission.FullTitle.IsEmpty() )
                return;
            this._commission.FullTitle = LocalizedStrings.Str159;
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "EquityChart", this.EquityChart.Save() );
        }        
    }
}
