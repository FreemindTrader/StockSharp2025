// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.goodInterfaceThinking
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg.Image
{
    internal interface goodInterfaceThinking
    {
        int NumPixelBits
        {
            get;
        }

        RGBA_Bytes PixelToColorRGBA_Bytes( byte[ ] buffer, int bufferOffset );

        void SetPixels( byte[ ] buffer, int bufferOffset, RGBA_Bytes sourceColor, int count );

        void SetPixels( byte[ ] buffer, int bufferOffset, RGBA_Bytes[ ] sourceColors, int sourceColorsOffset, int count );

        void BlendPixels( byte[ ] buffer, int bufferOffset, RGBA_Bytes sourceColor, int count );

        void BlendPixels( byte[ ] buffer, int bufferOffset, RGBA_Bytes[ ] sourceColors, int sourceColorsOffset, int count );

        void BlendPixels( byte[ ] buffer, int bufferOffset, RGBA_Bytes[ ] sourceColors, int sourceColorsOffset, byte sourceCover, int count );

        void BlendPixels( byte[ ] buffer, int bufferOffset, RGBA_Bytes[ ] sourceColors, int sourceColorsOffset, byte[ ] sourceCovers, int sourceCoversOffset, int count );
    }
}
