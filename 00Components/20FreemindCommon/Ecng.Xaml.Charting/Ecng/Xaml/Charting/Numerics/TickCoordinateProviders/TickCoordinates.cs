// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Numerics.TickCoordinateProviders.TickCoordinates
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
{
    public struct TickCoordinates
    {
        private readonly double[] _minorTicks;
        private readonly double[] _majorTicks;
        private readonly float[] _minorTickCoordinates;
        private readonly float[] _majorTickCoordinates;

        public TickCoordinates( double[ ] minorTicks, double[ ] majorTicks, float[ ] minorCoords, float[ ] majorCoords )
        {
            this._minorTicks = minorTicks;
            this._majorTicks = majorTicks;
            this._minorTickCoordinates = minorCoords;
            this._majorTickCoordinates = majorCoords;
        }

        public bool IsEmpty
        {
            get
            {
                if ( this._majorTickCoordinates != null )
                    return this._minorTickCoordinates == null;
                return true;
            }
        }

        public double[ ] MinorTicks
        {
            get
            {
                return this._minorTicks;
            }
        }

        public double[ ] MajorTicks
        {
            get
            {
                return this._majorTicks;
            }
        }

        public float[ ] MinorTickCoordinates
        {
            get
            {
                return this._minorTickCoordinates;
            }
        }

        public float[ ] MajorTickCoordinates
        {
            get
            {
                return this._majorTickCoordinates;
            }
        }
    }
}
