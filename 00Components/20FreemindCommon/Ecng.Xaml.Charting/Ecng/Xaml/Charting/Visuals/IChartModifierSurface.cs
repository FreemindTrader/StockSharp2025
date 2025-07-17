// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.IChartModifierSurface
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Collections.ObjectModel;
using System.Windows;

namespace StockSharp.Xaml.Charting.Visuals
{
    public interface IChartModifierSurface : IHitTestable
    {
        bool ClipToBounds
        {
            get; set;
        }

        ObservableCollection<UIElement> Children
        {
            get;
        }

        void Clear();

        bool CaptureMouse();

        void ReleaseMouseCapture();
    }
}
