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
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            Portfolio defaultPortfolio = null;
            IStudioCommandService commandService = CommandService;
            commandService.Register<ResetedCommand>( this, false, cmd => UnderlyingGrid.ClearPanels(), null );
            commandService.Register<SecuritiesRemovedCommand>( this, false, cmd =>
                {
                    bool flag = false;
                    foreach ( Security security1 in cmd.Securities )
                    {
                        Security security = security1;
                        Xaml.BuySellPanel buySellPanel = UnderlyingGrid.Panels.FirstOrDefault( p => p.Security == security );
                        if ( buySellPanel != null )
                        {
                            buySellPanel.Security = null;
                            flag = true;
                        }
                    }
                    if ( !flag )
                        return;
                    RaiseChangedCommand();
                }, null );
            commandService.Register<FirstInitSecuritiesCommand>( this, true, cmd =>
                {
                    foreach ( Security security in cmd.Securities.Take( 4 ) )
                        UnderlyingGrid.AddPanel( security ).Portfolio = defaultPortfolio;
                }, null );
            commandService.Register<FirstInitPortfoliosCommand>( this, true, cmd =>
                {
                    Portfolio portfolio = cmd.Portfolios.First();
                    if ( UnderlyingGrid.Panels.IsEmpty() )
                        defaultPortfolio = portfolio;
                    else
                        UnderlyingGrid.Panels.ForEach( p => p.Portfolio = portfolio );
                }, null );
            UnderlyingGrid.SecurityProvider = SecurityProvider;
            UnderlyingGrid.MarketDataProvider = MarketDataProvider;
            UnderlyingGrid.Portfolios = PortfolioDataSource;
            UnderlyingGrid.OrderRegistering += new Action<Security, Portfolio, Sides, Decimal, Decimal>( UnderlyingGridOnOrderRegistering );
        }

        public override void Dispose()
        {
            UnderlyingGrid.OrderRegistering -= new Action<Security, Portfolio, Sides, Decimal, Decimal>( UnderlyingGridOnOrderRegistering );
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<ResetedCommand>( this );
            commandService.UnRegister<SecuritiesRemovedCommand>( this );
            commandService.UnRegister<FirstInitSecuritiesCommand>( this );
            commandService.UnRegister<FirstInitPortfoliosCommand>( this );
            _subscriptionManager.Dispose();
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
            } ).Process( this, false );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "UnderlyingGrid", UnderlyingGrid.Save() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            UnderlyingGrid.Load( storage.GetValue<SettingsStorage>( "UnderlyingGrid", null ) );
        }

        private void UnderlyingGrid_OnPanelAdded( Xaml.BuySellPanel panel )
        {
            if ( panel.Security != null )
                RequestMarketData( panel.Security );
            panel.SecurityChanged += new Action<Security, Security>( PanelOnSecurityChanged );
            panel.PortfolioChanged += new Action<Portfolio, Portfolio>( PanelOnPortfolioChanged );
            RaiseChangedCommand();
        }

        private void UnderlyingGrid_OnPanelRemoved( Xaml.BuySellPanel panel )
        {
            if ( panel.Security != null )
                RefuseMarketData( panel.Security );
            panel.SecurityChanged -= new Action<Security, Security>( PanelOnSecurityChanged );
            panel.PortfolioChanged -= new Action<Portfolio, Portfolio>( PanelOnPortfolioChanged );
            RaiseChangedCommand();
        }

        private void PanelOnSecurityChanged( Security oldSecurity, Security newSecurity )
        {
            if ( oldSecurity != null )
                RefuseMarketData( oldSecurity );
            if ( newSecurity != null )
                RequestMarketData( newSecurity );
            RaiseChangedCommand();
        }

        private void PanelOnPortfolioChanged( Portfolio oldPortfolio, Portfolio newPortfolio )
        {
            RaiseChangedCommand();
        }

        private void RequestMarketData( Security security )
        {
            _subscriptionManager.CreateSubscription( security, DataType.Level1, null );
            _subscriptionManager.CreateSubscription( security, DataType.MarketDepth, null );
        }

        private void RefuseMarketData( Security security )
        {
            _subscriptionManager.RemoveSubscriptions( security );
        }


    }
}
