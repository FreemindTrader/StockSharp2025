
using StockSharp.Logging;
using System;
using System.Net;

namespace StockSharp.Server.Core
{
    /// <summary>
    /// The interface describing a session, create by <see cref="T:StockSharp.Server.Core.IMessageListener" />.
    /// </summary>
    public interface IMessageListenerSession : ILogReceiver, ILogSource, IDisposable
    {
        /// <summary>Identifier.</summary>
        string SessionId { get; }

        /// <summary>Server session.</summary>
        IMessageListenerSession ServerSession { get; }

        /// <summary>Is connected.</summary>
        bool IsConnected { get; }

        /// <summary>The number of errors.</summary>
        int ErrorCount { get; }

        /// <summary>The number of bytes received.</summary>
        long BytesReceived { get; }

        /// <summary>The number of bytes sent.</summary>
        long BytesSent { get; }

        /// <summary>Creation time.</summary>
        DateTimeOffset CreationTime { get; }

        /// <summary>Updated time.</summary>
        DateTimeOffset UpdatedTime { get; }

        /// <summary>Address.</summary>
        EndPoint Address { get; }

        /// <summary>Authorization token.</summary>
        string AuthorizationToken { get; }

        /// <summary>Client app version.</summary>
        string Version { get; }

        /// <summary>Client app language.</summary>
        string Language { get; }
    }
}
