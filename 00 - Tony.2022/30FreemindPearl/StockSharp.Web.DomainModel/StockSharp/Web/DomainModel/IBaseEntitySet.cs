
using System;

namespace StockSharp.Web.DomainModel
{
    public interface IBaseEntitySet
    {
        int Count { get; }

        Array Items { get; }
    }
}
