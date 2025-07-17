// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Events.TouchManipulationEventArgs
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace fx.Xaml.Charting
{
    public class TouchManipulationEventArgs : EventArgs
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
}
