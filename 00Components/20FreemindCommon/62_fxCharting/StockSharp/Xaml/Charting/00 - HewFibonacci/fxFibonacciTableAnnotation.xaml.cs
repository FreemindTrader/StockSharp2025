using fx.Algorithm;
using SciChart.Charting.DrawingTools.TradingAnnotations.Models;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Core.Extensions;
using SciChart.Data.Model;
using fx.Definitions;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using fx.Bars;

namespace StockSharp.Xaml.Charting.HewFibonacci
{
    /// <summary>
    /// Interaction logic for fxFibonacciTableAnnotation.xaml
    /// </summary>
    public partial class fxFibonacciTableAnnotation : CompositeAnnotation, IComponentConnector
    {
        private RangeEx<double> _yRange = null;
        protected Style         _gripStyle;
        private SBar          _lastBar = default;
        private bool            _isUp = false;

        private bool            _hasSelection = false;
        private bool            _hasConfluence = false;

        PooledList<SBar>            _targetBars = new PooledList<SBar>( );

        public fxFibonacciTableAnnotation( HewFibGannTargets fibTarget, Style gripStyle, ref SBar lastBar, PooledList<SBar> targetPoints )
        {
            InitializeComponent( );

            _gripStyle  = gripStyle;

            _lastBar    = lastBar;

            _targetBars = targetPoints;

            _isUp       = fibTarget != null ? fibTarget.IsUptrend: false;

            var fibRatios = fibTarget.GetFibLevels();

            if ( fibRatios == null )
                return;

            Annotations.Add( CreateFibRatioLine( fibRatios[ 0 ].Value, fibRatios[ 0 ].Brush ) );

            var fibText        = new SRlevelTextAnnotation( );
            fibText.Y1         = fibRatios[ 0 ].Value;
            fibText.Tag        = fibRatios[ 0 ].Value;
            fibText.Foreground = fibRatios[ 0 ].Brush;

            Annotations.Add( fibText );

            for ( int index = 1; index < fibRatios.Count; ++index )
            {
                Annotations.Add( CreateFibRatioLine( fibRatios[ index ].Value, fibRatios[ index ].Brush ) );

                var fibText2        = new SRlevelTextAnnotation( );
                fibText2.Y1         = fibRatios[ index ].Value;
                fibText2.Tag        = fibRatios[ 0 ].Value;
                fibText2.Foreground = fibRatios[ index ].Brush;

                Annotations.Add( fibText2 );
            }           

            IsEnabled = true;
        }

        public fxFibonacciTableAnnotation( HewFibGannTargets fibTarget, Style gripStyle, ref SBar lastBar, PooledList<SBar> targetPoints, bool useTony )
        {
            InitializeComponent( );

            _gripStyle    = gripStyle;

            _lastBar      = lastBar;

            _targetBars   = targetPoints;

            _isUp         = fibTarget.IsUptrend;

            var fibRatios = fibTarget.GetTonyLevels( );

            if ( fibRatios == null )
                return;

            Annotations.Add( CreateFibRatioLine( fibRatios[ 0 ].Value, fibRatios[ 0 ].Brush ) );

            var fibText        = new SRlevelTextAnnotation( );
            fibText.Y1         = fibRatios[ 0 ].Value;
            fibText.Tag        = fibRatios[ 0 ].Value;
            fibText.Foreground = fibRatios[ 0 ].Brush;

            Annotations.Add( fibText );

            for ( int index = 1; index < fibRatios.Count; ++index )
            {
                Annotations.Add( CreateFibRatioLine( fibRatios[ index ].Value, fibRatios[ index ].Brush ) );

                var fibText2        = new SRlevelTextAnnotation( );
                fibText2.Y1         = fibRatios[ index ].Value;
                fibText2.Tag        = fibRatios[ 0 ].Value;
                fibText2.Foreground = fibRatios[ index ].Brush;

                Annotations.Add( fibText2 );
            }

            IsEnabled = false;
        }


        public fxFibonacciTableAnnotation( FibonacciType fibType, Style gripStyle, ref SBar lastBar, bool isUp, PooledList<SBar> targetPoints )
        {
            InitializeComponent( );

            _gripStyle    = gripStyle;

            _lastBar      = lastBar;

            _targetBars = targetPoints;

            _isUp         = isUp;

            var fibRatios = HewFibGannTargets.GetFibLevels( fibType );

            if ( fibRatios == null )
                return;            

            Annotations.Add( CreateFibRatioLine( fibRatios[ 0 ].Value, fibRatios[ 0 ].Brush ) );

            var fibText        = new SRlevelTextAnnotation( );
            fibText.Y1         = fibRatios[ 0 ].Value;
            fibText.Tag        = fibRatios[ 0 ].Value;
            fibText.Foreground = fibRatios[ 0 ].Brush;

            Annotations.Add( fibText );            

            for ( int index = 1; index < fibRatios.Count; ++index )
            {
                Annotations.Add( CreateFibRatioLine( fibRatios[ index ].Value, fibRatios[ index ].Brush ) );

                var fibText2        = new SRlevelTextAnnotation( );
                fibText2.Y1         = fibRatios[ index ].Value;
                fibText2.Tag        = fibRatios[ 0 ].Value;
                fibText2.Foreground = fibRatios[ index ].Brush;                

                Annotations.Add( fibText2 );
            }
           
            IsEnabled = false;
        }

        public PooledList< double > HighLightSelected( bool CTRLKEY, IRange xRange, PooledDictionary<string, IRange> yRange)
        {
            var output = new PooledList< double >( );

            if ( _yRange == null )
            {
                return output;
            }
            
            var myYRange = yRange.First( ).Value;                        

            if ( _yRange.Contains( myYRange.Min.ToDouble() ) || _yRange.Contains( myYRange.Max.ToDouble()  ) )
            {
                RangeEx<DateTime> selectRange = new RangeEx<DateTime>(xRange.Min.ToDateTime(), xRange.Max.ToDateTime());

                double lastY = double.NaN;

                foreach (IAnnotation annotation in Annotations)
                {
                    var fibLine = annotation as SRlevelLineAnnotationBase;
                    var fibLabel = annotation as SRlevelTextAnnotation;

                    if (fibLine != null)
                    {
                        var fibLvlY = fibLine.Y1.ToDouble();

                        if (myYRange.IsValueWithinRange(fibLine.Y1))
                        {
                            var xBegin      = XAxis.GetCoordinate(X1);
                            var xEnd        = XAxis.GetCoordinate(X2);
                            var xBeginValue = XAxis.GetDataValue(xBegin).ToDateTime();
                            var xEndValue   = XAxis.GetDataValue(xEnd).ToDateTime();

                            RangeEx<DateTime> lineRange = new RangeEx<DateTime>(xBeginValue < xEndValue ? xBeginValue : xEndValue  , xBeginValue < xEndValue ? xEndValue  : xBeginValue );

                            if (selectRange.Overlaps(lineRange))
                            {
                                output.Add( fibLvlY );

                                fibLine.Opacity      = 1;
                                fibLine.LineSelected = true;
                                _lastBar             = default;
                                _hasSelection        = true;
                            }
                        }
                        else
                        {
                            if ( ! CTRLKEY ) fibLine.Opacity = 0.02;
                        }

                        lastY = fibLvlY;

                    }
                    else if (fibLabel != null)
                    {
                        fibLabel.X1 = X1.ToDouble() > X2.ToDouble() ? X2 : X1;
                        fibLabel.Y1 = lastY;

                        if (myYRange.IsValueWithinRange( lastY ))
                        {
                            fibLabel.Opacity = 1;
                            fibLabel.LineSelected = true;
                        }
                        else
                        {
                            if ( !CTRLKEY ) fibLabel.Opacity = 0.02;
                        }
                    }
                }
            }
            else
            {
                _hasSelection = false;
            }

            return output;
        }

        public void HighlightComingSRLines( ref SBar bar, double highlightedFibLine )
        {
            SRlevelLineAnnotationBase selected = null;

            double lastOpacity = 0.0;

            foreach ( IAnnotation annotation in Annotations )
            {
                var fibLine  = annotation as SRlevelLineAnnotationBase;
                var fibLabel = annotation as SRlevelTextAnnotation;

                if ( fibLine != null )
                {
                    var fibLvlY = fibLine.Y1.ToDouble();

                    if ( Math.Abs( highlightedFibLine - fibLvlY ) < 0.00001 )
                    {
                        if ( fibLvlY <= bar.High && fibLvlY >= bar.Low )
                        {
                            fibLine.Opacity = 1;
                        }
                        else if ( fibLvlY > bar.High )
                        {
                            var diff = fibLvlY - bar.High;
                            var pip  = diff / bar.PointSize;

                            lastOpacity = GetOpacityForSRLine( pip ); 

                            fibLine.Opacity = GetOpacityForSRLine( pip );
                        }
                        else if ( fibLvlY < bar.Low )
                        {
                            var diff = bar.Low - fibLvlY;

                            var pip = diff / bar.PointSize;

                            lastOpacity = GetOpacityForSRLine( pip );

                            fibLine.Opacity = GetOpacityForSRLine( pip );
                        }

                        selected = fibLine;
                        _hasConfluence = true;
                    }                    
                }
                else if ( fibLabel != null )
                {
                    if ( selected != null && fibLabel.ParentLine == selected )
                    {
                        fibLabel.Opacity = lastOpacity;
                    }                    
                }
            }
        }

        public void HighlightConfluence( double highlightedFibLine )
        {
            SRlevelLineAnnotationBase selected = null;


            foreach ( IAnnotation annotation in Annotations )
            {
                var fibLine = annotation as SRlevelLineAnnotationBase;
                var fibText = annotation as SRlevelTextAnnotation;

                if ( fibLine != null )
                {
                    var fibLvlY = fibLine.Y1.ToDouble();

                    if ( Math.Abs( highlightedFibLine - fibLvlY ) < 0.00001 )
                    {
                        fibLine.Opacity = 0.99934;
                        selected = fibLine;
                        _hasConfluence = true;
                    }                                  
                }
                else if ( fibText != null )
                {
                    if ( selected != null && fibText.ParentLine == selected )
                    {
                        fibText.Opacity = 0.99934;
                    }                                      
                }
            }
        }

        public void DimAllImportantLines()
        {
            foreach ( IAnnotation annotation in Annotations )
            {
                var fibLine = annotation as SRlevelLineAnnotationBase;
                var fibLabel = annotation as SRlevelTextAnnotation;

                if ( fibLine != null )
                {
                    if ( ! fibLine.LineSelected )
                    {
                        fibLine.Opacity = 0.02;
                    }                    
                }
                else if ( fibLabel != null )
                {
                    if ( !fibLabel.LineSelected )
                    {
                        fibLabel.Opacity = 0.02;
                    }                    
                }
            }
        }

        public void HighlightSingleConfluence( double highlightedFibLine )
        {
            SRlevelLineAnnotationBase selected = null;


            foreach ( IAnnotation annotation in Annotations )
            {
                var fibLine = annotation as SRlevelLineAnnotationBase;
                var fibLabel = annotation as SRlevelTextAnnotation;

                if ( fibLine != null )
                {
                    var fibLvlY = fibLine.Y1.ToDouble();

                    if ( Math.Abs( highlightedFibLine - fibLvlY ) < 0.00001 )
                    {
                        fibLine.Opacity = 0.99995;
                        selected = fibLine;
                        _hasConfluence = true;
                    }
                    else
                    {
                        fibLine.Opacity = 0.02;
                    }
                }
                else if ( fibLabel != null )
                {
                    if ( selected != null &&  fibLabel.ParentLine == selected )
                    {
                        fibLabel.CoordinateMode = AnnotationCoordinateMode.RelativeX;
                        fibLabel.X1 = 0.9;
                        fibLabel.Opacity = 0.99995;
                    }
                    else
                    {
                        fibLabel.Opacity = 0.02;

                    }
                }
            }
        }


        



        public void UpdateLineOpacity( ref SBar lastBar )
        {
            _lastBar = lastBar;

            double lastY = double.NaN;

            if( _hasSelection )
                return;

            if( _hasConfluence )
                return;

            foreach ( IAnnotation annotation in Annotations )
            {
                var fibLine  = annotation as SRlevelLineAnnotationBase;
                var fibLabel = annotation as SRlevelTextAnnotation;

                if ( fibLine != null )
                {
                    var fibLvlY = fibLine.Y1.ToDouble();
                    
                    if ( _lastBar != SBar.EmptySBar)
                    {
                        var xBegin      = XAxis.GetCoordinate(fibLine.X1);
                        var xEnd        = XAxis.GetCoordinate(fibLine.X2);
                        var xBeginValue = XAxis.GetDataValue(xBegin).ToDateTime();
                        var xEndValue   = XAxis.GetDataValue(xEnd).ToDateTime();

                        if (xEndValue > _lastBar.BarTime )
                        {
                            if ( fibLvlY <= _lastBar.High && fibLvlY >= _lastBar.Low )
                            {
                                fibLine.Opacity = 1;
                            }
                            else if ( fibLvlY > _lastBar.High )
                            {
                                var diff = fibLvlY - _lastBar.High;
                                var pip  = diff / _lastBar.PointSize;

                                if ( _isUp )
                                {
                                    fibLine.Opacity = 0.6;
                                }
                                else
                                {
                                    fibLine.Opacity = GetOpacityForPipDistance( pip );
                                }

                            }
                            else if ( fibLvlY < _lastBar.Low )
                            {
                                var diff = _lastBar.Low - fibLvlY;

                                var pip = diff / _lastBar.PointSize;

                                if ( _isUp )
                                {
                                    fibLine.Opacity = GetOpacityForPipDistance( pip );
                                }
                                else
                                {
                                    fibLine.Opacity = 0.6;
                                }

                            }
                        }
                    }
                    else
                    {
                        fibLine.Opacity = 0.8;
                    }

                    lastY = fibLvlY;
                }
                else if ( fibLabel != null )
                {                    
                    if ( _lastBar != SBar.EmptySBar)
                    {
                        if ( X2.ToDouble( ) > ( int )_lastBar.BarIndex )
                        {
                            if ( lastY <= _lastBar.High && lastY >= _lastBar.Low )
                            {
                                fibLabel.Opacity = 1;
                            }
                            else if ( lastY > _lastBar.High )
                            {
                                var diff = lastY - _lastBar.High;
                                var pip = diff / _lastBar.PointSize;

                                if ( _isUp )
                                {
                                    fibLabel.Opacity = 0.6;
                                }
                                else
                                {
                                    fibLabel.Opacity = GetOpacityForPipDistance( pip );
                                }

                            }
                            else if ( lastY < _lastBar.Low )
                            {
                                var diff = _lastBar.Low - lastY;
                                var pip = diff / _lastBar.PointSize;

                                if ( _isUp )
                                {
                                    fibLabel.Opacity = GetOpacityForPipDistance( pip );
                                }
                                else
                                {
                                    fibLabel.Opacity = 0.6;
                                }
                            }
                        }
                    }                    
                }
            }
        }

        public override void Update( ICoordinateCalculator<double> xCoordinateCalculator, ICoordinateCalculator<double> yCoordinateCalculator )
        {
            base.Update( xCoordinateCalculator, yCoordinateCalculator );            
            
            if ( _hasSelection )
                return;

            if ( _hasConfluence )
                return;

            if ( _targetBars.Count > 0 )
            {
                foreach ( SBar bar in _targetBars )
                {
                    if ( bar.IsWavePeak( ) || bar.IsGannPeak() )
                    {
                        DrawAnnotations( bar.High );
                    }
                    else if ( bar.IsWaveTrough( ) || bar.IsGannTrough() )
                    {
                        DrawAnnotations( bar.Low );
                    }
                    else
                    {
                        DrawAnnotations( bar.Close );
                    }
                }
            }    
            else
            {
                DrawAnnotations( );
            }
        }

        private void DrawAnnotations(  )
        {
            double yDelta = Y2.ToDouble( ) - Y1.ToDouble( );

            double lineLvl = double.NaN;
            double lastY = double.NaN;

            double minY = double.MaxValue;
            double maxY = double.MinValue;

            SRlevelLineAnnotationBase lastFib = null;


            foreach ( IAnnotation annotation in Annotations )
            {
                var fibLine = annotation as SRlevelLineAnnotationBase;
                var fibLabel = annotation as SRlevelTextAnnotation;

                if ( fibLine != null )
                {
                    lineLvl = fibLine.LineLevel;
                    var fibValue = yDelta * lineLvl;
                    var fibLvlY = Y1.ToDouble( ) + ( fibValue.IsNaN( ) ? 0.0 : fibValue );

                    if ( fibLvlY > maxY )
                    {
                        maxY = fibLvlY;
                    }

                    if ( fibLvlY < minY )
                    {
                        minY = fibLvlY;
                    }                    

                    fibLine.Y1 = fibLvlY;
                    fibLine.Y2 = fibLvlY;

                    IComparable smaller = X1;
                    IComparable larger = X2;

                    if ( X1 is int && X2 is int )
                    {
                        var x1Value = ( int ) X1;
                        var x2Value = ( int ) X2;

                        if ( x1Value > x2Value )
                        {
                            smaller = X2;
                            larger = X1;
                        }
                        else
                        {
                            smaller = X1;
                            larger = X2;
                        }

                    }
                    else if ( X1 is TimeSpan )
                    {
                        var x1Value = ( TimeSpan ) X1;
                        var x2Value = ( TimeSpan ) X2;

                        if ( x1Value > x2Value )
                        {
                            smaller = X2;
                            larger = X1;
                        }
                        else
                        {
                            smaller = X1;
                            larger = X2;
                        }
                    }


                    fibLine.X1 = smaller;
                    fibLine.X2 = larger;

                    var xIndex = larger.ToDouble( );

                    if ( _lastBar != SBar.EmptySBar)
                    {
                        if ( xIndex > ( int ) _lastBar.BarIndex )
                        {
                            if ( fibLvlY <= _lastBar.High && fibLvlY >= _lastBar.Low )
                            {
                                fibLine.Opacity = 1;
                            }
                            else if ( fibLvlY > _lastBar.High )
                            {
                                var diff = fibLvlY - _lastBar.High;
                                var pip = diff / _lastBar.PointSize;

                                if ( _isUp )
                                {
                                    fibLine.Opacity = 0.6;
                                }
                                else
                                {
                                    fibLine.Opacity = GetOpacityForPipDistance( pip );
                                }

                            }
                            else if ( fibLvlY < _lastBar.Low )
                            {
                                var diff = _lastBar.Low - fibLvlY;

                                var pip = diff / _lastBar.PointSize;

                                if ( _isUp )
                                {
                                    fibLine.Opacity = GetOpacityForPipDistance( pip );
                                }
                                else
                                {
                                    fibLine.Opacity = 0.6;
                                }

                            }
                        }
                        else
                        {
                            /* -------------------------------------------------------------------------------------------------------------------------------------------
                             * 
                             *  The reason I did this is to turn off all those Table annotation that is not projected into the future 
                             *  as the latest wave is not set yet.
                             * 
                             * ------------------------------------------------------------------------------------------------------------------------------------------- */

                            if ( fibLine.Opacity != 0.99 )
                            {
                                //fibLine.Opacity = 0.02;
                            }
                        }
                    }

                    lastY = fibLvlY;
                    lastFib = fibLine;

                    _yRange = new RangeEx<double>( minY, maxY );
                }
                else if ( fibLabel != null )
                {
                    fibLabel.X1 = X1.ToDouble( ) > X2.ToDouble( ) ? X2 : X1;
                    fibLabel.Y1 = lastY;
                    fibLabel.ParentLine = lastFib;

                    if ( _lastBar != SBar.EmptySBar)
                    {
                        if ( X2.ToDouble( ) > ( int ) _lastBar.BarIndex )
                        {
                            if ( lastY <= _lastBar.High && lastY >= _lastBar.Low )
                            {
                                fibLabel.Opacity = 1;
                            }
                            else if ( lastY > _lastBar.High )
                            {
                                var diff = lastY - _lastBar.High;
                                var pip = diff / _lastBar.PointSize;

                                if ( _isUp )
                                {
                                    fibLabel.Opacity = 0.6;
                                }
                                else
                                {
                                    fibLabel.Opacity = GetOpacityForPipDistance( pip );
                                }

                            }
                            else if ( lastY < _lastBar.Low )
                            {
                                var diff = _lastBar.Low - lastY;
                                var pip = diff / _lastBar.PointSize;

                                if ( _isUp )
                                {
                                    fibLabel.Opacity = GetOpacityForPipDistance( pip );
                                }
                                else
                                {
                                    fibLabel.Opacity = 0.6;
                                }


                            }
                        }
                    }

                    fibLabel.Text = string.Format( "{0:#0.##%} ({1:N4})", lineLvl, lastY );
                }


            }            
        }

        private void DrawAnnotations( double lineValue )
        {
            double yDelta = Y2.ToDouble( ) - Y1.ToDouble( );

            double lineLvl = double.NaN;
            double lastY = double.NaN;

            double minY = double.MaxValue;
            double maxY = double.MinValue;

            SRlevelLineAnnotationBase closestLine = null;
            SRlevelTextAnnotation closestText = null;
            double minDiff = double.MaxValue;
            
            SRlevelLineAnnotationBase lastFib = null;

            foreach ( IAnnotation annotation in Annotations )
            {
                var fibLine = annotation as SRlevelLineAnnotationBase;
                var fibLabel = annotation as SRlevelTextAnnotation;

                if ( fibLine != null )
                {
                    lineLvl = fibLine.LineLevel;

                    if( lineLvl == 0 )
                        continue;

                    var fibValue = yDelta * lineLvl;
                    var fibLvlY = Y1.ToDouble( ) + ( fibValue.IsNaN( ) ? 0.0 : fibValue );

                    if ( fibLvlY > maxY )
                    {
                        maxY = fibLvlY;
                    }

                    if ( fibLvlY < minY )
                    {
                        minY = fibLvlY;
                    }

                    if ( Math.Abs( fibLvlY - lineValue ) < minDiff )
                    {
                        closestLine = fibLine;
                        minDiff = Math.Abs( fibLvlY - lineValue );
                    }

                    fibLine.Y1 = fibLvlY;
                    fibLine.Y2 = fibLvlY;
                    fibLine.X1 = X1;
                    fibLine.X2 = X2;

                    if ( fibLine.Opacity != 0.99 )
                    {
                        //fibLine.Opacity = 0.1;
                    }

                    lastY = fibLvlY;
                    lastFib = fibLine;

                    _yRange = new RangeEx<double>( minY, maxY );
                }
                else if ( fibLabel != null )
                {
                    if( lastY.IsNaN() )
                        continue;

                    fibLabel.X1        = X1.ToDouble( ) > X2.ToDouble( ) ? X2 : X1;
                    fibLabel.Y1        = lastY;
                    fibLabel.ParentLine = lastFib;

                    if ( fibLabel.Opacity != 0.99 )
                    {
                        //fibLabel.Opacity = 0.1;
                    }
                    

                    if ( Math.Abs( fibLabel.Y1.ToDouble() - closestLine.Y1.ToDouble( ) ) < 0.00000001 )
                    {
                        closestText = fibLabel;
                    }

                    

                    fibLabel.Text = string.Format( "{0:#0.##%} ({1:N4})", lineLvl, lastY );
                }

                
            }

            if ( closestLine != null )
            {
                closestLine.Opacity = 0.99;
                closestText.Opacity = 0.99;
            }
        }

        private double GetOpacityForSRLine( double pip )
        {
            if ( pip < 10 )
            {
                return 1;
            }
            else if ( pip < 20 )
            {
                return 0.2;
            }
            else if ( pip > 20 )
            {
                return 0.1;
            }

            return 0.1;
        }

        private double GetOpacityForPipDistance( double pip )
        {
            if ( pip < 10 )
            {
                return 1;
            }
            else if ( pip < 20 )
            {
                return 0.5;
            }
            else if ( pip > 20 )
            {
                //return 0.12;
            }

            return 1;
        }

        public void MakeRatioLineSelected( bool isSelected )
        {
            foreach ( IAnnotation annotation in Annotations )
            {
                var ratio = annotation as SRlevelLineAnnotationBase;
                var fibonacciTextLevel = annotation as SRlevelTextAnnotation;

                if ( ratio != null )
                {
                    ratio.IsSelected = isSelected;
                }                
            }
        }

        protected override Point[ ] GetBasePoints( AnnotationCoordinates coordinates )
        {
            PooledList<Point> list = ( ( IEnumerable<Point> )base.GetBasePoints( coordinates ) ).ToPooledList( );
            list.Clear( );
            return list.ToArray( );
        }

        private IAnnotation CreateFibRatioLine( double fibValue, Brush fibColor )
        {
            var ratioLine                = new SRlevelLineAnnotation( );
            ratioLine.DataContext        = new SRlevelLineAnnotationViewModel( fibValue, fibColor );
            ratioLine.IsEditable         = false;
            ratioLine.ResizingGripsStyle = _gripStyle;

            return ratioLine;
        }

        protected override void AttachAnnotation( IAnnotation item )
        {
            base.AttachAnnotation( item );
            TextAnnotation textAnnotation = item as TextAnnotation;
            if ( textAnnotation == null )
            {
                return;
            }
            Binding binding = new Binding( ) { Source = ( this ), Path = new PropertyPath( FontSizeProperty ) };
            textAnnotation.SetBinding( FontSizeProperty, binding );
        }


        

        //public static RatioModel[ ] GetFibLevels( FibonacciType fibType )
        //{
        //    switch ( fibType )
        //    {
        //        case FibonacciType.Wave2Retracement:
        //        {
        //            return Wave2RetracementLevelsRatio;
        //        }

        //        case FibonacciType.Wave4Retracement:
        //        {
        //            return Wave4RetracementLevelsRatio;
        //        }

        //        case FibonacciType.ABCWaveBRetracement:
        //        {
        //            return ABCWaveBRetracementLevelsRatio;
        //        }

        //        case FibonacciType.Wave3Projection:
        //        {
        //            return Wave3ProjectionLevels;
        //        }

        //        case FibonacciType.Wave3CProjection:
        //        {
        //            return Wave3CProjectionLevels;
        //        }

        //        case FibonacciType.Wave5Projection:
        //        {
        //            return Wave5ProjectionLevels;
        //        }

        //        case FibonacciType.ABCWaveCProjection:
        //        {
        //            return ABCWaveCProjectionLevels;
        //        }

        //        case FibonacciType.Wave5CProjection:
        //        {
        //            return Wave5CProjectionLevels;
        //        }

        //        case FibonacciType.WaveEFBRetracement:
        //        {
        //            return WaveEFBRetracementLevels;
        //        }

        //        case FibonacciType.WaveTriBRetracement:
        //        {
        //            return WaveTriBRetracementLevels;
        //        }

        //        case FibonacciType.WaveTriCProjection:
        //        {
        //            return WaveTriCRetracementLevels;
        //        }
        //        
        //        case FibonacciType.WaveTriDProjection:
        //        {
        //            return WaveTriDRetracementLevels;
        //        }
        //        
        //        case FibonacciType.WaveTriEProjection:
        //        {
        //            return WaveTriERetracementLevels;
        //        }
        //        
        //        case FibonacciType.FirstXProjection:
        //        {
        //            return FirstXProjectionLevels;
        //        }
        //        
        //        case FibonacciType.SecondXProjection:
        //        {
        //            return SecondXProjectionLevels;
        //        }
        //        

        //        case FibonacciType.WaveCProjection:
        //        {
        //            return ABCWaveCProjectionLevels;
        //        }                


        //    }

        //    return null;
        //}
        
    }
}
