// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Image.DoCopyOrBlend
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg.Image
{
    internal static class DoCopyOrBlend
    {
        private const byte base_mask = 255;

        public static void BasedOnAlpha( IBlenderByte Blender, byte[ ] destBuffer, int bufferOffset, RGBA_Bytes sourceColor )
        {
            Blender.BlendPixel( destBuffer, bufferOffset, sourceColor );
        }

        public static void BasedOnAlphaAndCover( IBlenderByte Blender, byte[ ] destBuffer, int bufferOffset, RGBA_Bytes sourceColor, int cover )
        {
            if ( cover == ( int ) byte.MaxValue )
            {
                DoCopyOrBlend.BasedOnAlpha( Blender, destBuffer, bufferOffset, sourceColor );
            }
            else
            {
                sourceColor.alpha = ( byte ) ( ( int ) sourceColor.alpha * ( cover + 1 ) >> 8 );
                Blender.BlendPixel( destBuffer, bufferOffset, sourceColor );
            }
        }
    }
}
