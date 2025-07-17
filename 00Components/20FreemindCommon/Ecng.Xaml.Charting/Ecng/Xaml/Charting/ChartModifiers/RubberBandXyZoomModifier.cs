// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.RubberBandXyZoomModifier
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
namespace fx.Xaml.Charting
{
    public class RubberBandXyZoomModifier : ChartModifierBase
    {
        public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register(nameof (IsAnimated), typeof (bool), typeof (RubberBandXyZoomModifier), new PropertyMetadata((object) true));
        public static readonly DependencyProperty RubberBandFillProperty = DependencyProperty.Register(nameof (RubberBandFill), typeof (Brush), typeof (RubberBandXyZoomModifier), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty RubberBandStrokeProperty = DependencyProperty.Register(nameof (RubberBandStroke), typeof (Brush), typeof (RubberBandXyZoomModifier), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty RubberBandStrokeDashArrayProperty = DependencyProperty.Register(nameof (RubberBandStrokeDashArray), typeof (DoubleCollection), typeof (RubberBandXyZoomModifier), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty IsXAxisOnlyProperty = DependencyProperty.Register(nameof (IsXAxisOnly), typeof (bool), typeof (RubberBandXyZoomModifier), new PropertyMetadata((object) false));
        public static readonly DependencyProperty ZoomExtentsYProperty = DependencyProperty.Register(nameof (ZoomExtentsY), typeof (bool), typeof (RubberBandXyZoomModifier), new PropertyMetadata((object) true));
        public static readonly DependencyProperty MinDragSensitivityProperty = DependencyProperty.Register(nameof (MinDragSensitivity), typeof (double), typeof (RubberBandXyZoomModifier), new PropertyMetadata((object) 10.0));
        private IRubberBandOverlayPlacementStrategy _overlayPlacementStrategy;
        private Point _startPoint;
        private Point _endPoint;
        private bool _isDragging;
        private Shape _shape;

        public RubberBandXyZoomModifier()
        {
            DefaultStyleKey = ( object ) typeof( RubberBandXyZoomModifier );
        }

        public bool IsAnimated
        {
            get
            {
                return ( bool ) GetValue( RubberBandXyZoomModifier.IsAnimatedProperty );
            }
            set
            {
                SetValue( RubberBandXyZoomModifier.IsAnimatedProperty, ( object ) value );
            }
        }

        public Brush RubberBandFill
        {
            get
            {
                return ( Brush ) GetValue( RubberBandXyZoomModifier.RubberBandFillProperty );
            }
            set
            {
                SetValue( RubberBandXyZoomModifier.RubberBandFillProperty, ( object ) value );
            }
        }

        public Brush RubberBandStroke
        {
            get
            {
                return ( Brush ) GetValue( RubberBandXyZoomModifier.RubberBandStrokeProperty );
            }
            set
            {
                SetValue( RubberBandXyZoomModifier.RubberBandStrokeProperty, ( object ) value );
            }
        }

        public DoubleCollection RubberBandStrokeDashArray
        {
            get
            {
                return ( DoubleCollection ) GetValue( RubberBandXyZoomModifier.RubberBandStrokeDashArrayProperty );
            }
            set
            {
                SetValue( RubberBandXyZoomModifier.RubberBandStrokeDashArrayProperty, ( object ) value );
            }
        }

        public bool IsXAxisOnly
        {
            get
            {
                return ( bool ) GetValue( RubberBandXyZoomModifier.IsXAxisOnlyProperty );
            }
            set
            {
                SetValue( RubberBandXyZoomModifier.IsXAxisOnlyProperty, ( object ) value );
            }
        }

        public bool ZoomExtentsY
        {
            get
            {
                return ( bool ) GetValue( RubberBandXyZoomModifier.ZoomExtentsYProperty );
            }
            set
            {
                SetValue( RubberBandXyZoomModifier.ZoomExtentsYProperty, ( object ) value );
            }
        }

        public double MinDragSensitivity
        {
            get
            {
                return ( double ) GetValue( RubberBandXyZoomModifier.MinDragSensitivityProperty );
            }
            set
            {
                SetValue( RubberBandXyZoomModifier.MinDragSensitivityProperty, ( object ) value );
            }
        }

        public bool IsDragging
        {
            get
            {
                return _isDragging;
            }
        }

        public override void OnAttached()
        {
            base.OnAttached();
            ClearReticule();
        }

        public override void OnDetached()
        {
            base.OnDetached();
            ClearReticule();
        }

        public override void OnModifierMouseDown( ModifierMouseArgs e )
        {
            base.OnModifierMouseDown( e );
            if ( _isDragging || !MatchesExecuteOn( e.MouseButtons, ExecuteOn ) )
            {
                return;
            }

            e.Handled = true;
            if ( !( e.Source is IChartModifier ) || !ModifierSurface.GetBoundsRelativeTo( ( IHitTestable ) RootGrid ).Contains( e.MousePoint ) )
            {
                return;
            }

            if ( e.IsMaster )
            {
                ModifierSurface.CaptureMouse();
            }

            _startPoint = GetPointRelativeTo( e.MousePoint, ( IHitTestable ) ModifierSurface );
            _overlayPlacementStrategy = GetOverlayPlacementStrategy();
            _shape = _overlayPlacementStrategy.CreateShape( RubberBandFill, RubberBandStroke, RubberBandStrokeDashArray );
            _overlayPlacementStrategy.SetupShape( IsXAxisOnly, _startPoint, _startPoint );
            ModifierSurface.Children.Add( ( UIElement ) _shape );
            _isDragging = true;
        }

        private IRubberBandOverlayPlacementStrategy GetOverlayPlacementStrategy()
        {
            return XAxis == null || !XAxis.IsPolarAxis ? ( IRubberBandOverlayPlacementStrategy ) ( _overlayPlacementStrategy as CartesianRubberBandOverlayPlacementStrategy ?? new CartesianRubberBandOverlayPlacementStrategy( ( IChartModifier ) this ) ) : ( IRubberBandOverlayPlacementStrategy ) ( _overlayPlacementStrategy as PolarRubberBandOverlayPlacementStrategy ?? new PolarRubberBandOverlayPlacementStrategy( ( IChartModifier ) this ) );
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
            if ( !_isDragging )
            {
                return;
            }

            base.OnModifierMouseMove( e );
            e.Handled = true;
            UltrachartDebugLogger.Instance.WriteLine( "{0} MouseMove: x={1}, y={2}", ( object ) GetType().Name, ( object ) e.MousePoint.X, ( object ) e.MousePoint.Y );
            _overlayPlacementStrategy.UpdateShape( IsXAxisOnly, _startPoint, GetPointRelativeTo( e.MousePoint, ( IHitTestable ) ModifierSurface ) );
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            if ( !_isDragging )
            {
                return;
            }

            base.OnModifierMouseUp( e );
            UltrachartDebugLogger.Instance.WriteLine( "{0} MouseUp: x={1}, y={2}", ( object ) GetType().Name, ( object ) e.MousePoint.X, ( object ) e.MousePoint.Y );
            Point pointRelativeTo = GetPointRelativeTo(e.MousePoint, (IHitTestable) ModifierSurface);
            _endPoint = _overlayPlacementStrategy.UpdateShape( IsXAxisOnly, _startPoint, pointRelativeTo );
            ITransformationStrategy transformationStrategy = Services.GetService<IStrategyManager>().GetTransformationStrategy();
            Point point = transformationStrategy.Transform(_startPoint);
            Point endPoint = transformationStrategy.Transform(_endPoint);
            Point end = transformationStrategy.Transform(pointRelativeTo);
            if ( _overlayPlacementStrategy.CalculateDraggedDistance( point, end ) > MinDragSensitivity )
            {
                PerformZoom( point, endPoint );
                e.Handled = true;
            }
            else
            {
                ClearReticule();
            }

            _isDragging = false;
            if ( !e.IsMaster )
            {
                return;
            }

            ModifierSurface.ReleaseMouseCapture();
        }

        private void ClearReticule()
        {
            if ( ModifierSurface == null || _shape == null )
            {
                return;
            }

            ModifierSurface.Children.Remove( ( UIElement ) _shape );
            _shape = ( Shape ) null;
            _isDragging = false;
        }

        internal void PerformZoom( Point startPoint, Point endPoint )
        {
            ClearReticule();
            if ( Math.Abs( startPoint.X - endPoint.X ) < double.Epsilon || Math.Abs( startPoint.Y - endPoint.Y ) < double.Epsilon || ( XAxes.IsNullOrEmpty<IAxis>() || YAxes.IsNullOrEmpty<IAxis>() ) )
            {
                return;
            }

            Rect zoomRect = new Rect(startPoint, endPoint);
            using ( ParentSurface.SuspendUpdates() )
            {
                Dictionary<string, IRange> dictionary = new Dictionary<string, IRange>();
                foreach ( IAxis xax in XAxes )
                {
                    int num1 = xax.IsHorizontalAxis ? 1 : 0;
                    bool? isHorizontalAxis = XAxis?.IsHorizontalAxis;
                    int num2 = isHorizontalAxis.GetValueOrDefault() ? 1 : 0;
                    if ( num1 == num2 & isHorizontalAxis.HasValue )
                    {
                        IRange range = PerformZoomOnAxis(xax, zoomRect);
                        if ( range != null && !range.IsZero )
                        {
                            dictionary.Add( xax.Id, range );
                        }
                    }
                }
                if ( IsXAxisOnly )
                {
                    if ( !ZoomExtentsY )
                    {
                        return;
                    }

                    foreach ( IAxis yax in YAxes )
                    {
                        yax.TrySetOrAnimateVisibleRange( yax.GetWindowedYRange( ( IDictionary<string, IRange> ) dictionary ), IsAnimated ? TimeSpan.FromMilliseconds( 500.0 ) : TimeSpan.Zero );
                    }
                }
                else
                {
                    foreach ( IAxis yax in YAxes )
                    {
                        PerformZoomOnAxis( yax, zoomRect );
                    }
                }
            }
        }

        private IRange PerformZoomOnAxis( IAxis axis, Rect zoomRect )
        {
            double fromCoord = axis.IsHorizontalAxis ? zoomRect.Left : zoomRect.Bottom;
            double toCoord = axis.IsHorizontalAxis ? zoomRect.Right : zoomRect.Top;
            return PerformZoomOnAxis( axis, fromCoord, toCoord );
        }

        internal IRange PerformZoomOnAxis( IAxis axis, double fromCoord, double toCoord )
        {
            if ( axis == null )
            {
                return ( IRange ) null;
            }

            IAxisInteractivityHelper interactivityHelper = axis.GetCurrentInteractivityHelper();
            if ( interactivityHelper == null )
            {
                return ( IRange ) null;
            }

            IRange newRange = interactivityHelper.Zoom(axis.VisibleRange, fromCoord, toCoord - 1.0);
            axis.TrySetOrAnimateVisibleRange( newRange, IsAnimated ? TimeSpan.FromMilliseconds( 500.0 ) : TimeSpan.Zero );
            return newRange;
        }

        private void DebugZoom( double xMax, double xMin, Rect zoomRect )
        {
        }

        internal IRubberBandOverlayPlacementStrategy CurrentStrategy
        {
            get
            {
                return _overlayPlacementStrategy;
            }
        }
    }
}
