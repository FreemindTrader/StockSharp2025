using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Ecng.Xaml
{
    /// <summary>Delay helper.</summary>
    public class DelayActionHelper
    {

        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();

        private EventHandler _eventHandler;

        /// <summary>
        /// </summary>
        public double Interval
        {
            get
            {
                return ( double )this._dispatcherTimer.Interval.Milliseconds;
            }
            set
            {
                this._dispatcherTimer.Interval = ( TimeSpan.FromMilliseconds( value ) );
                this.Restart();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="action">
        /// </param>
        public void Start( Action action )
        {
            if ( this._eventHandler != null )
            {
                this._dispatcherTimer.Tick -= ( this._eventHandler );
            }
                
            this._eventHandler = ( p, e ) => { 
                                            this.Stop(); 
                                            action( ); 
                                        };

            this._dispatcherTimer.Tick += ( this._eventHandler );
            
            this.Restart();
        }

        /// <summary>
        /// </summary>
        public void Restart()
        {
            this._dispatcherTimer.Stop();
            this._dispatcherTimer.Start();
        }

        /// <summary>
        /// </summary>
        public void Stop()
        {
            this._dispatcherTimer.Stop();
        }

        private sealed class SomeLambda
        {
            public DelayActionHelper _delayActionHelper;
            public Action _myAction;


            internal void OnEventHandler( object p1, EventArgs e )
            {
                this._delayActionHelper.Stop();
                this._myAction();
            }
        }
    }
}
