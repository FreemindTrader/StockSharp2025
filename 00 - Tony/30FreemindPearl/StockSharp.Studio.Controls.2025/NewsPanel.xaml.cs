// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.NewsPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98BFC097-7702-4266-94A7-7FF1B7400370
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Controls.dll

using DevExpress.Mvvm;
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
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace StockSharp.Studio.Controls
{
    [Display( Description = "NewsPanel", Name = "News", ResourceType = typeof( LocalizedStrings ) )]
    [VectorIcon( "Mail" )]
    [Guid( "1334EA89-A152-48D1-AD6D-F05D9F89F3A4" )]
    [Doc( "topics/topics/terminal/user_interface/components/news.html" )]
    public partial class NewsPanel : BaseStudioControl
    {
        public static readonly DependencyProperty SubscribeNewsProperty = DependencyProperty.Register( nameof( SubscribeNews ), typeof( bool ), typeof( NewsPanel ), new PropertyMetadata( new PropertyChangedCallback( SubscribeNewsChanged ) ) );
        private readonly SubscriptionManager _subscriptionManager;
        private Subscription _subscription;
        
        private static void SubscribeNewsChanged( DependencyObject sender, DependencyPropertyChangedEventArgs args )
        {
            NewsPanel newsPanel = (NewsPanel) sender;
            if ( ( bool ) args.NewValue )
                newsPanel._subscription = newsPanel._subscriptionManager.CreateSubscription( EntitiesExtensions.NewsSecurity, StockSharp.Messages.DataType.News, ( Action<Subscription> ) ( s =>
                {
                    MarketDataMessage marketData = s.MarketData;
                    marketData.From = new DateTimeOffset?( ( DateTimeOffset ) Paths.Birthday );
                    marketData.Count = new long?( 100L );
                } ) );
            else if ( newsPanel._subscription != null )
            {
                newsPanel._subscriptionManager.RemoveSubscription( newsPanel._subscription );
                newsPanel._subscription = ( Subscription ) null;
            }
            newsPanel.RaiseChangedCommand();
        }

        public bool SubscribeNews
        {
            get
            {
                return ( bool ) this.GetValue( NewsPanel.SubscribeNewsProperty );
            }
            set
            {
                this.SetValue( NewsPanel.SubscribeNewsProperty,  value );
            }
        }

        public ICommand SearchCommand { get; }

        public NewsPanel()
        {
            this.SearchCommand = ( ICommand ) new Ecng.Xaml.DelegateCommand( ( Action ) ( () =>
            {
                SecurityPickerWindow wnd = new SecurityPickerWindow()
                {
                    SecurityProvider = ServicesRegistry.SecurityProvider,
                    SelectionMode = MultiSelectMode.None
                };
                if ( !wnd.ShowModal( ( DependencyObject ) this ) )
                    return;
                this._subscriptionManager.CreateSubscription( wnd.SelectedSecurity, StockSharp.Messages.DataType.News, ( Action<Subscription> ) null );
            } ) );
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl ) this );
            this.NewsGrid.NewsProvider = ServicesRegistry.NewsProvider;
            this.NewsGrid.LayoutChanged += RaiseChangedCommand;
            this.NewsGrid.SelectionChanged += ( GridSelectionChangedEventHandler ) ( ( s, e ) => this.NewsGrid.FirstSelectedNews.SendSelect<News>(  this, false ) );
            this.GotFocus += ( RoutedEventHandler ) ( ( s, e ) => this.NewsGrid.FirstSelectedNews.SendSelect<News>(  this, false ) );
            this.Register<EntityCommand<News>>(  this, false, ( Action<EntityCommand<News>> ) ( cmd =>
            {
                if ( cmd.Entity.IsStockSharp() )
                    return;
                ( ( ICollection<News> ) this.NewsGrid.News ).Add( cmd.Entity );
            } ), ( Func<EntityCommand<News>, bool> ) null );
            this.Register<ResetedCommand>(  this, false, ( Action<ResetedCommand> ) ( cmd => ( ( ICollection<News> ) this.NewsGrid.News ).Clear() ), ( Func<ResetedCommand, bool> ) null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "NewsGrid",  PersistableHelper.Save( ( IPersistable ) this.NewsGrid ) );
            storage.SetValue<string>( "BarManager",  this.BarManager.SaveDevExpressControl() );
            storage.SetValue<bool>( "SubscribeNews",  ( this.SubscribeNews  ) );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            PersistableHelper.LoadIfNotNull( ( IPersistable ) this.NewsGrid, storage, "NewsGrid" );
            string settings = (string) storage.GetValue<string>("BarManager",  null);
            if ( settings != null )
                this.BarManager.LoadDevExpressControl( settings );
            this.SubscribeNews = ( bool ) storage.GetValue<bool>( "SubscribeNews",  false );
        }

        public override void Dispose( CloseReason reason )
        {
            this.NewsGrid.LayoutChanged -= RaiseChangedCommand;
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        
    }
}
