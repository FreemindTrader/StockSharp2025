using fx.Algorithm;
using SciChart.Charting.DrawingTools.TradingAnnotations;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.Axes;
using SciChart.Core.Extensions;
using SciChart.Core.Utility.Mouse;
using SciChart.Data.Model;
using fx.Definitions;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using fx.Bars;

namespace fx.Charting.HewFibonacci
{    
    public class fxFibonacciRetracementAnnotationBase : MyTradingAnnotationBase, ITradingAnnotation, IfxFibonacciAnnotation
    {
        protected Style     _gripStyle;

        private IComparable _startingX;
        private IComparable _startingY;
        private IComparable _endingX;
        private IComparable _endingY;
        private IComparable _nextWaveX;
        private IComparable _nextWaveY;

        protected HewFibGannTargets _fibTarget;

        PooledList<SBar> _targetBars = new PooledList<SBar>( );

        public FibonacciType FibType
        {
            get;
            set;
        } = FibonacciType.Wave2Retracement;

        public fxFibonacciRetracementAnnotationBase( HewFibGannTargets fib, ref SBar lastBar ) : base( ref lastBar )
        {
            BasePointsCount = 2;
            _fibTarget      = fib;
            _lastBar        = lastBar;
        }

        

        public fxFibonacciRetracementAnnotationBase( HewFibGannTargets fib, ref SBar lastBar, PooledList<SBar> targetPoints ) : base( ref lastBar )
        {
            BasePointsCount = 2;
            _fibTarget      = fib;
            _lastBar        = lastBar;
            _targetBars     = targetPoints;
        }        
        

        public override void Update( ICoordinateCalculator<double> xCoordinateCalculator, ICoordinateCalculator<double> yCoordinateCalculator )
        {
            base.Update( xCoordinateCalculator, yCoordinateCalculator );

            if ( !IsLoaded || !_timeFrameWasChanged )
            {
                return;
            }

            RefreshBasePoints( );
        }

        private void RefreshBasePoints( )
        {
            Point[ ] basePoints = GetBasePoints( );            

            for ( int index = 0; index < basePoints.Count( ); ++index )
            {
                Point point = basePoints[ index ];
                SetBasePoint( FromCoordinate( point.X, XAxis ), FromCoordinate( point.Y, YAxis ), index );
            }

            _timeFrameWasChanged = false;
            Refresh( );
        }

        public override void UpdateBasePoint( int index, IComparable x, IComparable y )
        {
            base.UpdateBasePoint( index, x, y );
            if ( !IsAttached )
            {
                return;
            }

            SetBasePoint( x, y, index );

            if ( !IsCreated )
            {
                if ( Annotations == null )
                {
                    return;
                }

                var line     = Annotations.FirstOrDefault( a => a is LineAnnotation );
                var fibTable = Annotations.FirstOrDefault( a => a is fxFibonacciTableAnnotation );

                if ( line == null || fibTable == null )
                {
                    return;
                }

                line.X2     = x;
                line.Y2     = y;
                fibTable.X2 = x;
                fibTable.Y2 = y;
            }

            var xCalc = ParentSurface.XAxes.First().GetCurrentCoordinateCalculator( );
            var yCalc = ParentSurface.YAxes.First().GetCurrentCoordinateCalculator( );

            TryUpdate( xCalc, yCalc );
        }

        public void SetRetracementStartPoint( IComparable x, IComparable y )
        {
            _startingX = x;
            _startingY = y;            
        }


        public void SetRetracementEndPoint( IComparable x, IComparable y )
        {
            _endingX = x;
            _endingY = y;            
        }

        public void SetNextWaveEndPoint( IComparable x, IComparable y )
        {
            _nextWaveX = x;
            _nextWaveY = y;            
        }

        public void SetAllBasePoints()
        {
            if ( _nextWaveX != null )
            {
                var beginX    = _endingX;
                var beginY    = _startingY;

                var endX      = _nextWaveX;
                var endY      = _endingY;

                SetBasePoint( endX, endY );
                SetBasePoint( beginX, beginY );
                
            }
            else
            {
                //var diff    = (DateTime)(_endingX )- ( DateTime )(_startingX);
                //var newDist = TimeSpan.FromTicks( diff.Ticks * 10 );

                var beginX    = _endingX;
                var beginY    = _startingY;

                var endX      = DateTime.UtcNow.AddDays( 1 ) ;
                var endY      = _endingY;

                SetBasePoint( endX, endY );
                SetBasePoint( beginX, beginY );
                

            }
        }

        public TrendDirection Trend
        {
            get
            {
                if (  _startingY.ToDouble( ) > _endingY.ToDouble() )
                {
                    return TrendDirection.Uptrend;
                }

                return TrendDirection.DownTrend;
            }
        }

        public DateTime EndingTime
        {
            get
            {
                return _endingX.ToDateTime( );
            }
        }

        public HewFibGannTargets FibTarget
        {
            get
            {
                return _fibTarget;
            }
        }



        public override void SetBasePoint( IComparable x, IComparable y )
        {
            base.SetBasePoint( x, y );
            if ( !IsAttached )
            {
                return;
            }
            if ( Annotations.Count( ) == 2 )
            {
                UpdateBasePoint( 2, x, y );
                OnAnnotationCreated( );
                RefreshBasePoints( );
            }
            else
            {
                var line             = new LineAnnotation( );                
                line.X1              = x;
                line.Y1              = y;
                line.X2              = x;
                line.Y2              = y;
                line.StrokeDashArray = new DoubleCollection( ) { 2.0, 4.0 };
                line.StrokeThickness = 1.0;
                line.Stroke          = new SolidColorBrush( Color.FromArgb( byte.MaxValue, 186, 186, 186 ) );
                line.Opacity         = 0.5;
                Panel.SetZIndex( line, 1 );
                line.IsEditable      = false;                
                
                SetMovingPart( line, true );
                Annotations.Add( line );

                // Have to set the LineAnnotation hidden after being add to collection or else the Line will be visible.
                //line.IsHidden = true;

                ICategoryCoordinateCalculator<DateTime> coordinateCalculator = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;

                if ( coordinateCalculator != null )
                {
                    x = coordinateCalculator.TransformDataToIndex( x );
                }

                if ( _fibTarget  != null )
                {
                    var fibTable        = new fxFibonacciTableAnnotation( _fibTarget, _gripStyle, ref _lastBar,  _targetBars );
                    fibTable.X1         = x;
                    fibTable.Y1         = y;
                    fibTable.X2         = x;
                    fibTable.Y2         = y;
                    fibTable.IsEditable = false;                    

                    Annotations.Add( fibTable );
                }
                else
                {
                    var fibTable        = new fxFibonacciTableAnnotation( FibonacciType.Wave2Retracement, _gripStyle, ref _lastBar, _endingY.ToDouble() > _startingY.ToDouble( ), _targetBars );
                    fibTable.X1         = x;
                    fibTable.Y1         = y;
                    fibTable.X2         = x;
                    fibTable.Y2         = y;
                    fibTable.IsEditable = false;

                    Annotations.Add( fibTable );
                }
            }
        }

        


        protected override void OnAnnotationPointerMoved( ModifierMouseArgs e )
        {
            return;
        }

        protected override void SetBasePoint( Point newPoint, int index, IAxis xAxis, IAxis yAxis )
        {
            base.SetBasePoint( newPoint, index, xAxis, yAxis );
            SetBasePoint( FromCoordinate( newPoint.X, xAxis ), FromCoordinate( newPoint.Y, yAxis ), index );
        }

        protected override void SetBasePoint( IComparable xDataValue, IComparable yDataValue, int index )
        {
            IAnnotation line     = Annotations.FirstOrDefault( a => a is LineAnnotation );
            IAnnotation fibTable = Annotations.FirstOrDefault( a => a is fxFibonacciTableAnnotation );

            if ( line == null || fibTable == null )
            {
                return;
            }

            if ( index == 0 )
            {
                line.X1     = xDataValue;
                line.Y1     = yDataValue;
                fibTable.X1 = xDataValue;
                fibTable.Y1 = yDataValue;
            }
            else
            {
                line.X2     = xDataValue;
                line.Y2     = yDataValue;
                fibTable.X2 = xDataValue;
                fibTable.Y2 = yDataValue;
            }
        }
    }
}
