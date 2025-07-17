// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.InertialZoomPanModifier
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Windows;
using System.Windows.Threading;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public class InertialZoomPanModifier : ZoomPanModifierBase
    {
        private readonly DispatcherTimer _timer;
        private double _xSpeed;
        private double _ySpeed;
        private double _xCatSpeed;
        private double _yCatSpeed;
        private const double XFriction = 10.0;
        private const double YFriction = 10.0;
        private const double MaxSpeed = 250.0;

        public InertialZoomPanModifier()
        {
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds( 40.0 )
            };
            _timer.Tick += new EventHandler( PerformPan );
        }

        public override void Pan( Point currentPoint, Point lastPoint, Point startPoint )
        {
            AddPanAcceleration( currentPoint, lastPoint, startPoint );
        }

        private void AddPanAcceleration( Point currentPoint, Point lastPoint, Point startPoint )
        {
            _xSpeed += currentPoint.X - lastPoint.X;
            _ySpeed += lastPoint.Y - currentPoint.Y;
            _xCatSpeed += currentPoint.X - startPoint.X;
            _yCatSpeed += startPoint.Y - currentPoint.Y;
            _xSpeed = Math.Max( Math.Min( _xSpeed, 250.0 ), -250.0 );
            _ySpeed = Math.Max( Math.Min( _ySpeed, 250.0 ), -250.0 );
            _xCatSpeed = Math.Max( Math.Min( _xCatSpeed, 250.0 ), -250.0 );
            _yCatSpeed = Math.Max( Math.Min( _yCatSpeed, 250.0 ), -250.0 );

            if ( _timer.IsEnabled )
                return;
            _timer.Start();
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            if ( !IsDragging )
                return;
            base.OnModifierMouseUp( e );
        }

        public override void ResetInertia()
        {
            _timer.Stop();
            _xSpeed = _ySpeed = _xCatSpeed = _yCatSpeed = 0.0;
        }

        private void PerformPan( object sender, EventArgs eventArgs )
        {
            double xSpeed = _xSpeed;
            double ySpeed = _ySpeed;
            double xCatSpeed = _xCatSpeed;
            double yCatSpeed = _yCatSpeed;
            _xSpeed = _xSpeed > 0.0 ? Math.Max( 0.0, _xSpeed - 10.0 ) : Math.Min( 0.0, _xSpeed + 10.0 );
            _ySpeed = _ySpeed > 0.0 ? Math.Max( 0.0, _ySpeed - 10.0 ) : Math.Min( 0.0, _ySpeed + 10.0 );
            _xCatSpeed = _xCatSpeed > 0.0 ? Math.Max( 0.0, _xCatSpeed - 10.0 ) : Math.Min( 0.0, _xCatSpeed + 10.0 );
            _yCatSpeed = _yCatSpeed > 0.0 ? Math.Max( 0.0, _yCatSpeed - 10.0 ) : Math.Min( 0.0, _yCatSpeed + 10.0 );
            if ( Math.Abs( _xSpeed ) <= 0.0 && Math.Abs( _ySpeed ) <= 0.0 && ( Math.Abs( _xCatSpeed ) <= 0.0 && Math.Abs( _yCatSpeed ) <= 0.0 ) )
                _timer.Stop();
            if ( ParentSurface == null )
            {
                _timer.Stop();
            }
            else
            {
                using ( ParentSurface.SuspendUpdates() )
                {
                    if ( XyDirection != XyDirection.YDirection )
                    {
                        foreach ( IAxis xax in XAxes )
                        {
                            int num1 = xax.IsHorizontalAxis ? 1 : 0;
                            bool? isHorizontalAxis = XAxis?.IsHorizontalAxis;
                            int num2 = isHorizontalAxis.GetValueOrDefault() ? 1 : 0;
                            if ( num1 == num2 & isHorizontalAxis.HasValue )
                            {
                                using ( IUpdateSuspender updateSuspender = xax.SuspendUpdates() )
                                {
                                    updateSuspender.ResumeTargetOnDispose = false;
                                    double num3 = xSpeed;
                                    double num4 = ySpeed;
                                    if ( xax.IsCategoryAxis )
                                    {
                                        num3 = xCatSpeed;
                                        num4 = yCatSpeed;
                                    }
                                    double num5 = num3 * 0.5;
                                    double num6 = num4 * 0.5;
                                    xax.Scroll( xax.IsHorizontalAxis ? num5 : -num6, ClipModeX );
                                }
                            }
                            else
                                break;
                        }
                    }
                    if ( XyDirection == XyDirection.XDirection )
                    {
                        if ( !ZoomExtentsY )
                            return;
                        ParentSurface.ZoomExtentsY();
                    }
                    else
                    {
                        foreach ( IAxis yax in YAxes )
                            yax.Scroll( yax.IsHorizontalAxis ? -xSpeed : ySpeed, ClipMode.None );
                    }
                }
            }
        }
    }
}
