
using System;
using System.Threading;

namespace fx.Collections
{
    /// <summary>
    /// A semaphore class that uses only Monitor class for synchronization, avoiding operating system events.
    /// </summary>
    public sealed class ManagedSemaphore : IAdvancedDisposable
    {
        private readonly object _lock = new object( );
        private int _availableCount;

        /// <summary>
        /// Creates a new semaphore with the given availableCount.
        /// </summary>
        public ManagedSemaphore( int availableCount )
        {
            if( availableCount < 1 )
            {
                throw new ArgumentException( "availableCount must be at least 1.", "availableCount" );
            }

            _availableCount = availableCount;
        }

        /// <summary>
        /// Disposes this semaphore. If you try to enter or exit it after this, the action will always return
        /// immediately.
        /// </summary>
        public void Dispose( )
        {
            lock( _lock )
            {
                _availableCount = -1;
                Monitor.PulseAll( _lock );
            }
        }

        /// <summary>
        /// Gets a value indicating if this semaphore was disposed.
        /// </summary>
        public bool WasDisposed
        {
            get
            {
                return _availableCount == -1;
            }
        }

        /// <summary>
        /// Enters the actual semaphore.
        /// </summary>
        public void Enter( )
        {
            lock( _lock )
            {
                while( true )
                {
                    if( _availableCount == -1 )
                    {
                        return;
                    }

                    if( _availableCount > 0 )
                    {
                        _availableCount--;
                        return;
                    }

                    Monitor.Wait( _lock );
                }
            }
        }

        /// <summary>
        /// Enters the actual semaphore with the given count value. If you pass a value higher than the one used to
        /// create it, you will dead-lock (at least until the semaphore is disposed).
        /// </summary>
        public void Enter( int count )
        {
            if( count <= 0 )
            {
                throw new ArgumentException( "count of semaphores to enter must be at least 1.", "count" );
            }

            lock( _lock )
            {
                while( true )
                {
                    if( _availableCount == -1 )
                    {
                        return;
                    }

                    if( _availableCount >= count )
                    {
                        _availableCount -= count;
                        return;
                    }

                    Monitor.Wait( _lock );
                }
            }
        }

        /// <summary>
        /// Exits the semaphore. One thread can enter it and another one exit it. There is no check for that.
        /// </summary>
        public void Exit( )
        {
            lock( _lock )
            {
                if( _availableCount == -1 )
                {
                    return;
                }

                _availableCount++;
                Monitor.Pulse( _lock );
            }
        }

        /// <summary>
        /// Exits the semaphore the given amount. One thread can enter it and another one exit it. There is no check for
        /// that.
        /// </summary>
        public void Exit( int count )
        {
            if( count <= 0 )
            {
                throw new ArgumentException( "count of semaphores to exit must be at least 1.", "count" );
            }

            if( count == -1 )
            {
                Exit( );
                return;
            }

            lock( _lock )
            {
                if( _availableCount == -1 )
                {
                    return;
                }

                _availableCount += count;
                Monitor.PulseAll( _lock );
            }
        }
    }
}
