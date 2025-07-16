using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace StockSharp.Xaml.Charting.Visuals.Events;

public class TouchManipulationEventArgs : System.EventArgs
{
    public bool Handled
    {
        get; set;
    }

    public IEnumerable<TouchPoint> TouchPoints
    {
        get; set;
    }

    public TouchManipulationEventArgs( IEnumerable<TouchPoint> touchPoints )
    {
        this.TouchPoints = touchPoints;
    }
}