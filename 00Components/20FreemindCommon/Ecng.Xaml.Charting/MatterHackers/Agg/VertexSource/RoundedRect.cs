// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.RoundedRect
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.VertexSource
{
    internal class RoundedRect : IVertexSource
    {
        private arc currentProcessingArc = new arc();
        private RectangleDouble bounds;
        private Vector2 leftBottomRadius;
        private Vector2 rightBottomRadius;
        private Vector2 rightTopRadius;
        private Vector2 leftTopRadius;
        private int state;

        public RoundedRect( double left, double bottom, double right, double top, double radius )
        {
            this.bounds = new RectangleDouble( left, bottom, right, top );
            this.leftBottomRadius.x = radius;
            this.leftBottomRadius.y = radius;
            this.rightBottomRadius.x = radius;
            this.rightBottomRadius.y = radius;
            this.rightTopRadius.x = radius;
            this.rightTopRadius.y = radius;
            this.leftTopRadius.x = radius;
            this.leftTopRadius.y = radius;
            if ( left > right )
            {
                this.bounds.Left = right;
                this.bounds.Right = left;
            }
            if ( bottom <= top )
                return;
            this.bounds.Bottom = top;
            this.bounds.Top = bottom;
        }

        public RoundedRect( RectangleDouble bounds, double r )
          : this( bounds.Left, bounds.Bottom, bounds.Right, bounds.Top, r )
        {
        }

        public RoundedRect( RectangleInt bounds, double r )
          : this( ( double ) bounds.Left, ( double ) bounds.Bottom, ( double ) bounds.Right, ( double ) bounds.Top, r )
        {
        }

        public void rect( double left, double bottom, double right, double top )
        {
            this.bounds = new RectangleDouble( left, bottom, right, top );
            if ( left > right )
            {
                this.bounds.Left = right;
                this.bounds.Right = left;
            }
            if ( bottom <= top )
                return;
            this.bounds.Bottom = top;
            this.bounds.Top = bottom;
        }

        public void radius( double r )
        {
            this.leftBottomRadius.x = this.leftBottomRadius.y = this.rightBottomRadius.x = this.rightBottomRadius.y = this.rightTopRadius.x = this.rightTopRadius.y = this.leftTopRadius.x = this.leftTopRadius.y = r;
        }

        public void radius( double rx, double ry )
        {
            this.leftBottomRadius.x = this.rightBottomRadius.x = this.rightTopRadius.x = this.leftTopRadius.x = rx;
            this.leftBottomRadius.y = this.rightBottomRadius.y = this.rightTopRadius.y = this.leftTopRadius.y = ry;
        }

        public void radius( double leftBottomRadius, double rightBottomRadius, double rightTopRadius, double leftTopRadius )
        {
            this.leftBottomRadius = new Vector2( leftBottomRadius, leftBottomRadius );
            this.rightBottomRadius = new Vector2( rightBottomRadius, rightBottomRadius );
            this.rightTopRadius = new Vector2( rightTopRadius, rightTopRadius );
            this.leftTopRadius = new Vector2( leftTopRadius, leftTopRadius );
        }

        public void radius( double rx1, double ry1, double rx2, double ry2, double rx3, double ry3, double rx4, double ry4 )
        {
            this.leftBottomRadius.x = rx1;
            this.leftBottomRadius.y = ry1;
            this.rightBottomRadius.x = rx2;
            this.rightBottomRadius.y = ry2;
            this.rightTopRadius.x = rx3;
            this.rightTopRadius.y = ry3;
            this.leftTopRadius.x = rx4;
            this.leftTopRadius.y = ry4;
        }

        public void normalize_radius()
        {
            double num1 = Math.Abs(this.bounds.Top - this.bounds.Bottom);
            double num2 = Math.Abs(this.bounds.Right - this.bounds.Left);
            double num3 = 1.0;
            double num4 = num1 / (this.leftBottomRadius.x + this.rightBottomRadius.x);
            if ( num4 < num3 )
                num3 = num4;
            double num5 = num1 / (this.rightTopRadius.x + this.leftTopRadius.x);
            if ( num5 < num3 )
                num3 = num5;
            double num6 = num2 / (this.leftBottomRadius.y + this.rightBottomRadius.y);
            if ( num6 < num3 )
                num3 = num6;
            double num7 = num2 / (this.rightTopRadius.y + this.leftTopRadius.y);
            if ( num7 < num3 )
                num3 = num7;
            if ( num3 >= 1.0 )
                return;
            this.leftBottomRadius.x *= num3;
            this.leftBottomRadius.y *= num3;
            this.rightBottomRadius.x *= num3;
            this.rightBottomRadius.y *= num3;
            this.rightTopRadius.x *= num3;
            this.rightTopRadius.y *= num3;
            this.leftTopRadius.x *= num3;
            this.leftTopRadius.y *= num3;
        }

        public void approximation_scale( double s )
        {
            this.currentProcessingArc.approximation_scale( s );
        }

        public double approximation_scale()
        {
            return this.currentProcessingArc.approximation_scale();
        }

        public void rewind( int unused )
        {
            this.state = 0;
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            x = 0.0;
            y = 0.0;
            Path.FlagsAndCommand flagsAndCommand = Path.FlagsAndCommand.CommandStop;
            switch ( this.state )
            {
                case 0:
                    this.currentProcessingArc.init( this.bounds.Left + this.leftBottomRadius.x, this.bounds.Bottom + this.leftBottomRadius.y, this.leftBottomRadius.x, this.leftBottomRadius.y, Math.PI, 3.0 * Math.PI / 2.0 );
                    this.currentProcessingArc.rewind( 0 );
                    ++this.state;
                    goto case 1;
                case 1:
                    Path.FlagsAndCommand c = this.currentProcessingArc.vertex(out x, out y);
                    if ( !Path.is_stop( c ) )
                        return c;
                    ++this.state;
                    goto case 2;
                case 2:
                    this.currentProcessingArc.init( this.bounds.Right - this.rightBottomRadius.x, this.bounds.Bottom + this.rightBottomRadius.y, this.rightBottomRadius.x, this.rightBottomRadius.y, 3.0 * Math.PI / 2.0, 0.0 );
                    this.currentProcessingArc.rewind( 0 );
                    ++this.state;
                    goto case 3;
                case 3:
                    if ( !Path.is_stop( this.currentProcessingArc.vertex( out x, out y ) ) )
                        return Path.FlagsAndCommand.CommandLineTo;
                    ++this.state;
                    goto case 4;
                case 4:
                    this.currentProcessingArc.init( this.bounds.Right - this.rightTopRadius.x, this.bounds.Top - this.rightTopRadius.y, this.rightTopRadius.x, this.rightTopRadius.y, 0.0, Math.PI / 2.0 );
                    this.currentProcessingArc.rewind( 0 );
                    ++this.state;
                    goto case 5;
                case 5:
                    if ( !Path.is_stop( this.currentProcessingArc.vertex( out x, out y ) ) )
                        return Path.FlagsAndCommand.CommandLineTo;
                    ++this.state;
                    goto case 6;
                case 6:
                    this.currentProcessingArc.init( this.bounds.Left + this.leftTopRadius.x, this.bounds.Top - this.leftTopRadius.y, this.leftTopRadius.x, this.leftTopRadius.y, Math.PI / 2.0, Math.PI );
                    this.currentProcessingArc.rewind( 0 );
                    ++this.state;
                    goto case 7;
                case 7:
                    if ( !Path.is_stop( this.currentProcessingArc.vertex( out x, out y ) ) )
                        return Path.FlagsAndCommand.CommandLineTo;
                    ++this.state;
                    goto case 8;
                case 8:
                    flagsAndCommand = Path.FlagsAndCommand.CommandEndPoly | Path.FlagsAndCommand.FlagCCW | Path.FlagsAndCommand.FlagClose;
                    ++this.state;
                    break;
            }
            return flagsAndCommand;
        }
    }
}
