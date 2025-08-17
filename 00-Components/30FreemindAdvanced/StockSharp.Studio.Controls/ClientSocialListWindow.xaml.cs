// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.ClientSocialListWindow
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Xpf.Core;
using Ecng.Logging;
using Ecng.Xaml;
using StockSharp.Studio.WebApi;
using StockSharp.Web.Api.Interfaces;
using StockSharp.Web.DomainModel;

#nullable disable
namespace StockSharp.Studio.Controls;

public partial class ClientSocialListWindow : ThemedWindow, IComponentConnector
{
    public static readonly RoutedCommand SendCommand = new RoutedCommand();
    private readonly IClientSocialService _svc;

    private static IList<ClientSocial> Source => WebApiHelper.ClientSocialsSource;

    public ClientSocialListWindow()
    {
        this.InitializeComponent();
        if (this.IsDesignMode())
            return;
        this.SocialsGrid.ItemsSource = (object)ClientSocialListWindow.Source;
        this._svc = WebApiServicesRegistry.GetService<IClientSocialService>();
    }

    private void OnLoaded(object sender, RoutedEventArgs e) => this.RefreshBtn_Click(sender, e);

    private void RemoveBtn_Click(object sender, RoutedEventArgs e)
    {
        ClientSocial selectedItem = (ClientSocial)this.SocialsGrid.SelectedItem;
        ClientSocialListWindow.Source.Remove(selectedItem);
        LoggingHelper.ObserveErrorAndLog((Task)this._svc.DeleteAsync(selectedItem.Id));
    }

    private void RefreshBtn_Click(object sender, RoutedEventArgs e)
    {
        LoggingHelper.ObserveErrorAndLog(this._svc.RefreshAsync());
    }

    private void SendCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        LoggingHelper.ObserveErrorAndLog((Task)this._svc.SendAsync("Test message", new long[1]
        {
      ((BaseEntity) e.Parameter).Id
        }, Array.Empty<long>()));
    }


}
