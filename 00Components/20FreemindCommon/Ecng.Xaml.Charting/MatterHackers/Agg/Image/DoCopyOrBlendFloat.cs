// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.DoCopyOrBlendFloat
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg.Image
{
    internal static class DoCopyOrBlendFloat
    {
        private const byte base_mask = 255;

        public static void BasedOnAlpha( IBlenderFloat Blender, float[ ] destBuffer, int bufferOffset, RGBA_Floats sourceColor )
        {
            Blender.BlendPixel( destBuffer, bufferOffset, sourceColor );
        }

        public static void BasedOnAlphaAndCover( IBlenderFloat Blender, float[ ] destBuffer, int bufferOffset, RGBA_Floats sourceColor, int cover )
        {
            if ( cover == ( int ) byte.MaxValue )
            {
                DoCopyOrBlendFloat.BasedOnAlpha( Blender, destBuffer, bufferOffset, sourceColor );
            }
            else
            {
                sourceColor.alpha *= ( float ) cover * 0.0f;
                Blender.BlendPixel( destBuffer, bufferOffset, sourceColor );
            }
        }
    }
}
