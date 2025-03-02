using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Configuration;
using StockSharp.Configuration;
using StockSharp.Web.Api.Client;
using StockSharp.Web.Api.Interfaces;
using System;

namespace StockSharp.Studio.WebApi
{
    /// <summary>Services registry.</summary>
    public static class WebApiServicesRegistry
    {
        /// <summary>
        /// </summary>
        public static bool Offline { get; set; }

        /// <summary>
        /// </summary>
        public static IApiServiceProvider ApiServiceProvider
        {
            get
            {
                return ConfigManager.GetService<IApiServiceProvider>();
            }
        }

        /// <summary>Get required service instance.</summary>
        /// <remarks>Anonymous mode.</remarks>
        /// <typeparam name="TService">Service type.</typeparam>
        /// <returns>Required service instance.</returns>
        public static TService GetServiceAsAnonymous<TService>()
        {
            return WebApiServicesRegistry.ApiServiceProvider.GetService<TService>( ( string ) Converter.To<string>( ( object ) 145183L ) );
        }

        /// <summary>Get credentials.</summary>
        /// <returns></returns>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        public static ServerCredentials GetCredentials()
        {
            ServerCredentials credentials;
            if ( !ConfigurationServicesRegistry.CredentialsProvider.TryLoad( out credentials ) )
                throw new InvalidOperationException( "TryLoadCredentials == false" );
            return credentials;
        }

        /// <summary>Get required service instance.</summary>
        /// <typeparam name="TService">Service type.</typeparam>
        /// <returns>Required service instance.</returns>
        public static TService GetService<TService>()
        {
            ServerCredentials credentials = WebApiServicesRegistry.GetCredentials();
            IApiServiceProvider apiServiceProvider = WebApiServicesRegistry.ApiServiceProvider;
            TService service = !StringHelper.IsEmpty(credentials.Token ) ? apiServiceProvider.GetService<TService>(credentials.Token ) : apiServiceProvider.GetService<TService>(credentials.Email, credentials.Password );
            if ( !( ( object ) service is IWebSocketService ) )
                service = service.TrySetAsUser<TService>( true );
            return service;
        }
    }
}
