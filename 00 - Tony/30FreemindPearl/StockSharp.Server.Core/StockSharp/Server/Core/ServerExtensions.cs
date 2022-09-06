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
            PermissionCredentials permissionCredentials1 = new PermissionCredentials();
            permissionCredentials1.Email = message.Login;
            permissionCredentials1.Password = message.Password;
            PermissionCredentials permissionCredentials2 = permissionCredentials1;
            permissionCredentials2.IpRestrictions = message.IpRestrictions.ToArray();
            foreach ( KeyValuePair<UserPermissions, IDictionary<Tuple<string, string, object, DateTime?>, bool>> permission in message.Permissions )
            {
                SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool> synchronizedDictionary = new SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool>();
                ( ( ICollection<KeyValuePair<Tuple<string, string, object, DateTime?>, bool>> )synchronizedDictionary ).AddRange( permission.Value );
                permissionCredentials2.Permissions.Add( permission.Key, synchronizedDictionary );
            }
            return permissionCredentials2;
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
            foreach ( KeyValuePair<UserPermissions, SynchronizedDictionary<Tuple<string, string, object, DateTime?>, bool>> permission in credentials.Permissions )
                userInfoMessage.Permissions.Add( permission.Key, ( ( IEnumerable<KeyValuePair<Tuple<string, string, object, DateTime?>, bool>> )permission.Value ).ToDictionary() );
            return userInfoMessage;
        }
    }
}
