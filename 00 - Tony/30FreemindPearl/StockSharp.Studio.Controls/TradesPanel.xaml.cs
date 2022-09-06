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
            this.InitializeComponent();
            this.TradesGrid.LayoutChanged += RaiseChangedCommand;
            this.TradesGrid.SelectionChanged += ( GridSelectionChangedEventHandler )( ( s, e ) => this.RaiseSelectedCommand() );
            this.AlertBtn.SchemaChanged += RaiseChangedCommand;
            this.GotFocus += ( RoutedEventHandler )( ( s, e ) => this.RaiseSelectedCommand() );
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.Register<ResetedCommand>( ( object )this, false, ( Action<ResetedCommand> )( cmd => this.TradesGrid.Trades.Clear() ), ( Func<ResetedCommand, bool> )null );
            commandService.Register<EntityCommand<Trade>>( ( object )this, false, new Action<EntityCommand<Trade>>( ( TradesGrid.Trades ).AddEntities<Trade> ), ( Func<EntityCommand<Trade>, bool> )null );
            commandService.Register<FirstInitSecuritiesCommand>( ( object )this, false, ( Action<FirstInitSecuritiesCommand> )( cmd => this.AddSecurities( cmd.Securities.Take<Security>( 2 ), ( IEnumerable<Security> )this.Securities ) ), ( Func<FirstInitSecuritiesCommand, bool> )null );
        }

        public override void Dispose( CloseReason reason )
        {
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.UnRegister<ResetedCommand>( ( object )this );
            commandService.UnRegister<EntityCommand<Trade>>( ( object )this );
            commandService.UnRegister<FirstInitSecuritiesCommand>( ( object )this );
            if ( reason == CloseReason.CloseWindow )
                this.AlertBtn.Dispose();
            base.Dispose( reason );
        }

        private void RaiseSelectedCommand()
        {
            new SelectCommand<Trade>( this.TradesGrid.SelectedTrade, false ).Process( ( object )this, false );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "TradesGrid", this.TradesGrid.Save() );
            storage.SetValue<SettingsStorage>( "AlertBtn", this.AlertBtn.Save() );
            storage.SetValue<string>( "BarManager", this.BarManager.SaveDevExpressControl() );
            this.SaveSubscriptions( storage );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.TradesGrid.Load( storage.GetValue<SettingsStorage>( "TradesGrid", ( SettingsStorage )null ) );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "AlertBtn", ( SettingsStorage )null );
            if ( storage1 != null )
                this.AlertBtn.Load( storage1 );
            string settings = storage.GetValue<string>( "BarManager", ( string )null );
            if ( settings != null )
                this.BarManager.LoadDevExpressControl( settings );
            this.LoadSubscriptions( storage );
        }

        private void Filter_OnClick( object sender, RoutedEventArgs e )
        {
            this.FilterConfig();
        }

        
    }
}
