// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.NewsPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE68B1B0-AF00-4133-A0D9-5B961E14FCB1
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Studio.Controls.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using DataType = StockSharp.Messages.DataType;

#nullable disable
namespace StockSharp.Studio.Controls;

[Display(ResourceType = typeof(LocalizedStrings), Name = "News", Description = "NewsPanel")]
[VectorIcon("Mail")]
[Guid("1334EA89-A152-48D1-AD6D-F05D9F89F3A4")]
[Doc("topics/topics/terminal/user_interface/components/news.html")]
public partial class NewsPanel : BaseStudioControl, IComponentConnector
{
    public static readonly DependencyProperty SubscribeNewsProperty = DependencyProperty.Register(nameof(SubscribeNews), typeof(bool), typeof(NewsPanel), new PropertyMetadata(new PropertyChangedCallback(NewsPanel.SubscribeNewsChanged)));
    private readonly SubscriptionManager _subscriptionManager;
    private Subscription _subscription;
    
    private static void SubscribeNewsChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs args)
    {
        NewsPanel newsPanel = (NewsPanel)sender;
        if ((bool)args.NewValue)
            newsPanel._subscription = newsPanel._subscriptionManager.CreateSubscription(EntitiesExtensions.NewsSecurity, DataType.News, (Action<Subscription>)(s =>
            {
                MarketDataMessage marketData = s.MarketData;
                marketData.From = new DateTimeOffset?((DateTimeOffset)Paths.Birthday);
                marketData.Count = new long?(100L);
            }));
        else if (newsPanel._subscription != null)
        {
            newsPanel._subscriptionManager.RemoveSubscription(newsPanel._subscription);
            newsPanel._subscription = (Subscription)null;
        }
        newsPanel.RaiseChangedCommand();
    }

    public bool SubscribeNews
    {
        get => (bool)this.GetValue(NewsPanel.SubscribeNewsProperty);
        set => this.SetValue(NewsPanel.SubscribeNewsProperty, (object)value);
    }

    public ICommand SearchCommand { get; }

    public NewsPanel()
    {
        this.SearchCommand = (ICommand)new DelegateCommand((Action)(() =>
        {
            SecurityPickerWindow wnd = new SecurityPickerWindow()
            {
                SecurityProvider = ServicesRegistry.SecurityProvider,
                SelectionMode = MultiSelectMode.None
            };
            if (!wnd.ShowModal((DependencyObject)this))
                return;
            this._subscriptionManager.CreateSubscription(wnd.SelectedSecurity, DataType.News);
        }));
        this.InitializeComponent();
        this._subscriptionManager = new SubscriptionManager((IStudioControl)this);
        this.NewsGrid.SubscriptionProvider = ServicesRegistry.SubscriptionProvider;
        this.NewsGrid.LayoutChanged += RaiseChangedCommand;
        this.NewsGrid.SelectionChanged += (GridSelectionChangedEventHandler)((s, e) => this.NewsGrid.FirstSelectedNews.SendSelect<News>((object)this));
        this.GotFocus += (RoutedEventHandler)((s, e) => this.NewsGrid.FirstSelectedNews.SendSelect<News>((object)this));
        this.Register<EntityCommand<News>>((object)this, false, (Action<EntityCommand<News>>)(cmd =>
        {
            if (cmd.Entity.IsStockSharp())
                return;
            ((ICollection<News>)this.NewsGrid.News).Add(cmd.Entity);
        }));
        this.Register<ResetedCommand>((object)this, false, (Action<ResetedCommand>)(cmd => ((ICollection<News>)this.NewsGrid.News).Clear()));
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("NewsGrid", PersistableHelper.Save((IPersistable)this.NewsGrid));
        storage.SetValue<string>("BarManager", this.BarManager.SaveDevExpressControl());
        storage.SetValue<bool>("SubscribeNews", this.SubscribeNews);
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.LoadIfNotNull((IPersistable)this.NewsGrid, storage, "NewsGrid");
        string settings = storage.GetValue<string>("BarManager", (string)null);
        if (settings != null)
            this.BarManager.LoadDevExpressControl(settings);
        this.SubscribeNews = storage.GetValue<bool>("SubscribeNews", false);
    }

    public override void Dispose(CloseReason reason)
    {
        this.NewsGrid.LayoutChanged -= RaiseChangedCommand;
        this._subscriptionManager.Dispose();
        base.Dispose(reason);
    }

    
}
