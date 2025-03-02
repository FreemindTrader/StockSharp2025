using Ecng.Collections;
using Ecng.Common;
using Ecng.Net;
using StockSharp.Localization;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockSharp.Studio.WebApi
{
    /// <summary>
    /// Represents a provider for OAuth authentication in a Web API.
    /// </summary>
    public class WebApiOAuthProvider : IOAuthProvider
    {
        private readonly SynchronizedDictionary<long, IOAuthToken> _cache = new SynchronizedDictionary<long, IOAuthToken>();
        private TimeSpan _timeOut = TimeSpan.FromMinutes(5.0);

        /// <summary>The timeout for the OAuth authentication.</summary>
        public TimeSpan TimeOut
        {
            get
            {
                return this._timeOut;
            }
            set
            {
                if ( value <= TimeSpan.Zero )
                    throw new ArgumentOutOfRangeException( nameof( value ), ( object ) value, LocalizedStrings.InvalidValue );
                this._timeOut = value;
            }
        }

        async Task<IOAuthToken> IOAuthProvider.RequestToken( long socialId, bool isDemo, CancellationToken cancellationToken )
        {
            IOAuthToken oauthToken;
            if ( this._cache.TryGetValue( socialId, out oauthToken ) )
            {
                DateTime? expires = oauthToken.Expires;
                if ( !expires.HasValue || !( expires.GetValueOrDefault() < DateTime.UtcNow ) )
                    goto label_8;
            }
            ISocialService svc = WebApiServicesRegistry.GetService<ISocialService>();
            long domainId = WebApiHelper.CurrentDomain;
            oauthToken = ( IOAuthToken ) await svc.TryGetAccessTokenAsync( socialId, isDemo, domainId, cancellationToken );
            if ( oauthToken == null )
                oauthToken = await this.OpenBrowser( svc, socialId, isDemo, domainId, cancellationToken );
            if ( oauthToken != null )
                this._cache[ socialId ] = oauthToken;
            svc = ( ISocialService ) null;
        label_8:
            return oauthToken;
        }

        /// <summary>
        /// Opens a browser to perform OAuth authentication and returns the access token.
        /// </summary>
        /// <param name="svc">The social service.</param>
        /// <param name="socialId">The social ID.</param>
        /// <param name="isDemo"></param>
        /// <param name="domainId"></param>
        /// <param name="cts">The cancellation token.</param>
        /// <returns>The access token.</returns>
        protected virtual async Task<IOAuthToken> OpenBrowser( ISocialService svc, long socialId, bool isDemo, long domainId, CancellationToken cts )
        {
            CancellationToken token;
            IOAuthToken result1;
            try
            {
                
                var result = await svc.GetOAuthRequestAsync(socialId, isDemo, domainId, null, null, null, cts);
                
                IOHelper.OpenLink( result, true );
                TimeSpan interval = TimeSpan.FromSeconds(5.0);
                token = AsyncHelper.CreateChildToken( cts, new TimeSpan?( this.TimeOut ) ).Item2;
                
                while ( !token.IsCancellationRequested )
                {
                    await AsyncHelper.Delay(interval, token);
                    
                    var result99 =  await svc.TryGetAccessTokenAsync(socialId, isDemo, domainId, token);
                                        
                    if ( result99 != null )
                    {
                        return( ( IOAuthToken ) result99 );                        
                    }
                }                
            }
            catch ( Exception ex )
            {
                
            }
            return null;
        }
    }
}
