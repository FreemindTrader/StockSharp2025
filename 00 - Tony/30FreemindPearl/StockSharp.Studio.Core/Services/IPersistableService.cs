using System;

namespace StockSharp.Studio.Core.Services
{
    public interface IPersistableService
    {
        bool ContainsKey( string key );

        TValue GetValue<TValue>( string key, TValue defaultValue = default );

        void SetValue( string key, object value );

        void SetDelayValue( string key, Func<object> value );
    }
}
