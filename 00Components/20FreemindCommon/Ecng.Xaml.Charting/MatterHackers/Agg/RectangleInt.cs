// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.RectangleInt
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal struct RectangleInt
    {
        public int Left;
        public int Bottom;
        public int Right;
        public int Top;

        public RectangleInt( int x1_, int y1_, int x2_, int y2_ )
        {
            this.Left = x1_;
            this.Bottom = y1_;
            this.Right = x2_;
            this.Top = y2_;
        }

        public void SetRect( int left, int bottom, int right, int top )
        {
            this.init( left, bottom, right, top );
        }

        public void init( int x1_, int y1_, int x2_, int y2_ )
        {
            this.Left = x1_;
            this.Bottom = y1_;
            this.Right = x2_;
            this.Top = y2_;
        }

        public int Width
        {
            get
            {
                return this.Right - this.Left;
            }
        }

        public int Height
        {
            get
            {
                return this.Top - this.Bottom;
            }
        }

        public RectangleInt normalize()
        {
            if ( this.Left > this.Right )
            {
                int left = this.Left;
                this.Left = this.Right;
                this.Right = left;
            }
            if ( this.Bottom > this.Top )
            {
                int bottom = this.Bottom;
                this.Bottom = this.Top;
                this.Top = bottom;
            }
            return this;
        }

        public bool clip( RectangleInt r )
        {
            if ( this.Right > r.Right )
                this.Right = r.Right;
            if ( this.Top > r.Top )
                this.Top = r.Top;
            if ( this.Left < r.Left )
                this.Left = r.Left;
            if ( this.Bottom < r.Bottom )
                this.Bottom = r.Bottom;
            if ( this.Left <= this.Right )
                return this.Bottom <= this.Top;
            return false;
        }

        public bool is_valid()
        {
            if ( this.Left <= this.Right )
                return this.Bottom <= this.Top;
            return false;
        }

        public bool hit_test( int x, int y )
        {
            if ( x >= this.Left && x <= this.Right && y >= this.Bottom )
                return y <= this.Top;
            return false;
        }

        public bool IntersectRectangles( RectangleInt rectToCopy, RectangleInt rectToIntersectWith )
        {
            this.Left = rectToCopy.Left;
            this.Bottom = rectToCopy.Bottom;
            this.Right = rectToCopy.Right;
            this.Top = rectToCopy.Top;
            if ( this.Left < rectToIntersectWith.Left )
                this.Left = rectToIntersectWith.Left;
            if ( this.Bottom < rectToIntersectWith.Bottom )
                this.Bottom = rectToIntersectWith.Bottom;
            if ( this.Right > rectToIntersectWith.Right )
                this.Right = rectToIntersectWith.Right;
            if ( this.Top > rectToIntersectWith.Top )
                this.Top = rectToIntersectWith.Top;
            return this.Left < this.Right && this.Bottom < this.Top;
        }

        public bool IntersectWithRectangle( RectangleInt rectToIntersectWith )
        {
            if ( this.Left < rectToIntersectWith.Left )
                this.Left = rectToIntersectWith.Left;
            if ( this.Bottom < rectToIntersectWith.Bottom )
                this.Bottom = rectToIntersectWith.Bottom;
            if ( this.Right > rectToIntersectWith.Right )
                this.Right = rectToIntersectWith.Right;
            if ( this.Top > rectToIntersectWith.Top )
                this.Top = rectToIntersectWith.Top;
            return this.Left < this.Right && this.Bottom < this.Top;
        }

        public static bool DoIntersect( RectangleInt rect1, RectangleInt rect2 )
        {
            int left = rect1.Left;
            int bottom = rect1.Bottom;
            int right = rect1.Right;
            int top = rect1.Top;
            if ( left < rect2.Left )
                left = rect2.Left;
            if ( bottom < rect2.Bottom )
                bottom = rect2.Bottom;
            if ( right > rect2.Right )
                right = rect2.Right;
            if ( top > rect2.Top )
                top = rect2.Top;
            return left < right && bottom < top;
        }

        public void unite_rectangles( RectangleInt r1, RectangleInt r2 )
        {
            this.Left = r1.Left;
            this.Bottom = r1.Bottom;
            this.Right = r1.Right;
            this.Right = r1.Top;
            if ( this.Right < r2.Right )
                this.Right = r2.Right;
            if ( this.Top < r2.Top )
                this.Top = r2.Top;
            if ( this.Left > r2.Left )
                this.Left = r2.Left;
            if ( this.Bottom <= r2.Bottom )
                return;
            this.Bottom = r2.Bottom;
        }

        public void Inflate( int inflateSize )
        {
            this.Left -= inflateSize;
            this.Bottom -= inflateSize;
            this.Right += inflateSize;
            this.Top += inflateSize;
        }

        public void Offset( int x, int y )
        {
            this.Left += x;
            this.Bottom += y;
            this.Right += x;
            this.Top += y;
        }

        public static bool ClipRects( RectangleInt pBoundingRect, ref RectangleInt pSourceRect, ref RectangleInt pDestRect )
        {
            if ( pDestRect.Top < pBoundingRect.Top )
            {
                if ( pSourceRect.Height != pDestRect.Height )
                    throw new Exception( "source and dest rects must have the same height" );
                pSourceRect.Top += pBoundingRect.Top - pDestRect.Top;
                pDestRect.Top = pBoundingRect.Top;
                if ( pDestRect.Top >= pDestRect.Bottom )
                    return false;
            }
            if ( pDestRect.Bottom > pBoundingRect.Bottom )
            {
                if ( pSourceRect.Height != pDestRect.Height )
                    throw new Exception( "source and dest rects must have the same height" );
                pSourceRect.Bottom -= pDestRect.Bottom - pBoundingRect.Bottom;
                pDestRect.Bottom = pBoundingRect.Bottom;
                if ( pDestRect.Bottom <= pDestRect.Top )
                    return false;
            }
            if ( pDestRect.Left < pBoundingRect.Left )
            {
                if ( pSourceRect.Width != pDestRect.Width )
                    throw new Exception( "source and dest rects must have the same width" );
                pSourceRect.Left += pBoundingRect.Left - pDestRect.Left;
                pDestRect.Left = pBoundingRect.Left;
                if ( pDestRect.Left >= pDestRect.Right )
                    return false;
            }
            if ( pDestRect.Right > pBoundingRect.Right )
            {
                if ( pSourceRect.Width != pDestRect.Width )
                    throw new Exception( "source and dest rects must have the same width" );
                pSourceRect.Right -= pDestRect.Right - pBoundingRect.Right;
                pDestRect.Right = pBoundingRect.Right;
                if ( pDestRect.Right <= pDestRect.Left )
                    return false;
            }
            return true;
        }

        public static bool ClipRect( RectangleInt pBoundingRect, ref RectangleInt pDestRect )
        {
            if ( pDestRect.Top < pBoundingRect.Top )
            {
                pDestRect.Top = pBoundingRect.Top;
                if ( pDestRect.Top >= pDestRect.Bottom )
                    return false;
            }
            if ( pDestRect.Bottom > pBoundingRect.Bottom )
            {
                pDestRect.Bottom = pBoundingRect.Bottom;
                if ( pDestRect.Bottom <= pDestRect.Top )
                    return false;
            }
            if ( pDestRect.Left < pBoundingRect.Left )
            {
                pDestRect.Left = pBoundingRect.Left;
                if ( pDestRect.Left >= pDestRect.Right )
                    return false;
            }
            if ( pDestRect.Right > pBoundingRect.Right )
            {
                pDestRect.Right = pBoundingRect.Right;
                if ( pDestRect.Right <= pDestRect.Left )
                    return false;
            }
            return true;
        }
    }
}
