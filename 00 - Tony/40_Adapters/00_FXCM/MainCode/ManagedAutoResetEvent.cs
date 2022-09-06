using System;
using System.Threading;

namespace StockSharp.FxConnectFXCM
{
    public interface IAdvancedDisposable : IDisposable
    {
        /// <summary>
        /// Gets a value indicating if the object was already disposed.
        /// </summary>
        bool WasDisposed { get; }
    }

    public interface IEventWait :
        IDisposable
    {
        /// <summary>
        /// Waits for this event to be signalled.
        /// </summary>
        void WaitOne( );

        /// <summary>
        /// Waits for this event to be signalled or times-out.
        /// Returns if the object was signalled.
        /// </summary>
        bool WaitOne( int millisecondsTimeout );

        /// <summary>
        /// Waits for this event to be signalled or times-out.
        /// Returns if the object was signalled.
        /// </summary>
        bool WaitOne( TimeSpan timeout );

        /// <summary>
        /// Resets (unsignals) this wait event.
        /// </summary>
        void Reset( );

        /// <summary>
        /// Sets (signals) this wait event.
        /// </summary>
        void Set( );
    }

    /// <summary>
	/// An auto reset event that uses only Monitor methods to work, avoiding
	/// operating system events.
	/// </summary>
	public sealed class ManagedAutoResetEvent : IAdvancedDisposable, IEventWait
    {
        private readonly object _lock = new object( );
        private bool _value;
        private bool _wasDisposed;

        /// <summary>
        /// Creates a new event, not signaled.
        /// </summary>
        public ManagedAutoResetEvent( )
        {
        }

        /// <summary>
        /// Creates a new event, letting you say if it starts signaled or not.
        /// </summary>
        public ManagedAutoResetEvent( bool initialState )
        {
            _value = initialState;
        }

        /// <summary>
        /// Disposes this event. After disposing, it is always set.
        /// Calling Reset will not work and it will not throw exceptions, so you can
        /// dispose it when there are threads waiting on it.
        /// </summary>
        public void Dispose( )
        {
            lock ( _lock )
            {
                _wasDisposed = true;
                _value = true;
                Monitor.PulseAll( _lock );
            }
        }

        /// <summary>
        /// Gets a value indicating if this event was disposed.
        /// </summary>
        public bool WasDisposed
        {
            get
            {
                return _wasDisposed;
            }
        }

        /// <summary>
        /// Gets a value indicating if this auto-reset event is set.
        /// </summary>
        public bool IsSet
        {
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// Resets this event (makes it non-signaled).
        /// </summary>
        public void Reset( )
        {
            lock ( _lock )
            {
                if ( _wasDisposed )
                    return;

                _value = false;
            }
        }

        /// <summary>
        /// Signals the event, releasing one thread waiting on it.
        /// </summary>
        public void Set( )
        {
            lock ( _lock )
            {
                _value = true;
                Monitor.Pulse( _lock );
            }
        }

        /// <summary>
        /// Waits until this event is signaled.
        /// </summary>
        public void WaitOne( )
        {
            lock ( _lock )
            {
                while ( !_value )
                    Monitor.Wait( _lock );

                if ( !_wasDisposed )
                    _value = false;
            }
        }

        /// <summary>
        /// Waits until this event is signaled or until the timeout arrives.
        /// Return of true means it was signaled, false means timeout.
        /// </summary>
        public bool WaitOne( int millisecondsTimeout )
        {
            lock ( _lock )
            {
                while ( !_value )
                    if ( !Monitor.Wait( _lock, millisecondsTimeout ) )
                        return false;

                if ( !_wasDisposed )
                    _value = false;
            }

            return true;
        }

        /// <summary>
        /// Waits until this event is signaled or until the timeout arrives.
        /// Return of true means it was signaled, false means timeout.
        /// </summary>
        public bool WaitOne( TimeSpan timeout )
        {
            lock ( _lock )
            {
                while ( !_value )
                    if ( !Monitor.Wait( _lock, timeout ) )
                        return false;

                if ( !_wasDisposed )
                    _value = false;
            }

            return true;
        }
    }
}
