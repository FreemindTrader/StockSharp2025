using System;

namespace fx.Collections.Disposers
{
    /// <summary>
    /// Class returned by UpgradeableLock method.
    /// </summary>
    public struct SpinUpgradeableLock : IUpgradeableLock
    {
        private SpinReaderWriterLock? _lock;
        private bool _upgraded;

        internal SpinUpgradeableLock( SpinReaderWriterLock yieldLock )
        {
            _lock = yieldLock;
            _upgraded = false;
        }

        /// <summary>
        /// Upgrades this lock to a write-lock.
        /// </summary>
        public SpinWriteLock DisposableUpgrade( )
        {
            var yieldLock = _lock;
            if( yieldLock == null )
            {
                throw new ObjectDisposedException( GetType( ).FullName );
            }

            yieldLock._lock.UncheckedUpgradeToWriteLock( );
            return new SpinWriteLock( yieldLock );
        }

        /// <summary>
        /// Upgrades the lock to a write-lock. Returns true if the lock was upgraded, false if it was already upgraded
        /// before.
        /// </summary>
        public bool Upgrade( )
        {
            var yieldLock = _lock;
            if( yieldLock == null )
            {
                throw new ObjectDisposedException( GetType( ).FullName );
            }

            if( _upgraded )
            {
                return false;
            }

            yieldLock._lock.UncheckedUpgradeToWriteLock( );
            _upgraded = true;
            return true;
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

                yieldLock._lock.ExitUpgradeableLock( _upgraded );
            }
        }

        IDisposable IUpgradeableLock.DisposableUpgrade( )
        {
            return DisposableUpgrade( );
        }
    }
}
