// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_interpolator_linear_float
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Transform;

namespace MatterHackers.Agg
{
    internal sealed class span_interpolator_linear_float : ISpanInterpolatorFloat
    {
        private ITransform m_trans;
        private float currentX;
        private float stepX;
        private float currentY;
        private float stepY;

        public span_interpolator_linear_float()
        {
        }

        public span_interpolator_linear_float( ITransform trans )
        {
            this.m_trans = trans;
        }

        public span_interpolator_linear_float( ITransform trans, double x, double y, int len )
        {
            this.m_trans = trans;
            this.begin( x, y, len );
        }

        public ITransform transformer()
        {
            return this.m_trans;
        }

        public void transformer( ITransform trans )
        {
            this.m_trans = trans;
        }

        public void local_scale( out double x, out double y )
        {
            throw new NotImplementedException();
        }

        public void begin( double x, double y, int len )
        {
            double x1 = x;
            double y1 = y;
            this.m_trans.transform( ref x1, ref y1 );
            this.currentX = ( float ) x1;
            this.currentY = ( float ) y1;
            double x2 = x + (double) len;
            double y2 = y;
            this.m_trans.transform( ref x2, ref y2 );
            this.stepX = ( ( float ) x2 - this.currentX ) / ( float ) len;
            this.stepY = ( ( float ) y2 - this.currentY ) / ( float ) len;
        }

        public void resynchronize( double xe, double ye, int len )
        {
            throw new NotImplementedException();
        }

        public void Next()
        {
            this.currentX += this.stepX;
            this.currentY += this.stepY;
        }

        public void coordinates( out float x, out float y )
        {
            x = this.currentX;
            y = this.currentY;
        }
    }
}
