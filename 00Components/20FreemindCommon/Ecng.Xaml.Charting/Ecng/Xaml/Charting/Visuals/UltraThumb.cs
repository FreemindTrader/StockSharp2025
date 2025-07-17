// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.UltraThumb
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace StockSharp.Xaml.Charting.Visuals
{
    public class UltraThumb : Control
    {
        private bool _isDragging;
        private Point _originThumbPoint;
        private Point _previousCoordPosition;

        public event DragDeltaEventHandler UltraDragDelta;

        public UltraThumb()
        {
            this.DefaultStyleKey = ( object ) typeof( UltraThumb );
        }

        protected override void OnMouseLeftButtonDown( MouseButtonEventArgs e )
        {
            base.OnMouseLeftButtonDown( e );
            if ( this._isDragging )
                return;
            this._isDragging = true;
            e.Handled = true;
            this._originThumbPoint = e.GetPosition( ( IInputElement ) this );
            this.CaptureMouse();
        }

        protected override void OnMouseMove( MouseEventArgs e )
        {
            base.OnMouseMove( e );
            if ( !this._isDragging )
                return;
            Point position = e.GetPosition((IInputElement) this);
            if ( !( position != this._previousCoordPosition ) )
                return;
            this._previousCoordPosition = position;
            this.OnUltraDragDelta( position.X - this._originThumbPoint.X, position.Y - this._originThumbPoint.Y );
        }

        protected override void OnMouseLeftButtonUp( MouseButtonEventArgs e )
        {
            base.OnMouseLeftButtonUp( e );
            this._isDragging = false;
            this.ReleaseMouseCapture();
            this._originThumbPoint.X = 0.0;
            this._originThumbPoint.Y = 0.0;
        }

        protected virtual void OnUltraDragDelta( double horizontalChange, double verticalChange )
        {
            // ISSUE: reference to a compiler-generated field
            if ( this.UltraDragDelta == null )
                return;
            // ISSUE: reference to a compiler-generated field
            this.UltraDragDelta( ( object ) this, new DragDeltaEventArgs( horizontalChange, verticalChange ) );
        }
    }
}
