using Ecng.Common;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockSharp.Web.DomainModel
{
    public static class BaseEntitySetExtensions
    {
        public static BaseEntitySet<TEntity> ToEntitySet<TEntity>( this IEnumerable<TEntity> items, int count = 0)
        {
            TEntity[] entityArray = items != null ? items.ToArray() : null;
            if (count == 0 && items != null)
                count = entityArray.Length;
            return new BaseEntitySet<TEntity>() { Items = entityArray, Count = count };
        }

        public static async Task<BaseEntitySet<TEntity>> ToEntitySetAsync<TEntity>( this IEnumerable<Task<TEntity>> me, int count = 0)
        {
            return ((IEnumerable<TEntity>)await me.WhenAll<TEntity>()).ToEntitySet(count);
        }

        public static IEnumerable<TEntity> ToEnumerable<TEntity>( this BaseEntitySet<TEntity> set ) where TEntity : BaseEntity
        {
            return set.CheckOnNull("value").Items ?? (IEnumerable<TEntity>)Array.Empty<TEntity>();
        }
    }
}
