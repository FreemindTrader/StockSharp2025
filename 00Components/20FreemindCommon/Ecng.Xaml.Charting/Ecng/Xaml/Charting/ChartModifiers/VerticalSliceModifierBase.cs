// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.VerticalSliceModifierBase
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Common.Helpers;
using StockSharp.Xaml.Charting.Model.DataSeries;
using StockSharp.Xaml.Charting.Visuals.Axes;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public abstract class VerticalSliceModifierBase : TooltipModifierBase
    {
        internal static readonly DependencyProperty RolloverLabelProperty = DependencyProperty.RegisterAttached("RolloverLabel", typeof (TemplatableControl), typeof (RolloverModifier), new PropertyMetadata((PropertyChangedCallback) null));
        private List<FrameworkElement> _rolloverMarkers = new List<FrameworkElement>();
        private List<FrameworkElement> _tooltipLabels = new List<FrameworkElement>();
        private bool _needToUpdateTooltips;

        internal static Control GetRolloverLabel( DependencyObject o )
        {
            return ( Control ) o.GetValue( VerticalSliceModifierBase.RolloverLabelProperty );
        }

        internal static void SetRolloverLabel( DependencyObject o, Control value )
        {
            o.SetValue( VerticalSliceModifierBase.RolloverLabelProperty, ( object ) value );
        }

        public double HoverDelay
        {
            get
            {
                return ( double ) this.GetValue( TooltipModifierBase.HoverDelayProperty );
            }
            set
            {
                this.SetValue( TooltipModifierBase.HoverDelayProperty, ( object ) value );
            }
        }

        protected VerticalSliceModifierBase()
        {
            this._delayActionHelper = new DelayActionHelper()
            {
                Interval = this.HoverDelay
            };
        }

        public override void OnDetached()
        {
            base.OnDetached();
            this.ClearAll();
        }

        protected override void OnIsEnabledChanged()
        {
            base.OnIsEnabledChanged();
            base.OnDetached();
            this.RemoveMarkers( false );
        }

        private void RemoveMarkers( bool removeLabels = false )
        {
            if ( this.ModifierSurface != null )
            {
                foreach ( FrameworkElement rolloverMarker in this._rolloverMarkers )
                {
                    this.DetachRolloverMarker( rolloverMarker );
                    if ( removeLabels )
                        this.RemoveLabelFor( rolloverMarker );
                }
            }
            this._rolloverMarkers.Clear();
        }

        protected virtual void DetachRolloverMarker( FrameworkElement rolloverMarker )
        {
            this.ModifierSurface.Children.Remove( ( UIElement ) rolloverMarker );
            if ( this.HasToShowTooltip() )
                return;
            rolloverMarker.MouseMove -= new MouseEventHandler( this.OnRolloverMarkerMouseMove );
            rolloverMarker.MouseLeave -= new MouseEventHandler( this.OnRolloverMarkerMouseLeave );
            rolloverMarker.MouseLeftButtonDown -= new MouseButtonEventHandler( this.OnRolloverMarkerMouseLeave );
        }

        private void RemoveLabelFor( FrameworkElement rolloverMarker )
        {
            FrameworkElement frameworkElement = (FrameworkElement) rolloverMarker.GetValue(VerticalSliceModifierBase.RolloverLabelProperty);
            if ( frameworkElement == null )
                return;
            this._tooltipLabels.Remove( frameworkElement );
            if ( !this.ModifierSurface.Children.Contains( ( UIElement ) frameworkElement ) )
                return;
            this.ModifierSurface.Children.Remove( ( UIElement ) frameworkElement );
        }

        protected override bool IsHitPointValid( HitTestInfo hitTestInfo )
        {
            bool flag = !hitTestInfo.IsEmpty() && hitTestInfo.IsWithinDataBounds && hitTestInfo.IsVerticalHit && hitTestInfo.HitTestPoint.X.IsDefined() && hitTestInfo.HitTestPoint.Y.IsDefined();
            if ( hitTestInfo.DataSeriesType == DataSeriesType.Xyy )
                flag &= hitTestInfo.Y1HitTestPoint.Y.IsDefined();
            return flag;
        }

        protected override void HandleSlaveMouseEvent( Point mousePoint )
        {
            this.HandleMasterMouseEvent( mousePoint );
        }

        protected override void HandleMasterMouseEvent( Point mousePoint )
        {
            if ( !this.IsAttached || !this.IsEnabled || this.ParentSurface == null )
                return;
            if ( this.ShowTooltipOn == ShowTooltipOptions.MouseHover )
                this.ClearTooltipLabels();
            this.RemoveMarkers( false );
            ObservableCollection<IRenderableSeries> renderableSeries = this.ParentSurface.RenderableSeries;
            ObservableCollection<SeriesInfo> observableCollection = new ObservableCollection<SeriesInfo>();
            if ( renderableSeries != null )
            {
                this.FillWithIncludedSeries( this.GetSeriesInfoAt( mousePoint ), observableCollection );
                IAxis xAxis = this.XAxis;
                if ( xAxis != null )
                {
                    foreach ( SeriesInfo seriesInfo in ( IEnumerable<SeriesInfo> ) observableCollection.OrderBy<SeriesInfo, double>( ( Func<SeriesInfo, double> ) ( info =>
                    {
                        if ( !xAxis.IsHorizontalAxis )
                            return info.XyCoordinate.X;
                        return info.XyCoordinate.Y;
                    } ) ) )
                    {
                        FrameworkElement rolloverMarkerFrom = this.GetRolloverMarkerFrom(seriesInfo);
                        if ( this.TryAddRolloverMarker( rolloverMarkerFrom, seriesInfo ) )
                        {
                            this.AttachTooltipLabelToMarker( rolloverMarkerFrom, seriesInfo );
                            this.TryUpdateOverlays( seriesInfo.XyCoordinate );
                        }
                    }
                }
                this.UpdateOverlays( mousePoint );
            }
            this.SeriesData.UpdateSeriesInfo( ( IEnumerable<SeriesInfo> ) observableCollection );
        }

        protected virtual void FillWithIncludedSeries( IEnumerable<SeriesInfo> infos, ObservableCollection<SeriesInfo> seriesInfos )
        {
            infos.ForEachDo<SeriesInfo>( new Action<SeriesInfo>( ( ( Collection<SeriesInfo> ) seriesInfos ).Add ) );
        }

        protected override IEnumerable<SeriesInfo> GetSeriesInfoAt( Point point )
        {
            return this.GetSeriesInfoAt( ( Func<IRenderableSeries, HitTestInfo> ) ( renderSeries => renderSeries.VerticalSliceHitTest( point, this.UseInterpolation ) ) ).SplitToSinglePointInfo();
        }

        protected abstract FrameworkElement GetRolloverMarkerFrom( SeriesInfo seriesInfo );

        private bool TryAddRolloverMarker( FrameworkElement rolloverMarker, SeriesInfo seriesInfo )
        {
            bool flag = rolloverMarker != null;
            if ( flag )
            {
                flag = this.TryAddRolloverMarker( rolloverMarker, seriesInfo.XyCoordinate );
                if ( flag )
                    this.AttachRolloverMarker( rolloverMarker );
            }
            return flag;
        }

        private bool TryAddRolloverMarker( FrameworkElement rolloverMarker, Point showRolloverAt )
        {
            bool flag = this.ModifierSurface.IsPointWithinBounds(showRolloverAt);
            if ( flag )
                this.PlaceRolloverMarker( rolloverMarker, showRolloverAt );
            return flag;
        }

        private void PlaceRolloverMarker( FrameworkElement rolloverMarker, Point hitPoint )
        {
            double left = Canvas.GetLeft((UIElement) rolloverMarker);
            double top = Canvas.GetTop((UIElement) rolloverMarker);
            rolloverMarker.MeasureArrange();
            double length1 = hitPoint.X - rolloverMarker.DesiredSize.Width / 2.0;
            double length2 = hitPoint.Y - rolloverMarker.DesiredSize.Height / 2.0;
            if ( left.Equals( length1 ) && top.Equals( length2 ) )
                return;
            this._needToUpdateTooltips = true;
            Canvas.SetLeft( ( UIElement ) rolloverMarker, length1 );
            Canvas.SetTop( ( UIElement ) rolloverMarker, length2 );
        }

        private void AttachRolloverMarker( FrameworkElement rolloverMarker )
        {
            if ( !this.HasToShowTooltip() )
            {
                rolloverMarker.MouseMove += new MouseEventHandler( this.OnRolloverMarkerMouseMove );
                rolloverMarker.MouseLeave += new MouseEventHandler( this.OnRolloverMarkerMouseLeave );
                rolloverMarker.MouseLeftButtonDown += new MouseButtonEventHandler( this.OnRolloverMarkerMouseLeave );
            }
            this.ModifierSurface.Children.Add( ( UIElement ) rolloverMarker );
            this._rolloverMarkers.Add( rolloverMarker );
        }

        private void AttachTooltipLabelToMarker( FrameworkElement rolloverMarker, SeriesInfo seriesInfo )
        {
            FrameworkElement fromTemplate = rolloverMarker.GetValue(VerticalSliceModifierBase.RolloverLabelProperty) as FrameworkElement;
            if ( fromTemplate == null )
            {
                fromTemplate = ( FrameworkElement ) this.CreateFromTemplate( this.TooltipLabelTemplate, this.TooltipLabelTemplateSelector, ( object ) seriesInfo );
                rolloverMarker.SetValue( VerticalSliceModifierBase.RolloverLabelProperty, ( object ) fromTemplate );
            }
            fromTemplate.DataContext = ( object ) seriesInfo;
        }

        protected virtual void TryUpdateOverlays( Point atPoint )
        {
            if ( !this.IsEnabledAt( atPoint ) )
                return;
            this.TryUpdateAxesLabels( atPoint );
        }

        protected void TryUpdateAxesLabels( Point showAxesLabelsAt )
        {
            if ( !this.ShowAxisLabels || this.IsLabelsCacheActual() )
                return;
            this.RecreateLabels();
        }

        private void UpdateOverlays( Point mousePoint )
        {
            bool flag = !this._rolloverMarkers.IsNullOrEmpty<FrameworkElement>();
            if ( flag && this.HasToShowTooltip() )
            {
                if ( this.ShowTooltipOn == ShowTooltipOptions.MouseHover )
                    this._delayActionHelper.Start( ( Action ) ( () => this.UpdateTooltipLabels( this._rolloverMarkers ) ) );
                else
                    this.UpdateTooltipLabels( this._rolloverMarkers );
            }
            else if ( this.ShowTooltipOn != ShowTooltipOptions.MouseOver || !flag )
                this.ClearTooltipLabels();
            this.TryUpdateOverlays( mousePoint );
        }

        private void ClearTooltipLabels()
        {
            if ( this.ModifierSurface == null )
                return;
            foreach ( FrameworkElement tooltipLabel in this._tooltipLabels )
            {
                if ( this.ModifierSurface.Children.Contains( ( UIElement ) tooltipLabel ) )
                    this.ModifierSurface.Children.Remove( ( UIElement ) tooltipLabel );
            }
            this._tooltipLabels.Clear();
            foreach ( FrameworkElement rolloverMarker in this._rolloverMarkers )
                this.RemoveLabelFor( rolloverMarker );
        }

        private void OnRolloverMarkerMouseLeave( object sender, MouseEventArgs e )
        {
            this.ClearTooltipLabels();
            this._needToUpdateTooltips = true;
        }

        private void OnRolloverMarkerMouseMove( object sender, MouseEventArgs e )
        {
            List<FrameworkElement> rolloverMarkersAt = this.GetRolloverMarkersAt(e.GetPosition((IInputElement) (this.ModifierSurface as UIElement)));
            if ( !this._needToUpdateTooltips )
                return;
            this.UpdateTooltipLabels( rolloverMarkersAt );
            this._needToUpdateTooltips = false;
        }

        private List<FrameworkElement> GetRolloverMarkersAt( Point point )
        {
            List<FrameworkElement> frameworkElementList = new List<FrameworkElement>();
            foreach ( FrameworkElement rolloverMarker in this._rolloverMarkers )
            {
                if ( this.GetRolloverMarkerRect( rolloverMarker ).Contains( point ) )
                    frameworkElementList.Add( rolloverMarker );
            }
            return frameworkElementList;
        }

        private Rect GetRolloverMarkerRect( FrameworkElement rolloverMarker )
        {
            double y = (double) rolloverMarker.GetValue(Canvas.TopProperty);
            return new Rect( ( double ) rolloverMarker.GetValue( Canvas.LeftProperty ), y, rolloverMarker.ActualWidth, rolloverMarker.ActualHeight );
        }

        private void UpdateTooltipLabels( List<FrameworkElement> overlappedMarkers )
        {
            this.ClearTooltipLabels();
            if ( overlappedMarkers.IsEmpty<FrameworkElement>() )
                return;
            foreach ( FrameworkElement overlappedMarker in overlappedMarkers )
                this.MergeTooltipLabelFor( overlappedMarker );
            foreach ( FrameworkElement tooltipLabel in this._tooltipLabels )
            {
                if ( !this.ModifierSurface.Children.Contains( ( UIElement ) tooltipLabel ) )
                {
                    ( tooltipLabel.Parent as Panel ).SafeRemoveChild( ( object ) tooltipLabel );
                    this.ModifierSurface.Children.Add( ( UIElement ) tooltipLabel );
                }
            }
        }

        private void MergeTooltipLabelFor( FrameworkElement rolloverMarker )
        {
            FrameworkElement tooltipLabel = (FrameworkElement) rolloverMarker.GetValue(VerticalSliceModifierBase.RolloverLabelProperty);
            if ( tooltipLabel == null )
                return;
            double left = Canvas.GetLeft((UIElement) rolloverMarker);
            double top = Canvas.GetTop((UIElement) rolloverMarker);
            Rect rolloverMarkerRect = this.GetRolloverMarkerRect(rolloverMarker);
            Rect boundaryRect = new Rect(0.0, 0.0, this.ModifierSurface.ActualWidth, this.ModifierSurface.ActualHeight);
            this._tooltipLabels.Add( this.GetMergedTooltip( new Point( left, top ), tooltipLabel, rolloverMarkerRect, boundaryRect ) );
        }

        private FrameworkElement GetMergedTooltip( Point point, FrameworkElement tooltipLabel, Rect markerRect, Rect boundaryRect )
        {
            FrameworkElement tooltip = tooltipLabel;
            Rect mergedTooltipRect = this.GetTooltipLabelRect(tooltip, point, markerRect, boundaryRect);
            FrameworkElement tooltip1 = this._tooltipLabels.FirstOrDefault<FrameworkElement>((Func<FrameworkElement, bool>) (label => new Rect(Canvas.GetLeft((UIElement) label), Canvas.GetTop((UIElement) label), label.ActualWidth, label.ActualHeight).IntersectsWith(mergedTooltipRect)));
            if ( tooltip1 != null )
            {
                FrameworkElement tooltipLabel1 = (FrameworkElement) this.MergeTwoTooltips(tooltip1, tooltipLabel);
                tooltip = this.GetMergedTooltip( point, tooltipLabel1, markerRect, boundaryRect );
            }
            else
            {
                Canvas.SetLeft( ( UIElement ) tooltip, mergedTooltipRect.X );
                Canvas.SetTop( ( UIElement ) tooltip, mergedTooltipRect.Y );
            }
            return tooltip;
        }

        private Rect GetTooltipLabelRect( FrameworkElement tooltip, Point point, Rect markerRect, Rect boundaryRect )
        {
            tooltip.MeasureArrange();
            double num1;
            double num2;
            if ( this.ShowTooltipOn == ShowTooltipOptions.MouseOver )
            {
                num1 = 3.0;
                num2 = 0.0;
            }
            else
            {
                num1 = 0.0;
                num2 = markerRect.Height / 2.0 + tooltip.ActualHeight / 2.0;
            }
            double x = markerRect.Right + num1;
            Panel panel = tooltip as Panel;
            double y = panel == null || panel.Children.Count <= 1 ? markerRect.Bottom - num2 + num1 : point.Y - tooltip.ActualHeight / 2.0;
            Rect rect = new Rect(x, y, tooltip.ActualWidth, tooltip.ActualHeight);
            if ( boundaryRect.Right < rect.Right )
                x = markerRect.Left - tooltip.ActualWidth - num1;
            if ( boundaryRect.Bottom < rect.Bottom )
            {
                double num3 = rect.Bottom - boundaryRect.Bottom;
                y = y - num3 - num1;
            }
            if ( boundaryRect.Top > rect.Top )
            {
                double num3 = boundaryRect.Top - rect.Top;
                y = y + num3 + num1;
            }
            rect.X = x;
            rect.Y = y;
            return rect;
        }

        private StackPanel MergeTwoTooltips( FrameworkElement tooltip1, FrameworkElement tooltip2 )
        {
            StackPanel panel1 = tooltip1 as StackPanel;
            StackPanel panel2 = tooltip2 as StackPanel;
            StackPanel panel3;
            if ( panel1 != null )
            {
                panel1.SafeAddChild( ( object ) tooltip2, -1 );
                panel3 = panel1;
            }
            else if ( panel2 != null )
            {
                panel2.SafeAddChild( ( object ) tooltip1, 0 );
                panel3 = panel2;
            }
            else
            {
                panel3 = new StackPanel();
                panel3.SafeAddChild( ( object ) tooltip1, -1 );
                panel3.SafeAddChild( ( object ) tooltip2, -1 );
            }
            this._tooltipLabels.Remove( tooltip1 );
            this._tooltipLabels.Remove( tooltip2 );
            return panel3;
        }

        protected override void ClearAll()
        {
            this.RemoveMarkers( false );
            this.ClearTooltipLabels();
            this.ClearAxesOverlay();
            this._delayActionHelper.Stop();
        }

        protected override void OnTooltipLabelTemplateChanged()
        {
            if ( this.ParentSurface == null || this.ParentSurface.RenderableSeries == null )
                return;
            this.ParentSurface.RenderableSeries.Where<IRenderableSeries>( ( Func<IRenderableSeries, bool> ) ( series =>
            {
                if ( series.RolloverMarker != null )
                    return series.RolloverMarker.DataContext != null;
                return false;
            } ) ).ForEachDo<IRenderableSeries>( ( Action<IRenderableSeries> ) ( series => series.RolloverMarker.SetValue( VerticalSliceModifierBase.RolloverLabelProperty, ( object ) this.CreateFromTemplate( this.TooltipLabelTemplate, this.TooltipLabelTemplateSelector, series.RolloverMarker.DataContext ) ) ) );
        }

        protected override void OnAxisLabelTemplateChanged()
        {
            this.RecreateLabels();
        }

        protected override void OnSelectedSeriesChanged( IEnumerable<IRenderableSeries> oldSeries, IEnumerable<IRenderableSeries> newSeries )
        {
            base.OnSelectedSeriesChanged( oldSeries, newSeries );
            this.RemoveMarkers( true );
        }
    }
}
