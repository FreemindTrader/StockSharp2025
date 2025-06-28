// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OrdersPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Markup;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
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
using DataType = StockSharp.Messages.DataType;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "Orders", Description = "OrdersPanel")]
[VectorIcon("Order")]
[Doc("topics/designer/user_interface/components/orders.html")]
public partial class OrdersPanel : BaseStudioControl, IComponentConnector
{
    private readonly SubscriptionManager _subscriptionManager;

    public OrdersPanel()
    {
        this.InitializeComponent();
        this._subscriptionManager = new SubscriptionManager((IStudioControl)this);
        Security prevSecurity = (Security)null;
        Portfolio prevPortfolio = (Portfolio)null;
        OrderTypes? prevType = new OrderTypes?();
        this.OrderGrid.OrderRegistering += (Action)(() =>
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
            if (!wnd.ShowModal((DependencyObject)this))
                return;
            Order order = wnd.Order;
            prevType = order.Type;
            prevSecurity = order.Security;
            prevPortfolio = order.Portfolio;
            new RegisterOrderCommand(order).Process((object)this);
        });
        this.OrderGrid.OrderReRegistering += (Action<Order>)(order =>
        {
            OrderWindow orderWindow1 = new OrderWindow();
            orderWindow1.Title = StringHelper.Put(LocalizedStrings.ReregistrationOfOrder, new object[1]
        {
        (object) order.TransactionId
          });
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
            if (!wnd.ShowModal((DependencyObject)this))
                return;
            new ReRegisterOrderCommand(order, wnd.Order).Process((object)this);
        });
        this.OrderGrid.OrderCanceling += (Action<Order>)(order => new CancelOrderCommand(order).Process((object)this));
        this.OrderGrid.SelectionChanged += (GridSelectionChangedEventHandler)((s, e) => this.OrderGrid.SelectedOrder.SendSelect<Order>((object)this));
        this.OrderGrid.LayoutChanged += RaiseChangedCommand;
        this.GotFocus += (RoutedEventHandler)((s, e) => this.OrderGrid.SelectedOrder.SendSelect<Order>((object)this));
        this.Register<ResetedCommand>((object)this, false, (Action<ResetedCommand>)(cmd => ((ICollection<Order>)this.OrderGrid.Orders).Clear()));
        this.Register<OrderCommand>((object)this, false, new Action<OrderCommand>((this.OrderGrid.Orders).TryAddEntities<Order>));
        this.Register<ReRegisterOrderCommand>((object)this, false, (Action<ReRegisterOrderCommand>)(cmd =>
        {
            if (BaseStudioControl.Connector.IsOrderEditable(cmd.OldOrder).GetValueOrDefault())
                return;
            CollectionHelper.TryAdd<Order>((ICollection<Order>)this.OrderGrid.Orders, cmd.NewOrder);
        }));
        this.Register<OrderFailCommand>((object)this, false, (Action<OrderFailCommand>)(cmd =>
        {
            if (cmd.Type != OrderFailTypes.Register)
                return;
            OrderFail entity = cmd.Entity;
            CollectionHelper.TryAdd<Order>((ICollection<Order>)this.OrderGrid.Orders, entity.Order);
            this.OrderGrid.AddRegistrationFail(entity);
        }));
        this.Register<BindCommand>((object)this, true, (Action<BindCommand>)(cmd =>
        {
            if (!cmd.CheckControl((IStudioControl)this))
                return;
            cmd.Binder.Init((Action<Strategy>)(s =>
        {
            prevSecurity = s.Security;
            prevPortfolio = s.Portfolio;
        }), (Action<Strategy>)(s => { }));
            this.OrderGrid.IsInteractive = cmd.IsInteractive;
        }));
        this.WhenLoaded((Action)(() =>
        {
            new RequestBindSource((IStudioControl)this).SyncProcess((object)this);
            this._subscriptionManager.CreateSubscription(DataType.Transactions);
        }));

        Order CreateOrder(OrderTypes? type)
        {
            return new Order()
            {
                Type = type,
                Security = prevSecurity,
                Portfolio = prevPortfolio
            };
        }
    }

    public override void Dispose(CloseReason reason)
    {
        this.OrderGrid.LayoutChanged -= RaiseChangedCommand;
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("OrderGrid", PersistableHelper.Save((IPersistable)this.OrderGrid));
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.OrderGrid, storage, "OrderGrid");
    }


}
