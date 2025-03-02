using Ecng.Xaml;
using SciChart.Charting;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Charting.Visuals.TradeChart.MultiPane;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace fx.Charting
{
    internal sealed class ScichartOverviewSurface
    {
        public static readonly DependencyProperty ItemsControlParentSurfaceProperty = DependencyProperty.RegisterAttached( "ItemsControlParentSurface", typeof( ItemsControl ), typeof( ScichartOverviewSurface ), new PropertyMetadata( null, new PropertyChangedCallback( OnItemsControlParentSurfacePropertyChanged ) ) );

        public static void SetItemsControlParentSurface( UIElement uiElement, ItemsControl itemControl )
        {
            uiElement.SetValue( ItemsControlParentSurfaceProperty, itemControl );
        }

        public static ItemsControl GetItemsControlParentSurface( UIElement uiElement )
        {
            return ( ItemsControl )uiElement.GetValue( ItemsControlParentSurfaceProperty );
        }

        private static void OnItemsControlParentSurfacePropertyChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var overview    = ( SciChartOverview ) d;
            var itemControl = ( ItemsControl ) e.NewValue;

            itemControl.Loaded += ( s, evt ) =>
            {
                var itemsSource = itemControl.ItemsSource as INotifyCollectionChanged;

                if ( itemsSource != null )
                {
                    itemsSource.CollectionChanged += new NotifyCollectionChangedEventHandler( ( x, ev ) => AddItemControlToOverview( overview, itemControl ) );
                }

                AddItemControlToOverview( overview, itemControl );
            };
        }

        private static void AddItemControlToOverview( SciChartOverview ov, ItemsControl itemControl )
        {
            var myTypes = itemControl.GetType( );
            var t       = myTypes.GetTypeInfo( );
            var pList   = t.DeclaredProperties;
            var info    = pList.FirstOrDefault( p => p.Name == "Panes" );

            if ( info == null ) return;
            
            var itemPaneList = ( List< SciChart.Charting.Visuals.TradeChart.GroupPane> ) info.GetValue( itemControl, null );

            if ( itemPaneList.Count > 0 )
            {
                var child = itemPaneList[ 0 ].PaneElement.FindVisualChild<SciChartSurface>( );

                ov.ParentSurface = child;

                if ( child != null && child.RenderableSeries != null && child.RenderableSeries.Count > 0 )
                {
                    ov.DataSeries = child.RenderableSeries.FirstOrDefault().DataSeries;
                }

                
            }            
        }
    }
}
