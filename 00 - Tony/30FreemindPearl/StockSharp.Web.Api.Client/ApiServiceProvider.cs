// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ApiServiceProvider
// Assembly: StockSharp.Web.Api.Client, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9C29C3BA-4173-498F-98DB-80C2C449F660
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Client.dll

using Ecng.Collections;
using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;

namespace StockSharp.Web.Api.Client
{
    public class ApiServiceProvider : IApiServiceProvider
    {
        private readonly Dictionary<Type, Type> _clients = new Dictionary<Type, Type>();
        private readonly HttpMessageInvoker _http;

        public ApiServiceProvider()
          : this((HttpMessageInvoker)new HttpClient())
        {
        }

        public ApiServiceProvider(HttpMessageInvoker http)
        {
            HttpMessageInvoker httpMessageInvoker = http;
            if (httpMessageInvoker == null)
                throw new ArgumentNullException(nameof(http));
            _http = httpMessageInvoker;
            foreach (Type type in ((IEnumerable<Type>)typeof(ApiServiceProvider).Assembly.GetTypes()).Where<Type>((Func<Type, bool>)(t =>
         {
             if (!t.IsAbstract)
                 return t.IsSubclassOf(typeof(BaseApiClient));
             return false;
         })))
            {
                foreach (Type key in type.GetInterfaces())
                {
                    if (key.Namespace.StartsWithIgnoreCase("StockSharp") || key.Namespace.StartsWithIgnoreCase("Ecng"))
                        _clients.TryAdd2<Type, Type>(key, type);
                }
            }
        }

        public bool Tracing { get; set; }

        private Type GetClientType<TService>()
        {
            Type type;
            if (!_clients.TryGetValue(typeof(TService), out type))
                throw new InvalidOperationException(typeof(TService).AssemblyQualifiedName);
            return type;
        }

        private TService GetService<TService>(params object[] args)
        {
            BaseApiClient instance = GetClientType<TService>().CreateInstance<BaseApiClient>(((object[])new HttpMessageInvoker[1] { _http }).Concat<object>(args));
            if (Tracing)
                instance.Tracing = true;
            return instance.To<TService>();
        }

        TService IApiServiceProvider.GetService<TService>(SecureString token)
        {
            return GetService<TService>((object)CheckOnEmpty(token, nameof(token)));
        }

        TService IApiServiceProvider.GetService<TService>(
          string login,
          SecureString password)
        {
            return GetService<TService>((object)CheckOnEmpty(login, nameof(login)), (object)CheckOnEmpty(password, nameof(password)));
        }

        private string CheckOnEmpty(string str, string paramName)
        {
            if (str.IsEmpty())
                throw new ArgumentNullException(paramName);
            return str;
        }

        private SecureString CheckOnEmpty(SecureString str, string paramName)
        {
            if (str.IsEmpty())
                throw new ArgumentNullException(paramName);
            return str;
        }
    }
}
