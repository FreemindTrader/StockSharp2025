// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.IApiServiceProviderExtensions
// Assembly: StockSharp.Web.Api.Client, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9C29C3BA-4173-498F-98DB-80C2C449F660
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Client.dll

using Ecng.Common;

namespace StockSharp.Web.Api.Client
{
    public static class IApiServiceProviderExtensions
    {
        public static TService GetServiceAsApiAdmin<TService>(this IApiServiceProvider provider)
        {
            return provider.GetService<TService>(1L.To<string>());
        }

        public static TService GetService<TService>(this IApiServiceProvider provider, string token)
        {
            return provider.CheckOnNull<IApiServiceProvider>(nameof(provider)).GetService<TService>(token.Secure());
        }
    }
}
