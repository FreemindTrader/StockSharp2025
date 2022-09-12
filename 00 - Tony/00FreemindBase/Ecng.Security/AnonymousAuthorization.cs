using Ecng.Common;
using System;
using System.Net;
using System.Security;

namespace Ecng.Security
{
    public class AnonymousAuthorization : IAuthorization
    {
        public virtual string ValidateCredentials( string login, SecureString password, IPAddress clientAddress )
        {
            return Guid.NewGuid().To<string>();
        }
    }
}
