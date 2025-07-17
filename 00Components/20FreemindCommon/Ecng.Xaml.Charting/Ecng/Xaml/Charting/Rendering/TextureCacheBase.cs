// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.TextureCacheBase
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows;
namespace Ecng.Xaml.Charting
{
    public class TextureCacheBase
    {
        internal readonly Dictionary<CharSpriteKey, ISprite2D> FontCache = new Dictionary<CharSpriteKey, ISprite2D>();
        internal Dictionary<Tuple<string, float, FontWeight>, Size> MaxDigitSizeDict = new Dictionary<Tuple<string, float, FontWeight>, Size>();
    }
}
