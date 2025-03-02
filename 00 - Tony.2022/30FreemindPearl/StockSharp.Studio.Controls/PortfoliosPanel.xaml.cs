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
        public static readonly DependencyProperty ShowToolBarProperty = DependencyProperty.Register( nameof( ShowToolBar ), typeof( bool ), typeof( PortfoliosPanel ), new PropertyMetadata( true ) );
        private readonly SubscriptionManager _subscriptionManager;
        

        public bool ShowToolBar
        {
            get
            {
                return ( bool )GetValue( ShowToolBarProperty );
            }
            set
            {
                SetValue( ShowToolBarProperty, value );
            }
        }

        public PortfoliosPanel()
        {
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            if ( this.IsDesignMode() )
                return;
            PortfolioGrid.LayoutChanged += RaiseChangedCommand;
            PortfolioGrid.SelectionChanged += ( s, e ) => new SelectCommand<Position>( SelectedPosition, CanEditPosition ).Process( this, false );
            GotFocus += ( s, e ) => new SelectCommand<Position>( SelectedPosition, CanEditPosition ).Process( this, false );
            PortfolioGrid.ItemDoubleClick += ( sender, e ) =>
              {
                  if ( SelectedPosition == null )
                      return;
                  new PositionEditCommand( SelectedPosition ).Process( this, false );
              };
            IStudioCommandService commandService = CommandService;
            commandService.Register( this, false, new Action<EntityCommand<Position>>( ( PortfolioGrid.Positions ).TryAddEntities ), null );
            commandService.Register<ResetedCommand>( this, false, cmd => PortfolioGrid.Positions.Clear(), null );
            PortfolioGrid.PopupMenu.Items.Add( new BarItemSeparator() );
            CommonBarItemCollection items1 = PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem1 = new BarButtonItem();
            barButtonItem1.Glyph = ThemedIconsExtension.GetImage( "Remove2" );
            barButtonItem1.Content = LocalizedStrings.XamlStr173;
            barButtonItem1.Command = ClosePositionCommand;
            barButtonItem1.CommandTarget = this;
            items1.Add( barButtonItem1 );
            CommonBarItemCollection items2 = PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem2 = new BarButtonItem();
            barButtonItem2.Glyph = ThemedIconsExtension.GetImage( "Refresh" );
            barButtonItem2.Content = LocalizedStrings.RevertPosition;
            barButtonItem2.Command = RevertPositionCommand;
            barButtonItem2.CommandTarget = this;
            items2.Add( barButtonItem2 );
            CommonBarItemCollection items3 = PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem3 = new BarButtonItem();
            barButtonItem3.Glyph = ThemedIconsExtension.GetImage( "Money" );
            barButtonItem3.Content = LocalizedStrings.Withdraw;
            barButtonItem3.Command = WithdrawCommand;
            barButtonItem3.CommandTarget = this;
            items3.Add( barButtonItem3 );
            AlertBtn.SchemaChanged += RaiseChangedCommand;
            WhenLoaded( () => _subscriptionManager.CreateSubscription( DataType.PositionChanges, null ) );
        }

        private bool CanEditPosition { get; set; }

        private Position SelectedPosition
        {
            get
            {
                return PortfolioGrid?.SelectedPosition;
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "PortfolioGrid", PortfolioGrid.Save() );
            storage.SetValue( "AlertBtn", AlertBtn.Save() );
            storage.SetValue( "BarManager", BarManager.SaveDevExpressControl() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "PortfolioGrid", null );
            if ( storage1 != null )
            {
                PortfolioGrid.ForceLoad( storage1 );
            }
            else
            {
                SettingsStorage settingsStorage = storage.GetValue<SettingsStorage>( "PositionsPanel", null );
                if ( settingsStorage != null )
                    PortfolioGrid.ForceLoad( settingsStorage.GetValue<SettingsStorage>( "PortfolioGrid", null ) );
            }
            SettingsStorage storage2 = storage.GetValue<SettingsStorage>( "AlertBtn", null );
            if ( storage2 != null )
                AlertBtn.Load( storage2 );
            string settings = storage.GetValue<string>( "BarManager", null );
            if ( settings == null )
                return;
            BarManager.LoadDevExpressControl( settings );
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<EntityCommand<Portfolio>>( this );
            commandService.UnRegister<EntityCommand<Position>>( this );
            commandService.UnRegister<ResetedCommand>( this );
            _subscriptionManager.Dispose();
            base.Dispose();
        }

        private void CreatePortfolio_OnClick( object sender, RoutedEventArgs e )
        {
            new PositionEditCommand( new Portfolio() ).Process( this, false );
        }

        private void CreatePosition_OnClick( object sender, RoutedEventArgs e )
        {
            new PositionEditCommand( new Position() ).Process( this, false );
        }

        private void ClosePositionCommand_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            new ClosePositionCommand( SelectedPosition ).Process( this, false );
        }

        private void ClosePositionCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SelectedPosition.CanCloseOrRevert();
        }

        private void RevertPositionCommand_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            new RevertPositionCommand( SelectedPosition ).Process( this, false );
        }

        private void RevertPositionCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = SelectedPosition.CanCloseOrRevert();
        }

        private void WithdrawCommand_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            Position selectedPosition = SelectedPosition;
            IMessageAdapter adapter = PortfolioMessageAdapterProvider.TryGetAdapter( ServicesRegistry.AdapterProvider, selectedPosition.Portfolio );
            WithdrawWindow withdrawWindow = new WithdrawWindow();
            Decimal? nullable = selectedPosition.Security.VolumeStep;
            withdrawWindow.VolumeStep = nullable ?? new Decimal( 1, 0, 0, false, 6 );
            WithdrawWindow wnd = withdrawWindow;
            if ( !wnd.ShowModal( this ) )
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
            new RegisterOrderCommand( order ).Process( this, false );
        }

        private void WithdrawCommand_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            Position selectedPosition = SelectedPosition;
            CanExecuteRoutedEventArgs executeRoutedEventArgs = e;
            int num1;
            if ( selectedPosition.CanCloseOrRevert() )
            {
                Decimal? currentValue = selectedPosition.CurrentValue;
                Decimal num2 = new Decimal();
                if ( currentValue.GetValueOrDefault() > num2 & currentValue.HasValue )
                {
                    IMessageAdapter adapter = PortfolioMessageAdapterProvider.TryGetAdapter( ServicesRegistry.AdapterProvider, selectedPosition.Portfolio );
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
