// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.arc
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg.VertexSource
{
    internal class arc
    {
        private double m_OriginX;
        private double m_OriginY;
        private double m_RadiusX;
        private double m_RadiusY;
        private double m_StartAngle;
        private double m_EndAngle;
        private double m_Scale;
        private arc.EDirection m_Direction;
        private double m_CurrentFlatenAngle;
        private double m_FlatenDeltaAngle;
        private bool m_IsInitialized;
        private Path.FlagsAndCommand m_NextPathCommand;

        public arc()
        {
            this.m_Scale = 1.0;
            this.m_IsInitialized = false;
        }

        public arc( double OriginX, double OriginY, double RadiusX, double RadiusY, double Angle1, double Angle2 )
          : this( OriginX, OriginY, RadiusX, RadiusY, Angle1, Angle2, arc.EDirection.CounterClockWise )
        {
        }

        public arc( double OriginX, double OriginY, double RadiusX, double RadiusY, double Angle1, double Angle2, arc.EDirection Direction )
        {
            this.m_OriginX = OriginX;
            this.m_OriginY = OriginY;
            this.m_RadiusX = RadiusX;
            this.m_RadiusY = RadiusY;
            this.m_Scale = 1.0;
            this.normalize( Angle1, Angle2, Direction );
        }

        public void init( double OriginX, double OriginY, double RadiusX, double RadiusY, double Angle1, double Angle2 )
        {
            this.init( OriginX, OriginY, RadiusX, RadiusY, Angle1, Angle2, arc.EDirection.CounterClockWise );
        }

        public void init( double OriginX, double OriginY, double RadiusX, double RadiusY, double Angle1, double Angle2, arc.EDirection Direction )
        {
            this.m_OriginX = OriginX;
            this.m_OriginY = OriginY;
            this.m_RadiusX = RadiusX;
            this.m_RadiusY = RadiusY;
            this.normalize( Angle1, Angle2, Direction );
        }

        public void approximation_scale( double s )
        {
            this.m_Scale = s;
            if ( !this.m_IsInitialized )
                return;
            this.normalize( this.m_StartAngle, this.m_EndAngle, this.m_Direction );
        }

        public double approximation_scale()
        {
            return this.m_Scale;
        }

        public void rewind( int unused )
        {
            this.m_NextPathCommand = Path.FlagsAndCommand.CommandMoveTo;
            this.m_CurrentFlatenAngle = this.m_StartAngle;
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            x = 0.0;
            y = 0.0;
            if ( Path.is_stop( this.m_NextPathCommand ) )
                return Path.FlagsAndCommand.CommandStop;
            if ( this.m_CurrentFlatenAngle >= this.m_EndAngle - this.m_FlatenDeltaAngle / 4.0 )
            {
                x = this.m_OriginX + Math.Cos( this.m_EndAngle ) * this.m_RadiusX;
                y = this.m_OriginY + Math.Sin( this.m_EndAngle ) * this.m_RadiusY;
                this.m_NextPathCommand = Path.FlagsAndCommand.CommandStop;
                return Path.FlagsAndCommand.CommandLineTo;
            }
            x = this.m_OriginX + Math.Cos( this.m_CurrentFlatenAngle ) * this.m_RadiusX;
            y = this.m_OriginY + Math.Sin( this.m_CurrentFlatenAngle ) * this.m_RadiusY;
            this.m_CurrentFlatenAngle += this.m_FlatenDeltaAngle;
            Path.FlagsAndCommand nextPathCommand = this.m_NextPathCommand;
            this.m_NextPathCommand = Path.FlagsAndCommand.CommandLineTo;
            return nextPathCommand;
        }

        private void normalize( double Angle1, double Angle2, arc.EDirection Direction )
        {
            double num = (Math.Abs(this.m_RadiusX) + Math.Abs(this.m_RadiusY)) / 2.0;
            this.m_FlatenDeltaAngle = Math.Acos( num / ( num + 0.125 / this.m_Scale ) ) * 2.0;
            if ( Direction == arc.EDirection.CounterClockWise )
            {
                while ( Angle2 < Angle1 )
                    Angle2 += 2.0 * Math.PI;
            }
            else
            {
                while ( Angle1 < Angle2 )
                    Angle1 += 2.0 * Math.PI;
                this.m_FlatenDeltaAngle = -this.m_FlatenDeltaAngle;
            }
            this.m_Direction = Direction;
            this.m_StartAngle = Angle1;
            this.m_EndAngle = Angle2;
            this.m_IsInitialized = true;
        }

        public enum EDirection
        {
            ClockWise,
            CounterClockWise,
        }
    }
}
