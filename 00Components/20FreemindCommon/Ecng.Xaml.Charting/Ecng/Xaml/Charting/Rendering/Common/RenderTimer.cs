// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Rendering.Common.RenderTimer
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Windows.Media;
using System.Windows.Threading;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Utility.Mouse;

namespace StockSharp.Xaml.Charting.Rendering.Common
{
    internal class RenderTimer : IDisposable
    {
        private readonly IDispatcherFacade _dispatcher;
        private readonly Action _renderOperation;
        private DispatcherTimer _timer;
        private volatile bool _isInRenderOperation;
        private volatile bool _disposed;

        internal RenderTimer( double? maxFrameRate, IDispatcherFacade dispatcher, Action renderOperation )
        {
            this._dispatcher = dispatcher;
            this._renderOperation = renderOperation;
            if ( maxFrameRate.HasValue )
            {
                this._timer = new DispatcherTimer();
                this._timer.Interval = TimeSpan.FromMilliseconds( 1000.0 / NumberUtil.Constrain( maxFrameRate.Value, 0.0, 100.0 ) );
                this._timer.Tick += new EventHandler( this.TimerElapsed );
                this._timer.Start();
            }
            else
            {
                CompositionTarget.Rendering -= new EventHandler( this.OnCompositionTargetRendering );
                CompositionTarget.Rendering += new EventHandler( this.OnCompositionTargetRendering );
            }
        }

        private void TimerElapsed( object sender, EventArgs e )
        {
            if ( this._isInRenderOperation )
                return;
            this._isInRenderOperation = true;
            CompositionSyncedDelegate compositionSyncedDelegate = new CompositionSyncedDelegate(new Action(this.Callback));
        }

        private void Callback()
        {
            try
            {
                this._renderOperation();
            }
            finally
            {
                this._isInRenderOperation = false;
            }
        }

        private void OnCompositionTargetRendering( object sender, EventArgs e )
        {
            if ( this._isInRenderOperation )
                return;
            this._isInRenderOperation = true;
            this.Callback();
        }

        public void Dispose()
        {
            lock ( this )
            {
                if ( this._timer != null )
                {
                    this._timer.Stop();
                    this._timer.Tick -= new EventHandler( this.TimerElapsed );
                    this._timer = ( DispatcherTimer ) null;
                }
                else if ( !this._disposed )
                    this._dispatcher.BeginInvokeIfRequired( ( Action ) ( () => CompositionTarget.Rendering -= new EventHandler( this.OnCompositionTargetRendering ) ), DispatcherPriority.Normal );
                this._disposed = true;
            }
        }
    }
}
