// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.CandleChartPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo.Strategies;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "CandleChartPanel", Name = "Chart", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "CandleStick" )]
    [Doc( "topics/designer/user_interface/components/chart.html" )]
    [Guid( "1792B83D-6D34-4D83-B8F3-8668EE8CC4DF" )]
    public partial class CandleChartPanel : BaseStudioControl
    {
        private Strategy _strategy;
        
        public CandleChartPanel()
        {
            this.InitializeComponent();
            this.Register<BindCommand>(  this, true, ( Action<BindCommand> ) ( cmd =>
            {
                if ( !cmd.CheckControl( ( IStudioControl ) this ) )
                    return;
                this.ChartPanel.IsInteracted = cmd.IsInteractive;
                cmd.Binder.Init( ( Action<Strategy> ) ( s =>
          {
              this._strategy = s;
              this._strategy.SetChart( ( IChart ) this.ChartPanel );
          } ), ( Action<Strategy> ) ( s =>
          {
              if ( this._strategy == null || this._strategy != s )
                  return;
              this._strategy.SetChart( ( IChart ) null );
              this._strategy = ( Strategy ) null;
          } ) );
            } ), ( Func<BindCommand, bool> ) null );
            this.ChartPanel.MinimumRange = 200;
            this.WhenLoaded( ( Action ) ( () => new RequestBindSource( ( IStudioControl ) this ).SyncProcess(  this ) ) );
        }

        public override void Dispose( CloseReason reason )
        {
            this.ChartPanel.ClearAreas();
            this._strategy?.SetChart( ( IChart ) null );
            this._strategy = ( Strategy ) null;
            base.Dispose( reason );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.ChartPanel, storage, "ChartPanel" );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "ChartPanel",  PersistableHelper.Save( ( IPersistable ) this.ChartPanel ) );
        }

        
    }
}
