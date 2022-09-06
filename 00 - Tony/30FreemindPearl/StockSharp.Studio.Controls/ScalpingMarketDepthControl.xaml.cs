// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ScalpingMarketDepthControl
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

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
using System;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "MarketDepth" )]
    [DescriptionLoc( "Str3272", false )]
    [VectorIcon( "UpDown" )]
    [Doc( "topics/Terminal_Chart.html" )]
    public partial class ScalpingMarketDepthControl : BaseStudioControl, IStudioCommandScope, IComponentConnector
    {
        private Subscription _subscription;
        private bool _isLoaded;
        private readonly SubscriptionManager _subscriptionManager;

        bool IStudioCommandScope.UseParentScope
        {
            get
            {
                return true;
            }
        }

        bool IStudioCommandScope.RouteToGlobalScope
        {
            get
            {
                return false;
            }
        }

        public BuySellSettings Settings
        {
            get
            {
                return this.BuySellPanel.Settings;
            }
        }

        public ScalpingMarketDepthControl()
        {
            DelayActionHelper delayActionHelper = new DelayActionHelper() { Interval = TimeSpan.FromSeconds( 2.5 ).TotalMilliseconds };
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            this.BuySellPanel.Host = ( IStudioControl )this;
            this.Settings.ValueChanged += ( Action<string, object, object> )( ( name, _1, _2 ) =>
              {
                  if ( name == "Security" || name == "Depth" )
                      delayActionHelper.Start( new Action( this.Resubscribe ) );
                  ( ( DispatcherObject )this ).GuiAsync( new Action( this.UpdateTitle ) );
                  if ( this._isLoaded )
                      return;
                  this.RaiseChangedCommand();
              } );
            this.MdControl.LayoutChanged += RaiseChangedCommand;
            this.MdControl.RegisteringOrder += new Action<Sides, Decimal>( this.OnRegisteringOrder );
            this.MdControl.MovingOrder += new Action<Order, Decimal>( this.OnMovingOrder );
            this.MdControl.CancelingOrder += new Action<Order>( this.OnCancelingOrder );
            this.MdControl.SelectedQuoteChanged += new Action<QuoteChange?>( this.OnSelectedQuoteChanged );
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.Register<EntityCommand<QuoteChangeMessage>>( ( object )this, false, ( Action<EntityCommand<QuoteChangeMessage>> )( cmd =>
                {
                    QuoteChangeMessage entity = cmd.Entity;
                    Security security = this.Settings.Security;
                    if ( security == null || !( entity.SecurityId == security.ToSecurityId( ( SecurityIdGenerator )null, true, false ) ) )
                        return;
                    this.MdControl.UpdateDepth( entity, security );
                } ), ( Func<EntityCommand<QuoteChangeMessage>, bool> )null );
            commandService.Register<ClearMarketDepthCommand>( ( object )this, true, ( Action<ClearMarketDepthCommand> )( cmd =>
                {
                    this.MdControl.Clear();
                    if ( cmd.Security == null || cmd.Security == this.Settings.Security )
                        return;
                    this.Settings.Security = cmd.Security;
                } ), ( Func<ClearMarketDepthCommand, bool> )null );
            commandService.Register<OrderCommand>( ( object )this, false, ( Action<OrderCommand> )( cmd =>
                {
                    Order entity = cmd.Entity;
                    OrderTypes? type = entity.Type;
                    OrderTypes orderTypes = OrderTypes.Conditional;
                    if ( type.GetValueOrDefault() == orderTypes & type.HasValue || entity.Security != this.Settings.Security )
                        return;
                    this.MdControl.ProcessOrder( entity, cmd.Price, cmd.Balance, cmd.State );
                } ), ( Func<OrderCommand, bool> )null );
            commandService.Register<OrderFailCommand>( ( object )this, false, ( Action<OrderFailCommand> )( cmd => this.MdControl.ProcessOrderFail( cmd.Entity, cmd.State ) ), ( Func<OrderFailCommand, bool> )null );
            commandService.Register<SecuritiesRemovedCommand>( ( object )this, false, ( Action<SecuritiesRemovedCommand> )( cmd =>
                {
                    bool flag = false;
                    foreach ( Security security in cmd.Securities )
                    {
                        if ( this.Settings.Security == security )
                        {
                            this.Settings.Security = ( Security )null;
                            this.MdControl.Clear();
                            flag = true;
                            break;
                        }
                    }
                    if ( !flag )
                        return;
                    this.RaiseChangedCommand();
                } ), ( Func<SecuritiesRemovedCommand, bool> )null );
            commandService.Register<FirstInitSecuritiesCommand>( ( object )this, true, ( Action<FirstInitSecuritiesCommand> )( cmd =>
                {
                    if ( this.Settings.Security != null )
                        return;
                    this.Settings.Security = cmd.Securities.First<Security>();
                } ), ( Func<FirstInitSecuritiesCommand, bool> )null );
            commandService.Register<FirstInitPortfoliosCommand>( ( object )this, true, ( Action<FirstInitPortfoliosCommand> )( cmd =>
                {
                    if ( this.Settings.Portfolio != null )
                        return;
                    this.Settings.Portfolio = cmd.Portfolios.First<Portfolio>();
                } ), ( Func<FirstInitPortfoliosCommand, bool> )null );
            this.WhenLoaded( ( Action )( () => this._subscriptionManager.CreateSubscription( DataType.Transactions, ( Action<Subscription> )null ) ) );
        }

        public override void SendCommand( IStudioCommand command )
        {
            if ( command is CancelAllOrdersCommand )
                this.MdControl.CancelOrders( ( Func<Order, bool> )null );
            else
                base.SendCommand( command );
        }

        private void OnSelectedQuoteChanged( QuoteChange? quote )
        {
            if ( !quote.HasValue )
                return;
            this.BuySellPanel.Settings.LimitPrice = quote.Value.Price;
        }

        private void OnCancelingOrder( Order order )
        {
            new CancelOrderCommand( order ).Process( ( object )this, false );
        }

        private void OnMovingOrder( Order order, Decimal newPrice )
        {
            Order order1 = order.ReRegisterClone( new Decimal?( newPrice ), new Decimal?() );
            bool? nullable = BaseStudioControl.Connector.IsOrderReplaceable( order );
            bool flag = true;
            if ( nullable.GetValueOrDefault() == flag & nullable.HasValue )
            {
                new ReRegisterOrderCommand( order, order1 ).Process( ( object )this, false );
            }
            else
            {
                new CancelOrderCommand( order ).Process( ( object )this, false );
                new RegisterOrderCommand( order1 ).Process( ( object )this, false );
            }
        }

        private void Resubscribe()
        {
            if ( this._subscription != null )
            {
                this._subscriptionManager.RemoveSubscription( this._subscription );
                this._subscription = ( Subscription )null;
            }
            this.MdControl.Clear();
            this._subscriptionManager.MaxDepth = new int?( Math.Max( 1, Math.Min( 200, this.Settings.Depth ) ) );
            Security security = this.Settings.Security;
            if ( security == null )
                return;
            this._subscription = this._subscriptionManager.CreateSubscription( security, DataType.MarketDepth, ( Action<Subscription> )null );
        }

        private void OnRegisteringOrder( Sides side, Decimal price )
        {
            if ( this.Settings.Portfolio == null )
            {
                PortfolioPickerWindow wnd = new PortfolioPickerWindow() { Portfolios = BaseStudioControl.PortfolioDataSource };
                if ( wnd.ShowModal( ( DependencyObject )this ) )
                    this.Settings.Portfolio = wnd.SelectedPortfolio;
            }
            if ( this.Settings.Portfolio == null )
                return;
            new RegisterOrderCommand( this.CreateOrder( side, price, OrderTypes.Limit ) ).Process( ( object )this, false );
        }

        private void UpdateTitle()
        {
            this.Title = this.Settings.Security != null ? this.Settings.Security.Id : LocalizedStrings.MarketDepth;
        }

        private Order CreateOrder( Sides direction, Decimal price = 0M, OrderTypes type = OrderTypes.Market )
        {
            return new Order() { Portfolio = this.Settings.Portfolio, Security = this.Settings.Security, Direction = direction, Price = price, Volume = this.Settings.Volume, Type = new OrderTypes?( type ) };
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            this.MdControl.RegisteringOrder -= new Action<Sides, Decimal>( this.OnRegisteringOrder );
            this.MdControl.MovingOrder -= new Action<Order, Decimal>( this.OnMovingOrder );
            this.MdControl.CancelingOrder -= new Action<Order>( this.OnCancelingOrder );
            this.MdControl.SelectedQuoteChanged -= new Action<QuoteChange?>( this.OnSelectedQuoteChanged );
            commandService.UnRegister<EntityCommand<QuoteChangeMessage>>( ( object )this );
            commandService.UnRegister<ClearMarketDepthCommand>( ( object )this );
            commandService.UnRegister<OrderCommand>( ( object )this );
            commandService.UnRegister<ResetedCommand>( ( object )this );
            commandService.UnRegister<SecuritiesRemovedCommand>( ( object )this );
            commandService.UnRegister<FirstInitSecuritiesCommand>( ( object )this );
            commandService.UnRegister<FirstInitPortfoliosCommand>( ( object )this );
            this._subscriptionManager.Dispose();
            base.Dispose();
        }

        public override void Save( SettingsStorage settings )
        {
            base.Save( settings );
            settings.SetValue<SettingsStorage>( "MdControl", this.MdControl.Save() );
            settings.SetValue<SettingsStorage>( "BuySellPanel", this.BuySellPanel.Save() );
        }

        public override void Load( SettingsStorage settings )
        {
            this._isLoaded = true;
            try
            {
                base.Load( settings );
                this.MdControl.Load( settings.GetValue<SettingsStorage>( "MdControl", ( SettingsStorage )null ) );
                SettingsStorage storage = settings.GetValue<SettingsStorage>( "BuySellPanel", ( SettingsStorage )null );
                if ( storage == null )
                    return;
                this.BuySellPanel.Load( storage );
            }
            finally
            {
                this._isLoaded = false;
            }
        }


    }
}
