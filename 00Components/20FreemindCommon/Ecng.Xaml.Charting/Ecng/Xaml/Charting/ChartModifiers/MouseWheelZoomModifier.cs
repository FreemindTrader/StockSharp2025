// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.MouseWheelZoomModifier
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Windows;
namespace fx.Xaml.Charting
{
    public class MouseWheelZoomModifier : RelativeZoomModifierBase
    {
        public static readonly DependencyProperty ActionTypeProperty = DependencyProperty.Register(nameof (ActionType), typeof (ActionType), typeof (MouseWheelZoomModifier), new PropertyMetadata((object) ActionType.Zoom, (PropertyChangedCallback) ((sender, args) =>
        {
            MouseWheelZoomModifier wheelZoomModifier = sender as MouseWheelZoomModifier;
            ActionType newValue = (ActionType) args.NewValue;
            if (wheelZoomModifier == null)
            {
                return;
            }

            wheelZoomModifier._performAction = newValue == ActionType.Pan ? new Action<Point, double>(wheelZoomModifier.PerformPan) : new Action<Point, double>(wheelZoomModifier.PerformZoom);
        })));
        private Action<Point, double> _performAction;

        public MouseWheelZoomModifier()
        {
            GrowFactor = 0.1;
            _performAction = new Action<Point, double>( PerformZoom );
        }

        public ActionType ActionType
        {
            get
            {
                return ( ActionType ) GetValue( MouseWheelZoomModifier.ActionTypeProperty );
            }
            set
            {
                SetValue( MouseWheelZoomModifier.ActionTypeProperty, ( object ) value );
            }
        }

        private void PerformZoom( Point point, double value )
        {
            PerformZoom( point, value, value );
        }

        public override void OnModifierMouseWheel( ModifierMouseArgs e )
        {
            base.OnModifierMouseWheel( e );
            e.Handled = true;
            using ( ParentSurface.SuspendUpdates() )
            {
                double num = (double) -e.Delta / 120.0;
                XyDirection xyDirection = XyDirection;
                ActionType actionType = ActionType;
                if ( e.Modifier != MouseModifier.None )
                {
                    SetCurrentValue( MouseWheelZoomModifier.ActionTypeProperty, ( object ) ActionType.Pan );
                    if ( e.Modifier == MouseModifier.Ctrl )
                    {
                        SetCurrentValue( RelativeZoomModifierBase.XyDirectionProperty, ( object ) XyDirection.YDirection );
                    }
                    else if ( e.Modifier == MouseModifier.Shift )
                    {
                        SetCurrentValue( RelativeZoomModifierBase.XyDirectionProperty, ( object ) XyDirection.XDirection );
                    }
                }
                _performAction( GetPointRelativeTo( e.MousePoint, ( IHitTestable ) ModifierSurface ), num );
                SetCurrentValue( RelativeZoomModifierBase.XyDirectionProperty, ( object ) xyDirection );
                SetCurrentValue( MouseWheelZoomModifier.ActionTypeProperty, ( object ) actionType );
            }
        }

        private void PerformPan( Point mousePoint, double value )
        {
            if ( XyDirection == XyDirection.YDirection || XyDirection == XyDirection.XYDirection )
            {
                foreach ( IAxis yax in YAxes )
                {
                    double axisSize = GetAxisSize(yax);
                    double pixelsToScroll = value * GrowFactor * axisSize;
                    yax.Scroll( pixelsToScroll, ClipMode.None );
                }
                UltrachartDebugLogger.Instance.WriteLine( "Growing YRange: {0}", ( object ) value );
            }
            if ( XyDirection != XyDirection.XDirection && XyDirection != XyDirection.XYDirection )
            {
                return;
            }

            foreach ( IAxis xax in XAxes )
            {
                int num1 = xax.IsHorizontalAxis ? 1 : 0;
                bool? isHorizontalAxis = XAxis?.IsHorizontalAxis;
                int num2 = isHorizontalAxis.GetValueOrDefault() ? 1 : 0;
                if ( num1 == num2 & isHorizontalAxis.HasValue )
                {
                    double axisSize = GetAxisSize(xax);
                    double pixelsToScroll = -value * GrowFactor * axisSize;
                    xax.Scroll( pixelsToScroll, ClipMode.None );
                }
                else
                {
                    break;
                }
            }
            UltrachartDebugLogger.Instance.WriteLine( "Growing XRange: {0}", ( object ) ( int ) value );
        }

        private double GetAxisSize( IAxis axis )
        {
            double num = axis.IsHorizontalAxis ? axis.Width : axis.Height;
            if ( Math.Abs( num ) < double.Epsilon && ParentSurface != null && ParentSurface.RenderSurface != null )
            {
                num = axis.IsHorizontalAxis ? ParentSurface.RenderSurface.ActualWidth : ParentSurface.RenderSurface.ActualHeight;
            }

            if ( axis.IsPolarAxis )
            {
                num /= 2.0;
            }

            return num;
        }
    }
}
