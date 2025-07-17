// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.Common.RenderContextBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Utility;

namespace Ecng.Xaml.Charting.Rendering.Common
{
    public abstract class RenderContextBase : IRenderContext2D, IDisposable
    {
        private static readonly Dictionary<string, FontFamily> _fonts = new Dictionary<string, FontFamily>();
        private readonly DashSplitter _dashSplitter = new DashSplitter();
        protected readonly TextureCacheBase _textureCache;
        public const float FontSizeStep = 0.5f;

        protected RenderContextBase( TextureCacheBase textureCache )
        {
            this._textureCache = textureCache;
        }

        internal DashSplitter DashSplitter
        {
            get
            {
                return this._dashSplitter;
            }
        }

        public abstract RenderOperationLayers Layers
        {
            get;
        }

        public abstract Size ViewportSize
        {
            get;
        }

        public virtual void SetPrimitvesCachingEnabled( bool bEnabled )
        {
        }

        public abstract IBrush2D CreateBrush( Color color, double opacity = 1.0, bool? alphaBlend = null );

        public abstract IBrush2D CreateBrush( Brush brush, double opacity = 1.0, TextureMappingMode textureMappingMode = TextureMappingMode.PerScreen );

        public abstract IPen2D CreatePen( Color color, bool antiAliasing, float strokeThickness, double opacity = 1.0, double[ ] strokeDashArray = null, PenLineCap strokeEndLineCap = PenLineCap.Round );

        public abstract ISprite2D CreateSprite( FrameworkElement fe );

        public abstract void Clear();

        public abstract void DrawSprite( ISprite2D srcSprite, Rect srcRect, Point destPoint );

        public abstract void DrawSprites( ISprite2D sprite2D, Rect srcRect, IEnumerable<Point> points );

        public abstract void DrawSprites( ISprite2D sprite2D, IEnumerable<Rect> dstRects );

        public abstract void FillRectangle( IBrush2D brush, Point pt1, Point pt2, double gradientRotationAngle = 0.0 );

        public abstract void FillArea( IBrush2D brush, IEnumerable<Tuple<Point, Point>> lines, bool isVerticalChart = false, double gradientRotationAngle = 0.0 );

        public abstract void DrawEllipse( IPen2D strokePen, IBrush2D fillBrush, Point center, double width, double height );

        public abstract void DrawEllipses( IPen2D strokePen, IBrush2D fillBrush, IEnumerable<Point> centres, double width, double height );

        public abstract void Dispose();

        public abstract void DisposeResourceAfterDraw( IDisposable disposable );

        public abstract void DrawPixelsVertically( int x, int yStartBottom, int yEndTop, IList<int> pixelColorsArgb, double opacity, bool yAxisIsFlipped );

        public virtual void DrawQuad( IPen2D pen, Point pt1, Point pt2 )
        {
            if ( pt1.X < 0.0 && pt2.X < 0.0 || pt1.Y < 0.0 && pt2.Y < 0.0 || pt1.Y > this.ViewportSize.Height && pt2.Y > this.ViewportSize.Height || pt1.X > this.ViewportSize.Width && pt2.X > this.ViewportSize.Width )
                return;
            this.ClipRectangle( ref pt1, ref pt2, 1, 1 );
            using ( IPathDrawingContext pathDrawingContext = this.BeginLine( pen, pt2.X, pt1.Y ) )
            {
                pathDrawingContext.MoveTo( pt2.X, pt2.Y );
                pathDrawingContext.MoveTo( pt1.X, pt2.Y );
                pathDrawingContext.MoveTo( pt1.X, pt1.Y );
                pathDrawingContext.MoveTo( pt2.X, pt1.Y );
            }
        }

        public virtual void DrawLine( IPen2D pen, Point pt1, Point pt2 )
        {
            using ( IPathDrawingContext pathDrawingContext = this.BeginLine( pen, pt1.X, pt1.Y ) )
                pathDrawingContext.MoveTo( pt2.X, pt2.Y );
        }

        public virtual void DrawLines( IPen2D pen, IEnumerable<Point> points )
        {
            IEnumerator<Point> enumerator = points.GetEnumerator();
            enumerator.MoveNext();
            Point current1 = enumerator.Current;
            using ( IPathDrawingContext pathDrawingContext = this.BeginLine( pen, current1.X, current1.Y ) )
            {
                while ( enumerator.MoveNext() )
                {
                    Point current2 = enumerator.Current;
                    pathDrawingContext.MoveTo( current2.X, current2.Y );
                }
            }
        }

        public virtual void FillPolygon( IBrush2D brush, IEnumerable<Point> points )
        {
            IEnumerator<Point> enumerator = points.GetEnumerator();
            enumerator.MoveNext();
            Point current1 = enumerator.Current;
            using ( IPathDrawingContext pathDrawingContext = this.BeginPolygon( brush, current1.X, current1.Y, 0.0 ) )
            {
                while ( enumerator.MoveNext() )
                {
                    Point current2 = enumerator.Current;
                    pathDrawingContext.MoveTo( current2.X, current2.Y );
                }
            }
        }

        public void TextDrawDimensions( string text, float fontSize, Color foreColor, out float width, out float height, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) )
        {
            this.TextDrawDimensionsInternal( text, fontSize, foreColor, out width, out height, fontFamily, fontWeight );
        }

        public void DrawText( string text, Rect dstBoundingRect, AlignmentX alignX, AlignmentY alignY, Color foreColor, float fontSize, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) )
        {
            float width;
            float height;
            IEnumerable<ISprite2D> sprite2Ds = this.TextDrawDimensionsInternal(text, fontSize, foreColor, out width, out height, fontFamily, fontWeight);
            if ( ( double ) width > dstBoundingRect.Width || ( double ) height > dstBoundingRect.Height )
                return;
            Point startPoint = RenderContextBase.GetStartPoint(dstBoundingRect, new Rect(new Size((double) width, (double) height)), alignX, alignY);
            double x = startPoint.X;
            foreach ( ISprite2D srcSprite in sprite2Ds )
            {
                this.DrawSprite( srcSprite, new Rect( 0.0, 0.0, ( double ) srcSprite.Width, ( double ) srcSprite.Height ), new Point( x, startPoint.Y ) );
                x += ( double ) srcSprite.Width;
            }
        }

        public void DrawText( string text, Point basePoint, AlignmentX alignX, AlignmentY alignY, Color foreColor, float fontSize, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) )
        {
            float width;
            float height;
            IEnumerable<ISprite2D> sprite2Ds = this.TextDrawDimensionsInternal(text, fontSize, foreColor, out width, out height, fontFamily, fontWeight);
            Point startPoint = RenderContextBase.GetStartPoint(basePoint, new Rect(new Size((double) width, (double) height)), alignX, alignY);
            double x = startPoint.X;
            foreach ( ISprite2D srcSprite in sprite2Ds )
            {
                this.DrawSprite( srcSprite, new Rect( 0.0, 0.0, ( double ) srcSprite.Width, ( double ) srcSprite.Height ), new Point( x, startPoint.Y ) );
                x += ( double ) srcSprite.Width;
            }
        }

        public Size DigitMaxSize( float fontSize, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) )
        {
            if ( fontFamily == null )
                fontFamily = "Tahoma";
            if ( fontWeight == new FontWeight() )
                fontWeight = FontWeights.Regular;
            fontSize = fontSize.Round( 0.5f );
            Tuple<string, float, FontWeight> key = Tuple.Create<string, float, FontWeight>(fontFamily, fontSize, fontWeight);
            Size size;
            if ( this._textureCache.MaxDigitSizeDict.TryGetValue( key, out size ) )
                return size;
            double width = double.MinValue;
            double height = double.MinValue;
            foreach ( ISprite2D sprite2D in ( ( IEnumerable<char> ) new char[ 10 ]
            {
        '0',
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8',
        '9'
            } ).Select<char, ISprite2D>( ( Func<char, ISprite2D> ) ( d => this.GetCharSprite( d, fontFamily, fontSize, fontWeight, Colors.White ) ) ) )
            {
                if ( ( double ) sprite2D.Width > width )
                    width = ( double ) sprite2D.Width;
                if ( ( double ) sprite2D.Height > height )
                    height = ( double ) sprite2D.Height;
            }
            size = new Size( width, height );
            this._textureCache.MaxDigitSizeDict[ key ] = size;
            return size;
        }

        private IEnumerable<ISprite2D> TextDrawDimensionsInternal( string text, float fontSize, Color foreColor, out float width, out float height, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) )
        {
            if ( fontFamily == null )
                fontFamily = "Tahoma";
            if ( fontWeight == new FontWeight() )
                fontWeight = FontWeights.Regular;
            fontSize = fontSize.Round( 0.5f );
            ISprite2D[] array = text.Select<char, ISprite2D>((Func<char, ISprite2D>) (character => this.GetCharSprite(character, fontFamily, fontSize, fontWeight, foreColor))).ToArray<ISprite2D>();
            if ( array.Length == 0 )
            {
                width = height = 0.0f;
                return ( IEnumerable<ISprite2D> ) array;
            }
            width = ( ( IEnumerable<ISprite2D> ) array ).Sum<ISprite2D>( ( Func<ISprite2D, float> ) ( x => x.Width ) );
            height = ( ( IEnumerable<ISprite2D> ) array ).Max<ISprite2D>( ( Func<ISprite2D, float> ) ( x => x.Height ) );
            return ( IEnumerable<ISprite2D> ) array;
        }

        private ISprite2D GetCharSprite( char character, string fontFamily, float fontSize, FontWeight fontWeight, Color color )
        {
            CharSpriteKey key = new CharSpriteKey()
            {
                Character = character,
                ForeColor = color,
                FontFamily = fontFamily,
                FontWeight = fontWeight,
                FontSize = fontSize
            };
            ISprite2D sprite;
            if ( !this._textureCache.FontCache.TryGetValue( key, out sprite ) )
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = new string( character, 1 );
                textBlock.Foreground = ( Brush ) new SolidColorBrush( color );
                textBlock.FontFamily = RenderContextBase.GetFontByName( fontFamily );
                textBlock.FontSize = ( double ) fontSize;
                textBlock.FontWeight = fontWeight;
                textBlock.Margin = new Thickness( 0.0 );
                sprite = this.CreateSprite( ( FrameworkElement ) textBlock );
                this._textureCache.FontCache.Add( key, sprite );
            }
            return sprite;
        }

        public abstract IPathDrawingContext BeginLine( IPen2D pen, double startX, double startY );

        public abstract IPathDrawingContext BeginPolygon( IBrush2D brush, double startX, double startY, double gradientRotationAngle = 0.0 );

        internal static bool ClipLine( ref Point pt1, ref Point pt2, Size viewportSize )
        {
            Rect extents = new Rect(0.0, 0.0, viewportSize.Width, viewportSize.Height);
            if ( pt1.IsInBounds( viewportSize ) && pt2.IsInBounds( viewportSize ) )
                return true;
            double x0 = pt1.X.ClipToInt();
            double y0 = pt1.Y.ClipToInt();
            double x1 = pt2.X.ClipToInt();
            double y1 = pt2.Y.ClipToInt();
            int num = WriteableBitmapExtensions.CohenSutherlandLineClip(extents, ref x0, ref y0, ref x1, ref y1) ? 1 : 0;
            pt1.X = x0;
            pt1.Y = y0;
            pt2.X = x1;
            pt2.Y = y1;
            return num != 0;
        }

        protected void ClipRectangle( ref Point pt1, ref Point pt2, int yExtension, int xExtension )
        {
            pt1 = pt1.ClipPoint( this.ViewportSize, yExtension, xExtension );
            pt2 = pt2.ClipPoint( this.ViewportSize, yExtension, xExtension );
        }

        protected void ClipRectangle( ref Point pt1, ref Point pt2 )
        {
            pt1 = pt1.ClipPoint( this.ViewportSize, 0, 0 );
            pt2 = pt2.ClipPoint( this.ViewportSize, 0, 0 );
        }

        protected double ClipZeroLineForArea( double zeroLine, bool isVerticalChart )
        {
            if ( zeroLine < 0.0 )
                return 0.0;
            if ( isVerticalChart )
            {
                double num = zeroLine;
                Size viewportSize = this.ViewportSize;
                double width = viewportSize.Width;
                if ( num > width )
                {
                    viewportSize = this.ViewportSize;
                    return viewportSize.Width;
                }
            }
            else
            {
                double num = zeroLine;
                Size viewportSize = this.ViewportSize;
                double height = viewportSize.Height;
                if ( num > height )
                {
                    viewportSize = this.ViewportSize;
                    return viewportSize.Height;
                }
            }
            return zeroLine;
        }

        protected IEnumerable<Point> ClipArea( IEnumerable<Point> points, int xExtension = 0, int yExtension = 0 )
        {
            return PointUtil.ClipPolygon( points, this.ViewportSize, xExtension, yExtension );
        }

        protected bool IsInBounds( Point pt )
        {
            if ( pt.X >= 0.0 )
            {
                double x = pt.X;
                Size viewportSize = this.ViewportSize;
                double width = viewportSize.Width;
                if ( x <= width && pt.Y >= 0.0 )
                {
                    double y = pt.Y;
                    viewportSize = this.ViewportSize;
                    double height = viewportSize.Height;
                    return y <= height;
                }
            }
            return false;
        }

        protected IEnumerable<Point> ClipArea( IEnumerable<Tuple<Point, Point>> lines )
        {
            Tuple<Point, Point>[] array = lines.ToArray<Tuple<Point, Point>>();
            return this.ClipArea( ( ( IEnumerable<Tuple<Point, Point>> ) array ).Select<Tuple<Point, Point>, Point>( ( Func<Tuple<Point, Point>, Point> ) ( line => line.Item2 ) ), 0, 0 ).Concat<Point>( this.ClipArea( ( ( IEnumerable<Tuple<Point, Point>> ) array ).Select<Tuple<Point, Point>, Point>( ( Func<Tuple<Point, Point>, Point> ) ( line => line.Item1 ) ), 0, 0 ).Reverse<Point>() );
        }

        private static Point GetStartPoint( Rect outerRect, Rect innerRect, AlignmentX alignX, AlignmentY alignY )
        {
            if ( innerRect.Width > outerRect.Width || innerRect.Height > outerRect.Height )
                throw new ArgumentOutOfRangeException( nameof( innerRect ) );
            double x;
            switch ( alignX )
            {
                case AlignmentX.Left:
                    x = outerRect.Left;
                    break;
                case AlignmentX.Right:
                    x = outerRect.Right - innerRect.Width;
                    break;
                default:
                    x = outerRect.Left + outerRect.Width / 2.0 - innerRect.Width / 2.0;
                    break;
            }
            double y;
            switch ( alignY )
            {
                case AlignmentY.Top:
                    y = outerRect.Top;
                    break;
                case AlignmentY.Bottom:
                    y = outerRect.Bottom - innerRect.Height;
                    break;
                default:
                    y = outerRect.Top + outerRect.Height / 2.0 - innerRect.Height / 2.0;
                    break;
            }
            return new Point( x, y );
        }

        private static Point GetStartPoint( Point basePoint, Rect rect, AlignmentX alignX, AlignmentY alignY )
        {
            double x;
            switch ( alignX )
            {
                case AlignmentX.Left:
                    x = basePoint.X;
                    break;
                case AlignmentX.Right:
                    x = basePoint.X - rect.Width;
                    break;
                default:
                    x = basePoint.X - rect.Width / 2.0;
                    break;
            }
            double y;
            switch ( alignY )
            {
                case AlignmentY.Top:
                    y = basePoint.Y;
                    break;
                case AlignmentY.Bottom:
                    y = basePoint.Y - rect.Height;
                    break;
                default:
                    y = basePoint.Y - rect.Height / 2.0;
                    break;
            }
            return new Point( x, y );
        }

        private static FontFamily GetFontByName( string fontName )
        {
            FontFamily fontFamily1;
            if ( RenderContextBase._fonts.TryGetValue( fontName, out fontFamily1 ) )
                return fontFamily1;
            FontFamily fontFamily2 = new FontFamily(fontName);
            RenderContextBase._fonts.Add( fontName, fontFamily2 );
            return fontFamily2;
        }
    }
}
