// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Rendering.Common.IRenderContext2D
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace StockSharp.Xaml.Charting.Rendering.Common
{
    public interface IRenderContext2D : IDisposable
    {
        RenderOperationLayers Layers
        {
            get;
        }

        Size ViewportSize
        {
            get;
        }

        void SetPrimitvesCachingEnabled( bool bEnabled );

        IBrush2D CreateBrush( Color color, double opacity = 1.0, bool? alphaBlend = null );

        IBrush2D CreateBrush( Brush brush, double opacity = 1.0, TextureMappingMode textureMappingMode = TextureMappingMode.PerPrimitive );

        IPen2D CreatePen( Color color, bool antiAliasing, float strokeThickness, double opacity = 1.0, double[ ] strokeDashArray = null, PenLineCap strokeEndLineCap = PenLineCap.Round );

        ISprite2D CreateSprite( FrameworkElement fe );

        void Clear();

        void DrawSprite( ISprite2D srcSprite, Rect srcRect, Point destPoint );

        void DrawSprites( ISprite2D sprite2D, Rect srcRect, IEnumerable<Point> points );

        void DrawSprites( ISprite2D sprite2D, IEnumerable<Rect> dstRects );

        void FillRectangle( IBrush2D brush, Point pt1, Point pt2, double gradientRotationAngle = 0.0 );

        void FillPolygon( IBrush2D brush, IEnumerable<Point> points );

        void FillArea( IBrush2D brush, IEnumerable<Tuple<Point, Point>> lines, bool isVerticalChart = false, double gradientRotationAngle = 0.0 );

        void DrawQuad( IPen2D pen, Point pt1, Point pt2 );

        void DrawEllipse( IPen2D strokePen, IBrush2D fillBrush, Point center, double width, double height );

        void DrawEllipses( IPen2D strokePen, IBrush2D fillBrush, IEnumerable<Point> centres, double width, double height );

        void DrawLine( IPen2D pen, Point pt1, Point pt2 );

        void DrawLines( IPen2D pen, IEnumerable<Point> points );

        void DisposeResourceAfterDraw( IDisposable disposable );

        void DrawPixelsVertically( int x, int yStartBottom, int yEndTop, IList<int> pixelColorsArgb, double opacity, bool yAxisIsFlipped );

        void TextDrawDimensions( string text, float fontSize, Color foreColor, out float width, out float height, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) );

        Size DigitMaxSize( float fontSize, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) );

        void DrawText( string text, Rect dstBoundingRect, AlignmentX alignX, AlignmentY alignY, Color foreColor, float fontSize, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) );

        void DrawText( string text, Point basePoint, AlignmentX alignX, AlignmentY alignY, Color foreColor, float fontSize, string fontFamily = null, FontWeight fontWeight = default( FontWeight ) );

        IPathDrawingContext BeginLine( IPen2D pen, double startX, double startY );

        IPathDrawingContext BeginPolygon( IBrush2D brush, double startX, double startY, double gradientRotationAngle = 0.0 );
    }
}
