// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.curve4_inc
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.VertexSource
{
    internal sealed class curve4_inc
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
        private double m_dddfx;
        private double m_dddfy;
        private double m_saved_fx;
        private double m_saved_fy;
        private double m_saved_dfx;
        private double m_saved_dfy;
        private double m_saved_ddfx;
        private double m_saved_ddfy;

        public curve4_inc()
        {
            this.m_num_steps = 0;
            this.m_step = 0;
            this.m_scale = 1.0;
        }

        public curve4_inc( double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4 )
        {
            this.m_num_steps = 0;
            this.m_step = 0;
            this.m_scale = 1.0;
            this.init( x1, y1, x2, y2, x3, y3, x4, y4 );
        }

        public curve4_inc( curve4_points cp )
        {
            this.m_num_steps = 0;
            this.m_step = 0;
            this.m_scale = 1.0;
            this.init( cp[ 0 ], cp[ 1 ], cp[ 2 ], cp[ 3 ], cp[ 4 ], cp[ 5 ], cp[ 6 ], cp[ 7 ] );
        }

        public void reset()
        {
            this.m_num_steps = 0;
            this.m_step = -1;
        }

        public void init( double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4 )
        {
            this.m_start_x = x1;
            this.m_start_y = y1;
            this.m_end_x = x4;
            this.m_end_y = y4;
            double num1 = x2 - x1;
            double num2 = y2 - y1;
            double num3 = x3 - x2;
            double num4 = y3 - y2;
            double num5 = x4 - x3;
            double num6 = y4 - y3;
            this.m_num_steps = agg_basics.uround( ( Math.Sqrt( num1 * num1 + num2 * num2 ) + Math.Sqrt( num3 * num3 + num4 * num4 ) + Math.Sqrt( num5 * num5 + num6 * num6 ) ) * 0.25 * this.m_scale );
            if ( this.m_num_steps < 4 )
                this.m_num_steps = 4;
            double num7 = 1.0 / (double) this.m_num_steps;
            double num8 = num7 * num7;
            double num9 = num7 * num7 * num7;
            double num10 = 3.0 * num7;
            double num11 = 3.0 * num8;
            double num12 = 6.0 * num8;
            double num13 = 6.0 * num9;
            double num14 = x1 - x2 * 2.0 + x3;
            double num15 = y1 - y2 * 2.0 + y3;
            double num16 = (x2 - x3) * 3.0 - x1 + x4;
            double num17 = (y2 - y3) * 3.0 - y1 + y4;
            this.m_saved_fx = this.m_fx = x1;
            this.m_saved_fy = this.m_fy = y1;
            this.m_saved_dfx = this.m_dfx = ( x2 - x1 ) * num10 + num14 * num11 + num16 * num9;
            this.m_saved_dfy = this.m_dfy = ( y2 - y1 ) * num10 + num15 * num11 + num17 * num9;
            this.m_saved_ddfx = this.m_ddfx = num14 * num12 + num16 * num13;
            this.m_saved_ddfy = this.m_ddfy = num15 * num12 + num17 * num13;
            this.m_dddfx = num16 * num13;
            this.m_dddfy = num17 * num13;
            this.m_step = this.m_num_steps;
        }

        public void init( curve4_points cp )
        {
            this.init( cp[ 0 ], cp[ 1 ], cp[ 2 ], cp[ 3 ], cp[ 4 ], cp[ 5 ], cp[ 6 ], cp[ 7 ] );
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
                this.m_ddfx = this.m_saved_ddfx;
                this.m_ddfy = this.m_saved_ddfy;
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
            this.m_ddfx += this.m_dddfx;
            this.m_ddfy += this.m_dddfy;
            x = this.m_fx;
            y = this.m_fy;
            --this.m_step;
            return Path.FlagsAndCommand.CommandLineTo;
        }
    }
}
