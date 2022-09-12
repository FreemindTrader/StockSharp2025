using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Strategies;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;

namespace StockSharp.Xaml
{
    public class StrategiesDashboardItem : NotifiableObject
    {
        private readonly ThreadSafeObservableCollection<Tuple<DateTime, Decimal>> _items;
        private string                                                            _name;
        private Strategy                                                          _strategy;
        private object                                                            _tag;
        private int                                                               _orderCount;
        private int                                                               _tradesCount;
        private Decimal                                                           _pnlChange;
        private Decimal                                                           _pnl;
        private readonly IList<Tuple<DateTime, Decimal>>                          _pnlValues;
        private string                                                            _error;

        public StrategiesDashboardItem( )
        {
            
            ObservableCollectionEx<Tuple<DateTime, Decimal>> collection = new ObservableCollectionEx<Tuple<DateTime, Decimal>>();
            _pnlValues = ( IList<Tuple<DateTime, Decimal>> ) collection;
            _items = new ThreadSafeObservableCollection<Tuple<DateTime, Decimal>>( ( IListEx<Tuple<DateTime, Decimal>> ) collection );
        }

        public StrategiesDashboardItem( string name, Strategy strategy, object tag )
          : this()
        {
            if ( StringHelper.IsEmpty( name ) )
            {
                throw new ArgumentNullException( nameof( name ) );
            }

            Name = name;
            Strategy strategy1 = strategy;
            if ( strategy1 == null )
            {
                throw new ArgumentNullException( nameof( strategy ) );
            }

            Strategy = strategy1;
            Tag = tag;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            protected set
            {
                if ( _name == value )
                {
                    return;
                }

                _name = value;
                NotifyChanged( nameof( Name ) );
            }
        }

        public Strategy Strategy
        {
            get
            {
                return _strategy;
            }
            protected set
            {
                if ( _strategy == value )
                {
                    return;
                }

                if ( _strategy != null )
                {
                    Strategy.ProcessStateChanged -= OnProcessStateChanged;
                    Strategy.OrderRegistered     -= OnOrderRegistered;
                    Strategy.NewMyTrade          -= OnNewMyTrade;
                    Strategy.PnLChanged          -= OnPnLChanged;
                    Strategy.Error               -= OnError;
                    Strategy.PropertyChanged     -= OnPropertyChanged;
                }
                _strategy = value;
                NotifyChanged( nameof( Strategy ) );
                if ( _strategy == null )
                {
                    return;
                }

                Strategy.ProcessStateChanged += OnProcessStateChanged;
                Strategy.OrderRegistered += OnOrderRegistered;
                Strategy.NewMyTrade += OnNewMyTrade;
                Strategy.PnLChanged += OnPnLChanged;
                Strategy.Error += OnError;
                Strategy.PropertyChanged += OnPropertyChanged;
            }
        }

        public object Tag
        {
            get
            {
                return _tag;
            }
            protected set
            {
                _tag = value;
            }
        }

        public ImageSource ProcessStateIcon
        {
            get
            {
                return ( ImageSource ) IconsExtension.GetImage( Strategy.GetStrategyProcessStateIconName() );
            }
        }

        private void OnProcessStateChanged( Strategy strategy_1 )
        {
            NotifyPropertyChangedExHelper.Notify<StrategiesDashboardItem>(  this, "ProcessState" );
            NotifyPropertyChangedExHelper.Notify<StrategiesDashboardItem>(  this, "ProcessStateIcon" );
        }

        private void OnOrderRegistered( Order order_0 )
        {
            ++OrdersCount;
        }

        private void OnNewMyTrade( MyTrade myTrade_0 )
        {
            ++TradesCount;
        }

        private void OnPnLChanged( )
        {
            Decimal pnL = Strategy.PnL;
            if ( _pnl != pnL )
            {
                _items.Add( Tuple.Create<DateTime, Decimal>( DateTime.Now, pnL ) );
            }

            _pnlChange = pnL - _pnl;
            _pnl = pnL;
        }

        private void OnError( Strategy strategy_1, Exception exception_0 )
        {
            Error = exception_0.Message;
        }

        private void OnPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            if ( !StringHelper.CompareIgnoreCase( e.PropertyName, "AllowTrading" ) )
            {
                return;
            }

            NotifyPropertyChangedExHelper.Notify<StrategiesDashboardItem>(  this, "AllowTrading" );
        }

        [Browsable( false )]
        public int OrdersCount
        {
            get
            {
                return _orderCount;
            }
            set
            {
                if ( _orderCount == value )
                {
                    return;
                }

                _orderCount = value;
                NotifyPropertyChangedExHelper.Notify<StrategiesDashboardItem>(  this, nameof( OrdersCount ) );
            }
        }

        [Browsable( false )]
        public int TradesCount
        {
            get
            {
                return _tradesCount;
            }
            set
            {
                if ( _tradesCount == value )
                {
                    return;
                }

                _tradesCount = value;
                NotifyPropertyChangedExHelper.Notify<StrategiesDashboardItem>(  this, nameof( TradesCount ) );
            }
        }

        [Browsable( false )]
        public Decimal PnLChange
        {
            get
            {
                return _pnlChange;
            }
            set
            {
                if ( _pnlChange == value )
                {
                    return;
                }

                _pnlChange = value;
                NotifyPropertyChangedExHelper.Notify<StrategiesDashboardItem>(  this, nameof( PnLChange ) );
            }
        }

        [Browsable( false )]
        public IList<Tuple<DateTime, Decimal>> PnLValues
        {
            get
            {
                return _pnlValues;
            }
        }

        [Browsable( false )]
        public virtual ProcessStates ProcessState
        {
            get
            {
                return Strategy.ProcessState;
            }
        }

        [Browsable( false )]
        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                if ( _error == value )
                {
                    return;
                }

                _error = value;
                NotifyPropertyChangedExHelper.Notify<StrategiesDashboardItem>( this, nameof( Error ) );
            }
        }

        [Browsable( false )]
        public bool AllowTrading
        {
            get
            {
                Strategy strategy = Strategy;
                if ( strategy == null )
                {
                    return false;
                }

                return StrategyHelper.GetAllowTrading( strategy );
            }
            set
            {
                Strategy strategy = Strategy;
                if ( strategy != null )
                {
                    StrategyHelper.SetAllowTrading( strategy, value );
                }

                NotifyPropertyChangedExHelper.Notify<StrategiesDashboardItem>(  this, nameof( AllowTrading ) );
            }
        }
    }
}