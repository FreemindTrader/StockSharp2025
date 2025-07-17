// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.IRasterizer
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using MatterHackers.Agg.VertexSource;

namespace MatterHackers.Agg
{
    internal interface IRasterizer
    {
        int min_x();

        int min_y();

        int max_x();

        int max_y();

        void gamma( IGammaFunction gamma_function );

        bool sweep_scanline( IScanlineCache sl );

        void reset();

        void add_path( IVertexSource vs );

        void add_path( IVertexSource vs, int path_id );

        bool rewind_scanlines();
    }
}
