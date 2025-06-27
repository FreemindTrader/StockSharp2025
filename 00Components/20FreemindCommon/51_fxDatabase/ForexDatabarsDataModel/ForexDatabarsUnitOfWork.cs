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
    /// A ForexDatabarsUnitOfWork instance that represents the run-time implementation of the IForexDatabarsUnitOfWork interface.
    /// </summary>
    public class ForexDatabarsUnitOfWork : DbUnitOfWork<ForexDatabars>, IForexDatabarsUnitOfWork
    {
        /// <summary>
        /// The unit of work pattern to create ForexDatabars
        /// </summary>
        /// <param name="contextFactory">() => new ForexDatabars()</param>
        public ForexDatabarsUnitOfWork( Func<ForexDatabars> contextFactory )
            : base( contextFactory )
        {
        }

        

        IRepository<DbSymbolsInfo, long> IForexDatabarsUnitOfWork.SYMBOLSINFO
        {
            get { 
                    return GetRepository(
                                            x => x.Set< DbSymbolsInfo >(),
                                            x => x.Id
                                        ); 
                }              
        }


        IRepository<DbSafetyNetPips, long> IForexDatabarsUnitOfWork.SAFETYNETPIPS
        {
            get
            {
                return GetRepository(
                                            x => x.Set<DbSafetyNetPips>( ),
                                            x => x.Id
                                        );
            }
        }

        IRepository<DbClosedTrade, long> IForexDatabarsUnitOfWork.CLOSEDTRADES
        {
            get { 
                    return GetRepository(
                                            x => x.Set< DbClosedTrade >(),
                                            x => x.Id
                                        ); 
                }              
        }

        IRepository< DetailedOrderDB, long > IForexDatabarsUnitOfWork.ORDERS
        {
            get
            {
                return GetRepository(
                                            x => x.Set< DetailedOrderDB >(),
                                            x => x.Id
                                        ); 
            }
        }

        IRepository<DbElliottWave , long> IForexDatabarsUnitOfWork.ELLIOTTWAVES
        {
            get
            {
                return GetRepository(
                                            x => x.Set<DbElliottWave>( ) ,
                                            x => x.Id
                                        );
            }
        }

        IRepository<DbSmartWaveCycles , long> IForexDatabarsUnitOfWork.SMARTWAVECYCLES
        {
            get
            {
                return GetRepository(
                                            x => x.Set<DbSmartWaveCycles>( ) ,
                                            x => x.Id
                                        );
            }
        }
    

        IRepository<DbBaZi, long> IForexDatabarsUnitOfWork.BAZI
        {
            get
            {
                return GetRepository(
                                            x => x.Set<DbBaZi>( ),
                                            x => x.Id
                                        );
            }
        }

        IRepository<DbAccounts, long> IForexDatabarsUnitOfWork.ACCOUNTS
        {
            get
            {
                return GetRepository(
                                            x => x.Set<DbAccounts>( ),
                                            x => x.Id
                                        );
            }
        }

        IRepository<DBSubscribedSymbols, long> IForexDatabarsUnitOfWork.SUBSCRIBEDSYMBOLS
        {
            get
            {
                return GetRepository(
                                            x => x.Set<DBSubscribedSymbols>( ),
                                            x => x.Id
                                        );
            }
        }

        IRepository<DbForexNews, long> IForexDatabarsUnitOfWork.FOREXNEWS
        {
            get
            {
                return GetRepository(
                                            x => x.Set<DbForexNews>( ),
                                            x => x.Id
                                        );
            }
        }

        IRepository<DbLockedPositions, long> IForexDatabarsUnitOfWork.LOCKEDPOSITIONS
        {
            get
            {
                return GetRepository(
                                            x => x.Set<DbLockedPositions>( ),
                                            x => x.Id
                                        );
            }
        }


    }
}
