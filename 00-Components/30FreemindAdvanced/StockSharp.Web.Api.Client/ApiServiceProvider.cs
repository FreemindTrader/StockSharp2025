// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ApiServiceProvider
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Reflection;
using StockSharp.Web.Api.Client.WebSockets;
using StockSharp.Web.Api.Interfaces;

#nullable disable
namespace StockSharp.Web.Api.Client;

public class ApiServiceProvider : BaseLogReceiver, IApiServiceProvider, ILogSource, IDisposable
{
    private readonly Dictionary<Type, Type> _clients = new Dictionary<Type, Type>();
    private readonly HttpMessageInvoker _http;
    private readonly string _baseAddress;

    public ApiServiceProvider()
      : this((HttpMessageInvoker)new HttpClient(), "api.stocksharp.com/v1/")
    {
    }

    public ApiServiceProvider(HttpMessageInvoker http, string baseAddress)
    {
        if (StringHelper.IsEmpty(baseAddress))
            throw new ArgumentNullException(nameof(baseAddress));
        if (StringHelper.StartsWithIgnoreCase(baseAddress, nameof(http)))
            throw new ArgumentException(nameof(baseAddress));
        this._http = http ?? throw new ArgumentNullException(nameof(http));
        this._baseAddress = baseAddress;
        foreach (Type implementation in ReflectionHelper.FindImplementations<BaseApiClient>(typeof(ApiServiceProvider).Assembly, false, true, false, (Func<Type, bool>)null))
        {
            foreach (Type type in implementation.GetInterfaces())
            {
                if (StringHelper.StartsWithIgnoreCase(type.Namespace, "StockSharp") || StringHelper.StartsWithIgnoreCase(type.Namespace, "Ecng"))
                    CollectionHelper.TryAdd2<Type, Type>((IDictionary<Type, Type>)this._clients, type, implementation);
            }
        }
        this._clients.Add(typeof(IWebSocketService), typeof(WebSocketApiClient));
        this._clients.Add(typeof(WebSocketApiClient), typeof(WebSocketApiClient));
    }

    private Type GetClientType<TService>()
    {
        Type clientType;
        if (!this._clients.TryGetValue(typeof(TService), out clientType))
            throw new InvalidOperationException(typeof(TService).AssemblyQualifiedName);
        return clientType;
    }

    private TService GetService<TService>(params object[] args)
    {
        bool flag = typeof(TService) == typeof(WebSocketApiClient) || typeof(TService) == typeof(IWebSocketService);
        if (!flag)
            args = ArrayHelper.Concat<object>((object[])new HttpMessageInvoker[1]
            {
        this._http
            }, args);
        args = ArrayHelper.Concat<object>((object[])new Uri[1]
        {
      Converter.To<Uri>((object) $"{(flag ? "wss" : Uri.UriSchemeHttps)}://{this._baseAddress}")
        }, args);
        TService instance = TypeHelper.CreateInstance<TService>(this.GetClientType<TService>(), args);
        switch (instance)
        {
            case ILogSource ilogSource:
                ilogSource.Parent = (ILogSource)this;
                break;
            case BaseApiClient baseApiClient:
                baseApiClient.Logs = (ILogReceiver)this;
                break;
        }
        return Converter.To<TService>((object)instance);
    }

    TService IApiServiceProvider.GetService<TService>(SecureString token)
    {
        return this.GetService<TService>((object)this.CheckOnEmpty(token, nameof(token)));
    }

    TService IApiServiceProvider.GetService<TService>(string login, SecureString password)
    {
        return this.GetService<TService>((object)this.CheckOnEmpty(login, nameof(login)), (object)this.CheckOnEmpty(password, nameof(password)));
    }

    private string CheckOnEmpty(string str, string paramName)
    {
        return !StringHelper.IsEmpty(str) ? str : throw new ArgumentNullException(paramName);
    }

    private SecureString CheckOnEmpty(SecureString str, string paramName)
    {
        return !StringHelper.IsEmpty(str) ? str : throw new ArgumentNullException(paramName);
    }
}
