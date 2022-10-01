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

            GuiDispatcher.GlobalDispatcher.Dispatcher.GuiAsync( new Action( this.ExecuteAction ) );
        }

        private void TimeTickAction()
        {
            this._dispatcherTimer.Stop();
            if ( !this.IsEnabled )
                return;
            this._uiAction();
            this._dateTime = DateTime.UtcNow;
        }

        private void OnTimeTick( object _param1, EventArgs _param2 )
        {
            this.TimeTickAction();
        }

        private void ExecuteAction()
        {
            if ( this._dispatcherTimer.IsEnabled )
                return;
            TimeSpan timeSpan = DateTime.UtcNow - this._dateTime;
            if ( timeSpan < this._minDelay )
            {
                this._dispatcherTimer.Interval = ( this._minDelay - timeSpan );
                this._dispatcherTimer.Start();
            }
            else
                this.TimeTickAction();
        }
    }
}
