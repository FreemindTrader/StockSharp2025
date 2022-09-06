using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Serialization;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.Linq;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "BuySell" )]
    [DescriptionLoc( "BuySellPanel", false )]
    [VectorIcon( "Order" )]
    [Doc( "topics/Terminal_Buy_Sell.html" )]
    public partial class BuySellGridPanel : BaseStudioControl, IComponentConnector
    {
        private readonly SubscriptionManager _subscriptionManager;
        

        public BuySellGridPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            Portfolio defaultPortfolio = ( Portfolio )null;
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.Register<ResetedCommand>( ( object )this, false, ( Action<ResetedCommand> )( cmd => this.UnderlyingGrid.ClearPanels() ), ( Func<ResetedCommand, bool> )null );
            commandService.Register<SecuritiesRemovedCommand>( ( object )this, false, ( Action<SecuritiesRemovedCommand> )( cmd =>
                {
                    bool flag = false;
                    foreach ( Security security1 in cmd.Securities )
                    {
                        Security security = security1;
                        StockSharp.Xaml.BuySellPanel buySellPanel = this.UnderlyingGrid.Panels.FirstOrDefault<StockSharp.Xaml.BuySellPanel>( ( Func<StockSharp.Xaml.BuySellPanel, bool> )( p => p.Security == security ) );
                        if ( buySellPanel != null )
                        {
                            buySellPanel.Security = ( Security )null;
                            flag = true;
                        }
                    }
                    if ( !flag )
                        return;
                    this.RaiseChangedCommand();
                } ), ( Func<SecuritiesRemovedCommand, bool> )null );
            commandService.Register<FirstInitSecuritiesCommand>( ( object )this, true, ( Action<FirstInitSecuritiesCommand> )( cmd =>
                {
                    foreach ( Security security in cmd.Securities.Take<Security>( 4 ) )
                        this.UnderlyingGrid.AddPanel( security ).Portfolio = defaultPortfolio;
                } ), ( Func<FirstInitSecuritiesCommand, bool> )null );
            commandService.Register<FirstInitPortfoliosCommand>( ( object )this, true, ( Action<FirstInitPortfoliosCommand> )( cmd =>
                {
                    Portfolio portfolio = cmd.Portfolios.First<Portfolio>();
                    if ( this.UnderlyingGrid.Panels.IsEmpty<StockSharp.Xaml.BuySellPanel>() )
                        defaultPortfolio = portfolio;
                    else
                        this.UnderlyingGrid.Panels.ForEach<StockSharp.Xaml.BuySellPanel>( ( Action<StockSharp.Xaml.BuySellPanel> )( p => p.Portfolio = portfolio ) );
                } ), ( Func<FirstInitPortfoliosCommand, bool> )null );
            this.UnderlyingGrid.SecurityProvider = BaseStudioControl.SecurityProvider;
            this.UnderlyingGrid.MarketDataProvider = BaseStudioControl.MarketDataProvider;
            this.UnderlyingGrid.Portfolios = BaseStudioControl.PortfolioDataSource;
            this.UnderlyingGrid.OrderRegistering += new Action<Security, Portfolio, Sides, Decimal, Decimal>( this.UnderlyingGridOnOrderRegistering );
        }

        public override void Dispose()
        {
            this.UnderlyingGrid.OrderRegistering -= new Action<Security, Portfolio, Sides, Decimal, Decimal>( this.UnderlyingGridOnOrderRegistering );
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.UnRegister<ResetedCommand>( ( object )this );
            commandService.UnRegister<SecuritiesRemovedCommand>( ( object )this );
            commandService.UnRegister<FirstInitSecuritiesCommand>( ( object )this );
            commandService.UnRegister<FirstInitPortfoliosCommand>( ( object )this );
            this._subscriptionManager.Dispose();
            base.Dispose();
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
                Direction = side,
                Price = price,
                Volume = volume
            } ).Process( ( object )this, false );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "UnderlyingGrid", this.UnderlyingGrid.Save() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.UnderlyingGrid.Load( storage.GetValue<SettingsStorage>( "UnderlyingGrid", ( SettingsStorage )null ) );
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
            this._subscriptionManager.CreateSubscription( security, DataType.Level1, ( Action<Subscription> )null );
            this._subscriptionManager.CreateSubscription( security, DataType.MarketDepth, ( Action<Subscription> )null );
        }

        private void RefuseMarketData( Security security )
        {
            this._subscriptionManager.RemoveSubscriptions( security );
        }


    }
}
