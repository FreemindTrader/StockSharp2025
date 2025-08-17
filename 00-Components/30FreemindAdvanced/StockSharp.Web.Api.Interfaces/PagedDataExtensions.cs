// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Interfaces.PagedDataExtensions
// Assembly: StockSharp.Web.Api.Interfaces, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9EB02CA6-0DCD-4F94-B6F3-8DF6ED492679
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Interfaces.dll

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Ecng.Linq;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Interfaces;

public static class PagedDataExtensions
{
    public static async Task<IEnumerable<T>> GetPaginatedDataAsync<TService, T>(
                                                                                      this TService service,
                                                                                      Func<TService, int, int, CancellationToken, Task<BaseEntitySet<T>>> getPage,
                                                                                      int pageSize = 20,
                                                                                      int count = Int32.MaxValue ,
                                                                                      int maxIter = 10,
                                                                                      CancellationToken cancellationToken = default(CancellationToken))
    {
        List<T> list;
        IEnumerable<T> allData = new List<T>();

        if (service == null)
        {
            throw new ArgumentNullException(nameof(service));
        }

        if (getPage == null)
        {
            throw new ArgumentNullException(nameof(getPage));
        }

        if (pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        }

        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }


        list = new List<T>();
        int skip = 0;
        int iter = 0;

        do
        {
            int required = pageSize.Min(count - skip);

            var pageResult = await getPage(service, skip, required, cancellationToken);

            list.AddRange(pageResult.Items);

            if (pageResult.Items.Length >= required)
            {
                skip += required;
                if (iter++ > maxIter)
                    throw new InvalidOperationException("iter > maxIter");
            }
            else
                break;

            var pagingData = await getPage(service, skip, required, cancellationToken);

            list.AddRange(pagingData.Items);

            if (pagingData.Items.Length >= required)
            {
                skip += required;
                if (iter++ > maxIter)
                    throw new InvalidOperationException("iter > maxIter");
            }
            else
                break;
        }
        while (skip < count);
        allData = list;

        return allData;
    }    
}
