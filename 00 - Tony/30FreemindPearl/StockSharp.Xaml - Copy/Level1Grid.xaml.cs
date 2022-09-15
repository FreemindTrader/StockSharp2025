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
    public partial class Level1Grid : BaseGridControl
    {
        private readonly ThreadSafeObservableCollection<Level1ChangeMessage> _messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="Level1Grid"/>.
        /// </summary>
        public Level1Grid( )
        {
            InitializeComponent();

            var itemsSource = new ObservableCollectionEx<Level1ChangeMessage>();
            ItemsSource = itemsSource;

            _messages = new ThreadSafeObservableCollection<Level1ChangeMessage>( itemsSource ) { MaxCount = 10000 };
        }

        /// <summary>
        /// The maximum number of messages to display. The -1 value means an unlimited amount. The default value is 10000.
        /// </summary>
        public int MaxCount
        {
            get { return _messages.MaxCount; }
            set { _messages.MaxCount = value; }
        }

        /// <summary>
        /// The list of messages added to the table.
        /// </summary>
        public IListEx<Level1ChangeMessage> Messages => _messages;

        /// <summary>
        /// The selected message.
        /// </summary>
        public Level1ChangeMessage SelectedMessage => SelectedMessages.FirstOrDefault();

        /// <summary>
        /// Selected messages.
        /// </summary>
        public IEnumerable<Level1ChangeMessage> SelectedMessages => SelectedItems.Cast<Level1ChangeMessage>();
    }
}
