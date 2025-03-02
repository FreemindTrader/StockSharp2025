// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Common.InMemoryRestApiClientCacheEx
// Assembly: StockSharp.Web.Common, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E38A38B-3071-40E9-9B31-80D08347A76B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Common.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Net;
using Ecng.Serialization;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace StockSharp.Web.Common
{
    public class InMemoryRestApiClientCacheEx : InMemoryRestApiClientCache
    {
        public InMemoryRestApiClientCacheEx( TimeSpan timeout ) : base( timeout )
        {

        }

        protected override bool IsSupported( HttpMethod method )
        {
            if ( !base.IsSupported( method ) )
                return method == HttpMethod.Post;
            return true;
        }

        protected override ValueTuple<HttpMethod, string, object> ToKey(
          HttpMethod method,
          Uri uri,
          object body )
        {
            return new ValueTuple<HttpMethod, string, object>( method, ( ( string ) Converter.To<string>( ( object ) uri ) ).ToLowerInvariant(), body == null ? ( object ) string.Empty : ( object ) JsonHelper.ToJson( body, true ) );
        }

        public void ClearByEntityId( Type entityType, long entityId )
        {
            if ( ( object ) entityType == null )
                throw new ArgumentNullException( nameof( entityType ) );
            lock ( ( ( SynchronizedDictionary<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>> ) this.Cache ).SyncRoot )
            {
                foreach ( KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>> keyValuePair in ( ( IEnumerable<KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>>> ) this.Cache ).Where<KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>>>( ( Func<KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>>, bool> ) ( p =>
                {
                    BaseEntity baseEntity = p.Value.Item1 as BaseEntity;
                    if ( baseEntity != null && baseEntity.GetType() == entityType && baseEntity.Id == entityId )
                        return true;
                    IBaseEntitySet baseEntitySet = p.Value.Item1 as IBaseEntitySet;
                    if ( baseEntitySet != null )
                        return baseEntitySet.Has( entityType, entityId );
                    return false;
                } ) ).ToArray<KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>>>() )
                    ( ( SynchronizedDictionary<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>> ) this.Cache ).Remove( keyValuePair.Key );
            }
        }

        public T [ ] Find<T>()
        {
            List<T> objList = new List<T>();
            lock ( ( ( SynchronizedDictionary<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>> ) this.Cache ).SyncRoot )
            {
                using ( IEnumerator<KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>>> enumerator = ( ( SynchronizedDictionary<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>> ) this.Cache ).GetEnumerator() )
                {
                    while ( enumerator.MoveNext() )
                    {
                        KeyValuePair<ValueTuple<HttpMethod, string, object>, ValueTuple<object, DateTime>> current = enumerator.Current;
                        object obj1 = current.Value.Item1;
                        if ( obj1 is T )
                        {
                            T obj2 = (T) obj1;
                            objList.Add( obj2 );
                        }
                        else
                        {
                            IBaseEntitySet baseEntitySet = current.Value.Item1 as IBaseEntitySet;
                            if ( baseEntitySet != null )
                            {
                                T[] items = baseEntitySet.Items as T[];
                                if ( items != null )
                                    objList.AddRange( ( IEnumerable<T> ) items );
                            }
                        }
                    }
                }
            }
            return objList.ToArray();
        }
    }
}
