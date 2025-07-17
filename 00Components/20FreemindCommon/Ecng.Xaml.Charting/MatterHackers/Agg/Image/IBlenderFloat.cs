// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.IBlenderFloat
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg.Image
{
    internal interface IBlenderFloat
    {
        int NumPixelBits
        {
            get;
        }

        RGBA_Floats PixelToColorRGBA_Floats( float[ ] buffer, int bufferOffset );

        void CopyPixels( float[ ] buffer, int bufferOffset, RGBA_Floats sourceColor, int count );

        void BlendPixel( float[ ] buffer, int bufferOffset, RGBA_Floats sourceColor );

        void BlendPixels( float[ ] buffer, int bufferOffset, RGBA_Floats[ ] sourceColors, int sourceColorsOffset, byte[ ] sourceCovers, int sourceCoversOffset, bool firstCoverForAll, int count );
    }
}
