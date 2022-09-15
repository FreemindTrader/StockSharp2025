// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.DeferredUIAction
// Assembly: StockSharp.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 333C37E3-F521-4513-E734-AF062F7EDC8B
// Assembly location: T:\00-FreemindTrader\packages\stocksharp.xaml\5.0.135\lib\net6.0-windows7.0\StockSharp.Xaml.dll

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Ecng.Xaml
{
    /// <summary>
    /// Allows to execute some action on UI thread with limited frequency.
    /// </summary>
    public class DeferredUIAction
    {

        private bool _isEnabled = true;

        private readonly TimeSpan _minDelay;

        private readonly DispatcherTimer _dispatcherTimer;

        private readonly Action _uiAction;

        private DateTime _dateTime;

        /// <summary>Create instance.</summary>
        public DeferredUIAction( Action action, TimeSpan minDelay )
        {
            this._dispatcherTimer = new DispatcherTimer();
            this._uiAction = action;
            this._dispatcherTimer.Tick += ( new EventHandler( this.OnTimeTick ) );
            this._minDelay = minDelay;
        }

        /// <summary>Is enabled.</summary>
        public bool IsEnabled
        {
            get
            {
                return this._isEnabled;
            }
            set
            {
                this._isEnabled = value;
            }
        }

        /// <summary>
        /// </summary>
        public void Execute()
        {
            if ( this._dispatcherTimer.IsEnabled )
                return;

            GuiDispatcher.GlobalDispatcher.Dispatcher.GuiAsync( new Action( this.\u0023\u003Dz61J71T1PGRsBNoWviA\u003D\u003D) );
        }

        private void \u0023\u003DzPO4yY7w\u003D()
    {
      this._dispatcherTimer.Stop();
      if (!this.IsEnabled)
        return;
      this._uiAction();
      this._dateTime = DateTime.UtcNow;
    }

    [NullableContext( 1 )]
    private void OnTimeTick( [Nullable( 2 )] object _param1, EventArgs _param2 )
    {
        this.\u0023\u003DzPO4yY7w\u003D();
    }

    private void \u0023\u003Dz61J71T1PGRsBNoWviA\u003D\u003D()
    {
      if (this._dispatcherTimer.get_IsEnabled())
        return;
    TimeSpan timeSpan = DateTime.UtcNow - this._dateTime;
      if (timeSpan < this._minDelay)
      {
        this._dispatcherTimer.set_Interval( this._minDelay - timeSpan );
        this._dispatcherTimer.Start();
    }
      else
        this.\u0023\u003DzPO4yY7w\u003D();
    }
}
}
