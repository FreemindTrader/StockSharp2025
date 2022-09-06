
using System;

namespace StockSharp.Web.DomainModel
{
    [Flags]
    public enum ProductOrderFlags
    {
        None = 0,
        Trial = 1,
        Refund = 2,
        Test = 4,
        Cancelled = 8,
    }
}
