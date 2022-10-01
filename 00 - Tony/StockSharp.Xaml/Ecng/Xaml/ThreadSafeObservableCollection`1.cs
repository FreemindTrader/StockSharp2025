using Ecng.Collections;
using Ecng.ComponentModel;
using Ecng.Configuration;

namespace Ecng.Xaml
{
    /// <summary>
    /// </summary>
    public class ThreadSafeObservableCollection<TItem> : DispatcherObservableCollection<TItem>
    {
        public ThreadSafeObservableCollection( IListEx<TItem> items ) : this( ( IDispatcher )( ConfigManager.GetService<IDispatcher>() ?? GuiDispatcher.GlobalDispatcher ), items )
        {
        }

        public ThreadSafeObservableCollection( IDispatcher dispatcher, IListEx<TItem> items ) : base( dispatcher, items )
        {
            
        }
    }
}
