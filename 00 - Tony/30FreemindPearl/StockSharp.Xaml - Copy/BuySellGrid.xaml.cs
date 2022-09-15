using DevExpress.Xpf.Bars;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class BuySellGrid : UserControl, IPersistable
    {
        private ISecurityProvider isecurityProvider_0;
        private PortfolioDataSource portfolioDataSource_0;
        private IMarketDataProvider imarketDataProvider_0;
        
        public BuySellGrid( )
        {
            this.InitializeComponent( );
        }

        public ISecurityProvider SecurityProvider
        {
            get
            {
                return this.isecurityProvider_0;
            }
            set
            {
                this.isecurityProvider_0 = value;
            }
        }

        public PortfolioDataSource Portfolios
        {
            get
            {
                return this.portfolioDataSource_0;
            }
            set
            {
                this.portfolioDataSource_0 = value;
            }
        }

        public IMarketDataProvider MarketDataProvider
        {
            get
            {
                return this.imarketDataProvider_0;
            }
            set
            {
                this.imarketDataProvider_0 = value;
            }
        }

        public event Action<Security, Portfolio, Sides, Decimal, Decimal> OrderRegistering;

        public event Action<BuySellPanel> PanelAdded;

        public event Action<BuySellPanel> PanelRemoved;

        public IEnumerable<BuySellPanel> Panels
        {
            get
            {
                return this.UnderlyingGrid.Children.OfType<BuySellPanel>( );
            }
        }

        public void ClearPanels( )
        {
            this.UnderlyingGrid.Children.Clear( );
        }

        private void method_0( BuySellPanel buySellPanel_0 )
        {
            if ( buySellPanel_0 == null )
            {
                throw new ArgumentNullException( "panel" );
            }

            buySellPanel_0.SecurityProvider = this.SecurityProvider;
            buySellPanel_0.Portfolios = this.Portfolios;
            buySellPanel_0.MarketDataProvider = this.MarketDataProvider;
            buySellPanel_0.OrderRegistering += new Action<Security, Portfolio, Sides, Decimal, Decimal>( this.method_2 );
            buySellPanel_0.Closed += new Action<BuySellPanel>( this.method_1 );
            this.UnderlyingGrid.Children.Add( ( UIElement )buySellPanel_0 );
        }

        private void method_1( BuySellPanel buySellPanel_0 )
        {
            buySellPanel_0.OrderRegistering -= new Action<Security, Portfolio, Sides, Decimal, Decimal>( this.method_2 );
            buySellPanel_0.Closed -= new Action<BuySellPanel>( this.method_1 );
            this.UnderlyingGrid.Children.Remove( ( UIElement )buySellPanel_0 );
            Action<BuySellPanel> action2 = this.PanelRemoved;
            if ( action2 == null )
            {
                return;
            }

            action2( buySellPanel_0 );
        }

        private void method_2(
          Security security_0,
          Portfolio portfolio_0,
          Sides sides_0,
          Decimal decimal_0,
          Decimal decimal_1 )
        {
            Action<Security, Portfolio, Sides, Decimal, Decimal> action0 = this.OrderRegistering;
            if ( action0 == null )
            {
                return;
            }

            action0( security_0, portfolio_0, sides_0, decimal_0, decimal_1 );
        }

        private void method_3( object sender, ItemClickEventArgs e )
        {
            SecurityPickerWindow wnd = new SecurityPickerWindow( )
            {
                SecurityProvider = this.SecurityProvider
            };
            if ( !wnd.ShowModal( ( DependencyObject )this ) )
            {
                return;
            }

            foreach ( Security selectedSecurity in ( IEnumerable<Security> )wnd.SelectedSecurities )
            {
                BuySellPanel buySellPanel_0 = new BuySellPanel( );
                this.method_0( buySellPanel_0 );
                buySellPanel_0.Security = selectedSecurity;
                if ( this.Portfolios.ItemsourceCount( ) == 1 )
                {
                    buySellPanel_0.Portfolio = ( Portfolio )this.Portfolios.Items.First<PortfolioPropertyChangeHandler>( );
                }

                Action<BuySellPanel> action1 = this.PanelAdded;
                if ( action1 != null )
                {
                    action1( buySellPanel_0 );
                }
            }
        }

        public void Load( SettingsStorage storage )
        {
            this.ClearPanels( );
            foreach ( SettingsStorage storage1 in storage.GetValue<SettingsStorage[ ]>( "UnderlyingGrid", ( SettingsStorage[ ] )null ) )
            {
                BuySellPanel buySellPanel_0 = new BuySellPanel( );
                this.method_0( buySellPanel_0 );
                buySellPanel_0.Load( storage1 );
            }
            Action<BuySellPanel> action1 = this.PanelAdded;
            if ( action1 == null )
            {
                return;
            }

            foreach ( BuySellPanel panel in this.Panels )
            {
                action1( panel );
            }
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue<SettingsStorage[ ]>( "UnderlyingGrid", this.Panels.Select<BuySellPanel, SettingsStorage>( BuySellGrid.Class416.func_0 ?? ( BuySellGrid.Class416.func_0 = new Func<BuySellPanel, SettingsStorage>( BuySellGrid.Class416.class416_0.method_0 ) ) ).ToArray<SettingsStorage>( ) );
        }

        

        [Serializable]
        private sealed class Class416
        {
            public static readonly BuySellGrid.Class416 class416_0 = new BuySellGrid.Class416( );
            public static Func<BuySellPanel, SettingsStorage> func_0;

            internal SettingsStorage method_0( BuySellPanel buySellPanel_0 )
            {
                return buySellPanel_0.Save( );
            }
        }
    }
}
