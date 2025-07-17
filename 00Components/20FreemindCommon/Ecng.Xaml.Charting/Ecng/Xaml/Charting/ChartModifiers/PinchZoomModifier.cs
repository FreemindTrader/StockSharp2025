using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Ecng.Xaml.Charting
{
    public class PinchZoomModifier : RelativeZoomModifierBase
    {
        private const int MinAllowedManipulatorsAmount = 2;

        private bool _isDragging;

        private Point _center;

        private readonly Dictionary<int, Point> _points;

        private readonly Dictionary<int, Point> _startPoints;

        private double _dist;

        private double _distX;

        private double _distY;

        private readonly DispatcherTimer _bufferTimer;

        private bool _bufferState;

        private bool _bufferAny;

        private Point _bufferPoint;

        private double _bufferX;

        private double _bufferY;

        public bool IsDragging
        {
            get
            {
                return _isDragging;
            }
        }

        public bool IsUniform
        {
            get;
            set;
        }

        public PinchZoomModifier()
        {
            base.GrowFactor = 0.01;
            _points = new Dictionary<int, Point>();
            _startPoints = new Dictionary<int, Point>();
            _bufferTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds( 40 )
            };
            _bufferTimer.Tick += new EventHandler( PerformZoomBuffer );
        }

        private void ContinueZooming( Point mousePoint, double xValue, double yValue )
        {
            if ( !_bufferState )
            {
                PerformZoom( mousePoint, xValue, yValue );
                return;
            }
            _bufferPoint = mousePoint;
            _bufferX += xValue;
            _bufferY += yValue;
            _bufferAny = true;
        }

        private Point GetCenter()
        {
            return new Point( _startPoints.Average<KeyValuePair<int, Point>>( ( KeyValuePair<int, Point> it ) => it.Value.X ), _startPoints.Average<KeyValuePair<int, Point>>( ( KeyValuePair<int, Point> it ) => it.Value.Y ) );

        }

        private double Normalize( double value )
        {
            return Math.Min( 1, Math.Max( -1, value ) );
        }

        public override void OnModifierTouchDown( ModifierTouchManipulationArgs e )
        {
            foreach ( TouchPoint manipulator in e.Manipulators )
            {
                Point pointRelativeTo = base.GetPointRelativeTo(manipulator.Position, base.ModifierSurface);
                if ( pointRelativeTo.X < 0 || pointRelativeTo.X > base.ModifierSurface.ActualWidth || pointRelativeTo.Y < 0 || pointRelativeTo.Y > base.ModifierSurface.ActualHeight || _points.ContainsKey( manipulator.TouchDevice.Id ) )
                {
                    continue;
                }
                _points.Add( manipulator.TouchDevice.Id, pointRelativeTo );
                _startPoints.Add( manipulator.TouchDevice.Id, pointRelativeTo );
            }
            if ( _points.Count >= 2 )
            {
                _isDragging = true;
                _distX = _points.Max<KeyValuePair<int, Point>>( ( KeyValuePair<int, Point> it ) => it.Value.X ) - _points.Min<KeyValuePair<int, Point>>( ( KeyValuePair<int, Point> it ) => it.Value.X );
                _distY = _points.Max<KeyValuePair<int, Point>>( ( KeyValuePair<int, Point> it ) => it.Value.Y ) - _points.Min<KeyValuePair<int, Point>>( ( KeyValuePair<int, Point> it ) => it.Value.Y );
                _dist = Math.Sqrt( _distX * _distX + _distY * _distY );
                _center = GetCenter();
            }
            base.OnModifierTouchDown( e );
        }

        public override void OnModifierTouchMove( ModifierTouchManipulationArgs e )
        {
            double num;
            if ( !IsDragging )
            {
                return;
            }
            foreach ( TouchPoint manipulator in e.Manipulators )
            {
                if ( !_points.ContainsKey( manipulator.TouchDevice.Id ) )
                {
                    continue;
                }
                int id = manipulator.TouchDevice.Id;
                Point pointRelativeTo = base.GetPointRelativeTo(manipulator.Position, base.ModifierSurface);
                _points[ id ] = pointRelativeTo;
            }
            double num1 = _points.Max<KeyValuePair<int, Point>>((KeyValuePair<int, Point> it) => it.Value.X) - _points.Min<KeyValuePair<int, Point>>((KeyValuePair<int, Point> it) => it.Value.X);
            double num2 = _points.Max<KeyValuePair<int, Point>>((KeyValuePair<int, Point> it) => it.Value.Y) - _points.Min<KeyValuePair<int, Point>>((KeyValuePair<int, Point> it) => it.Value.Y);
            double num3 = Math.Sqrt(num1 * num1 + _distY * _distY);
            double num4 = num1 - _distX;
            double num5 = num2 - _distY;
            double num6 = num3 - _dist;
            if ( _startPoints.Count >= 2 )
            {
                _center = GetCenter();
                double num7 = -Normalize(num6);
                double num8 = (IsUniform ? num7 : -Normalize(num5));
                num = ( IsUniform ? num7 : -Normalize( num4 ) );
                ContinueZooming( _center, num8, num );
                _dist = num3;
                _distX = num1;
                _distY = num2;
            }
            base.OnModifierTouchMove( e );
        }

        public override void OnModifierTouchUp( ModifierTouchManipulationArgs e )
        {
            foreach ( TouchPoint manipulator in e.Manipulators )
            {
                int id = manipulator.TouchDevice.Id;
                if ( !_points.ContainsKey( id ) )
                {
                    continue;
                }
                _points.Remove( id );
                _startPoints.Remove( id );
            }
            if ( !_startPoints.Any<KeyValuePair<int, Point>>() )
            {
                _isDragging = false;
            }
        }

        protected override void PerformZoom( Point mousePoint, double xValue, double yValue )
        {
            _bufferState = true;
            _bufferAny = false;
            _bufferTimer.Start();
            base.PerformZoom( mousePoint, xValue, yValue );
        }

        private void PerformZoomBuffer( object state, EventArgs eventArgs )
        {
            _bufferTimer.Stop();
            _bufferState = false;
            if ( _bufferAny )
            {
                PerformZoom( _bufferPoint, _bufferX, _bufferY );
            }
            _bufferAny = false;
            double num = 0;
            double num1 = num;
            _bufferY = num;
            _bufferX = num1;
            _bufferPoint = new Point();
        }
    }
}
