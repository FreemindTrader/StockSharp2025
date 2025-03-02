using DevExpress.Data;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Grid;
using Ecng.Collections;
using Ecng.Common;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo;
using StockSharp.Algo.Strategies;
using StockSharp.Algo.Strategies.Analytics;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Studio.Controls;
using StockSharp.Studio.Core;
using StockSharp.Xaml.Charting;
using StockSharp.Xaml.GridControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace StockSharp.Hydra.Panes
{
    public partial class AnalyticsResultPane : BaseStudioControl, IPane, IStudioControl, IPersistable, IDisposable, IComponentConnector
    {
        private BaseAnalyticsStrategy _strategy;
        
        public AnalyticsResultPane()
        {
            InitializeComponent();
            SaveWithLayout = false;
        }

        public void Bind( BaseAnalyticsStrategy strategy )
        {
            BaseAnalyticsStrategy analyticsStrategy = strategy;
            if ( analyticsStrategy == null )
                throw new ArgumentNullException( nameof( strategy ) );
            _strategy = analyticsStrategy;
            Title = LocalizedStrings.AnalyticsResult.Put( _strategy.Name );
            CancelButton.Visibility = Visibility.Visible;
            LogManager logManager = LogManager;
            Exception error = null;
            _strategy.ProcessStateChanged += s =>
            {
                if ( _strategy == null || _strategy.ProcessState != ProcessStates.Stopped )
                    return;
                _strategy = null;
                logManager.Sources.Remove( strategy );
                GuiDispatcher.GlobalDispatcher.AddAction( () =>
          {
              CancelButton.Visibility = Visibility.Collapsed;
              if ( error == null )
                  return;
              int num = ( int )new MessageBoxBuilder().Owner( this ).Text( error.Message ).Error().Show();
          } );
            };
            _strategy.Error += ( s, ex ) =>
            {
                if ( error != null )
                    return;
                error = ex;
            };
            _strategy.Environment.SetValue( "Panel", new ResultPanel( this ) );
            _strategy.Connector = new Connector();
            logManager.Sources.Add( strategy );
            ( ( Action )( () => Do.Invariant( () =>
            {
                try
                {
                    _strategy.Start();
                }
                catch ( Exception ex )
                {
                    _strategy.AddErrorLog( ex );
                }
            } ) ) ).Thread().Name( Name + " analyze thread." ).Launch();
        }

        private void Cancel_OnClick( object sender, RoutedEventArgs e )
        {
            _strategy?.Stop();
        }

        bool IPane.IsValid
        {
            get
            {
                return false;
            }
        }

        public override void Dispose()
        {
            _strategy?.Dispose();
            base.Dispose();
        }

        

        private class AnalyticsGrid : IAnalyticsGrid
        {
            private readonly BaseGridControl _grid;

            public AnalyticsGrid( BaseGridControl grid )
            {
                BaseGridControl baseGridControl = grid;
                if ( baseGridControl == null )
                    throw new ArgumentNullException( nameof( grid ) );
                _grid = baseGridControl;
            }

            void IAnalyticsGrid.ClearColumns()
            {
                _grid.GuiSync( () => _grid.Columns.Clear() );
            }

            IAnalyticsGridColumn IAnalyticsGrid.AddColumn(
              string fieldName,
              string header )
            {
                if ( fieldName.IsEmpty() )
                    throw new ArgumentNullException( nameof( fieldName ) );
                return _grid.GuiSync( () =>
                {
                    GridColumn column = new GridColumn() { FieldName = fieldName, Header = header };
                    _grid.Columns.Add( column );
                    return new AnalyticsGridColumn( column );
                } );
            }

            ICollection<T> IAnalyticsGrid.CreateSource<T>()
            {
                return _grid.GuiSync( () =>
                {
                    UIObservableCollectionEx<T> observableCollectionEx = new UIObservableCollectionEx<T>();
                    _grid.ItemsSource = observableCollectionEx;
                    return new ThreadSafeObservableCollection<T>( observableCollectionEx );
                } );
            }

            void IAnalyticsGrid.SetSort(
              IAnalyticsGridColumn column,
              ListSortDirection direction )
            {
                if ( column == null )
                    throw new ArgumentNullException( nameof( column ) );
                _grid.GuiSync( () => _grid.SortBy( ( ( AnalyticsGridColumn )column ).Column, direction.To<string>().To<ColumnSortOrder>() ) );
            }

            private class AnalyticsGridColumn : IAnalyticsGridColumn
            {
                public readonly GridColumn Column;

                public AnalyticsGridColumn( GridColumn column )
                {
                    GridColumn gridColumn = column;
                    if ( gridColumn == null )
                        throw new ArgumentNullException( nameof( column ) );
                    Column = gridColumn;
                }

                double IAnalyticsGridColumn.Width
                {
                    get
                    {
                        return Column.GuiSync( () => Column.Width.Value );
                    }
                    set
                    {
                        Column.GuiSync( () => Column.Width = ( GridColumnWidth )value );
                    }
                }
            }
        }

        private class AnalyticsChart : IAnalyticsChart
        {
            private readonly BubbleChart _bubbleChart;
            private readonly HeatmapChart _heatmapChart;
            private readonly HistogramChart _histogramChart;

            public AnalyticsChart( BubbleChart bubbleChart )
            {
                BubbleChart bubbleChart1 = bubbleChart;
                if ( bubbleChart1 == null )
                    throw new ArgumentNullException( nameof( bubbleChart ) );
                _bubbleChart = bubbleChart1;
            }

            public AnalyticsChart( HeatmapChart heatmapChart )
            {
                HeatmapChart heatmapChart1 = heatmapChart;
                if ( heatmapChart1 == null )
                    throw new ArgumentNullException( nameof( heatmapChart ) );
                _heatmapChart = heatmapChart1;
            }

            public AnalyticsChart( HistogramChart histogramChart )
            {
                HistogramChart histogramChart1 = histogramChart;
                if ( histogramChart1 == null )
                    throw new ArgumentNullException( nameof( histogramChart ) );
                _histogramChart = histogramChart1;
            }

            void IAnalyticsChart.Append( DateTime x, Decimal y, Decimal z )
            {
                if ( _bubbleChart != null )
                    _bubbleChart.Append( x, y, z );
                else if ( _histogramChart != null )
                {
                    _histogramChart.Append( x.Ticks, y );
                }
                else
                {
                    HeatmapChart heatmapChart = _heatmapChart;
                }
            }

            void IAnalyticsChart.Update( DateTime x, Decimal y, Decimal z )
            {
                if ( _bubbleChart != null )
                    _bubbleChart.Update( x, y, z );
                else if ( _histogramChart != null )
                {
                    _histogramChart.Update( x.Ticks, y );
                }
                else
                {
                    HeatmapChart heatmapChart = _heatmapChart;
                }
            }
        }

        private class ResultPanel : IAnalyticsPanel
        {
            private readonly AnalyticsResultPane _parent;

            public ResultPanel( AnalyticsResultPane parent )
            {
                AnalyticsResultPane analyticsResultPane = parent;
                if ( analyticsResultPane == null )
                    throw new ArgumentNullException( nameof( parent ) );
                _parent = analyticsResultPane;
            }

            void IAnalyticsPanel.ClearControls()
            {
                _parent.GuiSync( () => _parent.DocumentGroup.Clear() );
            }

            IAnalyticsGrid IAnalyticsPanel.CreateGrid( string title )
            {
                return new AnalyticsGrid( AddTab( title, () => new BaseGridControl() ) );
            }

            IAnalyticsChart IAnalyticsPanel.CreateBubbleChart( string title )
            {
                return new AnalyticsChart( AddTab( title, () => new BubbleChart() ) );
            }

            IAnalyticsChart IAnalyticsPanel.CreateHistogramChart(
              string title )
            {
                return new AnalyticsChart( AddTab( title, () => new HistogramChart() ) );
            }

            IAnalyticsChart IAnalyticsPanel.CreateHeatmap( string title )
            {
                return new AnalyticsChart( AddTab( title, () => new HeatmapChart() ) );
            }

            private T AddTab<T>( string title, Func<T> createContent )
            {
                if ( createContent == null )
                    throw new ArgumentNullException( nameof( createContent ) );
                return _parent.GuiSync( () =>
                {
                    T obj = createContent();
                    DocumentGroup documentGroup = _parent.DocumentGroup;
                    documentGroup.Add( new DocumentPanel()
                    {
                        Caption = title,
                        AllowClose = false,
                        Content = obj
                    } );
                    return obj;
                } );
            }
        }
    }
}
