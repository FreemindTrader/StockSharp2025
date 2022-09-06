// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.MyTradesTable
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
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
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Str985" )]
    [DescriptionLoc( "Str3271", false )]
    [VectorIcon( "Deal" )]
    [Doc( "topics/Designer_Trades.html" )]
    public partial class MyTradesTable : BaseStudioControl, IComponentConnector
    {
        private readonly SubscriptionManager _subscriptionManager;
        
        public MyTradesTable()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            this.TradesGrid.LayoutChanged += RaiseChangedCommand;
            this.TradesGrid.SelectionChanged += ( GridSelectionChangedEventHandler )( ( s, e ) => new SelectCommand<MyTrade>( this.TradesGrid.SelectedTrade, false ).Process( ( object )this, false ) );
            this.GotFocus += ( RoutedEventHandler )( ( s, e ) => new SelectCommand<MyTrade>( this.TradesGrid.SelectedTrade, false ).Process( ( object )this, false ) );
            HashSet<long> tradeIds = new HashSet<long>();
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.Register<EntityCommand<MyTrade>>( ( object )this, false, ( Action<EntityCommand<MyTrade>> )( command =>
                {
                    if ( tradeIds.Contains( command.Entity.Trade.Id ) )
                        return;
                    tradeIds.Add( command.Entity.Trade.Id );
                    this.TradesGrid.Trades.TryAddEntities<MyTrade>( command );
                } ), ( Func<EntityCommand<MyTrade>, bool> )null );
            commandService.Register<ResetedCommand>( ( object )this, false, ( Action<ResetedCommand> )( cmd =>
                {
                    tradeIds.Clear();
                    this.TradesGrid.Trades.Clear();
                } ), ( Func<ResetedCommand, bool> )null );
            this.WhenLoaded( ( Action )( () => this._subscriptionManager.CreateSubscription( DataType.Transactions, ( Action<Subscription> )null ) ) );
        }

        public override void Save( SettingsStorage settings )
        {
            base.Save( settings );
            settings.SetValue<SettingsStorage>( "TradesGrid", this.TradesGrid.Save() );
        }

        public override void Load( SettingsStorage settings )
        {
            base.Load( settings );
            this.TradesGrid.Load( settings.GetValue<SettingsStorage>( "TradesGrid", ( SettingsStorage )null ) );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.UnRegister<EntityCommand<MyTrade>>( ( object )this );
            commandService.UnRegister<ResetedCommand>( ( object )this );
            this._subscriptionManager.Dispose();
            base.Dispose();
        }        
    }
}
