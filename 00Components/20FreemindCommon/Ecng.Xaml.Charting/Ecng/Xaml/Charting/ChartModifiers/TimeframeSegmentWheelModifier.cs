// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.TimeframeSegmentWheelModifier
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Windows;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Visuals;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public class TimeframeSegmentWheelModifier : RelativeZoomModifierBase
    {
        public static readonly DependencyProperty ActionTypeProperty = DependencyProperty.Register(nameof (ActionType), typeof (ActionType), typeof (TimeframeSegmentWheelModifier), new PropertyMetadata((object) ActionType.Pan, (PropertyChangedCallback) ((sender, args) =>
        {
            TimeframeSegmentWheelModifier segmentWheelModifier = sender as TimeframeSegmentWheelModifier;
            ActionType newValue = (ActionType) args.NewValue;
            if (segmentWheelModifier == null)
            {
                return;
            }

            segmentWheelModifier._performAction = newValue == ActionType.Pan ? new Action<Point, double>(segmentWheelModifier.PerformPan) : new Action<Point, double>(segmentWheelModifier.PerformZoom);
        })));
        private Action<Point, double> _performAction;

        public TimeframeSegmentWheelModifier()
        {
            GrowFactor = 0.05;
            _performAction = new Action<Point, double>( PerformPan );
        }

        public ActionType ActionType
        {
            get
            {
                return ( ActionType ) GetValue( TimeframeSegmentWheelModifier.ActionTypeProperty );
            }
            set
            {
                SetValue( TimeframeSegmentWheelModifier.ActionTypeProperty, ( object ) value );
            }
        }

        private void PerformZoom( Point point, double value )
        {
            PerformZoom( point, value, value );
        }

        public override void OnModifierMouseWheel( ModifierMouseArgs e )
        {
            base.OnModifierMouseWheel( e );
            using ( ParentSurface.SuspendUpdates() )
            {
                double num = (double) -e.Delta / 120.0;
                XyDirection xyDirection = XyDirection;
                ActionType actionType = ActionType;
                switch ( e.Modifier )
                {
                    case MouseModifier.None:
                        ActionType = ActionType.Pan;
                        XyDirection = XyDirection.YDirection;
                        break;
                    case MouseModifier.Shift:
                        return;
                    case MouseModifier.Ctrl:
                        ActionType = ActionType.Zoom;
                        break;
                    case MouseModifier.Shift | MouseModifier.Ctrl:
                        return;
                    case MouseModifier.Alt:
                        ActionType = ActionType.Pan;
                        XyDirection = XyDirection.XDirection;
                        break;
                    default:
                        return;
                }
                e.Handled = true;
                _performAction( GetPointRelativeTo( e.MousePoint, ( IHitTestable ) ModifierSurface ), num );
                XyDirection = xyDirection;
                ActionType = actionType;
            }
        }

        private void PerformPan( Point mousePoint, double value )
        {
            if ( XyDirection == XyDirection.YDirection || XyDirection == XyDirection.XYDirection )
            {
                foreach ( IAxis yax in YAxes )
                {
                    double num = yax.IsHorizontalAxis ? yax.Width : yax.Height;
                    double pixelsToScroll = value * GrowFactor * num;
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
                    double num3 = xax.IsHorizontalAxis ? xax.Width : xax.Height;
                    double pixelsToScroll = -value * GrowFactor * num3;
                    xax.Scroll( pixelsToScroll, ClipMode.None );
                }
                else
                {
                    break;
                }
            }
            UltrachartDebugLogger.Instance.WriteLine( "Growing XRange: {0}", ( object ) ( int ) value );
        }
    }
}
