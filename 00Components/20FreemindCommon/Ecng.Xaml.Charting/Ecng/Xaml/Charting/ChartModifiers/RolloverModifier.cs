// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.RolloverModifier
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Shapes;
namespace fx.Xaml.Charting
{
    public class RolloverModifier : VerticalSliceModifierBase
    {
        public static readonly DependencyProperty IncludeSeriesProperty = DependencyProperty.RegisterAttached("IncludeSeries", typeof (bool), typeof (RolloverModifier), new PropertyMetadata((object) true));
        public static readonly DependencyProperty DrawVerticalLineProperty = DependencyProperty.Register(nameof (DrawVerticalLine), typeof (bool), typeof (RolloverModifier), new PropertyMetadata((object) true));
        private bool _isLineDrawn;
        private Line _line;
        private IPlaceRolloverLineStrategy _placeRolloverLineStrategy;

        public static bool GetIncludeSeries( DependencyObject obj )
        {
            return ( bool ) obj.GetValue( RolloverModifier.IncludeSeriesProperty );
        }

        public static void SetIncludeSeries( DependencyObject obj, bool value )
        {
            obj.SetValue( RolloverModifier.IncludeSeriesProperty, ( object ) value );
        }

        public RolloverModifier()
        {
            DefaultStyleKey = ( object ) typeof( RolloverModifier );
            SetCurrentValue( InspectSeriesModifierBase.SeriesDataProperty, ( object ) new ChartDataObject() );
        }

        public bool DrawVerticalLine
        {
            get
            {
                return ( bool ) GetValue( RolloverModifier.DrawVerticalLineProperty );
            }
            set
            {
                SetValue( RolloverModifier.DrawVerticalLineProperty, ( object ) value );
            }
        }

        protected override void FillWithIncludedSeries( IEnumerable<SeriesInfo> infos, ObservableCollection<SeriesInfo> seriesInfos )
        {
            infos.ForEachDo<SeriesInfo>( ( Action<SeriesInfo> ) ( info =>
                {
                    if ( !info.RenderableSeries.GetIncludeSeries( Modifier.Rollover ) )
                    {
                        return;
                    }

                    seriesInfos.Add( info );
                } ) );
        }

        protected override FrameworkElement GetRolloverMarkerFrom( SeriesInfo seriesInfo )
        {
            BandSeriesInfo bandSeriesInfo = seriesInfo as BandSeriesInfo;
            FastBandRenderableSeries renderableSeries = seriesInfo.RenderableSeries as FastBandRenderableSeries;
            if ( bandSeriesInfo != null && renderableSeries != null && bandSeriesInfo.IsFirstSeries )
            {
                return renderableSeries.RolloverMarker1;
            }
            return seriesInfo.RenderableSeries.RolloverMarker;
        }

        protected override void OnSelectedSeriesChanged( IEnumerable<IRenderableSeries> oldSeries, IEnumerable<IRenderableSeries> newSeries )
        {
            base.OnSelectedSeriesChanged( oldSeries, newSeries );
            RemoveLine();
        }

        protected override void ClearAll()
        {
            base.ClearAll();
            RemoveLine();
        }

        protected override void HandleMasterMouseEvent( Point mousePoint )
        {
            _isLineDrawn = false;
            RemoveLine();
            base.HandleMasterMouseEvent( mousePoint );
        }

        private void RemoveLine()
        {
            if ( ModifierSurface == null || _line == null )
            {
                return;
            }

            ModifierSurface.Children.Remove( ( UIElement ) _line );
        }

        protected override void TryUpdateOverlays( Point atPoint )
        {
            base.TryUpdateOverlays( atPoint );
            if ( !IsEnabledAt( atPoint ) )
            {
                return;
            }

            if ( ShowAxisLabels && !_isLineDrawn )
            {
                UpdateAxesOverlay( atPoint );
            }

            TryDrawVerticalLine( atPoint );
        }

        private void TryDrawVerticalLine( Point showLineAt )
        {
            if ( !DrawVerticalLine || _isLineDrawn )
            {
                return;
            }

            bool isVerticalChart = XAxis != null && !XAxis.IsHorizontalAxis;
            _isLineDrawn = ShowVerticalLine( showLineAt, isVerticalChart );
        }

        private bool ShowVerticalLine( Point hitPoint, bool isVerticalChart )
        {
            _placeRolloverLineStrategy = GetPlaceRolloverLineStrategy();
            _line = _placeRolloverLineStrategy.ShowVerticalLine( hitPoint, isVerticalChart );
            bool flag = _line != null;
            if ( flag )
            {
                _line.Style = LineOverlayStyle;
                _line.IsHitTestVisible = false;
                ModifierSurface.Children.Add( ( UIElement ) _line );
            }
            return flag;
        }

        private IPlaceRolloverLineStrategy GetPlaceRolloverLineStrategy()
        {
            return XAxis == null || !XAxis.IsPolarAxis ? ( IPlaceRolloverLineStrategy ) ( _placeRolloverLineStrategy as CartesianPlaceRolloverLineStrategy ?? new CartesianPlaceRolloverLineStrategy( ( IChartModifier ) this ) ) : ( IPlaceRolloverLineStrategy ) ( _placeRolloverLineStrategy as PolarPlaceRolloverLineStrategy ?? new PolarPlaceRolloverLineStrategy( ( IChartModifier ) this ) );
        }
    }
}
