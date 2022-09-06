
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace StockSharp.Diagram
{
    /// <summary>
    /// The synchronization object for the composite elements debugger.
    /// </summary>
    public class DebuggerSyncObject : ViewModelBase
    {
        
        private readonly SyncObject _syncObject = new SyncObject();
        
        private readonly Func<DiagramSocket, bool, bool> _diagramSocketFunction;
        
        private readonly Action<DebuggerSyncObject> _waitOnInputEvent;
        
        private readonly Action<DebuggerSyncObject> _errorEvent;
        
        private readonly CompositionDiagramElement _compositionDiagramElement;
        
        private DiagramElement _currentElement;
        
        private bool _setWaitOnNext;
        
        private DiagramSocket _currentSocket;
        
        private Exception _currentError;
        
        private readonly INotifyPropertyChanged _propertyChangedEvent;
        
        private bool _isWaitingOnInput;
        
        private bool _isWaitingOnOutput;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Diagram.DebuggerSyncObject" />.
        /// </summary>
        /// <param name="rootElement">The root diagram element.</param>
        /// <param name="isBreak">The handler that returns a stop flag for the socket.</param>
        /// <param name="breakAction">The action with the element at stop.</param>
        /// <param name="errorAction">The action with the element at error.</param>
        public DebuggerSyncObject(
          CompositionDiagramElement rootElement,
          Func<DiagramSocket, bool, bool> isBreak,
          Action<DebuggerSyncObject> breakAction,
          Action<DebuggerSyncObject> errorAction )
        {
            CompositionDiagramElement compositionDiagramElement = rootElement;
            if ( compositionDiagramElement == null )
                throw new ArgumentNullException( "compositionDiagramElement == null" );
            _compositionDiagramElement = compositionDiagramElement;
            Func<DiagramSocket, bool, bool> func = isBreak;
            if ( func == null )
                throw new ArgumentNullException( "Func<DiagramSocket, bool, bool> isBreak == null" );
            _diagramSocketFunction = func;
            Action<DebuggerSyncObject> action1 = breakAction;
            if ( action1 == null )
                throw new ArgumentNullException( "breakAction == null" );
            _waitOnInputEvent = action1;
            Action<DebuggerSyncObject> action2 = errorAction;
            if ( action2 == null )
                throw new ArgumentNullException( "errorAction == null" );
            _errorEvent = action2;
            _propertyChangedEvent = ( INotifyPropertyChanged )new DebuggerNotifiableObject( this );
        }

        /// <summary>Gui wrapper for property binding.</summary>
        public INotifyPropertyChanged GuiWrapper
        {
            get
            {
                return _propertyChangedEvent;
            }
        }

        /// <summary>The current element.</summary>
        public DiagramElement CurrentElement
        {
            get
            {
                return _currentElement;
            }

            set
            {
                SetField( ref _currentElement, value, nameof( CurrentElement ) );
            }
        }



        /// <summary>The current socket.</summary>
        public DiagramSocket CurrentSocket
        {
            get
            {
                return _currentSocket;
            }

            set
            {
                SetField( ref _currentSocket, value, nameof( CurrentSocket ) );
            }
        }



        /// <summary>The current error.</summary>
        public Exception CurrentError
        {
            get
            {
                return _currentError;
            }

            set
            {
                SetField( ref _currentError, value, nameof( CurrentError ) );
            }
        }



        /// <inheritdoc />
        protected override void DisposeManaged()
        {
            GuiWrapper.DoDispose();
            base.DisposeManaged();
        }

        /// <summary>
        /// <see langword="true" />, if the debugger is stopped at the entry of the diagram element. Otherwise, <see langword="false" />.
        ///     </summary>
        public bool IsWaitingOnInput
        {
            get
            {
                return _isWaitingOnInput;
            }

            set
            {
                _isWaitingOnInput = value;
            }
        }



        /// <summary>
        /// <see langword="true" />, if the debugger is stopped at the exit of the diagram element. Otherwise, <see langword="false" />.
        ///     </summary>
        public bool IsWaitingOnOutput
        {
            get
            {
                return _isWaitingOnOutput;
            }

            set
            {
                _isWaitingOnOutput = value;
            }
        }



        /// <summary>Try wait on socket.</summary>
        /// <param name="socket">
        ///   <see cref="T:StockSharp.Diagram.DiagramSocket" />
        /// </param>
        /// <param name="isOnInput">Is wait on input.</param>
        /// <returns>Operation result.</returns>
        public bool TryWait( DiagramSocket socket, bool isOnInput )
        {
            if ( !_diagramSocketFunction( socket, true ) && !_setWaitOnNext )
            {
                if ( socket.Parent == CurrentElement )
                    CurrentElement = ( ( DiagramElement )null );
                return false;
            }
            try
            {
                socket.IsBreakActive = true;
                CurrentSocket = socket;
                CurrentElement = ( socket.Parent );

                Action<DebuggerSyncObject> inputEvent = _waitOnInputEvent;
                if ( inputEvent != null )
                    inputEvent( this );

                WaitOnInputOrOutput( isOnInput, true );
                _setWaitOnNext = false;
                _syncObject.Wait( new TimeSpan?() );
            }
            finally
            {
                socket.IsBreakActive = false;
                CurrentSocket = null;
                CurrentElement = ( ( DiagramElement )null );
            }
            WaitOnInputOrOutput( isOnInput, false );
            return true;
        }

        /// <summary>Try wait on error.</summary>
        /// <param name="element">
        ///   <see cref="T:StockSharp.Diagram.DiagramElement" />
        /// </param>
        /// <param name="error">Error.</param>
        /// <returns>Operation result.</returns>
        public bool TryWaitOnError( DiagramElement element, Exception error )
        {
            if ( element == _compositionDiagramElement )
                return false;
            CurrentError = error;
            CurrentElement = ( element );
            
            Action<DebuggerSyncObject> errorEvent = _errorEvent;
            if ( errorEvent != null )
                errorEvent( this );


            WaitOnInputOrOutput( true, true );
            _setWaitOnNext = false;
            _syncObject.Wait( new TimeSpan?() );
            CurrentError = null;
            CurrentElement = ( ( DiagramElement )null );
            WaitOnInputOrOutput( true, false );
            return true;
        }

        private void WaitOnInputOrOutput( bool _param1, bool _param2 )
        {
            if ( _param1 )
                IsWaitingOnInput = _param2;
            else
                IsWaitingOnOutput = _param2;
        }

        /// <summary>
        /// To set the flag for waiting at the entry of the next diagram element.
        /// </summary>
        public void SetWaitOnNext()
        {
            _setWaitOnNext = true;
        }

        /// <summary>Continue.</summary>
        public void Continue()
        {
            if ( !IsWaitingOnInput && !IsWaitingOnOutput )
                return;
            _syncObject.Pulse();
        }

        private sealed class DebuggerNotifiableObject : DispatcherNotifiableObject<DebuggerSyncObject>
        {
            public DebuggerNotifiableObject( DebuggerSyncObject _param1 )
              : base( ConfigManager.GetService<IDispatcher>(), _param1 )
            {
            }
        }
    }
}
