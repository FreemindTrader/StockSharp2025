// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.TradesPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "TradesPanel", Name = "TradesFeed", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Deal" )]
    [Doc( "topics/designer/user_interface/components/tick_trades.html" )]
    public partial class TradesPanel : BaseSubscriptionStudioControl, IBarManagerControl
    {
        
        BarManager IBarManagerControl.Bar
        {
            get
            {
                return this.BarManager;
            }
        }

        protected override StockSharp.Messages.DataType DataType { get; } = StockSharp.Messages.DataType.Ticks;

        public TradesPanel()
        {
            this.InitializeComponent();
            this.TryHideBar();
            this.TradesGrid.LayoutChanged += RaiseChangedCommand;
            this.TradesGrid.SelectionChanged += ( GridSelectionChangedEventHandler ) ( ( s, e ) => this.RaiseSelectedCommand() );
            this.GotFocus += ( RoutedEventHandler ) ( ( s, e ) => this.RaiseSelectedCommand() );
            this.Register<ResetedCommand>(  this, false, ( Action<ResetedCommand> ) ( cmd => ( ( ICollection<ITickTradeMessage> ) this.TradesGrid.Trades ).Clear() ), ( Func<ResetedCommand, bool> ) null );
            this.Register<EntityCommand<ITickTradeMessage>>(  this, false, new Action<EntityCommand<ITickTradeMessage>>( ( this.TradesGrid.Trades ).AddEntity<ITickTradeMessage> ), ( Func<EntityCommand<ITickTradeMessage>, bool> ) null );
            this.Register<FirstInitSecuritiesCommand>(  this, false, ( Action<FirstInitSecuritiesCommand> ) ( cmd => this.AddSecurities( cmd.Securities.Take<Security>( 2 ), ( IEnumerable<Security> ) this.Securities ) ), ( Func<FirstInitSecuritiesCommand, bool> ) null );
        }

        public override void Dispose( CloseReason reason )
        {
            this.TradesGrid.LayoutChanged -= RaiseChangedCommand;
            base.Dispose( reason );
        }

        private void RaiseSelectedCommand()
        {
            this.TradesGrid.SelectedTrade.SendSelect<ITickTradeMessage>(  this, false );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "TradesGrid",  PersistableHelper.Save( ( IPersistable ) this.TradesGrid ) );
            storage.SetValue<string>( "BarManager",  this.BarManager.SaveDevExpressControl() );
            this.SaveSubscriptions( storage );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.TradesGrid, storage, "TradesGrid" );
            string settings = (string) storage.GetValue<string>("BarManager",  null);
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
