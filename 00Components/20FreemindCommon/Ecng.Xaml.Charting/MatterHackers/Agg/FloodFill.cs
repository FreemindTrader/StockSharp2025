// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.FloodFill
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal class FloodFill
    {
        private FirstInFirstOutQueue<FloodFill.Range> ranges = new FirstInFirstOutQueue<FloodFill.Range>(9);
        private ImageBuffer destImage;
        protected int imageStride;
        protected byte[] destBuffer;
        protected bool[] pixelsChecked;
        private FloodFill.FillingRule fillRule;

        public FloodFill( RGBA_Bytes fillColor )
        {
            this.fillRule = ( FloodFill.FillingRule ) new FloodFill.ExactMatch( fillColor );
        }

        public FloodFill( RGBA_Bytes fillColor, int tolerance0To255 )
        {
            if ( tolerance0To255 > 0 )
                this.fillRule = ( FloodFill.FillingRule ) new FloodFill.ToleranceMatch( fillColor, tolerance0To255 );
            else
                this.fillRule = ( FloodFill.FillingRule ) new FloodFill.ExactMatch( fillColor );
        }

        public FloodFill( FloodFill.FillingRule fillRule )
        {
            this.fillRule = fillRule;
        }

        public void Fill( ImageBuffer bufferToFillOn, int x, int y )
        {
            if ( ( long ) ( uint ) x > ( long ) bufferToFillOn.Width || ( long ) ( uint ) y > ( long ) bufferToFillOn.Height )
                return;
            this.destImage = bufferToFillOn;
            this.imageStride = this.destImage.StrideInBytes();
            this.destBuffer = this.destImage.GetBuffer();
            int width = this.destImage.Width;
            int height = this.destImage.Height;
            this.pixelsChecked = new bool[ this.destImage.Width * this.destImage.Height ];
            int bufferOffsetXy = this.destImage.GetBufferOffsetXY(x, y);
            this.fillRule.SetStartColor( new RGBA_Bytes( ( int ) this.destImage.GetBuffer()[ bufferOffsetXy + 2 ], ( int ) this.destImage.GetBuffer()[ bufferOffsetXy + 1 ], ( int ) this.destImage.GetBuffer()[ bufferOffsetXy ] ) );
            this.LinearFill( x, y );
            while ( this.ranges.Count > 0 )
            {
                FloodFill.Range range = this.ranges.Dequeue();
                int y1 = range.y - 1;
                int y2 = range.y + 1;
                int index1 = width * (range.y - 1) + range.startX;
                int index2 = width * (range.y + 1) + range.startX;
                for ( int startX = range.startX ; startX <= range.endX ; ++startX )
                {
                    if ( range.y > 0 && !this.pixelsChecked[ index1 ] && this.fillRule.CheckPixel( this.destBuffer, this.destImage.GetBufferOffsetXY( startX, y1 ) ) )
                        this.LinearFill( startX, y1 );
                    if ( range.y < height - 1 && !this.pixelsChecked[ index2 ] && this.fillRule.CheckPixel( this.destBuffer, this.destImage.GetBufferOffsetXY( startX, y2 ) ) )
                        this.LinearFill( startX, y2 );
                    ++index2;
                    ++index1;
                }
            }
        }

        private void LinearFill( int x, int y )
        {
            int betweenPixelsInclusive = this.destImage.GetBytesBetweenPixelsInclusive();
            int width = this.destImage.Width;
            int num1 = x;
            int bufferOffsetXy1 = this.destImage.GetBufferOffsetXY(x, y);
            int index1 = width * y + x;
            do
            {
                this.fillRule.SetPixel( this.destBuffer, bufferOffsetXy1 );
                this.pixelsChecked[ index1 ] = true;
                --num1;
                --index1;
                bufferOffsetXy1 -= betweenPixelsInclusive;
            }
            while ( num1 > 0 && !this.pixelsChecked[ index1 ] && this.fillRule.CheckPixel( this.destBuffer, bufferOffsetXy1 ) );
            int startX = num1 + 1;
            int num2 = x;
            int bufferOffsetXy2 = this.destImage.GetBufferOffsetXY(x, y);
            int index2 = width * y + x;
            do
            {
                this.fillRule.SetPixel( this.destBuffer, bufferOffsetXy2 );
                this.pixelsChecked[ index2 ] = true;
                ++num2;
                ++index2;
                bufferOffsetXy2 += betweenPixelsInclusive;
            }
            while ( num2 < width && !this.pixelsChecked[ index2 ] && this.fillRule.CheckPixel( this.destBuffer, bufferOffsetXy2 ) );
            int endX = num2 - 1;
            this.ranges.Enqueue( new FloodFill.Range( startX, endX, y ) );
        }

        internal abstract class FillingRule
        {
            protected RGBA_Bytes startColor;
            protected RGBA_Bytes fillColor;

            protected FillingRule( RGBA_Bytes fillColor )
            {
                this.fillColor = fillColor;
            }

            public void SetStartColor( RGBA_Bytes startColor )
            {
                this.startColor = startColor;
            }

            public virtual void SetPixel( byte[ ] destBuffer, int bufferOffset )
            {
                destBuffer[ bufferOffset ] = this.fillColor.blue;
                destBuffer[ bufferOffset + 1 ] = this.fillColor.green;
                destBuffer[ bufferOffset + 2 ] = this.fillColor.red;
            }

            public abstract bool CheckPixel( byte[ ] destBuffer, int bufferOffset );
        }

        internal class ExactMatch : FloodFill.FillingRule
        {
            public ExactMatch( RGBA_Bytes fillColor )
              : base( fillColor )
            {
            }

            public override bool CheckPixel( byte[ ] destBuffer, int bufferOffset )
            {
                if ( ( int ) destBuffer[ bufferOffset ] == ( int ) this.startColor.red && ( int ) destBuffer[ bufferOffset + 1 ] == ( int ) this.startColor.green )
                    return ( int ) destBuffer[ bufferOffset + 2 ] == ( int ) this.startColor.blue;
                return false;
            }
        }

        internal class ToleranceMatch : FloodFill.FillingRule
        {
            private int tolerance0To255;

            public ToleranceMatch( RGBA_Bytes fillColor, int tolerance0To255 )
              : base( fillColor )
            {
                this.tolerance0To255 = tolerance0To255;
            }

            public override bool CheckPixel( byte[ ] destBuffer, int bufferOffset )
            {
                if ( ( int ) destBuffer[ bufferOffset ] >= ( int ) this.startColor.red - this.tolerance0To255 && ( int ) destBuffer[ bufferOffset ] <= ( int ) this.startColor.red + this.tolerance0To255 && ( ( int ) destBuffer[ bufferOffset + 1 ] >= ( int ) this.startColor.green - this.tolerance0To255 && ( int ) destBuffer[ bufferOffset + 1 ] <= ( int ) this.startColor.green + this.tolerance0To255 ) && ( int ) destBuffer[ bufferOffset + 2 ] >= ( int ) this.startColor.blue - this.tolerance0To255 )
                    return ( int ) destBuffer[ bufferOffset + 2 ] <= ( int ) this.startColor.blue + this.tolerance0To255;
                return false;
            }
        }

        private struct Range
        {
            public int startX;
            public int endX;
            public int y;

            public Range( int startX, int endX, int y )
            {
                this.startX = startX;
                this.endX = endX;
                this.y = y;
            }
        }
    }
}
