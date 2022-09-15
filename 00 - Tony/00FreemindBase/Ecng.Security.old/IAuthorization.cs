using System.Net;
using System.Security;

namespace Ecng.Security
{
    public interface IAuthorization
    {
        string ValidateCredentials( string login, SecureString password, IPAddress clientAddress );
    }
}
