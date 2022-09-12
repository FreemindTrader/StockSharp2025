using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using MoreLinq;
using StockSharp.Messages;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class ConnectorSupportedMessagesPanel : ListBox
    {
        private class SupportedMessage
        {
            private readonly ConnectorSupportedMessagesPanel _parent;
            private readonly IMessageAdapter _adapter;

            public SupportedMessage( ConnectorSupportedMessagesPanel parent, IMessageAdapter adapter, MessageTypes type )
            {
                _parent = parent;
                _adapter = adapter;
                Type = type;
                Name = type.GetDisplayName( );
            }

            public MessageTypes Type { get; }
            public string Name { get; private set; }

            public bool IsSelected
            {
                get { return _adapter.IsMessageSupported( Type ); }
                set
                {
                    if ( value )
                        _adapter.AddSupportedMessage( Type );
                    else
                        _adapter.RemoveSupportedMessage( Type );

                    if ( _parent.SelectedChanged  != null )
                    {
                        _parent.SelectedChanged.Invoke( );
                    }
                    
                }
            }
        }

        private readonly ObservableCollection<SupportedMessage> _supportedMessages = new ObservableCollection<SupportedMessage>();
        private IMessageAdapter _adapter;

        internal event Action SelectedChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorSupportedMessagesPanel"/>.
        /// </summary>
        public ConnectorSupportedMessagesPanel( )
        {
            InitializeComponent( );

            ItemsSource = _supportedMessages;
        }

        /// <summary>
		/// The message adapter.
		/// </summary>
		public IMessageAdapter Adapter
        {
            get { return _adapter; }
            set
            {
                if ( _adapter == value )
                    return;

                _adapter = value;

                _supportedMessages.Clear( );

                if ( _adapter == null )
                    return;

                var message = _adapter.GetType()
                    .CreateInstance<IMessageAdapter>(_adapter.TransactionIdGenerator)
                    .SupportedMessages
                    .Select(m => new SupportedMessage(this, _adapter, m))
                    .ToArray();

                _supportedMessages.AddRange( message );
            }
        }
    }
}
