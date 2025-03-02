// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.WebApi.WebApiAuthorization
// Assembly: StockSharp.Studio.WebApi, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 54E25E17-EECA-4F64-ACCA-A2705D24CD36
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.WebApi.dll

using Ecng.Security;
using StockSharp.Localization;
using StockSharp.Web.Api.Client;
using StockSharp.Web.Api.Interfaces;
using System;
using System.Net;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.WebApi
{
    /// <summary>
    /// The module of the connection access check based on the StockSharp authorization.
    /// </summary>
    public class WebApiAuthorization : IAuthorization
    {
        private readonly IApiServiceProvider _apiProvider;
        private readonly bool _useClientAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Studio.WebApi.WebApiAuthorization" />.
        /// </summary>
        public WebApiAuthorization()
          : this( WebApiServicesRegistry.ApiServiceProvider, false )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Studio.WebApi.WebApiAuthorization" />.
        /// </summary>
        /// <param name="apiProvider"><see cref="T:StockSharp.Web.Api.Client.IApiServiceProvider" />.</param>
        /// <param name="useClientAddress"><see cref="M:StockSharp.Web.Api.Client.ServiceExtensions.TrySetIp``1(``0,System.Net.IPAddress)" /></param>
        public WebApiAuthorization( IApiServiceProvider apiProvider, bool useClientAddress )
        {
            IApiServiceProvider apiServiceProvider = apiProvider;
            if ( apiServiceProvider == null )
                throw new ArgumentNullException( nameof( apiProvider ) );
            this._apiProvider = apiServiceProvider;
            this._useClientAddress = useClientAddress;
        }

        /// <inheritdoc />
        public virtual async ValueTask<string> ValidateCredentials( string login, SecureString password, IPAddress clientAddress, CancellationToken cancellationToken )
        {
            IClientService service = login == "x" ? this._apiProvider.GetService<IClientService>(password) : this._apiProvider.GetService<IClientService>(login, password);
            if ( this._useClientAddress )
                service = service.TrySetIp<IClientService>( clientAddress );
            string str;
            try
            {
                await service.PingAsync( cancellationToken );
                str = login;
            }
            catch ( Exception ex )
            {
                throw new UnauthorizedAccessException( LocalizedStrings.WrongLoginOrPassword, ex );
            }
            return str;
        }
    }
}
