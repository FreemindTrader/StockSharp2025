// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseEntitySetExtensions
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using System.Collections.Generic;
using System.Linq;

namespace StockSharp.Web.DomainModel
{
    public static class BaseEntitySetExtensions
    {
        public static BaseEntitySet<TEntity> ToEntitySet<TEntity>(
          this IEnumerable<TEntity> items,
          long count = 0 )
        {
            TEntity[] entityArray = items != null ? items.ToArray<TEntity>() : (TEntity[]) null;
            if ( count == 0L && items != null )
                count = ( long ) entityArray.Length;
            return new BaseEntitySet<TEntity>()
            {
                Items = entityArray,
                Count = count
            };
        }
    }
}
