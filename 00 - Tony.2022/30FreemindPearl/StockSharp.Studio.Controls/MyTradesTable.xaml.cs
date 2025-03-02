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
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            TradesGrid.LayoutChanged += RaiseChangedCommand;
            TradesGrid.SelectionChanged += ( s, e ) => new SelectCommand<MyTrade>( TradesGrid.SelectedTrade, false ).Process( this, false );
            GotFocus += ( s, e ) => new SelectCommand<MyTrade>( TradesGrid.SelectedTrade, false ).Process( this, false );
            HashSet<long> tradeIds = new HashSet<long>();
            IStudioCommandService commandService = CommandService;
            commandService.Register<EntityCommand<MyTrade>>( this, false, command =>
                {
                    if ( tradeIds.Contains( command.Entity.Trade.Id ) )
                        return;
                    tradeIds.Add( command.Entity.Trade.Id );
                    TradesGrid.Trades.TryAddEntities( command );
                }, null );
            commandService.Register<ResetedCommand>( this, false, cmd =>
                {
                    tradeIds.Clear();
                    TradesGrid.Trades.Clear();
                }, null );
            WhenLoaded( () => _subscriptionManager.CreateSubscription( DataType.Transactions, null ) );
        }

        public override void Save( SettingsStorage settings )
        {
            base.Save( settings );
            settings.SetValue( "TradesGrid", TradesGrid.Save() );
        }

        public override void Load( SettingsStorage settings )
        {
            base.Load( settings );
            TradesGrid.Load( settings.GetValue<SettingsStorage>( "TradesGrid", null ) );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<EntityCommand<MyTrade>>( this );
            commandService.UnRegister<ResetedCommand>( this );
            _subscriptionManager.Dispose();
            base.Dispose();
        }        
    }
}
