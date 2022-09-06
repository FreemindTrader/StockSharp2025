using System;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Generic;
using fx.Database.Common.Utils;
using fx.Database.Common.DataModel;
using fx.Database.Common.DataModel.EntityFramework;
using fx.Database;

namespace fx.Database.ForexDatabarsDataModel
{
    /// <summary>
    /// IForexDatabarsUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
    /// </summary>
    public interface IForexDatabarsUnitOfWork : IUnitOfWork
    {        
        IRepository <DbSymbolsInfo , long> SYMBOLSINFO       { get; }

        IRepository<DbSafetyNetPips, long> SAFETYNETPIPS           { get; }

        IRepository <DbClosedTrade , long> CLOSEDTRADES            { get; }

        IRepository < DetailedOrderDB, long > ORDERS               { get; }

        IRepository <DbElliottWave , long> ELLIOTTWAVES            { get; }

        IRepository< DbSmartWaveCycles , long> SMARTWAVECYCLES     { get; }

        IRepository<DbAccounts, long> ACCOUNTS                     { get; }

        IRepository<DbBaZi, long> BAZI { get; }

        IRepository<DBSubscribedSymbols, long> SUBSCRIBEDSYMBOLS   { get; }

        IRepository<DbForexNews, long> FOREXNEWS                 { get; }

        IRepository<DbLockedPositions, long> LOCKEDPOSITIONS { get; }
    }
}
