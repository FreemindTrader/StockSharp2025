// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Transform.Viewport
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.Transform
{
    internal sealed class Viewport
    {
        private double m_world_x1;
        private double m_world_y1;
        private double m_world_x2;
        private double m_world_y2;
        private double m_device_x1;
        private double m_device_y1;
        private double m_device_x2;
        private double m_device_y2;
        private Viewport.aspect_ratio_e m_aspect;
        private bool m_is_valid;
        private double m_align_x;
        private double m_align_y;
        private double m_wx1;
        private double m_wy1;
        private double m_wx2;
        private double m_wy2;
        private double m_dx1;
        private double m_dy1;
        private double m_kx;
        private double m_ky;

        public Viewport()
        {
            m_world_x1 = 0.0;
            m_world_y1 = 0.0;
            m_world_x2 = 1.0;
            m_world_y2 = 1.0;
            m_device_x1 = 0.0;
            m_device_y1 = 0.0;
            m_device_x2 = 1.0;
            m_device_y2 = 1.0;
            m_aspect = Viewport.aspect_ratio_e.aspect_ratio_stretch;
            m_is_valid = true;
            m_align_x = 0.5;
            m_align_y = 0.5;
            m_wx1 = 0.0;
            m_wy1 = 0.0;
            m_wx2 = 1.0;
            m_wy2 = 1.0;
            m_dx1 = 0.0;
            m_dy1 = 0.0;
            m_kx = 1.0;
            m_ky = 1.0;
        }

        public void preserve_aspect_ratio( double alignx, double aligny, Viewport.aspect_ratio_e aspect )
        {
            m_align_x = alignx;
            m_align_y = aligny;
            m_aspect = aspect;
            update();
        }

        public void device_viewport( double x1, double y1, double x2, double y2 )
        {
            m_device_x1 = x1;
            m_device_y1 = y1;
            m_device_x2 = x2;
            m_device_y2 = y2;
            update();
        }

        public void world_viewport( double x1, double y1, double x2, double y2 )
        {
            m_world_x1 = x1;
            m_world_y1 = y1;
            m_world_x2 = x2;
            m_world_y2 = y2;
            update();
        }

        public void device_viewport( out double x1, out double y1, out double x2, out double y2 )
        {
            x1 = m_device_x1;
            y1 = m_device_y1;
            x2 = m_device_x2;
            y2 = m_device_y2;
        }

        public void world_viewport( out double x1, out double y1, out double x2, out double y2 )
        {
            x1 = m_world_x1;
            y1 = m_world_y1;
            x2 = m_world_x2;
            y2 = m_world_y2;
        }

        public void world_viewport_actual( out double x1, out double y1, out double x2, out double y2 )
        {
            x1 = m_wx1;
            y1 = m_wy1;
            x2 = m_wx2;
            y2 = m_wy2;
        }

        public bool is_valid()
        {
            return m_is_valid;
        }

        public double align_x()
        {
            return m_align_x;
        }

        public double align_y()
        {
            return m_align_y;
        }

        public Viewport.aspect_ratio_e aspect_ratio()
        {
            return m_aspect;
        }

        public void transform( ref double x, ref double y )
        {
            x = ( x - m_wx1 ) * m_kx + m_dx1;
            y = ( y - m_wy1 ) * m_ky + m_dy1;
        }

        public void transform_scale_only( ref double x, ref double y )
        {
            x *= m_kx;
            y *= m_ky;
        }

        public void inverse_transform( ref double x, ref double y )
        {
            x = ( x - m_dx1 ) / m_kx + m_wx1;
            y = ( y - m_dy1 ) / m_ky + m_wy1;
        }

        public void inverse_transform_scale_only( ref double x, ref double y )
        {
            x /= m_kx;
            y /= m_ky;
        }

        public double device_dx()
        {
            return m_dx1 - m_wx1 * m_kx;
        }

        public double device_dy()
        {
            return m_dy1 - m_wy1 * m_ky;
        }

        public double scale_x()
        {
            return m_kx;
        }

        public double scale_y()
        {
            return m_ky;
        }

        public double scale()
        {
            return ( m_kx + m_ky ) * 0.5;
        }

        public Affine to_affine()
        {
            return Affine.NewTranslation( -m_wx1, -m_wy1 ) * Affine.NewScaling( m_kx, m_ky ) * Affine.NewTranslation( m_dx1, m_dy1 );
        }

        public Affine to_affine_scale_only()
        {
            return Affine.NewScaling( m_kx, m_ky );
        }

        private void update()
        {
            double num1 = 1E-30;
            if ( Math.Abs( m_world_x1 - m_world_x2 ) < num1 || Math.Abs( m_world_y1 - m_world_y2 ) < num1 || ( Math.Abs( m_device_x1 - m_device_x2 ) < num1 || Math.Abs( m_device_y1 - m_device_y2 ) < num1 ) )
            {
                m_wx1 = m_world_x1;
                m_wy1 = m_world_y1;
                m_wx2 = m_world_x1 + 1.0;
                m_wy2 = m_world_y2 + 1.0;
                m_dx1 = m_device_x1;
                m_dy1 = m_device_y1;
                m_kx = 1.0;
                m_ky = 1.0;
                m_is_valid = false;
            }
            else
            {
                double worldX1  = m_world_x1;
                double worldY1  = m_world_y1;
                double num2     = m_world_x2;
                double num3     = m_world_y2;
                double deviceX1 = m_device_x1;
                double deviceY1 = m_device_y1;
                double deviceX2 = m_device_x2;
                double deviceY2 = m_device_y2;
                if ( m_aspect != Viewport.aspect_ratio_e.aspect_ratio_stretch )
                {
                    m_kx = ( deviceX2 - deviceX1 ) / ( num2 - worldX1 );
                    m_ky = ( deviceY2 - deviceY1 ) / ( num3 - worldY1 );
                    if ( m_aspect == Viewport.aspect_ratio_e.aspect_ratio_meet == m_kx < m_ky )
                    {
                        double num4 = (num3 - worldY1) * m_ky / m_kx;
                        worldY1 += ( num3 - worldY1 - num4 ) * m_align_y;
                        num3 = worldY1 + num4;
                    }
                    else
                    {
                        double num4 = (num2 - worldX1) * m_kx / m_ky;
                        worldX1 += ( num2 - worldX1 - num4 ) * m_align_x;
                        num2 = worldX1 + num4;
                    }
                }
                m_wx1 = worldX1;
                m_wy1 = worldY1;
                m_wx2 = num2;
                m_wy2 = num3;
                m_dx1 = deviceX1;
                m_dy1 = deviceY1;
                m_kx = ( deviceX2 - deviceX1 ) / ( num2 - worldX1 );
                m_ky = ( deviceY2 - deviceY1 ) / ( num3 - worldY1 );
                m_is_valid = true;
            }
        }

        public enum aspect_ratio_e
        {
            aspect_ratio_stretch,
            aspect_ratio_meet,
            aspect_ratio_slice,
        }
    }
}
