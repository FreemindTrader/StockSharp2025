// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.IApiServiceProviderExtensions
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Common;

namespace StockSharp.Web.Api.Client
{
    public static class IApiServiceProviderExtensions
    {
        public static TService GetServiceAsApiAdmin<TService>( this IApiServiceProvider provider )
        {
            return provider.GetService<TService>( ( string ) Converter.To<string>( ( object ) 1L ) );
        }

        public static TService GetService<TService>( this IApiServiceProvider provider, string token )
        {
            return ( ( IApiServiceProvider ) TypeHelper.CheckOnNull<IApiServiceProvider>(  provider, nameof( provider ) ) ).GetService<TService>( StringHelper.Secure( token ) );
        }
    }
}
