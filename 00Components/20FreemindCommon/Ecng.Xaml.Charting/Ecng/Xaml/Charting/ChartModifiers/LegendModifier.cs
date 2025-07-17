// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ChartModifiers.LegendModifier
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Visuals.RenderableSeries;

namespace Ecng.Xaml.Charting.ChartModifiers
{
    public class LegendModifier : InspectSeriesModifierBase
    {
        public static readonly DependencyProperty LegendDataProperty = DependencyProperty.Register(nameof (LegendData), typeof (ChartDataObject), typeof (LegendModifier), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty GetLegendDataForProperty = DependencyProperty.Register(nameof (GetLegendDataFor), typeof (SourceMode), typeof (LegendModifier), new PropertyMetadata((object) SourceMode.AllSeries, (PropertyChangedCallback) ((s, e) => ((LegendModifier) s).UpdateLegend())));
        public static readonly DependencyProperty LegendPlacementProperty = DependencyProperty.Register(nameof (LegendPlacement), typeof (LegendPlacement), typeof (LegendModifier), new PropertyMetadata((object) LegendPlacement.Inside));
        public static readonly DependencyProperty LegendItemTemplateProperty = DependencyProperty.Register(nameof (LegendItemTemplate), typeof (DataTemplate), typeof (LegendModifier), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (LegendModifier), new PropertyMetadata((object) Orientation.Vertical));
        public static readonly DependencyProperty ShowSeriesMarkersProperty = DependencyProperty.Register(nameof (ShowSeriesMarkers), typeof (bool), typeof (LegendModifier), new PropertyMetadata((object) true));
        public static readonly DependencyProperty ShowVisibilityCheckboxesProperty = DependencyProperty.Register(nameof (ShowVisibilityCheckboxes), typeof (bool), typeof (LegendModifier), new PropertyMetadata((object) true));
        public static readonly DependencyProperty ShowLegendProperty = DependencyProperty.Register(nameof (ShowLegend), typeof (bool), typeof (LegendModifier), new PropertyMetadata((object) false, new PropertyChangedCallback(LegendModifier.OnShowLegendChanged)));
        public static readonly DependencyProperty LegendTemplateProperty = DependencyProperty.Register(nameof (LegendTemplate), typeof (ControlTemplate), typeof (LegendModifier), new PropertyMetadata(new PropertyChangedCallback(LegendModifier.OnLegendTemplateChanged)));
        private FrameworkElement _legend;

        public LegendModifier()
        {
            DefaultStyleKey = ( object ) typeof( LegendModifier );
            SetCurrentValue( InspectSeriesModifierBase.SeriesDataProperty, ( object ) new ChartDataObject() );
        }

        public bool ShowVisibilityCheckboxes
        {
            get
            {
                return ( bool ) GetValue( LegendModifier.ShowVisibilityCheckboxesProperty );
            }
            set
            {
                SetValue( LegendModifier.ShowVisibilityCheckboxesProperty, ( object ) value );
            }
        }

        public bool ShowSeriesMarkers
        {
            get
            {
                return ( bool ) GetValue( LegendModifier.ShowSeriesMarkersProperty );
            }
            set
            {
                SetValue( LegendModifier.ShowSeriesMarkersProperty, ( object ) value );
            }
        }

        public LegendPlacement LegendPlacement
        {
            get
            {
                return ( LegendPlacement ) GetValue( LegendModifier.LegendPlacementProperty );
            }
            set
            {
                SetValue( LegendModifier.LegendPlacementProperty, ( object ) value );
            }
        }

        public DataTemplate LegendItemTemplate
        {
            get
            {
                return ( DataTemplate ) GetValue( LegendModifier.LegendItemTemplateProperty );
            }
            set
            {
                SetValue( LegendModifier.LegendItemTemplateProperty, ( object ) value );
            }
        }

        public ChartDataObject LegendData
        {
            get
            {
                return ( ChartDataObject ) GetValue( LegendModifier.LegendDataProperty );
            }
            set
            {
                SetValue( LegendModifier.LegendDataProperty, ( object ) value );
            }
        }

        public Orientation Orientation
        {
            get
            {
                return ( Orientation ) GetValue( LegendModifier.OrientationProperty );
            }
            set
            {
                SetValue( LegendModifier.OrientationProperty, ( object ) value );
            }
        }

        public bool ShowLegend
        {
            get
            {
                return ( bool ) GetValue( LegendModifier.ShowLegendProperty );
            }
            set
            {
                SetValue( LegendModifier.ShowLegendProperty, ( object ) value );
            }
        }

        public ControlTemplate LegendTemplate
        {
            get
            {
                return ( ControlTemplate ) GetValue( LegendModifier.LegendTemplateProperty );
            }
            set
            {
                SetValue( LegendModifier.LegendTemplateProperty, ( object ) value );
            }
        }

        public SourceMode GetLegendDataFor
        {
            get
            {
                return ( SourceMode ) GetValue( LegendModifier.GetLegendDataForProperty );
            }
            set
            {
                SetValue( LegendModifier.GetLegendDataForProperty, ( object ) value );
            }
        }

        public override void OnAttached()
        {
            if ( ShowLegend )
            {
                ParentSurface.RootGrid.SafeAddChild( ( object ) _legend, -1 );
            }

            base.OnAttached();
        }

        public override void OnDetached()
        {
            ParentSurface.RootGrid.SafeRemoveChild( ( object ) _legend );
            base.OnDetached();
        }

        protected override void ClearAll()
        {
        }

        protected override void HandleMasterMouseEvent( Point mousePoint )
        {
        }

        protected override void HandleSlaveMouseEvent( Point mousePoint )
        {
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
        }

        public override void OnParentSurfaceRendered( UltrachartRenderedMessage e )
        {
            base.OnParentSurfaceRendered( e );
            UpdateLegend();
        }

        public virtual void UpdateLegend()
        {
            if ( !IsEnabled || !IsAttached || ( ParentSurface == null || ParentSurface.RenderableSeries == null ) )
            {
                return;
            }

            ObservableCollection<SeriesInfo> seriesInfo1 = GetSeriesInfo(ParentSurface.RenderableSeries.Where<IRenderableSeries>(new Func<IRenderableSeries, bool>(IsSeriesValid)));
            ObservableCollection<SeriesInfo> seriesInfo2 = SeriesData.SeriesInfo;
            IRenderableSeries[] newRenderableSeries = seriesInfo1.Select<SeriesInfo, IRenderableSeries>((Func<SeriesInfo, IRenderableSeries>) (x => x.RenderableSeries)).ToArray<IRenderableSeries>();
            seriesInfo2.RemoveWhere<SeriesInfo>( ( Predicate<SeriesInfo> ) ( info => !( ( IEnumerable<IRenderableSeries> ) newRenderableSeries ).Contains<IRenderableSeries>( info.RenderableSeries ) ) );
            foreach ( SeriesInfo seriesInfo3 in ( Collection<SeriesInfo> ) seriesInfo1 )
            {
                SeriesInfo newSeriesInfo = seriesInfo3;
                SeriesInfo oldSeriesInfo = seriesInfo2.FirstOrDefault<SeriesInfo>((Func<SeriesInfo, bool>) (x => x.RenderableSeries.Equals((object) newSeriesInfo.RenderableSeries)));
                if ( oldSeriesInfo != null )
                {
                    LegendModifier.UpdateSeriesInfo( oldSeriesInfo, newSeriesInfo );
                }
                else
                {
                    seriesInfo2.Add( newSeriesInfo );
                }
            }
        }

        private new bool IsSeriesValid( IRenderableSeries series )
        {
            if ( series != null && CheckSeriesMode( series ) )
            {
                return series.DataSeries != null;
            }

            return false;
        }

        private bool CheckSeriesMode( IRenderableSeries series )
        {
            if ( GetLegendDataFor == SourceMode.AllSeries || series.IsVisible && GetLegendDataFor == SourceMode.AllVisibleSeries || series.IsSelected && GetLegendDataFor == SourceMode.SelectedSeries )
            {
                return true;
            }

            if ( !series.IsSelected )
            {
                return GetLegendDataFor == SourceMode.UnselectedSeries;
            }

            return false;
        }

        protected virtual ObservableCollection<SeriesInfo> GetSeriesInfo( IEnumerable<IRenderableSeries> allSeries )
        {
            ObservableCollection<SeriesInfo> observableCollection = new ObservableCollection<SeriesInfo>();
            if ( allSeries != null )
            {
                foreach ( IRenderableSeries renderableSeries in allSeries )
                {
                    SeriesInfo seriesInfo = renderableSeries.GetSeriesInfo(renderableSeries.HitTest(new Point(ModifierSurface.ActualWidth, 0.0), false));
                    observableCollection.Add( seriesInfo );
                }
            }
            return observableCollection;
        }

        private static void UpdateSeriesInfo( SeriesInfo oldSeriesInfo, SeriesInfo newSeriesInfo )
        {
            oldSeriesInfo.DataSeriesIndex = newSeriesInfo.DataSeriesIndex;
            oldSeriesInfo.DataSeriesType = newSeriesInfo.DataSeriesType;
            oldSeriesInfo.IsHit = newSeriesInfo.IsHit;
            oldSeriesInfo.SeriesColor = newSeriesInfo.SeriesColor;
            oldSeriesInfo.SeriesName = newSeriesInfo.SeriesName;
            oldSeriesInfo.Value = newSeriesInfo.Value;
            oldSeriesInfo.XValue = newSeriesInfo.XValue;
            oldSeriesInfo.YValue = newSeriesInfo.YValue;
            oldSeriesInfo.XyCoordinate = newSeriesInfo.XyCoordinate;
        }

        private static void OnShowLegendChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            LegendModifier legendModifier = d as LegendModifier;
            if ( legendModifier == null || legendModifier.ParentSurface == null || legendModifier.LegendTemplate == null )
            {
                return;
            }

            if ( legendModifier.ShowLegend )
            {
                legendModifier._legend.DataContext = ( object ) legendModifier;
                legendModifier.ParentSurface.RootGrid.SafeAddChild( ( object ) legendModifier._legend, -1 );
            }
            else
            {
                legendModifier.ParentSurface.RootGrid.SafeRemoveChild( ( object ) legendModifier._legend );
            }
        }

        private static void OnLegendTemplateChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            LegendModifier legendModifier = d as LegendModifier;
            if ( legendModifier == null || e.NewValue == null )
            {
                return;
            }

            legendModifier._legend = legendModifier._legend ?? ( FrameworkElement ) new LegendPlaceholder();
            legendModifier._legend.DataContext = ( object ) legendModifier;
        }
    }
}
