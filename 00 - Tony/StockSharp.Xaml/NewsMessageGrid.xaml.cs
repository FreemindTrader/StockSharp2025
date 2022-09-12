using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
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
    public partial class NewsMessageGrid : BaseGridControl
    {
        public static RoutedCommand RequestStoryCommand = new RoutedCommand();
        public static RoutedCommand OpenUrlCommand = new RoutedCommand();

        private readonly ThreadSafeObservableCollection<NewsMessage> _newMessage;

        private INewsProvider _provider;

        public NewsMessageGrid( )
        {
            this.InitializeComponent();

            // Tony: Fix XAML CommandBindings Error in XAML 
            // I moved the CommandBinding here so the blue lines in XAML are gone. Hate to see those.
            var requestStoryCommand = new CommandBinding( RequestStoryCommand, ExecutedRequestStoryCommand, CanExecuteRequestStoryCommand );
            var openUrlCommand = new CommandBinding( OpenUrlCommand, ExecutedOpenUrlCommand, CanExecuteOpenUrlCommand );

            this.CommandBindings.Add( requestStoryCommand );
            this.CommandBindings.Add( openUrlCommand );


            var itemsSource = new ObservableCollectionEx<NewsMessage>();
            ItemsSource = itemsSource;

            _newMessage = new ThreadSafeObservableCollection<NewsMessage>( itemsSource ) { MaxCount = 10000 };

            ContextMenu.Items.Add( new Separator() );

            ContextMenu.Items.Add( new MenuItem { Header = LocalizedStrings.RequestNewsBody, Command = RequestStoryCommand, CommandTarget = this } );
            ContextMenu.Items.Add( new MenuItem { Header = LocalizedStrings.OpenUrl,         Command = OpenUrlCommand,      CommandTarget = this } );            
        }
        public int MaxCount
        {
            get
            {
                return _newMessage.MaxCount;
            }
            set
            {
                _newMessage.MaxCount = value;
            }
        }

        public IListEx<NewsMessage> Messages
        {
            get
            {
                return _newMessage;
            }
        }

        public NewsMessage SelectedMessage
        {
            get
            {
                return this.SelectedMessages.FirstOrDefault();
            }
        }

        public IEnumerable<NewsMessage> SelectedMessages
        {
            get
            {
                return SelectedItems.Cast<NewsMessage>();
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
            SelectedMessages.Where( n => n.Story.IsEmpty() ).ForEach( m => NewsProvider.RequestNewsStory( m.ToNews( null ) ) );
        }

        private void CanExecuteRequestStoryCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = NewsProvider != null && SelectedMessages.Any( n => n.Story.IsEmpty() );
        }

        private void CanExecuteOpenUrlCommand( object sender, CanExecuteRoutedEventArgs e )
        {
            var news = SelectedMessages;
            e.CanExecute = news.Count() == 1 && SelectedMessage.Url != null;
        }

        private void ExecutedOpenUrlCommand( object sender, ExecutedRoutedEventArgs e )
        {
            SelectedMessage.Url.ToString().TryOpenLink( ( DependencyObject ) this );
        }
    }
}
