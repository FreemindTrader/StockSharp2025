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
    public partial class PositionChangeGrid : BaseGridControl
    {
        private readonly ThreadSafeObservableCollection<PositionChangeMessage> _positionChangeMsg;

        public PositionChangeGrid()
        {
            InitializeComponent();

            var itemsSource = new ObservableCollectionEx<PositionChangeMessage>();
            ItemsSource = itemsSource;

            _positionChangeMsg = new ThreadSafeObservableCollection<PositionChangeMessage>( itemsSource ) { MaxCount = 10000 };
        }

        public int MaxCount
        {
            get
            {
                return _positionChangeMsg.MaxCount;
            }
            set
            {
                _positionChangeMsg.MaxCount =  value;
            }
        }

        public IListEx<PositionChangeMessage> Messages
        {
            get
            {
                return _positionChangeMsg;
            }
        }

        public PositionChangeMessage SelectedMessage
        {
            get
            {
                return this.SelectedMessages.FirstOrDefault();
            }
        }

        public IEnumerable<PositionChangeMessage> SelectedMessages
        {
            get
            {
                return SelectedItems.Cast<PositionChangeMessage>();
            }
        }
    }
}
