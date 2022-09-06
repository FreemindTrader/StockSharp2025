
using System;
using System.Threading;

namespace fx.Collections
{
    /// <summary>
    /// A manual reset event that uses only Monitor methods to work, avoiding operating system events.
    /// </summary>
    public sealed class ManagedManualResetEvent :
		IAdvancedDisposable,
		IEventWait
    {
        private readonly object _lock = new object( );
        private bool _value;
        private bool _wasDisposed;

        /// <summary>
        /// Creates a new event, not signaled.
        /// </summary>
        public ManagedManualResetEvent( )
        {
        }

        /// <summary>
        /// Creates a new event, letting you say if it starts signaled or not.
        /// </summary>
        public ManagedManualResetEvent( bool initialState )
        {
            _value = initialState;
        }

        /// <summary>
        /// Disposes this event. After disposing, it is always set. Calling Reset will not work and it will not throw
        /// exceptions, so you can dispose it when there are threads waiting on it.
        /// </summary>
        public void Dispose( )
        {
            lock( _lock )
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
            lock( _lock )
            {
                if( _wasDisposed )
                {
                    return;
                }

                _value = false;
            }
        }

        /// <summary>
        /// Signals the event, releasing any threads waiting on it.
        /// </summary>
        public void Set( )
        {
            lock( _lock )
            {
                _value = true;
                Monitor.PulseAll( _lock );
            }
        }

        /// <summary>
        /// Waits until this event is signaled.
        /// </summary>
        public void WaitOne( )
        {
            lock( _lock )
            {
                while( !_value )
                {
                    Monitor.Wait( _lock );
                }
            }
        }

        /// <summary>
        /// Waits until this event is signaled or until the timeout arrives. Return of true means it was signaled, false
        /// means timeout.
        /// </summary>
        public bool WaitOne( int millisecondsTimeout )
        {
            lock( _lock )
            {
                while( !_value )
                {
                    if( !Monitor.Wait( _lock, millisecondsTimeout ) )
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Waits until this event is signaled or until the timeout arrives. Return of true means it was signaled, false
        /// means timeout.
        /// </summary>
        public bool WaitOne( TimeSpan timeout )
        {
            lock( _lock )
            {
                while( !_value )
                {
                    if( !Monitor.Wait( _lock, timeout ) )
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
