// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.MyTradesPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "MyTradesTable", Name = "Trades", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Deal" )]
    [Doc( "topics/designer/user_interface/components/trades.html" )]
    public partial class MyTradesPanel : BaseStudioControl
    {
        private readonly SubscriptionManager _subscriptionManager;
        
        public MyTradesPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl ) this );
            this.TradesGrid.LayoutChanged += RaiseChangedCommand;
            this.TradesGrid.SelectionChanged += ( GridSelectionChangedEventHandler ) ( ( s, e ) => this.TradesGrid.SelectedTrade.SendSelect<MyTrade>(  this, false ) );
            this.GotFocus +=  ( ( s, e ) => this.TradesGrid.SelectedTrade.SendSelect<MyTrade>(  this, false ) );
            HashSet<long> tradeIds = new HashSet<long>();
            this.Register<EntityCommand<MyTrade>>(  this, false, ( Action<EntityCommand<MyTrade>> ) ( command =>
            {
                Trade trade = command.Entity.Trade;
                if ( trade.Id.HasValue )
                {
                    if ( tradeIds.Contains( trade.Id.Value ) )
                        return;
                    tradeIds.Add( trade.Id.Value );
                }
              ( ( IList<MyTrade> ) this.TradesGrid.Trades ).TryAddEntities<MyTrade>( command );
            } ), ( Func<EntityCommand<MyTrade>, bool> ) null );
            this.Register<ResetedCommand>(  this, false, ( Action<ResetedCommand> ) ( cmd =>
            {
                tradeIds.Clear();
                ( ( ICollection<MyTrade> ) this.TradesGrid.Trades ).Clear();
            } ), ( Func<ResetedCommand, bool> ) null );
            this.WhenLoaded( ( Action ) ( () => this._subscriptionManager.CreateSubscription( StockSharp.Messages.DataType.Transactions, ( Action<Subscription> ) null ) ) );
        }

        public override void Save( SettingsStorage settings )
        {
            base.Save( settings );
            settings.SetValue<SettingsStorage>( "TradesGrid",  PersistableHelper.Save( ( IPersistable ) this.TradesGrid ) );
        }

        public override void Load( SettingsStorage settings )
        {
            base.Load( settings );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.TradesGrid, settings, "TradesGrid" );
        }

        public override void Dispose( CloseReason reason )
        {
            this.TradesGrid.LayoutChanged -= RaiseChangedCommand;
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }        
    }
}
