using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using fx.Collections;

namespace fx.Database.Common.DataModel
{
    /// <summary>
    /// The base class for unit of works that provides the storage for repositories. 
    /// </summary>
    public class UnitOfWorkBase
    {
        readonly PooledDictionary< Type, object > repositories = new PooledDictionary< Type, object >();

        protected TRepository 
        GetRepositoryCore< TRepository, TEntity >( Func< TRepository > createRepositoryFunc ) where TRepository : IReadOnlyRepository< TEntity > where TEntity : class
        {
            object result                         = null;

            if ( !repositories.TryGetValue( typeof( TEntity ), out result ) )
            {
                result                            = createRepositoryFunc( );
                repositories[ typeof( TEntity ) ] = result;
            }

            return ( TRepository ) result;
        }
    }
}