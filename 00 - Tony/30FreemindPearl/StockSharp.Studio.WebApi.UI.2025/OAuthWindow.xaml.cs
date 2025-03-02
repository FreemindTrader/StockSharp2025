// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.OAuthWindow
// Assembly: StockSharp.Studio.WebApi.UI, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A13266F-BFDB-4F15-BB1A-891982F4135B
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.WebApi.UI.dll

using DevExpress.Xpf.Core;
using Ecng.Common;
using Ecng.Logging;
using Ecng.Net;
using Ecng.Xaml;
using StockSharp.Studio.WebApi;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Studio.Controls
{
    public partial class OAuthWindow : ThemedWindow
    {
        private bool _closing;
        private CancellationTokenSource _cts;

        public OAuthWindow()
        {
            this.InitializeComponent();
        }

        public long SocialId { get; set; }

        public long DomainId { get; set; }

        public bool IsDemo { get; set; }

        public ISocialService Service { get; set; }

        public IOAuthToken AccessToken { get; set; }

        public CancellationToken CancellationToken { get; set; }

        private void StartBtn_Click( object sender, RoutedEventArgs e )
        {
            this.StartBtn.IsEnabled = false;
            if ( WebApiServicesRegistry.Offline )
            {
                this.ShowOfflineWarning();
            }
            else
            {
                this._cts = AsyncHelper.CreateChildToken( this.CancellationToken, new TimeSpan?() ).Item1;
                CancellationToken token = this._cts.Token;
                Action action1;
                Action action2;
                Task.Run( ( Func<Task> ) ( async () =>
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
                        ( await service.GetOAuthRequestAsync( socialId, num != 0, domainId, ( string ) null, returnPageId, ( string ) null, cancellationToken2 ) ).TryOpenLink( ( DependencyObject ) this );
                        TimeSpan interval = TimeSpan.FromSeconds(5.0);
                        while ( !token.IsCancellationRequested )
                        {
                            await AsyncHelper.Delay( interval, token );
                            SocialToken accessTokenAsync = await this.Service.TryGetAccessTokenAsync(this.SocialId, this.IsDemo, this.DomainId, token);
                            if ( accessTokenAsync != null )
                            {
                                this.AccessToken = ( IOAuthToken ) accessTokenAsync;
                                //        ( ( DispatcherObject ) this ).GuiAsync( action1 ?? ( action1 = ( Action ) ( () =>
                                //{
                                //    if ( this._closing )
                                //        return;
                                //    this.DialogResult = new bool?( true );
                                //} ) ) );
                                //        break;
                            }
                        }
                        interval = new TimeSpan();
                    }
                    catch ( Exception ex )
                    {
                        if ( token.IsCancellationRequested )
                            return;
                        LoggingHelper.LogError( ex, ( string ) null );
                    }
                    finally
                    {
                        int num;
                        //    if ( num < 0 && token.IsCancellationRequested && this.AccessToken == null )
                        //        ( ( DispatcherObject ) this ).GuiAsync( action2 ?? ( action2 = ( Action ) ( () =>
                        //{
                        //    if ( this._closing )
                        //        return;
                        //    this.DialogResult = new bool?( false );
                        //} ) ) );
                    }
                } ), token );
            }
        }

        protected override void OnClosed( EventArgs e )
        {
            this._closing = true;
            this._cts?.Cancel();
            base.OnClosed( e );
        }
    }
}
