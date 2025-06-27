// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ClientBalanceApiClient
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Net.Http;
using System.Security;

namespace StockSharp.Web.Api.Client
{
    internal class ClientBalanceApiClient : BaseApiEntityClient<ClientBalance>, IClientBalanceService, IBaseEntityService<ClientBalance>
    {
        public ClientBalanceApiClient( Uri baseAddress, HttpMessageInvoker http, SecureString token )
          : base( baseAddress, http, token )
        {
        }

        public ClientBalanceApiClient(
          Uri baseAddress,
          HttpMessageInvoker http,
          string login,
          SecureString password )
          : base( baseAddress, http, login, password )
        {
        }
    }
}
