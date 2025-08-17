// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.WebApi.WebApiOAuthProvider
// Assembly: StockSharp.Studio.WebApi, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B97A7121-FFB7-49F4-8E30-FC5C471868BC
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.xml

using System;
using System.Threading;
using System.Threading.Tasks;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Net;
using StockSharp.Localization;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable enable
namespace StockSharp.Studio.WebApi;

/// <summary>
/// Represents a provider for OAuth authentication in a Web API.
/// </summary>
public class WebApiOAuthProvider : IOAuthProvider
{
    private readonly
#nullable disable
    SynchronizedDictionary<long, IOAuthToken> _cache = new SynchronizedDictionary<long, IOAuthToken>();
    private TimeSpan _timeOut = TimeSpan.FromMinutes(5.0);

    /// <summary>The timeout for the OAuth authentication.</summary>
    public TimeSpan TimeOut
    {
        get => this._timeOut;
        set
        {
            this._timeOut = !(value <= TimeSpan.Zero) ? value : throw new ArgumentOutOfRangeException(nameof(value), (object)value, LocalizedStrings.InvalidValue);
        }
    }

    async Task<IOAuthToken> IOAuthProvider.RequestToken(
      long socialId,
      bool isDemo,
      CancellationToken cancellationToken)
    {
        IOAuthToken ioAuthToken;
        if (this._cache.TryGetValue(socialId, out ioAuthToken))
        {
            DateTime? expires = ioAuthToken.Expires;
            if (!expires.HasValue || !(expires.GetValueOrDefault() < DateTime.UtcNow))
                goto label_8;
        }
        ISocialService svc = WebApiServicesRegistry.GetService<ISocialService>();
        long domainId = WebApiHelper.CurrentDomain;
        ioAuthToken = (IOAuthToken)await svc.TryGetAccessTokenAsync(socialId, isDemo, domainId, cancellationToken);
        if (ioAuthToken == null)
            ioAuthToken = await this.OpenBrowser(svc, socialId, isDemo, domainId, cancellationToken);
        if (ioAuthToken != null)
            this._cache[socialId] = ioAuthToken;
        svc = (ISocialService)null;
    label_8:
        return ioAuthToken;
    }

    /// <summary>
    /// Opens a browser to perform OAuth authentication and returns the access token.
    /// </summary>
    /// <param name="svc">The social service.</param>
    /// <param name="socialId">The social ID.</param>
    /// <param name="isDemo"></param>
    /// <param name="domainId"></param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The access token.</returns>
    protected virtual async Task<IOAuthToken> OpenBrowser(
      ISocialService svc,
      long socialId,
      bool isDemo,
      long domainId,
      CancellationToken cancellationToken)
    {
        ISocialService socialService = svc;
        long socialId1 = socialId;
        int num = isDemo ? 1 : 0;
        long domainId1 = domainId;
        CancellationToken cancellationToken1 = cancellationToken;
        long? returnPageId = new long?();
        CancellationToken cancellationToken2 = cancellationToken1;
        IOHelper.OpenLink(await socialService.GetOAuthRequestAsync(socialId1, num != 0, domainId1, returnPageId: returnPageId, cancellationToken: cancellationToken2), true);
        TimeSpan interval = TimeSpan.FromSeconds(5.0);
        CancellationToken token = AsyncHelper.CreateChildToken(cancellationToken, new TimeSpan?(this.TimeOut)).Item2;
        while (!token.IsCancellationRequested)
        {
            await AsyncHelper.Delay(interval, token);
            SocialToken accessTokenAsync = await svc.TryGetAccessTokenAsync(socialId, isDemo, domainId, token);
            if (accessTokenAsync != null)
                return (IOAuthToken)accessTokenAsync;
        }
        return (IOAuthToken)null;
    }
}
