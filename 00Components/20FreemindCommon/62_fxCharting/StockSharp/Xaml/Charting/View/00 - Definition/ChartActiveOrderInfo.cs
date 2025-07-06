using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;


#pragma warning disable CA1416

namespace StockSharp.Xaml.Charting
{
    public class ChartActiveOrderInfo : ViewModelBase, IPersistable
    {
        private double _chartX = 0.8;
        private bool _autoRemoveFromChart = true;
        private Order _order;
        private OrderStates _orderStates;
        private Sides sides_0;
        private Decimal _volume;
        private Decimal _filledVolume;
        private Decimal _price;
        private Decimal _priceStep;
        private bool _isFrozen;
        private ActiveOrdersUI _activeOrders;

        public event Action< ChartActiveOrderInfo, bool, bool > OrderStateChanged;

        public Order Order
        {
            get
            {
                return _order;
            }
            private set
            {
                SetField( ref _order, value, nameof( Order ) );
            }
        }

        public OrderStates State
        {
            get
            {
                return _orderStates;
            }
            set
            {
                SetField( ref _orderStates, value, nameof( State ) );
            }
        }

        public Sides Direction
        {
            get
            {
                return sides_0;
            }
            set
            {
                SetField( ref sides_0, value, nameof( Direction ) );
            }
        }

        public Decimal Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                SetField( ref _volume, value, nameof( Volume ) );
            }
        }

        public Decimal FilledVolume
        {
            get
            {
                return _filledVolume;
            }
            set
            {
                SetField( ref _filledVolume, value, nameof( FilledVolume ) );
            }
        }

        public Decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                SetField( ref _price, value, nameof( Price ) );
            }
        }

        public Decimal PriceStep
        {
            get
            {
                return _priceStep;
            }
            set
            {
                SetField( ref _priceStep, value, nameof( PriceStep ) );
            }
        }

        public double ChartX
        {
            get
            {
                return _chartX;
            }
            set
            {
                if( value < 0.0 || value > 1.0 )
                {
                    throw new ArgumentOutOfRangeException( nameof( ChartX ) );
                }
                SetField( ref _chartX, value, nameof( ChartX ) );
            }
        }

        public bool AutoRemoveFromChart
        {
            get
            {
                return _autoRemoveFromChart;
            }
            set
            {
                SetField( ref _autoRemoveFromChart, value, nameof( AutoRemoveFromChart ) );
            }
        }

        public bool IsFrozen
        {
            get
            {
                return _isFrozen;
            }
            set
            {
                SetField( ref _isFrozen, value, nameof( IsFrozen ) );
            }
        }

        public ActiveOrdersUI Element
        {
            get
            {
                return _activeOrders;
            }
            internal set
            {
                _activeOrders = value;
            }
        }

        public void UpdateOrderState( Order order, bool isError = false, bool isFrozen = false )
        {
            if( order == null )
            {
                if( Order == null )
                {
                    return;
                }
                State = OrderStates.None;
                Decimal num = new Decimal( );
                Price = num;
                Volume = FilledVolume = num;
                IsFrozen = isFrozen;
                Order = null;
                Action< ChartActiveOrderInfo, bool, bool > action0 = OrderStateChanged;
                if( action0 == null )
                {
                    return;
                }
                action0( this, false, isError );
            }
            else
            {
                Decimal num = order.Volume - order.Balance;
                bool flag = Order == order && FilledVolume != num;
                State = order.State;
                Direction = order.Direction;
                Volume = order.Volume;
                FilledVolume = num;
                Price = order.Price;
                PriceStep =   ( order.Security?.PriceStep ) ?? Decimal.Zero;
                IsFrozen = isFrozen;
                Order = order;
                Action< ChartActiveOrderInfo, bool, bool > action0 = OrderStateChanged;
                if( action0 == null )
                {
                    return;
                }
                action0( this, flag, isError );
            }
        }

        public void Load( SettingsStorage storage )
        {
            State = storage.GetValue( "State", ( OrderStates )0 );
            Direction = storage.GetValue( "Direction", ( Sides )0 );
            Volume = storage.GetValue( "Volume", new Decimal( ) );
            FilledVolume = storage.GetValue( "FilledVolume", new Decimal( ) );
            Price = storage.GetValue( "Price", new Decimal( ) );
            PriceStep = storage.GetValue( "PriceStep", new Decimal( ) );
            ChartX = storage.GetValue( "ChartX", 0.0 );
            AutoRemoveFromChart = storage.GetValue( "AutoRemoveFromChart", false );
            IsFrozen = storage.GetValue( "IsFrozen", false );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue( "State", State );
            storage.SetValue( "Direction", Direction );
            storage.SetValue( "Volume", Volume );
            storage.SetValue( "FilledVolume", FilledVolume );
            storage.SetValue( "Price", Price );
            storage.SetValue( "PriceStep", PriceStep );
            storage.SetValue( "ChartX", ChartX );
            storage.SetValue( "AutoRemoveFromChart", AutoRemoveFromChart );
            storage.SetValue( "IsFrozen", IsFrozen );
        }
    }
}
