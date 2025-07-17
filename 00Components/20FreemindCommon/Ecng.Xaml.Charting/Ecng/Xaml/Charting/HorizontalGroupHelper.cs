// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.HorizontalGroupHelper
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Visuals;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting
{
    public class HorizontalGroupHelper
    {
        public static readonly DependencyProperty HorizontalChartGroupProperty = DependencyProperty.RegisterAttached("HorizontalChartGroup", typeof (string), typeof (HorizontalGroupHelper), new PropertyMetadata((object) null, new PropertyChangedCallback(HorizontalGroupHelper.OnHorizontalChartGroupChanged)));
        internal static Dictionary<ChartGroup, string> HorizontalChartGroups = new Dictionary<ChartGroup, string>();

        public static void SetHorizontalChartGroup( DependencyObject element, string syncWidthGroup )
        {
            element.SetValue( HorizontalGroupHelper.HorizontalChartGroupProperty, ( object ) syncWidthGroup );
        }

        public static string GetHorizontalChartGroup( DependencyObject element )
        {
            return ( string ) element.GetValue( HorizontalGroupHelper.HorizontalChartGroupProperty );
        }

        private static void OnHorizontalChartGroupChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            UltrachartSurface surface = d as UltrachartSurface;
            if ( surface == null )
                throw new InvalidOperationException( "HorizontalChartGroupProperty can only be applied to types UltrachartSurface derived types" );
            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;
            if ( string.IsNullOrEmpty( newValue ) )
            {
                surface.Loaded -= new RoutedEventHandler( HorizontalGroupHelper.OnSurfaceLoaded );
                surface.Unloaded -= new RoutedEventHandler( HorizontalGroupHelper.OnSurfaceUnloaded );
                HorizontalGroupHelper.DetachUltrachartSurfaceFromGroup( surface );
            }
            else
            {
                if ( !( newValue != oldValue ) )
                    return;
                if ( !string.IsNullOrEmpty( oldValue ) )
                    HorizontalGroupHelper.DetachUltrachartSurfaceFromGroup( surface );
                surface.Loaded -= new RoutedEventHandler( HorizontalGroupHelper.OnSurfaceLoaded );
                surface.Unloaded -= new RoutedEventHandler( HorizontalGroupHelper.OnSurfaceUnloaded );
                surface.Loaded += new RoutedEventHandler( HorizontalGroupHelper.OnSurfaceLoaded );
                surface.Unloaded += new RoutedEventHandler( HorizontalGroupHelper.OnSurfaceUnloaded );
                if ( !surface.IsLoaded )
                    return;
                HorizontalGroupHelper.AttachUltrachartSurfaceToGroup( ( ISciChartSurface ) surface, newValue );
            }
        }

        private static void OnSurfaceLoaded( object sender, RoutedEventArgs e )
        {
            UltrachartSurface ultrachartSurface = sender as UltrachartSurface;
            string horizontalChartGroup = HorizontalGroupHelper.GetHorizontalChartGroup((DependencyObject) ultrachartSurface);
            HorizontalGroupHelper.AttachUltrachartSurfaceToGroup( ( ISciChartSurface ) ultrachartSurface, horizontalChartGroup );
        }

        private static void OnSurfaceUnloaded( object sender, RoutedEventArgs e )
        {
            HorizontalGroupHelper.DetachUltrachartSurfaceFromGroup( sender as UltrachartSurface );
        }

        private static void AttachUltrachartSurfaceToGroup( ISciChartSurface surface, string newGroupName )
        {
            ChartGroup key = new ChartGroup(surface);
            if ( HorizontalGroupHelper.HorizontalChartGroups.ContainsKey( key ) )
                return;
            HorizontalGroupHelper.HorizontalChartGroups.Add( key, newGroupName );
            HorizontalGroupHelper.SynchronizeAxisSizes( surface );
            surface.Rendered += new EventHandler<EventArgs>( HorizontalGroupHelper.OnUltrachartRendered );
        }

        private static void DetachUltrachartSurfaceFromGroup( UltrachartSurface surface )
        {
            foreach ( KeyValuePair<ChartGroup, string> horizontalChartGroup in HorizontalGroupHelper.HorizontalChartGroups )
            {
                if ( horizontalChartGroup.Key.UltrachartSurface == surface )
                    horizontalChartGroup.Key.RestoreState();
            }
            HorizontalGroupHelper.HorizontalChartGroups.Remove( new ChartGroup( ( ISciChartSurface ) surface ) );
            surface.Rendered -= new EventHandler<EventArgs>( HorizontalGroupHelper.OnUltrachartRendered );
        }

        private static void OnUltrachartRendered( object sender, EventArgs e )
        {
            HorizontalGroupHelper.SynchronizeAxisSizes( ( ISciChartSurface ) sender );
        }

        private static void SynchronizeAxisSizes( ISciChartSurface ultraChartSurface )
        {
            string HorizontalGroup;
            if ( !HorizontalGroupHelper.HorizontalChartGroups.TryGetValue( new ChartGroup( ultraChartSurface ), out HorizontalGroup ) )
                return;
            ChartGroup[] array = HorizontalGroupHelper.HorizontalChartGroups.Where<KeyValuePair<ChartGroup, string>>((Func<KeyValuePair<ChartGroup, string>, bool>) (pair => pair.Value == HorizontalGroup)).Select<KeyValuePair<ChartGroup, string>, ChartGroup>((Func<KeyValuePair<ChartGroup, string>, ChartGroup>) (p => p.Key)).ToArray<ChartGroup>();
            double bottomAreaMaxHeight = HorizontalGroupHelper.CalculateMaxAxisAreaWidth((IEnumerable<ChartGroup>) array, AxisAlignment.Bottom);
            double topAreaMaxHeight = HorizontalGroupHelper.CalculateMaxAxisAreaWidth((IEnumerable<ChartGroup>) array, AxisAlignment.Top);
            ( ( IEnumerable<ChartGroup> ) array ).Select<ChartGroup, ISciChartSurface>( ( Func<ChartGroup, ISciChartSurface> ) ( x => x.UltrachartSurface ) ).OfType<UltrachartSurface>().ForEachDo<UltrachartSurface>( ( Action<UltrachartSurface> ) ( x =>
            {
                if ( x.AxisAreaBottom != null )
                    x.AxisAreaBottom.Margin = new Thickness( 0.0, 0.0, 0.0, bottomAreaMaxHeight - x.AxisAreaBottom.ActualHeight );
                if ( x.AxisAreaTop == null )
                    return;
                x.AxisAreaTop.Margin = new Thickness( 0.0, topAreaMaxHeight - x.AxisAreaTop.ActualHeight, 0.0, 0.0 );
            } ) );
        }

        private static double CalculateMaxAxisAreaWidth( IEnumerable<ChartGroup> synchronizedCharts, AxisAlignment axisAlignment )
        {
            return synchronizedCharts.Select<ChartGroup, IEnumerable<AxisBase>>( ( Func<ChartGroup, IEnumerable<AxisBase>> ) ( x => x.UltrachartSurface.YAxes.OfType<AxisBase>().Where<AxisBase>( ( Func<AxisBase, bool> ) ( axis => axis.AxisAlignment == axisAlignment ) ) ) ).Select<IEnumerable<AxisBase>, double>( ( Func<IEnumerable<AxisBase>, double> ) ( collection => collection.Aggregate<AxisBase, double>( 0.0, ( Func<double, AxisBase, double> ) ( ( sum, axis ) => sum + axis.ActualHeight ) ) ) ).Max();
        }
    }
}
