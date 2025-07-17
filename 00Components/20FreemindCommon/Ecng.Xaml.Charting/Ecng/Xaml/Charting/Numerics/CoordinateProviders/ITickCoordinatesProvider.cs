// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Numerics.CoordinateProviders.ITickCoordinatesProvider
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace fx.Xaml.Charting
{
    public interface ITickCoordinatesProvider
    {
        void Init( IAxis parentAxis );

        TickCoordinates GetTickCoordinates( double[ ] minorTicks, double[ ] majorTicks );
    }
}
