// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.TradesPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Alerts;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str3278" )]
    [DescriptionLoc( "Str3279", false )]
    [VectorIcon( "Deal" )]
    [Doc( "topics/Designer_Tape_Trades.html" )]
    public partial class TradesPanel : BaseSubscriptionStudioControl, IComponentConnector
    {
        
        protected override DataType DataType { get; } = DataType.Ticks;

        public TradesPanel()
        {
            InitializeComponent();
            TradesGrid.LayoutChanged += RaiseChangedCommand;
            TradesGrid.SelectionChanged += ( s, e ) => RaiseSelectedCommand();
            AlertBtn.SchemaChanged += RaiseChangedCommand;
            GotFocus += ( s, e ) => RaiseSelectedCommand();
            IStudioCommandService commandService = CommandService;
            commandService.Register<ResetedCommand>( this, false, cmd => TradesGrid.Trades.Clear(), null );
            commandService.Register( this, false, new Action<EntityCommand<Trade>>( ( TradesGrid.Trades ).AddEntities ), null );
            commandService.Register<FirstInitSecuritiesCommand>( this, false, cmd => AddSecurities( cmd.Securities.Take( 2 ), Securities ), null );
        }

        public override void Dispose( CloseReason reason )
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<ResetedCommand>( this );
            commandService.UnRegister<EntityCommand<Trade>>( this );
            commandService.UnRegister<FirstInitSecuritiesCommand>( this );
            if ( reason == CloseReason.CloseWindow )
                AlertBtn.Dispose();
            base.Dispose( reason );
        }

        private void RaiseSelectedCommand()
        {
            new SelectCommand<Trade>( TradesGrid.SelectedTrade, false ).Process( this, false );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "TradesGrid", TradesGrid.Save() );
            storage.SetValue( "AlertBtn", AlertBtn.Save() );
            storage.SetValue( "BarManager", BarManager.SaveDevExpressControl() );
            SaveSubscriptions( storage );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            TradesGrid.Load( storage.GetValue<SettingsStorage>( "TradesGrid", null ) );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "AlertBtn", null );
            if ( storage1 != null )
                AlertBtn.Load( storage1 );
            string settings = storage.GetValue<string>( "BarManager", null );
            if ( settings != null )
                BarManager.LoadDevExpressControl( settings );
            LoadSubscriptions( storage );
        }

        private void Filter_OnClick( object sender, RoutedEventArgs e )
        {
            FilterConfig();
        }

        
    }
}
