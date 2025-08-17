// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.InMemoryRestApiClientCacheEx
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 02092862-EA5F-4AA7-B6CA-D0C9A4C8AFDF
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Common.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Net;
using Ecng.Serialization;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Web.Common;

public class InMemoryRestApiClientCacheEx(TimeSpan timeout) : InMemoryRestApiClientCache(timeout)
{
    protected virtual bool IsSupported(HttpMethod method)
    {
        return base.IsSupported(method) || method == HttpMethod.Post;
    }

    protected virtual (HttpMethod, string, object) ToKey(HttpMethod method, Uri uri, object body)
    {
        return (method, Converter.To<string>((object)uri).ToLowerInvariant(), body == null ? (object)string.Empty : (object)JsonHelper.ToJson(body, true));
    }

    public void ClearByEntityId(Type entityType, long entityId)
    {
        if ((object)entityType == null)
            throw new ArgumentNullException(nameof(entityType));
        lock (((SynchronizedDictionary<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>>)this.Cache).SyncRoot)
        {
            foreach (KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>> keyValuePair in ((IEnumerable<KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>>>)this.Cache).Where<KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>>>((Func<KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>>, bool>)(p =>
            {
                BaseEntity baseEntity = p.Value.Item1 as BaseEntity;
                if (baseEntity != null && baseEntity.GetType() == entityType && baseEntity.Id == entityId)
                    return true;
                IBaseEntitySet baseEntitySet = p.Value.Item1 as IBaseEntitySet;
                if (baseEntitySet != null)
                    return baseEntitySet.Has(entityType, entityId);
                return false;
            })).ToArray<KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>>>())
                ((SynchronizedDictionary<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>>)this.Cache).Remove(keyValuePair.Key);
        }
    }

    public T[] Find<T>()
    {
        List<T> objList = new List<T>();
        lock (((SynchronizedDictionary<(HttpMethod, string, object), (object, DateTime)>)this.Cache).SyncRoot)
        {
            foreach (KeyValuePair<(HttpMethod, string, object), (object, DateTime)> keyValuePair in (SynchronizedDictionary<(HttpMethod, string, object), (object, DateTime)>)this.Cache)
            {
                if (keyValuePair.Value.Item1 is T obj)
                    objList.Add(obj);
                else if (keyValuePair.Value.Item1 is IBaseEntitySet baseEntitySet && baseEntitySet.Items is T[] items)
                    objList.AddRange((IEnumerable<T>)items);
            }
        }
        return objList.ToArray();
    }
}
