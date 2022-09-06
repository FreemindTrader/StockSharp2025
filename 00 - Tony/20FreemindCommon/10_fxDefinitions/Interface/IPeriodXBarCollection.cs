using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public delegate void IPeriodCollection_Updated( IPeriodXBarCollection collection, int index );

    /// <summary>
    /// Interface to the collection of the price periods
    /// </summary>
    public interface IPeriodXBarCollection : IEnumerable< IAskBidBar >, IDisposable
    {
        /// <summary>
        /// The number of periods in the collection
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Get the period by its index
        /// </summary>
        /// <param name="index">The index of the period. The oldest period has index 0.</param>
        /// <returns></returns>
        IAskBidBar this[ int index ] { get; }

        /// <summary>
        /// The event raised when the collection is updated with a new tick
        /// </summary>
        event IPeriodCollection_Updated OnCollectionUpdate;

        /// <summary>
        /// Gets the instrument name of the collection
        /// </summary>
        string Instrument { get; }

        /// <summary>
        /// Gets the timeframe name of the collection
        /// </summary>
        string Timeframe { get; }

        /// <summary>
        /// Gets flag indicating that the collection is alive (i.e. is updated when a new price coming)
        /// </summary>
        bool IsAlive { get; }
    }
}
