// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.LineD
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    internal struct LineD
    {
        internal readonly double X1;
        internal readonly double Y1;
        internal readonly double X2;
        internal readonly double Y2;

        internal LineD( double x1, double y1, double x2, double y2 )
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
        }
    }
}
