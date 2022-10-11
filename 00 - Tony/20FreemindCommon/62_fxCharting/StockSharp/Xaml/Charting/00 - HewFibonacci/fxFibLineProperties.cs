using fx.Charting.HewFibonacci;
using fx.Collections;
using fx.Common;
using fx.Definitions;
using SciChart.Charting.Visuals.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace fx.Charting
{
    public class fxFibLineProperties
    {
        public fxFibLineProperties( fxFibLevel[ ] fibLevels, Style gripStyle )
        {
            if ( fibLevels.Length > 0  )
            {
                GripStyle = gripStyle;
                CreateFibAnnotation( fibLevels );
            }
        }

        public fxFibLineProperties( PooledList<fxFibLevelCluster> fibLevels, Style gripStyle )
        {
            if ( fibLevels.Count > 0 )
            {
                GripStyle = gripStyle;
                CreateFibAnnotation( fibLevels );
            }
        }

        


        public Style GripStyle
        {
            get { return _gripStyle; }
            set
            {
                _gripStyle = value;
            }
        }
        


        public AnnotationCollection FibTableAnnotation
        {
            get { return _fibTableAnnotation; }
            set
            {
                _fibTableAnnotation = value;
            }
        }

        private Brush GetBrush( FibonacciTargetType fibLevel, int strength )
        {
            switch ( fibLevel )
            {
                case FibonacciTargetType.Wave3B:
                return Wave3BColor;

                case FibonacciTargetType.Wave4:
                return Wave4;


                case FibonacciTargetType.Wave3A_Wave4:
                return Wave3A_Wave4;

                case FibonacciTargetType.Wave3C_Wave5:
                return Wave3C_Wave5;

                case FibonacciTargetType.MinimumTarget:
                return MinimumTargetColor;

                case FibonacciTargetType.AroundMinimumTarget:
                return AroundMinimumTargetColor;

                case FibonacciTargetType.WaveA:
                case FibonacciTargetType.Wave3A:
                case FibonacciTargetType.AverageTarget:
                return AvgTargetColor;

                case FibonacciTargetType.AroundWaveA:
                case FibonacciTargetType.AroundWave3A:
                case FibonacciTargetType.AroundAvgTarget:
                return AroundAvgTargetColor;

                case FibonacciTargetType.WaveC:
                case FibonacciTargetType.Wave3C:
                case FibonacciTargetType.StrongTarget:
                return StrongTargetColor;

                case FibonacciTargetType.AroundWaveC:
                case FibonacciTargetType.AroundWave3C:
                case FibonacciTargetType.AroundStrongTarget:
                return AroundStrongTargetColor;

                case FibonacciTargetType.Wave5C:
                case FibonacciTargetType.Wave5:
                case FibonacciTargetType.ExtremeTarget:
                return ExtremeTargetColor;

                case FibonacciTargetType.AroundWave5:
                case FibonacciTargetType.AroundExtremeTarget:
                return AroundExtremeTargetColor;


                default:
                {
                    return GetFibColor( strength );
                }

            }
        }

        private DoubleCollection GetStroke( FibonacciTargetType fibLevel )
        {
            switch ( fibLevel )
            {
                case FibonacciTargetType.Wave3B:
                return MinimumTargetDash;

                case FibonacciTargetType.Wave4:
                return MinimumTargetDash;

                
                case FibonacciTargetType.Wave3A_Wave4:
                return AvgTargetDash;

                case FibonacciTargetType.Wave3C_Wave5:
                return MinimumTargetDash;

                case FibonacciTargetType.MinimumTarget:
                return MinimumTargetDash;

                case FibonacciTargetType.AroundMinimumTarget:
                return AroundMinimumTargetDash;

                case FibonacciTargetType.WaveA:
                case FibonacciTargetType.Wave3A:
                case FibonacciTargetType.AverageTarget:                
                return AvgTargetDash;

                case FibonacciTargetType.AroundWave3A:
                case FibonacciTargetType.AroundAvgTarget:
                return AroundAvgTargetDash;

                case FibonacciTargetType.Wave3C:
                case FibonacciTargetType.StrongTarget:                
                return StrongTargetDash;

                case FibonacciTargetType.AroundWave3C:
                case FibonacciTargetType.AroundStrongTarget:
                    return AroundStrongTargetDash;

                case FibonacciTargetType.Wave5C:
                case FibonacciTargetType.Wave5:
                case FibonacciTargetType.ExtremeTarget:
                return ExtremeTargetDash;

                case FibonacciTargetType.AroundWave5:
                case FibonacciTargetType.AroundExtremeTarget:
                return AroundExtremeTargetDash;


                default:
                {
                    return MinimumTargetDash;
                }
            }
        }

        private int GetImportance( FibonacciTargetType fibLevel )
        {
            switch ( fibLevel )
            {
                case FibonacciTargetType.Wave3B:
                return 1;

                case FibonacciTargetType.Wave4:
                return 1;


                case FibonacciTargetType.Wave3A_Wave4:
                return AvgTargetImportance;

                case FibonacciTargetType.Wave3C_Wave5:
                return StrongTargetImportance;

                case FibonacciTargetType.MinimumTarget:
                return 1;

                case FibonacciTargetType.AroundMinimumTarget:
                return MinimumTargetImportance;

                case FibonacciTargetType.WaveA:
                case FibonacciTargetType.Wave3A:
                case FibonacciTargetType.AverageTarget:
                return AvgTargetImportance;

                case FibonacciTargetType.AroundWave3A:
                case FibonacciTargetType.AroundAvgTarget:
                return AroundAvgTargetImportance;

                case FibonacciTargetType.Wave3C:
                case FibonacciTargetType.StrongTarget:
                return StrongTargetImportance;

                case FibonacciTargetType.AroundWave3C:
                case FibonacciTargetType.AroundStrongTarget:
                return AroundStrongTargetImportance;

                case FibonacciTargetType.Wave5C:
                case FibonacciTargetType.Wave5:
                case FibonacciTargetType.ExtremeTarget:
                return ExtremeTargetImportance;

                case FibonacciTargetType.AroundWave5:
                case FibonacciTargetType.AroundExtremeTarget:
                return AroundExtremeTargetImportance;


                

                default:
                {
                    return 1;
                }
            }
        }


        Style _gripStyle;
        public static readonly SolidColorBrush MinimumTargetColor       = new SolidColorBrush( Colors.Peru );
        public static readonly int MinimumTargetImportance              = 1;
        public static readonly DoubleCollection MinimumTargetDash       = new DoubleCollection( ) { 2, 1 };

        public static readonly SolidColorBrush AroundMinimumTargetColor = new SolidColorBrush( Colors.PapayaWhip );
        public static readonly int AroundMinimumTargetImportance        = 1;
        public static readonly DoubleCollection AroundMinimumTargetDash = new DoubleCollection() { 2, 1 };

        public static readonly SolidColorBrush AvgTargetColor           = new SolidColorBrush( Colors.ForestGreen );
        public static readonly int AvgTargetImportance                  = 1;
        public static readonly DoubleCollection AvgTargetDash           = new DoubleCollection() { 3, 2 };

        public static readonly SolidColorBrush AroundAvgTargetColor     = new SolidColorBrush( Colors.DarkSeaGreen );
        public static readonly int AroundAvgTargetImportance            = 1;
        public static readonly DoubleCollection AroundAvgTargetDash     = new DoubleCollection() { 2, 2 };

        public static readonly SolidColorBrush StrongTargetColor        = new SolidColorBrush( Colors.DarkBlue );
        public static readonly int StrongTargetImportance               = 2;
        public static readonly DoubleCollection StrongTargetDash        = new DoubleCollection() { 3, 2 };

        public static readonly SolidColorBrush AroundStrongTargetColor  = new SolidColorBrush( Colors.LightSkyBlue );
        public static readonly int AroundStrongTargetImportance         = 1;
        public static readonly DoubleCollection AroundStrongTargetDash  = new DoubleCollection() { 3, 2 };

        public static readonly SolidColorBrush ExtremeTargetColor       = new SolidColorBrush( Colors.Red );
        public static readonly int ExtremeTargetImportance              = 2;
        public static readonly DoubleCollection ExtremeTargetDash       = new DoubleCollection() { 4, 2 };

        public static readonly SolidColorBrush AroundExtremeTargetColor = new SolidColorBrush( Colors.Orange );
        public static readonly int AroundExtremeTargetImportance        = 1;
        public static readonly DoubleCollection AroundExtremeTargetDash = new DoubleCollection() { 4, 2 };

        public static readonly SolidColorBrush StrengthBaseColor        = new SolidColorBrush( Color.FromArgb( byte.MaxValue, 119, 119, 135 ) );
        public static readonly SolidColorBrush Impt0Color               = new SolidColorBrush( Colors.LightGray );
        public static readonly SolidColorBrush Impt5Color               = new SolidColorBrush( Colors.LightGray );
        public static readonly SolidColorBrush Impt10Color              = new SolidColorBrush( Colors.Blue );
        public static readonly SolidColorBrush Impt20Color              = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush Impt50Color              = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush Impt100Color             = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush Impt95Color              = new SolidColorBrush( Colors.Magenta );
        public static readonly SolidColorBrush Impt85Color              = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush Impt75Color              = new SolidColorBrush( Colors.Blue );
        public static readonly SolidColorBrush Impt35Color              = new SolidColorBrush( Colors.Violet );
        public static readonly SolidColorBrush Impt25Color              = new SolidColorBrush( Colors.Plum );

        public static readonly SolidColorBrush BaseColor                = new SolidColorBrush( Color.FromArgb( byte.MaxValue, 119, 119, 135 ) );
        public static readonly SolidColorBrush Wave2Color               = new SolidColorBrush( Colors.LightGray );
        public static readonly SolidColorBrush Wave3AColor              = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush Wave3BColor              = new SolidColorBrush( Colors.LightGray );
        public static readonly SolidColorBrush Wave3CColor              = new SolidColorBrush( Colors.Red );
        public static readonly SolidColorBrush Wave4                    = new SolidColorBrush( Colors.LightGray );
        public static readonly SolidColorBrush Wave5                    = new SolidColorBrush( Colors.Green );
        public static readonly SolidColorBrush Wave3A_Wave4             = new SolidColorBrush( Colors.OrangeRed );
        public static readonly SolidColorBrush Wave3C_Wave5             = new SolidColorBrush( Colors.DarkOliveGreen );





        public static SolidColorBrush GetFibColor( int strength )
        {
            if ( strength == 0 )
            {
                return Impt0Color;
            }
            else if ( strength == 5 )
            {
                return Impt5Color;
            }
            else if ( strength == 10 )
            {
                return Impt10Color;
            }
            else if ( strength == 20 )
            {
                return Impt20Color;
            }
            else if ( strength == 25 )
            {
                return Impt25Color;
            }
            else if ( strength == 35 )
            {
                return Impt35Color;
            }
            else if ( strength == 50 )
            {
                return Impt50Color;
            }
            else if ( strength == 75 )
            {
                return Impt75Color;
            }
            else if ( strength == 85 )
            {
                return Impt85Color;
            }
            else if ( strength == 95 )
            {
                return Impt95Color;
            }
            else if ( strength == 100 )
            {
                return Impt100Color;
            }

            return StrengthBaseColor;
        }

        private void CreateFibAnnotation( fxFibLevel[ ] fibRatios )
        {
            _fibTableAnnotation = new AnnotationCollection( );            

            AddFibLine( fibRatios[ 0 ] );

            for ( int index = 1; index < fibRatios.Length; ++index )
            {
                AddFibLine( fibRatios[ index ] );                
            }
        }

        private void CreateFibAnnotation( PooledList<fxFibLevelCluster> fibRatios )
        {
            _fibTableAnnotation = new AnnotationCollection();

            AddFibLine( fibRatios[0] );

            for ( int index = 1; index < fibRatios.Count; ++index )
            {
                AddFibLine( fibRatios[index] );
            }
        }

        private void AddFibLine( fxFibLevel fibLine )
        {
            var fibBrush             = GetBrush( fibLine.FibTargetType, fibLine.FibStrength );

            var fline                = new SRlevelLineAnnotation();
            fline.StrokeThickness    = GetImportance( fibLine.FibTargetType );
            fline.DataContext        = new SRlevelLineAnnotationViewModel( fibLine.FibValue/100, fibBrush );
            fline.IsEditable         = false;
            fline.StrokeDashArray    = GetStroke( fibLine.FibTargetType );
            fline.ResizingGripsStyle = _gripStyle;

            _fibTableAnnotation.Add( fline );

            var fibText              = new SRlevelTextAnnotation();
            fibText.Y1               = fibLine.FibValue / 100;
            fibText.Tag              = fibLine.FibValue / 100;
            fibText.Foreground       = fibBrush;

            _fibTableAnnotation.Add( fibText );
        }

        private void AddFibLine( fxFibLevelCluster fibLine )
        {
            var fibBrush             = GetBrush( fibLine.FibTargetType, fibLine.FibStrength );

            var fline                = new SRlevelLineAnnotation();
            fline.StrokeThickness    = GetImportance( fibLine.FibTargetType );
            fline.DataContext        = new SRlevelLineAnnotationViewModel( fibLine.FibValue / 100, fibBrush );
            fline.IsEditable         = false;
            fline.StrokeDashArray    = GetStroke( fibLine.FibTargetType );
            fline.ResizingGripsStyle = _gripStyle;

            _fibTableAnnotation.Add( fline );

            var fibText              = new SRlevelTextAnnotation();
            fibText.Y1               = fibLine.FibValue / 100;
            fibText.Tag              = fibLine.FibValue / 100;
            fibText.Foreground       = fibBrush;

            _fibTableAnnotation.Add( fibText );
        }




        //
        // Summary:
        //     Initializes a new instance of the SciChart.Charting.DrawingTools.TradingAnnotations.Models.RatioModel
        //     class.
        public fxFibLineProperties( double value, Brush brush, FibonacciTargetType fibType, int importance )
        {
            FibValue = value;
            Brush = brush;
            FibTargetType = fibType;
            Importance = importance;


            if ( importance == 1 )
            {
                _strokeArray = new DoubleCollection();
                _strokeArray.Add( 2 );
                _strokeArray.Add( 4 );
            }
            else if ( importance == 2 )
            {
                _strokeArray = new DoubleCollection();
                _strokeArray.Add( 3 );
                _strokeArray.Add( 3 );
            }
            else if ( importance == 3 )
            {
                _strokeArray = new DoubleCollection();
                _strokeArray.Add( 10 );
                _strokeArray.Add( 5 );
            }
        }
        //
        // Summary:
        //     Gets or sets the Value that is used for displaying Level of the (SciChart.Charting.DrawingTools.TradingAnnotations.FibonacciAnnotation.FibonacciRatioLine)
        public double FibValue { get; set; }


        public int Importance
        {
            get { return _importance; }
            set
            {
                _importance = value;
            }
        }


        AnnotationCollection _fibTableAnnotation;
        int _importance;
        DoubleCollection _strokeArray;

        public DoubleCollection StrokeArray
        {
            get { return _strokeArray; }
            set
            {
                _strokeArray = value;
            }
        }

        //
        // Summary:
        //     Gets or sets brush that is used for coloring (SciChart.Charting.DrawingTools.TradingAnnotations.FibonacciAnnotation.FibonacciRatioLine)
        public Brush Brush { get; set; }

        public FibonacciTargetType FibTargetType { get; set; }
    }


}


