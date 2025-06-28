// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.BaseEntitySetExtensions
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace StockSharp.Web.DomainModel;

public static class BaseEntitySetExtensions
{
    public static BaseEntitySet<TEntity> ToEntitySet<TEntity>(
      this IEnumerable<TEntity> items,
      long count = 0)
    {
        TEntity[] array = items != null ? items.ToArray<TEntity>() : (TEntity[])null;
        if (count == 0L && items != null)
            count = (long)array.Length;
        return new BaseEntitySet<TEntity>()
        {
            Items = array,
            Count = count
        };
    }
}
