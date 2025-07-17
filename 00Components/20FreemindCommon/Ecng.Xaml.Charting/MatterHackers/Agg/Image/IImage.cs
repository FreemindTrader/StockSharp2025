// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.IImage
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using MatterHackers.VectorMath;

namespace MatterHackers.Agg.Image
{
    internal interface IImage
    {
        Vector2 OriginOffset
        {
            get; set;
        }

        int BitDepth
        {
            get;
        }

        int Width
        {
            get;
        }

        int Height
        {
            get;
        }

        RectangleInt GetBounds();

        int GetBufferOffsetY( int y );

        int GetBufferOffsetXY( int x, int y );

        Graphics2D NewGraphics2D();

        void MarkImageChanged();
    }
}
