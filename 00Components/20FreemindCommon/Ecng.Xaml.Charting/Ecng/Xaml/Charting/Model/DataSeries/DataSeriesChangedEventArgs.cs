// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.DataSeriesChangedEventArgs
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;

namespace Ecng.Xaml.Charting
{
    public class DataSeriesChangedEventArgs : EventArgs
    {
        public DataSeriesUpdate DataSeriesUpdate
        {
            get; private set;
        }

        public DataSeriesChangedEventArgs( DataSeriesUpdate dataSeriesUpdate )
        {
            this.DataSeriesUpdate = dataSeriesUpdate;
        }
    }
}
