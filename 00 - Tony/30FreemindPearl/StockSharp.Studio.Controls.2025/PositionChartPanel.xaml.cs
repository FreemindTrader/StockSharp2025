// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PositionChartPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

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
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "PositionChartPanel", Name = "PositionChart", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Portfolio" )]
    [Doc( "topics/designer/user_interface/components/positions.html" )]
    public partial class PositionChartPanel : BaseStudioControl
    {
        private readonly SynchronizedDictionary<SecurityId, IChartBandElement> _curves = new SynchronizedDictionary<SecurityId, IChartBandElement>();
        private readonly SubscriptionManager _subscriptionManager;
        private const string CurveIds = "CurveIds";
        
        public PositionChartPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl ) this );
            DateTimeOffset? lastTime = new DateTimeOffset?();
            this.Register<ResetedCommand>(  this, false, ( Action<ResetedCommand> ) ( cmd =>
            {
                lastTime = new DateTimeOffset?();
                this.EquityChart.Reset( ( IEnumerable<IChartBandElement> ) this._curves.Values );
            } ), ( Func<ResetedCommand, bool> ) null );
            this.Register<EntityCommand<Position>>(  this, false, ( Action<EntityCommand<Position>> ) ( cmd =>
            {
                Position entity = cmd.Entity;
                DateTimeOffset time = entity.LastChangeTime;
                SecurityId secId = entity.Security.Id.ToSecurityId((SecurityIdGenerator) null);
                if ( time == new DateTimeOffset() || lastTime.HasValue && time < lastTime.Value || secId.IsMoney() )
                    return;
                Decimal? v = entity.CurrentValue;
                if ( !v.HasValue )
                    return;
                lastTime = new DateTimeOffset?( time );
                ( ( DispatcherObject ) this ).GuiAsync( ( Action ) ( () =>
          {
              IChartDrawData data = this.EquityChart.CreateData();
              IChartBandElement curve;
              if ( !this._curves.TryGetValue( secId, out curve ) )
              {
                  if ( v.Value == Decimal.Zero )
                      return;
                  SynchronizedDictionary<SecurityId, IChartBandElement> curves = this._curves;
                  SecurityId securityId = secId;
                  EquityCurveChart equityChart = this.EquityChart;
                  string title = secId.ToString();
                  Color steelBlue = Colors.SteelBlue;
                  Guid id = new Guid();
                  IChartBandElement chartBandElement = curve = equityChart.CreateCurve(title, steelBlue, DrawStyles.Line, id);
                  curves[securityId]= chartBandElement;
              }
              if ( this.EquityChart.Elements.FirstOrDefault<IChartBandElement>( ( Func<IChartBandElement, bool> ) ( e => e.Id == curve.Id ) ) == null )
                  return;
              data.Group( time ).Add( curve, v.Value );
              this.EquityChart.Draw( data );
          } ) );
            } ), ( Func<EntityCommand<Position>, bool> ) null );
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
            SettingsStorage[] settingsStorageArray = (SettingsStorage[]) storage.GetValue<SettingsStorage[]>("CurveIds",  null);
            SettingsStorage storage1 = (SettingsStorage) storage.GetValue<SettingsStorage>("EquityChart",  null);
            if ( storage1 == null || settingsStorageArray == null )
                return;
            this.EquityChart.Clear();
            this._curves.Clear();
            this.EquityChart.Load( storage1 );
            foreach ( SettingsStorage settingsStorage in settingsStorageArray )
            {
                var m0 = settingsStorage.GetValue<string>("SecurityId",  null);
                SecurityId? nullable = m0 != null ? new SecurityId?(((string) m0).ToSecurityId((SecurityIdGenerator) null)) : new SecurityId?();
                Guid id = (Guid) settingsStorage.GetValue<Guid>("Id",  new Guid());
                if ( nullable.HasValue && !( id == new Guid() ) )
                {
                    IEnumerable<IChartBandElement> elements = this.EquityChart.Elements;
                    IChartBandElement chartBandElement = elements != null ? elements.FirstOrDefault<IChartBandElement>((Func<IChartBandElement, bool>) (e => e.Id == id)) : (IChartBandElement) null;
                    if ( chartBandElement != null )
                        this._curves [ nullable.Value] = chartBandElement;
                }
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "EquityChart",  PersistableHelper.Save( ( IPersistable ) this.EquityChart ) );
            storage.SetValue<SettingsStorage [ ]>( "CurveIds",  ( ( IEnumerable<KeyValuePair<SecurityId, IChartBandElement>> ) this._curves ).Select<KeyValuePair<SecurityId, IChartBandElement>, SettingsStorage>( ( Func<KeyValuePair<SecurityId, IChartBandElement>, SettingsStorage> ) ( kv =>
            {
                SecurityId key;
                IChartBandElement chartBandElement;
                kv.Deconstruct( out key, out chartBandElement );
                SecurityId securityId = key;
                IChartBandElement curve = chartBandElement;
                SettingsStorage settingsStorage = new SettingsStorage();
                if ( this.EquityChart.Elements.FirstOrDefault<IChartBandElement>( ( Func<IChartBandElement, bool> ) ( e => e.Id == curve.Id ) ) == null )
                    return ( SettingsStorage ) null;
                settingsStorage.SetValue<string>( "SecurityId",  securityId.ToString() );
                settingsStorage.SetValue<Guid>( "Id",  curve.Id );
                return settingsStorage;
            } ) ).Where<SettingsStorage>( ( Func<SettingsStorage, bool> ) ( ss => ss != null ) ).ToArray<SettingsStorage>() );
        }        
    }
}
