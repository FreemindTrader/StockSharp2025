// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.RectangleDouble
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using MatterHackers.VectorMath;

namespace MatterHackers.Agg
{
    internal struct RectangleDouble
    {
        public static readonly RectangleDouble ZeroIntersection = new RectangleDouble(double.MaxValue, double.MaxValue, double.MinValue, double.MinValue);
        public double Left;
        public double Bottom;
        public double Right;
        public double Top;

        public RectangleDouble( double left, double bottom, double right, double top )
        {
            this.Left = left;
            this.Bottom = bottom;
            this.Right = right;
            this.Top = top;
        }

        public RectangleDouble( RectangleInt intRect )
        {
            this.Left = ( double ) intRect.Left;
            this.Bottom = ( double ) intRect.Bottom;
            this.Right = ( double ) intRect.Right;
            this.Top = ( double ) intRect.Top;
        }

        public void SetRect( double left, double bottom, double right, double top )
        {
            this.init( left, bottom, right, top );
        }

        public static bool operator ==( RectangleDouble a, RectangleDouble b )
        {
            return a.Left == b.Left && a.Bottom == b.Bottom && ( a.Right == b.Right && a.Top == b.Top );
        }

        public static bool operator !=( RectangleDouble a, RectangleDouble b )
        {
            return a.Left != b.Left || a.Bottom != b.Bottom || ( a.Right != b.Right || a.Top != b.Top );
        }

        public override int GetHashCode()
        {
            return new { x1 = this.Left, x2 = this.Right, y1 = this.Bottom, y2 = this.Top }.GetHashCode();
        }

        public override bool Equals( object obj )
        {
            if ( obj.GetType() == typeof( RectangleDouble ) )
                return this == ( RectangleDouble ) obj;
            return false;
        }

        public void init( double left, double bottom, double right, double top )
        {
            this.Left = left;
            this.Bottom = bottom;
            this.Right = right;
            this.Top = top;
        }

        public double Width
        {
            get
            {
                return this.Right - this.Left;
            }
        }

        public double Height
        {
            get
            {
                return this.Top - this.Bottom;
            }
        }

        public RectangleDouble normalize()
        {
            if ( this.Left > this.Right )
            {
                double left = this.Left;
                this.Left = this.Right;
                this.Right = left;
            }
            if ( this.Bottom > this.Top )
            {
                double bottom = this.Bottom;
                this.Bottom = this.Top;
                this.Top = bottom;
            }
            return this;
        }

        public bool clip( RectangleDouble r )
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

        public bool Contains( double x, double y )
        {
            if ( x >= this.Left && x <= this.Right && y >= this.Bottom )
                return y <= this.Top;
            return false;
        }

        public bool Contains( RectangleDouble innerRect )
        {
            return this.Contains( innerRect.Left, innerRect.Bottom ) && this.Contains( innerRect.Right, innerRect.Top );
        }

        public bool Contains( Vector2 position )
        {
            return this.Contains( position.x, position.y );
        }

        public bool IntersectRectangles( RectangleDouble rectToCopy, RectangleDouble rectToIntersectWith )
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

        public bool IntersectWithRectangle( RectangleDouble rectToIntersectWith )
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

        public void unite_rectangles( RectangleDouble r1, RectangleDouble r2 )
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

        public void ExpandToInclude( RectangleDouble rectToInclude )
        {
            if ( this.Right < rectToInclude.Right )
                this.Right = rectToInclude.Right;
            if ( this.Top < rectToInclude.Top )
                this.Top = rectToInclude.Top;
            if ( this.Left > rectToInclude.Left )
                this.Left = rectToInclude.Left;
            if ( this.Bottom <= rectToInclude.Bottom )
                return;
            this.Bottom = rectToInclude.Bottom;
        }

        public void ExpandToInclude( double x, double y )
        {
            if ( this.Right < x )
                this.Right = x;
            if ( this.Top < y )
                this.Top = y;
            if ( this.Left > x )
                this.Left = x;
            if ( this.Bottom <= y )
                return;
            this.Bottom = y;
        }

        public void Inflate( double inflateSize )
        {
            this.Left -= inflateSize;
            this.Bottom -= inflateSize;
            this.Right += inflateSize;
            this.Top += inflateSize;
        }

        public void Offset( Vector2 offset )
        {
            this.Offset( offset.x, offset.y );
        }

        public void Offset( double x, double y )
        {
            this.Left += x;
            this.Bottom += y;
            this.Right += x;
            this.Top += y;
        }

        public static RectangleDouble operator *( RectangleDouble a, double b )
        {
            return new RectangleDouble( a.Left * b, a.Bottom * b, a.Right * b, a.Top * b );
        }

        public static RectangleDouble operator *( double b, RectangleDouble a )
        {
            return new RectangleDouble( a.Left * b, a.Bottom * b, a.Right * b, a.Top * b );
        }

        public double XCenter
        {
            get
            {
                return ( this.Right - this.Left ) / 2.0;
            }
        }

        public void Inflate( BorderDouble borderDouble )
        {
            this.Left -= borderDouble.Left;
            this.Right += borderDouble.Right;
            this.Bottom -= borderDouble.Bottom;
            this.Top += borderDouble.Top;
        }
    }
}
