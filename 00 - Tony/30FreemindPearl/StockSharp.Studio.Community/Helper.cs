using Ecng.Common;

using Ecng.Configuration;
using Ecng.Net;
using StockSharp.Algo.Storages;
using StockSharp.Configuration;
using StockSharp.Logging;
using StockSharp.Web.Api.Client;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS0168

namespace StockSharp.Studio.Community
{
    /// <summary>
    /// 
    /// </summary>
    public static class Helper
    {
        /// <summary>Current domain id.</summary>
        public static long CurrentDomain
        {
            get
            {
                return !( Paths.Domain == "com" ) ? 1L : 2L;
            }
        }

        /// <summary>Current product id.</summary>
        public static long ProductId { get; private set; }

        /// <summary>Get product domain info.</summary>
        /// <param name="product">Product.</param>
        /// <returns>Product domain info.</returns>
        public static ProductDomain GetCurrentDomain( this Product product )
        {
            if ( product == null )
                throw new ArgumentNullException( nameof( product ) );
            ProductDomain[ ] items = product.Domains?.Items;
            long currDomain = CurrentDomain;
            return items.FirstOrDefault( d =>
            {
                Domain domain = d.Domain;
                if ( domain == null )
                    return false;
                return domain.Id == currDomain;
            } ) ?? items.FirstOrDefault();
        }

        /// <summary>Get product group domain info.</summary>
        /// <param name="group">Product group.</param>
        /// <returns>Product group domain info.</returns>
        public static ProductGroupDomain GetCurrentDomain( this ProductGroup group )
        {
            if ( group == null )
                throw new ArgumentNullException( nameof( group ) );
            ProductGroupDomain[ ] items = group.Domains?.Items;
            long currDomain = CurrentDomain;
            return items.FirstOrDefault( d =>
            {
                Domain domain = d.Domain;
                if ( domain == null )
                    return false;
                return domain.Id == currDomain;
            } ) ?? items.FirstOrDefault();
        }

        /// <summary>Get product's name.</summary>
        /// <param name="product">Product.</param>
        /// <returns>Name.</returns>
        public static string GetName( this Product product )
        {
            string str = product.GetCurrentDomain()?.Name;
            if ( str.IsEmpty() )
                str = product.PackageId.IsEmpty( product.Id.To<string>() );
            return str;
        }

        /// <summary>Get group's name.</summary>
        /// <param name="group">Product group.</param>
        /// <returns>Name.</returns>
        public static string GetName( this ProductGroup group )
        {
            string name = group.GetCurrentDomain()?.Name;
            if ( name.IsEmpty() )
                name = group.Id.To<string>();
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onlineMode"></param>
        /// <param name="owner"></param>
        public static void RegisterServices( bool onlineMode, Type owner )
        {
            ProductId = Extensions.GetProductId();
            ConfigManager.RegisterService<IApiServiceProvider>( new ApiServiceProvider() );
            ConfigManager.RegisterService<ICredentialsProvider>( new DefaultCredentialsProvider() );
            ConfigManager.RegisterService<IHtmlFormatter>( new HtmlFormatter() );
            CommunityServicesRegistry.Offline = !onlineMode;
        }

        public static Task InitServices(
                                            Action<SystemMessage> newsReceieved,
                                            Action disconnected,
                                            CancellationToken token = default( CancellationToken )
                                        )
        {
            if ( newsReceieved == null )
            {
                throw new ArgumentNullException( nameof( newsReceieved ) );
            }

            if ( disconnected == null )
            {
                throw new ArgumentNullException( nameof( disconnected ) );
            }


            return Task.Run( async () =>
            {

                ISessionService sessionSvc;
                Session session;
                IMessageService messageSvc;
                try
                {
                    sessionSvc = CommunityServicesRegistry.GetService<ISessionService>();
                    ISessionService sessionService = sessionSvc;
                    Session entity = new Session();
                    entity.Product = new Product()
                    {
                        Id = ProductId
                    };
                    entity.Version = Paths.InstalledVersion;

                    session = await sessionService.AddAsync( entity, token );
                    TimeSpan interval = TimeSpan.FromMinutes( 5.0 );
                    long lastNewsId = 0;
                    int unauth = 0;
                    messageSvc = CommunityServicesRegistry.GetService<IMessageService>();

                    while ( true )
                    {
                        try
                        {
                            await interval.Delay( token );
                            await sessionSvc.UpdateAsync( session, token );

                            if ( newsReceieved != null )
                            {
                                var result = await messageSvc.FindSystemAsync( 0L, 20, false, null, null, null, token );

                                foreach ( SystemMessage systemMessage in result.Items )
                                {
                                    newsReceieved( systemMessage );
                                    lastNewsId = systemMessage.Id.Max( lastNewsId );
                                }
                            }
                            unauth = 0;
                        }
                        catch ( Exception ex )
                        {
                            if ( !( ex is OperationCanceledException ) )
                            {
                                ex.LogError( null );
                                HttpRequestException requestEx = ex as HttpRequestException;

                                if ( requestEx != null )
                                {
                                    HttpStatusCode? statusCode = requestEx.TryGetStatusCode();
                                    if ( statusCode.HasValue )
                                    {
                                        switch ( statusCode.GetValueOrDefault() )
                                        {
                                            case HttpStatusCode.Unauthorized:
                                            case HttpStatusCode.Forbidden:
                                            {
                                                if ( ++unauth > 5 )
                                                {
                                                    Action action = disconnected;
                                                    if ( action != null )
                                                    {
                                                        action();
                                                        sessionSvc = null;
                                                        session = null;
                                                        messageSvc = null;

                                                        return;
                                                    }
                                                    else
                                                    {
                                                        sessionSvc = null;
                                                        session = null;
                                                        messageSvc = null;

                                                        return;
                                                    }

                                                }
                                                else
                                                    continue;
                                            }

                                            default:
                                            continue;
                                        }
                                    }
                                }
                            }
                            else
                                break;
                        }
                    }
                    session.Logout = true;
                    try
                    {
                        var mySession = sessionSvc.UpdateAsync( session, new CancellationToken() );
                    }
                    catch ( Exception ex )
                    {
                        ex.LogError( null );
                    }
                }
                catch ( Exception ex )
                {
                    sessionSvc = null;
                    session = null;
                    messageSvc = null;
                    return;
                }

            } );
        }

        private class HtmlFormatter : IHtmlFormatter
        {
            ValueTask<string> IHtmlFormatter.CleanAsync(
              string text,
              CancellationToken cancellationToken )
            {
                return CommunityServicesRegistry.GetService<IMessageService>().BodyCleanAsync( text, cancellationToken ).AsValueTask();
            }

            ValueTask<string> IHtmlFormatter.ActivateRuleAsync(
              string text,
              object rule,
              object context,
              CancellationToken cancellationToken )
            {
                throw new NotSupportedException();
            }

            ValueTask<string> IHtmlFormatter.ToHtmlAsync(
              string text,
              object context,
              CancellationToken cancellationToken )
            {
                IMessageService service = CommunityServicesRegistry.GetService<IMessageService>();
                string body = text;
                long currentDomain = CurrentDomain;
                CancellationToken cancellationToken1 = cancellationToken;
                int? truncate = new int?();
                bool? preventScaling = new bool?();
                CancellationToken cancellationToken2 = cancellationToken1;
                return service.BodyToHtmlAsync( body, currentDomain, truncate, preventScaling, cancellationToken2 ).AsValueTask();
            }
        }
    }
}

