
using System;

namespace StockSharp.Web.DomainModel
{
    [Flags]
    public enum ProductFlags
    {
        None = 0,
        IsApproved = 1,
        IsCatalog = 2,
        MoneyReserved = 4,
    }
}
