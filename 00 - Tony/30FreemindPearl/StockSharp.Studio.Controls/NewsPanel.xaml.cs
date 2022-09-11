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
        public static readonly DependencyProperty SubscribeNewsProperty = DependencyProperty.Register( nameof( SubscribeNews ), typeof( bool ), typeof( NewsPanel ), new PropertyMetadata( new PropertyChangedCallback( SubscribeNewsChanged ) ) );
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
                return ( bool )GetValue( SubscribeNewsProperty );
            }
            set
            {
                SetValue( SubscribeNewsProperty, value );
            }
        }

        public NewsPanel()
        {
            InitializeComponent();
            _subscriptionManager = new SubscriptionManager( this );
            NewsGrid.NewsProvider = ServicesRegistry.NewsProvider;
            NewsGrid.LayoutChanged += RaiseChangedCommand;
            NewsGrid.SelectionChanged += ( s, e ) => new SelectCommand<News>( NewsGrid.FirstSelectedNews, false ).Process( this, false );
            AlertBtn.SchemaChanged += RaiseChangedCommand;
            GotFocus += ( s, e ) => new SelectCommand<News>( NewsGrid.FirstSelectedNews, false ).Process( this, false );
            IStudioCommandService commandService = CommandService;
            commandService.Register<EntityCommand<News>>( this, false, cmd => NewsGrid.News.AddRange( cmd.Entities.Where( n => !n.IsStockSharp() ) ), null );
            commandService.Register<ResetedCommand>( this, false, cmd => NewsGrid.News.Clear(), null );
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue( "NewsGrid", NewsGrid.Save() );
            storage.SetValue( "AlertBtn", AlertBtn.Save() );
            storage.SetValue( "BarManager", BarManager.SaveDevExpressControl() );
            storage.SetValue( "SubscribeNews", SubscribeNews );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            NewsGrid.Load( storage.GetValue<SettingsStorage>( "NewsGrid", null ) );
            SettingsStorage storage1 = storage.GetValue<SettingsStorage>( "AlertBtn", null );
            if ( storage1 != null )
                AlertBtn.Load( storage1 );
            string settings = storage.GetValue<string>( "BarManager", null );
            if ( settings != null )
                BarManager.LoadDevExpressControl( settings );
            SubscribeNews = storage.GetValue( "SubscribeNews", false );
        }

        public override void Dispose( CloseReason reason )
        {
            IStudioCommandService commandService = CommandService;
            commandService.UnRegister<EntityCommand<News>>( this );
            commandService.UnRegister<ResetedCommand>( this );
            if ( reason == CloseReason.CloseWindow )
                AlertBtn.Dispose();
            _subscriptionManager.Dispose();
            base.Dispose( reason );
        }

        
    }
}
