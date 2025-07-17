// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Utility.Mouse.ITouchPositionProvider
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows.Input;

namespace StockSharp.Xaml.Charting.Utility.Mouse
{
    public interface ITouchPositionProvider
    {
        TouchPointCollection GetPosition( IPublishMouseEvents source, TouchFrameEventArgs e );
    }
}
