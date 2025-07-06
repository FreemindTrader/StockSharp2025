using fx.Algorithm;
using SciChart.Charting.DrawingTools.TradingAnnotations;
using SciChart.Charting.DrawingTools.TradingAnnotations.FibonacciAnnotation;
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
using fx.Bars;

namespace StockSharp.Xaml.Charting.HewFibonacci
{
    public class fxFibonacciExtensionAnnotationBase : MyTradingAnnotationBase, ITradingAnnotation, IfxFibonacciAnnotation
    {                
        private bool             _done3Points = false;

        protected Style          _gripStyle;
        private IComparable      _startingX;
        private IComparable      _startingY;
        private IComparable      _endingX;
        private IComparable      _endingY;
        private IComparable      _projectionX;
        private IComparable      _projectionY;

        private HewFibGannTargets _fibTarget;

        PooledList<SBar>          _targetBars = new PooledList<SBar>( );

        public FibonacciType FibType
        {
            get;
            set;
        } = FibonacciType.ABCWaveCProjection;

        public fxFibonacciExtensionAnnotationBase( HewFibGannTargets fib, ref SBar lastBar ) : base( ref lastBar )
        {
            BasePointsCount = 3;
            _fibTarget = fib;
            
        }

        public fxFibonacciExtensionAnnotationBase( FibonacciType fibType, ref SBar lastBar, PooledList<SBar> targetPoints ) : base( ref lastBar )
        {
            BasePointsCount = 3;
            FibType = fibType;
            
            _targetBars = targetPoints;
        }

        public fxFibonacciExtensionAnnotationBase( HewFibGannTargets fib, ref SBar lastBar, PooledList<SBar> targetPoints ) : base( ref lastBar )
        {
            BasePointsCount = 3;
            _fibTarget = fib;
            _lastBar = lastBar;
            _targetBars = targetPoints;
        }

        public void SetProjectionStartPoint( IComparable x, IComparable y )
        {
            _startingX = x;
            _startingY = y;
        }


        public void SetProjectionEndPoint( IComparable x, IComparable y )
        {
            _endingX = x;
            _endingY = y;
        }

        public void SetProjectionProjPoint( IComparable x, IComparable y )
        {
            _projectionX = x;
            _projectionY = y;
        }

        public TrendDirection Trend
        {
            get
            {
                if ( _startingY.ToDouble( ) > _endingY.ToDouble( ) )
                {
                    return TrendDirection.DownTrend;
                }

                return TrendDirection.Uptrend;
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

        private DateTime _endingIndexTime = DateTime.MinValue;

        public void SetEndingIndexTime( DateTime endingWave )
        {
            _endingIndexTime = endingWave;
        }

        public void SetAllBasePoints( )
        {
            //var beginX    = _endingX;
            //var beginY    = _startingY;

            //var endX      = DateTime.UtcNow.AddDays( 1 ) ;
            //var endY      = _endingY;

            //SetBasePoint( endX, endY );
            //SetBasePoint( beginX, beginY );

            SetBasePoint( _startingX, _startingY );
            SetBasePoint( _endingX, _endingY );
            SetBasePoint( _projectionX, _projectionY );            
        }

        
        

        public override void Update( ICoordinateCalculator<double> xCoordinateCalculator, ICoordinateCalculator<double> yCoordinateCalculator )
        {
            base.Update( xCoordinateCalculator, yCoordinateCalculator );

            if ( !IsLoaded || !_timeFrameWasChanged )
            {
                return;
            }

            InitBasePoints( );
        }

        protected override void OnAnnotationPointerMoved( ModifierMouseArgs e )
        {
            return;
        }

        private void InitBasePoints( )
        {
            Point[ ] basePoints = GetBasePoints( );

            for ( int index = 0; index < basePoints.Count( ); ++index )
            {
                Point point = basePoints[ index ];
                SetBasePoint( FromCoordinate( point.X, XAxis ), FromCoordinate( point.Y, YAxis ), index );
            }

            _timeFrameWasChanged = false;
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
                IAnnotation[ ] movingLinesPart = MovingLinesPartAnnotations.ToArray( );

                if ( index == 1 )
                {
                    IAnnotation firstPt = movingLinesPart[ 0 ];

                    if ( firstPt != null )
                    {
                        firstPt.X2 = x;
                        firstPt.Y2 = y;
                    }
                }
                else
                {
                    var endPt        = movingLinesPart[ 1 ];
                    var projectionPt = Annotations.LastOrDefault( );
                    var tuple        = GetProjection( x, y );

                    if ( endPt == null || projectionPt == null )
                    {
                        return;
                    }
                    endPt.X2 = x;
                    endPt.Y2 = y;

                    var calc = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;

                    if ( calc != null )
                    {
                        x = calc.TransformDataToIndex( x );
                    }

                    projectionPt.X1 = x;
                    projectionPt.Y1 = y;
                    projectionPt.X2 = tuple.Item1;
                    projectionPt.Y2 = tuple.Item2;
                }
            }
            TryUpdate( XAxis.GetCurrentCoordinateCalculator( ), YAxis.GetCurrentCoordinateCalculator( ) );
        }

        public override void SetBasePoint( IComparable x, IComparable y )
        {
            base.SetBasePoint( x, y );

            if ( !IsAttached )
            {
                return;
            }

            if ( _done3Points )
            {
                return;
            }

            if ( Annotations.Count( ) == 3 )
            {
                OnAnnotationCreated( );
                UpdateBasePoint( 2, x, y );
                InitBasePoints( );

                _done3Points = true;
            }
            else if ( Annotations.Count == 1 )
            {
                var firstMovingLines = MovingLinesPartAnnotations.First( );
                firstMovingLines.X2  = x;
                firstMovingLines.Y2  = y;

                var line             = new LineAnnotation( );
                line.X1              = x;
                line.Y1              = y;
                line.X2              = x;
                line.Y2              = y;

                Panel.SetZIndex( line, 1 );
                line.IsEditable      = false;

                SetMovingPart( line, true );
                Annotations.Add( line );

                ICategoryCoordinateCalculator<DateTime> coordinateCalculator = XAxis.GetCurrentCoordinateCalculator( ) as ICategoryCoordinateCalculator<DateTime>;

                if ( coordinateCalculator != null )
                {
                    x = coordinateCalculator.TransformDataToIndex( x );
                }

                if ( _fibTarget != null )
                {
                    var table = new fxFibonacciTableAnnotation( _fibTarget, _gripStyle, ref _lastBar,  _targetBars );
                    table.X1 = x;
                    table.Y1 = y;
                    table.X2 = x;
                    table.Y2 = y;

                    Annotations.Add( table );
                }
                else
                {
                    var table = new fxFibonacciTableAnnotation( FibonacciType.ABCWaveCProjection, _gripStyle, ref _lastBar, _endingY.ToDouble( ) > _startingY.ToDouble( ), _targetBars );
                    table.X1 = x;
                    table.Y1 = y;
                    table.X2 = x;
                    table.Y2 = y;

                    Annotations.Add( table );
                }
                

                
            }
            else
            {
                var line = new LineAnnotation( );
                line.X1 = x;
                line.Y1 = y;
                line.X2 = x;
                line.Y2 = y;
                
                SetMovingPart( line, true );
                Annotations.Add( line );
            }
        }

        private Tuple<IComparable, IComparable> GetProjection( IComparable x, IComparable y )
        {
            Point[ ] basePoints = GetBasePoints( );
            Point begin         = basePoints[ 0 ];
            Point end           = basePoints[ 1 ];
            double deltaX       = ( end.X - begin.X ) * 2;
            double deltaY       = end.Y - begin.Y;

            if ( _endingIndexTime != DateTime.MinValue )
            {
                return new Tuple<IComparable, IComparable>( _endingIndexTime, FromCoordinate( YAxis.GetCoordinate( y ) + deltaY, YAxis ) );
            }

            return new Tuple<IComparable, IComparable>( FromCoordinate( XAxis.GetCoordinate( x ) + deltaX, XAxis ), FromCoordinate( YAxis.GetCoordinate( y ) + deltaY, YAxis ) );
        }

        protected override void SetBasePoint( Point newPoint, int index, IAxis xAxis, IAxis yAxis )
        {
            base.SetBasePoint( newPoint, index, xAxis, yAxis );
            SetBasePoint( FromCoordinate( newPoint.X, xAxis ), FromCoordinate( newPoint.Y, yAxis ), index );
        }

        protected override void SetBasePoint( IComparable xDataValue, IComparable yDataValue, int index )
        {
            IAnnotation[ ] moveLinesPart = MovingLinesPartAnnotations.ToArray( );
            if ( !IsCreated )
            {
                return;
            }
            IAnnotation fibTable = Annotations.Single( a => a is fxFibonacciTableAnnotation );
            int length = moveLinesPart.Length;

            if ( index == 0 )
            {
                moveLinesPart[ index ].X1 = xDataValue;
                moveLinesPart[ index ].Y1 = yDataValue;

                var tuple = GetProjection( fibTable.X1, fibTable.Y1 );
                fibTable.X2 = tuple.Item1;
                fibTable.Y2 = tuple.Item2;
            }
            else if ( index == length )
            {
                moveLinesPart[ index - 1 ].X2 = xDataValue;
                moveLinesPart[ index - 1 ].Y2 = yDataValue;
                fibTable.X1 = xDataValue;
                fibTable.Y1 = yDataValue;

                var tuple = GetProjection( xDataValue, yDataValue );
                fibTable.X2 = tuple.Item1;
                fibTable.Y2 = tuple.Item2;
            }
            else
            {
                moveLinesPart[ index - 1 ].X2 = moveLinesPart[ index ].X1 = xDataValue;
                moveLinesPart[ index - 1 ].Y2 = moveLinesPart[ index ].Y1 = yDataValue;

                var tuple = GetProjection( fibTable.X1, fibTable.Y1 );
                fibTable.X2 = tuple.Item1;
                fibTable.Y2 = tuple.Item2;
            }
        }
    }
}
