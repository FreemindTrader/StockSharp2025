using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;

namespace StockSharp.Xaml.GridControl
{
    public interface IPriceChartDataProvider : IDisposable
    {
        ICollection<Tuple<DateTime, Decimal>> Get( Security security, Level1Fields field );
    }
}
