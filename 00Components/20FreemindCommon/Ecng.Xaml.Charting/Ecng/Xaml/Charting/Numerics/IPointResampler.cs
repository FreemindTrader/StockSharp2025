// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.IPointResampler
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections;
using Ecng.Xaml.Charting.Model.DataSeries;

namespace Ecng.Xaml.Charting.Numerics
{
    public interface IPointResampler
    {
        IPointSeries Execute( ResamplingMode resamplingMode, IndexRange pointRange, int viewportWidth, bool isFifo, bool isCategoryAxis, IList xColumn, IList yColumn, bool? dataIsSorted, bool? dataIsEvenlySpaced, bool? dataIsDisplayedAs2d, IRange visibleXRange );
    }
}
