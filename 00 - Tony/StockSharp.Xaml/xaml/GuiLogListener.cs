using Ecng.Xaml;
using StockSharp.Logging;
using System;
using System.Collections.Generic;

namespace StockSharp.Xaml
{
    public class GuiLogListener : LogListener
    {
        private readonly GuiDispatcher _dispatcher = GuiDispatcher.GlobalDispatcher;
        private readonly ILogListener _listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiLogListener"/>.
        /// </summary>
        /// <param name="listener">The visual component that requires synchronization with GUI threads when new messages are recorded <see cref="LogMessage"/>.</param>
        public GuiLogListener( ILogListener listener )
        {
            if ( listener == null )
                throw new ArgumentNullException( nameof( listener ) );

            _listener = listener;
        }

        /// <summary>
        /// To record messages.
        /// </summary>
        /// <param name="messages">Debug messages.</param>
        protected override void OnWriteMessages( IEnumerable<LogMessage> messages )
        {
            _dispatcher.AddAction( ( ) => _listener.WriteMessages( messages ) );
        }
    }
}
