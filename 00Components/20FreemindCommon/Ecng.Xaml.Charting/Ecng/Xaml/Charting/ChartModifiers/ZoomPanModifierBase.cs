// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ZoomPanModifierBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Windows;
using System.Windows.Input;
namespace Ecng.Xaml.Charting
{
    public abstract class ZoomPanModifierBase : ChartModifierBase
    {
        public static readonly DependencyProperty XyDirectionProperty = DependencyProperty.Register(nameof (XyDirection), typeof (XyDirection), typeof (ZoomPanModifierBase), new PropertyMetadata((object) XyDirection.XYDirection));
        public static readonly DependencyProperty ClipModeXProperty = DependencyProperty.Register(nameof (ClipModeX), typeof (ClipMode), typeof (ZoomPanModifierBase), new PropertyMetadata((object) ClipMode.StretchAtExtents));
        public static readonly DependencyProperty ZoomExtentsYProperty = DependencyProperty.Register(nameof (ZoomExtentsY), typeof (bool), typeof (ZoomPanModifierBase), new PropertyMetadata((object) true));
        private Point _startPoint;
        private Point _lastPoint;

        public bool ZoomExtentsY
        {
            get
            {
                return ( bool ) this.GetValue( ZoomPanModifierBase.ZoomExtentsYProperty );
            }
            set
            {
                this.SetValue( ZoomPanModifierBase.ZoomExtentsYProperty, ( object ) value );
            }
        }

        public XyDirection XyDirection
        {
            get
            {
                return ( XyDirection ) this.GetValue( ZoomPanModifierBase.XyDirectionProperty );
            }
            set
            {
                this.SetValue( ZoomPanModifierBase.XyDirectionProperty, ( object ) value );
            }
        }

        public ClipMode ClipModeX
        {
            get
            {
                return ( ClipMode ) this.GetValue( ZoomPanModifierBase.ClipModeXProperty );
            }
            set
            {
                this.SetValue( ZoomPanModifierBase.ClipModeXProperty, ( object ) value );
            }
        }

        public bool IsDragging
        {
            get; protected set;
        }

        protected ZoomPanModifierBase()
        {
            this.SetCurrentValue( ChartModifierBase.ExecuteOnProperty, ( object ) ExecuteOn.MouseLeftButton );
            this.IsPolarChartSupported = false;
        }

        public override void OnModifierMouseDown( ModifierMouseArgs e )
        {
            if ( this.IsDragging || !this.MatchesExecuteOn( e.MouseButtons, this.ExecuteOn ) || ( this.XAxes.IsNullOrEmpty<IAxis>() || !e.IsMaster ) || !this.ModifierSurface.GetBoundsRelativeTo( ( IHitTestable ) this.RootGrid ).Contains( e.MousePoint ) )
                return;
            Point mousePoint = e.MousePoint;
            base.OnModifierMouseDown( e );
            e.Handled = true;
            this.SetCursor( Cursors.Hand );
            UltrachartDebugLogger.Instance.WriteLine( "{0} MouseDown: x={1}, y={2}", ( object ) this.GetType().Name, ( object ) e.MousePoint.X, ( object ) e.MousePoint.Y );
            if ( e.IsMaster )
                this.ModifierSurface.CaptureMouse();
            this._startPoint = mousePoint;
            this._lastPoint = this._startPoint;
            this.IsDragging = true;
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            base.OnModifierMouseUp( e );
            e.Handled = true;
            this.IsDragging = false;
            this._startPoint = new Point();
            if ( e.IsMaster )
                this.ModifierSurface.ReleaseMouseCapture();
            this.SetCursor( Cursors.Arrow );
            UltrachartDebugLogger.Instance.WriteLine( "{0} MouseUp: x={1}, y={2}", ( object ) this.GetType().Name, ( object ) e.MousePoint.X, ( object ) e.MousePoint.Y );
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
            if ( !this.IsDragging )
                return;
            base.OnModifierMouseMove( e );
            e.Handled = true;
            UltrachartDebugLogger.Instance.WriteLine( "{0} MouseMove: x={1}, y={2}", ( object ) this.GetType().Name, ( object ) e.MousePoint.X, ( object ) e.MousePoint.Y );
            Point mousePoint = e.MousePoint;
            this.Pan( mousePoint, this._lastPoint, this._startPoint );
            this._lastPoint = mousePoint;
        }

        public abstract void Pan( Point currentPoint, Point lastPoint, Point startPoint );
    }
}
