﻿// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OrdersPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.Common;
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
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "Orders" )]
    [DescriptionLoc( "Str3268", false )]
    [VectorIcon( "Order" )]
    [Doc( "topics/Designer_Orders.html" )]
    public partial class OrdersPanel : BaseStudioControl, IComponentConnector
    {
        private readonly SubscriptionManager _subscriptionManager;
        

        public OrdersPanel()
        {
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            Security prevSecurity = null;
            Portfolio prevPortfolio = null;
            OrderTypes? prevType = new OrderTypes?();
            OrderGrid.OrderRegistering += () =>
           {
               OrderWindow wnd = new OrderWindow() { SecurityProvider = SecurityProvider, MarketDataProvider = MarketDataProvider, Portfolios = PortfolioDataSource, AdapterProvider = ServicesRegistry.AdapterProvider, PortfolioAdapterProvider = PortfolioMessageAdapterProvider, Order = CreateOrder( prevType ) };
               if ( !wnd.ShowModal( this ) )
                   return;
               Order order = wnd.Order;
               prevType = order.Type;
               prevSecurity = order.Security;
               prevPortfolio = order.Portfolio;
               new RegisterOrderCommand( order ).Process( this, false );
           };
            OrderGrid.OrderReRegistering += order =>
           {
               OrderWindow orderWindow1 = new OrderWindow();
               orderWindow1.Title = LocalizedStrings.Str2976Params.Put( order.TransactionId );
               orderWindow1.SecurityProvider = SecurityProvider;
               orderWindow1.MarketDataProvider = MarketDataProvider;
               orderWindow1.Portfolios = PortfolioDataSource;
               orderWindow1.AdapterProvider = ServicesRegistry.AdapterProvider;
               orderWindow1.PortfolioAdapterProvider = PortfolioMessageAdapterProvider;
               OrderWindow orderWindow2 = orderWindow1;
               Order oldOrder = order;
               Decimal? nullable = new Decimal?( order.Balance );
               Decimal? newPrice = new Decimal?();
               Decimal? newVolume = nullable;
               Order order1 = oldOrder.ReRegisterClone( newPrice, newVolume );
               orderWindow2.Order = order1;
               orderWindow1.SecurityEnabled = false;
               orderWindow1.PortfolioEnabled = false;
               orderWindow1.OrderTypeEnabled = false;
               OrderWindow wnd = orderWindow1;
               if ( !wnd.ShowModal( this ) )
                   return;
               new ReRegisterOrderCommand( order, wnd.Order ).Process( this, false );
           };
            OrderGrid.OrderCanceling += order => new CancelOrderCommand( order ).Process( this, false );
            OrderGrid.SelectionChanged += ( s, e ) => new SelectCommand<Order>( OrderGrid.SelectedOrder, false ).Process( this, false );
            OrderGrid.LayoutChanged += RaiseChangedCommand;
            AlertBtn.SchemaChanged += RaiseChangedCommand;
            GotFocus += ( s, e ) => new SelectCommand<Order>( OrderGrid.SelectedOrder, false ).Process( this, false );
            IStudioCommandService commandService = CommandService;
            commandService.Register<ResetedCommand>( this, false, cmd => OrderGrid.Orders.Clear(), null );
            commandService.Register(
                                        this,
                                        false,
                                        new Action<OrderCommand>( ( OrderGrid.Orders ).TryAddEntities ),
                                        null );

            commandService.Register<ReRegisterOrderCommand>( this, false, cmd =>
             {
                 bool? nullable = Connector.IsOrderEditable( cmd.OldOrder );
                 bool flag = true;
                 if ( nullable.GetValueOrDefault() == flag & nullable.HasValue )
                     return;
                 OrderGrid.Orders.Add( cmd.NewOrder );
             }, null );

            commandService.Register<OrderFailCommand>( this, false, cmd =>
             {
                 if ( cmd.Type != OrderFailTypes.Register )
                     return;
                 OrderFail entity = cmd.Entity;
                 Order order = entity.Order;
                 if ( !OrderGrid.Orders.Contains( order ) )
                     OrderGrid.Orders.Add( order );
                 OrderGrid.AddRegistrationFail( entity );
             }, null );

            commandService.Register<BindCommand>( this, true, cmd =>
             {
                 if ( !cmd.CheckControl( this ) )
                     return;
                 prevSecurity = cmd.Source.Security;
                 prevPortfolio = cmd.Source.Portfolio;
                 OrderGrid.IsInteractive = cmd.IsInteractive;
             }, null );

            WhenLoaded( () =>
            {
                new RequestBindSource( this ).SyncProcess( this );
                _subscriptionManager.CreateSubscription( DataType.Transactions, null );
            } );

            Order CreateOrder( OrderTypes? type )
            {
                return new Order() { Type = type, Security = prevSecurity, Portfolio = prevPortfolio };
            }
        }

        public override void Dispose( CloseReason reason )
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<ResetedCommand>( this );
            commandService.UnRegister<OrderCommand>( this );
            commandService.UnRegister<ReRegisterOrderCommand>( this );
            commandService.UnRegister<OrderFailCommand>( this );
            commandService.UnRegister<BindCommand>( this );
            if ( reason == CloseReason.CloseWindow )
                AlertBtn.Dispose();
            _subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "OrderGrid", OrderGrid.Save() );
            storage.SetValue( "AlertBtn", AlertBtn.Save() );
            storage.SetValue( "BarManager", BarManager.SaveDevExpressControl() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            OrderGrid.Load( storage.GetValue<SettingsStorage>( "OrderGrid", null ) );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "AlertBtn", null );
            if ( storage1 != null )
                AlertBtn.Load( storage1 );
            string settings = storage.GetValue<string>( "BarManager", null );
            if ( settings == null )
                return;
            BarManager.LoadDevExpressControl( settings );
        }
    }
}
