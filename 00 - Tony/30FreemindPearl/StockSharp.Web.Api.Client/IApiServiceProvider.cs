// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.IApiServiceProvider
// Assembly: StockSharp.Web.Api.Client, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9C29C3BA-4173-498F-98DB-80C2C449F660
// Assembly location: T:\00-StockSharp\Data\StockSharp.Web.Api.Client.dll

using System.Security;

namespace StockSharp.Web.Api.Client
{
    public interface IApiServiceProvider
    {
        TService GetService<TService>(SecureString token);

        TService GetService<TService>(string login, SecureString password);

        bool Tracing { get; set; }
    }
}
