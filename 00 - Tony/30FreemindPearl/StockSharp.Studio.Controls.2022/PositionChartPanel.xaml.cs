using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
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
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str3244" )]
    [DescriptionLoc( "Str3245", false )]
    [VectorIcon( "Portfolio" )]
    [Doc( "topics/Designer_Chart_Position.html" )]
    public partial class PositionChartPanel : BaseStudioControl, IComponentConnector
    {
        private readonly SynchronizedDictionary<SecurityId, IChartBandElement> _curves = new SynchronizedDictionary<SecurityId, IChartBandElement>();
        private readonly SubscriptionManager _subscriptionManager;
        private const string CurveIds = "CurveIds";
        
        public PositionChartPanel()
        {
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            IStudioCommandService commandService = CommandService;
            DateTimeOffset? lastTime = new DateTimeOffset?();
            commandService.Register<ResetedCommand>( this, false, cmd =>
                {
                    lastTime = new DateTimeOffset?();
                    EquityChart.Reset( _curves.Values );
                }, null );
            commandService.Register<EntityCommand<Position>>( this, false, cmd =>
                {
                    Position entity = cmd.Entity;
                    DateTimeOffset time = entity.LastChangeTime;
                    SecurityId secId = entity.Security.Id.ToSecurityId( null );
                    if ( time.IsDefault() || lastTime.HasValue && time < lastTime.Value || secId.IsMoney() )
                        return;
                    Decimal? v = entity.CurrentValue;
                    if ( !v.HasValue )
                        return;
                    lastTime = new DateTimeOffset?( time );
                    this.GuiAsync( () =>
             {
                 IChartDrawData data = EquityChart.CreateData();
                 IChartBandElement curve;
                 if ( !_curves.TryGetValue( secId, out curve ) )
                 {
                     if ( v.Value == Decimal.Zero )
                         return;
                     SynchronizedDictionary<SecurityId, IChartBandElement> curves = _curves;
                     SecurityId index = secId;
                     EquityCurveChart equityChart = EquityChart;
                     string title = secId.ToString();
                     Color steelBlue = Colors.SteelBlue;
                     Guid id = new Guid();
                     IChartBandElement chartBandElement = curve = equityChart.CreateCurve( title, steelBlue, ChartIndicatorDrawStyles.Line, id );
                     curves[index] = chartBandElement;
                 }
                 if ( EquityChart.Elements.FirstOrDefault( e => e.Id == curve.Id ) == null )
                     return;
                 data.Group( time ).Add( curve, v.Value );
                 EquityChart.Draw( data );
             } );
                }, null );
            WhenLoaded( () => _subscriptionManager.CreateSubscription( DataType.PositionChanges, null ) );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<ResetedCommand>( this );
            commandService.UnRegister<EntityCommand<Position>>( this );
            _subscriptionManager.Dispose();
            base.Dispose();
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage[ ] settingsStorageArray = storage.GetValue<SettingsStorage[ ]>( "CurveIds", null );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "EquityChart", null );
            if ( storage1 == null || settingsStorageArray == null )
                return;
            EquityChart.Clear();
            _curves.Clear();
            EquityChart.Load( storage1 );
            foreach ( SettingsStorage settingsStorage in settingsStorageArray )
            {
                string id1 = settingsStorage.GetValue<string>( "SecurityId", null );
                SecurityId? nullable = id1 != null ? new SecurityId?( id1.ToSecurityId( null ) ) : new SecurityId?();
                Guid id = settingsStorage.GetValue( "Id", new Guid() );
                if ( nullable.HasValue && !id.IsDefault() )
                {
                    IEnumerable<IChartBandElement> elements = EquityChart.Elements;
                    IChartBandElement chartBandElement = elements != null ? elements.FirstOrDefault( e => e.Id == id ) : null;
                    if ( chartBandElement != null )
                        _curves[nullable.Value] = chartBandElement;
                }
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "EquityChart", EquityChart.Save() );
            storage.SetValue( "CurveIds", _curves.Select( kv =>
                 {
                     SecurityId key;
                     IChartBandElement chartBandElement;
                     kv.Deconstruct( out key, out chartBandElement );
                     SecurityId securityId = key;
                     IChartBandElement curve = chartBandElement;
                     SettingsStorage settingsStorage = new SettingsStorage();
                     if ( EquityChart.Elements.FirstOrDefault( e => e.Id == curve.Id ) == null )
                         return null;
                     settingsStorage.SetValue( "SecurityId", securityId.ToString() );
                     settingsStorage.SetValue( "Id", curve.Id );
                     return settingsStorage;
                 } ).Where( ss => ss != null ).ToArray() );
        }        
    }
}
