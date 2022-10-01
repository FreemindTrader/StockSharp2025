using Ecng.ComponentModel;
using Ecng.Configuration;
using System.ComponentModel;

namespace Ecng.Xaml
{
    /// <summary>
    ///   <see cref="T:Ecng.ComponentModel.DispatcherNotifiableObject`1" />
    /// </summary>
    public class NotifiableObjectGuiWrapper<T> : DispatcherNotifiableObject<T> where T : class, INotifyPropertyChanged
    {
        /// <summary>
        /// </summary>
        /// <param name="value">
        /// </param>
        public NotifiableObjectGuiWrapper( T value ) : base( ConfigManager.GetService<IDispatcher>(), value )
        {
        }
    }
}
