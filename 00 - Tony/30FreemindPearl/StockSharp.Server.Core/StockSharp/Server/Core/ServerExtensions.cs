using Ecng.Collections;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace StockSharp.Server.Core
{
    /// <summary>Extensions.</summary>
    public static class ServerExtensions
    {
        /// <summary>
        /// Convert <see cref="T:StockSharp.Messages.UserInfoMessage" /> to <see cref="T:StockSharp.Server.Core.PermissionCredentials" /> value.
        /// </summary>
        /// <param name="message">The message contains information about user.</param>
        /// <returns>Credentials with set of permissions.</returns>
        public static PermissionCredentials ToCredentials( this UserInfoMessage message )
        {
            if ( message == null )
                throw new ArgumentNullException( nameof( message ) );
            
            var credential            = new PermissionCredentials();
            credential.Email          = message.Login;
            credential.Password       = message.Password;            
            credential.IpRestrictions = message.IpRestrictions.ToArray();

            

            foreach ( var permission in message.Permissions )
            {                
                var permissionDict = new SynchronizedDictionary<UserPermissions, IDictionary<(string, string, string, DateTime?), bool>>();                

                permissionDict.Add( permission.Key, permission.Value );

                credential.Permissions.Add( permission.Key, permission.Value );
            }


            return credential;
        }

        /// <summary>
        /// Convert <see cref="T:StockSharp.Server.Core.PermissionCredentials" /> to <see cref="T:StockSharp.Messages.UserInfoMessage" /> value.
        /// </summary>
        /// <param name="credentials">Credentials with set of permissions.</param>
        /// <param name="copyPassword">Copy <see cref="P:Ecng.ComponentModel.ServerCredentials.Password" /> value.</param>
        /// <returns>The message contains information about user.</returns>
        public static UserInfoMessage ToUserInfoMessage( this PermissionCredentials credentials, bool copyPassword )
        {
            if ( credentials == null )
                throw new ArgumentNullException( nameof( credentials ) );
            
            UserInfoMessage userInfoMessage = new UserInfoMessage()
            {
                Login = credentials.Email,
                IpRestrictions = credentials.IpRestrictions.ToArray()
            };

            if ( copyPassword )
                userInfoMessage.Password = credentials.Password;

            foreach( var permission in credentials.Permissions )
            {
                userInfoMessage.Permissions.Add( permission.Key, permission.Value );
            }
                
                    
            return userInfoMessage;
        }
    }
}
