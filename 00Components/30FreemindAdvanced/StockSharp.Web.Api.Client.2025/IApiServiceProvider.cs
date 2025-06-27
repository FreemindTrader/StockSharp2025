// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.IApiServiceProvider
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Logging;
using System;
using System.Security;

namespace StockSharp.Web.Api.Client
{
    public interface IApiServiceProvider : ILogSource, IDisposable
    {
        TService GetService<TService>( SecureString token );

        TService GetService<TService>( string login, SecureString password );
    }
}
