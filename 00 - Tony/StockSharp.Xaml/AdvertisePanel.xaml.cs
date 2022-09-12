using Ecng.Collections;
using Ecng.Common;
using Ecng.Localization;
using Ecng.Web.BBCodes;
using Ecng.Xaml;
using StockSharp.Community;
using StockSharp.Localization;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using TheArtOfDev.HtmlRenderer.WPF;
using Wintellect.PowerCollections;

namespace StockSharp.Xaml
{
    /// <summary>
    /// Interaction logic for AdvertisePanel.xaml
    /// </summary>
    public partial class AdvertisePanel
    {
        public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register(nameof (TextColor), typeof (Color), typeof (AdvertisePanel), new PropertyMetadata( Colors.DarkOrange, new PropertyChangedCallback( OnTextColorPropertyChange )));

        private readonly SynchronizedList<CommunityNews> _news = new SynchronizedList<CommunityNews>();
        private int _index = -1;
        private readonly BBCodeParser _parser;
        private DispatcherTimer _timer;
        private NotificationClient _client;

        private static void OnTextColorPropertyChange( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( AdvertisePanel ) d ).OnNextClick( );

            //( AdvertisePanel  ) d.
        }

        public static readonly DependencyProperty ClientProperty = DependencyProperty.Register(nameof (Client), typeof (NotificationClient), typeof (AdvertisePanel), new PropertyMetadata((object) null, new PropertyChangedCallback( OnClientPropertyChanged )));

        private static void OnClientPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( AdvertisePanel ) d ).NotificationClientChanged( ( NotificationClient ) e.NewValue );
        }

        public AdvertisePanel( )
        {
            InitializeComponent( );

            if ( DesignerProperties.GetIsInDesignMode( this ) )
                return;

            var sizes = new[] { 10, 20, 30, 40, 50, 60, 70, 110, 120 };

            _parser = new BBCodeParser( new[ ]
            {
                new BBTag("b", "<b>", "</b>" , new BBAttribute[0]),
                new BBTag("i", "<span style=\"font-style:italic;\">", "</span>"  ),
                new BBTag("u", "<span style=\"text-decoration:underline;\">", "</span>" ),
                new BBTag("center", "<div style=\"align:center;color:${color};\">", "</div>", new BBAttribute("color", "", c => this.TextColor.ToString() )),                
                new BBTag("size", "<span style=\"font-size:${fontSize}%;\">", "</div>", new BBAttribute("fontSize", "", c => sizes[c.AttributeValue.To<int>()].To<string>())),
                new BBTag("code", "<pre class=\"prettyprint\">", "</pre>"),
                new BBTag("img", "<img src=\"${content}\" />", "", false, true),
                new BBTag("quote", "<blockquote>", "</blockquote>"),
                new BBTag("list", "<ul>", "</ul>"),
                new BBTag("*", "<li>", "</li>", true, false),
                new BBTag("url", "<a href=\"${href}\">", "</a>", new BBAttribute("href", ""), new BBAttribute("href", "href"), new BBAttribute("color", "", c => this.TextColor.ToString() ) ), 
            } );

            _timer = new DispatcherTimer( );
            _timer.Tick += OnTick;
            _timer.Interval = new TimeSpan( 0, 1, 0 );
            _timer.Start( );
        }

        private static string ToString( Color inputColor )
        {
            return string.Format( "#{0:X2}{1:X2}{2:X2}", ( object ) inputColor.R, ( object ) inputColor.G, ( object ) inputColor.B );
        }

        private void OnTick( object sender, EventArgs e )
        {
            OnNextClick( );
        }

        private void OnNextClick(  )
        {
            CommunityNews news;

            lock ( _news.SyncRoot )
            {
                if ( _news.Count == 0 )
                    return;

                if ( _index >= ( _news.Count - 1 ) )
                    _index = 0;
                else
                    _index++;

                news = _news[ _index ];
            }

            ShowNews( news );
        }

        private void OnPrevClick( )
        {
            CommunityNews news;

            lock ( _news.SyncRoot )
            {
                if ( _news.Count == 0 )
                    return;

                if ( _index < 1 )
                    _index = _news.Count - 1;
                else
                    _index--;

                news = _news[ _index ];
            }

            ShowNews( news );
        }

        private void ShowNews( CommunityNews news )
        {
            HtmlPanel.Text = _parser.ToHtml( GetContent( news ) );
        }

        private static string GetContent( CommunityNews news )
        {
            var isRu = LocalizedStrings.ActiveLanguage == Languages.Russian;
            return isRu ? news.RussianBody : news.EnglishBody;
        }

        private void NotificationClientChanged( NotificationClient newClient )
        {
            if ( _client == newClient )
                return;

            if ( _client != null )
            {
                //_client.NewsReceived -= OnNewsReceived;
                //_client.UnSubscribeNews( );
            }

            _client = newClient;

            if ( _client != null )
            {
                //_client.NewsReceived += OnNewsReceived;
                //_client.SubscribeNews( );
            }
        }

        private void OnNewsReceived( CommunityNews news )
        {
            var now = DateTime.UtcNow;

            lock ( _news.SyncRoot )
            {
                _news.RemoveWhere( n => n.EndDate <= now );

                if ( GetContent( news ).IsEmpty( ) )
                    return;

                _news.Add( news );
                _index = 0;
            }

            this.GuiAsync( ( ) => ShowNews( news ) );
        }

        public Color TextColor
        {
            get
            {
                return ( Color ) this.GetValue( AdvertisePanel.TextColorProperty );
            }
            set
            {
                this.SetValue( AdvertisePanel.TextColorProperty, ( object ) value );
            }
        }

        public NotificationClient Client
        {
            get
            {
                return ( NotificationClient ) this.GetValue( AdvertisePanel.ClientProperty );
            }
            set
            {
                if ( _client == value )
                    return;

                if ( _client != null )
                {
                    _client.NewsReceived -= OnNewsReceived;
                    _client.UnSubscribeNews( );
                }

                _client = value;

                if ( _client != null )
                {
                    _client.NewsReceived += OnNewsReceived;
                    _client.SubscribeNews( );
                }

                this.SetValue( AdvertisePanel.ClientProperty, ( object ) value );
            }
        }

        private void SimpleButton_Click_Previous( object sender, RoutedEventArgs e )
        {
            this.OnPrevClick(  );
        }

        private void SimpleButton_Click_Next( object sender, RoutedEventArgs e )
        {
            this.OnNextClick( );
        }

        
    }
}
