// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.CredentialsWindow
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5447883D-4177-4B33-881A-41D29A366FB7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.UI.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Logging;
using Ecng.Xaml;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Studio.WebApi;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.Common;
using StockSharp.Web.DomainModel;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

#nullable enable
namespace StockSharp.Studio.Controls;

public partial class CredentialsWindow : ThemedWindow, IComponentConnector
{
  private bool _isLoggedIn;
  private 
  #nullable disable
  CancellationTokenSource _cts;
  internal DXTabControl TabCtrl;
  internal TextEdit EmailCtrl;
  internal PasswordBoxEdit PasswordCtrl;
  internal HyperlinkEdit AccessTokenLink;
  internal PasswordBoxEdit AccessTokenCtrl;
  internal DXTabItem OAuthTab;
  internal TextBlock LeftSeconds;
  internal CheckEdit AutoLogonCtrl;
  internal HyperlinkEdit RegisterLink;
  internal HyperlinkEdit ForgotLink;
  private bool _contentLoaded;

  public CredentialsWindow()
  {
    this.InitializeComponent();
    this.RegisterLink.EditValue = (object) Paths.GetPageUrl(252L, (object) null);
    this.ForgotLink.EditValue = (object) Paths.GetPageUrl(253L, (object) null);
    this.AccessTokenLink.EditValue = (object) Paths.GetPageUrl(243L, (object) null);
  }

  public string Email
  {
    get => this.EmailCtrl.Text;
    set => this.EmailCtrl.Text = value;
  }

  public SecureString Password
  {
    get => StringHelper.Secure(this.PasswordCtrl.Password);
    set => this.PasswordCtrl.Password = StringHelper.UnSecure(value);
  }

  public SecureString AccessToken
  {
    get => StringHelper.Secure(this.AccessTokenCtrl.Text);
    set => this.AccessTokenCtrl.Text = StringHelper.UnSecure(value);
  }

  public bool AutoLogon
  {
    get => this.AutoLogonCtrl.IsChecked.GetValueOrDefault();
    set => this.AutoLogonCtrl.IsChecked = new bool?(value);
  }

  public bool IsLoggedIn
  {
    get => this._isLoggedIn;
    set
    {
      this._isLoggedIn = value;
      this.EmailCtrl.IsReadOnly = value;
      this.PasswordCtrl.IsEnabled = !value;
    }
  }

  public IClientService ClientService { get; init; }

  public static Task<(ServerCredentials credentials, bool autoLogon)> TryShow(
    DependencyObject owner,
    ServerCredentials credentials)
  {
    IClientService serviceAsAnonymous = WebApiServicesRegistry.GetServiceAsAnonymous<IClientService>();
    return CredentialsWindow.TryShow(owner, serviceAsAnonymous, credentials);
  }

  public static async Task<(ServerCredentials credentials, bool autoLogon)> TryShow(
    DependencyObject owner,
    IClientService clientSvc,
    ServerCredentials credentials)
  {
    if (owner == null)
      throw new ArgumentNullException(nameof (owner));
    if (clientSvc == null)
      throw new ArgumentNullException(nameof (clientSvc));
    bool autoLogon = true;
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10.0));
    try
    {
      Client currentAsync = await clientSvc.GetCurrentAsync(cancellationToken: cancellationTokenSource.Token);
      credentials = new ServerCredentials()
      {
        Email = currentAsync.Email,
        Token = StringHelper.Secure(currentAsync.GetAccessToken())
      };
    }
    catch
    {
      GuiDispatcher.GlobalDispatcher.AddSyncAction((Action) (() =>
      {
        CredentialsWindow wnd = new CredentialsWindow()
        {
          IsLoggedIn = false,
          ClientService = clientSvc
        };
        if (credentials != null)
        {
          wnd.Email = credentials.Email;
          wnd.Password = credentials.Password;
          wnd.AccessToken = credentials.Token;
          wnd.AutoLogon = Extensions.CanAutoLogin(credentials);
        }
        else
          wnd.AutoLogon = true;
        if (wnd.ShowModal(owner))
        {
          autoLogon = wnd.AutoLogon;
          credentials = new ServerCredentials()
          {
            Email = wnd.Email,
            Password = wnd.Password,
            Token = wnd.AccessToken
          };
        }
        else
        {
          credentials = (ServerCredentials) null;
          autoLogon = false;
        }
      }));
    }
    return (credentials, autoLogon);
  }

  private void Proxy_OnClick(object sender, HyperlinkEditRequestNavigationEventArgs e)
  {
    BaseApplication.EditProxySettings((Window) this);
    e.Handled = true;
  }

  protected override void OnClosed(EventArgs e)
  {
    this._cts?.Cancel();
    base.OnClosed(e);
  }

  private void TabCtrl_SelectionChanged(object sender, TabControlSelectionChangedEventArgs e)
  {
    this._cts?.Cancel();
    this._cts = (CancellationTokenSource) null;
    this._cts = new CancellationTokenSource();
    CancellationToken token = this._cts.Token;
    if (this.TabCtrl.SelectedItem != this.OAuthTab)
      return;
    displayLeft(3);
    Task.Run((Func<Task>) (async () =>
    {
      try
      {
        for (int i = 1; i < 3; ++i)
        {
          await AsyncHelper.Delay(TimeSpan.FromSeconds(1.0), token);
          int left = 3 - i;
          ((DispatcherObject) this).GuiAsync((Action) (() => displayLeft(left)));
        }
        string salt = Extensions.ToN(Guid.NewGuid());
        if (!IOHelper.OpenLink($"{Paths.GetPageUrl(251L, (object) null)}?ss_oauth={salt}", false))
          return;
        IClientService clientSvc = this.ClientService ?? WebApiServicesRegistry.GetServiceAsAnonymous<IClientService>();
        while (!token.IsCancellationRequested)
        {
          int num;
          try
          {
            Client client = await clientSvc.GetCurrentAsync(salt: salt, cancellationToken: token);
            ((DispatcherObject) this).GuiAsync((Action) (() =>
            {
              this.Email = client.Email;
              this.AccessToken = StringHelper.Secure(client.GetAccessToken());
              this.DialogResult = new bool?(true);
            }));
            break;
          }
          catch
          {
            num = 1;
          }
          if (num == 1)
          {
            if (!token.IsCancellationRequested)
              await AsyncHelper.Delay(TimeSpan.FromSeconds(5.0), token);
            else
              break;
          }
        }
        salt = (string) null;
        clientSvc = (IClientService) null;
      }
      catch (Exception ex)
      {
        if (token.IsCancellationRequested)
          return;
        LoggingHelper.LogError(ex, (string) null);
      }
    }), token);

    void displayLeft(int left)
    {
      this.LeftSeconds.Text = StringHelper.Put(LocalizedStrings.LeftSeconds, new object[1]
      {
        (object) left
      });
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/StockSharp.Studio.WebApi.UI;V5.0.0;component/credentialswindow.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "9.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.TabCtrl = (DXTabControl) target;
        this.TabCtrl.SelectionChanged += new TabControlSelectionChangedEventHandler(this.TabCtrl_SelectionChanged);
        break;
      case 2:
        this.EmailCtrl = (TextEdit) target;
        break;
      case 3:
        this.PasswordCtrl = (PasswordBoxEdit) target;
        break;
      case 4:
        this.AccessTokenLink = (HyperlinkEdit) target;
        break;
      case 5:
        this.AccessTokenCtrl = (PasswordBoxEdit) target;
        break;
      case 6:
        this.OAuthTab = (DXTabItem) target;
        break;
      case 7:
        this.LeftSeconds = (TextBlock) target;
        break;
      case 8:
        this.AutoLogonCtrl = (CheckEdit) target;
        break;
      case 9:
        this.RegisterLink = (HyperlinkEdit) target;
        break;
      case 10:
        this.ForgotLink = (HyperlinkEdit) target;
        break;
      case 11:
        ((HyperlinkEdit) target).RequestNavigation += new HyperlinkEditRequestNavigationEventHandler(this.Proxy_OnClick);
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
