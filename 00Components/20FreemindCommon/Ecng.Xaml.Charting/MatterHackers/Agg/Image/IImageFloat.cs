// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.IImageFloat
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg.Image
{
    internal interface IImageFloat : IImage
    {
        int StrideInFloats();

        int StrideInFloatsAbs();

        IBlenderFloat GetBlender();

        void SetBlender( IBlenderFloat value );

        int GetFloatsBetweenPixelsInclusive();

        float[ ] GetBuffer();

        RGBA_Floats GetPixel( int x, int y );

        void copy_pixel( int x, int y, float[ ] c, int floatOffset );

        void CopyFrom( IImageFloat sourceImage );

        void CopyFrom( IImageFloat sourceImage, RectangleInt sourceImageRect, int destXOffset, int destYOffset );

        void SetPixel( int x, int y, RGBA_Floats color );

        void BlendPixel( int x, int y, RGBA_Floats sourceColor, byte cover );

        void copy_hline( int x, int y, int len, RGBA_Floats sourceColor );

        void copy_vline( int x, int y, int len, RGBA_Floats sourceColor );

        void blend_hline( int x, int y, int x2, RGBA_Floats sourceColor, byte cover );

        void blend_vline( int x, int y1, int y2, RGBA_Floats sourceColor, byte cover );

        void copy_color_hspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorIndex );

        void copy_color_vspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorIndex );

        void blend_solid_hspan( int x, int y, int len, RGBA_Floats sourceColor, byte[ ] covers, int coversIndex );

        void blend_solid_vspan( int x, int y, int len, RGBA_Floats sourceColor, byte[ ] covers, int coversIndex );

        void blend_color_hspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll );

        void blend_color_vspan( int x, int y, int len, RGBA_Floats[ ] colors, int colorsIndex, byte[ ] covers, int coversIndex, bool firstCoverForAll );
    }
}
