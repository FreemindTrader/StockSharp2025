using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Alerts;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Portfolios" )]
    [DescriptionLoc( "Str3269", false )]
    [Guid( "AC608491-B1FE-4A80-8707-25F3CF263302" )]
    [VectorIcon( "Portfolio" )]
    [Doc( "topics/Designer_Panel_Portfolios.html" )]
    public partial class PortfoliosPanel : BaseStudioControl, IComponentConnector
    {
        public static RoutedCommand ClosePositionCommand = new RoutedCommand();
        public static RoutedCommand RevertPositionCommand = new RoutedCommand();
        public static RoutedCommand WithdrawCommand = new RoutedCommand();
        public static readonly DependencyProperty ShowToolBarProperty = DependencyProperty.Register( nameof( ShowToolBar ), typeof( bool ), typeof( PortfoliosPanel ), new PropertyMetadata( ( object )true ) );
        private readonly SubscriptionManager _subscriptionManager;
        

        public bool ShowToolBar
        {
            get
            {
                return ( bool )this.GetValue( PortfoliosPanel.ShowToolBarProperty );
            }
            set
            {
                this.SetValue( PortfoliosPanel.ShowToolBarProperty, ( object )value );
            }
        }

        public PortfoliosPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            if ( this.IsDesignMode() )
                return;
            this.PortfolioGrid.LayoutChanged += RaiseChangedCommand;
            this.PortfolioGrid.SelectionChanged += ( GridSelectionChangedEventHandler )( ( s, e ) => new SelectCommand<StockSharp.BusinessEntities.Position>( this.SelectedPosition, this.CanEditPosition ).Process( ( object )this, false ) );
            this.GotFocus += ( RoutedEventHandler )( ( s, e ) => new SelectCommand<StockSharp.BusinessEntities.Position>( this.SelectedPosition, this.CanEditPosition ).Process( ( object )this, false ) );
            this.PortfolioGrid.ItemDoubleClick += ( Action<object, ItemDoubleClickEventArgs> )( ( sender, e ) =>
              {
                  if ( this.SelectedPosition == null )
                      return;
                  new PositionEditCommand( this.SelectedPosition ).Process( ( object )this, false );
              } );
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.Register<EntityCommand<StockSharp.BusinessEntities.Position>>( ( object )this, false, new Action<EntityCommand<StockSharp.BusinessEntities.Position>>( ( PortfolioGrid.Positions ).TryAddEntities<StockSharp.BusinessEntities.Position> ), ( Func<EntityCommand<StockSharp.BusinessEntities.Position>, bool> )null );
            commandService.Register<ResetedCommand>( ( object )this, false, ( Action<ResetedCommand> )( cmd => this.PortfolioGrid.Positions.Clear() ), ( Func<ResetedCommand, bool> )null );
            this.PortfolioGrid.PopupMenu.Items.Add( ( IBarItem )new BarItemSeparator() );
            CommonBarItemCollection items1 = this.PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem1 = new BarButtonItem();
            barButtonItem1.Glyph = ThemedIconsExtension.GetImage( "Remove2" );
            barButtonItem1.Content = ( object )LocalizedStrings.XamlStr173;
            barButtonItem1.Command = ( ICommand )PortfoliosPanel.ClosePositionCommand;
            barButtonItem1.CommandTarget = ( IInputElement )this;
            items1.Add( ( IBarItem )barButtonItem1 );
            CommonBarItemCollection items2 = this.PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem2 = new BarButtonItem();
            barButtonItem2.Glyph = ThemedIconsExtension.GetImage( "Refresh" );
            barButtonItem2.Content = ( object )LocalizedStrings.RevertPosition;
            barButtonItem2.Command = ( ICommand )PortfoliosPanel.RevertPositionCommand;
            barButtonItem2.CommandTarget = ( IInputElement )this;
            items2.Add( ( IBarItem )barButtonItem2 );
            CommonBarItemCollection items3 = this.PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem3 = new BarButtonItem();
            barButtonItem3.Glyph = ThemedIconsExtension.GetImage( "Money" );
            barButtonItem3.Content = ( object )LocalizedStrings.Withdraw;
            barButtonItem3.Command = ( ICommand )PortfoliosPanel.WithdrawCommand;
            barButtonItem3.CommandTarget = ( IInputElement )this;
            items3.Add( ( IBarItem )barButtonItem3 );
            this.AlertBtn.SchemaChanged += RaiseChangedCommand;
            this.WhenLoaded( ( Action )( () => this._subscriptionManager.CreateSubscription( DataType.PositionChanges, ( Action<Subscription> )null ) ) );
        }

        private bool CanEditPosition { get; set; }

        private StockSharp.BusinessEntities.Position SelectedPosition
        {
            get
            {
                return this.PortfolioGrid?.SelectedPosition;
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "PortfolioGrid", this.PortfolioGrid.Save() );
            storage.SetValue<SettingsStorage>( "AlertBtn", this.AlertBtn.Save() );
            storage.SetValue<string>( "BarManager", this.BarManager.SaveDevExpressControl() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "PortfolioGrid", ( SettingsStorage )null );
            if ( storage1 != null )
            {
                this.PortfolioGrid.ForceLoad<PortfolioGrid>( storage1 );
            }
            else
            {
                SettingsStorage settingsStorage = storage.GetValue<SettingsStorage>( "PositionsPanel", ( SettingsStorage )null );
                if ( settingsStorage != null )
                    this.PortfolioGrid.ForceLoad<PortfolioGrid>( settingsStorage.GetValue<SettingsStorage>( "PortfolioGrid", ( SettingsStorage )null ) );
            }
            SettingsStorage storage2 = storage.GetValue<SettingsStorage>( "AlertBtn", ( SettingsStorage )null );
            if ( storage2 != null )
                this.AlertBtn.Load( storage2 );
            string settings = storage.GetValue<string>( "BarManager", ( string )null );
            if ( settings == null )
                return;
            this.BarManager.LoadDevExpressControl( settings );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.UnRegister<EntityCommand<Portfolio>>( ( object )this );
            commandService.UnRegister<EntityCommand<StockSharp.BusinessEntities.Position>>( ( object )this );
            commandService.UnRegister<ResetedCommand>( ( object )this );
            this._subscriptionManager.Dispose();
            base.Dispose();
        }

        private void CreatePortfolio_OnClick( object sender, RoutedEventArgs e )
        {
            new PositionEditCommand( ( StockSharp.BusinessEntities.Position )new Portfolio() ).Process( ( object )this, false );
        }

        private void CreatePosition_OnClick( object sender, RoutedEventArgs e )
        {
            new PositionEditCommand( new StockSharp.BusinessEntities.Position() ).Process( ( object )this, false );
        }

        private void ClosePositionCommand_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            new StockSharp.Studio.Core.Commands.ClosePositionCommand( this.SelectedPosition ).Process( ( object )this, false );
        }

        private void ClosePositionCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.SelectedPosition.CanCloseOrRevert();
        }

        private void RevertPositionCommand_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            new StockSharp.Studio.Core.Commands.RevertPositionCommand( this.SelectedPosition ).Process( ( object )this, false );
        }

        private void RevertPositionCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = this.SelectedPosition.CanCloseOrRevert();
        }

        private void WithdrawCommand_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            StockSharp.BusinessEntities.Position selectedPosition = this.SelectedPosition;
            IMessageAdapter adapter = BaseStudioControl.PortfolioMessageAdapterProvider.TryGetAdapter( ServicesRegistry.AdapterProvider, selectedPosition.Portfolio );
            WithdrawWindow withdrawWindow = new WithdrawWindow();
            Decimal? nullable = selectedPosition.Security.VolumeStep;
            withdrawWindow.VolumeStep = nullable ?? new Decimal( 1, 0, 0, false, ( byte )6 );
            WithdrawWindow wnd = withdrawWindow;
            if ( !wnd.ShowModal( ( DependencyObject )this ) )
                return;
            nullable = wnd.Volume;
            if ( !nullable.HasValue )
                return;
            OrderCondition orderCondition = adapter.CreateOrderCondition();
            IWithdrawOrderCondition withdrawOrderCondition = orderCondition as IWithdrawOrderCondition;
            if ( withdrawOrderCondition == null )
                return;
            withdrawOrderCondition.IsWithdraw = true;
            withdrawOrderCondition.WithdrawInfo = wnd.WithdrawInfo;
            Order order = new Order();
            order.Portfolio = selectedPosition.Portfolio;
            order.Security = selectedPosition.Security;
            order.Type = new OrderTypes?( OrderTypes.Conditional );
            order.Condition = orderCondition;
            nullable = wnd.Volume;
            order.Volume = nullable.Value;
            new RegisterOrderCommand( order ).Process( ( object )this, false );
        }

        private void WithdrawCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            StockSharp.BusinessEntities.Position selectedPosition = this.SelectedPosition;
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            int num1;
            if ( selectedPosition.CanCloseOrRevert() )
            {
                Decimal? currentValue = selectedPosition.CurrentValue;
                Decimal num2 = new Decimal();
                if ( currentValue.GetValueOrDefault() > num2 & currentValue.HasValue )
                {
                    IMessageAdapter adapter = BaseStudioControl.PortfolioMessageAdapterProvider.TryGetAdapter( ServicesRegistry.AdapterProvider, selectedPosition.Portfolio );
                    num1 = adapter != null ? ( adapter.IsSupportWithdraw() ? 1 : 0 ) : 0;
                    goto label_4;
                }
            }
            num1 = 0;
        label_4:
            executeRoutedEventArgs.CanExecute = num1 != 0;
        }

        
    }
}
