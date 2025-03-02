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
                return BuySellPanel.Settings;
            }
        }

        public ScalpingMarketDepthControl()
        {
            DelayActionHelper delayActionHelper = new DelayActionHelper() { Interval = TimeSpan.FromSeconds( 2.5 ).TotalMilliseconds };
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            BuySellPanel.Host = this;
            Settings.ValueChanged += ( name, _1, _2 ) =>
              {
                  if ( name == "Security" || name == "Depth" )
                      delayActionHelper.Start( new Action( Resubscribe ) );
                  this.GuiAsync( new Action( UpdateTitle ) );
                  if ( _isLoaded )
                      return;
                  RaiseChangedCommand();
              };
            MdControl.LayoutChanged += RaiseChangedCommand;
            MdControl.RegisteringOrder += new Action<Sides, Decimal>( OnRegisteringOrder );
            MdControl.MovingOrder += new Action<Order, Decimal>( OnMovingOrder );
            MdControl.CancelingOrder += new Action<Order>( OnCancelingOrder );
            MdControl.SelectedQuoteChanged += new Action<QuoteChange?>( OnSelectedQuoteChanged );
            IStudioCommandService commandService = CommandService;
            commandService.Register<EntityCommand<QuoteChangeMessage>>( this, false, cmd =>
                {
                    QuoteChangeMessage entity = cmd.Entity;
                    Security security = Settings.Security;
                    if ( security == null || !( entity.SecurityId == security.ToSecurityId( null, true, false ) ) )
                        return;
                    MdControl.UpdateDepth( entity, security );
                }, null );
            commandService.Register<ClearMarketDepthCommand>( this, true, cmd =>
                {
                    MdControl.Clear();
                    if ( cmd.Security == null || cmd.Security == Settings.Security )
                        return;
                    Settings.Security = cmd.Security;
                }, null );
            commandService.Register<OrderCommand>( this, false, cmd =>
                {
                    Order entity = cmd.Entity;
                    OrderTypes? type = entity.Type;
                    OrderTypes orderTypes = OrderTypes.Conditional;
                    if ( type.GetValueOrDefault() == orderTypes & type.HasValue || entity.Security != Settings.Security )
                        return;
                    MdControl.ProcessOrder( entity, cmd.Price, cmd.Balance, cmd.State );
                }, null );
            commandService.Register<OrderFailCommand>( this, false, cmd => MdControl.ProcessOrderFail( cmd.Entity, cmd.State ), null );
            commandService.Register<SecuritiesRemovedCommand>( this, false, cmd =>
                {
                    bool flag = false;
                    foreach ( Security security in cmd.Securities )
                    {
                        if ( Settings.Security == security )
                        {
                            Settings.Security = null;
                            MdControl.Clear();
                            flag = true;
                            break;
                        }
                    }
                    if ( !flag )
                        return;
                    RaiseChangedCommand();
                }, null );
            commandService.Register<FirstInitSecuritiesCommand>( this, true, cmd =>
                {
                    if ( Settings.Security != null )
                        return;
                    Settings.Security = cmd.Securities.First();
                }, null );
            commandService.Register<FirstInitPortfoliosCommand>( this, true, cmd =>
                {
                    if ( Settings.Portfolio != null )
                        return;
                    Settings.Portfolio = cmd.Portfolios.First();
                }, null );
            WhenLoaded( () => _subscriptionManager.CreateSubscription( DataType.Transactions, null ) );
        }

        public override void SendCommand( IStudioCommand command )
        {
            if ( command is CancelAllOrdersCommand )
                MdControl.CancelOrders( null );
            else
                base.SendCommand( command );
        }

        private void OnSelectedQuoteChanged( QuoteChange? quote )
        {
            if ( !quote.HasValue )
                return;
            BuySellPanel.Settings.LimitPrice = quote.Value.Price;
        }

        private void OnCancelingOrder( Order order )
        {
            new CancelOrderCommand( order ).Process( this, false );
        }

        private void OnMovingOrder( Order order, Decimal newPrice )
        {
            Order order1 = order.ReRegisterClone( new Decimal?( newPrice ), new Decimal?() );
            bool? nullable = Connector.IsOrderReplaceable( order );
            bool flag = true;
            if ( nullable.GetValueOrDefault() == flag & nullable.HasValue )
            {
                new ReRegisterOrderCommand( order, order1 ).Process( this, false );
            }
            else
            {
                new CancelOrderCommand( order ).Process( this, false );
                new RegisterOrderCommand( order1 ).Process( this, false );
            }
        }

        private void Resubscribe()
        {
            if ( _subscription != null )
            {
                _subscriptionManager.RemoveSubscription( _subscription );
                _subscription = null;
            }
            MdControl.Clear();
            _subscriptionManager.MaxDepth = new int?( Math.Max( 1, Math.Min( 200, Settings.Depth ) ) );
            Security security = Settings.Security;
            if ( security == null )
                return;
            _subscription = _subscriptionManager.CreateSubscription( security, DataType.MarketDepth, null );
        }

        private void OnRegisteringOrder( Sides side, Decimal price )
        {
            if ( Settings.Portfolio == null )
            {
                PortfolioPickerWindow wnd = new PortfolioPickerWindow() { Portfolios = PortfolioDataSource };
                if ( wnd.ShowModal( this ) )
                    Settings.Portfolio = wnd.SelectedPortfolio;
            }
            if ( Settings.Portfolio == null )
                return;
            new RegisterOrderCommand( CreateOrder( side, price, OrderTypes.Limit ) ).Process( this, false );
        }

        private void UpdateTitle()
        {
            Title = Settings.Security != null ? Settings.Security.Id : LocalizedStrings.MarketDepth;
        }

        private Order CreateOrder( Sides direction, Decimal price = 0M, OrderTypes type = OrderTypes.Market )
        {
            return new Order() { Portfolio = Settings.Portfolio, Security = Settings.Security, Direction = direction, Price = price, Volume = Settings.Volume, Type = new OrderTypes?( type ) };
        }

        public override void Dispose()
        {
            IStudioCommandService commandService = CommandService;
            MdControl.RegisteringOrder -= new Action<Sides, Decimal>( OnRegisteringOrder );
            MdControl.MovingOrder -= new Action<Order, Decimal>( OnMovingOrder );
            MdControl.CancelingOrder -= new Action<Order>( OnCancelingOrder );
            MdControl.SelectedQuoteChanged -= new Action<QuoteChange?>( OnSelectedQuoteChanged );
            commandService.UnRegister<EntityCommand<QuoteChangeMessage>>( this );
            commandService.UnRegister<ClearMarketDepthCommand>( this );
            commandService.UnRegister<OrderCommand>( this );
            commandService.UnRegister<ResetedCommand>( this );
            commandService.UnRegister<SecuritiesRemovedCommand>( this );
            commandService.UnRegister<FirstInitSecuritiesCommand>( this );
            commandService.UnRegister<FirstInitPortfoliosCommand>( this );
            _subscriptionManager.Dispose();
            base.Dispose();
        }

        public override void Save( SettingsStorage settings )
        {
            base.Save( settings );
            settings.SetValue( "MdControl", MdControl.Save() );
            settings.SetValue( "BuySellPanel", BuySellPanel.Save() );
        }

        public override void Load( SettingsStorage settings )
        {
            _isLoaded = true;
            try
            {
                base.Load( settings );
                MdControl.Load( settings.GetValue<SettingsStorage>( "MdControl", null ) );
                SettingsStorage storage = settings.GetValue<SettingsStorage>( "BuySellPanel", null );
                if ( storage == null )
                    return;
                BuySellPanel.Load( storage );
            }
            finally
            {
                _isLoaded = false;
            }
        }


    }
}
