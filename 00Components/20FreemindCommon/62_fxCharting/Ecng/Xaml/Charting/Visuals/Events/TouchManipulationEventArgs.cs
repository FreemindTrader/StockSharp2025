using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace StockSharp.Xaml.Charting.Visuals.Events;

public class TouchManipulationEventArgs : EventArgs
{
    public bool Handled
    {
        get;
        set;
    }

    public IEnumerable<TouchPoint> TouchPoints
    {
        get;
        set;
    }

    public TouchManipulationEventArgs(IEnumerable<TouchPoint> touchPoints)
    {
        this.TouchPoints = touchPoints;
    }
}
