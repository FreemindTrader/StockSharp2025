// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Helpers.PolarCartesianTransformationHelper
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;

namespace Ecng.Xaml.Charting.Common.Helpers
{
    internal class PolarCartesianTransformationHelper
    {
        private readonly Point _origin;

        public PolarCartesianTransformationHelper( Point origin )
        {
            this._origin = origin;
        }

        public PolarCartesianTransformationHelper( double viewportWidth, double viewportHeight )
        {
            this._origin = new Point( viewportWidth / 2.0, viewportHeight / 2.0 );
        }

        public Point Origin
        {
            get
            {
                return this._origin;
            }
        }

        public Point ToCartesian( double angle, double r )
        {
            angle *= Math.PI / 180.0;
            return new Point( this._origin.X + r * Math.Cos( angle ), this._origin.Y + r * Math.Sin( angle ) );
        }

        public Point ToPolar( double x, double y )
        {
            if ( x.Equals( this._origin.X ) && y.Equals( this._origin.Y ) )
                return new Point( 0.0, 0.0 );
            x -= this._origin.X;
            y -= this._origin.Y;
            double y1 = Math.Sqrt(x * x + y * y);
            double x1 = Math.Atan(y / x) / (Math.PI / 180.0);
            if ( x < 0.0 )
                x1 += 180.0;
            else if ( y < 0.0 )
                x1 += 360.0;
            return new Point( x1, y1 );
        }
    }
}
