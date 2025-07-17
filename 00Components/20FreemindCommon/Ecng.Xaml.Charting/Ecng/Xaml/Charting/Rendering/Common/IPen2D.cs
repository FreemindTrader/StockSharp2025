// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Rendering.Common.IPen2D
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows.Media;

namespace Ecng.Xaml.Charting
{
    public interface IPen2D : IPathColor, IDisposable, IDashSplittingContext
    {
        float StrokeThickness
        {
            get;
        }

        bool Antialiased
        {
            get;
        }

        PenLineCap StrokeEndLineCap
        {
            get;
        }
    }
}
