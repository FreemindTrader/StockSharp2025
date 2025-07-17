// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Font.StyledTypeFaceImageCache
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections.Generic;
using MatterHackers.Agg.Image;

namespace MatterHackers.Agg.Font
{
    internal class StyledTypeFaceImageCache
    {
        private Dictionary<TypeFace, Dictionary<double, Dictionary<char, ImageBuffer>>> typeFaceImageCache = new Dictionary<TypeFace, Dictionary<double, Dictionary<char, ImageBuffer>>>();
        private static StyledTypeFaceImageCache globalInstance;

        private StyledTypeFaceImageCache()
        {
        }

        public static Dictionary<char, ImageBuffer> GetCorrectCache( TypeFace typeFace, double emSizeInPoints )
        {
            Dictionary<double, Dictionary<char, ImageBuffer>> dictionary1;
            StyledTypeFaceImageCache.Instance().typeFaceImageCache.TryGetValue( typeFace, out dictionary1 );
            Dictionary<char, ImageBuffer> dictionary2;
            dictionary1.TryGetValue( emSizeInPoints, out dictionary2 );
            return dictionary2;
        }

        private static StyledTypeFaceImageCache Instance()
        {
            if ( StyledTypeFaceImageCache.globalInstance == null )
                StyledTypeFaceImageCache.globalInstance = new StyledTypeFaceImageCache();
            return StyledTypeFaceImageCache.globalInstance;
        }
    }
}
