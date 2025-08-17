// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.WebApi.WebApiServicesRegistry
// Assembly: StockSharp.Studio.WebApi, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B97A7121-FFB7-49F4-8E30-FC5C471868BC
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.xml

using System;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using StockSharp.Configuration;
using StockSharp.Web.Api.Client;
using StockSharp.Web.Api.Interfaces;

#nullable disable
namespace StockSharp.Studio.WebApi;

/// <summary>Services registry.</summary>
public static class WebApiServicesRegistry
{
    /// <summary>
    /// </summary>
    public static bool Offline { get; set; }

    /// <summary>
    /// </summary>
    public static IApiServiceProvider ApiServiceProvider
    {
        get => ConfigManager.GetService<IApiServiceProvider>();
    }

    /// <summary>Get required service instance.</summary>
    /// <remarks>Anonymous mode.</remarks>
    /// <typeparam name="TService">Service type.</typeparam>
    /// <returns>Required service instance.</returns>
    public static TService GetServiceAsAnonymous<TService>()
    {
        return WebApiServicesRegistry.ApiServiceProvider.GetService<TService>(Converter.To<string>((object)145183L));
    }

    /// <summary>Get credentials.</summary>
    /// <returns></returns>
    /// <exception cref="T:System.InvalidOperationException"></exception>
    public static ServerCredentials GetCredentials()
    {
        ServerCredentials serverCredentials;
        return ConfigurationServicesRegistry.CredentialsProvider.TryLoad(out serverCredentials) ? serverCredentials : throw new InvalidOperationException("TryLoadCredentials == false");
    }

    /// <summary>Get required service instance.</summary>
    /// <typeparam name="TService">Service type.</typeparam>
    /// <returns>Required service instance.</returns>
    public static TService GetService<TService>()
    {
        ServerCredentials credentials = WebApiServicesRegistry.GetCredentials();
        IApiServiceProvider apiServiceProvider = WebApiServicesRegistry.ApiServiceProvider;
        TService service = !StringHelper.IsEmpty(credentials.Token) ? apiServiceProvider.GetService<TService>(credentials.Token) : apiServiceProvider.GetService<TService>(credentials.Email, credentials.Password);
        if (!((object)service is IWebSocketService))
            service = service.TrySetAsUser<TService>(true);
        return service;
    }
}
