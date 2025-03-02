using System;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Generic;
using fx.Database.Common.Utils;
using fx.Database.Common.DataModel;
using fx.Database.Common.DataModel.EntityFramework;
using fx.Database;
using DevExpress.Mvvm;

namespace fx.Database.ForexDatabarsDataModel
{
    /// <summary>
    /// Provides methods to obtain the relevant IUnitOfWorkFactory.
    /// </summary>
    public static class UnitOfWorkSource
    {

        #region inner classes

        class DbUnitOfWorkFactory : IUnitOfWorkFactory< IForexDatabarsUnitOfWork >
        {
            public static readonly IUnitOfWorkFactory< IForexDatabarsUnitOfWork > Instance = new DbUnitOfWorkFactory();

            DbUnitOfWorkFactory( )
            {
            }

            IForexDatabarsUnitOfWork IUnitOfWorkFactory<IForexDatabarsUnitOfWork>.CreateUnitOfWork( )
            {
                return new ForexDatabarsUnitOfWork(() => new ForexDatabars());
            }
        }

        //class DesignUnitOfWorkFactory : IUnitOfWorkFactory< IForexDatabarsUnitOfWork >
        //{
        //    public static readonly IUnitOfWorkFactory< IForexDatabarsUnitOfWork > Instance = new DesignUnitOfWorkFactory();

        //    DesignUnitOfWorkFactory( )
        //    {
        //    }

        //    IForexDatabarsUnitOfWork IUnitOfWorkFactory<IForexDatabarsUnitOfWork>.CreateUnitOfWork( )
        //    {
        //        return new ForexDatabarsDesignTimeUnitOfWork();
        //    }
        //}

        #endregion

        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the current mode (run-time or design-time).
        /// </summary>
        public static IUnitOfWorkFactory< IForexDatabarsUnitOfWork > GetUnitOfWorkFactory( )
        {
            return DbUnitOfWorkFactory.Instance;
        }

        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation based on the given mode (run-time or design-time).
        /// </summary>
        /// <param name="isInDesignTime">Used to determine which implementation of IUnitOfWorkFactory should be returned.</param>
        //public static IUnitOfWorkFactory< IForexDatabarsUnitOfWork > GetUnitOfWorkFactory( bool isInDesignTime )
        //{
        //    return isInDesignTime ? DesignUnitOfWorkFactory.Instance : DbUnitOfWorkFactory.Instance;
        //}
    }
}