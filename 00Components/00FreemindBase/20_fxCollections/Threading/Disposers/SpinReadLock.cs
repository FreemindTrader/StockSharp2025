using System;

namespace fx.Collections.Disposers
{
    /// <summary>
    /// Class returned by ReadLock method.
    /// </summary>
    public struct SpinReadLock : IDisposable
    {
        private SpinReaderWriterLock? _lock;

        internal SpinReadLock( SpinReaderWriterLock yieldLock )
        {
            _lock = yieldLock;
        }

        /// <summary>
        /// Releases the lock.
        /// </summary>
        public void Dispose( )
        {
            var yieldLock = _lock;

            if( yieldLock != null )
            {
                _lock = null;
                yieldLock._lock.ExitReadLock( );
            }
        }
    }
}