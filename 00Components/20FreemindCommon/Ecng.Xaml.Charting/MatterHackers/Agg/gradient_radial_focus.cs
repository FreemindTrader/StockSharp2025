// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.gradient_radial_focus
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class gradient_radial_focus : IGradient
    {
        private int m_r;
        private int m_fx;
        private int m_fy;
        private double m_r2;
        private double m_fx2;
        private double m_fy2;
        private double m_mul;

        public gradient_radial_focus()
        {
            this.m_r = 1600;
            this.m_fx = 0;
            this.m_fy = 0;
            this.update_values();
        }

        public gradient_radial_focus( double r, double fx, double fy )
        {
            this.m_r = agg_basics.iround( r * 16.0 );
            this.m_fx = agg_basics.iround( fx * 16.0 );
            this.m_fy = agg_basics.iround( fy * 16.0 );
            this.update_values();
        }

        public void init( double r, double fx, double fy )
        {
            this.m_r = agg_basics.iround( r * 16.0 );
            this.m_fx = agg_basics.iround( fx * 16.0 );
            this.m_fy = agg_basics.iround( fy * 16.0 );
            this.update_values();
        }

        public double radius()
        {
            return ( double ) this.m_r / 16.0;
        }

        public double focus_x()
        {
            return ( double ) this.m_fx / 16.0;
        }

        public double focus_y()
        {
            return ( double ) this.m_fy / 16.0;
        }

        public int calculate( int x, int y, int d )
        {
            double num1 = (double) (x - this.m_fx);
            double num2 = (double) (y - this.m_fy);
            double num3 = num1 * (double) this.m_fy - num2 * (double) this.m_fx;
            double num4 = this.m_r2 * (num1 * num1 + num2 * num2) - num3 * num3;
            return agg_basics.iround( ( num1 * ( double ) this.m_fx + num2 * ( double ) this.m_fy + Math.Sqrt( Math.Abs( num4 ) ) ) * this.m_mul );
        }

        private void update_values()
        {
            this.m_r2 = ( double ) this.m_r * ( double ) this.m_r;
            this.m_fx2 = ( double ) this.m_fx * ( double ) this.m_fx;
            this.m_fy2 = ( double ) this.m_fy * ( double ) this.m_fy;
            double num = this.m_r2 - (this.m_fx2 + this.m_fy2);
            if ( num == 0.0 )
            {
                if ( this.m_fx != 0 )
                {
                    if ( this.m_fx < 0 )
                        ++this.m_fx;
                    else
                        --this.m_fx;
                }
                if ( this.m_fy != 0 )
                {
                    if ( this.m_fy < 0 )
                        ++this.m_fy;
                    else
                        --this.m_fy;
                }
                this.m_fx2 = ( double ) this.m_fx * ( double ) this.m_fx;
                this.m_fy2 = ( double ) this.m_fy * ( double ) this.m_fy;
                num = this.m_r2 - ( this.m_fx2 + this.m_fy2 );
            }
            this.m_mul = ( double ) this.m_r / num;
        }
    }
}
