// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OAuthProvider
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5447883D-4177-4B33-881A-41D29A366FB7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.UI.dll

using Ecng.Common;
using Ecng.Net;
using Ecng.Xaml;
using StockSharp.Studio.WebApi;
using StockSharp.Web.Api.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace StockSharp.Studio.Controls;

public class OAuthProvider : WebApiOAuthProvider
{
  protected override Task<IOAuthToken> OpenBrowser(
    ISocialService svc,
    long socialId,
    bool isDemo,
    long domainId,
    CancellationToken cancellationToken)
  {
    return AsyncHelper.FromResult<IOAuthToken>(GuiDispatcher.GlobalDispatcher.AddSyncAction<IOAuthToken>((Func<IOAuthToken>) (() =>
    {
      OAuthWindow wnd = new OAuthWindow()
      {
        SocialId = socialId,
        DomainId = domainId,
        IsDemo = isDemo,
        Service = svc,
        CancellationToken = cancellationToken
      };
      return XamlHelper.ShowModal(wnd, Application.Current.MainWindow) ? wnd.AccessToken : (IOAuthToken) null;
    })));
  }
}
