
using Ecng.Common;
using Ecng.Security;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security;

namespace StockSharp.Server.Core
{
    /// <summary>
    /// The module of the connection access check based on the <see cref="T:StockSharp.Server.Core.PermissionCredentialsStorage" /> authorization.
    /// </summary>
    public class PermissionCredentialsAuthorization : IAuthorization
    {
        
        private readonly PermissionCredentialsStorage _permissionCredentialsStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockSharp.Server.Core.PermissionCredentialsAuthorization" />.
        /// </summary>
        /// <param name="storage">Storage for <see cref="T:StockSharp.Server.Core.PermissionCredentials" />.</param>
        public PermissionCredentialsAuthorization( PermissionCredentialsStorage storage ) => _permissionCredentialsStorage = storage ?? throw new ArgumentNullException( nameof( storage ) );

        string IAuthorization.ValidateCredentials( string login, SecureString password, IPAddress clientAddress )
        {
            if ( login.IsEmpty() )
                throw new ArgumentNullException( nameof( login ), LocalizedStrings.Str1896 );
            if ( password.IsEmpty() )
                throw new ArgumentNullException( nameof( password ), LocalizedStrings.Str1897 );

            PermissionCredentials byLogin = _permissionCredentialsStorage.TryGetByLogin( login );

            if ( byLogin == null || !byLogin.Password.IsEqualTo( password ) )
                throw new UnauthorizedAccessException( LocalizedStrings.WrongLoginOrPassword );
            IPAddress[ ] array = byLogin.IpRestrictions.ToArray();
            if ( array.Length != 0 && ( clientAddress == null || !array.Contains( clientAddress ) ) )
                throw new UnauthorizedAccessException( LocalizedStrings.IpAddrNotValid.Put( clientAddress ) );
            return Guid.NewGuid().To<string>();
        }
    }
}
