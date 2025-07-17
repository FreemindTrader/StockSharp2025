// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.ITickLabelViewModel
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace Ecng.Xaml.Charting
{
    public interface ITickLabelViewModel
    {
        bool HasExponent
        {
            get; set;
        }

        string Separator
        {
            get; set;
        }

        string Exponent
        {
            get; set;
        }

        string Text
        {
            get; set;
        }
    }
}
