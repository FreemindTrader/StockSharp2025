// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Rendering.HighSpeedRasterizer.HsRenderContext
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace fx.Xaml.Charting
{
    internal class HsRenderContext : RenderContextBase
    {
        private readonly RenderOperationLayers _renderLayers = new RenderOperationLayers();
        private readonly List<IDisposable> _resourcesToDispose = new List<IDisposable>();
        private BitmapContext _bitmapContext;
        private readonly Image _image;
        private readonly WriteableBitmap _renderWriteableBitmap;
        private Size _viewportSize;

        private TextureCache TextureCache
        {
            get
            {
                return ( TextureCache ) this._textureCache;
            }
        }

        public HsRenderContext( Image image, WriteableBitmap renderWriteableBitmap, Size viewportSize, TextureCacheBase textureCache )
          : base( textureCache )
        {
            this._image = image;
            this._renderWriteableBitmap = renderWriteableBitmap;
            this._viewportSize = viewportSize;
            this._bitmapContext = this._renderWriteableBitmap.GetBitmapContext();
        }

        public override RenderOperationLayers Layers
        {
            get
            {
                return this._renderLayers;
            }
        }

        public override Size ViewportSize
        {
            get
            {
                return this._viewportSize;
            }
        }

        public override IBrush2D CreateBrush( Color seriesColor, double opacity = 1.0, bool? alphaBlend = null )
        {
            Color color = seriesColor;
            int colorCode = WriteableBitmapExtensions.ConvertColor(opacity, seriesColor);
            bool? nullable = alphaBlend;
            int num = nullable.HasValue ? (nullable.GetValueOrDefault() ? 1 : 0) : ((double) seriesColor.A * opacity < (double) byte.MaxValue ? 1 : 0);
            return ( IBrush2D ) new HsBrush( color, colorCode, num != 0 );
        }

        public override IBrush2D CreateBrush( Brush brush, double opacity = 1.0, TextureMappingMode textureMappingMode = TextureMappingMode.PerScreen )
        {
            if ( brush == null )
                return this.CreateBrush( Color.FromArgb( ( byte ) 0, ( byte ) 0, ( byte ) 0, ( byte ) 0 ), 1.0, new bool?() );
            if ( brush is SolidColorBrush )
                return this.CreateBrush( ( ( SolidColorBrush ) brush ).Color, opacity, new bool?() );
            return ( IBrush2D ) new TextureBrush( brush, textureMappingMode, this.TextureCache );
        }

        public override IPen2D CreatePen( Color seriesColor, bool antiAliasing, float strokeThickness, double opacity = 1.0, double[ ] strokeDashArray = null, PenLineCap strokeEndLineCap = PenLineCap.Round )
        {
            return ( IPen2D ) new HsPen( seriesColor, WriteableBitmapExtensions.ConvertColor( opacity, seriesColor ), strokeThickness, strokeEndLineCap, antiAliasing, opacity, strokeDashArray );
        }

        public override ISprite2D CreateSprite( FrameworkElement fe )
        {
            return ( ISprite2D ) new HsSprite2D( this.TextureCache.GetWriteableBitmapTexture( fe ) );
        }

        public override void Clear()
        {
            this._bitmapContext.Clear();
        }

        public override void DrawSprite( ISprite2D sprite2D, Rect srcRect, Point destPoint )
        {
            this._bitmapContext.WriteableBitmap.Blit( new Rect( destPoint.X, destPoint.Y, ( double ) sprite2D.Width, ( double ) sprite2D.Height ), ( sprite2D as HsSprite2D ).WriteableBitmap, srcRect );
        }

        public override void DrawSprites( ISprite2D sprite2D, Rect srcRect, IEnumerable<Point> points )
        {
            Rect destRect = new Rect(0.0, 0.0, (double) sprite2D.Width, (double) sprite2D.Height);
            WriteableBitmap writeableBitmap = (sprite2D as HsSprite2D).WriteableBitmap;
            foreach ( Point point in points )
            {
                destRect.X = point.X;
                destRect.Y = point.Y;
                this._bitmapContext.WriteableBitmap.Blit( destRect, writeableBitmap, srcRect );
            }
        }

        public override void DrawSprites( ISprite2D sprite2D, IEnumerable<Rect> dstRects )
        {
            WriteableBitmap writeableBitmap = (sprite2D as HsSprite2D).WriteableBitmap;
            foreach ( Rect dstRect in dstRects )
                this._bitmapContext.WriteableBitmap.Blit( dstRect, writeableBitmap, new Rect( 0.0, 0.0, ( double ) sprite2D.Width, ( double ) sprite2D.Height ) );
        }

        public override void FillRectangle( IBrush2D brush, Point pt1, Point pt2, double gradientRotationAngle = 0.0 )
        {
            if ( pt1.X < 0.0 && pt2.X < 0.0 || pt1.Y < 0.0 && pt2.Y < 0.0 )
                return;
            double y1 = pt1.Y;
            Size viewportSize = this.ViewportSize;
            double height1 = viewportSize.Height;
            if ( y1 > height1 )
            {
                double y2 = pt2.Y;
                viewportSize = this.ViewportSize;
                double height2 = viewportSize.Height;
                if ( y2 > height2 )
                    return;
            }
            double x1 = pt1.X;
            viewportSize = this.ViewportSize;
            double width1 = viewportSize.Width;
            if ( x1 > width1 )
            {
                double x2 = pt2.X;
                viewportSize = this.ViewportSize;
                double width2 = viewportSize.Width;
                if ( x2 > width2 )
                    return;
            }
            this.ClipRectangle( ref pt1, ref pt2 );
            Rect primitiveRect = new Rect(pt1, pt2);
            if ( brush is TextureBrush )
            {
                TextureBrush textureBrush = (TextureBrush) brush;
                int[] texture = textureBrush.GetIntTexture(this.ViewportSize);
                this._bitmapContext.WriteableBitmap.FillRectangle( ( int ) primitiveRect.Left, ( int ) primitiveRect.Top, ( int ) primitiveRect.Right, ( int ) primitiveRect.Bottom, ( Func<int, int, int> ) ( ( x, y ) => texture[ textureBrush.GetIntOffsetConsideringMappingMode( x, y, primitiveRect, gradientRotationAngle ) ] ), brush.AlphaBlend ? WriteableBitmapExtensions.BlendMode.Alpha : WriteableBitmapExtensions.BlendMode.None );
            }
            else
                this._bitmapContext.WriteableBitmap.FillRectangle( ( int ) primitiveRect.Left, ( int ) primitiveRect.Top, ( int ) primitiveRect.Right, ( int ) primitiveRect.Bottom, brush.ColorCode, brush.AlphaBlend ? WriteableBitmapExtensions.BlendMode.Alpha : WriteableBitmapExtensions.BlendMode.None );
        }

        public override void FillArea( IBrush2D brush, IEnumerable<Tuple<Point, Point>> lines, bool isVerticalChart = false, double gradientRotationAngle = 0.0 )
        {
            foreach ( IEnumerable<Tuple<Point, Point>> splitMultilineByGap in lines.SplitMultilineByGaps() )
            {
                Point[] array = this.ClipArea(splitMultilineByGap).ToArray<Point>();
                int[] points = new int[array.Length * 2 + 2];
                int index1 = 0;
                int num1 = 0;
                for ( ; index1 < array.Length ; ++index1 )
                {
                    int[] numArray1 = points;
                    int index2 = num1;
                    int num2 = index2 + 1;
                    int x = (int) array[index1].X;
                    numArray1[ index2 ] = x;
                    int[] numArray2 = points;
                    int index3 = num2;
                    num1 = index3 + 1;
                    int num3 = (int) array[index1].Y - 1;
                    numArray2[ index3 ] = num3;
                }
                int[] numArray = points;
                int index4 = num1;
                int index5 = index4 + 1;
                int x1 = (int) array[0].X;
                numArray[ index4 ] = x1;
                points[ index5 ] = ( int ) array[ 0 ].Y - 1;
                if ( brush is TextureBrush )
                {
                    TextureBrush textureBrush = (TextureBrush) brush;
                    int[] texture = textureBrush.GetIntTexture(this.ViewportSize);
                    this._bitmapContext.WriteableBitmap.FillPolygon( points, ( Func<int, int, int> ) ( ( x, y ) => texture[ textureBrush.GetIntOffsetNotConsideringMappingMode( x, y, gradientRotationAngle ) ] ), brush.AlphaBlend ? WriteableBitmapExtensions.BlendMode.Alpha : WriteableBitmapExtensions.BlendMode.None );
                }
                else
                    this._bitmapContext.WriteableBitmap.FillPolygon( points, brush.ColorCode, brush.AlphaBlend ? WriteableBitmapExtensions.BlendMode.Alpha : WriteableBitmapExtensions.BlendMode.None );
            }
        }

        public override void DisposeResourceAfterDraw( IDisposable disposable )
        {
            if ( disposable == null )
                return;
            this._resourcesToDispose.Add( disposable );
        }

        public void Blit( Rect destRect, WriteableBitmap srcImage, Rect srcRect )
        {
            this._bitmapContext.WriteableBitmap.Blit( destRect, srcImage, srcRect );
        }

        public void FillRectangle( IBrush2D fillBrush, int x1, int y1, int x2, int y2 )
        {
            this._bitmapContext.WriteableBitmap.FillRectangle( x1, y1, x2, y2, fillBrush.ColorCode, fillBrush.AlphaBlend ? WriteableBitmapExtensions.BlendMode.Alpha : WriteableBitmapExtensions.BlendMode.None );
        }

        public override void DrawEllipse( IPen2D strokePen, IBrush2D fillBrush, Point center, double width, double height )
        {
            if ( !this.IsInBounds( center ) )
                return;
            if ( width <= 1.0 && height <= 1.0 )
            {
                WriteableBitmapExtensions.DrawPixel( this._bitmapContext, ( int ) this.ViewportSize.Width, ( int ) this.ViewportSize.Height, ( int ) center.X, ( int ) center.Y, fillBrush.ColorCode );
            }
            else
            {
                if ( fillBrush != null && !fillBrush.IsTransparent )
                    WriteableBitmapExtensions.FillEllipseCentered( this._bitmapContext, ( int ) center.X, ( int ) center.Y, ( int ) width / 2, ( int ) height / 2, fillBrush.ColorCode, WriteableBitmapExtensions.BlendMode.Alpha );
                if ( strokePen == null || strokePen.IsTransparent )
                    return;
                WriteableBitmapExtensions.DrawEllipseCentered( this._bitmapContext, ( int ) center.X, ( int ) center.Y, ( int ) width / 2, ( int ) height / 2, strokePen.ColorCode, ( int ) strokePen.StrokeThickness );
            }
        }

        public override void DrawEllipses( IPen2D strokePen, IBrush2D fillBrush, IEnumerable<Point> centres, double width, double height )
        {
            foreach ( Point centre in centres )
                this.DrawEllipse( strokePen, fillBrush, centre, width, height );
        }

        public override void DrawPixelsVertically( int x, int yStartBottom, int yEndTop, IList<int> pixelColorsArgb, double opacity, bool yAxisIsFlipped )
        {
            this._bitmapContext.WriteableBitmap.DrawPixelsVertically( x, yStartBottom, yEndTop, pixelColorsArgb, opacity, yAxisIsFlipped );
        }

        public override void Dispose()
        {
            this._bitmapContext.Dispose();
            if ( this._image != null && this._image.Source != this._renderWriteableBitmap )
                this._image.Source = ( ImageSource ) this._renderWriteableBitmap;
            foreach ( IDisposable disposable in this._resourcesToDispose )
                disposable.Dispose();
        }

        public override sealed IPathDrawingContext BeginLine( IPen2D pen, double startX, double startY )
        {
            return ( IPathDrawingContext ) new HsRenderContext.WbxLineDrawingContext( ( HsPen ) pen, this, startX, startY );
        }

        public override sealed IPathDrawingContext BeginPolygon( IBrush2D brush, double startX, double startY, double gradientRotationAngle = 0.0 )
        {
            return ( IPathDrawingContext ) new HsRenderContext.WbxPolygonDrawingContext( brush, this, startX, startY ) { GradientRotationAngle = gradientRotationAngle };
        }

        internal sealed class WbxPolygonDrawingContext : IPathDrawingContext, IDisposable
        {
            private List<int> _points = new List<int>();
            private IBrush2D _brush;
            private readonly HsRenderContext _renderContext;

            public WbxPolygonDrawingContext( IBrush2D brush, HsRenderContext renderContext, double startX, double startY )
            {
                this._renderContext = renderContext;
                this.Begin( ( IPathColor ) brush, startX, startY );
            }

            public double GradientRotationAngle
            {
                get; set;
            }

            public IPathDrawingContext Begin( IPathColor fill, double x, double y )
            {
                this._brush = ( IBrush2D ) fill;
                this._points.Add( ( int ) x.ClipToInt() );
                this._points.Add( ( int ) y.ClipToInt() );
                return ( IPathDrawingContext ) this;
            }

            public IPathDrawingContext MoveTo( double x, double y )
            {
                this._points.Add( ( int ) x.ClipToInt() );
                this._points.Add( ( int ) y.ClipToInt() );
                return ( IPathDrawingContext ) this;
            }

            public void End()
            {
                WriteableBitmapExtensions.BlendMode blendMode = this._brush.AlphaBlend ? WriteableBitmapExtensions.BlendMode.Alpha : WriteableBitmapExtensions.BlendMode.None;
                TextureBrush textureBrush = this._brush as TextureBrush;
                if ( textureBrush != null )
                {
                    int[] texture = textureBrush.GetIntTexture(this._renderContext.ViewportSize);
                    this._renderContext._bitmapContext.WriteableBitmap.FillPolygon( this._points.ToArray(), ( Func<int, int, int> ) ( ( x, y ) => texture[ textureBrush.GetIntOffsetNotConsideringMappingMode( x, y, this.GradientRotationAngle ) ] ), blendMode );
                }
                else
                    this._renderContext._bitmapContext.WriteableBitmap.FillPolygon( this._points.ToArray(), this._brush.ColorCode, blendMode );
            }

            void IDisposable.Dispose()
            {
                this.End();
            }
        }

        private sealed class WbxLineDrawingContext : IPathDrawingContext, IDisposable
        {
            private int _previousLineEndX = -1;
            private int _previousLineEndY = -1;
            private readonly HsRenderContext _context;
            private HsPen _pen;
            private double _lastX;
            private double _lastY;
            private readonly BitmapContext _bitmapContext;
            private Size _viewportSize;

            public WbxLineDrawingContext( HsPen pen, HsRenderContext context, double x, double y )
            {
                this._context = context;
                this._bitmapContext = this._context._bitmapContext;
                this._viewportSize = this._context.ViewportSize;
                this.Begin( ( IPathColor ) pen, x, y );
            }

            public IPathDrawingContext Begin( IPathColor pen, double x, double y )
            {
                this._pen = ( HsPen ) pen;
                this._lastX = x;
                this._lastY = y;
                return ( IPathDrawingContext ) this;
            }

            public IPathDrawingContext MoveTo( double x, double y )
            {
                if ( !this._pen.HasDashes )
                {
                    this.LineToImplementation( x, y );
                    this._lastX = x;
                    this._lastY = y;
                    return ( IPathDrawingContext ) this;
                }
                Point pt1 = new Point(this._lastX, this._lastY);
                Point pt2 = new Point(x, y);
                RenderContextBase.ClipLine( ref pt1, ref pt2, this._viewportSize );
                DashSplitter dashSplitter = this._context.DashSplitter;
                dashSplitter.Reset( pt1, pt2, this._viewportSize, ( IDashSplittingContext ) this._pen );
                while ( dashSplitter.MoveNext() )
                {
                    LineD current = dashSplitter.Current;
                    this.End();
                    this.Begin( ( IPathColor ) this._pen, current.X1, current.Y1 );
                    this.LineToImplementation( current.X2, current.Y2 );
                }
                this._lastX = x;
                this._lastY = y;
                return ( IPathDrawingContext ) this;
            }

            private void LineToImplementation( double x, double y )
            {
                if ( ( double ) this._pen.StrokeThickness > 1.0 )
                    WriteableBitmapExtensions.DrawPennedLine( this._bitmapContext, ( int ) this._viewportSize.Width, ( int ) this._viewportSize.Height, ( int ) this._lastX.ClipToInt(), ( int ) this._lastY.ClipToInt(), ( int ) x.ClipToInt(), ( int ) y.ClipToInt(), this._pen.Pen );
                else if ( this._pen.Antialiased )
                {
                    int x1 = (int) this._lastX.ClipToInt();
                    int y1 = (int) this._lastY.ClipToInt();
                    int x2 = (int) x.ClipToInt();
                    int y2 = (int) y.ClipToInt();
                    bool skipFirstPixel = x1 == this._previousLineEndX && y1 == this._previousLineEndY;
                    WriteableBitmapExtensions.DrawLineAa( this._bitmapContext, ( int ) this._viewportSize.Width, ( int ) this._viewportSize.Height, x1, y1, x2, y2, this._pen.ColorCode, skipFirstPixel );
                    this._previousLineEndX = x2;
                    this._previousLineEndY = y2;
                }
                else
                    WriteableBitmapExtensions.DrawLineBresenham( this._bitmapContext, ( int ) this._viewportSize.Width, ( int ) this._viewportSize.Height, ( int ) this._lastX.ClipToInt(), ( int ) this._lastY.ClipToInt(), ( int ) x.ClipToInt(), ( int ) y.ClipToInt(), this._pen.ColorCode );
                this._lastX = x;
                this._lastY = y;
            }

            public void End()
            {
            }

            void IDisposable.Dispose()
            {
                this.End();
            }
        }
    }
}
