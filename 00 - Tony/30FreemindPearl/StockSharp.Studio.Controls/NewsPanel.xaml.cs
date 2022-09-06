// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Controls.NewsPanel
// Assembly: StockSharp.Studio.Controls, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D457EB08-5750-4F4B-A104-96BE70F84CCF
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Controls.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Alerts;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using StockSharp.Studio.Core;
using StockSharp.Studio.Core.Commands;
using StockSharp.Xaml;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace StockSharp.Studio.Controls
{
    [DisplayNameLoc( "News" )]
    [DescriptionLoc( "Str3238", false )]
    [VectorIcon( "Telegram" )]
    public partial class NewsPanel : BaseStudioControl, IComponentConnector
    {
        public static readonly DependencyProperty SubscribeNewsProperty = DependencyProperty.Register(
            nameof( SubscribeNews ),
            typeof( bool ),
            typeof( NewsPanel ),
            new PropertyMetadata( new PropertyChangedCallback( SubscribeNewsChanged ) ) );
        private readonly SubscriptionManager _subscriptionManager;
        private Subscription _subscription;

        private static void SubscribeNewsChanged( DependencyObject sender, DependencyPropertyChangedEventArgs args )
        {
            NewsPanel newsPanel = ( NewsPanel )sender;
            if ( ( bool )args.NewValue )
            {
                newsPanel._subscription = newsPanel._subscriptionManager.CreateSubscription( DataType.News );
            }
            else if ( newsPanel._subscription != null )
            {
                newsPanel._subscriptionManager.RemoveSubscription( newsPanel._subscription );
                newsPanel._subscription = null;
            }
            newsPanel.RaiseChangedCommand();
        }

        public bool SubscribeNews
        {
            get
            {
                return ( bool )this.GetValue( NewsPanel.SubscribeNewsProperty );
            }
            set
            {
                this.SetValue( NewsPanel.SubscribeNewsProperty, ( object )value );
            }
        }

        public NewsPanel()
        {
            this.InitializeComponent();
            this._subscriptionManager = new SubscriptionManager( ( IStudioControl )this );
            this.NewsGrid.NewsProvider = ServicesRegistry.NewsProvider;
            this.NewsGrid.LayoutChanged += RaiseChangedCommand;
            this.NewsGrid.SelectionChanged += ( GridSelectionChangedEventHandler )( ( s, e ) => new SelectCommand<News>( this.NewsGrid.FirstSelectedNews, false ).Process( ( object )this, false ) );
            this.AlertBtn.SchemaChanged += RaiseChangedCommand;
            this.GotFocus += ( RoutedEventHandler )( ( s, e ) => new SelectCommand<News>( this.NewsGrid.FirstSelectedNews, false ).Process( ( object )this, false ) );
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.Register<EntityCommand<News>>( ( object )this, false, ( Action<EntityCommand<News>> )( cmd => this.NewsGrid.News.AddRange( cmd.Entities.Where<News>( ( Func<News, bool> )( n => !n.IsStockSharp() ) ) ) ), ( Func<EntityCommand<News>, bool> )null );
            commandService.Register<ResetedCommand>( ( object )this, false, ( Action<ResetedCommand> )( cmd => this.NewsGrid.News.Clear() ), ( Func<ResetedCommand, bool> )null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "NewsGrid", this.NewsGrid.Save() );
            storage.SetValue<SettingsStorage>( "AlertBtn", this.AlertBtn.Save() );
            storage.SetValue<string>( "BarManager", this.BarManager.SaveDevExpressControl() );
            storage.SetValue<bool>( "SubscribeNews", this.SubscribeNews );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            this.NewsGrid.Load( storage.GetValue<SettingsStorage>( "NewsGrid", ( SettingsStorage )null ) );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "AlertBtn", ( SettingsStorage )null );
            if ( storage1 != null )
                this.AlertBtn.Load( storage1 );
            string settings = storage.GetValue<string>( "BarManager", ( string )null );
            if ( settings != null )
                this.BarManager.LoadDevExpressControl( settings );
            this.SubscribeNews = storage.GetValue<bool>( "SubscribeNews", false );
        }

        public override void Dispose( CloseReason reason )
        {
            IStudioCommandService commandService = BaseStudioControl.CommandService;
            commandService.UnRegister<EntityCommand<News>>( ( object )this );
            commandService.UnRegister<ResetedCommand>( ( object )this );
            if ( reason == CloseReason.CloseWindow )
                this.AlertBtn.Dispose();
            this._subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        
    }
}
