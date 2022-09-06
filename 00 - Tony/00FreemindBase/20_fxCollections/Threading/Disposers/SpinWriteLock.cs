using System;

namespace fx.Collections.Disposers
{
    /// <summary>
    /// Class returned by WriteLock method.
    /// </summary>
    public struct SpinWriteLock :
		IDisposable
    {
        private SpinReaderWriterLock? _lock;

        internal SpinWriteLock( SpinReaderWriterLock yieldLock )
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
                _lock = null!;
                yieldLock._lock.ExitWriteLock( );
            }
        }
    }
}
