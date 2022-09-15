using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace StockSharp.Xaml
{
    public partial class NewsGrid : BaseGridControl
    {
        public static RoutedCommand RequestStoryCommand = new RoutedCommand();
        public static RoutedCommand OpenUrlCommand = new RoutedCommand();

        private readonly ThreadSafeObservableCollection<StockSharp.BusinessEntities.News> _news;
        private INewsProvider _provider;

        public NewsGrid( )
        {
            InitializeComponent();            

            // Tony: Fix XAML CommandBindings Error in XAML 
            // I moved the CommandBinding here so the blue lines in XAML are gone. Hate to see those.
            var requestStoryCommand = new CommandBinding( RequestStoryCommand, ExecutedRequestStoryCommand, CanExecuteRequestStoryCommand );
            var openUrlCommand      = new CommandBinding( OpenUrlCommand, ExecutedOpenUrlCommand, CanExecuteOpenUrlCommand );
            
            this.CommandBindings.Add( requestStoryCommand );
            this.CommandBindings.Add( openUrlCommand );
            

            var itemsSource = new ObservableCollectionEx<News>();
            ItemsSource = itemsSource;

            _news = new ThreadSafeObservableCollection<News>( itemsSource ) { MaxCount = 10000 };

            ContextMenu.Items.Add( new Separator() );
            
            ContextMenu.Items.Add( new MenuItem { Header = LocalizedStrings.RequestNewsBody, Command = RequestStoryCommand, CommandTarget = this } );           
            ContextMenu.Items.Add( new MenuItem { Header = LocalizedStrings.OpenUrl,         Command = OpenUrlCommand,      CommandTarget = this } );
        }

        public int MaxCount
        {
            get
            {
                return _news.MaxCount;
            }
            set
            {
                _news.MaxCount = value;
            }
        }

        public IListEx< News> News
        {
            get
            {
                return _news;
            }
        }

        public News FirstSelectedNews
        {
            get
            {
                return SelectedNews.FirstOrDefault< News >();
            }
        }

        public IEnumerable< News > SelectedNews
        {
            get
            {
                return SelectedItems.Cast< News >();
            }
        }

        public INewsProvider NewsProvider
        {
            get
            {
                return _provider;
            }
            set
            {
                _provider = value;
            }
        }

        private void ExecutedRequestStoryCommand( object sender, ExecutedRoutedEventArgs e )
        {
            SelectedNews.Where( n => n.Story.IsEmpty() ).ForEach( NewsProvider.RequestNewsStory );
        }

        private void CanExecuteRequestStoryCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = NewsProvider != null && SelectedNews.Any( n => n.Story.IsEmpty() );
        }

        private void CanExecuteOpenUrlCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            var news = SelectedNews;
            e.CanExecute = news.Count() == 1 && FirstSelectedNews.Url != null;
        }

        private void ExecutedOpenUrlCommand( object sender, ExecutedRoutedEventArgs e )
        {
            FirstSelectedNews.Url.ToString().TryOpenLink( ( DependencyObject ) this );
        }
    }
}
