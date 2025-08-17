// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.IApiServiceProviderExtensions
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;

#nullable disable
namespace StockSharp.Web.Api.Client;

public static class IApiServiceProviderExtensions
{
    public static TService GetServiceAsApiAdmin<TService>(this IApiServiceProvider provider)
    {
        return provider.GetService<TService>(Converter.To<string>((object)1L));
    }

    public static TService GetService<TService>(this IApiServiceProvider provider, string token)
    {
        return TypeHelper.CheckOnNull<IApiServiceProvider>(provider, nameof(provider)).GetService<TService>(StringHelper.Secure(token));
    }
}
