// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.span_image_filter_rgb_2x2
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace MatterHackers.Agg
{
    internal class span_image_filter_rgb_2x2 : span_image_filter
    {
        private const int base_mask = 255;

        public span_image_filter_rgb_2x2( IImageBufferAccessor src, ISpanInterpolator inter, ImageFilterLookUpTable filter )
          : base( src, inter, filter )
        {
        }

        public override void generate( RGBA_Bytes[ ] span, int spanIndex, int x, int y, int len )
        {
            throw new NotImplementedException();
        }
    }
}
