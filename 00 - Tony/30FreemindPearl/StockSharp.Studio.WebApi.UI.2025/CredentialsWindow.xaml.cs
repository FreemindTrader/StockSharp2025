// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.CredentialsWindow
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A13266F-BFDB-4F15-BB1A-891982F4135B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.WebApi.UI.dll

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
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Studio.Controls
{
    public partial class CredentialsWindow : ThemedWindow
    {
        private bool _isLoggedIn;
        private CancellationTokenSource _cts;

        public CredentialsWindow()
        {
            this.InitializeComponent();
            this.RegisterLink.EditValue = ( object ) Paths.GetPageUrl( 252L, ( object ) null );
            this.ForgotLink.EditValue = ( object ) Paths.GetPageUrl( 253L, ( object ) null );
            this.AccessTokenLink.EditValue = ( object ) Paths.GetPageUrl( 243L, ( object ) null );
        }

        public string Email
        {
            get
            {
                return this.EmailCtrl.Text;
            }
            set
            {
                this.EmailCtrl.Text = value;
            }
        }

        public SecureString Password
        {
            get
            {
                return StringHelper.Secure( this.PasswordCtrl.Password );
            }
            set
            {
                this.PasswordCtrl.Password = StringHelper.UnSecure( value );
            }
        }

        public SecureString AccessToken
        {
            get
            {
                return StringHelper.Secure( this.AccessTokenCtrl.Text );
            }
            set
            {
                this.AccessTokenCtrl.Text = StringHelper.UnSecure( value );
            }
        }

        public bool AutoLogon
        {
            get
            {
                return this.AutoLogonCtrl.IsChecked.GetValueOrDefault();
            }
            set
            {
                this.AutoLogonCtrl.IsChecked = new bool?( value );
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return this._isLoggedIn;
            }
            set
            {
                this._isLoggedIn = value;
                this.EmailCtrl.IsReadOnly = value;
                this.PasswordCtrl.IsEnabled = !value;
            }
        }

        public IClientService ClientService { get; set; }


        public static Task<(ServerCredentials credentials, bool autoLogon)> TryShow( DependencyObject owner, ServerCredentials credentials )
        {
            IClientService serviceAsAnonymous = WebApiServicesRegistry.GetServiceAsAnonymous<IClientService>();
            return CredentialsWindow.TryShow( owner, serviceAsAnonymous, credentials );
        }


        public static async Task<(ServerCredentials credentials, bool autoLogon)> TryShow( DependencyObject owner, IClientService clientSvc, ServerCredentials credentials )
        {
            ValueTuple<ServerCredentials, bool> serverResponse = ( null, false );
            try
            {
                if ( owner == null )
                    throw new ArgumentNullException( nameof( owner ) );

                if ( clientSvc == null )
                    throw new ArgumentNullException( nameof( clientSvc ) );

                bool autoLogon = true;
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10.0));
                try
                {
                    var result = await clientSvc.GetCurrentAsync(new bool?(), (string) null, cancellationTokenSource.Token);

                    ServerCredentials serverCredentials = new ServerCredentials();
                    serverCredentials.Email = ( result.Email );
                    serverCredentials.Token = ( StringHelper.Secure( result.GetAccessToken() ) );
                    credentials = serverCredentials;
                }
                catch
                {
                    GuiDispatcher.GlobalDispatcher.AddSyncAction( ( Action ) ( () =>
                    {
                        CredentialsWindow wnd = new CredentialsWindow()
                        {
                            IsLoggedIn = false,
                            ClientService = clientSvc
                        };
                        if ( credentials != null )
                        {
                            wnd.Email = credentials.Email;
                            wnd.Password = credentials.Password;
                            wnd.AccessToken = credentials.Token;
                            wnd.AutoLogon = Ecng.ComponentModel.Extensions.CanAutoLogin( credentials );
                        }
                        else
                            wnd.AutoLogon = true;
                        if ( wnd.ShowModal( owner ) )
                        {
                            autoLogon = wnd.AutoLogon;
                            ServerCredentials serverCredentials = new ServerCredentials();
                            serverCredentials.Email = ( wnd.Email );
                            serverCredentials.Password = ( wnd.Password );
                            serverCredentials.Token = ( wnd.AccessToken );
                            credentials = serverCredentials;
                        }
                        else
                        {
                            credentials = ( ServerCredentials ) null;
                            autoLogon = false;
                        }
                    } ) );
                }
                serverResponse = (credentials, autoLogon);
            }
            catch ( Exception ex )
            {

            }

            return serverResponse;
        }

        private void Proxy_OnClick( object sender, HyperlinkEditRequestNavigationEventArgs e )
        {
            BaseApplication.EditProxySettings( ( Window ) this );
            e.Handled = true;
        }

        protected override void OnClosed( EventArgs e )
        {
            this._cts?.Cancel();
            base.OnClosed( e );
        }

        private void TabCtrl_SelectionChanged( object sender, TabControlSelectionChangedEventArgs e )
        {
            this._cts?.Cancel();
            this._cts = ( CancellationTokenSource ) null;
            this._cts = new CancellationTokenSource();
            CancellationToken token = this._cts.Token;
            if ( this.TabCtrl.SelectedItem != this.OAuthTab )
                return;
            displayLeft( 3 );
            Task.Run( ( Func<Task> ) ( async () =>
            {
                try
                {
                    for ( int i = 1; i < 3; ++i )
                    {
                        await AsyncHelper.Delay( TimeSpan.FromSeconds( 1.0 ), token );
                        int left = 3 - i;
                        this.GuiAsync( ( Action ) ( () => displayLeft( left ) ) );
                    }
                    string salt = Ecng.ComponentModel.Extensions.ToN(Guid.NewGuid());
                    if ( !IOHelper.OpenLink( Paths.GetPageUrl( 251L, ( object ) null ) + "?ss_oauth=" + salt, false ) )
                        return;
                    IClientService clientSvc = this.ClientService ?? WebApiServicesRegistry.GetServiceAsAnonymous<IClientService>();
                    while ( !token.IsCancellationRequested )
                    {
                        int num;
                        try
                        {
                            Client client = await clientSvc.GetCurrentAsync(new bool?(), salt, token);
                            ( this ).GuiAsync( ( Action ) ( () =>
                      {
                          this.Email = client.Email;
                          this.AccessToken = StringHelper.Secure( client.GetAccessToken() );
                          this.DialogResult = new bool?( true );
                      } ) );
                            break;
                        }
                        catch
                        {
                            num = 1;
                        }
                        if ( num == 1 )
                        {
                            if ( !token.IsCancellationRequested )
                                await AsyncHelper.Delay( TimeSpan.FromSeconds( 5.0 ), token );
                            else
                                break;
                        }
                    }
                    salt = ( string ) null;
                    clientSvc = ( IClientService ) null;
                }
                catch ( Exception ex )
                {
                    if ( token.IsCancellationRequested )
                        return;
                    LoggingHelper.LogError( ex, ( string ) null );
                }
            } ), token );

            void displayLeft( int left )
            {
                this.LeftSeconds.Text = StringHelper.Put( LocalizedStrings.LeftSeconds, new object [1]
                {
           left
                } );
            }
        }


    }
}
