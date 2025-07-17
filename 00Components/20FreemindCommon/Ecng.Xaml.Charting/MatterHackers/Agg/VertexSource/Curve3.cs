// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.Curve3
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg.VertexSource
{
    internal sealed class Curve3 : ICurve, IVertexSource
    {
        private curve3_inc m_curve_inc;
        private curve3_div m_curve_div;
        private Curves.CurveApproximationMethod m_approximation_method;

        public Curve3()
        {
            this.m_curve_inc = new curve3_inc();
            this.m_curve_div = new curve3_div();
            this.m_approximation_method = Curves.CurveApproximationMethod.curve_div;
        }

        public Curve3( double x1, double y1, double x2, double y2, double x3, double y3 )
        {
            this.m_approximation_method = Curves.CurveApproximationMethod.curve_div;
            this.init( x1, y1, x2, y2, x3, y3 );
        }

        public void reset()
        {
            this.m_curve_inc.reset();
            this.m_curve_div.reset();
        }

        public void init( double x1, double y1, double x2, double y2, double x3, double y3 )
        {
            if ( this.m_approximation_method == Curves.CurveApproximationMethod.curve_inc )
                this.m_curve_inc.init( x1, y1, x2, y2, x3, y3 );
            else
                this.m_curve_div.init( x1, y1, x2, y2, x3, y3 );
        }

        public void approximation_method( Curves.CurveApproximationMethod v )
        {
            this.m_approximation_method = v;
        }

        public Curves.CurveApproximationMethod approximation_method()
        {
            return this.m_approximation_method;
        }

        public void approximation_scale( double s )
        {
            this.m_curve_inc.approximation_scale( s );
            this.m_curve_div.approximation_scale( s );
        }

        public double approximation_scale()
        {
            return this.m_curve_inc.approximation_scale();
        }

        public void angle_tolerance( double a )
        {
            this.m_curve_div.angle_tolerance( a );
        }

        public double angle_tolerance()
        {
            return this.m_curve_div.angle_tolerance();
        }

        public void cusp_limit( double v )
        {
            this.m_curve_div.cusp_limit( v );
        }

        public double cusp_limit()
        {
            return this.m_curve_div.cusp_limit();
        }

        public void rewind( int path_id )
        {
            if ( this.m_approximation_method == Curves.CurveApproximationMethod.curve_inc )
                this.m_curve_inc.rewind( path_id );
            else
                this.m_curve_div.rewind( path_id );
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            if ( this.m_approximation_method == Curves.CurveApproximationMethod.curve_inc )
                return this.m_curve_inc.vertex( out x, out y );
            return this.m_curve_div.vertex( out x, out y );
        }
    }
}
