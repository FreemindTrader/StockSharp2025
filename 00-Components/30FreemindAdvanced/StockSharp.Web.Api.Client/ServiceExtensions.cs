// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ServiceExtensions
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Common;
using Ecng.Linq;
using Ecng.Net;
using StockSharp.Web.Api.Client.Linq;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Api.Client;

public static class ServiceExtensions
{
    public static void SetHeader<TService>(this TService service, string name, object value)
    {
        Converter.To<BaseApiClient>((object)service).PerRequestHeaders[name] = Converter.To<string>(value);
    }

    public static bool RemoveHeader<TService>(this TService service, string name)
    {
        return Converter.To<BaseApiClient>((object)service).PerRequestHeaders.Remove(name);
    }

    public static TService TrySetIp<TService>(this TService service, IPAddress value)
    {
        if (value != null)
            service.SetHeader<TService>("SS-ADDR", (object)value);
        return service;
    }

    private static TService TrySet<TService>(this TService service, string name, bool value)
    {
        if (value)
            service.SetHeader<TService>(name, (object)true);
        else
            service.RemoveHeader<TService>(name);
        return service;
    }

    public static TService TrySetAsUser<TService>(this TService service, bool value)
    {
        return service.TrySet<TService>("SS-ASU", value);
    }

    public static TService TrySetExtended<TService>(this TService service, bool value)
    {
        return service.TrySet<TService>("SS-EX", value);
    }

    public static TService TrySetCache<TService>(this TService service, IRestApiClientCache value)
    {
        if (service is BaseApiClient baseApiClient)
            baseApiClient.Cache = value;
        return service;
    }

    public static TService TrySetErrorInfo<TService>(this TService service, bool value)
    {
        return service.TrySet<TService>("SS-EI", value);
    }

    public static IQueryable<T> AsQueryable<T>(
      this IBaseEntityService<T> service,
      string methodName = "FindAsync",
      long maxTake = 20)
      where T : BaseEntity
    {
        return ServiceExtensions.AsQueryable<T>(service, methodName, maxTake);
    }

    public static IQueryable<T> AsQueryable<T>(this object service, string methodName = "FindAsync", long maxTake = 20) where T : BaseEntity
    {
        return (IQueryable<T>)new ApiClientDataQuery<T>(service, methodName, maxTake);
    }

    public static bool By<TEntity, TValue>(this TEntity _, string __, TValue ___)
    {
        throw new NotSupportedException();
    }

    public static Task<BaseEntitySet<TEntity>> ToEntitySetAsync<TEntity>(this IQueryable<TEntity> source, CancellationToken cancellationToken)
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }

        var f = new Func<IQueryable<TEntity>, CancellationToken, Task<BaseEntitySet<TEntity>>>(ServiceExtensions.ToEntitySetAsync<TEntity>);

        return source.Provider.Execute<Task<BaseEntitySet<TEntity>>>(Expression.Call(null, QueryableExtensions.GetMethodInfo<IQueryable<TEntity>, CancellationToken, Task<BaseEntitySet<TEntity>>>(f, source, cancellationToken), new Expression[]
        {
                source.Expression,
                Expression.Constant(cancellationToken)
        }));
    }
}
