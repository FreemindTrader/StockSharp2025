// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ServiceExtensions
// Assembly: StockSharp.Web.Api.Client, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9C29C3BA-4173-498F-98DB-80C2C449F660
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Client.dll

using Ecng.Common;
using Ecng.Net;
using System.Net;

namespace StockSharp.Web.Api.Client
{
    public static class ServiceExtensions
    {
        public static void SetHeader<TService>(this TService service, string name, object value)
        {
            ((object)service).To<BaseApiClient>().PerRequestHeaders[name] = value.To<string>();
        }

        public static TService TrySetIp<TService>(this TService service, IPAddress value)
        {
            if (value != null)
                service.SetHeader<TService>("SS-ADDR", (object)value);
            return service;
        }

        public static TService TrySetAsUser<TService>(this TService service, bool value)
        {
            if (value)
                service.SetHeader<TService>("SS-ASU", (object)true);
            return service;
        }

        public static TService TrySetExtended<TService>(this TService service, bool value)
        {
            if (value)
                service.SetHeader<TService>("SS-EX", (object)true);
            return service;
        }

        public static TService TrySetCache<TService>(this TService service, IRestApiClientCache value)
        {
            BaseApiClient baseApiClient = (object)service as BaseApiClient;
            if (baseApiClient != null)
                baseApiClient.Cache = value;
            return service;
        }

        public static TService TrySetErrorInfo<TService>(this TService service, bool value)
        {
            if (value)
                service.SetHeader<TService>("SS-EI", (object)true);
            return service;
        }
    }
}
