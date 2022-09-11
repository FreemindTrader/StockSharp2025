using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace StockSharp.Studio.Controls
{
    public partial class BuySellPanel : BaseStudioControl, IComponentConnector
    {
        private readonly HashSet<Window> _windows = new HashSet<Window>();
        

        public BuySellSettings Settings { get; } = new BuySellSettings();

        public BuySellPanel()
        {
            InitializeComponent();
            Settings.PropertyChanged += ( _1, _2 ) => RaiseChangedCommand();
            SettingsPropertyGrid.SelectedObject = Settings;
            Loaded += ( _1, _2 ) =>
              {
                  TrySubscribeWindow( Application.Current.MainWindow );
                  TrySubscribeWindow( Window.GetWindow( this ) );
              };
        }

        private void TrySubscribeWindow( Window win )
        {
            if ( win == null || _windows.Contains( win ) )
                return;
            _windows.Add( win );
            win.PreviewMouseDown += ( _, args ) =>
              {
                  if ( !SettingsPopup.IsOpen || IsInsidePopup( args.OriginalSource ) )
                      return;
                  SettingsPopup.IsOpen = false;
              };
        }

        private bool IsInsidePopup( object src )
        {
            for ( DependencyObject reference = src as DependencyObject; reference != null; reference = VisualTreeHelper.GetParent( reference ) )
            {
                if ( reference == SettingsPanel )
                    return true;
            }
            return false;
        }

        public IStudioControl Host { get; set; }

        private void SendRegisterOrder( Sides direction, Decimal price = 0M, OrderTypes type = OrderTypes.Market )
        {
            if ( Host == null )
                return;
            if ( Settings.Security == null )
            {
                SecurityPickerWindow wnd = new SecurityPickerWindow() { SecurityProvider = ServicesRegistry.SecurityProvider, SelectionMode = MultiSelectMode.Row };
                if ( wnd.ShowModal( this ) )
                    Settings.Security = wnd.SelectedSecurity;
                if ( Settings.Security == null )
                    return;
            }
            if ( Settings.Portfolio == null )
            {
                PortfolioPickerWindow wnd = new PortfolioPickerWindow() { Portfolios = StudioServicesRegistry.PortfolioDataSource };
                if ( wnd.ShowModal( this ) )
                    Settings.Portfolio = wnd.SelectedPortfolio;
                if ( Settings.Portfolio == null )
                    return;
            }
            Host.SendCommand( new RegisterOrderCommand( new Order()
            {
                Portfolio = Settings.Portfolio,
                Security = Settings.Security,
                Direction = direction,
                Price = price,
                Volume = Settings.Volume,
                Type = new OrderTypes?( type )
            } ) );
        }

        private void BuyAtLimit_Click( object sender, RoutedEventArgs e )
        {
            if ( Settings.LimitPrice <= Decimal.Zero )
                return;
            SendRegisterOrder( Sides.Buy, Settings.LimitPrice, OrderTypes.Limit );
        }

        private void SellAtLimit_Click( object sender, RoutedEventArgs e )
        {
            if ( Settings.LimitPrice <= Decimal.Zero )
                return;
            SendRegisterOrder( Sides.Sell, Settings.LimitPrice, OrderTypes.Limit );
        }

        private void BuyAtMarket_Click( object sender, RoutedEventArgs e )
        {
            SendRegisterOrder( Sides.Buy, Decimal.Zero, OrderTypes.Market );
        }

        private void SellAtMarket_Click( object sender, RoutedEventArgs e )
        {
            SendRegisterOrder( Sides.Sell, Decimal.Zero, OrderTypes.Market );
        }

        private void ClosePosition_Click( object sender, RoutedEventArgs e )
        {
            if ( Host == null || Settings.Security == null )
                return;
            Host.SendCommand( new ClosePositionCommand( Settings.Security ) );
        }

        private void RevertPosition_Click( object sender, RoutedEventArgs e )
        {
            if ( Host == null || Settings.Security == null )
                return;
            Host.SendCommand( new RevertPositionCommand( Settings.Security ) );
        }

        private void CancelAll_Click( object sender, RoutedEventArgs e )
        {
            if ( Host == null )
                return;
            Host.SendCommand( new CancelAllOrdersCommand() );
        }

        public override void Save( SettingsStorage storage )
        {
            storage.SetValue( "Settings", Settings.Save() );
            base.Save( storage );
        }

        public override void Load( SettingsStorage storage )
        {
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "Settings", null );
            if ( storage1 != null )
                Settings.Load( storage1 );
            base.Load( storage );
        }

        
    }
}
