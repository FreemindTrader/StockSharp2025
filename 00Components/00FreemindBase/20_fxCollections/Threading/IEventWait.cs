using System;

namespace fx.Collections
{
    /// <summary>
    /// Interface implemented by PooledEventWait. And, thanks to StructuralCaster, can be used to access custom
    /// EventWait objects.
    /// </summary>
    public interface IEventWait :
        IDisposable
    {
        /// <summary>
        /// Waits for this event to be signalled.
        /// </summary>
        void WaitOne( );

        /// <summary>
        /// Waits for this event to be signalled or times-out. Returns if the object was signalled.
        /// </summary>
        bool WaitOne( int millisecondsTimeout );

        /// <summary>
        /// Waits for this event to be signalled or times-out. Returns if the object was signalled.
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
}
