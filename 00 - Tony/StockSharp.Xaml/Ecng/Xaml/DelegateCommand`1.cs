using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Ecng.Xaml
{
    /// <summary>
    /// Delegate command capable of taking argument.
    /// <typeparam name="T">The argument type.</typeparam></summary>
    public class DelegateCommand<T> : ICommand
    {

        private readonly Action<T> _action;

        private readonly Predicate<T> _predicate;

        private readonly EventHandler _eventHandler;

        /// <summary>
        /// Creates a new command with conditional execution.
        /// <param name="execute">The execution logic.</param><param name="canExecute">The execution status logic.</param></summary>
        public DelegateCommand( Action<T> execute, Predicate<T> canExecute = null )
        {
            Action<T> action = execute;
            if ( action == null )
                throw new ArgumentNullException( nameof( action ) );
            this._action = action;
            this._predicate = canExecute;
            if ( canExecute == null )
                return;
            this._eventHandler = new EventHandler( this.MyEventHandler );
            CommandManager.RequerySuggested += this._eventHandler;
        }

        private void MyEventHandler( object o, EventArgs e )
        {
            EventHandler h = _eventHandler;
            if ( h == null )
                return;
            h( ( object )this, e );
        }

        /// <inheritdoc />
        public void Execute( object parameter )
        {
            this._action( ( T )parameter );
        }

        /// <inheritdoc />
        public bool CanExecute( object parameter )
        {
            if ( this._predicate != null )
                return this._predicate( ( T )parameter );
            return true;
        }

        /// <summary>
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}
