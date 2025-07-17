// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Common.Helpers.TimedMethod
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Windows.Threading;

namespace fx.Xaml.Charting
{
    public class TimedMethod : IDisposable
    {
        private int _milliseconds = 100;
        private DispatcherPriority _priority = DispatcherPriority.Background;
        private readonly Action _action;
        private DispatcherTimer _timer;

        private TimedMethod( Action action )
        {
            this._action = action;
        }

        public static TimedMethod Invoke( Action action )
        {
            return new TimedMethod( action );
        }

        public TimedMethod WithPriority( DispatcherPriority priority )
        {
            this._priority = priority;
            return this;
        }

        public TimedMethod After( int milliseconds )
        {
            this._milliseconds = milliseconds;
            return this;
        }

        public void Dispose()
        {
            if ( this._timer == null )
                return;
            this._timer.Stop();
            this._timer = ( DispatcherTimer ) null;
        }

        public TimedMethod Go()
        {
            if ( this._milliseconds <= 0 )
            {
                this._action();
                return this;
            }
            this._timer = new DispatcherTimer( this._priority );
            this._timer.Interval = TimeSpan.FromMilliseconds( ( double ) this._milliseconds );
            this._timer.Tick += ( EventHandler ) ( ( s, e ) =>
            {
                this._action();
                this._timer.Stop();
            } );
            this._timer.Start();
            return this;
        }
    }
}
