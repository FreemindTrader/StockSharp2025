using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Xaml;
using StockSharp.Messages;
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
    public partial class ExecutionGrid : BaseGridControl
    {
        private readonly ThreadSafeObservableCollection<ExecutionMessage> _messages;
        public ExecutionGrid( )
        {
            InitializeComponent();

            var itemsSource = new ObservableCollectionEx<ExecutionMessage>();
            ItemsSource = itemsSource;

            _messages = new ThreadSafeObservableCollection<ExecutionMessage>( itemsSource ) { MaxCount = 1000000 };
        }

        /// <summary>
		/// The maximum number of rows to display. The -1 value means an unlimited amount. The default is 1000000.
		/// </summary>
		public int MaxCount
        {
            get { return _messages.MaxCount; }
            set { _messages.MaxCount = value; }
        }

        /// <summary>
		/// The list of messages added to the table.
		/// </summary>
		public IListEx<ExecutionMessage> Messages => _messages;

        /// <summary>
        /// The selected message.
        /// </summary>
        public ExecutionMessage SelectedMessage => SelectedMessages.FirstOrDefault();

        /// <summary>
		/// Selected messages.
		/// </summary>
		public IEnumerable<ExecutionMessage> SelectedMessages => SelectedItems.Cast<ExecutionMessage>();

        public void HideColumns( ExecutionTypes type )
        {            
            if ( type != ExecutionTypes.Tick )
            {
                if ( type != ExecutionTypes.OrderLog )
                    throw new ArgumentOutOfRangeException( nameof( type ) );

                TradeVolumeColumn.Visible =  false ;
            }
            else
            {
                OrderPriceColumn.Visible  = false;
                OrderVolumeColumn.Visible = false;
                BalanceColumn.Visible     = false;
                OrderSideColumn.Visible   = false;
                OrderTypeColumn.Visible   = false;
                OrderStateColumn.Visible  = false;
                OriginSideColumn.Visible  = false;
                TradeIdColumn.Visible     = false;
                TradePriceColumn.Visible  = false;               
            }
        }
    }
}
