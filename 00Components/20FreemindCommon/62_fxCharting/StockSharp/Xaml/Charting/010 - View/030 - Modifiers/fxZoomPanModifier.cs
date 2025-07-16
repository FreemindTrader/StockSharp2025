using StockSharp.Charting;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Axes;
using SciChart.Core.Framework;
using SciChart.Core.Utility.Mouse;
using SciChart.Data.Model;
using System;
using System.Collections.Generic;
using fx.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace StockSharp.Xaml.Charting
{
    public class fxZoomPanModifier : ZoomPanModifierBase
    {
        private readonly uint _pixelPerCm = 38;
        const double MIN_SCALE_TRIGGER = 0.05;
        const int MIN_ROTATIONANGLE_TRIGGER_DEGREE = 10;
        const int MIN_FINGER_DISTANCE_FOR_ROTATION_CM = 2;

        public static readonly DependencyProperty IncludeAxisProperty = DependencyProperty.RegisterAttached( "IncludeAxis", typeof( bool ), typeof( fxZoomPanModifier ), new PropertyMetadata( true ) );
        private PooledDictionary<string, IRange> _categoryAxisRange;
        private IRange _yRange;
        private IRange _xRange;

        private AutoRange _backup;

        double _prevdistanceInCm;

        double _touchScale = 0.0d;
        //double _touchRotation = 0.0d;

        private Point? _startPoint;
        private Point? _lastPoint;

        int zoomCount = 0;

        public static void SetIncludeAxis( DependencyObject element, bool value )
        {
            element.SetValue( ZoomPanModifier.IncludeAxisProperty, value );
        }

        public static bool GetIncludeAxis( DependencyObject element )
        {
            return ( bool ) element.GetValue( ZoomPanModifier.IncludeAxisProperty );
        }

        private static bool Includes( IAxis axis )
        {
            if ( !( axis is AxisBase axisBase ) )
            {
                return true;
            }
            return ZoomPanModifier.GetIncludeAxis( axisBase );
        }

        //public override void OnModifierTouchUp( ModifierTouchArgs e )
        //{
        //    base.OnModifierTouchUp( e );
        //    _prevdistanceInCm = 0;
        //}

        //public override void OnModifierTouchDown( ModifierTouchArgs e )
        //{


        //    base.OnModifierTouchDown( e );
        //}

        public override void OnModifierMouseDown( ModifierMouseArgs e )
        {
            base.OnModifierMouseDown( e );
            IAxis yaxis = YAxis;
            IAxis xaxis = XAxis;

            _startPoint = e.MousePoint;

            if ( yaxis?.VisibleRange != null )
            {
                _yRange = ( IRange ) yaxis.VisibleRange.Clone();
            }

            if ( xaxis?.VisibleRange != null )
            {
                _xRange = ( IRange ) xaxis.VisibleRange.Clone();
            }

            _categoryAxisRange = XAxes.Where( ax => ax.IsCategoryAxis ).ToPooledDictionary( ax => ax.Id, ax => ax.VisibleRange );
        }

        public override void OnModifierKeyDown( ModifierKeyArgs e )
        {
            if ( e.Key == Key.Escape && IsDragging )
            {
                IAxis yaxis = YAxis;
                IAxis xaxis = XAxis;

                if ( yaxis?.VisibleRange != null )
                {
                    yaxis.AnimateVisibleRangeTo( _yRange, TimeSpan.Zero, null );
                    yaxis.AutoRange = _backup;
                }

                if ( xaxis?.VisibleRange != null )
                {
                    xaxis.AnimateVisibleRangeTo( _xRange, TimeSpan.Zero, null );
                }

                IsDragging = false;
                SetCursor( null );
            }
            else if ( e.Key == Key.LeftCtrl && IsDragging )
            {
                IAxis yaxis = YAxis;

                _backup = yaxis.AutoRange;

                if ( _backup == AutoRange.Once )
                {
                    _backup = AutoRange.Always;
                }

                yaxis.AutoRange = AutoRange.Never;
            }
            base.OnModifierKeyDown( e );
        }

        public override void OnModifierKeyUp( ModifierKeyArgs e )
        {
            if ( e.Key == Key.LeftCtrl && IsDragging )
            {
                IAxis yaxis = YAxis;

                yaxis.AutoRange = _backup;
            }
            base.OnModifierKeyUp( e );
        }

        public override void OnModifierTouchManipulationStarted( ModifierManipulationStartedArgs e )
        {
            base.OnModifierTouchManipulationStarted( e );
            IAxis yaxis = YAxis;
            IAxis xaxis = XAxis;

            _startPoint = e.ManipulationOrigin;
            _lastPoint = _startPoint;
            _prevdistanceInCm = 0;

            if ( yaxis?.VisibleRange != null )
            {
                _yRange = ( IRange ) yaxis.VisibleRange.Clone();
            }

            if ( xaxis?.VisibleRange != null )
            {
                _xRange = ( IRange ) xaxis.VisibleRange.Clone();
            }

            

            _backup = yaxis.AutoRange;

            if ( _backup == AutoRange.Once )
            {
                _backup = AutoRange.Always;
            }

            yaxis.AutoRange = AutoRange.Never;


            zoomCount = 0;

            _categoryAxisRange = XAxes.Where( ax => ax.IsCategoryAxis ).ToPooledDictionary( ax => ax.Id, ax => ax.VisibleRange );
        }

        public override void OnModifierTouchManipulationDelta( ModifierManipulationDeltaArgs e )
        {
            //
            //System.Diagnostics.Debug.WriteLine( "Delta" );

            var fingerCount = e.Manipulators.Count( );

            if ( fingerCount >= 2 )
            {
                var manipulatorBounds = Rect.Empty;

                foreach ( var manipulator in e.Manipulators )
                {
                    manipulatorBounds.Union( manipulator.GetPosition( ParentSurface.ModifierSurface as IInputElement ) );
                }


                var distance = (manipulatorBounds.TopLeft - manipulatorBounds.BottomRight).Length;
                var distanceInCm = distance /_pixelPerCm;

                if ( _prevdistanceInCm == 0 )
                {
                    _prevdistanceInCm = distanceInCm;
                }
                else
                {
                    if ( distanceInCm > _prevdistanceInCm )
                    {
                        //expanding
                        _touchScale = distanceInCm / _prevdistanceInCm;

                        Zoom();

                        zoomCount++;

                        if ( zoomCount > 8 )
                        {
                            e.Complete();

                        }  
                    }
                    else
                    {
                        //Shrinking
                        _touchScale = distanceInCm / _prevdistanceInCm;

                        Zoom();

                        zoomCount--;

                        if ( zoomCount < -8 )
                        {
                            e.Complete();
                            
                        }    
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine( "One Finger" );
                //base.OnModifierTouchManipulationDelta( e );

                IAxis yaxis = YAxis;
                IAxis xaxis = XAxis;

                if ( yaxis?.VisibleRange != null )
                {
                    yaxis.AnimateVisibleRangeTo( _yRange, TimeSpan.Zero, null );
                }

                if ( xaxis?.VisibleRange != null )
                {
                    xaxis.AnimateVisibleRangeTo( _xRange, TimeSpan.Zero, null );
                }

                IsDragging = false;
                SetCursor( null );

                var curPt = e.Manipulators.First().GetPosition( ParentSurface.ModifierSurface as IInputElement );

                _lastPoint = curPt;

                e.Cancel();
            }
        }

        public override void OnModifierTouchManipulationCompleted( ModifierManipulationCompletedArgs e )
        {
            base.OnModifierTouchManipulationCompleted( e );

            _touchScale = 0;
            //_touchRotation = 0;
            _prevdistanceInCm = 0;
        }

        public override void Pan( Point currentPoint, Point lastPoint, Point startPoint )
        {
            FreemindCandlestickRenderableSeries series = null;

            foreach ( var iRenderableSeries in ParentSurface.RenderableSeries )
            {
                if ( iRenderableSeries is FreemindCandlestickRenderableSeries )
                {
                    series = ( FreemindCandlestickRenderableSeries ) iRenderableSeries;
                }
            }

            if( series == null )
                return;

            series.CanUpdate = false;


            double xDelta = currentPoint.X - lastPoint.X;
            double yDelta = lastPoint.Y - currentPoint.Y;

            using ( ParentSurface.SuspendUpdates() )
            {
                if ( XyDirection != XyDirection.YDirection )
                {
                    foreach ( IAxis xax in XAxes )
                    {
                        if ( Includes( xax ) )
                        {
                            if ( xax.IsHorizontalAxis == XAxis.IsHorizontalAxis )
                            {
                                using ( IUpdateSuspender updateSuspender = xax.SuspendUpdates() )
                                {
                                    updateSuspender.ResumeTargetOnDispose = false;

                                    if ( xax.IsCategoryAxis )
                                    {
                                        double deltaX = currentPoint.X - startPoint.X;
                                        double deltaY = startPoint.Y - currentPoint.Y;
                                        xax.Scroll( _categoryAxisRange[ xax.Id ], xax.IsHorizontalAxis ? deltaX : -deltaY, ClipModeX );
                                    }
                                    else
                                    {
                                        xax.Scroll( xax.IsHorizontalAxis ? xDelta : -yDelta, ClipModeX );
                                    }

                                    if ( _touchScale != 0 )
                                    {

                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                if ( XyDirection == XyDirection.XDirection )
                {
                    if ( !ZoomExtentsY )
                    {
                        return;
                    }
                    ParentSurface.ZoomExtentsY();
                }
                else
                {
                    foreach ( IAxis yax in YAxes )
                    {
                        if ( Includes( yax ) )
                        {
                            yax.Scroll( yax.IsHorizontalAxis ? -xDelta : yDelta, ClipMode.None );
                        }
                    }
                }
            }
        }

        public void Zoom()
        {
            var series = ( FreemindCandlestickRenderableSeries ) ParentSurface.RenderableSeries.First( );

            series.CanUpdate = false;

            using ( ParentSurface.SuspendUpdates() )
            {
                if ( XyDirection != XyDirection.YDirection )
                {
                    foreach ( IAxis xax in XAxes )
                    {
                        if ( Includes( xax ) )
                        {
                            if ( xax.IsHorizontalAxis == XAxis.IsHorizontalAxis )
                            {
                                using ( IUpdateSuspender updateSuspender = xax.SuspendUpdates() )
                                {
                                    updateSuspender.ResumeTargetOnDispose = false;

                                    if ( _touchScale > 1 )
                                    {
                                        xax.ZoomBy( -0.1, -0.1 );
                                    }
                                    else
                                    {
                                        xax.ZoomBy( 0.1, 0.1 );
                                    }

                                    ParentSurface.ZoomExtentsY();

                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    foreach ( IAxis xax in YAxes )
                    {
                        if ( Includes( xax ) )
                        {
                            using ( IUpdateSuspender updateSuspender = xax.SuspendUpdates() )
                            {
                                updateSuspender.ResumeTargetOnDispose = false;

                                if ( _touchScale > 1 )
                                {
                                    xax.ZoomBy( -0.1, -0.1 );
                                }
                                else
                                {
                                    xax.ZoomBy( 0.1, 0.1 );
                                }
                            }
                        }
                    }
                }
                if ( XyDirection == XyDirection.XDirection )
                {
                    if ( !ZoomExtentsY )
                    {
                        return;
                    }
                    ParentSurface.ZoomExtentsY();
                }
                else
                {
                    foreach ( IAxis yax in YAxes )
                    {
                        if ( Includes( yax ) )
                        {
                            //yax.Scroll( yax.IsHorizontalAxis ? -xDelta : yDelta, ClipMode.None );
                        }
                    }
                }
            }
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            if ( ParentSurface.RenderableSeries.Count <= 0 )
                return;

            var firstSeries = ParentSurface.RenderableSeries.First( );

            if ( firstSeries is FreemindCandlestickRenderableSeries )
            {
                var series = ( FreemindCandlestickRenderableSeries )firstSeries;

                series.CanUpdate = true;
            }

            if ( !IsDragging )
            {
                return;
            }
            base.OnModifierMouseUp( e );
            _categoryAxisRange = new PooledDictionary<string, IRange>();
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
            base.OnModifierMouseMove( e );
            _lastPoint = e.MousePoint;

            if ( e.IsMaster || !ZoomExtentsY || ( XyDirection != XyDirection.XDirection || !IsDragging ) )
            {
                return;
            }
            SetZoomState( ZoomStates.UserZooming );
            ParentSurface?.ZoomExtentsY();


        }
    }
}
