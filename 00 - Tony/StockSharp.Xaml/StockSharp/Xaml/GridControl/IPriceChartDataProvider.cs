using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;

namespace StockSharp.Xaml.GridControl
{
    /// <summary>Price chart data provider interface.</summary>
    public interface IPriceChartDataProvider : IDisposable
    {
        /// <summary>
        /// Get changes for <see cref="T:StockSharp.BusinessEntities.Security" />.
        /// </summary>
        /// <param name="security">Security.</param>
        /// <param name="field">Level1 field.</param>
        /// <returns>Price items collection.</returns>
        ICollection<Tuple<DateTime, Decimal>> Get(
          Security security,
          Level1Fields field );
    }
}
