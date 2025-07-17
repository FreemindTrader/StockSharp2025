// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.IPatternFilter
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal interface IPatternFilter
    {
        int dilation();

        void pixel_high_res( ImageBuffer sourceImage, RGBA_Bytes[ ] destBuffer, int destBufferOffset, int x, int y );
    }
}
