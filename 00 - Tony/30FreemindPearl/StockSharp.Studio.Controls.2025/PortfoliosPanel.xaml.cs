// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.PortfoliosPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "PortfoliosPanel", Name = "Portfolios", ResourceType = typeof( LocalizedStrings ) )]
    [Guid( "AC608491-B1FE-4A80-8707-25F3CF263302" )]
    [VectorIcon( "Portfolio" )]
    [Doc( "topics/designer/user_interface/portfolios.html" )]
    public partial class PortfoliosPanel : BaseStudioControl
    {
        public static readonly DependencyProperty ShowToolBarProperty = DependencyProperty.Register(nameof (ShowToolBar), typeof (bool), typeof (PortfoliosPanel), new PropertyMetadata((object) true));
        private readonly SubscriptionManager _subscriptionManager;
        
        public bool ShowToolBar
        {
            get
            {
                return ( bool ) this.GetValue( PortfoliosPanel.ShowToolBarProperty );
            }
            set
            {
                this.SetValue( PortfoliosPanel.ShowToolBarProperty,  value );
            }
        }

        public PortfoliosPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl ) this );
            if ( this.IsDesignMode() )
                return;
            this.PortfolioGrid.LayoutChanged += RaiseChangedCommand;
            this.PortfolioGrid.SelectionChanged += ( GridSelectionChangedEventHandler ) ( ( s, e ) => this.SelectedPosition.SendSelect<StockSharp.BusinessEntities.Position>(  this, this.CanEditPosition ) );
            this.GotFocus += ( RoutedEventHandler ) ( ( s, e ) => this.SelectedPosition.SendSelect<StockSharp.BusinessEntities.Position>(  this, this.CanEditPosition ) );
            this.PortfolioGrid.ItemDoubleClick += ( Action<object, ItemDoubleClickEventArgs> ) ( ( sender, e ) =>
            {
                if ( this.SelectedPosition == null )
                    return;
                new PositionEditCommand( this.SelectedPosition ).Process(  this, false );
            } );
            this.Register<EntityCommand<StockSharp.BusinessEntities.Position>>(  this, false, new Action<EntityCommand<StockSharp.BusinessEntities.Position>>( ( this.PortfolioGrid.Positions ).TryAddEntity<StockSharp.BusinessEntities.Position> ), ( Func<EntityCommand<StockSharp.BusinessEntities.Position>, bool> ) null );
            this.Register<ResetedCommand>(  this, false, ( Action<ResetedCommand> ) ( cmd => ( ( ICollection<StockSharp.BusinessEntities.Position> ) this.PortfolioGrid.Positions ).Clear() ), ( Func<ResetedCommand, bool> ) null );
            this.PortfolioGrid.PopupMenu.Items.Add( ( IBarItem ) new BarItemSeparator() );
            CommonBarItemCollection items1 = this.PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem1 = new BarButtonItem();
            barButtonItem1.Glyph = ThemedIconsExtension.GetImage( "Remove2" );
            barButtonItem1.Content =  LocalizedStrings.ClosePosition;
            barButtonItem1.Command = ( ICommand ) new DelegateCommand( new Action<object>( this.ClosePositionCommand_Executed ), new Func<object, bool>( this.ClosePositionCommand_CanExecute ) );
            barButtonItem1.CommandTarget = ( IInputElement ) this;
            items1.Add( ( IBarItem ) barButtonItem1 );
            CommonBarItemCollection items2 = this.PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem2 = new BarButtonItem();
            barButtonItem2.Glyph = ThemedIconsExtension.GetImage( "Refresh" );
            barButtonItem2.Content =  LocalizedStrings.RevertPosition;
            barButtonItem2.Command = ( ICommand ) new DelegateCommand( new Action<object>( this.RevertPositionCommand_Executed ), new Func<object, bool>( this.RevertPositionCommand_CanExecute ) );
            barButtonItem2.CommandTarget = ( IInputElement ) this;
            items2.Add( ( IBarItem ) barButtonItem2 );
            CommonBarItemCollection items3 = this.PortfolioGrid.PopupMenu.Items;
            BarButtonItem barButtonItem3 = new BarButtonItem();
            barButtonItem3.Glyph = ThemedIconsExtension.GetImage( "Money" );
            barButtonItem3.Content =  LocalizedStrings.Withdraw;
            barButtonItem3.Command = ( ICommand ) new DelegateCommand( new Action<object>( this.WithdrawCommand_Executed ), new Func<object, bool>( this.WithdrawCommand_CanExecute ) );
            barButtonItem3.CommandTarget = ( IInputElement ) this;
            items3.Add( ( IBarItem ) barButtonItem3 );
            this.WhenLoaded( ( Action ) ( () => this._subscriptionManager.CreateSubscription( StockSharp.Messages.DataType.PositionChanges, ( Action<Subscription> ) null ) ) );
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
            storage.SetValue<SettingsStorage>( "PortfolioGrid",  PersistableHelper.Save( ( IPersistable ) this.PortfolioGrid ) );
            storage.SetValue<string>( "BarManager",  this.BarManager.SaveDevExpressControl() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            if ( !PersistableHelper.LoadIfNotNull( ( IPersistable ) this.PortfolioGrid, storage, "PortfolioGrid" ) )
            {
                SettingsStorage settingsStorage = (SettingsStorage) storage.GetValue<SettingsStorage>("PositionsPanel",  null);
                if ( settingsStorage != null )
                    PersistableHelper.LoadIfNotNull( ( IPersistable ) this.PortfolioGrid, settingsStorage, "PortfolioGrid" );
            }
            string settings = (string) storage.GetValue<string>("BarManager",  null);
            if ( settings == null )
                return;
            this.BarManager.LoadDevExpressControl( settings );
        }

        public override void Dispose( CloseReason reason )
        {
            this.PortfolioGrid.LayoutChanged -= RaiseChangedCommand;
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        private void CreatePortfolio_OnClick( object sender, RoutedEventArgs e )
        {
            new PositionEditCommand( ( StockSharp.BusinessEntities.Position ) new Portfolio() ).Process(  this, false );
        }

        private void CreatePosition_OnClick( object sender, RoutedEventArgs e )
        {
            new PositionEditCommand( new StockSharp.BusinessEntities.Position() ).Process(  this, false );
        }

        private void ClosePositionCommand_Executed( object sender )
        {
            new ClosePositionCommand( this.SelectedPosition ).Process(  this, false );
        }

        private bool ClosePositionCommand_CanExecute( object sender )
        {
            return this.SelectedPosition.CanCloseOrRevert();
        }

        private void RevertPositionCommand_Executed( object sender )
        {
            new RevertPositionCommand( this.SelectedPosition ).Process(  this, false );
        }

        private bool RevertPositionCommand_CanExecute( object sender )
        {
            return this.SelectedPosition.CanCloseOrRevert();
        }

        private void WithdrawCommand_Executed( object sender )
        {
            StockSharp.BusinessEntities.Position selectedPosition = this.SelectedPosition;
            IMessageAdapter adapter = BaseStudioControl.PortfolioMessageAdapterProvider.TryGetAdapter(ServicesRegistry.AdapterProvider, selectedPosition.Portfolio);
            WithdrawWindow wnd = new WithdrawWindow()
            {
                VolumeStep = selectedPosition.Security.VolumeStep.GetValueOrDefault(new Decimal(1, 0, 0, false, (byte) 6))
            };
            if ( !wnd.ShowModal( ( DependencyObject ) this ) )
                return;
            Decimal? volume = wnd.Volume;
            if ( !volume.HasValue )
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
            volume = wnd.Volume;
            order.Volume = volume.Value;
            new RegisterOrderCommand( order ).Process(  this, false );
        }

        private bool WithdrawCommand_CanExecute( object sender )
        {
            StockSharp.BusinessEntities.Position selectedPosition = this.SelectedPosition;
            if ( selectedPosition.CanCloseOrRevert() )
            {
                Decimal? currentValue = selectedPosition.CurrentValue;
                Decimal num = new Decimal();
                if ( currentValue.GetValueOrDefault() > num & currentValue.HasValue )
                {
                    IMessageAdapter adapter = BaseStudioControl.PortfolioMessageAdapterProvider.TryGetAdapter(ServicesRegistry.AdapterProvider, selectedPosition.Portfolio);
                    if ( adapter == null )
                        return false;
                    return adapter.IsSupportWithdraw();
                }
            }
            return false;
        }
        
    }
}
