// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OAuthWindow
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5447883D-4177-4B33-881A-41D29A366FB7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.UI.dll

using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Net;
using StockSharp.Studio.WebApi;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

#nullable enable
namespace StockSharp.Studio.Controls;

public partial class OAuthWindow : ThemedWindow, IComponentConnector
{
  private bool _closing;
  private 
  #nullable disable
  CancellationTokenSource _cts;
  internal SimpleButton StartBtn;
  private bool _contentLoaded;

  public OAuthWindow() => this.InitializeComponent();

  public long SocialId { get; set; }

  public long DomainId { get; set; }

  public bool IsDemo { get; set; }

  public ISocialService Service { get; set; }

  public IOAuthToken AccessToken { get; set; }

  public CancellationToken CancellationToken { get; set; }

  private void StartBtn_Click(object sender, RoutedEventArgs e)
  {
    this.StartBtn.IsEnabled = false;
    if (WebApiServicesRegistry.Offline)
    {
      this.ShowOfflineWarning();
    }
    else
    {
      this._cts = AsyncHelper.CreateChildToken(this.CancellationToken, new TimeSpan?()).Item1;
      CancellationToken token = this._cts.Token;
      Task.Run((Func<Task>) (async () =>
      {
        try
        {
          ISocialService service = this.Service;
          long socialId = this.SocialId;
          int num = this.IsDemo ? 1 : 0;
          long domainId = this.DomainId;
          CancellationToken cancellationToken1 = this.CancellationToken;
          long? returnPageId = new long?();
          CancellationToken cancellationToken2 = cancellationToken1;
          (await service.GetOAuthRequestAsync(socialId, num != 0, domainId, returnPageId: returnPageId, cancellationToken: cancellationToken2)).TryOpenLink((DependencyObject) this);
          TimeSpan interval = TimeSpan.FromSeconds(5.0);
          while (!token.IsCancellationRequested)
          {
            await AsyncHelper.Delay(interval, token);
            SocialToken accessTokenAsync = await this.Service.TryGetAccessTokenAsync(this.SocialId, this.IsDemo, this.DomainId, token);
            if (accessTokenAsync != null)
            {
              this.AccessToken = (IOAuthToken) accessTokenAsync;
              ((DispatcherObject) this).GuiAsync((Action) (() =>
              {
                if (this._closing)
                  return;
                this.DialogResult = new bool?(true);
              }));
              break;
            }
          }
          interval = new TimeSpan();
        }
        catch (Exception ex)
        {
          if (token.IsCancellationRequested)
            return;
          LoggingHelper.LogError(ex, (string) null);
        }
        finally
        {
          int num;
          if (num < 0 && token.IsCancellationRequested && this.AccessToken == null)
            ((DispatcherObject) this).GuiAsync((Action) (() =>
            {
              if (this._closing)
                return;
              this.DialogResult = new bool?(false);
            }));
        }
      }), token);
    }
  }

  protected override void OnClosed(EventArgs e)
  {
    this._closing = true;
    this._cts?.Cancel();
    base.OnClosed(e);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Studio.WebApi.UI;V5.0.0;component/oauthwindow.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
    {
      this.StartBtn = (SimpleButton) target;
      this.StartBtn.Click += new RoutedEventHandler(this.StartBtn_Click);
    }
    else
      this._contentLoaded = true;
  }
}
