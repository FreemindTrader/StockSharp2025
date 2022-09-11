// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OrderLogPanel
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
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "OrderLog" )]
    [DescriptionLoc( "OrderLog", false )]
    [VectorIcon( "Order" )]
    [Doc( "topics/Terminal_orderlog.html" )]
    public partial class OrderLogPanel : BaseSubscriptionStudioControl, IComponentConnector
    {        
        protected override DataType DataType { get; } = DataType.OrderLog;

        public OrderLogPanel()
        {
            InitializeComponent();
            OrderLogGrid.LayoutChanged += new Action( RaiseChangedCommand );
            OrderLogGrid.SelectionChanged += ( s, e ) => RaiseSelectedCommand();
            AlertBtn.SchemaChanged += new Action( RaiseChangedCommand );
            GotFocus += ( s, e ) => RaiseSelectedCommand();
            IStudioCommandService commandService = CommandService;
            commandService.Register<ResetedCommand>( this, false, cmd => OrderLogGrid.LogItems.Clear(), null );
            commandService.Register(
                this,
                false,
                new Action<EntityCommand<OrderLogItem>>( ( OrderLogGrid.LogItems ).AddEntities ),
                null );
        }

        private void RaiseSelectedCommand()
        {
            new SelectCommand<OrderLogItem>( OrderLogGrid.SelectedLogItem, false ).Process( this, false );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "OrderLogGrid", OrderLogGrid.Save() );
            storage.SetValue( "AlertBtn", AlertBtn.Save() );
            storage.SetValue( "BarManager", BarManager.SaveDevExpressControl() );
            SaveSubscriptions( storage );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            OrderLogGrid.Load( storage.GetValue<SettingsStorage>( "OrderLogGrid", null ) );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "AlertBtn", null );
            if ( storage1 != null )
                AlertBtn.Load( storage1 );
            string settings = storage.GetValue<string>( "BarManager", null );
            if ( settings != null )
                BarManager.LoadDevExpressControl( settings );
            LoadSubscriptions( storage );
        }

        public override void Dispose( CloseReason reason )
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<ResetedCommand>( this );
            commandService.UnRegister<EntityCommand<OrderLogItem>>( this );
            if ( reason == CloseReason.CloseWindow )
                AlertBtn.Dispose();
            base.Dispose( reason );
        }

        private void Filter_OnClick( object sender, RoutedEventArgs e )
        {
            FilterConfig();
        }        
    }
}
