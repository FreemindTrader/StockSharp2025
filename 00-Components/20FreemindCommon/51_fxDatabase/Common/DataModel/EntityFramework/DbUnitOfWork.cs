using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace fx.Database.Common.DataModel.EntityFramework
{
    /// <summary>
    /// A DbUnitOfWork instance represents the implementation of the Unit Of Work pattern 
    /// such that it can be used to query from a database and group together changes that will then be written back to the store as a unit. 
    /// </summary>
    /// <typeparam name="TDbContext">DbContext type. In my case, this is the ForexDatabars</typeparam>
    public abstract class DbUnitOfWork< TDbContext > : UnitOfWorkBase, IUnitOfWork where TDbContext : DbContext
    {
        readonly Lazy< TDbContext > context;

        /// <summary>
        /// The Unit of work pattern on Database
        /// </summary>
        /// <param name="contextFactory">() => new ForexDatabars()</param>
        public DbUnitOfWork( Func< TDbContext > contextFactory )
        {
            context = new Lazy< TDbContext >( contextFactory );
        }

        /// <summary>
        /// Instance of underlying DbContext. Lazy initialization occurs the first time the Lazy<T>.Value property is accessed.
        /// </summary>
        public TDbContext Context
        {
            get { return context.Value; }
        }

        void IUnitOfWork.SaveChanges( )
        {
            try
            {                
                Context.SaveChanges( );
            }
            catch( DbUpdateConcurrencyException ex )
            {
                foreach ( var item in ex.Entries  )
                {
                    
                }
            }
            catch ( DbUpdateException ex )
            {
                var error = DbExceptionsConverter.Convert( ex );
            }
        }

        bool IUnitOfWork.HasChanges( )
        {
            return Context.ChangeTracker.HasChanges( );
        }


        /// <summary>
        /// GetRepository
        /// </summary>
        /// <typeparam name="TEntity">EURUSDXX</typeparam>
        /// <typeparam name="TPrimaryKey">TimeSpan</typeparam>
        /// <param name="dbSetAccessor">x => x.Set( EURUSD01 )()</param>
        /// <param name="getPrimaryKeyExpression">x => x.Id</param>
        /// <returns></returns>
        protected IRepository< TEntity, TPrimaryKey >
        GetRepository< TEntity, TPrimaryKey >( 
                                                Func< TDbContext, DbSet< TEntity > >       dbSetAccessor, 
                                                Expression< Func< TEntity, TPrimaryKey > > getPrimaryKeyExpression 
                                             )
                                             where TEntity : class, IFxcm
        {
            return GetRepositoryCore< IRepository< TEntity, TPrimaryKey >, TEntity >( 
                                                                                        () => new DbRepository< TEntity, TPrimaryKey, TDbContext >( this, dbSetAccessor, getPrimaryKeyExpression ) 
                                                                                    );
        }

        protected IReadOnlyRepository< TEntity >
        GetReadOnlyRepository< TEntity >( 
                                            Func< TDbContext, DbSet< TEntity > > dbSetAccessor 
                                        )
                                        where TEntity : class, IFxcm
        {
            return GetRepositoryCore< IReadOnlyRepository< TEntity >, TEntity >( 
                                                                                    () => new DbReadOnlyRepository< TEntity, TDbContext >(this, dbSetAccessor) 
                                                                               );
        }
    }
}