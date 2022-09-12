using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Xaml;
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
    public partial class TradeGrid : BaseGridControl
    {
        private readonly ThreadSafeObservableCollection<Trade> _trades;

        public TradeGrid( )
        {
            InitializeComponent();

            var itemsSource = new ObservableCollectionEx<Trade>();
            ItemsSource = itemsSource;

            _trades = new ThreadSafeObservableCollection<Trade>( itemsSource ) { MaxCount = 1000000 };            
        }

        public int MaxCount
        {
            get
            {
                return _trades.MaxCount;
            }
            set
            {
                _trades.MaxCount = value;
            }
        }

        public IListEx<Trade> Trades
        {
            get
            {
                return _trades;
            }
        }

        public Trade SelectedTrade
        {
            get
            {
                return SelectedTrades.FirstOrDefault<Trade>();
            }
        }

        public IEnumerable<Trade> SelectedTrades
        {
            get
            {
                return SelectedItems.Cast<Trade>();
            }
        }
    }
}
