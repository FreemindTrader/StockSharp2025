// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OrderLogPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
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
using System.Windows;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "OrderLog", Name = "OrderLog", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Order" )]
    [Doc( "topics/terminal/user_interface/components/order_log.html" )]
    public partial class OrderLogPanel : BaseSubscriptionStudioControl, IBarManagerControl
    {        
        BarManager IBarManagerControl.Bar
        {
            get
            {
                return this.BarManager;
            }
        }

        protected override StockSharp.Messages.DataType DataType { get; } = StockSharp.Messages.DataType.OrderLog;

        public OrderLogPanel()
        {
            this.InitializeComponent();
            this.TryHideBar();
            this.OrderLogGrid.LayoutChanged += RaiseChangedCommand;
            this.OrderLogGrid.SelectionChanged += ( GridSelectionChangedEventHandler ) ( ( s, e ) => this.RaiseSelectedCommand() );
            this.GotFocus += ( RoutedEventHandler ) ( ( s, e ) => this.RaiseSelectedCommand() );
            this.Register<ResetedCommand>(  this, false, ( Action<ResetedCommand> ) ( cmd => ( ( ICollection<IOrderLogMessage> ) this.OrderLogGrid.LogItems ).Clear() ), ( Func<ResetedCommand, bool> ) null );
            this.Register<EntityCommand<IOrderLogMessage>>(  this, false, new Action<EntityCommand<IOrderLogMessage>>( ( this.OrderLogGrid.LogItems ).AddEntity<IOrderLogMessage> ), ( Func<EntityCommand<IOrderLogMessage>, bool> ) null );
        }

        private void RaiseSelectedCommand()
        {
            this.OrderLogGrid.SelectedLogItem.SendSelect<IOrderLogMessage>(  this, false );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "OrderLogGrid",  PersistableHelper.Save( ( IPersistable ) this.OrderLogGrid ) );
            storage.SetValue<string>( "BarManager",  this.BarManager.SaveDevExpressControl() );
            this.SaveSubscriptions( storage );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.OrderLogGrid, storage, "OrderLogGrid" );
            string settings = (string) storage.GetValue<string>("BarManager",  null);
            if ( settings != null )
                this.BarManager.LoadDevExpressControl( settings );
            this.LoadSubscriptions( storage );
        }

        public override void Dispose( CloseReason reason )
        {
            this.OrderLogGrid.LayoutChanged -= RaiseChangedCommand;
            base.Dispose( reason );
        }

        private void Filter_OnClick( object sender, RoutedEventArgs e )
        {
            this.FilterConfig();
        }

        
    }
}
