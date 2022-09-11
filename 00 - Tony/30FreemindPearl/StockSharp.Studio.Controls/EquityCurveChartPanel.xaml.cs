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
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            _totalPnL = EquityChart.CreateCurve( LocalizedStrings.PnL, Colors.Green, Colors.Red, ChartIndicatorDrawStyles.Area, _totalPnlId );
            _unrealizedPnL = EquityChart.CreateCurve( LocalizedStrings.PnLUnreal, Colors.Black, ChartIndicatorDrawStyles.Line, _unrealizedPnLId );
            _commission = EquityChart.CreateCurve( LocalizedStrings.Str159, Colors.Red, ChartIndicatorDrawStyles.DashedLine, _commissionId );
            IStudioCommandService commandService = CommandService;
            DateTimeOffset? lastTime = new DateTimeOffset?();
            commandService.Register<ResetedCommand>( this, false, cmd =>
                {
                    lastTime = new DateTimeOffset?();
                    EquityChart.Reset( new IChartBandElement[3]
          {
          _totalPnL,
          _unrealizedPnL,
          _commission
                  } );
                }, null );
            commandService.Register<PnLChangedCommand>( this, false, cmd =>
                {
                    if ( cmd.Time.IsDefault() || lastTime.HasValue && cmd.Time < lastTime.Value )
                        return;
                    lastTime = new DateTimeOffset?( cmd.Time );
                    IChartDrawData data = EquityChart.CreateData();
                    IChartDrawData.IChartDrawDataItem chartDrawDataItem1 = data.Group( cmd.Time );
                    bool flag = false;
                    Decimal? nullable = cmd.TotalPnL;
                    if ( nullable.HasValue )
                    {
                        IChartDrawData.IChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
                        IChartBandElement totalPnL = _totalPnL;
                        nullable = cmd.TotalPnL;
                        Decimal num = nullable.Value;
                        chartDrawDataItem2.Add( totalPnL, num );
                        flag = true;
                    }
                    nullable = cmd.UnrealizedPnL;
                    if ( nullable.HasValue )
                    {
                        IChartDrawData.IChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
                        IChartBandElement unrealizedPnL = _unrealizedPnL;
                        nullable = cmd.UnrealizedPnL;
                        Decimal num = nullable.Value;
                        chartDrawDataItem2.Add( unrealizedPnL, num );
                        flag = true;
                    }
                    nullable = cmd.Commission;
                    if ( nullable.HasValue )
                    {
                        IChartDrawData.IChartDrawDataItem chartDrawDataItem2 = chartDrawDataItem1;
                        IChartBandElement commission = _commission;
                        nullable = cmd.Commission;
                        Decimal num = nullable.Value;
                        chartDrawDataItem2.Add( commission, num );
                        flag = true;
                    }
                    if ( !flag )
                        return;
                    EquityChart.Draw( data );
                }, null );
            WhenLoaded( () => _subscriptionManager.CreateSubscription( DataType.PositionChanges, null ) );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<ResetedCommand>( this );
            commandService.UnRegister<PnLChangedCommand>( this );
            _subscriptionManager.Dispose();
            base.Dispose();
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "EquityChart", null );
            if ( storage1 == null )
                return;
            EquityChart.Load( storage1 );
            _totalPnL = EquityChart.Elements.First( e => e.Id == _totalPnlId );
            _unrealizedPnL = EquityChart.Elements.First( e => e.Id == _unrealizedPnLId );
            _commission = EquityChart.Elements.First( e => e.Id == _commissionId );
            if ( _totalPnL.FullTitle.IsEmpty() )
                _totalPnL.FullTitle = LocalizedStrings.PnL;
            if ( _unrealizedPnL.FullTitle.IsEmpty() )
                _unrealizedPnL.FullTitle = LocalizedStrings.PnLUnreal;
            if ( !_commission.FullTitle.IsEmpty() )
                return;
            _commission.FullTitle = LocalizedStrings.Str159;
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "EquityChart", EquityChart.Save() );
        }        
    }
}
