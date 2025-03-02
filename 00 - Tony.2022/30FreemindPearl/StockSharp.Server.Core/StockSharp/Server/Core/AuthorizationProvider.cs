
using Ecng.Collections;
using Ecng.Security;

namespace StockSharp.Server.Core
{
    /// <summary>
    /// <see cref="T:Ecng.Security.IAuthorization" /> provider.
    ///     </summary>
    public class AuthorizationProvider : CachedSynchronizedDictionary<AuthorizationModes, IAuthorization>
    {
    }
}
