using Ecng.Collections;
using Ecng.Common;
using StockSharp.BusinessEntities;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class MyTradeGrid : BaseGridControl, IComponentConnector
    {
        private readonly GridScheduledExecutorService<MyTrade, MyTradePropertyChangeHandler> _myTradeItems;        

        public MyTradeGrid( )
        {
            InitializeComponent( );
                        
            _myTradeItems = new GridScheduledExecutorService<MyTrade, MyTradePropertyChangeHandler>( this, ( t, h ) => new MyTradePropertyChangeHandler( t, h ) ) { MaxCount = 10000 };
        }

        public int MaxCount
        {
            get
            {
                return _myTradeItems.MaxCount;
            }
            set
            {
                _myTradeItems.MaxCount = value;
            }
        }

        public IListEx<MyTrade> Trades
        {
            get
            {
                return ( IListEx<MyTrade> )_myTradeItems.Source;
            }
        }

        public MyTrade SelectedTrade
        {
            get
            {
                return SelectedTrades.FirstOrDefault( );
            }
        }

        public IEnumerable<MyTrade> SelectedTrades
        {
            get
            {
                return SelectedItems.Cast<MyTrade>( );
            }
        }

        

        private sealed class MyTradePropertyChangeHandler : MyTrade, IModelUpdate
        {
            private readonly SyncObject _lock = new SyncObject( );
            private readonly Action<MyTradePropertyChangeHandler> _myTradePropertyChangedHandler;
            private bool _hasChanges;
            private readonly MyTrade _myTrade;

            public MyTradePropertyChangeHandler( MyTrade myTrade, Action<MyTradePropertyChangeHandler> handler )
            {
                if ( myTrade == null )
                {
                    throw new ArgumentNullException( "myTrade" );
                }

                _myTrade = myTrade;

                if ( handler == null )
                {
                    throw new ArgumentNullException( "onChanged" );
                }

                _myTradePropertyChangedHandler = handler;
                MyTrade.PropertyChanged += new PropertyChangedEventHandler( OnPropertyChanged );
                Update( );
            }

            public MyTrade MyTrade
            {
                get
                {
                    return _myTrade;
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
                Order              = MyTrade.Order;
                Trade              = MyTrade.Trade;
                Commission         = MyTrade.Commission;
                CommissionCurrency = MyTrade.CommissionCurrency;
                Position           = MyTrade.Position;
                Slippage           = MyTrade.Slippage;
                PnL                = MyTrade.PnL;
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

                _myTradePropertyChangedHandler( this );
            }
        }        
    }
}
