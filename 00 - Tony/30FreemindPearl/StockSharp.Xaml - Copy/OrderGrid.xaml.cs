using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using MoreLinq;
using Ecng.Common;
using StockSharp.Algo;

namespace StockSharp.Xaml
{
    public partial class OrderGrid : BaseGridControl, IComponentConnector
    {
        public static RoutedCommand RegisterOrderCommand   = new RoutedCommand( );
        public static RoutedCommand ReRegisterOrderCommand = new RoutedCommand( );
        public static RoutedCommand CancelOrderCommand     = new RoutedCommand( );
        public static RoutedCommand CopyErrorCommand       = new RoutedCommand( );

        private readonly GridScheduledExecutorService<Order, OrderChangeHandler> _orderItems;
        private bool _isInteractive;        

        public OrderGrid( )
        {
            InitializeComponent( );
            IsInteractive = true;

            // Tony: Fix XAML CommandBindings Error in XAML 
            // I moved the CommandBinding here so the blue lines in XAML are gone. Hate to see those.
            var registerOrderCommand   = new CommandBinding( RegisterOrderCommand  , ExecuteRegisterOrderCommand  , CanExecuteRegisterOrderCommand );
            var reRegisterOrderCommand = new CommandBinding( ReRegisterOrderCommand, ExecuteReRegisterOrderCommand, CanExecuteReRegisterOrderCommand );
            var cancelOrderCommand     = new CommandBinding( CancelOrderCommand    , ExecuteCancelOrderCommand    , CanExecuteCancelOrderCommand );
            var copyErrorCommand       = new CommandBinding( CopyErrorCommand      , ExecuteCopyErrorCommand );

            this.CommandBindings.Add( registerOrderCommand );
            this.CommandBindings.Add( reRegisterOrderCommand );
            this.CommandBindings.Add( cancelOrderCommand );
            this.CommandBindings.Add( copyErrorCommand );


            _orderItems = new GridScheduledExecutorService<Order, OrderChangeHandler>( this, ( o, h ) => new OrderChangeHandler( o, h ) ) { MaxCount = 100000 };

            ContextMenu.Items.Add( new Separator( ) );
            ContextMenu.Items.Add( new MenuItem { Header = LocalizedStrings.Str1566,    Command = RegisterOrderCommand,   CommandTarget = this } );
            ContextMenu.Items.Add( new MenuItem { Header = LocalizedStrings.XamlStr174, Command = ReRegisterOrderCommand, CommandTarget = this } );
            ContextMenu.Items.Add( new MenuItem { Header = LocalizedStrings.XamlStr421, Command = CancelOrderCommand,     CommandTarget = this } );
        }

        public bool IsInteractive
        {
            get
            {
                return _isInteractive;
            }
            set
            {
                _isInteractive = value;
            }
        }

        public int MaxCount
        {
            get
            {
                return _orderItems.MaxCount;
            }
            set
            {
                _orderItems.MaxCount = value;
            }
        }

        public IListEx<Order> Orders
        {
            get
            {
                return _orderItems.Source;
            }
        }

        public void AddRegistrationFail( OrderFail fail )
        {
            if ( fail == null )
            {
                throw new ArgumentNullException( nameof( fail ) );
            }

            var item = _orderItems.Source.TryGet( fail.Order );

            if ( item == null )
            {
                return;
            }

            item.CommentString = fail.Error.Message;
        }

        public Order SelectedOrder
        {
            get
            {
                return SelectedOrders.FirstOrDefault( );
            }
        }

        public IEnumerable<Order> SelectedOrders
        {
            get
            {
                return SelectedItems.Cast<OrderChangeHandler>( ).Select( h => h.Order ).ToArray( );
            }
        }

        public event Action SelectedOrderChanged;

        public event Action OrderRegistering;

        public event Action<Order> OrderReRegistering;

        public event Action<Order> OrderCanceling;

        public event Action<IEnumerable<Order>> OrdersCanceling;

        protected virtual void OnOrderAdded( Order order )
        {
        }

        protected override void RaiseSelectionChanged( GridSelectionChangedEventArgs e )
        {
            base.RaiseSelectionChanged( e );

            SelectedOrderChanged?.Invoke( );
        }

        private void ExecuteRegisterOrderCommand( object sender, ExecutedRoutedEventArgs e )
        {
            OrderRegistering?.Invoke( );
        }

        private void CanExecuteRegisterOrderCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = IsInteractive && OrderRegistering != null;
        }

        private void ExecuteReRegisterOrderCommand( object sender, ExecutedRoutedEventArgs e )
        {
            OrderReRegistering?.Invoke( SelectedOrder );
        }

        private void CanExecuteReRegisterOrderCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = IsInteractive && OrderReRegistering != null && SelectedOrder != null && SelectedOrder.State == OrderStates.Active;
        }



        private void ExecuteCancelOrderCommand( object sender, ExecutedRoutedEventArgs e )
        {
            Action<IEnumerable<Order>> ordersCancelingEvent = OrdersCanceling;

            if ( ordersCancelingEvent != null )
            {
                ordersCancelingEvent( SelectedOrders.Where( o =>
                {
                    if ( o == null )
                    {
                        throw new ArgumentNullException( "order" );
                    }

                    if ( o.State != OrderStates.Active )
                    {
                        return o.State == OrderStates.Pending;
                    }

                    return true;

                } ) );
            }

            Action<Order> orderCancelingEvent = OrderCanceling;
            if ( orderCancelingEvent == null )
            {
                return;
            }

            SelectedOrders.ForEach( orderCancelingEvent );
        }

        private void CanExecuteCancelOrderCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = IsInteractive && OrderCanceling != null && SelectedOrders.Any( o => {
                if ( o == null )
                {
                    throw new ArgumentNullException( "order" );
                }

                if ( o.State != OrderStates.Active )
                {
                    return o.State == OrderStates.Pending;
                }

                return true;
            } );
        }

        private void ExecuteCopyErrorCommand( object sender, ExecutedRoutedEventArgs e )
        {
            e.Parameter.CopyToClipboard( );
            e.Handled = true;
        }

       

        private sealed class OrderChangeHandler : Order, IModelUpdate
        {
            private readonly SyncObject _lock = new SyncObject( );
            private readonly Action<OrderChangeHandler> _orderPropertyChangedHandler;
            private bool _hasChanges;
            private Order _order;
            private string _commentString;
            private string _conditionString;
            private string _orderId;
            private OrderEnum _orderState;
            private long _oderTif;

            public OrderChangeHandler( Order myOrder, Action<OrderChangeHandler> handler )
            {

                if ( myOrder == null )
                {
                    throw new ArgumentNullException( "order" );
                }

                Order = myOrder;

                if ( handler == null )
                {
                    throw new ArgumentNullException( "onChanged" );
                }

                _orderPropertyChangedHandler = handler;
                Order.PropertyChanged += new PropertyChangedEventHandler( OnPropertyChanged );
                Update( );
            }

            public Order Order
            {
                get
                {
                    return _order;
                }
                set
                {
                    _order = value;
                }
            }

            public string CommentString
            {
                get
                {
                    return _commentString;
                }
                set
                {
                    _commentString = value;
                    NotifyChanged( nameof( CommentString ) );
                }
            }

            public string ConditionString
            {
                get
                {
                    return _conditionString;
                }
                set
                {
                    _conditionString = value;
                    NotifyChanged( nameof( ConditionString ) );
                }
            }

            public string OrderId
            {
                get
                {
                    return _orderId;
                }
                set
                {
                    if ( _orderId == value )
                    {
                        return;
                    }

                    _orderId = value;
                    NotifyChanged( nameof( OrderId ) );
                }
            }

            public OrderEnum OrderState
            {
                get
                {
                    return _orderState;
                }
                set
                {
                    if ( _orderState == value )
                    {
                        return;
                    }

                    _orderState = value;
                    NotifyChanged( nameof( OrderState ) );
                }
            }

            public long OrderTif
            {
                get
                {
                    return _oderTif;
                }
                set
                {
                    if ( _oderTif == value )
                    {
                        return;
                    }

                    _oderTif = value;
                    NotifyChanged( nameof( OrderTif ) );
                }
            }

            public void UpdateModel( )
            {
                lock ( _lock )
                {
                    if ( !_hasChanges )
                    {
                        return;
                    }

                    _hasChanges = false;
                }
                Update( );
            }

            private void Update( )
            {
                Type                = Order.Type;
                Id                  = Order.Id;
                LocalTime           = Order.LocalTime;
                Portfolio           = Order.Portfolio;
                State               = Order.State;
                TransactionId       = Order.TransactionId;
                LastChangeTime      = Order.LastChangeTime;
                Balance             = Order.Balance;
                BoardId             = Order.BoardId;
                BrokerCode          = Order.BrokerCode;
                ClientCode          = Order.ClientCode;
                Comment             = Order.Comment;
                Commission          = Order.Commission;
                Condition           = Order.Condition;
                Currency            = Order.Currency;
                Direction           = Order.Direction;
                ExpiryDate          = Order.ExpiryDate;
                ExtensionInfo       = Order.ExtensionInfo != null ? new Dictionary<string, object>( Order.ExtensionInfo ) : null;
                IsMargin            = Order.IsMargin;
                IsMarketMaker       = Order.IsMarketMaker;
                IsSystem            = Order.IsSystem;
                LatencyCancellation = Order.LatencyCancellation;
                LatencyRegistration = Order.LatencyRegistration;
                Price               = Order.Price;
                RepoInfo            = Order.RepoInfo;
                RpsInfo             = Order.RpsInfo;
                Security            = Order.Security;
                Slippage            = Order.Slippage;
                Status              = Order.Status;
                StringId            = Order.StringId;
                Time                = Order.Time;
                TimeInForce         = Order.TimeInForce;
                UserOrderId         = Order.UserOrderId;
                VisibleVolume       = Order.VisibleVolume;
                Volume              = Order.Volume;
                OrderId             = GetOrderID( );
                OrderState          = GetOrderState( );
                OrderTif            = GetOrderTif( );
                ConditionString     = GetOrderConditionString( );
            }

            private void OnPropertyChanged( object sender, PropertyChangedEventArgs e )
            {
                bool hasChanges;
                lock ( _lock )
                {
                    hasChanges = _hasChanges;
                    _hasChanges = true;
                }
                if ( hasChanges )
                {
                    return;
                }

                _orderPropertyChangedHandler( this );
            }

            private string GetOrderID( )
            {
                Order order = Order;
                if ( order == null )
                {
                    return null;
                }

                return order.Id.To<string>( ) ?? order.StringId;
            }

            private OrderEnum GetOrderState( )
            {
                Order order = Order;
                if ( order == null )
                {
                    return OrderEnum.None;
                }

                switch ( order.State )
                {
                    case OrderStates.None:
                        return OrderEnum.None;
                    case OrderStates.Active:
                        return OrderEnum.Active;
                    case OrderStates.Done:
                        return !order.IsMatched( ) ? OrderEnum.Cancelled : OrderEnum.Matched;
                    case OrderStates.Failed:
                        return OrderEnum.Failed;
                    case OrderStates.Pending:
                        return OrderEnum.Pending;
                    default:
                        throw new InvalidOperationException( LocalizedStrings.Str1596Params.Put( order.State, order ) );
                }
            }

            private long GetOrderTif( )
            {
                Order order = Order;
                if ( order == null )
                {
                    return -1;
                }

                TimeInForce? timeInForce = order.TimeInForce;
                DateTimeOffset? expiryDate = order.ExpiryDate;
                if ( timeInForce.HasValue )
                {
                    switch ( timeInForce.GetValueOrDefault( ) )
                    {
                        case StockSharp.Messages.TimeInForce.PutInQueue:
                            break;

                        case StockSharp.Messages.TimeInForce.MatchOrCancel:
                            return 0;

                        case StockSharp.Messages.TimeInForce.CancelBalance:
                            return 1;

                        default:
                            throw new ArgumentOutOfRangeException( );
                    }
                }
                if ( !expiryDate.HasValue )
                {
                    return long.MaxValue;
                }

                return expiryDate.GetValueOrDefault( ).To<long>( );
            }

            private string GetOrderConditionString( )
            {
                if ( Order.Condition != null )
                {
                    return Order.Condition.Parameters.Select( kp => string.Format( "{0} = {1}", kp.Key, kp.Value ) ).Join( Environment.NewLine );
                }

                return string.Empty;
            }            
        }

        
    }
}
