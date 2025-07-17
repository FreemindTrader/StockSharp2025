// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Annotations.LineAnnotationBase
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
namespace fx.Xaml.Charting
{
    public abstract class LineAnnotationBase : AnnotationBase
    {
        public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(nameof (StrokeDashArray), typeof (DoubleCollection), typeof (LineAnnotationBase), new PropertyMetadata((PropertyChangedCallback) null));
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(nameof (StrokeThickness), typeof (double), typeof (LineAnnotationBase), new PropertyMetadata((object) 1.0));
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(nameof (Stroke), typeof (Brush), typeof (LineAnnotationBase), new PropertyMetadata((PropertyChangedCallback) null));

        public Brush Stroke
        {
            get
            {
                return ( Brush ) this.GetValue( LineAnnotationBase.StrokeProperty );
            }
            set
            {
                this.SetValue( LineAnnotationBase.StrokeProperty, ( object ) value );
            }
        }

        public double StrokeThickness
        {
            get
            {
                return ( double ) this.GetValue( LineAnnotationBase.StrokeThicknessProperty );
            }
            set
            {
                this.SetValue( LineAnnotationBase.StrokeThicknessProperty, ( object ) value );
            }
        }

        public DoubleCollection StrokeDashArray
        {
            get
            {
                return ( DoubleCollection ) this.GetValue( LineAnnotationBase.StrokeDashArrayProperty );
            }
            set
            {
                this.SetValue( LineAnnotationBase.StrokeDashArrayProperty, ( object ) value );
            }
        }

        protected override void GetPropertiesFromIndex( int index, out DependencyProperty x, out DependencyProperty y )
        {
            if ( index == 0 )
            {
                x = AnnotationBase.X1Property;
                y = AnnotationBase.Y1Property;
            }
            else
            {
                x = AnnotationBase.X2Property;
                y = AnnotationBase.Y2Property;
            }
        }

        protected override Cursor GetSelectedCursor()
        {
            return Cursors.SizeAll;
        }

        public override bool IsPointWithinBounds( Point point )
        {
            IAnnotationCanvas canvas = base.GetCanvas(base.AnnotationCanvas);
            if ( this.XAxis == null || this.YAxis == null )
            {
                return false;
            }
            ICoordinateCalculator<double> currentCoordinateCalculator = this.XAxis.GetCurrentCoordinateCalculator();
            ICoordinateCalculator<double> coordinateCalculator = this.YAxis.GetCurrentCoordinateCalculator();
            AnnotationCoordinates coordinates = base.GetCoordinates(canvas, currentCoordinateCalculator, coordinateCalculator);
            return PointUtil.DistanceFromLine( point, new Point( coordinates.X1Coord, coordinates.Y1Coord ), new Point( coordinates.X2Coord, coordinates.Y2Coord ), true ) < 7.07;
        }
    }
}
