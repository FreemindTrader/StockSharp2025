// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.UltrachartGroup
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Licensing;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.Axes;
using StockSharp.Xaml.Licensing.Core;

namespace StockSharp.Xaml.Charting
{
    [TemplatePart( Name = "PART_MainGrid" )]
    [TemplatePart( Name = "PART_TabbedContent" )]
    [TemplatePart( Name = "PART_StackedContent" )]
    [TemplatePart( Name = "PART_MainPane" )]
    [TemplatePart( Name = "PART_UltrachartGroupModifierCanvas" )]
    [UltrachartLicenseProvider( typeof( UltraTradeChartLicenseProvider ) )]
    public class UltrachartGroup : ItemsControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty VerticalChartGroupProperty = DependencyProperty.RegisterAttached("VerticalChartGroup", typeof (string), typeof (UltrachartGroup), new PropertyMetadata((object) null, new PropertyChangedCallback(UltrachartGroup.OnVerticalChartGroupChanged)));
        public static readonly DependencyProperty IsTabbedProperty = DependencyProperty.Register(nameof (IsTabbed), typeof (bool), typeof (UltrachartGroup), new PropertyMetadata((object) false, new PropertyChangedCallback(UltrachartGroup.OnIsTabbedChanged)));
        internal static Dictionary<ChartGroup, string> VerticalChartGroups = new Dictionary<ChartGroup, string>();
        private readonly List<ItemPane> _items = new List<ItemPane>();
        private ContentPresenter _mainPane;
        private TabControl _tabbedViewPanel;
        private StackPanel _stackedViewPanel;
        private Canvas _modifierCanvas;
        private double _resizeTotalDragDiff;
        private double _resizeInitialHeight;
        private double _resizeTotalHeight;
        private double _resizeInitialMouseYCoord;
        private double _resizeMinHeight;
        private ItemPane _resizePane;
        private ItemPane _resizeUpperPane;
        private Action<ItemPane> _addPane;

        public UltrachartGroup()
        {
            DefaultStyleKey = ( object ) typeof( UltrachartGroup );
            _addPane = new Action<ItemPane>( AddStackedPane );
        }

        public bool IsTabbed
        {
            get
            {
                return ( bool ) GetValue( UltrachartGroup.IsTabbedProperty );
            }
            set
            {
                SetValue( UltrachartGroup.IsTabbedProperty, ( object ) value );
            }
        }

        public bool HasTabbedItems
        {
            get
            {
                if ( _tabbedViewPanel != null )
                {
                    return _tabbedViewPanel.Items.Count > 0;
                }

                return false;
            }
        }

        public static void SetVerticalChartGroup( DependencyObject element, string syncWidthGroup )
        {
            element.SetValue( UltrachartGroup.VerticalChartGroupProperty, ( object ) syncWidthGroup );
        }

        public static string GetVerticalChartGroup( DependencyObject element )
        {
            return ( string ) element.GetValue( UltrachartGroup.VerticalChartGroupProperty );
        }

        protected override bool IsItemItsOwnContainerOverride( object item )
        {
            return base.IsItemItsOwnContainerOverride( item );
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return base.GetContainerForItemOverride();
        }

        protected override void OnItemsSourceChanged( IEnumerable oldValue, IEnumerable newValue )
        {
            base.OnItemsSourceChanged( oldValue, newValue );
            OnItemsCollectionChanged( oldValue, newValue );
        }

        private void OnItemsCollectionChanged( IEnumerable oldValue, IEnumerable newValue )
        {
            if ( _mainPane == null )
            {
                return;
            }

            if ( oldValue != null )
            {
                RemovePanes( oldValue.Cast<IChildPane>() );
            }

            if ( newValue == null )
            {
                return;
            }

            AddPanes( newValue.Cast<IChildPane>() );
        }

        protected override void OnItemsChanged( NotifyCollectionChangedEventArgs e )
        {
            base.OnItemsChanged( e );
            if ( _mainPane == null || e.Action == NotifyCollectionChangedAction.Add && e.NewItems == null || e.Action == NotifyCollectionChangedAction.Remove && e.OldItems == null )
            {
                return;
            }

            switch ( e.Action )
            {
                case NotifyCollectionChangedAction.Add:
                    AddPanes( e.NewItems.Cast<IChildPane>() );
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemovePanes( e.OldItems.Cast<IChildPane>() );
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ClearAll();
                    break;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            new StockSharp.Xaml.Licensing.Core.LicenseManager().Validate<UltrachartGroup>( this, ( IProviderFactory ) new UltrachartLicenseProviderFactory() );
            ClearAll();
            _mainPane = GetTemplateChild( "PART_MainPane" ) as ContentPresenter;
            _tabbedViewPanel = GetTemplateChild( "PART_TabbedContent" ) as TabControl;
            _stackedViewPanel = GetTemplateChild( "PART_StackedContent" ) as StackPanel;
            _modifierCanvas = GetTemplateChild( "PART_UltrachartGroupModifierCanvas" ) as Canvas;
            if ( ItemsSource == null )
            {
                return;
            }

            OnItemsCollectionChanged( ( IEnumerable ) null, ItemsSource );
        }

        private void ClearAll()
        {
            while ( !_items.IsNullOrEmpty<ItemPane>() )
            {
                RemovePane( _items[ 0 ] );
            }
        }

        private void RemovePanes( IEnumerable<IChildPane> items )
        {
            items.ForEachDo<IChildPane>( ( Action<IChildPane> ) ( item => RemovePane( item ) ) );
        }

        private ItemPane RemovePane( IChildPane item )
        {
            ItemPane itemPane = _items.FirstOrDefault<ItemPane>((Func<ItemPane, bool>) (pane => pane.PaneViewModel.Equals((object) item)));
            RemovePane( itemPane );
            return itemPane;
        }

        private void RemovePane( ItemPane itemPane )
        {
            if ( itemPane == null )
            {
                return;
            }

            if ( !itemPane.IsMainPane )
            {
                Unsubscribe( itemPane.PaneElement as UltrachartGroupPane );
                _items.Remove( itemPane );
                if ( itemPane.IsTabbed )
                {
                    RemoveTabbedPane( itemPane );
                }
                else
                {
                    RemoveStackedPane( itemPane );
                }
            }
            else
            {
                RemoveMainPane( itemPane );
            }
        }

        private void RemoveStackedPane( ItemPane item )
        {
            _stackedViewPanel.Children.Remove( ( UIElement ) item.PaneElement );
        }

        private void RemoveTabbedPane( ItemPane itemPane )
        {
            TabItem tabItem = _tabbedViewPanel.Items.OfType<TabItem>().FirstOrDefault<TabItem>((Func<TabItem, bool>) (item => item.Content.Equals((object) itemPane.PaneElement)));
            if ( tabItem == null )
            {
                return;
            }

            tabItem.Content = ( object ) null;
            _tabbedViewPanel.Items.Remove( ( object ) tabItem );
            OnPropertyChanged( "HasTabbedItems" );
        }

        private void RemoveMainPane( ItemPane mainPane )
        {
            if ( !mainPane.IsMainPane )
            {
                throw new ArgumentException( "Attempt to remove MainPane was failed. Passed invalid ItemPane argument." );
            }

            _mainPane.Content = ( object ) null;
            _items.Remove( mainPane );
            mainPane.IsMainPane = false;
        }

        private void AddPanes( IEnumerable<IChildPane> items )
        {
            if ( _mainPane.Content == null && items.Any<IChildPane>() )
            {
                AddMainPane( items.First<IChildPane>() );
                items.Skip<IChildPane>( 1 ).ForEachDo<IChildPane>( new Action<IChildPane>( AddPane ) );
            }
            else
            {
                items.ForEachDo<IChildPane>( new Action<IChildPane>( AddPane ) );
            }
        }

        private void AddMainPane( IChildPane paneViewModel )
        {
            FrameworkElement itemFromTemplate = GetItemFromTemplate();
            ItemPane itemPane = new ItemPane()
            {
                PaneViewModel = paneViewModel,
                PaneElement = itemFromTemplate,
                IsMainPane = true
            };
            itemFromTemplate.DataContext = ( object ) itemPane.PaneViewModel;
            _mainPane.Content = ( object ) itemFromTemplate;
            _items.Add( itemPane );
        }

        private void AddPane( IChildPane paneViewModel )
        {
            FrameworkElement itemFromTemplate = GetItemFromTemplate();
            ItemPane itemPane = new ItemPane()
            {
                PaneViewModel = paneViewModel,
                PaneElement = itemFromTemplate
            };
            itemFromTemplate.DataContext = ( object ) paneViewModel;
            itemPane.ChangeOrientationCommand = ( ICommand ) new ActionCommand( ( Action ) ( () => MovePane( itemPane ) ) );
            itemPane.ClosePaneCommand = paneViewModel.ClosePaneCommand;

            UltrachartGroupPane ultrachartGroupPane = new UltrachartGroupPane();
            ultrachartGroupPane.Content = ( object ) itemFromTemplate;
            ultrachartGroupPane.DataContext = ( object ) itemPane;
            ultrachartGroupPane.Style = ItemContainerStyle;
            UltrachartGroupPane itemContainer = ultrachartGroupPane;
            itemPane.PaneElement = ( FrameworkElement ) itemContainer;
            Subscribe( itemContainer );
            _addPane( itemPane );
            _items.Add( itemPane );
        }

        private void AddStackedPane( ItemPane container )
        {
            container.IsTabbed = false;
            _stackedViewPanel.Children.Add( ( UIElement ) container.PaneElement );
        }

        private void AddTabbedPane( ItemPane container )
        {
            SynchronizeTabbedHeight( container.PaneElement as UltrachartGroupPane );
            container.IsTabbed = true;
            TabItem tabItem1 = new TabItem();
            tabItem1.Header = ( object ) container.PaneViewModel.Title;
            tabItem1.DataContext = ( object ) container;
            tabItem1.Content = ( object ) container.PaneElement;

            TabItem tabItem2 = tabItem1;
            _tabbedViewPanel.Items.Add( ( object ) tabItem2 );
            _tabbedViewPanel.SelectedItem = ( object ) tabItem2;
            OnPropertyChanged( "HasTabbedItems" );
        }

        public void MovePane( IChildPane item )
        {
            ItemPane container = RemovePane(item);
            if ( container == null )
            {
                return;
            }

            if ( container.IsTabbed )
            {
                AddStackedPane( container );
            }
            else
            {
                AddTabbedPane( container );
            }
        }

        private void MovePane( ItemPane container )
        {
            if ( container.IsTabbed )
            {
                RemoveTabbedPane( container );
                AddStackedPane( container );
            }
            else
            {
                RemoveStackedPane( container );
                AddTabbedPane( container );
            }
        }

        private void Subscribe( UltrachartGroupPane itemContainer )
        {
            itemContainer.Resizing += new EventHandler<DragDeltaEventArgs>( OnItemContainerResizing );
            itemContainer.Resized += new EventHandler<DragCompletedEventArgs>( OnItemContainerResized );
        }

        private void Unsubscribe( UltrachartGroupPane itemContainer )
        {
            itemContainer.Resizing -= new EventHandler<DragDeltaEventArgs>( OnItemContainerResizing );
            itemContainer.Resized -= new EventHandler<DragCompletedEventArgs>( OnItemContainerResized );
        }

        internal class TonyUltraGroup
        {
            public UltrachartGroup MyGroup;
            internal double newHeight;

            internal ItemPane GetItemPaneByUiElement( UIElement sender )
            {
                throw new NotImplementedException();
            }
        }

        private void OnItemContainerResizing( object sender, DragDeltaEventArgs e )
        {
            ItemPane groupPane = _items.FirstOrDefault< ItemPane >( p => p.PaneElement == ( UltrachartGroupPane ) sender );

            if ( _resizePane == null )
            {
                _resizePane = groupPane;

                int num = _stackedViewPanel.Children.IndexOf(_resizePane.PaneElement);
                if ( num < 0 )
                {
                    Guard.IsTrue( _resizePane.IsTabbed, "unexpected pane type" );
                    int count = _stackedViewPanel.Children.Count;
                    ItemPane resizeUpperPane;
                    if ( count <= 0 )
                    {
                        resizeUpperPane = _items.First( ( ItemPane i ) => i.IsMainPane );
                    }
                    else
                    {
                        resizeUpperPane = _items.FirstOrDefault<ItemPane>( p => p.PaneElement == _stackedViewPanel.Children[ count - 1 ] );
                    }
                    _resizeUpperPane = resizeUpperPane;
                }
                else if ( num == 0 )
                {
                    _resizeUpperPane = _items.First( ( ItemPane i ) => i.IsMainPane );
                }
                else
                {
                    _resizeUpperPane = _items.FirstOrDefault<ItemPane>( p => p.PaneElement == _stackedViewPanel.Children[ num - 1 ] );
                }

                _resizeTotalDragDiff = 0.0;
                _resizeInitialHeight = _resizePane.PaneElement.ActualHeight;
                _resizeTotalHeight = _resizeInitialHeight + _resizeUpperPane.PaneElement.ActualHeight;

                var myWindows = Ecng.Xaml.XamlHelper.GetWindow(_resizeUpperPane.PaneElement);

                _resizeInitialMouseYCoord = Mouse.GetPosition( myWindows ).Y;
                _resizeMinHeight = ( ( UltrachartGroupPane ) _resizePane.PaneElement ).MeasureMinHeight();
            }

            var myWindows2 = Ecng.Xaml.XamlHelper.GetWindow(_resizeUpperPane.PaneElement);

            double y = Mouse.GetPosition(myWindows2).Y;
            _resizeTotalDragDiff = y - _resizeInitialMouseYCoord;

            var newHeight = Math.Min( _resizeTotalHeight - _resizeMinHeight, Math.Max( _resizeMinHeight, _resizeInitialHeight - _resizeTotalDragDiff ) );
            _resizePane.PaneElement.Height = newHeight;

            if ( !_resizeUpperPane.IsMainPane )
            {
                _resizeUpperPane.PaneElement.Height = _resizeTotalHeight - newHeight;
            }

            if ( _resizePane.IsTabbed )
            {
                ( from item in _items
                  where item.IsTabbed
                  select item ).ForEachDo( delegate ( ItemPane item )
                  {
                      item.PaneElement.Height = newHeight;
                  } );
            }
        }

        private void OnItemContainerResized( object sender, DragCompletedEventArgs e )
        {
            _resizePane = _resizeUpperPane = ( ItemPane ) null;
            _resizeTotalDragDiff = _resizeInitialHeight = 0.0;
        }

        private double CoerceDesiredHeight( ItemPane pane, double desiredHeight )
        {
            double actualHeight = pane.PaneElement.ActualHeight;
            double num = Math.Min(desiredHeight - actualHeight, _mainPane.ActualHeight);
            return actualHeight + num;
        }

        private void SynchronizeTabbedHeight( UltrachartGroupPane itemContainer )
        {
            ItemPane itemPane = _items.FirstOrDefault<ItemPane>((Func<ItemPane, bool>) (item =>
            {
                if (item.IsTabbed)
                {
                    return item.PaneElement != itemContainer;
                }

                return false;
            }));
            if ( itemPane == null )
            {
                return;
            }

            itemContainer.Height = itemPane.PaneElement.Height;
        }

        private void OnItemContainerSizeChanged( object sender, SizeChangedEventArgs e )
        {
            UltrachartGroupPane itemContainer = sender as UltrachartGroupPane;
            if ( itemContainer == null )
            {
                return;
            }

            Size size = e.NewSize;
            double height1 = size.Height;
            ref double local = ref height1;
            size = e.PreviousSize;
            double height2 = size.Height;
            if ( local.CompareTo( height2 ) == 0 || _items.FirstOrDefault<ItemPane>( ( Func<ItemPane, bool> ) ( item =>
            {
                if ( item.PaneElement == itemContainer )
                {
                    return item.IsTabbed;
                }

                return false;
            } ) ) == null )
            {
                return;
            }

            _items.Where<ItemPane>( ( Func<ItemPane, bool> ) ( item => item.IsTabbed ) ).ForEachDo<ItemPane>( ( Action<ItemPane> ) ( item => item.PaneElement.Height = e.NewSize.Height ) );
        }

        private static void OnVerticalChartGroupChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface surface = d as UltrachartSurface;
            if ( surface == null )
            {
                throw new InvalidOperationException( "VerticalChartGroupProperty can only be applied to types UltrachartSurface derived types" );
            }

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;
            if ( string.IsNullOrEmpty( newValue ) )
            {
                surface.Loaded -= new RoutedEventHandler( UltrachartGroup.OnSurfaceLoaded );
                surface.Unloaded -= new RoutedEventHandler( UltrachartGroup.OnSurfaceUnloaded );
                UltrachartGroup.DetachUltrachartSurfaceFromGroup( surface );
            }
            else
            {
                if ( !( newValue != oldValue ) )
                {
                    return;
                }

                if ( !string.IsNullOrEmpty( oldValue ) )
                {
                    UltrachartGroup.DetachUltrachartSurfaceFromGroup( surface );
                }

                surface.Loaded -= new RoutedEventHandler( UltrachartGroup.OnSurfaceLoaded );
                surface.Unloaded -= new RoutedEventHandler( UltrachartGroup.OnSurfaceUnloaded );
                surface.Loaded += new RoutedEventHandler( UltrachartGroup.OnSurfaceLoaded );
                surface.Unloaded += new RoutedEventHandler( UltrachartGroup.OnSurfaceUnloaded );
                if ( !surface.IsLoaded )
                {
                    return;
                }

                UltrachartGroup.AttachUltrachartSurfaceToGroup( ( ISciChartSurface ) surface, newValue );
            }
        }

        private static void OnSurfaceLoaded( object sender, RoutedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = sender as UltrachartSurface;
            UltrachartGroup.AttachUltrachartSurfaceToGroup( ( ISciChartSurface ) ultrachartSurface, UltrachartGroup.GetVerticalChartGroup( ( DependencyObject ) ultrachartSurface ) );
        }

        private static void OnSurfaceUnloaded( object sender, RoutedEventArgs e )
        {
            UltrachartGroup.DetachUltrachartSurfaceFromGroup( sender as UltrachartSurface );
        }

        private static void AttachUltrachartSurfaceToGroup( ISciChartSurface surface, string newGroupName )
        {
            ChartGroup key = new ChartGroup(surface);
            if ( UltrachartGroup.VerticalChartGroups.ContainsKey( key ) )
            {
                return;
            }

            UltrachartGroup.VerticalChartGroups.Add( key, newGroupName );
            UltrachartGroup.SynchronizeAxisSizes( surface );
            surface.Rendered += new EventHandler<EventArgs>( UltrachartGroup.OnUltrachartRendered );
        }

        private static void DetachUltrachartSurfaceFromGroup( UltrachartSurface surface )
        {
            foreach ( KeyValuePair<ChartGroup, string> verticalChartGroup in UltrachartGroup.VerticalChartGroups )
            {
                if ( verticalChartGroup.Key.UltrachartSurface == surface )
                {
                    verticalChartGroup.Key.RestoreState();
                }
            }
            UltrachartGroup.VerticalChartGroups.Remove( new ChartGroup( ( ISciChartSurface ) surface ) );
            surface.Rendered -= new EventHandler<EventArgs>( UltrachartGroup.OnUltrachartRendered );
        }

        private static void OnUltrachartRendered( object sender, EventArgs e )
        {
            UltrachartGroup.SynchronizeAxisSizes( ( ISciChartSurface ) sender );
        }

        private static void SynchronizeAxisSizes( ISciChartSurface ultraChartSurface )
        {
            string verticalGroup;
            if ( !UltrachartGroup.VerticalChartGroups.TryGetValue( new ChartGroup( ultraChartSurface ), out verticalGroup ) )
            {
                return;
            }

            ChartGroup[] array = UltrachartGroup.VerticalChartGroups.Where<KeyValuePair<ChartGroup, string>>((Func<KeyValuePair<ChartGroup, string>, bool>) (pair => pair.Value == verticalGroup)).Select<KeyValuePair<ChartGroup, string>, ChartGroup>((Func<KeyValuePair<ChartGroup, string>, ChartGroup>) (p => p.Key)).ToArray<ChartGroup>();
            double leftAreaMaxCalculatedWidth = UltrachartGroup.CalculateMaxAxisAreaWidth((IEnumerable<ChartGroup>) array, AxisAlignment.Left);
            double rightAreaMaxCalculatedWidth = UltrachartGroup.CalculateMaxAxisAreaWidth((IEnumerable<ChartGroup>) array, AxisAlignment.Right);
            ( ( IEnumerable<ChartGroup> ) array ).Select<ChartGroup, ISciChartSurface>( ( Func<ChartGroup, ISciChartSurface> ) ( x => x.UltrachartSurface ) ).OfType<UltrachartSurface>().ForEachDo<UltrachartSurface>( ( Action<UltrachartSurface> ) ( x =>
            {
                if ( x.AxisAreaLeft != null )
                {
                    x.AxisAreaLeft.Margin = new Thickness( leftAreaMaxCalculatedWidth - x.AxisAreaLeft.ActualWidth, 0.0, 0.0, 0.0 );
                }

                if ( x.AxisAreaRight == null )
                {
                    return;
                }

                x.AxisAreaRight.Margin = new Thickness( 0.0, 0.0, rightAreaMaxCalculatedWidth - x.AxisAreaRight.ActualWidth, 0.0 );
            } ) );
        }

        private static double CalculateMaxAxisAreaWidth( IEnumerable<ChartGroup> synchronizedCharts, AxisAlignment axisAlignment )
        {
            return synchronizedCharts.Select<ChartGroup, IEnumerable<AxisBase>>( ( Func<ChartGroup, IEnumerable<AxisBase>> ) ( x => x.UltrachartSurface.YAxes.OfType<AxisBase>().Where<AxisBase>( ( Func<AxisBase, bool> ) ( axis => axis.AxisAlignment == axisAlignment ) ) ) ).Select<IEnumerable<AxisBase>, double>( ( Func<IEnumerable<AxisBase>, double> ) ( collection => collection.Aggregate<AxisBase, double>( 0.0, ( Func<double, AxisBase, double> ) ( ( sum, axis ) => sum + axis.ActualWidth ) ) ) ).Max();
        }

        private FrameworkElement GetItemFromTemplate()
        {
            return ItemTemplate.LoadContent() as FrameworkElement;
        }

        private static void OnIsTabbedChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartGroup ultrachartGroup = d as UltrachartGroup;
            if ( ( bool ) e.NewValue )
            {
                ultrachartGroup._addPane = new Action<ItemPane>( ultrachartGroup.AddTabbedPane );
                ultrachartGroup._items.Where<ItemPane>( ( Func<ItemPane, bool> ) ( container =>
                {
                    if ( !container.IsTabbed )
                    {
                        return !container.IsMainPane;
                    }

                    return false;
                } ) ).ForEachDo<ItemPane>( new Action<ItemPane>( ultrachartGroup.MovePane ) );
            }
            else
            {
                ultrachartGroup._addPane = new Action<ItemPane>( ultrachartGroup.AddStackedPane );
                ultrachartGroup._items.Where<ItemPane>( ( Func<ItemPane, bool> ) ( container =>
                {
                    if ( container.IsTabbed )
                    {
                        return !container.IsMainPane;
                    }

                    return false;
                } ) ).ForEachDo<ItemPane>( new Action<ItemPane>( ultrachartGroup.MovePane ) );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged( string propertyName )
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if ( propertyChanged == null )
            {
                return;
            }

            propertyChanged( ( object ) this, new PropertyChangedEventArgs( propertyName ) );
        }

        internal List<ItemPane> Panes
        {
            get
            {
                return _items;
            }
        }
    }
}
