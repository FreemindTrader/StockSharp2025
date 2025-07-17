// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.IMarkers
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal interface IMarkers
    {
        void remove_all();

        void add_vertex( double x, double y, Path.FlagsAndCommand unknown );
    }
}
