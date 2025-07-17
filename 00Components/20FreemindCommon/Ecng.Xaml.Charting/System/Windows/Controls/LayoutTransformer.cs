// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.LayoutTransformer
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Diagnostics;
using System.Windows.Media;

namespace System.Windows.Controls
{
    [TemplatePart( Name = "TransformRoot", Type = typeof( Grid ) )]
    [TemplatePart( Name = "Presenter", Type = typeof( ContentPresenter ) )]
    public sealed class LayoutTransformer : ContentControl
    {
        public new static readonly DependencyProperty LayoutTransformProperty = DependencyProperty.Register(nameof (LayoutTransform), typeof (Transform), typeof (LayoutTransformer), new PropertyMetadata(new PropertyChangedCallback(LayoutTransformer.LayoutTransformChanged)));
        private Size _childActualSize = Size.Empty;
        private const string TransformRootName = "TransformRoot";
        private const string PresenterName = "Presenter";
        private const double AcceptableDelta = 0.0001;
        private const int DecimalsAfterRound = 4;
        private Panel _transformRoot;
        private ContentPresenter _contentPresenter;
        private MatrixTransform _matrixTransform;
        private Matrix _transformation;

        public new Transform LayoutTransform
        {
            get
            {
                return ( Transform ) this.GetValue( LayoutTransformer.LayoutTransformProperty );
            }
            set
            {
                this.SetValue( LayoutTransformer.LayoutTransformProperty, ( object ) value );
            }
        }

        private FrameworkElement Child
        {
            get
            {
                if ( this._contentPresenter == null )
                    return ( FrameworkElement ) null;
                return this._contentPresenter.Content as FrameworkElement ?? ( FrameworkElement ) this._contentPresenter;
            }
        }

        public LayoutTransformer()
        {
            this.DefaultStyleKey = ( object ) typeof( LayoutTransformer );
            this.IsTabStop = false;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._transformRoot = ( Panel ) ( this.GetTemplateChild( "TransformRoot" ) as Grid );
            this._contentPresenter = this.GetTemplateChild( "Presenter" ) as ContentPresenter;
            this._matrixTransform = new MatrixTransform();
            if ( this._transformRoot != null )
                this._transformRoot.RenderTransform = ( Transform ) this._matrixTransform;
            this.ApplyLayoutTransform();
        }

        private static void LayoutTransformChanged( DependencyObject o, DependencyPropertyChangedEventArgs e )
        {
            ( ( LayoutTransformer ) o ).ProcessTransform( ( Transform ) e.NewValue );
        }

        public void ApplyLayoutTransform()
        {
            this.ProcessTransform( this.LayoutTransform );
        }

        private void ProcessTransform( Transform transform )
        {
            this._transformation = LayoutTransformer.RoundMatrix( this.GetTransformMatrix( transform ), 4 );
            if ( this._matrixTransform != null )
                this._matrixTransform.Matrix = this._transformation;
            this.InvalidateMeasure();
        }

        private Matrix GetTransformMatrix( Transform transform )
        {
            if ( transform != null )
            {
                TransformGroup transformGroup = transform as TransformGroup;
                if ( transformGroup != null )
                {
                    Matrix matrix1 = Matrix.Identity;
                    foreach ( Transform child in transformGroup.Children )
                        matrix1 = LayoutTransformer.MatrixMultiply( matrix1, this.GetTransformMatrix( child ) );
                    return matrix1;
                }
                RotateTransform rotateTransform = transform as RotateTransform;
                if ( rotateTransform != null )
                {
                    double num1 = 2.0 * Math.PI * rotateTransform.Angle / 360.0;
                    double m12 = Math.Sin(num1);
                    double num2 = Math.Cos(num1);
                    return new Matrix( num2, m12, -m12, num2, 0.0, 0.0 );
                }
                ScaleTransform scaleTransform = transform as ScaleTransform;
                if ( scaleTransform != null )
                    return new Matrix( scaleTransform.ScaleX, 0.0, 0.0, scaleTransform.ScaleY, 0.0, 0.0 );
                SkewTransform skewTransform = transform as SkewTransform;
                if ( skewTransform != null )
                {
                    double angleX = skewTransform.AngleX;
                    return new Matrix( 1.0, 2.0 * Math.PI * skewTransform.AngleY / 360.0, 2.0 * Math.PI * angleX / 360.0, 1.0, 0.0, 0.0 );
                }
                MatrixTransform matrixTransform = transform as MatrixTransform;
                if ( matrixTransform != null )
                    return matrixTransform.Matrix;
            }
            return Matrix.Identity;
        }

        protected override Size MeasureOverride( Size availableSize )
        {
            if ( this._transformRoot == null || this.Child == null )
                return Size.Empty;
            this._transformRoot.Measure( !( this._childActualSize == Size.Empty ) ? this._childActualSize : this.ComputeLargestTransformedSize( availableSize ) );
            Rect rect = LayoutTransformer.RectTransform(new Rect(0.0, 0.0, this._transformRoot.DesiredSize.Width, this._transformRoot.DesiredSize.Height), this._transformation);
            return new Size( rect.Width, rect.Height );
        }

        protected override Size ArrangeOverride( Size finalSize )
        {
            FrameworkElement child = this.Child;
            if ( this._transformRoot == null || child == null )
                return finalSize;
            Size a = this.ComputeLargestTransformedSize(finalSize);
            if ( LayoutTransformer.IsSizeSmaller( a, this._transformRoot.DesiredSize ) )
                a = this._transformRoot.DesiredSize;
            Rect rect = LayoutTransformer.RectTransform(new Rect(0.0, 0.0, a.Width, a.Height), this._transformation);
            this._transformRoot.Arrange( new Rect( -rect.Left + ( finalSize.Width - rect.Width ) / 2.0, -rect.Top + ( finalSize.Height - rect.Height ) / 2.0, a.Width, a.Height ) );
            if ( LayoutTransformer.IsSizeSmaller( a, child.RenderSize ) && Size.Empty == this._childActualSize )
            {
                this._childActualSize = new Size( child.ActualWidth, child.ActualHeight );
                this.InvalidateMeasure();
            }
            else
                this._childActualSize = Size.Empty;
            return finalSize;
        }

        private Size ComputeLargestTransformedSize( Size arrangeBounds )
        {
            Size size = Size.Empty;
            bool flag1 = double.IsInfinity(arrangeBounds.Width);
            if ( flag1 )
                arrangeBounds.Width = arrangeBounds.Height;
            bool flag2 = double.IsInfinity(arrangeBounds.Height);
            if ( flag2 )
                arrangeBounds.Height = arrangeBounds.Width;
            double m11 = this._transformation.M11;
            double m12 = this._transformation.M12;
            double m21 = this._transformation.M21;
            double m22 = this._transformation.M22;
            double num1 = Math.Abs(arrangeBounds.Width / m11);
            double num2 = Math.Abs(arrangeBounds.Width / m21);
            double num3 = Math.Abs(arrangeBounds.Height / m12);
            double num4 = Math.Abs(arrangeBounds.Height / m22);
            double num5 = num1 / 2.0;
            double num6 = num2 / 2.0;
            double num7 = num3 / 2.0;
            double num8 = num4 / 2.0;
            double num9 = -(num2 / num1);
            double num10 = -(num4 / num3);
            if ( 0.0 == arrangeBounds.Width || 0.0 == arrangeBounds.Height )
                size = new Size( arrangeBounds.Width, arrangeBounds.Height );
            else if ( flag1 & flag2 )
                size = new Size( double.PositiveInfinity, double.PositiveInfinity );
            else if ( !LayoutTransformer.MatrixHasInverse( this._transformation ) )
                size = new Size( 0.0, 0.0 );
            else if ( 0.0 == m12 || 0.0 == m21 )
            {
                double num11 = flag2 ? double.PositiveInfinity : num4;
                double num12 = flag1 ? double.PositiveInfinity : num1;
                if ( 0.0 == m12 && 0.0 == m21 )
                    size = new Size( num12, num11 );
                else if ( 0.0 == m12 )
                {
                    double height = Math.Min(num6, num11);
                    size = new Size( num12 - Math.Abs( m21 * height / m11 ), height );
                }
                else if ( 0.0 == m21 )
                {
                    double width = Math.Min(num7, num12);
                    size = new Size( width, num11 - Math.Abs( m12 * width / m22 ) );
                }
            }
            else if ( 0.0 == m11 || 0.0 == m22 )
            {
                double num11 = flag2 ? double.PositiveInfinity : num3;
                double num12 = flag1 ? double.PositiveInfinity : num2;
                if ( 0.0 == m11 && 0.0 == m22 )
                    size = new Size( num11, num12 );
                else if ( 0.0 == m11 )
                {
                    double height = Math.Min(num8, num12);
                    size = new Size( num11 - Math.Abs( m22 * height / m12 ), height );
                }
                else if ( 0.0 == m22 )
                {
                    double width = Math.Min(num5, num11);
                    size = new Size( width, num12 - Math.Abs( m11 * width / m21 ) );
                }
            }
            else if ( num6 <= num10 * num5 + num4 )
                size = new Size( num5, num6 );
            else if ( num8 <= num9 * num7 + num2 )
            {
                size = new Size( num7, num8 );
            }
            else
            {
                double width = (num4 - num2) / (num9 - num10);
                size = new Size( width, num9 * width + num2 );
            }
            return size;
        }

        private static bool IsSizeSmaller( Size a, Size b )
        {
            if ( a.Width + 0.0001 >= b.Width )
                return a.Height + 0.0001 < b.Height;
            return true;
        }

        private static Matrix RoundMatrix( Matrix matrix, int decimals )
        {
            return new Matrix( Math.Round( matrix.M11, decimals ), Math.Round( matrix.M12, decimals ), Math.Round( matrix.M21, decimals ), Math.Round( matrix.M22, decimals ), matrix.OffsetX, matrix.OffsetY );
        }

        private static Rect RectTransform( Rect rect, Matrix matrix )
        {
            Point point1 = matrix.Transform(new Point(rect.Left, rect.Top));
            Point point2 = matrix.Transform(new Point(rect.Right, rect.Top));
            Point point3 = matrix.Transform(new Point(rect.Left, rect.Bottom));
            Point point4 = matrix.Transform(new Point(rect.Right, rect.Bottom));
            double x = Math.Min(Math.Min(point1.X, point2.X), Math.Min(point3.X, point4.X));
            double y = Math.Min(Math.Min(point1.Y, point2.Y), Math.Min(point3.Y, point4.Y));
            double num1 = Math.Max(Math.Max(point1.X, point2.X), Math.Max(point3.X, point4.X));
            double num2 = Math.Max(Math.Max(point1.Y, point2.Y), Math.Max(point3.Y, point4.Y));
            return new Rect( x, y, num1 - x, num2 - y );
        }

        private static Matrix MatrixMultiply( Matrix matrix1, Matrix matrix2 )
        {
            return new Matrix( matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21, matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22, matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21, matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22, matrix1.OffsetX * matrix2.M11 + matrix1.OffsetY * matrix2.M21 + matrix2.OffsetX, matrix1.OffsetX * matrix2.M12 + matrix1.OffsetY * matrix2.M22 + matrix2.OffsetY );
        }

        private static bool MatrixHasInverse( Matrix matrix )
        {
            return 0.0 != matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21;
        }

        [Conditional( "DIAGNOSTICWRITELINE" )]
        private static void DiagnosticWriteLine( string message )
        {
        }
    }
}
