// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ClientSocialListWindow
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Core;
using Ecng.Logging;
using Ecng.Xaml;
using StockSharp.Studio.WebApi;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StockSharp.Studio.Controls
{
    public partial class ClientSocialListWindow : ThemedWindow
    {
        public static readonly RoutedCommand SendCommand = new RoutedCommand();
        private readonly IClientSocialService _svc;
        
        private static IList<ClientSocial> Source
        {
            get
            {
                return WebApiHelper.ClientSocialsSource;
            }
        }

        public ClientSocialListWindow()
        {
            this.InitializeComponent();
            if ( this.IsDesignMode() )
                return;
            this.SocialsGrid.ItemsSource =  ClientSocialListWindow.Source;
            this._svc = WebApiServicesRegistry.GetService<IClientSocialService>();
        }

        private void OnLoaded( object sender, RoutedEventArgs e )
        {
            this.RefreshBtn_Click( sender, e );
        }

        private void RemoveBtn_Click( object sender, RoutedEventArgs e )
        {
            ClientSocial selectedItem = (ClientSocial) this.SocialsGrid.SelectedItem;
            ClientSocialListWindow.Source.Remove( selectedItem );
            LoggingHelper.ObserveErrorAndLog( ( Task ) this._svc.DeleteAsync( selectedItem.Id, new CancellationToken() ) );
        }

        private void RefreshBtn_Click( object sender, RoutedEventArgs e )
        {
            LoggingHelper.ObserveErrorAndLog( this._svc.RefreshAsync( new CancellationToken() ) );
        }

        private void SendCommand_OnExecuted( object sender, ExecutedRoutedEventArgs e )
        {
            LoggingHelper.ObserveErrorAndLog( ( Task ) this._svc.SendAsync( "Test message", new long [1]
            {
        ((BaseEntity) e.Parameter).Id
            }, Array.Empty<long>(), new long?(), new DateTime?(), new CancellationToken() ) );
        }

        
    }
}
