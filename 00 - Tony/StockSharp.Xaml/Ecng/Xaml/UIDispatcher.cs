using Ecng.ComponentModel;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    public class UIDispatcher : IDispatcher
    {

        private readonly Dispatcher _uiDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Ecng.Xaml.UIDispatcher" />.
        /// </summary>
        /// <param name="d">
        ///   <see cref="T:System.Windows.Threading.Dispatcher" />
        /// </param>
        public UIDispatcher( Dispatcher d )
        {            
            if ( d == null )
                throw new ArgumentNullException( nameof( d ) );

            this._uiDispatcher = d;
        }

        bool IDispatcher.CheckAccess()
        {
            return this._uiDispatcher.CheckAccess();
        }

        void IDispatcher.Invoke(Action _param1)
        {
            this._uiDispatcher.Invoke( _param1 );
        }

        void IDispatcher.InvokeAsync(Action _param1)
        {
            this._uiDispatcher.InvokeAsync( _param1 );
        }
    }
}
