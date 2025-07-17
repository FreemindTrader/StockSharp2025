// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.IColorType
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal interface IColorType
    {
        RGBA_Floats GetAsRGBA_Floats();

        RGBA_Bytes GetAsRGBA_Bytes();

        RGBA_Bytes gradient( RGBA_Bytes c, double k );

        int Red0To255
        {
            get; set;
        }

        int Green0To255
        {
            get; set;
        }

        int Blue0To255
        {
            get; set;
        }

        int Alpha0To255
        {
            get; set;
        }
    }
}
