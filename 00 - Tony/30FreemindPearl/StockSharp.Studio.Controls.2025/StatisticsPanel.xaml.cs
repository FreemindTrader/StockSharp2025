// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.StatisticsPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo.Statistics;
using StockSharp.Algo.Strategies;
using StockSharp.Localization;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core.Commands;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "StatisticsPanel", Name = "Statistics", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Clipboard2" )]
    [Doc( "topics/designer/user_interface/components/statistics.html" )]
    public partial class StatisticsPanel : BaseStudioControl
    {        
        public StatisticsPanel()
        {
            this.InitializeComponent();
            this.Register<BindCommand>(  this, true, ( Action<BindCommand> ) ( cmd =>
            {
                if ( !cmd.CheckControl( ( IStudioControl ) this ) )
                    return;
                cmd.Binder.Init( ( Action<Strategy> ) ( s => this.StatisticsGrid.StatisticManager = s.StatisticManager ), ( Action<Strategy> ) ( s => this.StatisticsGrid.StatisticManager = ( IStatisticManager ) null ) );
            } ), ( Func<BindCommand, bool> ) null );
            this.Register<ResetedCommand>(  this, true, ( Action<ResetedCommand> ) ( cmd => this.StatisticsGrid.Reset() ), ( Func<ResetedCommand, bool> ) null );
            this.WhenLoaded( ( Action ) ( () => new RequestBindSource( ( IStudioControl ) this ).SyncProcess(  this ) ) );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "StatisticsGrid",  PersistableHelper.Save( ( IPersistable ) this.StatisticsGrid ) );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.StatisticsGrid, storage, "StatisticsGrid" );
        }     
    }
}
