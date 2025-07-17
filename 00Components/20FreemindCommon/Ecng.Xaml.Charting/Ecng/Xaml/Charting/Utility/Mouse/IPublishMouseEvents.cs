// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.IPublishMouseEvents
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows.Input;
namespace Ecng.Xaml.Charting
{
    public interface IPublishMouseEvents
    {
        event MouseButtonEventHandler MouseLeftButtonDown;

        event MouseButtonEventHandler MouseLeftButtonUp;

        event MouseButtonEventHandler MouseRightButtonDown;

        event MouseButtonEventHandler MouseRightButtonUp;

        event MouseEventHandler MouseMove;

        event MouseWheelEventHandler MouseWheel;

        event MouseEventHandler MouseLeave;

        event MouseButtonEventHandler MouseMiddleButtonDown;

        event MouseButtonEventHandler MouseMiddleButtonUp;

        event EventHandler<TouchManipulationEventArgs> TouchDown;

        event EventHandler<TouchManipulationEventArgs> TouchMove;

        event EventHandler<TouchManipulationEventArgs> TouchUp;
    }
}
