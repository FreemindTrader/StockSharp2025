using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using StockSharp.Xaml.GridControl;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Markup;
using Ecng.Collections;
using Ecng.Xaml;
using StockSharp.Algo.Strategies;
using Ecng.Common;
using Ecng.ComponentModel;
using StockSharp.Algo.Statistics;
using Wintellect.PowerCollections;
using StockSharp.Localization;
using DevExpress.Data;
using MoreLinq;

namespace StockSharp.Xaml
{
    internal static class PointCountDepProp
    {
        public static readonly DependencyProperty PointCountProperty = DependencyProperty.RegisterAttached("PointCount", typeof (int), typeof ( PointCountDepProp ), new PropertyMetadata((object) 0, new PropertyChangedCallback( OnPointCountChanged)));

        public static int GetPointCount( DependencyObject d )
        {
            return ( int ) d.GetValue( PointCountProperty );
        }

        public static void SetPointCount( DependencyObject d, int count )
        {
            d.SetValue(PointCountProperty, ( object ) count );
        }

        private static void OnPointCountChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( ( BaseColumn ) ( d as GridColumn ) )?.View?.DataControl.RefreshData();
        }
    }

    public partial class StrategiesStatisticsPanel : UserControl
    {
        private readonly SynchronizedDictionary<Strategy, StrategyItem> _map = new SynchronizedDictionary<Strategy, StrategyItem>();
        private readonly HashSet<string> _excludeParameters;
        private bool _showProgress;
        private bool _showStrategyName;
        private bool _showSecurity;
        private bool _showPnLChart;
        private readonly ThreadSafeObservableCollection<StrategyItem> _strategies;        

        public StrategiesStatisticsPanel( )
        {
            InitializeComponent();
            ShowStrategyName        = true;
            ShowSecurity            = true;
            _excludeParameters      = new HashSet<string>() { "MaxErrorCount" };
            var itemSource          = new ObservableCollectionEx<StrategyItem>();
            ResultsGrid.ItemsSource = itemSource;
            _strategies             = new ThreadSafeObservableCollection<StrategyItem>( ( IListEx<StrategyItem> ) itemSource );
        }

        public HashSet<string> ExcludeParameters
        {
            get
            {
                return _excludeParameters;
            }
        }

        public bool ShowProgress
        {
            get
            {
                return _showProgress;
            }
            set
            {
                _showProgress = value;
            }
        }

        public bool ShowStrategyName
        {
            get
            {
                return _showStrategyName;
            }
            set
            {
                _showStrategyName = value;
            }
        }

        public bool ShowSecurity
        {
            get
            {
                return _showSecurity;
            }
            set
            {
                _showSecurity = value;
            }
        }

        public bool ShowPnLChart
        {
            get
            {
                return _showPnLChart;
            }
            set
            {
                _showPnLChart = value;
            }
        }

        public Strategy SelectedStrategy
        {
            get
            {
                return (( StrategyItem ) ResultsGrid.SelectedItem )?.Strategy;
            }
        }

        public IEnumerable<Strategy> SelectedStrategies
        {
            get
            {
                return ResultsGrid.SelectedItems.OfType< StrategyItem >().Select( i => i.Strategy ).ToArray();
            }
        }

        

        public event Action<Strategy> StrategyDoubleClick;

        public event Action SelectionChanged;

        public void AddStrategies( IEnumerable<Strategy> strategies )
        {
            Action toBeDone = delegate()
            {
                foreach( Strategy strategy in strategies )
                {
                    StrategyItem myStrategy = new StrategyItem( strategy );
                    _map.Add( strategy, myStrategy );
                    _strategies.Add( myStrategy );
                    UpdateColmns( strategy );
                }
            };

            GuiDispatcher.GlobalDispatcher.AddAction( toBeDone  );
        }


        
        public void Clear( )
        {
            _map.Clear();
            _strategies.Clear();
            ResultsGrid.Columns.Clear();
        }

        private void UpdateColmns( Strategy strategy )
        {
            if ( ResultsGrid.Columns.Count > 0 )
            {
                return;
            }
            

            if ( ShowProgress )
            {
                var columns                  = ResultsGrid.Columns;
                var col                      = new GridColumn();
                col.Header                   = LocalizedStrings.Str1570;
                col.FieldName                = ( "Progress" );
                var binding                  = new Binding("Progress");
                binding.StringFormat         = "{0}%";
                col.Binding                  = binding;

                columns.Add( col );
            }

            if ( ShowStrategyName )
            {
                var columns                  = ResultsGrid.Columns;
                var col                      = new GridColumn();
                col.Header                   = "Name";
                col.FieldName                = "Strategy.Name";
                col.Binding                  = new Binding( "Strategy.Name" );

                columns.Add( col );
            }

            if ( ShowSecurity )
            {
                var columns                  = ResultsGrid.Columns;
                var col                      = new GridColumn();
                col.Header                   = "Security";
                col.FieldName                = "Strategy.Security";
                col.Binding                  = new Binding( "Strategy.Security" );

                columns.Add( col );
            }

            if ( ShowPnLChart )
            {
                var columns                  = ResultsGrid.Columns;
                var col                      = new GridColumn();
                col.Header                   = LocalizedStrings.PnLChart;
                col.FieldName                = "ShowPnLChart";
                col.UnboundType              = UnboundColumnType.Object;

                var settings                 = new SparklineEditSettings();
                settings.PointArgumentMember = "Item1";
                settings.PointValueMember    = "Item2";

                var styleSettings            = new AreaSparklineStyleSettings();
                styleSettings.Brush          = Brushes.Green;
                styleSettings.AreaOpacity    = ( 0.4 );
                settings.StyleSettings       = styleSettings;
                col.EditSettings             = settings;

                columns.Add( col );                
            }

            int parameterCount = 0;

            foreach ( var cachedValue in strategy.Parameters.CachedValues )
            {
                if ( cachedValue.CanOptimize( ExcludeParameters ) )
                {
                    string path = string.Format("Parameters[{0}].Value", parameterCount );
                    var col       = new GridColumn();
                    col.Header    = cachedValue.Name;
                    col.FieldName = path;
                    col.Binding   = new Binding( path );
                    ( ( Collection<GridColumn> ) ResultsGrid.Columns ).Add( col );
                }

                ++parameterCount;
            }

            int statisticsCount = 0;

            foreach ( IStatisticParameter parameter in strategy.StatisticManager.Parameters )
            {                
                string path   = string.Format( "Statistics[{0}].Value", statisticsCount);
                var col       = new GridColumn();
                col.Header    =  parameter.DisplayName;
                col.FieldName = ( path );
                col.Binding   = new Binding( path );

                if ( TypeHelper.IsNumeric( parameter.Value.GetType() ) )
                {
                    col.Binding.StringFormat = "{0:0.##}";
                }

                ResultsGrid.Columns.Add( col );

                ++statisticsCount;
            }            
        }

        public void AddContextMenuItem( object menuItem )
        {
            ResultsGrid.ContextMenu.Items.Add( menuItem );
        }

        
        public void SetColumnVisibility( string name, Visibility visibility )
        {
            var gridColumns = ResultsGrid.Columns.Where( col => col.FieldName.CompareIgnoreCase( name ) );

            foreach ( var col in gridColumns )
            {
                col.Visible = ( visibility == Visibility.Visible );
            }            
        }

        public void UpdateProgress( Strategy strategy, int progress )
        {
            StrategyItem class613 = (StrategyItem) CollectionHelper.TryGetValue<Strategy, StrategyItem>( _map, strategy);
            if ( class613 == null )
            {
                return;
            }

            class613.Progress = progress;
        }

        

        private void ResultsGrid_ItemDoubleClick( object sender, ItemDoubleClickEventArgs e )
        {
            if ( SelectedStrategy == null )
            {
                return;
            }
            
            StrategyDoubleClick?.Invoke( SelectedStrategy );
        }        

        private void ResultsGrid_SelectedItemChanged( object sender, SelectedItemChangedEventArgs e )
        {
            SelectionChanged?.Invoke( );
        }
        private void ResultsGrid_CustomUnboundColumnData( object sender, GridColumnDataEventArgs e )
        {
            if ( e.Column.FieldName != "ShowPnLChart" )
            {
                return;
            }

            var pnLvalues = _strategies[ e.ListSourceRowIndex ].PnLValues;
            int pointCount = PointCountDepProp.GetPointCount((DependencyObject) e.Column );

            e.Value = Extensions.AveragePriceByCount( pnLvalues, pointCount );
        }

        

        
        

        private sealed class StrategyItem : NotifiableObject
        {
            private readonly CachedSynchronizedList<Tuple<DateTime, Decimal>> _pnlValues = new CachedSynchronizedList<Tuple<DateTime, decimal>>();
            private readonly SyncObject _lock = new SyncObject();
            private Decimal _pnl;
            private readonly Strategy _strategy;
            private int _progress;

            public StrategyItem( Strategy strategy )
            {
                if ( strategy == null )
                {
                    throw new ArgumentNullException( "strategy" );
                }

                _strategy = strategy;
                Strategy.PnLChanged += ( new Action( OnPnLChanged ) );
            }

            public Strategy Strategy
            {
                get
                {
                    return _strategy;
                }
            }

            public IStrategyParam[ ] Parameters
            {
                get
                {
                    return ( ( IEnumerable<IStrategyParam> ) Strategy.Parameters.CachedValues ).ToArray<IStrategyParam>();
                }
            }

            public IStatisticParameter[ ] Statistics
            {
                get
                {
                    return ( ( IEnumerable<IStatisticParameter> ) Strategy.StatisticManager.Parameters ).ToArray<IStatisticParameter>();
                }
            }

            public int Progress
            {
                get
                {
                    return _progress;
                }
                set
                {
                    if ( _progress == value )
                    {
                        return;
                    }

                    _progress = value;
                    NotifyChanged( nameof( Progress ) );
                    NotifyChanged( "PnLValues" );
                }
            }

            public ICollection<Tuple<DateTime, Decimal>> PnLValues
            {
                get
                {
                    return ( ICollection<Tuple<DateTime, Decimal>> ) _pnlValues.Cache;
                }
            }

            private void OnPnLChanged( )
            {
                Decimal pnL = Strategy.PnL;

                if ( _pnl == pnL )
                {
                    return;
                } 
                
                _pnlValues.Add( Tuple.Create( DateTime.Now, pnL ) );
                _pnl = pnL;
            }
        }        
    }
}
