// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.IImageBufferAccessorFloat
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using MatterHackers.Agg.Image;

namespace MatterHackers.Agg
{
    internal interface IImageBufferAccessorFloat
    {
        float[ ] span( int x, int y, int len, out int bufferIndex );

        float[ ] next_x( out int bufferFloatOffset );

        float[ ] next_y( out int bufferFloatOffset );

        IImageFloat SourceImage
        {
            get;
        }
    }
}
