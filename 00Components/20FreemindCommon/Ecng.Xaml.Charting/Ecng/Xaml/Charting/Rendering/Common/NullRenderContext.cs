// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Rendering.Common.NullRenderContext
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting.Rendering.Common
{
    public class NullRenderContext : IRenderContext2D, IDisposable
    {
        public RenderOperationLayers Layers
        {
            get
            {
                return new RenderOperationLayers();
            }
        }

        public Size ViewportSize
        {
            get
            {
                return new Size();
            }
        }

        public void SetPrimitvesCachingEnabled( bool bEnabled )
        {
        }

        public IBrush2D CreateBrush( Color color, double opacity = 1.0, bool? alphaBlend = null )
        {
            return ( IBrush2D ) null;
        }

        public IBrush2D CreateBrush( Brush brush, double opacity = 1.0, TextureMappingMode textureMappingMode = TextureMappingMode.PerScreen )
        {
            return ( IBrush2D ) null;
        }

        public IPen2D CreatePen( Color color, bool antiAliasing, float strokeThickness, double opacity = 1.0, double[ ] strokeDashArray = null, PenLineCap strokeEndLineCap = PenLineCap.Round )
        {
            return ( IPen2D ) null;
        }

        public ISprite2D CreateSprite( FrameworkElement fe )
        {
            return ( ISprite2D ) null;
        }

        public void Clear()
        {
        }

        public void DrawSprite( ISprite2D srcSprite, Rect srcRect, Point destPoint )
        {
        }

        public void DrawSprites( ISprite2D sprite2D, Rect srcRect, IEnumerable<Point> points )
        {
        }

        public void DrawSprites( ISprite2D sprite2D, IEnumerable<Rect> dstRects )
        {
        }

        public void FillRectangle( IBrush2D brush, Point pt1, Point pt2, double gradientRotationAngle = 0.0 )
        {
        }

        public void FillPolygon( IBrush2D brush, IEnumerable<Point> points )
        {
        }

        public void FillArea( IBrush2D brush, IEnumerable<Tuple<Point, Point>> lines, bool isVerticalChart = false, double gradientRotationAngle = 0.0 )
        {
        }

        public void DrawQuad( IPen2D pen, Point pt1, Point pt2 )
        {
        }

        public void DrawEllipse( IPen2D strokePen, IBrush2D fillBrush, Point center, double width, double height )
        {
        }

        public void DrawEllipses( IPen2D strokePen, IBrush2D fillBrush, IEnumerable<Point> centres, double width, double height )
        {
        }

        public void DrawLine( IPen2D pen, Point pt1, Point pt2 )
        {
        }

        public void DrawLines( IPen2D pen, IEnumerable<Point> points )
        {
        }

        public void DisposeResourceAfterDraw( IDisposable disposable )
        {
        }

        public void DrawPixelsVertically( int x, int yStartBottom, int yEndTop, IList<int> pixelColorsArgb, double opacity, bool yAxisIsFlipped )
        {
        }

        public void TextDrawDimensions( string text, float fontSize, Color foreColor, out float width, out float height, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) )
        {
            width = height = 0.0f;
        }

        public Size DigitMaxSize( float fontSize, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) )
        {
            return Size.Empty;
        }

        public void DrawText( string text, Rect dstBoundingRect, AlignmentX alignX, AlignmentY alignY, Color foreColor, float fontSize, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) )
        {
        }

        public void DrawText( string text, Point basePoint, AlignmentX alignX, AlignmentY alignY, Color foreColor, float fontSize, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) )
        {
        }

        public IPathDrawingContext BeginLine( IPen2D pen, double startX, double startY )
        {
            return ( IPathDrawingContext ) null;
        }

        public IPathDrawingContext BeginPolygon( IBrush2D brush, double startX, double startY, double gradientRotationAngle = 0.0 )
        {
            return ( IPathDrawingContext ) null;
        }

        public void Dispose()
        {
        }
    }
}
