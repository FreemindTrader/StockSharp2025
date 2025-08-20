using DevExpress.Mvvm;
using fx.Algorithm;
using fx.Common;
using SciChart.Charting.DrawingTools.TradingAnnotations.Models;
using SciChart.Charting.Numerics.CoordinateCalculators;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Core.Extensions;
using SciChart.Data.Model;
using fx.Definitions;
using StockSharp.BusinessEntities;
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

#pragma warning disable 414

namespace StockSharp.Xaml.Charting.HewFibonacci
{
    /// <summary>
    /// Interaction logic for PivotPointLevelsAnnotation.xaml
    /// </summary>
    public partial class PivotPointLevelsAnnotation : CompositeAnnotation, IComponentConnector, IfxImportantLevel
    {
        protected Style _gripStyle;

        private RangeEx<double> _yRange = null;

        IList< SRlevel > _pivotLevels;

        private SBar _lastBar = default;

        private Security _selectedSecurity = null;

        private TimeSpan _period;

        private bool _hasSelection = false;
        private bool _hasConfluence = false;

        PooledList< double > _selectedLines = new PooledList< double >( );

        public PivotPointLevelsAnnotation( IList<SRlevel> pivotLevels, Style gripStyle, Security selectedSecurity, TimeSpan period )
        {
            InitializeComponent( );

            if ( pivotLevels.Count == 0 )
                return;

            ImportantLines = pivotLevels;

            _selectedSecurity        = selectedSecurity;
            _gripStyle               = gripStyle;
            _pivotLevels             = pivotLevels;
            _period                  = period;

            double minY              = double.MaxValue;
            double maxY              = double.MinValue;

            var firstFib             = CreateSRlevelLine( pivotLevels[ 0 ].SRvalue, GetBrush( pivotLevels[ 0 ] ) );
            firstFib.X1              = pivotLevels[ 0 ].SRtimeBlock.Start;
            firstFib.Y1              = pivotLevels[ 0 ].SRvalue;
            firstFib.X2              = pivotLevels[ 0 ].SRtimeBlock.End;
            firstFib.Y2              = pivotLevels[ 0 ].SRvalue;
            firstFib.StrokeDashArray = GetStrokeDaskArray( pivotLevels[ 0 ] );

            if ( pivotLevels[ 0 ].SRvalue > maxY )
            {
                maxY = pivotLevels[ 0 ].SRvalue;
            }

            if ( pivotLevels[ 0 ].SRvalue < minY )
            {
                minY = pivotLevels[ 0 ].SRvalue;
            }

            Annotations.Add( firstFib );

            var fibText        = new SRlevelTextAnnotation( );
            fibText.Y1         = pivotLevels[ 0 ].SRvalue;
            fibText.Tag        = pivotLevels[ 0 ].SRname;
            fibText.Foreground = GetBrush( pivotLevels[ 0 ] );
            fibText.ParentLine = firstFib;

            Annotations.Add( fibText );

            for ( int index = 1; index < pivotLevels.Count; ++index )
            {
                var ppLine             = CreateSRlevelLine( pivotLevels[ index ].SRvalue, GetBrush( pivotLevels[ index ] ) );
                ppLine.X1              = pivotLevels[ index ].SRtimeBlock.Start;
                ppLine.Y1              = pivotLevels[ index ].SRvalue;
                ppLine.X2              = pivotLevels[ index ].SRtimeBlock.End;
                ppLine.Y2              = pivotLevels[ index ].SRvalue;
                ppLine.StrokeDashArray = GetStrokeDaskArray( pivotLevels[ index ] );

                if ( pivotLevels[ index ].SRvalue > maxY )
                {
                    maxY = pivotLevels[ index ].SRvalue;
                }

                if ( pivotLevels[ index ].SRvalue < minY )
                {
                    minY = pivotLevels[ index ].SRvalue;
                }

                Annotations.Add( ppLine );

                var ppText            = new SRlevelTextAnnotation( );
                ppText.X1             = pivotLevels[ index ].SRtimeBlock.Start;
                ppText.Y1             = pivotLevels[ index ].SRvalue;
                ppText.Tag            = pivotLevels[ index ].SRname;
                ppText.Foreground     = GetBrush( pivotLevels[ index ] );
                ppText.Opacity        = GetOpacity( pivotLevels[ index ] ) ;
                ppText.Text           = string.Format( "{0}", ppText.Tag );
                ppText.ParentLine = ppLine;

                Annotations.Add( ppText );
            }

            _yRange = new RangeEx<double>( minY, maxY );

            IsEnabled = false;

            Messenger.Default.Register<PivotsPointOpacityMessage>( this, x => OnPivotsPointOpacityChanged( x ) );
        }

        private void OnPivotsPointOpacityChanged( PivotsPointOpacityMessage x )
        {
            if ( _selectedSecurity == x.Symbol && _period == x.Period )
            {
                foreach ( IAnnotation annotation in Annotations )
                {
                    var pivotLines = annotation as SRlevelLineAnnotationBase;
                    var pivotText  = annotation as SRlevelTextAnnotation;

                    if ( pivotLines != null )
                    {
                        var fibLvlY = pivotLines.Y1.ToDouble( );

                        if ( _lastBar != SBar.EmptySBar)
                        {
                            if ( X2.ToDouble( ) > ( int )_lastBar.BarIndex )
                            {
                                pivotLines.Opacity = x.Opacity;
                            }
                        }
                    }
                    else if ( pivotText != null )
                    {
                        if ( _lastBar != SBar.EmptySBar)
                        {
                            if ( X2.ToDouble( ) > ( int )_lastBar.BarIndex )
                            {
                                pivotText.Opacity = x.Opacity;
                            }
                        }
                    }
                }
            }            
        }

        
        

        public PooledList<double> GetSelectedLines( )
        {
            return _selectedLines;
        }

        public void UpdateLastX( ref SBar lastBar )
        {
            _lastBar = lastBar;

            double lastY = double.NaN;

            if( _hasSelection )
                return;

            foreach ( IAnnotation annotation in Annotations )
            {
                var fibLine = annotation as SRlevelLineAnnotationBase;
                var fibLabel = annotation as SRlevelTextAnnotation;

                if ( fibLine != null )
                {
                    var fibLvlY = fibLine.Y1.ToDouble( );

                    if ( _lastBar != SBar.EmptySBar)
                    {
                        if ( X2.ToDouble( ) > ( int )_lastBar.BarIndex )
                        {
                            if ( fibLvlY <= _lastBar.High && fibLvlY >= _lastBar.Low )
                            {
                                fibLine.Opacity = 1;
                            }
                            else if ( fibLvlY > _lastBar.High )
                            {
                                var diff = fibLvlY - _lastBar.High;
                                var pip = diff / _lastBar.PointSize;

                                fibLine.Opacity = GetOpacityForPipDistance( pip );
                            }
                            else if ( fibLvlY < _lastBar.Low )
                            {
                                var diff = _lastBar.Low - fibLvlY;

                                var pip = diff / _lastBar.PointSize;

                                fibLine.Opacity = GetOpacityForPipDistance( pip );
                            }
                        }
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

                                fibLabel.Opacity = GetOpacityForPipDistance( pip );
                            }
                            else if ( lastY < _lastBar.Low )
                            {
                                var diff = _lastBar.Low - lastY;
                                var pip = diff / _lastBar.PointSize;

                                fibLabel.Opacity = GetOpacityForPipDistance( pip );
                            }
                        }
                    }
                }
            }
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
            else if ( pip > 50 )
            {
                return 0.1;
            }
            else if ( pip > 20 )
            {
                return 0.2;
            }

            return 0.2;
        }

        

        private DoubleCollection GetStrokeDaskArray( SRlevel sRlevel )
        {
            switch ( sRlevel.SR1stType )
            {
                case SR1stType.MonthPP:
                case SR1stType.WeekPP:
                case SR1stType.DailyPP:
                {
                    switch ( sRlevel.SR2ndType )
                    {
                        case SR2ndType.HalfResistance:
                        {
                            return new DoubleCollection( ) { 1, 6 };
                        }

                        case SR2ndType.Resistance:
                        {
                            return new DoubleCollection( ) { 2, 6 };
                        }

                        case SR2ndType.HalfSupport:
                        {
                            return new DoubleCollection( ) { 1, 6 };
                        }

                        case SR2ndType.Support:
                        {
                            return new DoubleCollection( ) { 2, 6 };
                        }

                        case SR2ndType.Y:
                        {
                            return new DoubleCollection( ) { 3, 6 };
                        }

                        case SR2ndType.DirectionNo:
                        {
                            return new DoubleCollection( ) { 3, 6 };
                        }
                    }
                }
                break;
            }

            return new DoubleCollection( ) { 2, 4 };
        }

        private static Brush GetBrush( SRlevel sRlevel )
        {
            switch ( sRlevel.SR1stType  )
            {                
                case SR1stType.MonthPP:
                case SR1stType.WeekPP:
                case SR1stType.DailyPP:
                {
                    switch ( sRlevel.SR2ndType )
                    {
                        case SR2ndType.HalfResistance:
                        {
                            return HalfResistanceColor;
                        }                        

                        case SR2ndType.Resistance:
                        {
                            return ResistanceColor;
                        }                        

                        case SR2ndType.HalfSupport:
                        {
                            return HalfSupportLineColor;
                        }                        

                        case SR2ndType.Support:
                        {
                            return SupportLineColor;
                        }
                        
                        case SR2ndType.Y:
                        {
                            return MeanLineColor;
                        }                        

                        case SR2ndType.DirectionNo:
                        {
                            return DirectionNumberColor;
                        }                        
                    }
                }
                break;              
            }

            return BaseColor;
        } 
        
        private double GetOpacity( SRlevel sRlevel )
        {
            switch ( sRlevel.SR1stType )
            {
                case SR1stType.MonthPP:
                case SR1stType.WeekPP:
                case SR1stType.DailyPP:
                {
                    switch ( sRlevel.SR2ndType )
                    {
                        case SR2ndType.HalfResistance:
                        {
                            return 0.5;
                        }

                        case SR2ndType.Resistance:
                        {
                            return 0.7;
                        }

                        case SR2ndType.HalfSupport:
                        {
                            return 0.5;
                        }

                        case SR2ndType.Support:
                        {
                            return 0.7;
                        }

                        case SR2ndType.Y:
                        {
                            return 1;
                        }

                        case SR2ndType.DirectionNo:
                        {
                            return 1;
                        }
                    }
                }
                break;
            }
            return 0.1;
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

        private SRlevelLineAnnotation CreateSRlevelLine( double fibValue, Brush fibColor )
        {
            var ratioLine                = new SRlevelLineAnnotation( );
            ratioLine.DataContext        = new SRlevelLineAnnotationViewModel( fibValue, fibColor );
            ratioLine.IsEditable         = true;
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


        public static readonly SolidColorBrush BaseColor            = new SolidColorBrush( Color.FromArgb( byte.MaxValue, 119, 119, 135 ) );
        public static readonly SolidColorBrush HalfResistanceColor  = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush ResistanceColor      = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush HalfSupportLineColor = new SolidColorBrush( Colors.Green );
        public static readonly SolidColorBrush SupportLineColor     = new SolidColorBrush( Colors.Green );
        public static readonly SolidColorBrush MeanLineColor        = new SolidColorBrush( Colors.Black );
        public static readonly SolidColorBrush DirectionNumberColor = new SolidColorBrush( Colors.Blue );

        public void HighlightConfluence( double highlightedFibLine )
        {
            SRlevelLineAnnotationBase selected = null;


            foreach ( IAnnotation annotation in Annotations )
            {
                var pivotLine = annotation as SRlevelLineAnnotationBase;
                var pivotText = annotation as SRlevelTextAnnotation;

                if ( pivotLine != null )
                {
                    var fibLvlY = pivotLine.Y1.ToDouble();

                    if ( Math.Abs( highlightedFibLine - fibLvlY ) < 0.00001 )
                    {
                        pivotLine.Opacity = 0.99934;
                        selected          = pivotLine;
                        _hasConfluence    = true;
                    }
                    else
                    {
                        if ( pivotLine.Opacity != 0.99934 )
                        {
                            pivotLine.Opacity = 0.02;
                        }
                    }
                }
                else if ( pivotText != null )
                {
                    if ( selected != null && pivotText.ParentLine == selected )
                    {
                        pivotText.Opacity = 0.99934;
                    }
                    else
                    {
                        if ( pivotText.Opacity != 0.99934 )
                        {
                            pivotText.Opacity = 0.02;
                        }

                    }
                }
            }
        }

        public void HighlightComingSRLines( ref SBar bar, double highlightedFibLine )
        {
            HighlightConfluence( highlightedFibLine );
        }

        public void DimAllImportantLines( )
        {
            foreach ( IAnnotation annotation in Annotations )
            {
                var pivotLine = annotation as SRlevelLineAnnotationBase;
                var pivotText = annotation as SRlevelTextAnnotation;

                if ( pivotLine != null )
                {
                    pivotLine.Opacity = 0.02;
                }
                else if ( pivotText != null )
                {
                    pivotText.Opacity = 0.02;
                }
            }
        }

        public PooledList<double> HighlightedSelected( bool CTRLKEY, IRange xRange, PooledDictionary<string, IRange> yRange )
        {
            if ( ! CTRLKEY )
            {
                _selectedLines.Clear( );
            }
            

            var myYRange = yRange.First( ).Value;

            if ( _yRange.Contains( myYRange.Min.ToDouble( ) ) || _yRange.Contains( myYRange.Max.ToDouble( ) ) )
            {
                RangeEx<DateTime> selectRange = new RangeEx<DateTime>(xRange.Min.ToDateTime(), xRange.Max.ToDateTime());

                double lastY = double.NaN;

                foreach ( IAnnotation annotation in Annotations )
                {
                    var pivotLine = annotation as SRlevelLineAnnotation;
                    var pivotText = annotation as SRlevelTextAnnotation;

                    if ( pivotLine != null )
                    {
                        var fibLvlY = pivotLine.Y1.ToDouble();

                        if ( myYRange.IsValueWithinRange( pivotLine.Y1 ) )
                        {
                            var xBegin      = XAxis.GetCoordinate(X1);
                            var xEnd        = XAxis.GetCoordinate(X2);
                            var xBeginValue = XAxis.GetDataValue(xBegin).ToDateTime();
                            var xEndValue   = XAxis.GetDataValue(xEnd).ToDateTime();

                            RangeEx<DateTime> lineRange = new RangeEx<DateTime>(xBeginValue < xEndValue ? xBeginValue : xEndValue  , xBeginValue < xEndValue ? xEndValue  : xBeginValue );

                            if ( selectRange.Overlaps( lineRange ) )
                            {
                                _selectedLines.Add( fibLvlY );
                                pivotLine.Opacity = 1;
                                _hasSelection = true;
                                _lastBar = default;
                            }
                        }
                        else
                        {
                            pivotLine.Opacity = 0.1;
                        }

                        lastY = fibLvlY;

                    }
                    else if ( pivotText != null )
                    {
                        pivotText.X1 = X1.ToDouble( ) > X2.ToDouble( ) ? X2 : X1;
                        pivotText.Y1 = lastY;

                        if ( myYRange.IsValueWithinRange( lastY ) )
                        {
                            pivotText.Opacity = 1;
                        }
                        else
                        {
                            pivotText.Opacity = 0.1;
                        }
                    }
                }
            }
            else
            {
                _hasSelection = false;
            }

            return _selectedLines;
        }

        public void HighlightSingleConfluence( double highlightedPivotLine )
        {
            SRlevelLineAnnotationBase selected = null;


            foreach ( IAnnotation annotation in Annotations )
            {
                var pivotLine = annotation as SRlevelLineAnnotationBase;
                var pivotText = annotation as SRlevelTextAnnotation;

                if ( pivotLine != null )
                {
                    var fibLvlY = pivotLine.Y1.ToDouble();

                    if ( Math.Abs( highlightedPivotLine - fibLvlY ) < 0.00001 )
                    {
                        pivotLine.Opacity = 0.99995;
                        selected          = pivotLine;
                        _hasConfluence    = true;
                    }
                    else
                    {
                        pivotLine.Opacity = 0.02;
                    }
                }
                else if ( pivotText != null )
                {
                    if ( pivotText.ParentLine == selected )
                    {
                        pivotText.CoordinateMode = AnnotationCoordinateMode.RelativeX;
                        pivotText.X1             = 0.9;
                        pivotText.Opacity        = 0.99995;
                    }
                    else
                    {
                        pivotText.Opacity = 0.02;

                    }
                }
            }
        }

        private IList< SRlevel > _pivotPointLevels;

        public IList<SRlevel> ImportantLines
        {
            get
            {
                return _pivotPointLevels;
            }

            set
            {
                _pivotPointLevels = value;
            }
        }

        public Guid LineGuid { get; set; }

        public bool IsLocked { get; set; }

        public TrendDirection Trend { get;  }

        public DateTime EndingTime { get; }
    }
}



