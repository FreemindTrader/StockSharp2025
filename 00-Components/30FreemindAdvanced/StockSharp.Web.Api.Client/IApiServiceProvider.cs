// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.IApiServiceProvider
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3B0C137-E2D2-4BFF-B274-A742CBFA5E54
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.Api.Client.dll

using System;
using System.Security;
using Ecng.Logging;

#nullable disable
namespace StockSharp.Web.Api.Client;

public interface IApiServiceProvider : ILogSource, IDisposable
{
    TService GetService<TService>(SecureString token);

    TService GetService<TService>(string login, SecureString password);
}
