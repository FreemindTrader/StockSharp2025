using System;
using System.Linq;
using System.Data;

using System.Linq.Expressions;
using System.Collections.Generic;
using fx.Database.Common.Utils;
using fx.Database.Common.DataModel;

using System.Threading.Tasks;


//#if ( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP3_0 || NETCOREAPP3_1 )
//using Microsoft.EntityFrameworkCore;
//#else
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.Entity.Migrations;
//#endif

namespace fx.Database.Common.DataModel.EntityFramework
{
    /// <summary>
    /// A DbRepository is a IRepository interface implementation representing the collection of all entities in the unit of work, or that can be queried from the database, of a given type. 
    /// DbRepository objects are created from a DbUnitOfWork using the GetRepository method. 
    /// DbRepository provides only write operations against entities of a given type in addition to the read-only operation provided DbReadOnlyRepository base class.
    /// </summary>
    /// <typeparam name="TEntity">Repository entity type.</typeparam>
    /// <typeparam name="TPrimaryKey">Entity primary key type.</typeparam>
    /// <typeparam name="TDbContext">DbContext type.</typeparam>
    public class DbRepository< TEntity, TPrimaryKey, TDbContext > : DbReadOnlyRepository< TEntity, TDbContext >, IRepository< TEntity, TPrimaryKey >
    where TEntity    : class, IFxcm
    where TDbContext : DbContext
    {
        readonly Expression< Func< TEntity, TPrimaryKey > > getPrimaryKeyExpression;

        readonly EntityTraits< TEntity, TPrimaryKey >       entityTraits;

        /// <summary>
        /// Initializes a new instance of DbRepository class.
        /// </summary>
        /// <param name="unitOfWork">Owner unit of work that provides context for repository entities.</param>
        /// <param name="dbSetAccessor">Function that returns DbSet entities from Entity Framework DbContext.</param>
        /// <param name="getPrimaryKeyExpression">Lambda-expression that returns entity primary key.</param>
        public DbRepository( DbUnitOfWork< TDbContext > unitOfWork, Func< TDbContext, DbSet< TEntity > > dbSetAccessor, Expression< Func< TEntity, TPrimaryKey > > getPrimaryKeyExpression )
            : base(
                        unitOfWork, 
                        dbSetAccessor
                  )
        {
            this.getPrimaryKeyExpression = getPrimaryKeyExpression;
            entityTraits            = ExpressionHelper.GetEntityTraits( this, getPrimaryKeyExpression );
        }

        /// <summary>
        /// DbSet.Add() method attaches the entire entity graph to the new context and automatically applies Added entity state to all the entities.
        /// </summary>
        
        protected virtual TEntity CreateCore( )
        {
            TEntity newEntity = DbSet.Create( );


            /// <image url="$(SolutionDir)\..\..\30 - CommonImages\DbSetAdd.jpg"/>
            DbSet.Add( newEntity );

            return newEntity;
        }

        /// <summary>
        /// DbSet.Attach method attaches a whole entity graph to the new context with Unchanged entity state. And we manually set it to Modified State
        /// </summary>
        protected virtual void AttachCore( TEntity entity )
        {
            /// <image url="$(SolutionDir)\..\..\30 - CommonImages\DbSetAttach.jpg"/>
            DbSet.Attach( entity );
            
            Context.Entry( entity ).State = System.Data.Entity.EntityState.Modified;
        }

        protected virtual void DetachCore( TEntity entity )
        {
            Context.Entry( entity ).State = System.Data.Entity.EntityState.Detached;
        }

        protected virtual void AddRangeCore( IList< TEntity > entityList )
        {
            //DbSet.AddRange(  entityList ); 

           foreach( var entity in entityList )
           {
                DbSet.Add( entity );
           }
            

            //DbSet.AddOrUpdate<TEntity>( , entityList.ToArray( ) );

        }

        protected virtual TEntity FindLatestCore( )
        {
            // http://stackoverflow.com/questions/2876616/returning-ienumerablet-vs-iqueryablet

            //IQueryable< TEntity > result = from StartDate in DbSet orderby StartDate descending select StartDate;  

            var result = ( from e in DbSet orderby e.StartDate descending select e ).FirstOrDefault( );            
            
            return result;
        }

        protected virtual TEntity FindOldestCore( )
        {
            // http://stackoverflow.com/questions/2876616/returning-ienumerablet-vs-iqueryablet

            // IQueryable< TEntity > result = from StartDate in DbSet orderby StartDate ascending select StartDate;            

            var result = ( from e in DbSet orderby e.StartDate ascending select e ).FirstOrDefault( );                        

            return result;
        }

        protected virtual void UpdateCore( TEntity entity )
        {
        }

        protected virtual EntityState GetStateCore( TEntity entity )
        {
            return GetEntityState( Context.Entry( entity ).State );
        }


//#if ( NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP3_0 || NETCOREAPP3_1 )
///// <summary>
//        /// http://www.entityframeworktutorial.net/EntityFramework5/update-entity-graph-using-dbcontext.aspx
//        /// </summary>
//        /// <param name="entityStates"></param>
//        /// <returns></returns>
//        static EntityState GetEntityState( Microsoft.EntityFrameworkCore.EntityState entityStates )
//        {
//            switch ( entityStates )
//            {
//                case Microsoft.EntityFrameworkCore.EntityState.Added:
//                    return EntityState.Added;

//                case Microsoft.EntityFrameworkCore.EntityState.Deleted:
//                    return EntityState.Deleted;

//                case Microsoft.EntityFrameworkCore.EntityState.Detached:
//                    return EntityState.Detached;

//                case Microsoft.EntityFrameworkCore.EntityState.Modified:
//                    return EntityState.Modified;

//                case Microsoft.EntityFrameworkCore.EntityState.Unchanged:
//                    return EntityState.Unchanged;

//                default:
//                    throw new NotImplementedException();
//            }
//        }
//#else
        /// <summary>
        /// http://www.entityframeworktutorial.net/EntityFramework5/update-entity-graph-using-dbcontext.aspx
        /// </summary>
        /// <param name="entityStates"></param>
        /// <returns></returns>
        static EntityState GetEntityState( System.Data.Entity.EntityState entityStates )
        {
            switch ( entityStates )
            {
                case System.Data.Entity.EntityState.Added:
                    return EntityState.Added;

                case System.Data.Entity.EntityState.Deleted:
                    return EntityState.Deleted;

                case System.Data.Entity.EntityState.Detached:
                    return EntityState.Detached;

                case System.Data.Entity.EntityState.Modified:
                    return EntityState.Modified;

                case System.Data.Entity.EntityState.Unchanged:
                    return EntityState.Unchanged;

                default:
                    throw new NotImplementedException( );
            }
        }
//#endif




        protected virtual TEntity FindCore( TPrimaryKey key )
        {
            /// <image url="$(SolutionDir)\..\..\30 - CommonImages\DbSetFind.jpg"/>
            return DbSet.Find( key );
        }

        protected virtual void RemoveCore( TEntity entity )
        {
            try
            {
                /// <image url="$(SolutionDir)\..\..\30 - CommonImages\DbSetRemove.jpg"/>
                DbSet.Remove( entity );
            }
            //catch ( DbEntityValidationException ex )
            //{
            //    throw DbExceptionsConverter.Convert( ex );
            //}
            catch ( DbUpdateException ex )
            {
                throw DbExceptionsConverter.Convert( ex );
            }
        }

        protected virtual TEntity FindByStartDateCore( long mystartDate )
        {
            return ( from e in DbSet where e.StartDate == mystartDate select e ).SingleOrDefault( );            
        }

        protected virtual TEntity ReloadCore( TEntity entity )
        {
            Context.Entry( entity ).Reload( );

            return FindCore( GetPrimaryKeyCore( entity ) );
        }

        protected virtual TPrimaryKey GetPrimaryKeyCore( TEntity entity )
        {
            return entityTraits.GetPrimaryKey( entity );
        }

        protected virtual void SetPrimaryKeyCore( TEntity entity, TPrimaryKey key )
        {
            var setPrimaryKeyaction = entityTraits.SetPrimaryKey;
            setPrimaryKeyaction( entity, key );
        }

        
        #region IRepository

        TEntity IRepository<TEntity, TPrimaryKey>.Find( TPrimaryKey key )
        {
            return FindCore( key );
        }

        void IRepository<TEntity, TPrimaryKey>.ChangeStateToModified( TEntity entity )
        {
            Context.Entry( entity ).State = System.Data.Entity.EntityState.Modified;
        }


        void IRepository<TEntity, TPrimaryKey>.Remove( TEntity entity )
        {
            RemoveCore( entity );
        }

        TEntity IRepository<TEntity, TPrimaryKey>.FindByStartDate( long startDate )
        {
            return FindByStartDateCore( startDate );
        }

        TEntity IRepository<TEntity, TPrimaryKey>.FindLatest( )
        {
            return FindLatestCore( );
        }

        TEntity IRepository<TEntity, TPrimaryKey>.FindOldest( )
        {
            return FindOldestCore( );
        }

        TEntity IRepository<TEntity, TPrimaryKey>.Create( )
        {
            return CreateCore( );
        }

        void IRepository<TEntity, TPrimaryKey>.Attach( TEntity entity )
        {
            AttachCore( entity );
        }

        void IRepository<TEntity, TPrimaryKey>.Detach( TEntity entity )
        {
            DetachCore( entity );
        }

        void IRepository<TEntity, TPrimaryKey>.AddRange( IList< TEntity > entityList )
        {
            AddRangeCore( entityList );
        }

        void IRepository<TEntity, TPrimaryKey>.Update( TEntity entity )
        {
            UpdateCore( entity );
        }

        EntityState IRepository<TEntity, TPrimaryKey>.GetState( TEntity entity )
        {
            return GetStateCore( entity );
        }

        TEntity IRepository<TEntity, TPrimaryKey>.Reload( TEntity entity )
        {
            return ReloadCore( entity );
        }

        Expression< Func< TEntity, TPrimaryKey > > IRepository< TEntity, TPrimaryKey >.GetPrimaryKeyExpression
        {
            get { return getPrimaryKeyExpression; }
        }

        void IRepository<TEntity, TPrimaryKey>.SetPrimaryKey( TEntity entity, TPrimaryKey key )
        {
            SetPrimaryKeyCore( entity, key );
        }

        TPrimaryKey IRepository<TEntity, TPrimaryKey>.GetPrimaryKey( TEntity entity )
        {
            return GetPrimaryKeyCore( entity );
        }

        bool IRepository<TEntity, TPrimaryKey>.HasPrimaryKey( TEntity entity )
        {
            return entityTraits.HasPrimaryKey( entity );
        }

        #endregion

    }
}
