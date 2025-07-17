// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.MouseDelegates
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Windows.Input;
namespace fx.Xaml.Charting
{
    public class MouseDelegates
    {
        public IReceiveMouseEvents Target
        {
            get; set;
        }

        internal RenderSynchronizedMouseMove SynchronizedMouseMove
        {
            get; set;
        }

        public MouseButtonEventHandler MouseLeftUpDelegate
        {
            get; set;
        }

        public MouseButtonEventHandler MouseLeftDownDelegate
        {
            get; set;
        }

        public MouseEventHandler MouseMoveDelegate
        {
            get; set;
        }

        public MouseButtonEventHandler MouseRightUpDelegate
        {
            get; set;
        }

        public MouseButtonEventHandler MouseRightDownDelegate
        {
            get; set;
        }

        public MouseButtonEventHandler MouseMiddleDownDelegate
        {
            get; set;
        }

        public MouseButtonEventHandler MouseMiddleUpDelegate
        {
            get; set;
        }

        public MouseWheelEventHandler MouseWheelDelegate
        {
            get; set;
        }

        public MouseEventHandler MouseLeaveDelegate
        {
            get; set;
        }

        public EventHandler<TouchManipulationEventArgs> TouchDownDelegate
        {
            get; set;
        }

        public EventHandler<TouchManipulationEventArgs> TouchMoveDelegate
        {
            get; set;
        }

        public EventHandler<TouchManipulationEventArgs> TouchUpDelegate
        {
            get; set;
        }
    }
}
