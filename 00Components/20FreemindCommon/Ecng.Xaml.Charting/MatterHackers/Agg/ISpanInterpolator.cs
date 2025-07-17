// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ISpanInterpolator
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using MatterHackers.Agg.Transform;

namespace MatterHackers.Agg
{
    internal interface ISpanInterpolator
    {
        void begin( double x, double y, int len );

        void coordinates( out int x, out int y );

        void Next();

        ITransform transformer();

        void transformer( ITransform trans );

        void resynchronize( double xe, double ye, int len );

        void local_scale( out int x, out int y );
    }
}
