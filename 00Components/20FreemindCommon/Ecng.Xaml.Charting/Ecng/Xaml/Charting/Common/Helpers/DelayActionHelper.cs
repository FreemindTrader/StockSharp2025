// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Helpers.DelayActionHelper
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows.Threading;

namespace Ecng.Xaml.Charting
{
    internal class DelayActionHelper
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private EventHandler _eventHandler;

        public double Interval
        {
            get
            {
                return ( double ) this._timer.Interval.Milliseconds;
            }
            set
            {
                this._timer.Interval = TimeSpan.FromMilliseconds( value );
                this.Restart();
            }
        }

        public void Start( Action action )
        {
            if ( this._eventHandler != null )
                this._timer.Tick -= this._eventHandler;
            this._eventHandler = ( EventHandler ) ( ( sender, args ) =>
            {
                this.Stop();
                action();
            } );
            this._timer.Tick += this._eventHandler;
            this.Restart();
        }

        public void Restart()
        {
            this._timer.Stop();
            this._timer.Start();
        }

        public void Stop()
        {
            this._timer.Stop();
        }
    }
}
