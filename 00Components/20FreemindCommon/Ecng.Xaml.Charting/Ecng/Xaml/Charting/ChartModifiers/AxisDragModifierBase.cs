using System.Windows;
using System.Windows.Input;
using Ecng.Xaml.Charting.Utility;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting.ChartModifiers
{
    public abstract class AxisDragModifierBase : ChartModifierBase
    {
        private readonly static DependencyProperty AxisIdProperty;

        public readonly static DependencyProperty DragModeProperty;

        public readonly static DependencyProperty MinTouchAreaProperty;

        private readonly static System.Windows.Input.Cursor DefaultCursor;

        private bool _isDragging;

        private Point _lastPoint;

        private bool _isSecondHalf;

        public string AxisId
        {
            get
            {
                return ( string ) base.GetValue( AxisDragModifierBase.AxisIdProperty );
            }
            set
            {
                base.SetValue( AxisDragModifierBase.AxisIdProperty, value );
            }
        }

        public AxisDragModes DragMode
        {
            get
            {
                return ( AxisDragModes ) base.GetValue( AxisDragModifierBase.DragModeProperty );
            }
            set
            {
                base.SetValue( AxisDragModifierBase.DragModeProperty, value );
            }
        }

        public bool IsDragging
        {
            get
            {
                return this._isDragging;
            }
            protected set
            {
                this._isDragging = value;
            }
        }

        public double MinTouchArea
        {
            get
            {
                return ( double ) base.GetValue( AxisDragModifierBase.MinTouchAreaProperty );
            }
            set
            {
                base.SetValue( AxisDragModifierBase.MinTouchAreaProperty, value );
            }
        }

        static AxisDragModifierBase()
        {
            AxisDragModifierBase.AxisIdProperty = DependencyProperty.Register( "AxisId", typeof( string ), typeof( AxisDragModifierBase ), new PropertyMetadata( "DefaultAxisId", new PropertyChangedCallback( AxisDragModifierBase.OnAxisIdChanged ) ) );
            AxisDragModifierBase.DragModeProperty = DependencyProperty.Register( "DragMode", typeof( AxisDragModes ), typeof( AxisDragModifierBase ), new PropertyMetadata( ( object ) AxisDragModes.Scale ) );
            AxisDragModifierBase.MinTouchAreaProperty = DependencyProperty.Register( "MinTouchArea", typeof( double ), typeof( AxisDragModifierBase ), new PropertyMetadata( ( object ) 0 ) );
            AxisDragModifierBase.DefaultCursor = Cursors.Arrow;
        }

        protected AxisDragModifierBase()
        {
            base.IsPolarChartSupported = false;
        }

        protected virtual DoubleRange CalculateRelativeRange( IRange fromRange, IAxis axis )
        {
            DoubleRange doubleRange = fromRange.AsDoubleRange();
            double max = doubleRange.Max;
            double min = doubleRange.Min;
            DoubleRange doubleRange1 = axis.VisibleRange.AsDoubleRange();
            object growBy = axis.GrowBy;
            if ( growBy == null )
            {
                growBy = new DoubleRange( 0, 0 );
            }
            IRange<double> range = (IRange<double>)growBy;
            double num = (doubleRange1.Min + doubleRange1.Min * range.Max + doubleRange1.Max * range.Min) / (1 + range.Min + range.Max);
            double max1 = (doubleRange1.Max + num * range.Max) / (1 + range.Max);
            double num1 = (max - max1) / (max1 - num);
            return new DoubleRange( ( min - num ) / ( -max1 + num ), num1 );

        }

        protected abstract IRange CalculateScaledRange( Point currentPoint, Point lastPoint, bool isSecondHalf, IAxis axis );

        protected abstract IAxis GetCurrentAxis();

        protected virtual bool GetIsSecondHalf( Point point, Rect axisBounds, bool isHorizontalAxis )
        {
            if ( !isHorizontalAxis )
            {
                axisBounds.Height = axisBounds.Height / 2;
            }
            else
            {
                axisBounds.Width = axisBounds.Width / 2;
            }
            return !axisBounds.Contains( point );
        }

        protected virtual System.Windows.Input.Cursor GetUsedCursor( IAxis axis )
        {
            System.Windows.Input.Cursor cursor = (axis.IsHorizontalAxis ? Cursors.SizeWE : Cursors.SizeNS);
            if ( !base.IsEnabled )
            {
                return AxisDragModifierBase.DefaultCursor;
            }
            return cursor;
        }

        public override void OnAttached()
        {
            base.OnAttached();
            this.SetAxisCursor( null );
        }

        private static void OnAxisIdChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            AxisDragModifierBase axisDragModifierBase = (AxisDragModifierBase)d;
            string oldValue = (string)e.OldValue;
            if ( axisDragModifierBase.IsAttached )
            {
                IAxis axi = (axisDragModifierBase is XAxisDragModifier ? axisDragModifierBase.GetXAxis(oldValue) : axisDragModifierBase.GetYAxis(oldValue));
                if ( axi != null )
                {
                    axi.SetMouseCursor( AxisDragModifierBase.DefaultCursor );
                }
                axisDragModifierBase.SetAxisCursor( null );
            }
        }

        public override void OnDetached()
        {
            base.OnDetached();
            this.SetAxisCursor( AxisDragModifierBase.DefaultCursor );
        }

        protected override void OnIsEnabledChanged()
        {
            if ( !base.IsEnabled )
            {
                this.SetAxisCursor( AxisDragModifierBase.DefaultCursor );
            }
        }

        public override void OnModifierMouseDown( ModifierMouseArgs e )
        {
            base.OnModifierMouseDown( e );
            IAxis currentAxis = this.GetCurrentAxis();
            if ( this.IsDragging || !base.MatchesExecuteOn( e.MouseButtons, base.ExecuteOn ) || currentAxis == null )
            {
                return;
            }
            Rect boundsRelativeTo = currentAxis.GetBoundsRelativeTo(base.RootGrid);
            if ( currentAxis.IsHorizontalAxis && boundsRelativeTo.Height < this.MinTouchArea )
            {
                boundsRelativeTo.Y = boundsRelativeTo.Y - ( this.MinTouchArea - boundsRelativeTo.Height ) / 2;
                boundsRelativeTo.Height = this.MinTouchArea;
            }
            if ( !currentAxis.IsHorizontalAxis && boundsRelativeTo.Width < this.MinTouchArea )
            {
                boundsRelativeTo.X = boundsRelativeTo.X - ( this.MinTouchArea - boundsRelativeTo.Width ) / 2;
                boundsRelativeTo.Width = this.MinTouchArea;
            }
            if ( !boundsRelativeTo.Contains( e.MousePoint ) )
            {
                return;
            }
            this._isSecondHalf = this.GetIsSecondHalf( e.MousePoint, boundsRelativeTo, currentAxis.IsHorizontalAxis );
            if ( currentAxis.FlipCoordinates )
            {
                this._isSecondHalf = !this._isSecondHalf;
            }
            this._lastPoint = e.MousePoint;
            UltrachartDebugLogger instance = UltrachartDebugLogger.Instance;
            object[] name = new object[] { base.GetType().Name, null, null };
            Point mousePoint = e.MousePoint;
            name[ 1 ] = mousePoint.X;
            mousePoint = e.MousePoint;
            name[ 2 ] = mousePoint.Y;
            instance.WriteLine( "{0} MouseDown: x={1}, y={2}", name );
            if ( e.IsMaster )
            {
                currentAxis.CaptureMouse();
            }
            this._isDragging = true;
            e.Handled = true;
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
            this.SetAxisCursor( null );
            if ( !this.IsDragging )
            {
                return;
            }
            base.OnModifierMouseMove( e );
            e.Handled = true;
            UltrachartDebugLogger instance = UltrachartDebugLogger.Instance;
            object[] name = new object[] { base.GetType().Name, null, null };
            Point mousePoint = e.MousePoint;
            name[ 1 ] = mousePoint.X;
            mousePoint = e.MousePoint;
            name[ 2 ] = mousePoint.Y;
            instance.WriteLine( "{0} MouseMove: x={1}, y={2}", name );
            Point point = e.MousePoint;
            if ( this.DragMode != AxisDragModes.Scale )
            {
                this.PerformPan( point, this._lastPoint );
            }
            else
            {
                this.PerformScale( point, this._lastPoint, this._isSecondHalf );
            }
            this._lastPoint = point;
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            if ( !this.IsDragging )
            {
                return;
            }
            base.OnModifierMouseUp( e );
            e.Handled = true;
            this._isDragging = false;
            if ( e.IsMaster )
            {
                this.GetCurrentAxis().ReleaseMouseCapture();
            }
            UltrachartDebugLogger instance = UltrachartDebugLogger.Instance;
            object[] name = new object[] { base.GetType().Name, null, null };
            Point mousePoint = e.MousePoint;
            name[ 1 ] = mousePoint.X;
            mousePoint = e.MousePoint;
            name[ 2 ] = mousePoint.Y;
            instance.WriteLine( "{0} MouseUp: x={1}, y={2}", name );
        }

        protected abstract void PerformPan( Point currentPoint, Point lastPoint );

        protected virtual void PerformScale( Point currentPoint, Point lastPoint, bool isSecondHalf )
        {
            IAxis currentAxis = this.GetCurrentAxis();
            IRange range = this.CalculateScaledRange(currentPoint, lastPoint, isSecondHalf, currentAxis);
            if ( currentAxis.AutoRange != AutoRange.Always )
            {
                currentAxis.VisibleRange = range;
                return;
            }
            ( ( AxisBase ) currentAxis ).SetValue( AxisBase.GrowByProperty, this.CalculateRelativeRange( range, currentAxis ) );
        }

        private void SetAxisCursor( System.Windows.Input.Cursor cursor = null )
        {
            IAxis currentAxis = this.GetCurrentAxis();
            if ( currentAxis != null )
            {
                currentAxis.SetMouseCursor( cursor ?? this.GetUsedCursor( currentAxis ) );
            }
        }
    }
}
