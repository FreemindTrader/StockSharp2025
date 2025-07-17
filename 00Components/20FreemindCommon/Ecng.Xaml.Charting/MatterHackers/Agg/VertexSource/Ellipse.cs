// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.Ellipse
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using MatterHackers.VectorMath;

namespace MatterHackers.Agg.VertexSource
{
    internal class Ellipse : IVertexSource
    {
        public double originX;
        public double originY;
        public double radiusX;
        public double radiusY;
        private double m_scale;
        private int numSteps;
        private int m_step;
        private bool m_cw;

        public Ellipse()
        {
            this.originX = 0.0;
            this.originY = 0.0;
            this.radiusX = 1.0;
            this.radiusY = 1.0;
            this.m_scale = 1.0;
            this.numSteps = 4;
            this.m_step = 0;
            this.m_cw = false;
        }

        public Ellipse( Vector2 origin, double Radius )
          : this( origin.x, origin.y, Radius, Radius, 0, false )
        {
        }

        public Ellipse( Vector2 origin, double RadiusX, double RadiusY, int num_steps = 0, bool cw = false )
          : this( origin.x, origin.y, RadiusX, RadiusY, num_steps, cw )
        {
        }

        public Ellipse( double OriginX, double OriginY, double RadiusX, double RadiusY, int num_steps = 0, bool cw = false )
        {
            this.originX = OriginX;
            this.originY = OriginY;
            this.radiusX = RadiusX;
            this.radiusY = RadiusY;
            this.m_scale = 1.0;
            this.numSteps = num_steps;
            this.m_step = 0;
            this.m_cw = cw;
            if ( this.numSteps != 0 )
                return;
            this.calc_num_steps();
        }

        public void init( double OriginX, double OriginY, double RadiusX, double RadiusY )
        {
            this.init( OriginX, OriginY, RadiusX, RadiusY, 0, false );
        }

        public void init( double OriginX, double OriginY, double RadiusX, double RadiusY, int num_steps )
        {
            this.init( OriginX, OriginY, RadiusX, RadiusY, num_steps, false );
        }

        public void init( double OriginX, double OriginY, double RadiusX, double RadiusY, int num_steps, bool cw )
        {
            this.originX = OriginX;
            this.originY = OriginY;
            this.radiusX = RadiusX;
            this.radiusY = RadiusY;
            this.numSteps = num_steps;
            this.m_step = 0;
            this.m_cw = cw;
            if ( this.numSteps != 0 )
                return;
            this.calc_num_steps();
        }

        public void approximation_scale( double scale )
        {
            this.m_scale = scale;
            this.calc_num_steps();
        }

        public void rewind( int path_id )
        {
            this.m_step = 0;
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            x = 0.0;
            y = 0.0;
            if ( this.m_step == this.numSteps )
            {
                ++this.m_step;
                return Path.FlagsAndCommand.CommandEndPoly | Path.FlagsAndCommand.FlagCCW | Path.FlagsAndCommand.FlagClose;
            }
            if ( this.m_step > this.numSteps )
                return Path.FlagsAndCommand.CommandStop;
            double num = (double) this.m_step / (double) this.numSteps * 2.0 * Math.PI;
            if ( this.m_cw )
                num = 2.0 * Math.PI - num;
            x = this.originX + Math.Cos( num ) * this.radiusX;
            y = this.originY + Math.Sin( num ) * this.radiusY;
            ++this.m_step;
            return this.m_step != 1 ? Path.FlagsAndCommand.CommandLineTo : Path.FlagsAndCommand.CommandMoveTo;
        }

        private void calc_num_steps()
        {
            double num = (Math.Abs(this.radiusX) + Math.Abs(this.radiusY)) / 2.0;
            this.numSteps = ( int ) Math.Round( 2.0 * Math.PI / ( Math.Acos( num / ( num + 0.125 / this.m_scale ) ) * 2.0 ) );
        }
    }
}
