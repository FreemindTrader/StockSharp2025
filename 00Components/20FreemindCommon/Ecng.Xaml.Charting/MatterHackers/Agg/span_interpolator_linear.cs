// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_interpolator_linear
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using MatterHackers.Agg.Transform;

namespace MatterHackers.Agg
{
    internal sealed class span_interpolator_linear : ISpanInterpolator
    {
        private ITransform m_trans;
        private dda2_line_interpolator m_li_x;
        private dda2_line_interpolator m_li_y;

        public span_interpolator_linear()
        {
        }

        public span_interpolator_linear( ITransform trans )
        {
            this.m_trans = trans;
        }

        public span_interpolator_linear( ITransform trans, double x, double y, int len )
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

        public void local_scale( out int x, out int y )
        {
            throw new NotImplementedException();
        }

        public void begin( double x, double y, int len )
        {
            double x1 = x;
            double y1 = y;
            this.m_trans.transform( ref x1, ref y1 );
            int y1_1 = agg_basics.iround(x1 * 256.0);
            int y1_2 = agg_basics.iround(y1 * 256.0);
            double x2 = x + (double) len;
            double y2 = y;
            this.m_trans.transform( ref x2, ref y2 );
            int y2_1 = agg_basics.iround(x2 * 256.0);
            int y2_2 = agg_basics.iround(y2 * 256.0);
            this.m_li_x = new dda2_line_interpolator( y1_1, y2_1, len );
            this.m_li_y = new dda2_line_interpolator( y1_2, y2_2, len );
        }

        public void resynchronize( double xe, double ye, int len )
        {
            this.m_trans.transform( ref xe, ref ye );
            this.m_li_x = new dda2_line_interpolator( this.m_li_x.y(), agg_basics.iround( xe * 256.0 ), len );
            this.m_li_y = new dda2_line_interpolator( this.m_li_y.y(), agg_basics.iround( ye * 256.0 ), len );
        }

        public void Next()
        {
            this.m_li_x.Next();
            this.m_li_y.Next();
        }

        public void coordinates( out int x, out int y )
        {
            x = this.m_li_x.y();
            y = this.m_li_y.y();
        }

        public enum subpixel_scale_e
        {
            SubpixelShift = 8,
            subpixel_shift = 8,
            subpixel_scale = 256, // 0x00000100
        }
    }
}
