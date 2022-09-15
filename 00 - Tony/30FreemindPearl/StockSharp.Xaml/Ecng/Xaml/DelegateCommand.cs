using System;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    public class DelegateCommand : DelegateCommand<object>
    {
        /// <summary>
        /// </summary>
        public DelegateCommand( Action<object> execute, Predicate<object> canExecute = null )
          : base( execute, canExecute )
        {
        }
    }
}
