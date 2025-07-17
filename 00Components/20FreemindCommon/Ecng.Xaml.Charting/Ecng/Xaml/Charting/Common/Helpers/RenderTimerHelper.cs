// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Common.Helpers.RenderTimerHelper
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using StockSharp.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Charting.Utility;

namespace StockSharp.Xaml.Charting.Common.Helpers
{
    internal class RenderTimerHelper
    {
        private RenderTimer _renderTimer;
        private volatile bool _needToRedraw;
        private readonly Action _action;
        private readonly IDispatcherFacade _dispatcher;

        public RenderTimerHelper( Action action, IDispatcherFacade dispatcher )
        {
            this._action = action;
            this._dispatcher = dispatcher;
        }

        public void OnLoaded()
        {
            this.StopTimer();
            this.StartTimer();
            this.OnRenderTimeElapsed();
        }

        private void StopTimer()
        {
            if ( this._renderTimer == null )
                return;
            this._renderTimer.Dispose();
            this._renderTimer = ( RenderTimer ) null;
        }

        private void StartTimer()
        {
            this._renderTimer = new RenderTimer( new double?(), this._dispatcher, new Action( this.OnRenderTimeElapsed ) );
        }

        public void OnUnlodaed()
        {
            this.StopTimer();
        }

        private void OnRenderTimeElapsed()
        {
            if ( !this._needToRedraw )
                return;
            try
            {
                this._action();
            }
            finally
            {
                this._needToRedraw = false;
            }
        }

        public void Invalidate()
        {
            this._needToRedraw = true;
        }
    }
}
