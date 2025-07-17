// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ZoomExtentsModifier
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
namespace fx.Xaml.Charting
{
    public class ZoomExtentsModifier : ChartModifierBase
    {
        public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register(nameof (IsAnimated), typeof (bool), typeof (ZoomExtentsModifier), new PropertyMetadata((object) true));
        public static readonly DependencyProperty XyDirectionProperty = DependencyProperty.Register(nameof (XyDirection), typeof (XyDirection), typeof (ZoomExtentsModifier), new PropertyMetadata((object) XyDirection.XYDirection));
        private DateTime _lastTap = DateTime.MinValue;
        private Point _lastTapPosition;

        public ZoomExtentsModifier()
        {
            this.ReceiveHandledEvents = true;
            this.SetCurrentValue( ChartModifierBase.ExecuteOnProperty, ( object ) ExecuteOn.MouseDoubleClick );
            this.DoubleTapThreshold = TimeSpan.FromMilliseconds( 500.0 );
        }

        public XyDirection XyDirection
        {
            get
            {
                return ( XyDirection ) this.GetValue( ZoomExtentsModifier.XyDirectionProperty );
            }
            set
            {
                this.SetValue( ZoomExtentsModifier.XyDirectionProperty, ( object ) value );
            }
        }

        public bool IsAnimated
        {
            get
            {
                return ( bool ) this.GetValue( ZoomExtentsModifier.IsAnimatedProperty );
            }
            set
            {
                this.SetValue( ZoomExtentsModifier.IsAnimatedProperty, ( object ) value );
            }
        }

        public override void OnModifierDoubleClick( ModifierMouseArgs e )
        {
            if ( this.ExecuteOn != ExecuteOn.MouseDoubleClick )
                return;
            base.OnModifierDoubleClick( e );
            e.Handled = true;
            this.PerformZoom();
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            if ( this.ExecuteOn != ExecuteOn.MouseRightButton || e.MouseButtons != MouseButtons.Right )
                return;
            base.OnModifierMouseUp( e );
            e.Handled = true;
            this.PerformZoom();
        }

        protected virtual void PerformZoom()
        {
            if ( this.ParentSurface == null )
                return;
            if ( this.ParentSurface.ChartModifier != null )
                this.ParentSurface.ChartModifier.ResetInertia();
            TimeSpan duration = this.IsAnimated ? TimeSpan.FromMilliseconds(500.0) : TimeSpan.Zero;
            if ( this.XyDirection == XyDirection.XYDirection )
                this.ParentSurface.AnimateZoomExtents( duration );
            else if ( this.XyDirection == XyDirection.YDirection )
                this.ParentSurface.AnimateZoomExtentsY( duration );
            else
                this.ParentSurface.AnimateZoomExtentsX( duration );
        }

        public TimeSpan DoubleTapThreshold
        {
            get; set;
        }

        public override void OnModifierTouchDown( ModifierTouchManipulationArgs e )
        {
            base.OnModifierTouchDown( e );
            if ( e.Manipulators.Count<TouchPoint>() != 1 || this.ParentSurface == null || this.ParentSurface.RootGrid == null )
                return;
            TouchPoint touchPoint = e.Manipulators.Single<TouchPoint>();
            if ( touchPoint == null || touchPoint.Action != TouchAction.Down )
                return;
            DateTime now = DateTime.Now;
            Point position = touchPoint.Position;
            if ( !( now - this._lastTap < this.DoubleTapThreshold ) || PointUtil.Distance( position, this._lastTapPosition ) >= 10.0 || ( !this.ParentSurface.RootGrid.IsPointWithinBounds( position ) || this.ExecuteOn != ExecuteOn.MouseDoubleClick ) )
                return;
            this.PerformZoom();
        }

        public override void OnModifierTouchUp( ModifierTouchManipulationArgs e )
        {
            base.OnModifierTouchUp( e );
            if ( e.Manipulators.Count<TouchPoint>() != 1 )
                return;
            this._lastTap = DateTime.Now;
            this._lastTapPosition = e.Manipulators.Single<TouchPoint>().Position;
        }
    }
}
