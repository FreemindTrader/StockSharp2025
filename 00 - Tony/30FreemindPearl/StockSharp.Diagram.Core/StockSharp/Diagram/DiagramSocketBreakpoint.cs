
using Ecng.Serialization;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace StockSharp.Diagram
{
    /// <summary>
    /// <see cref="T:StockSharp.Diagram.DiagramSocket" /> breakpoint.
    ///     </summary>
    public class DiagramSocketBreakpoint : IPersistable
    {
        
        private DiagramSocket _socket;

        /// <summary>
        /// Initialize <see cref="T:StockSharp.Diagram.DiagramSocketBreakpoint" />.
        /// </summary>
        /// <param name="socket">Diagram socket.</param>
        public DiagramSocketBreakpoint( DiagramSocket socket )
        {
            DiagramSocket diagramSocket = socket;
            if ( diagramSocket == null )
                throw new ArgumentNullException( "diagramSocket == null" );
            Socket = diagramSocket;
        }

        /// <summary>Diagram socket.</summary>
        [Browsable( false )]
        public DiagramSocket Socket
        {
            get
            {
                return _socket;
            }
            set
            {
                _socket = value;
            }
        }

        /// <summary>Whether need to break on socket.</summary>
        /// <returns>Check result.</returns>
        public bool NeedBreak()
        {
            object obj = Socket.Value;
            if ( obj == null )
                return false;
            return OnNeedBreak( obj );
        }

        /// <summary>Whether need to break on socket.</summary>
        /// <param name="value">Current value.</param>
        /// <returns>Check result.</returns>
        protected virtual bool OnNeedBreak( object value )
        {
            return true;
        }

        /// <summary>Load settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public virtual void Load( SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( "Storage == null" );
        }

        /// <summary>Save settings.</summary>
        /// <param name="storage">Settings storage.</param>
        public virtual void Save( SettingsStorage storage )
        {
            if ( storage == null )
                throw new ArgumentNullException( "Storage == null" );
        }
    }
}
