using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Algo.History
{
    public interface ISecurityDownloader
    {
        void Refresh(
      ISecurityStorage securityStorage,
      Security criteria,
      Action< Security > newSecurity,
      Func< bool > isCancelled );
    }
}
