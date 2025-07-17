// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.BorderDouble
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal struct BorderDouble
    {
        public double Left;
        public double Bottom;
        public double Right;
        public double Top;

        public BorderDouble( double valueForAll )
        {
            this.Left = valueForAll;
            this.Right = valueForAll;
            this.Bottom = valueForAll;
            this.Top = valueForAll;
        }

        public BorderDouble( double leftRight, double bottomTopValue )
        {
            this.Left = leftRight;
            this.Right = leftRight;
            this.Bottom = bottomTopValue;
            this.Top = bottomTopValue;
        }

        public BorderDouble( double left, double bottom, double right, double top )
        {
            this.Left = left;
            this.Bottom = bottom;
            this.Right = right;
            this.Top = top;
        }

        public static bool operator ==( BorderDouble a, BorderDouble b )
        {
            return a.Left == b.Left && a.Bottom == b.Bottom && ( a.Right == b.Right && a.Top == b.Top );
        }

        public static bool operator !=( BorderDouble a, BorderDouble b )
        {
            return a.Left != b.Left || a.Bottom != b.Bottom || ( a.Right != b.Right || a.Top != b.Top );
        }

        public static BorderDouble operator *( BorderDouble a, double b )
        {
            return new BorderDouble( a.Left * b, a.Bottom * b, a.Right * b, a.Top * b );
        }

        public static BorderDouble operator *( double b, BorderDouble a )
        {
            return new BorderDouble( a.Left * b, a.Bottom * b, a.Right * b, a.Top * b );
        }

        public override int GetHashCode()
        {
            return new { x1 = this.Left, x2 = this.Right, y1 = this.Bottom, y2 = this.Top }.GetHashCode();
        }

        public override bool Equals( object obj )
        {
            if ( obj.GetType() == typeof( BorderDouble ) )
                return this == ( BorderDouble ) obj;
            return false;
        }

        public double Width
        {
            get
            {
                return this.Left + this.Right;
            }
        }

        public double Height
        {
            get
            {
                return this.Bottom + this.Top;
            }
        }
    }
}
