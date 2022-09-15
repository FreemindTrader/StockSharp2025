using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Serialization;
using Ecng.Xaml;
using MoreLinq;
using StockSharp.Algo.Strategies;
using StockSharp.Localization;
using StockSharp.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockSharp.Xaml
{
    public partial class Monitor : UserControl, IPersistable, IDisposable, ILogListener
    {
        public static RoutedCommand ClearCommand = new RoutedCommand();

        private readonly Dictionary<Guid, NodeInfo> _logInfo = new Dictionary<Guid, NodeInfo>();
        private bool _showStrategies = true;
        private int _maxItemsCount = LogMessageCollection.DefaultMaxItemsCount;
        
        private static readonly MemoryStatisticsValue<LogMessage> _msgStat = new MemoryStatisticsValue<LogMessage>(LocalizedStrings.Str1565 );
        public static readonly DependencyProperty ShowStrategiesProperty = DependencyProperty.Register(nameof (ShowStrategies), typeof (bool), typeof (Monitor), new PropertyMetadata((object) true, new PropertyChangedCallback(ShowStrategiesPropertyChanged)));
        public static readonly DependencyProperty MaxItemsCountProperty = DependencyProperty.Register(nameof (MaxItemsCount), typeof (int), typeof (Monitor), new PropertyMetadata((object) LogMessageCollection.DefaultMaxItemsCount, new PropertyChangedCallback(MaxItemsCountChanged)));
        private readonly LogSourceNode CoreRootNode;
        private readonly LogSourceNode StrategyRootNode;
        private int _totalMessageCount;        

        static Monitor( )
        {
            MemoryStatistics.Instance.Values.Add( _msgStat );
        }

        public Monitor( )
        {
            InitializeComponent();
            CoreRootNode = new LogSourceNode( LogSourceNode.CoreRootGuid, LocalizedStrings.Str1559, ( LogSourceNode ) null );
            StrategyRootNode = new LogSourceNode( LogSourceNode.StrategyRootGuid, LocalizedStrings.Str1355, CoreRootNode );
            CoreRootNode.Add( StrategyRootNode );
            _logInfo.Add( CoreRootNode.Key, new NodeInfo( CoreRootNode ) );
            _logInfo.Add( StrategyRootNode.Key, new NodeInfo( StrategyRootNode ) );

            var observableCollection = new ObservableCollection<LogSourceNode>();
            observableCollection.Add( CoreRootNode );
            SourcesTree.ItemsSource = observableCollection;
            SourcesTree.SelectedItem = CoreRootNode;
            LogCtrl.ClearCommand = ( ICommand ) new DevExpress.Mvvm.DelegateCommand( ( ) => ClearCommandCommand( null ), ( ) => CanClear( null ) );
        }

        private static void ShowStrategiesPropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var monitor = (Monitor) XamlHelper.FindLogicalChild<Monitor>(d);
            monitor._showStrategies = ( bool ) e.NewValue;
            TreeListControl tree = monitor.SourcesTree;
            if ( monitor._showStrategies )
            {
                monitor.CoreRootNode.Add( monitor.StrategyRootNode );
            }
            else
            {
                monitor.CoreRootNode.Remove( monitor.StrategyRootNode );
                tree.SelectedItem = monitor.CoreRootNode;
            }
        }

        public bool ShowStrategies
        {
            get
            {
                return _showStrategies;
            }
            set
            {
                SetValue( ShowStrategiesProperty, value );
            }
        }

        private static void MaxItemsCountChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            d.FindLogicalChild<Monitor>()._maxItemsCount = ( int ) e.NewValue;
        }

        public int MaxItemsCount
        {
            get
            {
                return _maxItemsCount;
            }
            set
            {
                SetValue( MaxItemsCountProperty, value );
            }
        }

        public LogControl LogControl
        {
            get
            {
                return LogCtrl;
            }
        }

        public void Clear( )
        {
            var toRemove = _logInfo
                .Where(p =>
                {
                    var isSystem = p.Key == CoreRootNode.Key || p.Key == StrategyRootNode.Key;

                    if (isSystem)
                    {
                        p.Value.Item1.Clear();
                    }

                    return !isSystem;
                }).ToArray();

            toRemove.ForEach( p =>
            {
                _msgStat.Remove( p.Value.Item1.Count );

                p.Value.Item1.Clear();
                p.Value.Item2.ParentNode.Remove( p.Value.Item2 );

                _logInfo.Remove( p.Key );
            } );

            SourcesTree.SelectedItem = CoreRootNode;


        }

        public void WriteMessages( IEnumerable<LogMessage> messages )
        {
            if ( messages == null )
            {
                throw new ArgumentNullException( nameof( messages ) );
            }

            messages.GroupBy( m => m.Source ).ForEach( g => WriteMessages( g.Key, g ) );

            CheckCount();
        }

        private NodeInfo EnsureBuildNodes( ILogSource source )
        {
            var info = _logInfo.TryGetValue(source.Id);

            if ( info != null )
            {
                return info;
            }

            var newSources = new Stack<ILogSource>();
            newSources.Push( source );

            var root = source.Parent;

            // ищем корневую ноду, которая уже была ранее добавлена
            while ( root != null && !_logInfo.ContainsKey( root.Id ) )
            {
                newSources.Push( root );
                root = root.Parent;
            }

            LogSourceNode parentNode;

            if ( root == null )
            {
                parentNode = ( newSources.Peek() is Strategy ) ? StrategyRootNode : CoreRootNode;
            }
            else
            {
                parentNode = _logInfo[ root.Id ].Item2;
            }

            foreach ( var newSource in newSources )
            {
                var sourceNode = new LogSourceNode(newSource.Id, newSource.Name, parentNode);
                GuiDispatcher.GlobalDispatcher.AddSyncAction( () => parentNode.Add( sourceNode ) );                
                parentNode = sourceNode;
                _logInfo.Add( newSource.Id, new NodeInfo( sourceNode ) );
            }

            return _logInfo[ source.Id ];
        }

        private void WriteMessages( ILogSource source, IEnumerable<LogMessage> messages )
        {
            var currentNode = EnsureBuildNodes(source).Item2;
            var messagesCache = messages.ToArray();

            while ( currentNode != null )
            {
                var list = _logInfo[currentNode.Key].Item1;

                list.AddRange( messagesCache );

                _msgStat.Add( messagesCache );
                _totalMessageCount += messagesCache.Length;

                currentNode = currentNode.ParentNode;
            }
        }

        private void CheckCount( )
        {
            if ( MaxItemsCount == -1 || _totalMessageCount < 1.5 * MaxItemsCount )
            {
                return;
            }

            int removedCount = 0;
            int countToRemove = _totalMessageCount - MaxItemsCount;

            foreach ( var nodeInfo in _logInfo.Values )
            {
                var list = nodeInfo.Item1;
                int count = (int) ( list.Count * ( countToRemove / (double) _totalMessageCount));

                if ( count > 0 )
                {
                    int removed = list.RemoveRange(0, count);
                    _msgStat.Remove( removed );

                    removedCount += removed;
                }
            }
            _totalMessageCount -= removedCount;
        }

        private void SourcesTree_SelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {
            LogSourceNode newItem;
            LogCtrl.Messages = ( newItem = ( ( CurrentItemChangedEventArgs ) e ).NewItem as LogSourceNode ) != null ? _logInfo[ newItem.Key ].Item1 : new LogMessageCollection();
        }

        public void Load( SettingsStorage storage )
        {
            SettingsStorage storage1 = (SettingsStorage) storage.GetValue<SettingsStorage>("LogControl",  null);
            if ( storage1 == null )
            {
                return;
            }

            LogCtrl.Load( storage1 );
        }

        public void Save( SettingsStorage storage )
        {
            storage.SetValue<SettingsStorage>( "LogControl", PersistableHelper.Save( ( IPersistable ) LogCtrl ) );
        }

        void IDisposable.Dispose( )
        {
        }




        private void ClearNode( LogSourceNode node )
        {
            _logInfo[ node.Key ].Item1.Clear();

            foreach ( LogSourceNode childNode in node.ChildNodes )
            {
                ClearNode( childNode );
            }
        }

        private void RemoveNode( LogSourceNode node )
        {
            foreach ( LogSourceNode childNode in node.ChildNodes.ToArray<LogSourceNode>( ) )
            {
                RemoveNode( childNode );
                if ( !( childNode.Key == CoreRootNode.Key ) && !( childNode.Key == StrategyRootNode.Key ) )
                {
                    _logInfo.Remove( childNode.Key );
                    node.Remove( childNode );
                }
            }
        }

        public event Action LayoutChanged;

        private void RaiseLayoutchangedEvent( )
        {
            Action myEvent = LayoutChanged;
            if ( myEvent == null )
            {
                return;
            }

            myEvent();
        }




        private void ClearCommandCommand( object object_0 )
        {
            ClearNode( ( LogSourceNode ) SourcesTree.SelectedItem );
        }

        private bool CanClear( object object_0 )
        {
            LogSourceNode selectedItem;
            if ( ( selectedItem = ( ( DataControlBase ) SourcesTree ).SelectedItem as LogSourceNode ) == null )
            {
                return false;
            }

            var m1 = _logInfo.TryGetValue( selectedItem.Key);
            if ( m1 == null )
            {
                return false;
            }

            return m1.Item1.Count > 0;
        }



        private void method_14( IGrouping<ILogSource, LogMessage> message )
        {
            WriteMessages( message.Key, ( IEnumerable<LogMessage> ) message );
        }

        private sealed class NodeInfo : Tuple<LogMessageCollection, LogSourceNode>
        {
            public NodeInfo( LogSourceNode logSourceNode_0 )
              : base( new LogMessageCollection(), logSourceNode_0 )
            {
            }
        }



        private void LogCtrl_LayoutChanged( )
        {
            RaiseLayoutchangedEvent();
        }

        private void CommandBinding_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            LogSourceNode selectedItem = (LogSourceNode) ((DataControlBase) SourcesTree).SelectedItem;
            ClearNode( selectedItem );
            RemoveNode( selectedItem );
        }

        private void CommandBinding_CanExecute( object sender, CanExecuteRoutedEventArgs e )
        {
            LogSourceNode selectedItem = SourcesTree.SelectedItem as LogSourceNode;

            if ( selectedItem != null )
            {
                NodeInfo nodeInfo = (NodeInfo) _logInfo.TryGetValue<Guid, NodeInfo>(  selectedItem.Key );
                e.CanExecute = nodeInfo != null && ( ( BaseObservableCollection ) nodeInfo.Item1 ).Count > 0 || ( ( BaseObservableCollection ) selectedItem.ChildNodes ).Count > 0;
            }
            else
            {
                e.CanExecute = false;
            }
        }
    }
}
