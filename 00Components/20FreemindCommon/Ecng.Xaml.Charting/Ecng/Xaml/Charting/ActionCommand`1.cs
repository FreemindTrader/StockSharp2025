// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ActionCommand`1
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Windows.Input;

namespace StockSharp.Xaml.Charting
{
    public class ActionCommand<T> : ICommand where T : class
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public ActionCommand( Action<T> execute )
          : this( execute, ( Predicate<T> ) null )
        {
        }

        public ActionCommand( Action<T> execute, Predicate<T> canExecute )
        {
            if ( execute == null )
                throw new ArgumentNullException( "execute cannot be null" );
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute( object parameter )
        {
            if ( this._canExecute != null )
                return this._canExecute( ( T ) parameter );
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler canExecuteChanged = this.CanExecuteChanged;
            if ( canExecuteChanged == null )
                return;
            canExecuteChanged( ( object ) this, EventArgs.Empty );
        }

        public void Execute( object parameter )
        {
            T obj = parameter as T;
            if ( parameter != null && ( object ) obj == null )
                throw new InvalidOperationException( "Wrong type of parameter being passed in.  Expected [" + ( object ) typeof( T ) + "]but was [" + ( object ) parameter.GetType() + "]" );
            if ( !this.CanExecute( ( object ) obj ) )
                throw new InvalidOperationException( "Should not try to execute command that cannot be executed" );
            this._execute( obj );
        }
    }
}
