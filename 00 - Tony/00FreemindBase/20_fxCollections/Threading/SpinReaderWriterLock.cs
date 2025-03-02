using fx.Collections.Disposers;
using System;

namespace fx.Collections
{
    /// <summary>
    /// A reader writer lock that uses SpinWait if it does not have the lock. If the locks are held for to much time,
    /// this is CPU consuming. In my general tests, it is about 20 times faster than ReaderWriterLockSlim class and two
    /// times faster than the YieldReaderWriterLock.
    /// </summary>
    public sealed class SpinReaderWriterLock :
		IReaderWriterLock
    {
        #region Fields
        internal SpinReaderWriterLockSlim _lock;
		#endregion

        #region ReadLock
        /// <summary>
        /// Acquires a read lock that must be used in a using clause.
        /// </summary>
        public SpinReadLock ReadLock( )
        {
            _lock.EnterReadLock( );
            return new SpinReadLock( this );
        }
		#endregion

        #region UpgradeableLock
        /// <summary>
        /// Acquires a upgradeable read lock that must be used in a using clause.
        /// </summary>
        public SpinUpgradeableLock UpgradeableLock( )
        {
            _lock.EnterUpgradeableLock( );
            return new SpinUpgradeableLock( this );
        }
		#endregion

        #region WriteLock
        /// <summary>
        /// Acquires a write lock that must be used in a using clause. If you are using a UpgradeableLock use the
        /// Upgrade method of the YieldUpgradeableLock instead or you will cause a dead-lock.
        /// </summary>
        public SpinWriteLock WriteLock( )
        {
            _lock.EnterWriteLock( );
            return new SpinWriteLock( this );
        }
		#endregion

        #region IReaderWriterLock Members
        IDisposable IReaderWriterLock.ReadLock( )
        {
            return ReadLock( );
        }

        IUpgradeableLock IReaderWriterLock.UpgradeableLock( )
        {
            return UpgradeableLock( );
        }

        IDisposable IReaderWriterLock.WriteLock( )
        {
            return WriteLock( );
        }
        #endregion
    }
}
