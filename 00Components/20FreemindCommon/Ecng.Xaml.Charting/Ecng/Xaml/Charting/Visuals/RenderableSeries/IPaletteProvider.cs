// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.IPaletteProvider
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System.Windows.Media;

namespace fx.Xaml.Charting
{
    public interface IPaletteProvider
    {
        Color? GetColor( IRenderableSeries series, double xValue, double yValue );

        Color? OverrideColor( IRenderableSeries series, double xValue, double openValue, double highValue, double lowValue, double closeValue );

        Color? OverrideColor( IRenderableSeries series, double xValue, double yValue, double zValue );
    }
}
