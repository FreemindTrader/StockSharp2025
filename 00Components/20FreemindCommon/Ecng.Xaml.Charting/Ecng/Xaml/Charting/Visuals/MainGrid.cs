// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.MainGrid
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace fx.Xaml.Charting
{
    public class MainGrid : Grid, IMainGrid, IPublishMouseEvents, IHitTestable
    {
        private readonly IList<TouchPoint> _downPoints = (IList<TouchPoint>) new List<TouchPoint>();
        private readonly IList<TouchPoint> _upPoints = (IList<TouchPoint>) new List<TouchPoint>();
        private readonly IList<TouchPoint> _movePoints = (IList<TouchPoint>) new List<TouchPoint>();
        private readonly RoutedEventHandler _loadedHandler;
        private readonly RoutedEventHandler _unloadedHandler;

        public event EventHandler<TouchManipulationEventArgs> TouchDown;

        public event EventHandler<TouchManipulationEventArgs> TouchMove;

        public event EventHandler<TouchManipulationEventArgs> TouchUp;

        public event MouseButtonEventHandler MouseMiddleButtonDown;

        public event MouseButtonEventHandler MouseMiddleButtonUp;

        private void PreviewMouseUpHandler( object sender, MouseButtonEventArgs e )
        {
            if ( e.ChangedButton != MouseButton.Middle )
            {
                return;
            }
            // ISSUE: reference to a compiler-generated field
            MouseButtonEventHandler mouseMiddleButtonUp = this.MouseMiddleButtonUp;
            if ( mouseMiddleButtonUp == null )
            {
                return;
            }

            mouseMiddleButtonUp( sender, e );
        }

        private void PreviewMouseDownHandler( object sender, MouseButtonEventArgs e )
        {
            if ( e.ChangedButton != MouseButton.Middle )
            {
                return;
            }
            // ISSUE: reference to a compiler-generated field
            MouseButtonEventHandler middleButtonDown = this.MouseMiddleButtonDown;
            if ( middleButtonDown == null )
            {
                return;
            }

            middleButtonDown( sender, e );
        }

        public MainGrid()
        {
            this.PreviewMouseDown += new MouseButtonEventHandler( this.PreviewMouseDownHandler );
            this.PreviewMouseUp += new MouseButtonEventHandler( this.PreviewMouseUpHandler );
            TouchFrameEventHandler handler = new TouchFrameEventHandler(this.OnTouchFrameReported);
            this._loadedHandler = ( RoutedEventHandler ) ( ( s, a ) =>
            {
                Touch.FrameReported -= handler;
                Touch.FrameReported += handler;
            } );
            this._unloadedHandler = ( RoutedEventHandler ) ( ( s, a ) => Touch.FrameReported -= handler );
            this.Loaded += this._loadedHandler;
            this.Unloaded += this._unloadedHandler;
        }

        public void UnregisterEventsOnShutdown()
        {
            Touch.FrameReported -= new TouchFrameEventHandler( this.OnTouchFrameReported );
            this.Loaded -= this._loadedHandler;
            this.Unloaded -= this._unloadedHandler;
        }

        private void TouchDownHandler( object sender, TouchManipulationEventArgs e )
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<TouchManipulationEventArgs> touchDown = this.TouchDown;
            if ( touchDown == null )
            {
                return;
            }

            touchDown( sender, e );
        }

        private void TouchUpHandler( object sender, TouchManipulationEventArgs e )
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<TouchManipulationEventArgs> touchUp = this.TouchUp;
            if ( touchUp == null )
            {
                return;
            }

            touchUp( sender, e );
        }

        private void TouchMoveHandler( object sender, TouchManipulationEventArgs e )
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler<TouchManipulationEventArgs> touchMove = this.TouchMove;
            if ( touchMove == null )
            {
                return;
            }

            touchMove( sender, e );
        }

        private void OnTouchFrameReported( object sender, TouchFrameEventArgs e )
        {
            TouchPointCollection touchPoints;
            try
            {
                touchPoints = e.GetTouchPoints( ( IInputElement ) this );
            }
            catch ( Exception ex )
            {
                return;
            }
            this._downPoints.Clear();
            this._upPoints.Clear();
            this._movePoints.Clear();
            foreach ( TouchPoint touchPoint in ( Collection<TouchPoint> ) touchPoints )
            {
                switch ( touchPoint.Action )
                {
                    case TouchAction.Down:
                        this._downPoints.Add( touchPoint );
                        continue;
                    case TouchAction.Move:
                        this._movePoints.Add( touchPoint );
                        continue;
                    case TouchAction.Up:
                        this._upPoints.Add( touchPoint );
                        continue;
                    default:
                        continue;
                }
            }
            if ( this._upPoints.Count > 0 )
            {
                this.TouchUpHandler( sender, new TouchManipulationEventArgs( ( IEnumerable<TouchPoint> ) this._upPoints ) );
            }

            if ( this._movePoints.Count > 0 )
            {
                this.TouchMoveHandler( sender, new TouchManipulationEventArgs( ( IEnumerable<TouchPoint> ) this._movePoints ) );
            }

            if ( this._downPoints.Count <= 0 )
            {
                return;
            }

            this.TouchDownHandler( sender, new TouchManipulationEventArgs( ( IEnumerable<TouchPoint> ) this._downPoints ) );
        }

        public Point TranslatePoint( Point point, IHitTestable relativeTo )
        {
            return ElementExtensions.TranslatePoint( this, point, relativeTo );
        }

        public bool IsPointWithinBounds( Point point )
        {
            return HitTestableExtensions.IsPointWithinBounds( this, point );
        }

        public Rect GetBoundsRelativeTo( IHitTestable relativeTo )
        {
            return ElementExtensions.GetBoundsRelativeTo( this, relativeTo );
        }

        [SpecialName]
        double IHitTestable.ActualWidth
        {
            get
            {
                return this.ActualWidth;
            }

        }

        [SpecialName]
        double IHitTestable.ActualHeight
        {
            get
            {
                return this.ActualHeight;
            }
        }
    }
}
