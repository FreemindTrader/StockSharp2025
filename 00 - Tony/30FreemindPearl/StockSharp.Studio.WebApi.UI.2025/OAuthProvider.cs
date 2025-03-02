// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OAuthProvider
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A13266F-BFDB-4F15-BB1A-891982F4135B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.WebApi.UI.dll

using Ecng.Common;
using Ecng.Net;
using Ecng.Xaml;
using StockSharp.Studio.WebApi;
using StockSharp.Web.Api.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Studio.Controls
{
    public class OAuthProvider : WebApiOAuthProvider
    {
        protected override Task<IOAuthToken> OpenBrowser(
          ISocialService svc,
          long socialId,
          bool isDemo,
          long domainId,
          CancellationToken cancellationToken )
        {
            return ( Task<IOAuthToken> ) AsyncHelper.FromResult<IOAuthToken>( GuiDispatcher.GlobalDispatcher.AddSyncAction<IOAuthToken>( ( Func<IOAuthToken> ) ( () =>
            {
                OAuthWindow wnd = new OAuthWindow()
                {
                    SocialId = socialId,
                    DomainId = domainId,
                    IsDemo = isDemo,
                    Service = svc,
                    CancellationToken = cancellationToken
                };
                if ( XamlHelper.ShowModal( wnd, Application.Current.MainWindow ) )
                    return wnd.AccessToken;
                return ( IOAuthToken ) null;
            } ) ) );
        }
    }
}
