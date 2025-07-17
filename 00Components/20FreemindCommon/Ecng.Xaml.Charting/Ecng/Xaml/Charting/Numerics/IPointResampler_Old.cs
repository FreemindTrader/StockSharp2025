// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.IPointResampler_Old
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.Collections;

namespace Ecng.Xaml.Charting
{
    internal interface IPointResampler_Old
    {
        int Resolution
        {
            get;
        }

        ResamplingMode ResamplingMode
        {
            get;
        }

        bool RequiresReduction( IndexRange pointIndices, int viewportWidth );

        IPointResampler_Old WithMode( ResamplingMode newMode );

        IList ReducePoints( IList inputPoints, int viewportWidth );

        IList ReducePoints( IList inputPoints, IndexRange pointIndices, int viewportWidth );
    }
}
