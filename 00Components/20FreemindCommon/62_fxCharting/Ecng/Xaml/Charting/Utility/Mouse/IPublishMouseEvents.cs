using StockSharp.Xaml.Charting.Visuals.Events;
using System;
using System.Windows.Input;

namespace StockSharp.Xaml.Charting.Utility.Mouse;

/// <summary>
/// Tony Modified
/// </summary>
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
