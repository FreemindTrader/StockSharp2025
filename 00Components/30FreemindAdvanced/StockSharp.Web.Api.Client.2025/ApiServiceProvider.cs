// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.Api.Client.ApiServiceProvider
// Assembly: StockSharp.Web.Api.Client, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A51CD7A-C8B0-46DE-9611-D9A359DFC774
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.Api.Client.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Reflection;
using StockSharp.Web.Api.Client.WebSockets;
using StockSharp.Web.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;

namespace StockSharp.Web.Api.Client
{
    public class ApiServiceProvider : BaseLogReceiver, IApiServiceProvider, ILogSource, IDisposable
    {
        private readonly Dictionary<Type, Type> _clients;
        private readonly HttpMessageInvoker _http;
        private readonly string _baseAddress;

        public ApiServiceProvider() : this( ( HttpMessageInvoker ) new HttpClient(), "api.stocksharp.com/v1/" )
        {
        }

        public ApiServiceProvider( HttpMessageInvoker http, string baseAddress )
        {            
            if ( StringHelper.IsEmpty( baseAddress ) )
                throw new ArgumentNullException( nameof( baseAddress ) );
            if ( StringHelper.StartsWithIgnoreCase( baseAddress, nameof( http ) ) )
                throw new ArgumentException( nameof( baseAddress ) );
            HttpMessageInvoker httpMessageInvoker = http;
            if ( httpMessageInvoker == null )
                throw new ArgumentNullException( nameof( http ) );
            this._http = httpMessageInvoker;
            this._baseAddress = baseAddress;
            foreach ( Type implementation in ReflectionHelper.FindImplementations<BaseApiClient>( typeof( ApiServiceProvider ).Assembly, false, true, false, ( Func<Type, bool> ) null ) )
            {
                foreach ( Type type in implementation.GetInterfaces() )
                {
                    if ( StringHelper.StartsWithIgnoreCase( type.Namespace, "StockSharp" ) || StringHelper.StartsWithIgnoreCase( type.Namespace, "Ecng" ) )
                        CollectionHelper.TryAdd2<Type, Type>(  this._clients,  type,  implementation );
                }
            }
            this._clients.Add( typeof( IWebSocketService ), typeof( WebSocketApiClient ) );
            this._clients.Add( typeof( WebSocketApiClient ), typeof( WebSocketApiClient ) );
        }

        private Type GetClientType<TService>()
        {
            Type type;
            if ( !this._clients.TryGetValue( typeof( TService ), out type ) )
                throw new InvalidOperationException( typeof( TService ).AssemblyQualifiedName );
            return type;
        }

        private TService GetService<TService>( params object [ ] args )
        {
            bool flag = typeof (TService) == typeof (WebSocketApiClient) || typeof (TService) == typeof (IWebSocketService);
            if ( !flag )
                args = ( object [ ] ) ArrayHelper.Concat<object>(  new HttpMessageInvoker [1]
                {
          this._http
                },  args );
            args = ( object [ ] ) ArrayHelper.Concat<object>(  new Uri [1]
            {
        (Uri) Converter.To<Uri>( ((flag ? "wss" : Uri.UriSchemeHttps) + "://" + this._baseAddress))
            },  args );
            TService instance = TypeHelper.CreateInstance<TService>(this.GetClientType<TService>(), args);
            ILogSource ilogSource =  instance as ILogSource;
            if ( ilogSource != null )
            {
                ilogSource.Parent = this;
            }
            else
            {
                BaseApiClient baseApiClient =  instance as BaseApiClient;
                if ( baseApiClient != null )
                    baseApiClient.Logs = ( ILogReceiver ) this;
            }
            return Converter.To<TService>( ( object ) instance );
        }

        TService IApiServiceProvider.GetService<TService>( SecureString token )
        {
            return this.GetService<TService>( ( object ) this.CheckOnEmpty( token, nameof( token ) ) );
        }

        TService IApiServiceProvider.GetService<TService>(
          string login,
          SecureString password )
        {
            return this.GetService<TService>( ( object ) this.CheckOnEmpty( login, nameof( login ) ), ( object ) this.CheckOnEmpty( password, nameof( password ) ) );
        }

        private string CheckOnEmpty( string str, string paramName )
        {
            if ( StringHelper.IsEmpty( str ) )
                throw new ArgumentNullException( paramName );
            return str;
        }

        private SecureString CheckOnEmpty( SecureString str, string paramName )
        {
            if ( StringHelper.IsEmpty( str ) )
                throw new ArgumentNullException( paramName );
            return str;
        }
    }
}
