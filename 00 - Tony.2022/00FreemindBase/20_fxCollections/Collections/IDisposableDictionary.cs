using System;

namespace fx.Collections
{
    public interface IAdvancedDisposable : IDisposable
    {
        /// <summary>
        /// Gets a value indicating if the object was already disposed.
        /// </summary>
        bool WasDisposed
        {
            get;
        }
    }

    /// <summary>
    /// Interface that must be implemented by IPfzDictionaries that also support dispose. This is used, for example, by
    /// the WeakDictionaries and also by the "Locked" dictionaries, which release the lock over the main dictionary when
    /// disposed.
    /// </summary>
    public interface IDisposableDictionary : IPfzDictionary, IAdvancedDisposable
    {
    }

    /// <summary>
    /// Interface that must be implemented by IPfzDictionaries that also support dispose. This is used, for example, by
    /// the WeakDictionaries and also by the "Locked" dictionaries, which release the lock over the main dictionary when
    /// disposed.
    /// </summary>
    public interface IDisposableDictionary< TKey, TValue > : IPfzDictionary< TKey, TValue >, IDisposableDictionary
    {
    }
}