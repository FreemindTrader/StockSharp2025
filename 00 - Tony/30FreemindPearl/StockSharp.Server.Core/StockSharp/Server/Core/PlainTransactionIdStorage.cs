using Ecng.Common;
using System;

namespace StockSharp.Server.Core
{
    /// <summary>
    /// Plain implementation of <see cref="T:StockSharp.Server.Core.ITransactionIdStorage" />.
    /// </summary>
    public class PlainTransactionIdStorage : ITransactionIdStorage
    {
        ISessionTransactionIdStorage ITransactionIdStorage.Get( string _param1, bool _param2 )
        {
            return new PlainSessionTransactionIdStorage();
        }

        private sealed class PlainSessionTransactionIdStorage : ISessionTransactionIdStorage
        {
            string ISessionTransactionIdStorage.TryGetRequestId( long _param1 )
            {
                return _param1.To<string>();
            }

            string ISessionTransactionIdStorage.CreateRequestId() => DateTime.UtcNow.Ticks.To<string>();

            bool ISessionTransactionIdStorage.RemoveRequestId( string _param1 )
            {
                return true;
            }

            long? ISessionTransactionIdStorage.TryGetTransactionId( string _param1 )
            {
                return new long?( _param1.To<long>() );
            }

            long ISessionTransactionIdStorage.CreateTransactionId( string _param1 )
            {
                return _param1.To<long>();
            }

            bool ISessionTransactionIdStorage.RemoveTransactionId( long _param1 )
            {
                return true;
            }
        }
    }
}
