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
    public partial class OrderLogGrid : BaseGridControl
    {
        private readonly ThreadSafeObservableCollection<OrderLogItem> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderLogGrid"/>.
        /// </summary>
        public OrderLogGrid( )
        {
            InitializeComponent();

            var itemsSource = new ObservableCollectionEx<OrderLogItem>();
            ItemsSource = itemsSource;

            _items = new ThreadSafeObservableCollection<OrderLogItem>( itemsSource ) { MaxCount = 100000 };
        }

        /// <summary>
        /// The maximum number of rows to display. The -1 value means an unlimited amount. The default value is 100000.
        /// </summary>
        public int MaxCount
        {
            get { return _items.MaxCount; }
            set { _items.MaxCount = value; }
        }

        /// <summary>
        /// Order log.
        /// </summary>
        public IListEx<OrderLogItem> LogItems => _items;

        /// <summary>
        /// The selected row.
        /// </summary>
        public OrderLogItem SelectedLogItem => SelectedLogItems.FirstOrDefault();

        /// <summary>
        /// Selected rows.
        /// </summary>
        public IEnumerable<OrderLogItem> SelectedLogItems => SelectedItems.Cast<OrderLogItem>();
    }
}

