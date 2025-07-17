// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.CursorModifier
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Common.Helpers;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.Axes;
using StockSharp.Xaml.Charting.Visuals.RenderableSeries;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public class CursorModifier : TooltipModifierBase
    {
        public static readonly DependencyProperty IncludeSeriesProperty = DependencyProperty.RegisterAttached("IncludeSeries", typeof (bool), typeof (CursorModifier), new PropertyMetadata((object) true));
        public static readonly DependencyProperty ShowTooltipProperty = DependencyProperty.Register(nameof (ShowTooltip), typeof (bool), typeof (CursorModifier), new PropertyMetadata((object) false, (PropertyChangedCallback) null));
        private ObservableCollection<StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo> _axisInfo = new ObservableCollection<StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo>();
        private System.Windows.Shapes.Line _lineX;
        private System.Windows.Shapes.Line _lineY;
        private Ellipse _cursorPoint;
        private TemplatableControl _cursorLabelCache;
        private StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo _xAxisInfo;
        private StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo _yAxisInfo;
        private const double CursorXyOffset = 6.0;

        public static bool GetIncludeSeries( DependencyObject obj )
        {
            return ( bool ) obj.GetValue( CursorModifier.IncludeSeriesProperty );
        }

        public static void SetIncludeSeries( DependencyObject obj, bool value )
        {
            obj.SetValue( CursorModifier.IncludeSeriesProperty, ( object ) value );
        }

        public ObservableCollection<StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo> AxisInfo
        {
            get
            {
                return this._axisInfo;
            }
            set
            {
                this._axisInfo = value;
                this.OnPropertyChanged( nameof( AxisInfo ) );
            }
        }

        public StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo XAxisInfo
        {
            get
            {
                return this._xAxisInfo;
            }
            set
            {
                this._xAxisInfo = value;
                this.OnPropertyChanged( nameof( XAxisInfo ) );
            }
        }

        public StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo YAxisInfo
        {
            get
            {
                return this._yAxisInfo;
            }
            set
            {
                this._yAxisInfo = value;
                this.OnPropertyChanged( nameof( YAxisInfo ) );
            }
        }

        public bool ShowTooltip
        {
            get
            {
                return ( bool ) this.GetValue( CursorModifier.ShowTooltipProperty );
            }
            set
            {
                this.SetValue( CursorModifier.ShowTooltipProperty, ( object ) value );
            }
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

        public CursorModifier()
        {
            this.DefaultStyleKey = ( object ) typeof( CursorModifier );
            this.SetCurrentValue( InspectSeriesModifierBase.SeriesDataProperty, ( object ) new ChartDataObject() );
            this._delayActionHelper = new DelayActionHelper()
            {
                Interval = this.HoverDelay
            };
        }

        public override void OnAttached()
        {
            base.OnAttached();
            this.CurrentPoint = new Point( double.NaN, double.NaN );
            this.ClearAll();
        }

        public override void OnDetached()
        {
            base.OnDetached();
            this.ClearAll();
        }

        protected override void OnParentSurfaceMouseEnter()
        {
            this.ClearAll();
        }

        protected override void HandleMasterMouseEvent( Point mousePoint )
        {
            this.ShowCrosshairCursor( mousePoint );
            if ( this.ShowTooltip || this.ShowAxisLabels )
            {
                this.GetSeriesData( mousePoint );
                this.GetAxesData( mousePoint );
            }
            if ( this.ShowTooltip )
            {
                if ( this.SeriesData.SeriesInfo.IsNullOrEmpty<SeriesInfo>() || !this.HasToShowTooltip() )
                    this.ClearCursorOverlay();
                else if ( this.ShowTooltipOn == ShowTooltipOptions.MouseHover )
                {
                    this.ClearCursorOverlay();
                    this._delayActionHelper.Start( ( Action ) ( () => this.PlaceTooltip( mousePoint ) ) );
                }
                else
                    this.PlaceTooltip( mousePoint );
            }
            if ( !this.ShowAxisLabels )
                return;
            this.UpdateAxesLabels( mousePoint );
        }

        private void PlaceTooltip( Point mousePoint )
        {
            SeriesInfo seriesInfo = this.SeriesData.SeriesInfo != null ? this.SeriesData.SeriesInfo.FirstOrDefault<SeriesInfo>() : (SeriesInfo) null;
            Point point = seriesInfo != null ? seriesInfo.XyCoordinate : mousePoint;
            if ( this.IsEnabledAt( point ) )
                this.UpdateCursorOverlay( point );
            else
                this.ClearCursorOverlay();
        }

        private void UpdateAxesLabels( Point mousePoint )
        {
            if ( !this.IsLabelsCacheActual() )
                this.RecreateLabels();
            this.UpdateAxesOverlay( mousePoint );
        }

        private void GetSeriesData( Point currentPoint )
        {
            this.SeriesData.UpdateSeriesInfo( this.GetSeriesInfoAt( currentPoint ) );
        }

        protected override IEnumerable<SeriesInfo> GetSeriesInfoAt( Point point )
        {
            return this.GetSeriesInfoAt( ( Func<IRenderableSeries, HitTestInfo> ) ( renderSeries => renderSeries.VerticalSliceHitTest( point, this.UseInterpolation ) ) );
        }

        protected virtual void GetAxesData( Point mousePoint )
        {
            IEnumerable<StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo> axisInfos1 = this.YAxes.Select<IAxis, StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo>((Func<IAxis, StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo>) (a => this.HitTestAxis(a, mousePoint)));
            this.YAxisInfo = axisInfos1.FirstOrDefault<StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo>();
            IEnumerable<StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo> axisInfos2 = this.XAxes.Select<IAxis, StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo>((Func<IAxis, StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo>) (a => this.HitTestAxis(a, mousePoint)));
            this.XAxisInfo = axisInfos2.FirstOrDefault<StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo>();
            ObservableCollection<StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo> collection = new ObservableCollection<StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo>();
            collection.AddRange<StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo>( axisInfos2 );
            collection.AddRange<StockSharp.Xaml.Charting.Visuals.RenderableSeries.AxisInfo>( axisInfos1 );
            this.AxisInfo = collection;
        }

        private void ShowCrosshairCursor( Point mousePoint )
        {
            Rect boundsRelativeTo = this.ModifierSurface.GetBoundsRelativeTo((IHitTestable) this.RootGrid);
            if ( this._lineX == null || this._lineY == null )
            {
                System.Windows.Shapes.Line line1 = new System.Windows.Shapes.Line();
                line1.Style = this.LineOverlayStyle;
                line1.IsHitTestVisible = false;
                this._lineX = line1;
                System.Windows.Shapes.Line line2 = new System.Windows.Shapes.Line();
                line2.Style = this.LineOverlayStyle;
                line2.IsHitTestVisible = false;
                this._lineY = line2;
                if ( this._cursorPoint == null )
                {
                    Ellipse ellipse = new Ellipse();
                    ellipse.Fill = this._lineY.Stroke;
                    ellipse.Width = 5.0;
                    ellipse.Height = 5.0;
                    this._cursorPoint = ellipse;
                }
                this.ModifierSurface.Children.Add( ( UIElement ) this._lineX );
                this.ModifierSurface.Children.Add( ( UIElement ) this._lineY );
            }
            this._lineX.X1 = 0.0;
            this._lineX.X2 = boundsRelativeTo.Width - 1.0;
            this._lineX.Y1 = mousePoint.Y;
            this._lineX.Y2 = mousePoint.Y;
            this._lineY.X1 = mousePoint.X;
            this._lineY.X2 = mousePoint.X;
            this._lineY.Y1 = 0.0;
            this._lineY.Y2 = boundsRelativeTo.Height - 1.0;
        }

        protected override void HandleSlaveMouseEvent( Point mousePoint )
        {
            UltrachartDebugLogger.Instance.WriteLine( "CursorModifier Slave MouseMove: {0}, {1}", ( object ) mousePoint.X, ( object ) mousePoint.Y );
            Rect boundsRelativeTo = this.ModifierSurface.GetBoundsRelativeTo((IHitTestable) this.RootGrid);
            if ( this._lineY == null )
            {
                System.Windows.Shapes.Line line = new System.Windows.Shapes.Line();
                line.Style = this.LineOverlayStyle;
                line.IsHitTestVisible = false;
                this._lineY = line;
                this.ModifierSurface.Children.Add( ( UIElement ) this._lineY );
            }
            this._lineY.X1 = mousePoint.X;
            this._lineY.X2 = mousePoint.X;
            this._lineY.Y1 = 0.0;
            this._lineY.Y2 = boundsRelativeTo.Height - 1.0;
        }

        protected override void ClearAll()
        {
            if ( this.ModifierSurface != null )
            {
                if ( this._lineX != null && this.ModifierSurface.Children.Contains( ( UIElement ) this._lineX ) )
                {
                    this.ModifierSurface.Children.Remove( ( UIElement ) this._lineX );
                    this._lineX = ( System.Windows.Shapes.Line ) null;
                }
                if ( this._lineY != null && this.ModifierSurface.Children.Contains( ( UIElement ) this._lineY ) )
                {
                    this.ModifierSurface.Children.Remove( ( UIElement ) this._lineY );
                    this._lineY = ( System.Windows.Shapes.Line ) null;
                }
            }
            this.ClearOverlay();
            this.CurrentPoint = new Point( double.NaN, double.NaN );
            this._delayActionHelper.Stop();
        }

        private void ClearOverlay()
        {
            this.ClearCursorOverlay();
            this.ClearAxesOverlay();
        }

        private void ClearCursorOverlay()
        {
            if ( this._cursorLabelCache == null || this.ModifierSurface == null )
                return;
            if ( this.ModifierSurface.Children.Contains( ( UIElement ) this._cursorLabelCache ) )
                this.ModifierSurface.Children.Remove( ( UIElement ) this._cursorLabelCache );
            if ( !this.ModifierSurface.Children.Contains( ( UIElement ) this._cursorPoint ) )
                return;
            this.ModifierSurface.Children.Remove( ( UIElement ) this._cursorPoint );
        }

        private void UpdateCursorOverlay( Point mousePoint )
        {
            if ( this._cursorLabelCache == null || !this.ShowTooltip )
                return;
            this.PlaceOverlay( ( FrameworkElement ) this._cursorLabelCache, mousePoint );
            Canvas.SetLeft( ( UIElement ) this._cursorPoint, mousePoint.X - this._cursorPoint.ActualWidth * 0.5 );
            Canvas.SetTop( ( UIElement ) this._cursorPoint, mousePoint.Y - this._cursorPoint.ActualHeight * 0.5 );
            if ( !this.ModifierSurface.Children.Contains( ( UIElement ) this._cursorLabelCache ) )
                this.ModifierSurface.Children.Add( ( UIElement ) this._cursorLabelCache );
            if ( this.ModifierSurface.Children.Contains( ( UIElement ) this._cursorPoint ) )
                return;
            this.ModifierSurface.Children.Add( ( UIElement ) this._cursorPoint );
        }

        private void PlaceOverlay( FrameworkElement overlay, Point mousePoint )
        {
            Rect rect1 = new Rect(0.0, 0.0, this.ModifierSurface.ActualWidth, this.ModifierSurface.ActualHeight);
            double num1 = mousePoint.X + 6.0;
            double y = mousePoint.Y + 6.0;
            Rect rect2 = new Rect(num1, y, overlay.ActualWidth, overlay.ActualHeight);
            if ( rect1.Right < rect2.Right )
                num1 = mousePoint.X - rect2.Width - 6.0;
            if ( rect1.Bottom < rect2.Bottom )
            {
                double num2 = rect2.Bottom - rect1.Bottom;
                double num3 = y - num2;
                y = num3 < 0.0 ? 0.0 : num3;
            }
            Canvas.SetLeft( ( UIElement ) overlay, num1 );
            Canvas.SetTop( ( UIElement ) overlay, y < 0.0 ? 0.0 : y );
        }

        protected override void OnTooltipLabelTemplateChanged()
        {
            this._cursorLabelCache = this.CreateFromTemplate( this.TooltipLabelTemplate, this.TooltipLabelTemplateSelector, ( object ) this );
        }

        protected override void OnAxisLabelTemplateChanged()
        {
            this.RecreateLabels();
        }
    }
}
