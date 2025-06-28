// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.CredentialsWindow
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5447883D-4177-4B33-881A-41D29A366FB7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.WebApi.UI.dll

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
using Extensions = Ecng.ComponentModel.Extensions;

#nullable enable
namespace StockSharp.Studio.Controls;

public partial class CredentialsWindow : ThemedWindow, IComponentConnector
{
    private bool _isLoggedIn;
    private
#nullable disable
    CancellationTokenSource _cts;
    
    public CredentialsWindow()
    {
        this.InitializeComponent();
        this.RegisterLink.EditValue = (object)Paths.GetPageUrl(252L, (object)null);
        this.ForgotLink.EditValue = (object)Paths.GetPageUrl(253L, (object)null);
        this.AccessTokenLink.EditValue = (object)Paths.GetPageUrl(243L, (object)null);
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
            throw new ArgumentNullException(nameof(owner));
        if (clientSvc == null)
            throw new ArgumentNullException(nameof(clientSvc));
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
            GuiDispatcher.GlobalDispatcher.AddSyncAction((Action)(() =>
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
                    credentials = (ServerCredentials)null;
                    autoLogon = false;
                }
            }));
        }
        return (credentials, autoLogon);
    }

    private void Proxy_OnClick(object sender, HyperlinkEditRequestNavigationEventArgs e)
    {
        BaseApplication.EditProxySettings((Window)this);
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
        this._cts = (CancellationTokenSource)null;
        this._cts = new CancellationTokenSource();
        CancellationToken token = this._cts.Token;
        if (this.TabCtrl.SelectedItem != this.OAuthTab)
            return;
        displayLeft(3);
        Task.Run((Func<Task>)(async () =>
        {
            try
            {
                for (int i = 1; i < 3; ++i)
                {
                    await AsyncHelper.Delay(TimeSpan.FromSeconds(1.0), token);
                    int left = 3 - i;
                    ((DispatcherObject)this).GuiAsync((Action)(() => displayLeft(left)));
                }
                string salt = Extensions.ToN(Guid.NewGuid());
                if (!IOHelper.OpenLink($"{Paths.GetPageUrl(251L, (object)null)}?ss_oauth={salt}", false))
                    return;
                IClientService clientSvc = this.ClientService ?? WebApiServicesRegistry.GetServiceAsAnonymous<IClientService>();
                while (!token.IsCancellationRequested)
                {
                    int num;
                    try
                    {
                        Client client = await clientSvc.GetCurrentAsync(salt: salt, cancellationToken: token);
                        ((DispatcherObject)this).GuiAsync((Action)(() =>
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
                salt = (string)null;
                clientSvc = (IClientService)null;
            }
            catch (Exception ex)
            {
                if (token.IsCancellationRequested)
                    return;
                LoggingHelper.LogError(ex, (string)null);
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
    
}
