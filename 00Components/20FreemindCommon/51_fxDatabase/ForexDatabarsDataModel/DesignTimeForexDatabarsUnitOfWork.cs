//using System;
//using System.Linq;
//using System.Data;
//using System.Data.Entity;
//using System.Linq.Expressions;
//using System.Collections.Generic;
//using fx.Database.Common.Utils;
//using fx.Database.Common.DataModel;
//using fx.Database.Common.DataModel.EntityFramework;
//using fx.Database;

//namespace fx.Database.ForexDatabarsDataModel
//{
//    /// <summary>
//    /// A ForexDatabarsDesignTimeUnitOfWork instance that represents the design-time implementation of the IForexDatabarsUnitOfWork interface.
//    /// </summary>
//    public class ForexDatabarsDesignTimeUnitOfWork : DesignTimeUnitOfWork, IForexDatabarsUnitOfWork
//    {

//        /// <summary>
//        /// Initializes a new instance of the ForexDatabarsDesignTimeUnitOfWork class.
//        /// </summary>
//        public ForexDatabarsDesignTimeUnitOfWork()
//        {
//        }

//        
//        IRepository< DbSymbolsInfo, long > IForexDatabarsUnitOfWork.SYMBOLSINFO
//        {
//            get
//            {
//                return GetRepository( (DbSymbolsInfo x) => x.Id );
//            }
//        }

//        IRepository<DbSafetyNetPips, long> IForexDatabarsUnitOfWork.SAFETYNETPIPS
//        {
//            get
//            {
//                return GetRepository( ( DbSafetyNetPips x ) => x.Id );
//            }
//        }

//        IRepository< DbClosedTrade, long > IForexDatabarsUnitOfWork.CLOSEDTRADES
//        {
//            get
//            {
//                return GetRepository( (DbClosedTrade x) => x.Id );
//            }
//        }

//        IRepository< DetailedOrderDB, long > IForexDatabarsUnitOfWork.ORDERS
//        {
//            get
//            {
//                return GetRepository( ( DetailedOrderDB x ) => x.Id );
//            }
//        }

//        IRepository<DbElliottWave , long> IForexDatabarsUnitOfWork.ELLIOTTWAVES
//        {
//            get
//            {
//                return GetRepository( ( DbElliottWave x ) => x.Id );
//            }
//        }

//        IRepository<DbSmartWaveCycles , long> IForexDatabarsUnitOfWork.SMARTWAVECYCLES
//        {
//            get
//            {
//                return GetRepository( ( DbSmartWaveCycles x ) => x.Id );
//            }
//        }

//        IRepository<DbAccounts, long> IForexDatabarsUnitOfWork.ACCOUNTS
//        {
//            get
//            {
//                return GetRepository( ( DbAccounts x ) => x.Id );
//            }
//        }

//        IRepository<DbBaZi, long> IForexDatabarsUnitOfWork.BAZI
//        {
//            get
//            {
//                return GetRepository( ( DbBaZi x ) => x.Id );
//            }
//        }

//        IRepository<DBSubscribedSymbols, long> IForexDatabarsUnitOfWork.SUBSCRIBEDSYMBOLS
//        {
//            get
//            {
//                return GetRepository( ( DBSubscribedSymbols x ) => x.Id );
//            }
//        }


//        IRepository<DbForexNews, long> IForexDatabarsUnitOfWork.FOREXNEWS
//        {
//            get
//            {
//                return GetRepository( ( DbForexNews x ) => x.Id );
//            }
//        }

//        IRepository<DbLockedPositions, long> IForexDatabarsUnitOfWork.LOCKEDPOSITIONS
//        {
//            get
//            {
//                return GetRepository(
//                                            ( DbLockedPositions x ) => x.Id
//                                        );
//            }
//        }

//    }
//}
