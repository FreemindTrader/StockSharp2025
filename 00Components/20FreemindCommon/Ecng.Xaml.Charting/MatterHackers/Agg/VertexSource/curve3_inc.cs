// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.curve3_inc
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.VertexSource
{
    internal sealed class curve3_inc
    {
        private int m_num_steps;
        private int m_step;
        private double m_scale;
        private double m_start_x;
        private double m_start_y;
        private double m_end_x;
        private double m_end_y;
        private double m_fx;
        private double m_fy;
        private double m_dfx;
        private double m_dfy;
        private double m_ddfx;
        private double m_ddfy;
        private double m_saved_fx;
        private double m_saved_fy;
        private double m_saved_dfx;
        private double m_saved_dfy;

        public curve3_inc()
        {
            this.m_num_steps = 0;
            this.m_step = 0;
            this.m_scale = 1.0;
        }

        public curve3_inc( double x1, double y1, double x2, double y2, double x3, double y3 )
        {
            this.m_num_steps = 0;
            this.m_step = 0;
            this.m_scale = 1.0;
            this.init( x1, y1, x2, y2, x3, y3 );
        }

        public void reset()
        {
            this.m_num_steps = 0;
            this.m_step = -1;
        }

        public void init( double x1, double y1, double x2, double y2, double x3, double y3 )
        {
            this.m_start_x = x1;
            this.m_start_y = y1;
            this.m_end_x = x3;
            this.m_end_y = y3;
            double num1 = x2 - x1;
            double num2 = y2 - y1;
            double num3 = x3 - x2;
            double num4 = y3 - y2;
            this.m_num_steps = agg_basics.uround( ( Math.Sqrt( num1 * num1 + num2 * num2 ) + Math.Sqrt( num3 * num3 + num4 * num4 ) ) * 0.25 * this.m_scale );
            if ( this.m_num_steps < 4 )
                this.m_num_steps = 4;
            double num5 = 1.0 / (double) this.m_num_steps;
            double num6 = num5 * num5;
            double num7 = (x1 - x2 * 2.0 + x3) * num6;
            double num8 = (y1 - y2 * 2.0 + y3) * num6;
            this.m_saved_fx = this.m_fx = x1;
            this.m_saved_fy = this.m_fy = y1;
            this.m_saved_dfx = this.m_dfx = num7 + ( x2 - x1 ) * ( 2.0 * num5 );
            this.m_saved_dfy = this.m_dfy = num8 + ( y2 - y1 ) * ( 2.0 * num5 );
            this.m_ddfx = num7 * 2.0;
            this.m_ddfy = num8 * 2.0;
            this.m_step = this.m_num_steps;
        }

        public void approximation_method( Curves.CurveApproximationMethod method )
        {
        }

        public Curves.CurveApproximationMethod approximation_method()
        {
            return Curves.CurveApproximationMethod.curve_inc;
        }

        public void approximation_scale( double s )
        {
            this.m_scale = s;
        }

        public double approximation_scale()
        {
            return this.m_scale;
        }

        public void angle_tolerance( double angle )
        {
        }

        public double angle_tolerance()
        {
            return 0.0;
        }

        public void cusp_limit( double limit )
        {
        }

        public double cusp_limit()
        {
            return 0.0;
        }

        public void rewind( int path_id )
        {
            if ( this.m_num_steps == 0 )
            {
                this.m_step = -1;
            }
            else
            {
                this.m_step = this.m_num_steps;
                this.m_fx = this.m_saved_fx;
                this.m_fy = this.m_saved_fy;
                this.m_dfx = this.m_saved_dfx;
                this.m_dfy = this.m_saved_dfy;
            }
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            if ( this.m_step < 0 )
            {
                x = 0.0;
                y = 0.0;
                return Path.FlagsAndCommand.CommandStop;
            }
            if ( this.m_step == this.m_num_steps )
            {
                x = this.m_start_x;
                y = this.m_start_y;
                --this.m_step;
                return Path.FlagsAndCommand.CommandMoveTo;
            }
            if ( this.m_step == 0 )
            {
                x = this.m_end_x;
                y = this.m_end_y;
                --this.m_step;
                return Path.FlagsAndCommand.CommandLineTo;
            }
            this.m_fx += this.m_dfx;
            this.m_fy += this.m_dfy;
            this.m_dfx += this.m_ddfx;
            this.m_dfy += this.m_ddfy;
            x = this.m_fx;
            y = this.m_fy;
            --this.m_step;
            return Path.FlagsAndCommand.CommandLineTo;
        }
    }
}
