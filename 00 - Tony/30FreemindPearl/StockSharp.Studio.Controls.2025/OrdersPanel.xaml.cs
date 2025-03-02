// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OrdersPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Grid;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Controls.Commands;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "OrdersPanel", Name = "Orders", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Order" )]
    [Doc( "topics/designer/user_interface/components/orders.html" )]
    public partial class OrdersPanel : BaseStudioControl
    {
        private readonly SubscriptionManager _subscriptionManager;
        
        public OrdersPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl ) this );
            Security prevSecurity = (Security) null;
            Portfolio prevPortfolio = (Portfolio) null;
            OrderTypes? prevType = new OrderTypes?();
            this.OrderGrid.OrderRegistering += ( Action ) ( () =>
            {
                OrderWindow wnd = new OrderWindow()
                {
                    SecurityProvider = BaseStudioControl.SecurityProvider,
                    MarketDataProvider = BaseStudioControl.MarketDataProvider,
                    Portfolios = BaseStudioControl.PortfolioDataSource,
                    AdapterProvider = ServicesRegistry.AdapterProvider,
                    PortfolioAdapterProvider = BaseStudioControl.PortfolioMessageAdapterProvider,
                    Order = CreateOrder(prevType)
                };
                if ( !wnd.ShowModal( ( DependencyObject ) this ) )
                    return;
                Order order = wnd.Order;
                prevType = order.Type;
                prevSecurity = order.Security;
                prevPortfolio = order.Portfolio;
                new RegisterOrderCommand( order ).Process(  this, false );
            } );
            this.OrderGrid.OrderReRegistering += ( Action<Order> ) ( order =>
            {
                OrderWindow orderWindow1 = new OrderWindow();
                orderWindow1.Title = StringHelper.Put( LocalizedStrings.ReregistrationOfOrder, new object [1]
          {
          (object) order.TransactionId
              } );
                orderWindow1.SecurityProvider = BaseStudioControl.SecurityProvider;
                orderWindow1.MarketDataProvider = BaseStudioControl.MarketDataProvider;
                orderWindow1.Portfolios = BaseStudioControl.PortfolioDataSource;
                orderWindow1.AdapterProvider = ServicesRegistry.AdapterProvider;
                orderWindow1.PortfolioAdapterProvider = BaseStudioControl.PortfolioMessageAdapterProvider;
                OrderWindow orderWindow2 = orderWindow1;
                Order oldOrder = order;
                Decimal? nullable = new Decimal?(order.Balance);
                Decimal? newPrice = new Decimal?();
                Decimal? newVolume = nullable;
                Order order1 = oldOrder.ReRegisterClone(newPrice, newVolume);
                orderWindow2.Order = order1;
                orderWindow1.SecurityEnabled = false;
                orderWindow1.PortfolioEnabled = false;
                orderWindow1.OrderTypeEnabled = false;
                OrderWindow wnd = orderWindow1;
                if ( !wnd.ShowModal( ( DependencyObject ) this ) )
                    return;
                new ReRegisterOrderCommand( order, wnd.Order ).Process(  this, false );
            } );
            this.OrderGrid.OrderCanceling += ( Action<Order> ) ( order => new CancelOrderCommand( order ).Process(  this, false ) );
            this.OrderGrid.SelectionChanged += ( GridSelectionChangedEventHandler ) ( ( s, e ) => this.OrderGrid.SelectedOrder.SendSelect<Order>(  this, false ) );
            this.OrderGrid.LayoutChanged += RaiseChangedCommand;
            this.GotFocus += ( RoutedEventHandler ) ( ( s, e ) => this.OrderGrid.SelectedOrder.SendSelect<Order>(  this, false ) );
            this.Register<ResetedCommand>(  this, false, ( Action<ResetedCommand> ) ( cmd => ( ( ICollection<Order> ) this.OrderGrid.Orders ).Clear() ), ( Func<ResetedCommand, bool> ) null );
            this.Register<OrderCommand>(  this, false, new Action<OrderCommand>( (  this.OrderGrid.Orders ).TryAddEntities<Order> ), ( Func<OrderCommand, bool> ) null );
            this.Register<ReRegisterOrderCommand>(  this, false, ( Action<ReRegisterOrderCommand> ) ( cmd =>
            {
                if ( BaseStudioControl.Connector.IsOrderEditable( cmd.OldOrder ).GetValueOrDefault() )
                    return;
                ( ( ICollection<Order> ) this.OrderGrid.Orders ).Add( cmd.NewOrder );
            } ), ( Func<ReRegisterOrderCommand, bool> ) null );
            this.Register<OrderFailCommand>(  this, false, ( Action<OrderFailCommand> ) ( cmd =>
            {
                if ( cmd.Type != OrderFailTypes.Register )
                    return;
                OrderFail entity = cmd.Entity;
                Order order = entity.Order;
                if ( !( ( ICollection<Order> ) this.OrderGrid.Orders ).Contains( order ) )
                    ( ( ICollection<Order> ) this.OrderGrid.Orders ).Add( order );
                this.OrderGrid.AddRegistrationFail( entity );
            } ), ( Func<OrderFailCommand, bool> ) null );
            this.Register<BindCommand>(  this, true, ( Action<BindCommand> ) ( cmd =>
            {
                if ( !cmd.CheckControl( ( IStudioControl ) this ) )
                    return;
                cmd.Binder.Init( ( Action<Strategy> ) ( s =>
          {
              prevSecurity = s.Security;
              prevPortfolio = s.Portfolio;
          } ), ( Action<Strategy> ) ( s => { } ) );
                this.OrderGrid.IsInteractive = cmd.IsInteractive;
            } ), ( Func<BindCommand, bool> ) null );
            this.WhenLoaded( ( Action ) ( () =>
            {
                new RequestBindSource( ( IStudioControl ) this ).SyncProcess(  this );
                this._subscriptionManager.CreateSubscription( StockSharp.Messages.DataType.Transactions, ( Action<Subscription> ) null );
            } ) );

            Order CreateOrder( OrderTypes? type )
            {
                return new Order()
                {
                    Type = type,
                    Security = prevSecurity,
                    Portfolio = prevPortfolio
                };
            }
        }

        public override void Dispose( CloseReason reason )
        {
            this.OrderGrid.LayoutChanged -= RaiseChangedCommand;
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "OrderGrid",  PersistableHelper.Save( ( IPersistable ) this.OrderGrid ) );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.OrderGrid, storage, "OrderGrid" );
        }        
    }
}
