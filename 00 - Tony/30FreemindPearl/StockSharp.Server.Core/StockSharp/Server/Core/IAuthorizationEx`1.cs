using Ecng.Security;
using System.Net;
using System.Security;

namespace StockSharp.Server.Core
{
    /// <summary>
    /// Extended version <see cref="T:Ecng.Security.IAuthorization" />.
    /// </summary>
    public interface IAuthorizationEx<TSession> : IAuthorization where TSession : IMessageListenerSession
    {
        /// <summary>Validate credentials.</summary>
        /// <param name="serverName">Server name.</param>
        /// <param name="sessions">Server sessions.</param>
        /// <param name="version">Client app version.</param>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <param name="clientAddress">Client address.</param>
        /// <returns>Session ID and server session.</returns>
        (string sessionId, TSession session) ValidateCredentials( string serverName, TSession[ ] sessions, string version, string login, SecureString password, IPAddress clientAddress );

        /// <summary>Close session.</summary>
        /// <param name="session">Session.</param>
        void Close( IMessageListenerSession session );
    }
}
