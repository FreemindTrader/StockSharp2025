// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.FlattenCurves
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg.VertexSource
{
    internal class FlattenCurves : IVertexSource
    {
        private IVertexSource vertexSource;
        private double lastX;
        private double lastY;
        private Curve3 m_curve3;
        private Curve4 m_curve4;

        public FlattenCurves( IVertexSource source )
        {
            this.m_curve3 = new Curve3();
            this.m_curve4 = new Curve4();
            this.vertexSource = source;
            this.lastX = 0.0;
            this.lastY = 0.0;
        }

        public double ApproximationScale
        {
            get
            {
                return this.m_curve4.approximation_scale();
            }
            set
            {
                this.m_curve3.approximation_scale( value );
                this.m_curve4.approximation_scale( value );
            }
        }

        public void SetVertexSource( IVertexSource source )
        {
            this.vertexSource = source;
        }

        public Curves.CurveApproximationMethod ApproximationMethod
        {
            set
            {
                this.m_curve3.approximation_method( value );
                this.m_curve4.approximation_method( value );
            }
            get
            {
                return this.m_curve4.approximation_method();
            }
        }

        public double AngleTolerance
        {
            set
            {
                this.m_curve3.angle_tolerance( value );
                this.m_curve4.angle_tolerance( value );
            }
            get
            {
                return this.m_curve4.angle_tolerance();
            }
        }

        public double CuspLimit
        {
            set
            {
                this.m_curve3.cusp_limit( value );
                this.m_curve4.cusp_limit( value );
            }
            get
            {
                return this.m_curve4.cusp_limit();
            }
        }

        public void rewind( int path_id )
        {
            this.vertexSource.rewind( path_id );
            this.lastX = 0.0;
            this.lastY = 0.0;
            this.m_curve3.reset();
            this.m_curve4.reset();
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            if ( !Path.is_stop( this.m_curve3.vertex( out x, out y ) ) )
            {
                this.lastX = x;
                this.lastY = y;
                return Path.FlagsAndCommand.CommandLineTo;
            }
            if ( !Path.is_stop( this.m_curve4.vertex( out x, out y ) ) )
            {
                this.lastX = x;
                this.lastY = y;
                return Path.FlagsAndCommand.CommandLineTo;
            }
            Path.FlagsAndCommand flagsAndCommand = this.vertexSource.vertex(out x, out y);
            switch ( flagsAndCommand )
            {
                case Path.FlagsAndCommand.CommandCurve3:
                    double x1;
                    double y1;
                    int num1 = (int) this.vertexSource.vertex(out x1, out y1);
                    this.m_curve3.init( this.lastX, this.lastY, x, y, x1, y1 );
                    int num2 = (int) this.m_curve3.vertex(out x, out y);
                    int num3 = (int) this.m_curve3.vertex(out x, out y);
                    flagsAndCommand = Path.FlagsAndCommand.CommandLineTo;
                    break;
                case Path.FlagsAndCommand.CommandCurve4:
                    double x2;
                    double y2;
                    int num4 = (int) this.vertexSource.vertex(out x2, out y2);
                    double x3;
                    double y3;
                    int num5 = (int) this.vertexSource.vertex(out x3, out y3);
                    this.m_curve4.init( this.lastX, this.lastY, x, y, x2, y2, x3, y3 );
                    int num6 = (int) this.m_curve4.vertex(out x, out y);
                    int num7 = (int) this.m_curve4.vertex(out x, out y);
                    flagsAndCommand = Path.FlagsAndCommand.CommandLineTo;
                    break;
            }
            this.lastX = x;
            this.lastY = y;
            return flagsAndCommand;
        }
    }
}
