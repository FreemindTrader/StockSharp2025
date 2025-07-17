// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ITickLabelsPool
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    internal interface ITickLabelsPool
    {
        int Count
        {
            get;
        }

        int AvailableCount
        {
            get;
        }

        bool IsEmpty
        {
            get;
        }

        DefaultTickLabel Get();

        DefaultTickLabel Get( Func<DefaultTickLabel, DefaultTickLabel> actionOnCreation );

        void Put( DefaultTickLabel item );

        void Dispose();
    }
}
