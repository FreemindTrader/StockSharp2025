// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.BuySellGridPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "BuySellPanel", Name = "BuySell", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Order" )]
    [Doc( "topics/terminal/user_interface/components/buy_sell.html" )]
    public partial class BuySellGridPanel : BaseStudioControl
    {
        private readonly SubscriptionManager _subscriptionManager;
        
        public BuySellGridPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl ) this );
            Portfolio defaultPortfolio = (Portfolio) null;
            this.Register<ResetedCommand>(  this, false, ( Action<ResetedCommand> ) ( cmd => this.UnderlyingGrid.ClearPanels() ), ( Func<ResetedCommand, bool> ) null );
            this.Register<EntitiesRemovedCommand<Security>>(  this, false, ( Action<EntitiesRemovedCommand<Security>> ) ( cmd =>
            {
                bool flag = false;
                foreach ( Security entity in cmd.Entities )
                {
                    Security security = entity;
                    StockSharp.Xaml.BuySellPanel buySellPanel = this.UnderlyingGrid.Panels.FirstOrDefault<StockSharp.Xaml.BuySellPanel>((Func<StockSharp.Xaml.BuySellPanel, bool>) (p => p.Security == security));
                    if ( buySellPanel != null )
                    {
                        buySellPanel.Security = ( Security ) null;
                        flag = true;
                    }
                }
                if ( !flag )
                    return;
                this.RaiseChangedCommand();
            } ), ( Func<EntitiesRemovedCommand<Security>, bool> ) null );
            this.Register<FirstInitSecuritiesCommand>(  this, true, ( Action<FirstInitSecuritiesCommand> ) ( cmd =>
            {
                foreach ( Security security in cmd.Securities.Take<Security>( 4 ) )
                    this.UnderlyingGrid.AddPanel( security ).Portfolio = defaultPortfolio;
            } ), ( Func<FirstInitSecuritiesCommand, bool> ) null );
            this.Register<FirstInitPortfoliosCommand>(  this, true, ( Action<FirstInitPortfoliosCommand> ) ( cmd =>
            {
                Portfolio portfolio = cmd.Portfolios.First<Portfolio>();
                if ( CollectionHelper.IsEmpty<StockSharp.Xaml.BuySellPanel>(  this.UnderlyingGrid.Panels ) )
                    defaultPortfolio = portfolio;
                else
                    CollectionHelper.ForEach<StockSharp.Xaml.BuySellPanel>(  this.UnderlyingGrid.Panels,  ( p => p.Portfolio = portfolio ) );
            } ), ( Func<FirstInitPortfoliosCommand, bool> ) null );
            this.UnderlyingGrid.SecurityProvider = BaseStudioControl.SecurityProvider;
            this.UnderlyingGrid.MarketDataProvider = BaseStudioControl.MarketDataProvider;
            this.UnderlyingGrid.Portfolios = BaseStudioControl.PortfolioDataSource;
            this.UnderlyingGrid.OrderRegistering += new Action<Security, Portfolio, Sides, Decimal, Decimal>( this.UnderlyingGridOnOrderRegistering );
        }

        public override void Dispose( CloseReason reason )
        {
            this.UnderlyingGrid.OrderRegistering -= new Action<Security, Portfolio, Sides, Decimal, Decimal>( this.UnderlyingGridOnOrderRegistering );
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        private void UnderlyingGridOnOrderRegistering(
          Security security,
          Portfolio portfolio,
          Sides side,
          Decimal price,
          Decimal volume )
        {
            new RegisterOrderCommand( new Order()
            {
                Security = security,
                Portfolio = portfolio,
                Side = side,
                Price = price,
                Volume = volume
            } ).Process(  this, false );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "UnderlyingGrid",  PersistableHelper.Save( ( IPersistable ) this.UnderlyingGrid ) );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.UnderlyingGrid, storage, "UnderlyingGrid" );
        }

        private void UnderlyingGrid_OnPanelAdded( StockSharp.Xaml.BuySellPanel panel )
        {
            if ( panel.Security != null )
                this.RequestMarketData( panel.Security );
            panel.SecurityChanged += new Action<Security, Security>( this.PanelOnSecurityChanged );
            panel.PortfolioChanged += new Action<Portfolio, Portfolio>( this.PanelOnPortfolioChanged );
            this.RaiseChangedCommand();
        }

        private void UnderlyingGrid_OnPanelRemoved( StockSharp.Xaml.BuySellPanel panel )
        {
            if ( panel.Security != null )
                this.RefuseMarketData( panel.Security );
            panel.SecurityChanged -= new Action<Security, Security>( this.PanelOnSecurityChanged );
            panel.PortfolioChanged -= new Action<Portfolio, Portfolio>( this.PanelOnPortfolioChanged );
            this.RaiseChangedCommand();
        }

        private void PanelOnSecurityChanged( Security oldSecurity, Security newSecurity )
        {
            if ( oldSecurity != null )
                this.RefuseMarketData( oldSecurity );
            if ( newSecurity != null )
                this.RequestMarketData( newSecurity );
            this.RaiseChangedCommand();
        }

        private void PanelOnPortfolioChanged( Portfolio oldPortfolio, Portfolio newPortfolio )
        {
            this.RaiseChangedCommand();
        }

        private void RequestMarketData( Security security )
        {
            this._subscriptionManager.CreateSubscription( security, StockSharp.Messages.DataType.Level1, ( Action<Subscription> ) null );
            this._subscriptionManager.CreateSubscription( security, StockSharp.Messages.DataType.MarketDepth, ( Action<Subscription> ) null );
        }

        private void RefuseMarketData( Security security )
        {
            this._subscriptionManager.RemoveSubscriptions( security );
        }

        
    }
}
