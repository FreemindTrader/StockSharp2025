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
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            DateTimeOffset? lastTime = new DateTimeOffset?();
            commandService.Register<ResetedCommand>( ( object )this, false, ( Action<ResetedCommand> )( cmd =>
                {
                    lastTime = new DateTimeOffset?();
                    this.EquityChart.Reset( ( IEnumerable<IChartBandElement> )this._curves.Values );
                } ), ( Func<ResetedCommand, bool> )null );
            commandService.Register<EntityCommand<Position>>( ( object )this, false, ( Action<EntityCommand<Position>> )( cmd =>
                {
                    Position entity = cmd.Entity;
                    DateTimeOffset time = entity.LastChangeTime;
                    SecurityId secId = entity.Security.Id.ToSecurityId( ( SecurityIdGenerator )null );
                    if ( time.IsDefault<DateTimeOffset>() || lastTime.HasValue && time < lastTime.Value || secId.IsMoney() )
                        return;
                    Decimal? v = entity.CurrentValue;
                    if ( !v.HasValue )
                        return;
                    lastTime = new DateTimeOffset?( time );
                    ( ( DispatcherObject )this ).GuiAsync( ( Action )( () =>
                {
                    IChartDrawData data = this.EquityChart.CreateData();
                    IChartBandElement curve;
                    if ( !this._curves.TryGetValue( secId, out curve ) )
                    {
                        if ( v.Value == Decimal.Zero )
                            return;
                        SynchronizedDictionary<SecurityId, IChartBandElement> curves = this._curves;
                        SecurityId index = secId;
                        EquityCurveChart equityChart = this.EquityChart;
                        string title = secId.ToString();
                        Color steelBlue = Colors.SteelBlue;
                        Guid id = new Guid();
                        IChartBandElement chartBandElement = curve = equityChart.CreateCurve( title, steelBlue, ChartIndicatorDrawStyles.Line, id );
                        curves[index] = chartBandElement;
                    }
                    if ( this.EquityChart.Elements.FirstOrDefault<IChartBandElement>( ( Func<IChartBandElement, bool> )( e => e.Id == curve.Id ) ) == null )
                        return;
                    data.Group( time ).Add( curve, v.Value );
                    this.EquityChart.Draw( data );
                } ) );
                } ), ( Func<EntityCommand<Position>, bool> )null );
            this.WhenLoaded( ( Action )( () => this._subscriptionManager.CreateSubscription( DataType.PositionChanges, ( Action<Subscription> )null ) ) );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.UnRegister<ResetedCommand>( ( object )this );
            commandService.UnRegister<EntityCommand<Position>>( ( object )this );
            this._subscriptionManager.Dispose();
            base.Dispose();
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage[ ] settingsStorageArray = storage.GetValue<SettingsStorage[ ]>( "CurveIds", ( SettingsStorage[ ] )null );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "EquityChart", ( SettingsStorage )null );
            if ( storage1 == null || settingsStorageArray == null )
                return;
            this.EquityChart.Clear();
            this._curves.Clear();
            this.EquityChart.Load( storage1 );
            foreach ( SettingsStorage settingsStorage in settingsStorageArray )
            {
                string id1 = settingsStorage.GetValue<string>( "SecurityId", ( string )null );
                SecurityId? nullable = id1 != null ? new SecurityId?( id1.ToSecurityId( ( SecurityIdGenerator )null ) ) : new SecurityId?();
                Guid id = settingsStorage.GetValue<Guid>( "Id", new Guid() );
                if ( nullable.HasValue && !id.IsDefault<Guid>() )
                {
                    IEnumerable<IChartBandElement> elements = this.EquityChart.Elements;
                    IChartBandElement chartBandElement = elements != null ? elements.FirstOrDefault<IChartBandElement>( ( Func<IChartBandElement, bool> )( e => e.Id == id ) ) : ( IChartBandElement )null;
                    if ( chartBandElement != null )
                        this._curves[nullable.Value] = chartBandElement;
                }
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "EquityChart", this.EquityChart.Save() );
            storage.SetValue<SettingsStorage[ ]>( "CurveIds", this._curves.Select<KeyValuePair<SecurityId, IChartBandElement>, SettingsStorage>( ( Func<KeyValuePair<SecurityId, IChartBandElement>, SettingsStorage> )( kv =>
                 {
                     SecurityId key;
                     IChartBandElement chartBandElement;
                     kv.Deconstruct( out key, out chartBandElement );
                     SecurityId securityId = key;
                     IChartBandElement curve = chartBandElement;
                     SettingsStorage settingsStorage = new SettingsStorage();
                     if ( this.EquityChart.Elements.FirstOrDefault<IChartBandElement>( ( Func<IChartBandElement, bool> )( e => e.Id == curve.Id ) ) == null )
                         return ( SettingsStorage )null;
                     settingsStorage.SetValue<string>( "SecurityId", securityId.ToString() );
                     settingsStorage.SetValue<Guid>( "Id", curve.Id );
                     return settingsStorage;
                 } ) ).Where<SettingsStorage>( ( Func<SettingsStorage, bool> )( ss => ss != null ) ).ToArray<SettingsStorage>() );
        }        
    }
}
